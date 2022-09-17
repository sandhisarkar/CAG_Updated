using LItems;
using Microsoft.Office.Interop.Excel;
using NovaNet.Utils;
using NovaNet.wfe;
using System;
using System.Collections;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using Constants = NovaNet.Utils.Constants;

namespace ImageHeaven
{
    public partial class frmBatchReport : Form
    {
        private ImageConfig config = null;
        private static string docType;

        OdbcConnection sqlCon = null;
        NovaNet.Utils.dbCon dbcon = null;
        CtrlPolicy pPolicy = null;
        private CtrlImage pImage = null;
        wfePolicy wPolicy = null;
        wfeImage wImage = null;
        private string boxNo = null;
        private string policyNumber = null;
        private string projCode = null;
        private string batchCode = null;
        private string picPath = null;
        private udtPolicy policyData = null;
        string policyPath = null;
        private int policyStatus = 0;
        private int clickedIndexValue;
        private CtrlBox pBox = null;
        private int selBoxNo;
        string[] imageName;
        int policyRowIndex;
        //private CtrlBatch pBatch = null;

        //private MagickNet.Image imgQc;
        string imagePath = null;
        string photoPath = null;
        //private CtrlBox pBox=null;
        private Imagery img;
        private Imagery imgAll;
        private Credentials crd = new Credentials();
        public static NovaNet.Utils.exLog.Logger exMailLog = new NovaNet.Utils.exLog.emailLogger("./errLog.log", NovaNet.Utils.exLog.LogLevel.Dev, Constants._MAIL_TO, Constants._MAIL_FROM, Constants._SMTP);
        public static NovaNet.Utils.exLog.Logger exTxtLog = new NovaNet.Utils.exLog.txtLogger("./errLog.log", NovaNet.Utils.exLog.LogLevel.Dev);
        private string imgFileName = string.Empty;
        private int zoomWidth;
        private int zoomHeight;
        private Size zoomSize = new Size();
        private int keyPressed = 1;
        //private DataTable gTable;
        ihwQuery wQ;
        private string selDocType = string.Empty;
        private int currntPg = 0;
        private bool firstDoc = true;
        private string prevDoc;
        private int policyLen = 0;

        private OdbcDataAdapter sqlAdap = null;

        public static string currStage = string.Empty;

        public static string category = string.Empty;

        public frmBatchReport()
        {
            InitializeComponent();
        }
        public frmBatchReport(OdbcConnection prmCon)
        {
            InitializeComponent();
            sqlCon = prmCon;
            
        }
        private void frmBatchReport_Load(object sender, EventArgs e)
        {
            PopulateProjectCombo();
        }

        private void PopulateProjectCombo()
        {
            DataSet ds = new DataSet();

            dbcon = new NovaNet.Utils.dbCon();

            wfeProject tmpProj = new wfeProject(sqlCon);
            //cmbProject.Items.Add("Select");
            ds = tmpProj.GetAllValues();
            if (ds.Tables[0].Rows.Count > 0)
            {
                deComboBox1.DataSource = ds.Tables[0];
                deComboBox1.DisplayMember = ds.Tables[0].Columns[1].ToString();
                deComboBox1.ValueMember = ds.Tables[0].Columns[0].ToString();
            }
        }

        private void deComboBox1_Leave(object sender, EventArgs e)
        {
            PopulateBatchCombo();
        }

        private void PopulateBatchCombo()
        {
            string projKey = null;
            DataSet ds = new DataSet();

            dbcon = new NovaNet.Utils.dbCon();
            NovaNet.wfe.eSTATES[] bState = new NovaNet.wfe.eSTATES[2];
            wfeBatch tmpBatch = new wfeBatch(sqlCon);
            if (deComboBox1.SelectedValue != null)
            {
                projKey = deComboBox1.SelectedValue.ToString();
                projCode = projKey;
                wQ = new ihwQuery(sqlCon);

                ds = GetAllValues(Convert.ToInt32(projKey));


                if (ds.Tables[0].Rows.Count > 0)
                {
                    deComboBox2.DataSource = ds.Tables[0];
                    deComboBox2.DisplayMember = ds.Tables[0].Columns[1].ToString();
                    deComboBox2.ValueMember = ds.Tables[0].Columns[0].ToString();
                }
                else
                {
                    deComboBox2.DataSource = ds.Tables[0];
                }
            }
        }

        public System.Data.DataSet GetAllValues(int prmProjectKey)
        {
            string sqlStr = null;

            DataSet batchDs = new DataSet();

            try
            {
                
                sqlStr = "select batch_key,batch_code from batch_master where proj_code=" + prmProjectKey + " and status >="+2+" order by batch_code"; 

                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(batchDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();

                exMailLog.Log(ex);
            }
            return batchDs;
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            if(deComboBox1.Text != null && deComboBox2.Text !=null)
            {


                grdStatus.DataSource = null;
                System.Data.DataTable Dt = new System.Data.DataTable();

               
                Dt = _GetResultBundle(deComboBox1.SelectedValue.ToString(),deComboBox2.SelectedValue.ToString());

                grdStatus.DataSource = Dt;

                Dt.Columns.Add("Image Count");

                for(int i=0;i<Dt.Rows.Count;i++)
                {
                    string filename = _GetResultBundle(deComboBox1.SelectedValue.ToString(), deComboBox2.SelectedValue.ToString()).Rows[i][1].ToString();
                    Dt.Rows[i]["Image Count"] = Dt.Rows[i]["Image Count"].ToString() + _GetImageCount(deComboBox1.SelectedValue.ToString(), deComboBox2.SelectedValue.ToString(), filename).Rows[0][0].ToString();
                }

                if (Dt.Rows.Count > 0)
                {
                    deButton20.Enabled = true;
                }
                else
                {
                    deButton20.Enabled = false;
                }

                FormatDataGridView();

                
            }
        }
        private void FormatDataGridView()
        {
            


            //Set Autosize on for all the columns
            for (int i = 0; i < grdStatus.Columns.Count; i++)
            {
                grdStatus.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }


        }
        public System.Data.DataTable _GetResultBundle(string proj_key, string batch_key)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string sql = "select  b.batch_code as 'Batch Code',a.filename as 'File Name' from  metadata_entry a, batch_master b where a.proj_code = b.proj_code and a.batch_key = b.batch_key and a.proj_code = '" + proj_key+ "' and a.batch_key='"+batch_key+"'";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }
        public System.Data.DataTable _GetImageCount(string proj_key, string batch_key,string filename)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string sql = "select Count(distinct page_name) from image_master where proj_key = '" + proj_key + "' and batch_key='" + batch_key + "' and policy_number = '"+filename+"' and status <> 29";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        private void deButton21_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public System.Data.DataTable _GetBundle(string proj, string bundle)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string sql = "select distinct proj_code,batch_key,batch_code as 'Batch Code',batch_name as 'Batch Name' from batch_master where proj_code = '" + proj + "' and batch_key = '" + bundle + "' ";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }
        public int GetTotalImageCount(string projKey, string bundleKey)
        {
            string sqlStr = null;
            DataSet projDs = new DataSet();
            int count;

            try
            {
                sqlStr = @"select count(*) from image_master where proj_key=" + projKey + " and batch_key=" + bundleKey +" and status <> 29";
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(projDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();

                //stateLog = new MemoryStream();
                //tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                //stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex);
            }
            if (projDs.Tables[0].Rows.Count > 0)
            {
                count = Convert.ToInt32(projDs.Tables[0].Rows[0][0].ToString());
            }
            else
                count = 0;

            return count;
        }
        private void deButton20_Click(object sender, EventArgs e)
        {
            if (grdStatus.Rows.Count>0)
            {
                Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);

                Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

                app.Visible = false;

                worksheet = (Microsoft.Office.Interop.Excel._Worksheet)workbook.Sheets["Sheet1"];


                worksheet = (Microsoft.Office.Interop.Excel._Worksheet)workbook.ActiveSheet;

                worksheet.Name = "Batch Wise Report";

                worksheet.Cells[1, 2] = "Batch Report";
                Range range44 = worksheet.get_Range("B1");
                range44.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.YellowGreen);

                worksheet.Rows.AutoFit();
                worksheet.Columns.AutoFit();


                worksheet.Cells[3, 1] = "Batch Name : " + _GetBundle(deComboBox1.SelectedValue.ToString(), deComboBox2.SelectedValue.ToString()).Rows[0][2].ToString();
                Range range43 = worksheet.get_Range("A3");
                range43.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
                worksheet.Rows.AutoFit();
                worksheet.Columns.AutoFit();

                worksheet.Cells[4, 1] = "Total Image Count : " + GetTotalImageCount(deComboBox1.SelectedValue.ToString(), deComboBox2.SelectedValue.ToString()).ToString();
                Range range33 = worksheet.get_Range("A4");
                range33.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
                worksheet.Rows.AutoFit();
                worksheet.Columns.AutoFit();

                Range range = worksheet.get_Range("A3", "A4");
                range.Borders.Color = ColorTranslator.ToOle(Color.Black);


                Range range1 = worksheet.get_Range("A6", "C6");
                range1.Borders.Color = ColorTranslator.ToOle(Color.Black);

                for (int i = 1; i < grdStatus.Columns.Count + 1; i++)
                {


                    Range range2 = worksheet.get_Range("A6", "C6");
                    range2.Borders.Color = ColorTranslator.ToOle(Color.Black);
                    range2.EntireRow.AutoFit();
                    range2.EntireColumn.AutoFit();
                    worksheet.Cells[6, i] = grdStatus.Columns[i - 1].HeaderText;
                }

                for (int i = 0; i < grdStatus.Rows.Count; i++)
                {
                    for (int j = 0; j < grdStatus.Columns.Count; j++)
                    {
                        Range range3 = worksheet.Cells;
                        //range3.Borders.Color = ColorTranslator.ToOle(Color.Black);
                        range3.EntireRow.AutoFit();
                        range3.EntireColumn.AutoFit();
                        worksheet.Cells[i + 7, j + 1] = grdStatus.Rows[i].Cells[j].Value.ToString();
                        worksheet.Cells[i + 7, j + 1].Borders.Color = ColorTranslator.ToOle(Color.Black);

                    }

                }

                string namexls = _GetBundle(deComboBox1.SelectedValue.ToString(), deComboBox2.SelectedValue.ToString()).Rows[0][2].ToString()+"_Batch_Wise_Report" + ".xls";
                string path = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
                sfdUAT.Filter = "Xls files (*.xls)|*.xls";
                sfdUAT.FilterIndex = 2;
                sfdUAT.RestoreDirectory = true;
                sfdUAT.FileName = namexls;
                sfdUAT.ShowDialog();

                workbook.SaveAs(sfdUAT.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                app.Quit();
            }
        }
    }
}
