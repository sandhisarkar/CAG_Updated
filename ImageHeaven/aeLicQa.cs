using LItems;
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

namespace ImageHeaven
{
    public partial class aeLicQa : Form
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
        private DataTable gTable;
        ihwQuery wQ;
        private string selDocType = string.Empty;
        private int currntPg = 0;
        private bool firstDoc = true;
        private string prevDoc;
        private int policyLen = 0;

        private OdbcDataAdapter sqlAdap = null;

        public static string currStage = string.Empty;

        public static string dept = string.Empty;
        public static string category = string.Empty;

        public aeLicQa()
        {
            InitializeComponent();
        }

        public aeLicQa(OdbcConnection prmCon, Credentials prmCrd, string stage)
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            this.Name = "Quality control";
            InitializeComponent();
            sqlCon = prmCon;
            img = IgrFactory.GetImagery(Constants.IGR_CLEARIMAGE);
            //img = IgrFactory.GetImagery(Constants.IGR_GDPICTURE);
            imgAll = IgrFactory.GetImagery(Constants.IGR_CLEARIMAGE);
            //this.Text = "Quality control";
            crd = prmCrd;
            exMailLog.SetNextLogger(exTxtLog);

            currStage = stage;
            if (currStage == "1")
            {
                this.Text = "Quality control (Part - I)";
            }
            else
            {
                this.Text = "Quality control (Part - II)";
            }
            //img = IgrFactory.GetImagery(Constants.IGR_GDPICTURE);			
            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
        }


        private void aeLicQa_Load(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.RealTime;
            System.Windows.Forms.ToolTip bttnToolTip = new System.Windows.Forms.ToolTip();
            System.Windows.Forms.ToolTip otherToolTip = new System.Windows.Forms.ToolTip();
            this.WindowState = FormWindowState.Maximized;
            PopulateProjectCombo();
            rdoShowAll.Checked = true;
            cmdZoomIn.ForeColor = Color.Black;
            cmdZoomOut.ForeColor = Color.Black;
            chkRejectBatch.Visible = false;
            bttnToolTip.SetToolTip(cmdZoomIn, "Shortcut Key- (+)");
            bttnToolTip.SetToolTip(cmdZoomOut, "Shortcut Key- (-)");
            if (currStage == "1")
            {
                textBox1.Text = "100";
                textBox1.Enabled = false;
            }
            else
            {
                textBox1.Text = "25";
                textBox1.Enabled = true;
            }

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
                cmbProject.DataSource = ds.Tables[0];
                cmbProject.DisplayMember = ds.Tables[0].Columns[1].ToString();
                cmbProject.ValueMember = ds.Tables[0].Columns[0].ToString();
            }
        }

        private void cmbProject_Leave(object sender, EventArgs e)
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
            if (cmbProject.SelectedValue != null)
            {
                projKey = cmbProject.SelectedValue.ToString();
                projCode = projKey;
                wQ = new ihwQuery(sqlCon);

                ds = GetAllValues(Convert.ToInt32(projKey));


                if (ds.Tables[0].Rows.Count > 0)
                {
                    cmbBatch.DataSource = ds.Tables[0];
                    cmbBatch.DisplayMember = ds.Tables[0].Columns[1].ToString();
                    cmbBatch.ValueMember = ds.Tables[0].Columns[0].ToString();
                }
                else
                {
                    cmbBatch.DataSource = ds.Tables[0];
                }
            }
        }

        public System.Data.DataSet GetAllValues(int prmProjectKey)
        {
            string sqlStr = null;

            DataSet batchDs = new DataSet();

            try
            {
                if (currStage == "1")
                { sqlStr = "select batch_key,batch_code from batch_master where proj_code=" + prmProjectKey + " and phase1 = 'F' and (status = '6' or status = '7' or status = '8') order by batch_code"; }
                else
                { sqlStr = "select batch_key,batch_code from batch_master where proj_code=" + prmProjectKey + " and phase2 = 'F' and (status = '6' or status = '7') order by batch_code"; }

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

        public DataSet GetAllBox(int prmBatchKey)
        {
            string sqlStr = null;
            DataSet dsBox = new DataSet();
            OdbcDataAdapter sqlAdap = null;

            sqlStr = "select distinct count(filename) as files from metadata_entry where proj_code=" + projCode + " and batch_key=" + prmBatchKey + " ";
            try
            {
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(dsBox);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();

                exMailLog.Log(ex);
            }
            return dsBox;
        }
        public DataSet GetReadyImageCount(eSTATES[] state, eSTATES[] prmPolicyState)
        {
            string sqlStr = null;
            DataSet dsImage = new DataSet();
            OdbcDataAdapter sqlAdap = null;


            sqlStr = "select count(page_name) as page_Count,sum(qc_size) as index_size from image_master A,metadata_entry B" +
                    " where A.proj_key = B.proj_code and A.batch_key = B.batch_key and A.policy_number = B.filename and B.proj_code=" + projCode +
                " and B.batch_key=" + batchCode + " and a.box_number='1' and A.status<>29";
            /*
			for(int j=0;j<state.Length;j++)
			{
				if((int)state[j]!= 0)
				{
					if(j==0)
					{
						sqlStr=sqlStr + " and (A.status=" + (int)state[j] ;
					}
					else
						sqlStr=sqlStr + " or A.status=" + (int)state[j] ;
				}
			}
			sqlStr = sqlStr + " and A.status<>" + (int)eSTATES.PAGE_DELETED + " )";
            */
            for (int j = 0; j < state.Length; j++)
            {
                if ((int)state[j] != 0)
                {
                    if (j == 0)
                    {
                        sqlStr = sqlStr + " and (b.status = 4 or b.status = 40 or B.status=" + (int)prmPolicyState[j];
                    }
                    else
                        sqlStr = sqlStr + " or B.status=" + (int)prmPolicyState[j];
                }
            }
            sqlStr = sqlStr + " )";

            try
            {
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(dsImage);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();

                exMailLog.Log(ex);
            }
            return dsImage;
        }
        public int GetPolicyCount(eSTATES[] state)
        {
            string sqlStr = null;
            DataSet dsImage = new DataSet();
            OdbcDataAdapter sqlAdap = null;

            sqlStr = "select count(*) from metadata_entry " +
                    " where proj_code=" + projCode +
                " and batch_key=" + batchCode + " and (status = 4 or status = 5 or status = 40 or status = 19 or status = 22 or status = 30 or status = 31 ) ";


            try
            {
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(dsImage);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();

                exMailLog.Log(ex);
            }
            if (dsImage.Tables[0].Rows.Count > 0)
            {
                return Convert.ToInt32(dsImage.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }
        private void PopulateBoxDetails()
        {
            string batchKey = null;
            DataSet ds = new DataSet();
            CtrlBox cBox = new CtrlBox((int)cmbProject.SelectedValue, (int)cmbBatch.SelectedValue, "0");
            dbcon = new NovaNet.Utils.dbCon();

            wfeBox tmpBox = new wfeBox(sqlCon, cBox);
            DataTable dt = new DataTable();
            DataSet imageCount = new DataSet();
            DataRow dr;
            int indexPolicyCont = 0;
            double avgSize;
            string totSize;
            string totPage;
            NovaNet.wfe.eSTATES[] state = new NovaNet.wfe.eSTATES[5];
            NovaNet.wfe.eSTATES[] policyState = new NovaNet.wfe.eSTATES[5];

            //dt.Columns.Add("BoxNo");
            dt.Columns.Add("Files");
            dt.Columns.Add("Ready");
            dt.Columns.Add("ScannedPages");
            dt.Columns.Add("Avg_Size");
            dt.Columns.Add("TotalSize");

            if (cmbBatch.SelectedValue != null)
            {
                batchKey = cmbBatch.SelectedValue.ToString();
                batchCode = batchKey;
                ds = GetAllBox(Convert.ToInt32(batchKey));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dr = dt.NewRow();
                        //dr["BoxNo"] = ds.Tables[0].Rows[i]["box_number"];
                        dr["Files"] = ds.Tables[0].Rows[i]["files"].ToString();

                        pPolicy = new CtrlPolicy(Convert.ToInt32(cmbProject.SelectedValue.ToString()), Convert.ToInt32(cmbBatch.SelectedValue.ToString()), "1", "0");
                        wPolicy = new wfePolicy(sqlCon, pPolicy);

                        policyState[0] = NovaNet.wfe.eSTATES.POLICY_INDEXED;
                        policyState[1] = NovaNet.wfe.eSTATES.POLICY_FQC;
                        policyState[2] = NovaNet.wfe.eSTATES.POLICY_CHECKED;
                        policyState[3] = NovaNet.wfe.eSTATES.POLICY_EXCEPTION;
                        policyState[4] = NovaNet.wfe.eSTATES.POLICY_EXPORTED;
                        indexPolicyCont = GetPolicyCount(policyState);

                        dr["Ready"] = indexPolicyCont;

                        pImage = new CtrlImage(Convert.ToInt32(cmbProject.SelectedValue.ToString()), Convert.ToInt32(cmbBatch.SelectedValue.ToString()), "1", "0", string.Empty, string.Empty);
                        wImage = new wfeImage(sqlCon, pImage);

                        state[0] = eSTATES.PAGE_INDEXED;
                        state[1] = eSTATES.PAGE_FQC;
                        state[2] = eSTATES.PAGE_CHECKED;
                        state[3] = eSTATES.PAGE_EXCEPTION;
                        state[4] = eSTATES.PAGE_EXPORTED;
                        //state[5] = eSTATES.PAGE_ON_HOLD;
                        imageCount = GetReadyImageCount(state, policyState);
                        totPage = imageCount.Tables[0].Rows[0]["page_count"].ToString();
                        dr["ScannedPages"] = totPage;
                        totSize = imageCount.Tables[0].Rows[0]["index_size"].ToString();
                        if (totSize != string.Empty)
                        {
                            dr["TotalSize"] = Math.Round(Convert.ToDouble(totSize), 2);
                        }
                        else
                        {
                            dr["TotalSize"] = string.Empty;
                        }

                        if ((totSize != string.Empty) && (totPage != "0"))
                        {
                            avgSize = Math.Round(Convert.ToDouble(totSize) / Convert.ToDouble(totPage), 2);
                            dr["Avg_Size"] = avgSize.ToString();
                        }

                        dt.Rows.Add(dr);
                    }
                    grdBox.DataSource = dt;
                    grdBox.ForeColor = Color.Black;
                }
            }
        }

        private void CheckBatchRejection(string pBatchKey)
        {
            wfeBatch wBatch = new wfeBatch(sqlCon);
            wQ = new ihwQuery(sqlCon);
            if (chkReadyUat.Checked == false)
            {

                if (wBatch.PolicyWithLICException(Convert.ToInt32(cmbProject.SelectedValue.ToString()), Convert.ToInt32(pBatchKey)) == true)
                {
                    chkRejectBatch.Visible = false;
                }
                else
                {
                    chkRejectBatch.Visible = false;
                }

            }
            else
            {
                chkRejectBatch.Visible = false;
            }
        }

        public int GetTotalPolicies(eSTATES prmState)
        {
            string sqlStr = null;
            DataSet dsBox = new DataSet();
            OdbcDataAdapter sqlAdap = null;

            sqlStr = "select filename as filename from metadata_entry where proj_code=" + projCode + " and batch_key=" + batchCode;
            if ((int)prmState == 0)
            {
                sqlStr = sqlStr + " and 1=1 order by filename";
            }
            else
            {
                sqlStr = sqlStr + " and status=" + (int)prmState + " order by filename";
            }

            try
            {
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(dsBox);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();

                exMailLog.Log(ex);
            }

            return dsBox.Tables[0].Rows.Count;
        }
        public int GetTotalPolicies(eSTATES[] prmState)
        {
            string sqlStr = null;
            DataSet dsBox = new DataSet();
            OdbcDataAdapter sqlAdap = null;

            sqlStr = "select filename as filename from metadata_entry where proj_code=" + projCode + " and batch_key=" + batchCode;

            for (int j = 0; j < prmState.Length; j++)
            {
                if ((int)prmState[j] != 0)
                {
                    if (j == 0)
                    {
                        sqlStr = sqlStr + " and (status=" + (int)prmState[j];
                    }
                    else
                        sqlStr = sqlStr + " or status=" + (int)prmState[j];
                }
            }
            sqlStr = sqlStr + ") order by filename";
            try
            {
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(dsBox);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();

                exMailLog.Log(ex);
            }

            return dsBox.Tables[0].Rows.Count;
        }
        public double GetTotalBatchSize()
        {
            string sqlStr = null;
            DataSet dsBox = new DataSet();
            OdbcDataAdapter sqlAdap = null;
            double size = 0;

            ///changed in version 1.0.0.1
            sqlStr = "select sum(A.qc_size) as size from image_master A,metadata_entry B where A.proj_key=B.proj_code and A.batch_key=B.batch_key and A.policy_number=B.filename and A.proj_key=" + projCode + " and A.batch_key=" + batchCode + " and B.status<>" + (int)eSTATES.POLICY_ON_HOLD + " and A.status<>" + (int)eSTATES.PAGE_DELETED;
            try
            {
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(dsBox);
                size = Convert.ToInt32(dsBox.Tables[0].Rows[0]["size"]) / 1024;
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();

                exMailLog.Log(ex);
            }


            return size;
        }
        public int GetTotalImageCount()
        {
            string sqlStr = null;
            DataSet projDs = new DataSet();
            int count;

            try
            {
                sqlStr = @"select count(*) from image_master where proj_key=" + projCode + " and batch_key=" + batchCode;
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(projDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();


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
        public int GetTotalImageCount(eSTATES[] state, bool prmIsSignaturePage, eSTATES[] prmPolicyState)
        {
            string sqlStr = null;
            DataSet dsBox = new DataSet();
            OdbcDataAdapter sqlAdap = null;

            sqlStr = "select count(page_name) as page_Count,sum(qc_size) as index_size from image_master A,metadata_entry B" +
                    " where A.proj_key = B.proj_code and A.batch_key = B.batch_key and A.policy_number = B.filename and B.proj_code=" + projCode +
                " and B.batch_key=" + batchCode + " and A.status<>29";
            /*
            for (int j = 0; j < state.Length; j++)
            {
                if ((int)state[j] != 0)
                {
                    if (j == 0)
                    {
                        sqlStr = sqlStr + " and (A.status=" + (int)state[j];
                    }
                    else
                        sqlStr = sqlStr + " or A.status=" + (int)state[j];
                }
            }
             
            sqlStr = sqlStr + " and A.status<>" + (int)eSTATES.PAGE_DELETED + " )";
             */
            for (int j = 0; j < prmPolicyState.Length; j++)
            {
                if ((int)prmPolicyState[j] != 0)
                {
                    if (j == 0)
                    {
                        sqlStr = sqlStr + " and (b.status = 4 or b.status = 5 or B.status=" + (int)prmPolicyState[j];
                    }
                    else
                        sqlStr = sqlStr + " or B.status = " + (int)prmPolicyState[j];
                }
            }
            if (prmIsSignaturePage == false)
            {
                sqlStr = sqlStr + " )";
            }
            else
            {
                sqlStr = sqlStr + " ) and A.doc_type<>''";
            }
            try
            {
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(dsBox);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();

                exMailLog.Log(ex);
            }

            return Convert.ToInt32(dsBox.Tables[0].Rows[0]["page_Count"].ToString());
        }


        public string GetBatchPhase1(int prmBatchKey)
        {
            string sqlStr = null;
            string status = string.Empty;
            DataSet batchDs = new DataSet();

            try
            {
                sqlStr = "select phase1 from batch_master where batch_key=" + prmBatchKey;
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(batchDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();

                exMailLog.Log(ex);
            }
            if (batchDs.Tables[0].Rows.Count > 0)
            {
                status = batchDs.Tables[0].Rows[0]["phase1"].ToString();
            }

            return status;
        }
        public string GetBatchPhase2(int prmBatchKey)
        {
            string sqlStr = null;
            string status = string.Empty;
            DataSet batchDs = new DataSet();

            try
            {
                sqlStr = "select phase2 from batch_master where batch_key=" + prmBatchKey;
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(batchDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();

                exMailLog.Log(ex);
            }
            if (batchDs.Tables[0].Rows.Count > 0)
            {
                status = batchDs.Tables[0].Rows[0]["phase2"].ToString();
            }

            return status;
        }
        public int GetBatchStatus(int prmBatchKey)
        {
            string sqlStr = null;
            int status = 0;
            DataSet batchDs = new DataSet();

            try
            {
                sqlStr = "select status from batch_master where batch_key=" + prmBatchKey;
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(batchDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();

                exMailLog.Log(ex);
            }
            if (batchDs.Tables[0].Rows.Count > 0)
            {
                status = Convert.ToInt32(batchDs.Tables[0].Rows[0]["status"].ToString());
            }

            return status;
        }
        public DataTable _GetBundleStatus(string proj, string bundle)
        {
            DataTable dt = new DataTable();
            string sql = "select distinct status,dept_name,category from batch_master where proj_code = '" + proj + "' and batch_key = '" + bundle + "' ";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }
        private void cmbBatch_Leave(object sender, EventArgs e)
        {
            try
            {
                if ((cmbProject.SelectedValue != null) && (cmbBatch.SelectedValue != null))
                {
                    wfeBox wBox;
                    category = _GetBundleStatus(cmbProject.SelectedValue.ToString(), cmbBatch.SelectedValue.ToString()).Rows[0][2].ToString();
                    PopulateBoxDetails();
                    eSTATES state = new eSTATES();

                    eSTATES[] tempState = new eSTATES[6];
                    eSTATES[] policyState = new eSTATES[6];
                    pBox = new CtrlBox(Convert.ToInt32(cmbProject.SelectedValue.ToString()), Convert.ToInt32(cmbBatch.SelectedValue.ToString()), "1");
                    wBox = new wfeBox(sqlCon, pBox);
                    lblTotPolicies.Text = GetTotalPolicies(state).ToString();
                    lblPolRcvd.Text = Convert.ToString((Convert.ToInt32(lblTotPolicies.Text) - Convert.ToInt32(GetTotalPolicies(eSTATES.POLICY_MISSING))));
                    lblPolHold.Text = GetTotalPolicies(eSTATES.POLICY_ON_HOLD).ToString();

                    policyState[0] = NovaNet.wfe.eSTATES.POLICY_INDEXED;
                    policyState[1] = NovaNet.wfe.eSTATES.POLICY_FQC;
                    policyState[2] = NovaNet.wfe.eSTATES.POLICY_CHECKED;
                    policyState[3] = NovaNet.wfe.eSTATES.POLICY_EXCEPTION;
                    policyState[4] = NovaNet.wfe.eSTATES.POLICY_EXPORTED;
                    policyState[5] = NovaNet.wfe.eSTATES.POLICY_NOT_INDEXED;
                    lblScannedPol.Text = GetTotalPolicies(policyState).ToString();
                    lblBatchSz.Text = GetTotalBatchSize().ToString();
                    tempState[0] = eSTATES.PAGE_INDEXED;
                    tempState[1] = eSTATES.PAGE_FQC;
                    tempState[2] = eSTATES.PAGE_CHECKED;
                    tempState[3] = eSTATES.PAGE_EXCEPTION;
                    tempState[4] = eSTATES.PAGE_EXPORTED;
                    tempState[5] = eSTATES.PAGE_NOT_INDEXED;
                    int scannedPol = Convert.ToInt32(lblScannedPol.Text);
                    lblAvgDocketSz.Text = Convert.ToString(Math.Round(Convert.ToDouble(Convert.ToDouble(lblBatchSz.Text) / scannedPol), 2));
                    lblTotImages.Text = GetTotalImageCount(tempState, false, policyState).ToString();
                    //lblSigCount.Text = GetTotalImageCount(tempState, true, policyState).ToString();
                    //lblNetImageCount.Text = Convert.ToString(GetTotalImageCount(tempState, false, policyState) - GetTotalImageCount(tempState, true, policyState));
                    double bSize = Convert.ToInt32(lblBatchSz.Text) * 1024;
                    double tImage = Convert.ToInt32(lblTotImages.Text);
                    double aImageSize = bSize / tImage;
                    lblAvgImageSize.Text = Math.Round(aImageSize, 1).ToString() + " KB";
                    wfeBatch wBatch = new wfeBatch(sqlCon);
                    if (category.ToString() == "Pension Case File")
                    {
                        groupBox4.Visible = true;
                        groupBox3.Visible = false;
                        groupBox6.Visible = false;
                        groupBox10.Visible = false;
                        groupBox11.Visible = false;
                        groupBox12.Visible = false;
                    }
                    else if (category.ToString() == "Pension Case Registers")
                    {
                        groupBox3.Visible = true;
                        groupBox4.Visible = false;
                        groupBox6.Visible = false;
                        groupBox10.Visible = false;
                        groupBox11.Visible = false;
                        groupBox12.Visible = false;
                    }
                    else if (category.ToString() == "Pension Rule Files")
                    {
                        groupBox6.Visible = true;
                        groupBox4.Visible = false;
                        groupBox3.Visible = false;
                        groupBox10.Visible = false;
                        groupBox11.Visible = false;
                        groupBox12.Visible = false;
                    }
                    else if (category.ToString() == "Ledger Cards")
                    {
                        groupBox10.Visible = true;
                        groupBox4.Visible = false;
                        groupBox3.Visible = false;
                        groupBox6.Visible = false;
                        groupBox11.Visible = false;
                        groupBox12.Visible = false;
                    }
                    else if (category.ToString() == "Nomination")
                    {
                        groupBox11.Visible = true;
                        groupBox4.Visible = false;
                        groupBox3.Visible = false;
                        groupBox6.Visible = false;
                        groupBox10.Visible = false;
                        groupBox12.Visible = false;
                    }
                    else if (category.ToString() == "Final Payment Case File")
                    {
                        groupBox12.Visible = true;
                        groupBox4.Visible = false;
                        groupBox3.Visible = false;
                        groupBox6.Visible = false;
                        groupBox10.Visible = false;
                        groupBox11.Visible = false;
                    }
                    if (currStage == "1")
                    {
                        //if (GetBatchStatus(Convert.ToInt32(cmbBatch.SelectedValue.ToString())) == (int)eSTATES.BATCH_READY_FOR_UAT || GetBatchStatus(Convert.ToInt32(cmbBatch.SelectedValue.ToString())) == (int)8)
                        if (GetBatchPhase1(Convert.ToInt32(cmbBatch.SelectedValue.ToString())) == "T")
                        {

                            chkReadyUat.Enabled = false;
                            chkReadyUat.Checked = true;
                            cmdAccepted.Enabled = false;
                            cmdRejected.Enabled = false;
                        }
                        else
                        {
                            chkReadyUat.Enabled = true;
                            chkReadyUat.Checked = false;
                            cmdAccepted.Enabled = true;
                            cmdRejected.Enabled = true;
                        }
                    }
                    else
                    {
                        //if (GetBatchStatus(Convert.ToInt32(cmbBatch.SelectedValue.ToString())) == (int)8)
                        if (GetBatchPhase2(Convert.ToInt32(cmbBatch.SelectedValue.ToString())) == "T")
                        {
                            chkReadyUat.Enabled = false;
                            chkReadyUat.Checked = true;
                            cmdAccepted.Enabled = false;
                            cmdRejected.Enabled = false;
                        }
                        else
                        {
                            chkReadyUat.Enabled = true;
                            chkReadyUat.Checked = false;
                            cmdAccepted.Enabled = true;
                            cmdRejected.Enabled = true;
                        }
                    }

                    CheckBatchRejection(cmbBatch.SelectedValue.ToString());
                    lblTotPol.Text = wBox.GetLICCheckedCount().ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while generating batch information........" + "  " + ex.Message);
            }
        }

        private void grdBox_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            selBoxNo = Convert.ToInt32("1");
            if (Convert.ToInt32(textBox1.Text.Trim()) <= 100)
            {
                PolicyDetails("1");
            }
            else
            {
                MessageBox.Show("Cannot search files for this batch over 100 percent");
            }
            grdPolicy.ForeColor = Color.Black;
        }
        public DataSet GetPolicyList(eSTATES[] prmState, int limit)
        {
            string sqlStr = null;

            DataSet policyDs = new DataSet();

            try
            {
                if (Convert.ToInt32(textBox1.Text) < 100)
                {
                    if (prmState.Length == 0)
                    {
                        sqlStr = "select a.filename,a.department,a.subcat,a.state_name,a.emp_name,a.desg,a.fileid,date_format(a.birth_date,'%Y-%m-%d'),date_format(a.joining_date,'%Y-%m-%d'),date_format(a.death_date,'%Y-%m-%d')," +
                            "date_format(a.retirement_date,'%Y-%m-%d'),a.psa_name,a.section,a.pension_file_no,a.ppo_fppo,a.gpo_dgpo,a.ppo_gpo_cpo,a.mobile,a.hrms_id,a.spouce,a.place_payment,a.rule_file,a.vol,a.subject,a.series,a.acc,a.subscriber_name," +
                            "a.ledger_year,date_format(a.accept_date,'%Y-%m-%d'),a.fp_auth_no,date_format(a.fp_date,'%Y-%m-%d'),a.status,a.family_pensioner,a.ge_no,a.pen_no,a.promoted_dep,a.sub_doc_type," +
                            "a.index_no,a.promotion_date,a.id_no,a.branch_name from metadata_entry A where a.proj_code=" + projCode + " and a.batch_key=" + batchCode + " order by rand() limit " + limit;
                    }
                    else
                    {

                        sqlStr = "select a.filename,a.department,a.subcat,a.state_name,a.emp_name,a.desg,a.fileid,date_format(a.birth_date,'%Y-%m-%d'),date_format(a.joining_date,'%Y-%m-%d'),date_format(a.death_date,'%Y-%m-%d')," +
                            "date_format(a.retirement_date,'%Y-%m-%d'),a.psa_name,a.section,a.pension_file_no,a.ppo_fppo,a.gpo_dgpo,a.ppo_gpo_cpo,a.mobile,a.hrms_id,a.spouce,a.place_payment,a.rule_file,a.vol,a.subject,a.series,a.acc,a.subscriber_name," +
                            "a.ledger_year,date_format(a.accept_date,'%Y-%m-%d'),a.fp_auth_no,date_format(a.fp_date,'%Y-%m-%d'),a.status,a.family_pensioner,a.ge_no,a.pen_no,a.promoted_dep,a.sub_doc_type," +
                            "a.index_no,a.promotion_date,a.id_no,a.branch_name from metadata_entry A where a.proj_code=" + projCode + " and a.batch_key=" + batchCode;


                        for (int j = 0; j < prmState.Length; j++)
                        {
                            if ((int)prmState[j] != 0)
                            {
                                if (j == 0)
                                {
                                    sqlStr = sqlStr + " and (a.status=" + (int)prmState[j];
                                }
                                else
                                    sqlStr = sqlStr + " or a.status=" + (int)prmState[j];
                            }
                        }
                        sqlStr = sqlStr + ") order by rand() limit " + limit + " ";
                    }
                }
                else
                {
                    if (prmState.Length == 0)
                    {
                        sqlStr = "select a.filename,a.department,a.subcat,a.state_name,a.emp_name,a.desg,a.fileid,date_format(a.birth_date,'%Y-%m-%d'),date_format(a.joining_date,'%Y-%m-%d'),date_format(a.death_date,'%Y-%m-%d')," +
                            "date_format(a.retirement_date,'%Y-%m-%d'),a.psa_name,a.section,a.pension_file_no,a.ppo_fppo,a.gpo_dgpo,a.ppo_gpo_cpo,a.mobile,a.hrms_id,a.spouce,a.place_payment,a.rule_file,a.vol,a.subject,a.series,a.acc,a.subscriber_name," +
                            "a.ledger_year,date_format(a.accept_date,'%Y-%m-%d'),a.fp_auth_no,date_format(a.fp_date,'%Y-%m-%d'),a.status,a.family_pensioner,a.ge_no,a.pen_no,a.promoted_dep,a.sub_doc_type," +
                            "a.index_no,a.promotion_date,a.id_no,a.branch_name from metadata_entry A where a.proj_code=" + projCode + " and a.batch_key=" + batchCode + " order by a.item_no asc limit " + limit;
                    }
                    else
                    {

                        sqlStr = "select a.filename,a.department,a.subcat,a.state_name,a.emp_name,a.desg,a.fileid,date_format(a.birth_date,'%Y-%m-%d'),date_format(a.joining_date,'%Y-%m-%d'),date_format(a.death_date,'%Y-%m-%d')," +
                            "date_format(a.retirement_date,'%Y-%m-%d'),a.psa_name,a.section,a.pension_file_no,a.ppo_fppo,a.gpo_dgpo,a.ppo_gpo_cpo,a.mobile,a.hrms_id,a.spouce,a.place_payment,a.rule_file,a.vol,a.subject,a.series,a.acc,a.subscriber_name," +
                            "a.ledger_year,date_format(a.accept_date,'%Y-%m-%d'),a.fp_auth_no,date_format(a.fp_date,'%Y-%m-%d'),a.status,a.family_pensioner,a.ge_no,a.pen_no,a.promoted_dep,a.sub_doc_type," +
                            "a.index_no,a.promotion_date,a.id_no,a.branch_name from metadata_entry A where a.proj_code=" + projCode + " and a.batch_key=" + batchCode;


                        for (int j = 0; j < prmState.Length; j++)
                        {
                            if ((int)prmState[j] != 0)
                            {
                                if (j == 0)
                                {
                                    sqlStr = sqlStr + " and (a.status=" + (int)prmState[j];
                                }
                                else
                                    sqlStr = sqlStr + " or a.status=" + (int)prmState[j];
                            }
                        }
                        sqlStr = sqlStr + ") order by a.item_no asc limit " + limit + " ";
                    }
                }
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(policyDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();

                exMailLog.Log(ex);
            }

            return policyDs;
        }
        public DataSet GetPolicyList(eSTATES[] prmState)
        {
            string sqlStr = null;

            DataSet policyDs = new DataSet();

            try
            {
                if (prmState.Length == 0)
                {
                    sqlStr = "select a.filename,a.department,a.subcat,a.state_name,a.emp_name,a.desg,a.fileid,date_format(a.birth_date,'%Y-%m-%d'),date_format(a.joining_date,'%Y-%m-%d'),date_format(a.death_date,'%Y-%m-%d')," +
                        "date_format(a.retirement_date,'%Y-%m-%d'),a.psa_name,a.section,a.pension_file_no,a.ppo_fppo,a.gpo_dgpo,a.ppo_gpo_cpo,a.mobile,a.hrms_id,a.spouce,a.place_payment,a.rule_file,a.vol,a.subject,a.series,a.acc,a.subscriber_name," +
                        "a.ledger_year,date_format(a.accept_date,'%Y-%m-%d'),a.fp_auth_no,date_format(a.fp_date,'%Y-%m-%d'),a.status,a.family_pensioner,a.ge_no,a.pen_no,a.promoted_dep,a.sub_doc_type," +
                        "a.index_no,a.promotion_date,a.id_no,a.branch_name from metadata_entry A where a.proj_code=" + projCode + " and a.batch_key=" + batchCode;
                }
                else
                {

                    sqlStr = "select a.filename,a.department,a.subcat,a.state_name,a.emp_name,a.desg,a.fileid,date_format(a.birth_date,'%Y-%m-%d'),date_format(a.joining_date,'%Y-%m-%d'),date_format(a.death_date,'%Y-%m-%d')," +
                        "date_format(a.retirement_date,'%Y-%m-%d'),a.psa_name,a.section,a.pension_file_no,a.ppo_fppo,a.gpo_dgpo,a.ppo_gpo_cpo,a.mobile,a.hrms_id,a.spouce,a.place_payment,a.rule_file,a.vol,a.subject,a.series,a.acc,a.subscriber_name," +
                        "a.ledger_year,date_format(a.accept_date,'%Y-%m-%d'),a.fp_auth_no,date_format(a.fp_date,'%Y-%m-%d'),a.status,a.family_pensioner,a.ge_no,a.pen_no,a.promoted_dep,a.sub_doc_type," +
                        "a.index_no,a.promotion_date,a.id_no,a.branch_name from metadata_entry A where a.proj_code=" + projCode + " and a.batch_key=" + batchCode;


                    for (int j = 0; j < prmState.Length; j++)
                    {
                        if ((int)prmState[j] != 0)
                        {
                            if (j == 0)
                            {
                                sqlStr = sqlStr + " and (a.status=" + (int)prmState[j];
                            }
                            else
                                sqlStr = sqlStr + " or a.status=" + (int)prmState[j];
                        }
                    }
                    sqlStr = sqlStr + ") order by a.item_no asc";
                }

                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(policyDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();

                exMailLog.Log(ex);
            }

            return policyDs;
        }
        void PolicyDetails(string prmBoxNo)
        {
            DataTable dt = new DataTable();
            DataRow dr;
            DataSet ds = new DataSet();
            DataSet dsPolicy = new DataSet();
            DataSet dsImage = new DataSet();
            eSTATES[] filterState = new eSTATES[1];
            double avgSize;
            string totSize = string.Empty;
            string totPage;
            string yr;
            string mm;
            string dd;
            NovaNet.wfe.eSTATES[] state = new NovaNet.wfe.eSTATES[6];

            dt.Columns.Add("SrlNo");

            dt.Columns.Add("FileName");
            dt.Columns.Add("Department");
            dt.Columns.Add("Category");
            dt.Columns.Add("State_Name");
            dt.Columns.Add("Emp_Name");
            dt.Columns.Add("Designation");
            dt.Columns.Add("FileID");
            dt.Columns.Add("BirthDate");
            dt.Columns.Add("JoiningDate");
            dt.Columns.Add("DeathDate");
            dt.Columns.Add("RetirementDate");
            dt.Columns.Add("PSAName");
            dt.Columns.Add("Section");
            dt.Columns.Add("PensionFileNo");
            dt.Columns.Add("PPO/FPPO No");
            dt.Columns.Add("GPO/DGPO No");
            dt.Columns.Add("PPO/GPO/CPO/Item No");
            dt.Columns.Add("Mobile");
            dt.Columns.Add("HRMS");
            dt.Columns.Add("Spouse");
            dt.Columns.Add("PaymentPlace");
            dt.Columns.Add("RuleFileNo");
            dt.Columns.Add("Volume");
            dt.Columns.Add("Subject");
            dt.Columns.Add("Series");
            dt.Columns.Add("Account");
            dt.Columns.Add("Subscriber");
            dt.Columns.Add("LedgerYear");
            dt.Columns.Add("AcceptanceDate");
            dt.Columns.Add("FPAuthNo");
            dt.Columns.Add("FPDate");
            dt.Columns.Add("FamilyPensioner");
            dt.Columns.Add("GENo");
            dt.Columns.Add("PenNo");
            dt.Columns.Add("PromotedDepartment");
            dt.Columns.Add("SubDocType");
            dt.Columns.Add("IndexNo");
            dt.Columns.Add("PromotionDate");
            dt.Columns.Add("IDNo");
            dt.Columns.Add("BranchName");

            dt.Columns.Add("ScannedPages");
            dt.Columns.Add("TotalSize");
            dt.Columns.Add("Avg_Size");

            dt.Columns.Add("STATUS");
            dt.Columns.Add("FILESTATUS");





            if ((prmBoxNo != string.Empty) && (prmBoxNo != null) && (cmbProject.SelectedValue.ToString() != string.Empty) && (cmbProject.SelectedValue.ToString() != null) && (cmbBatch.SelectedValue.ToString() != string.Empty) && ((cmbBatch.SelectedValue.ToString() != null)))
            {
                dept = _GetBundleStatus(cmbProject.SelectedValue.ToString(), cmbBatch.SelectedValue.ToString()).Rows[0][1].ToString();
                category = _GetBundleStatus(cmbProject.SelectedValue.ToString(), cmbBatch.SelectedValue.ToString()).Rows[0][2].ToString();
                if (category.ToString() == "Pension Case File")
                {
                    groupBox4.Visible = true;
                    groupBox3.Visible = false;
                    groupBox6.Visible = false;
                    groupBox10.Visible = false;
                    groupBox11.Visible = false;
                    groupBox12.Visible = false;
                    groupBox14.Visible = false;
                    groupBox15.Visible = false;
                }
                else if (category.ToString() == "Pension Case Registers")
                {
                    groupBox3.Visible = true;
                    groupBox4.Visible = false;
                    groupBox6.Visible = false;
                    groupBox10.Visible = false;
                    groupBox11.Visible = false;
                    groupBox12.Visible = false;
                    groupBox14.Visible = false;
                    groupBox15.Visible = false;

                }
                else if (category.ToString() == "Pension Rule Files")
                {
                    groupBox6.Visible = true;
                    groupBox4.Visible = false;
                    groupBox3.Visible = false;
                    groupBox10.Visible = false;
                    groupBox11.Visible = false;
                    groupBox12.Visible = false;
                    groupBox14.Visible = false;
                    groupBox15.Visible = false;

                }
                else if (category.ToString() == "Ledger Cards")
                {
                    groupBox10.Visible = true;
                    groupBox4.Visible = false;
                    groupBox3.Visible = false;
                    groupBox6.Visible = false;
                    groupBox11.Visible = false;
                    groupBox12.Visible = false;
                    groupBox14.Visible = false;
                    groupBox15.Visible = false;

                }
                else if (category.ToString() == "Nomination")
                {
                    groupBox11.Visible = true;
                    groupBox4.Visible = false;
                    groupBox3.Visible = false;
                    groupBox6.Visible = false;
                    groupBox10.Visible = false;
                    groupBox12.Visible = false;
                    groupBox14.Visible = false;
                    groupBox15.Visible = false;

                }
                else if (category.ToString() == "Final Payment Case File")
                {
                    groupBox12.Visible = true;
                    groupBox4.Visible = false;
                    groupBox3.Visible = false;
                    groupBox6.Visible = false;
                    groupBox10.Visible = false;
                    groupBox11.Visible = false;
                    groupBox14.Visible = false;
                    groupBox15.Visible = false;

                }
                else if (dept.ToLower() == "ge")
                {
                    groupBox12.Visible = false;
                    groupBox4.Visible = false;
                    groupBox3.Visible = false;
                    groupBox6.Visible = false;
                    groupBox10.Visible = false;
                    groupBox11.Visible = false;
                    groupBox14.Visible = true;
                    groupBox15.Visible = false;

                }
                else if (dept.ToLower() == "admin")
                {
                    groupBox12.Visible = false;
                    groupBox4.Visible = false;
                    groupBox3.Visible = false;
                    groupBox6.Visible = false;
                    groupBox10.Visible = false;
                    groupBox11.Visible = false;
                    groupBox14.Visible = false;
                    groupBox15.Visible = true;
                }

                boxNo = prmBoxNo;
                pPolicy = new CtrlPolicy(Convert.ToInt32(cmbProject.SelectedValue.ToString()), Convert.ToInt32(cmbBatch.SelectedValue.ToString()), prmBoxNo, "0");
                wPolicy = new wfePolicy(sqlCon, pPolicy);

                eSTATES[] tot = new eSTATES[0];
                dsPolicy = GetPolicyList(tot);

                int tot_count = dsPolicy.Tables[0].Rows.Count;
                int percent = Convert.ToInt32(tot_count * Convert.ToInt32(textBox1.Text.Trim()) / 100);


                if (rdoShowAll.Checked == true)
                {
                    eSTATES[] allState = new eSTATES[0];
                    dsPolicy = GetPolicyList(allState, percent);
                    deLabel2.Text = "Showing " + dsPolicy.Tables[0].Rows.Count + " out of " + tot_count + " files";
                }
                if (rdoChecked.Checked == true)
                {
                    filterState[0] = eSTATES.POLICY_CHECKED;
                    dsPolicy = GetPolicyList(filterState, percent);
                    deLabel2.Text = "Showing " + dsPolicy.Tables[0].Rows.Count + " out of " + tot_count + " files";
                }
                if (rdoExceptions.Checked == true)
                {
                    filterState[0] = eSTATES.POLICY_EXCEPTION;
                    dsPolicy = GetPolicyList(filterState, percent);
                    deLabel2.Text = "Showing " + dsPolicy.Tables[0].Rows.Count + " out of " + tot_count + " files";
                }

                if (rdoOnHold.Checked == true)
                {
                    filterState[0] = eSTATES.POLICY_ON_HOLD;
                    dsPolicy = GetPolicyList(filterState, percent);
                    deLabel2.Text = "Showing " + dsPolicy.Tables[0].Rows.Count + " out of " + tot_count + " files";
                }
                if (rdoMissing.Checked == true)
                {
                    filterState[0] = eSTATES.POLICY_MISSING;
                    dsPolicy = GetPolicyList(filterState, percent);
                    deLabel2.Text = "Showing " + dsPolicy.Tables[0].Rows.Count + " out of " + tot_count + " files";
                }

                if (rdo150.Checked == true)
                {
                    eSTATES[] allState = new eSTATES[0];
                    dsPolicy = GetPolicyList(allState, percent);
                    deLabel2.Text = "Showing " + dsPolicy.Tables[0].Rows.Count + " out of " + tot_count + " files";
                }

                for (int i = 0; i < dsPolicy.Tables[0].Rows.Count; i++)
                {
                    pImage = new CtrlImage(Convert.ToInt32(cmbProject.SelectedValue.ToString()), Convert.ToInt32(cmbBatch.SelectedValue.ToString()), prmBoxNo, dsPolicy.Tables[0].Rows[i]["filename"].ToString(), string.Empty, string.Empty);
                    wImage = new wfeImage(sqlCon, pImage);

                    //NovaNet.wfe.eSTATES[] state = new NovaNet.wfe.eSTATES[4];
                    state[0] = NovaNet.wfe.eSTATES.PAGE_EXCEPTION;
                    state[1] = NovaNet.wfe.eSTATES.PAGE_INDEXED;
                    state[2] = NovaNet.wfe.eSTATES.PAGE_CHECKED;
                    state[3] = NovaNet.wfe.eSTATES.PAGE_FQC;
                    state[4] = NovaNet.wfe.eSTATES.PAGE_EXPORTED;
                    state[5] = NovaNet.wfe.eSTATES.PAGE_ON_HOLD;
                    dsImage = wImage.GetPolicyWiseImageInfo(state);
                    if (rdo150.Checked == true)
                    {

                        totSize = dsImage.Tables[0].Rows[0]["qc_size"].ToString();
                        if (totSize != String.Empty)
                        {
                            double totFileSize = Convert.ToDouble(totSize) / 1024;
                            if (Convert.ToDouble(totFileSize) > ihConstants._DOCKET_MAX_SIZE)
                            {
                                if ((Convert.ToInt32(dsPolicy.Tables[0].Rows[i]["status"].ToString()) != (int)eSTATES.POLICY_SCANNED) && (Convert.ToInt32(dsPolicy.Tables[0].Rows[i]["status"].ToString()) != (int)eSTATES.POLICY_QC) && (Convert.ToInt32(dsPolicy.Tables[0].Rows[i]["status"].ToString()) != (int)eSTATES.POLICY_ON_HOLD))
                                {
                                    dr = dt.NewRow();
                                    dr["SrlNo"] = i + 1;


                                    dr["FileName"] = dsPolicy.Tables[0].Rows[i]["filename"].ToString();
                                    dr["Department"] = dsPolicy.Tables[0].Rows[i]["department"].ToString();
                                    dr["Category"] = dsPolicy.Tables[0].Rows[i]["subcat"].ToString();
                                    dr["State_Name"] = dsPolicy.Tables[0].Rows[i]["state_name"].ToString();
                                    dr["Emp_Name"] = dsPolicy.Tables[0].Rows[i]["emp_name"].ToString();
                                    dr["Designation"] = dsPolicy.Tables[0].Rows[i][5].ToString();
                                    dr["FileID"] = dsPolicy.Tables[0].Rows[i][6].ToString();
                                    dr["BirthDate"] = dsPolicy.Tables[0].Rows[i][7].ToString();
                                    dr["JoiningDate"] = dsPolicy.Tables[0].Rows[i][8].ToString();
                                    dr["DeathDate"] = dsPolicy.Tables[0].Rows[i][9].ToString();
                                    dr["RetirementDate"] = dsPolicy.Tables[0].Rows[i][10].ToString();
                                    dr["PSAName"] = dsPolicy.Tables[0].Rows[i][11].ToString();
                                    dr["Section"] = dsPolicy.Tables[0].Rows[i][12].ToString();
                                    dr["PensionFileNo"] = dsPolicy.Tables[0].Rows[i][13].ToString();
                                    dr["PPO/FPPO No"] = dsPolicy.Tables[0].Rows[i][14].ToString();
                                    dr["GPO/DGPO No"] = dsPolicy.Tables[0].Rows[i][15].ToString();
                                    dr["PPO/GPO/CPO/Item No"] = dsPolicy.Tables[0].Rows[i][16].ToString();
                                    dr["Mobile"] = dsPolicy.Tables[0].Rows[i][17].ToString();
                                    dr["HRMS"] = dsPolicy.Tables[0].Rows[i][18].ToString();
                                    dr["Spouse"] = dsPolicy.Tables[0].Rows[i][19].ToString();
                                    dr["PaymentPlace"] = dsPolicy.Tables[0].Rows[i][20].ToString();
                                    dr["RuleFileNo"] = dsPolicy.Tables[0].Rows[i][21].ToString();
                                    dr["Volume"] = dsPolicy.Tables[0].Rows[i][22].ToString();
                                    dr["Subject"] = dsPolicy.Tables[0].Rows[i][23].ToString();
                                    dr["Series"] = dsPolicy.Tables[0].Rows[i][24].ToString();
                                    dr["Account"] = dsPolicy.Tables[0].Rows[i][25].ToString();
                                    dr["Subscriber"] = dsPolicy.Tables[0].Rows[i][26].ToString();
                                    dr["LedgerYear"] = dsPolicy.Tables[0].Rows[i][27].ToString();
                                    dr["AcceptanceDate"] = dsPolicy.Tables[0].Rows[i][28].ToString();
                                    dr["FPAuthNo"] = dsPolicy.Tables[0].Rows[i][29].ToString();
                                    dr["FPDate"] = dsPolicy.Tables[0].Rows[i][30].ToString();

                                    dr["FamilyPensioner"] = dsPolicy.Tables[0].Rows[i]["family_pensioner"].ToString();
                                    dr["GENo"] = dsPolicy.Tables[0].Rows[i]["ge_no"].ToString();
                                    dr["PenNo"] = dsPolicy.Tables[0].Rows[i]["pen_no"].ToString();
                                    dr["PromotedDepartment"] = dsPolicy.Tables[0].Rows[i]["promoted_dep"].ToString();
                                    dr["SubDocType"] = dsPolicy.Tables[0].Rows[i]["sub_doc_type"].ToString();
                                    dr["IndexNo"] = dsPolicy.Tables[0].Rows[i]["index_no"].ToString();
                                    dr["PromotionDate"] = dsPolicy.Tables[0].Rows[i]["promotion_date"].ToString();
                                    dr["IDNo"] = dsPolicy.Tables[0].Rows[i]["id_no"].ToString();
                                    dr["BranchName"] = dsPolicy.Tables[0].Rows[i]["branch_name"].ToString();


                                    pImage = new CtrlImage(Convert.ToInt32(cmbProject.SelectedValue.ToString()), Convert.ToInt32(cmbBatch.SelectedValue.ToString()), prmBoxNo, dsPolicy.Tables[0].Rows[i]["filename"].ToString(), string.Empty, string.Empty);
                                    wImage = new wfeImage(sqlCon, pImage);

                                    if ((Convert.ToInt32(dsPolicy.Tables[0].Rows[i]["status"].ToString()) != (int)eSTATES.POLICY_MISSING))
                                    {
                                        totPage = dsImage.Tables[0].Rows[0]["page_count"].ToString();
                                    }
                                    else
                                    {
                                        totPage = "0";
                                    }
                                    dr["ScannedPages"] = totPage;
                                    if ((Convert.ToInt32(dsPolicy.Tables[0].Rows[i]["status"].ToString()) != (int)eSTATES.POLICY_MISSING))
                                    {
                                        totSize = dsImage.Tables[0].Rows[0]["qc_size"].ToString();
                                    }
                                    else
                                    {
                                        totSize = string.Empty;
                                    }
                                    if (totSize != string.Empty)
                                    {
                                        totSize = Convert.ToString(Math.Round(Convert.ToDouble(totSize), 2));
                                    }
                                    dr["TotalSize"] = totSize;

                                    dr["STATUS"] = dsPolicy.Tables[0].Rows[i]["status"];
                                    if ((Convert.ToInt32(dsPolicy.Tables[0].Rows[i]["status"].ToString()) == (int)eSTATES.POLICY_NOT_INDEXED) || (Convert.ToInt32(dsPolicy.Tables[0].Rows[i]["status"].ToString()) == (int)4))
                                    {
                                        dr["FILESTATUS"] = "Final QC";
                                    }
                                    if ((Convert.ToInt32(dsPolicy.Tables[0].Rows[i]["status"].ToString()) == (int)eSTATES.POLICY_INDEXED) || (Convert.ToInt32(dsPolicy.Tables[0].Rows[i]["status"].ToString()) == (int)eSTATES.POLICY_FQC))
                                    {
                                        dr["FILESTATUS"] = "Final QC";
                                    }
                                    if ((Convert.ToInt32(dsPolicy.Tables[0].Rows[i]["status"].ToString()) == (int)eSTATES.POLICY_ON_HOLD))
                                    {
                                        dr["FILESTATUS"] = "On hold";
                                    }
                                    if ((Convert.ToInt32(dsPolicy.Tables[0].Rows[i]["status"].ToString()) == (int)eSTATES.POLICY_MISSING))
                                    {
                                        dr["FILESTATUS"] = "Missing";
                                    }
                                    if ((Convert.ToInt32(dsPolicy.Tables[0].Rows[i]["status"].ToString()) == (int)eSTATES.POLICY_EXCEPTION))
                                    {
                                        dr["FILESTATUS"] = "In exception";
                                    }
                                    if ((Convert.ToInt32(dsPolicy.Tables[0].Rows[i]["status"].ToString()) == (int)eSTATES.POLICY_CHECKED))
                                    {
                                        dr["FILESTATUS"] = "Checked";
                                    }
                                    if ((Convert.ToInt32(dsPolicy.Tables[0].Rows[i]["status"].ToString()) == (int)eSTATES.POLICY_EXPORTED) || (Convert.ToInt32(dsPolicy.Tables[0].Rows[i]["status"].ToString()) == (int)9))
                                    {
                                        dr["FILESTATUS"] = "Exported";
                                    }
                                    if ((Convert.ToInt32(dsPolicy.Tables[0].Rows[i]["status"].ToString()) == (int)eSTATES.POLICY_SCANNED) || (Convert.ToInt32(dsPolicy.Tables[0].Rows[i]["status"].ToString()) == (int)eSTATES.POLICY_QC) || (Convert.ToInt32(dsPolicy.Tables[0].Rows[i]["status"].ToString()) == (int)eSTATES.POLICY_ON_HOLD))
                                    {
                                        dr["ScannedPages"] = "0";
                                        dr["TotalSize"] = string.Empty;
                                        totPage = "0";
                                        totSize = string.Empty;
                                    }
                                    if ((totSize != string.Empty) && (totPage != "0"))
                                    {
                                        avgSize = Convert.ToDouble(totSize) / Convert.ToDouble(totPage);
                                        dr["Avg_Size"] = Convert.ToString(Math.Round(avgSize, 2));
                                    }

                                    dt.Rows.Add(dr);
                                }
                            }
                        }
                    }
                    else
                    {
                        dr = dt.NewRow();
                        dr["SrlNo"] = i + 1;

                        dr["FileName"] = dsPolicy.Tables[0].Rows[i]["filename"].ToString();
                        dr["Department"] = dsPolicy.Tables[0].Rows[i]["department"].ToString();
                        dr["Category"] = dsPolicy.Tables[0].Rows[i]["subcat"].ToString();
                        dr["State_Name"] = dsPolicy.Tables[0].Rows[i]["state_name"].ToString();
                        dr["Emp_Name"] = dsPolicy.Tables[0].Rows[i]["emp_name"].ToString();
                        dr["Designation"] = dsPolicy.Tables[0].Rows[i][5].ToString();
                        dr["FileID"] = dsPolicy.Tables[0].Rows[i][6].ToString();
                        dr["BirthDate"] = dsPolicy.Tables[0].Rows[i][7].ToString();
                        dr["JoiningDate"] = dsPolicy.Tables[0].Rows[i][8].ToString();
                        dr["DeathDate"] = dsPolicy.Tables[0].Rows[i][9].ToString();
                        dr["RetirementDate"] = dsPolicy.Tables[0].Rows[i][10].ToString();
                        dr["PSAName"] = dsPolicy.Tables[0].Rows[i][11].ToString();
                        dr["Section"] = dsPolicy.Tables[0].Rows[i][12].ToString();
                        dr["PensionFileNo"] = dsPolicy.Tables[0].Rows[i][13].ToString();
                        dr["PPO/FPPO No"] = dsPolicy.Tables[0].Rows[i][14].ToString();
                        dr["GPO/DGPO No"] = dsPolicy.Tables[0].Rows[i][15].ToString();
                        dr["PPO/GPO/CPO/Item No"] = dsPolicy.Tables[0].Rows[i][16].ToString();
                        dr["Mobile"] = dsPolicy.Tables[0].Rows[i][17].ToString();
                        dr["HRMS"] = dsPolicy.Tables[0].Rows[i][18].ToString();
                        dr["Spouse"] = dsPolicy.Tables[0].Rows[i][19].ToString();
                        dr["PaymentPlace"] = dsPolicy.Tables[0].Rows[i][20].ToString();
                        dr["RuleFileNo"] = dsPolicy.Tables[0].Rows[i][21].ToString();
                        dr["Volume"] = dsPolicy.Tables[0].Rows[i][22].ToString();
                        dr["Subject"] = dsPolicy.Tables[0].Rows[i][23].ToString();
                        dr["Series"] = dsPolicy.Tables[0].Rows[i][24].ToString();
                        dr["Account"] = dsPolicy.Tables[0].Rows[i][25].ToString();
                        dr["Subscriber"] = dsPolicy.Tables[0].Rows[i][26].ToString();
                        dr["LedgerYear"] = dsPolicy.Tables[0].Rows[i][27].ToString();
                        dr["AcceptanceDate"] = dsPolicy.Tables[0].Rows[i][28].ToString();
                        dr["FPAuthNo"] = dsPolicy.Tables[0].Rows[i][29].ToString();
                        dr["FPDate"] = dsPolicy.Tables[0].Rows[i][30].ToString();

                        dr["FamilyPensioner"] = dsPolicy.Tables[0].Rows[i]["family_pensioner"].ToString();
                        dr["GENo"] = dsPolicy.Tables[0].Rows[i]["ge_no"].ToString();
                        dr["PenNo"] = dsPolicy.Tables[0].Rows[i]["pen_no"].ToString();
                        dr["PromotedDepartment"] = dsPolicy.Tables[0].Rows[i]["promoted_dep"].ToString();
                        dr["SubDocType"] = dsPolicy.Tables[0].Rows[i]["sub_doc_type"].ToString();
                        dr["IndexNo"] = dsPolicy.Tables[0].Rows[i]["index_no"].ToString();
                        dr["PromotionDate"] = dsPolicy.Tables[0].Rows[i]["promotion_date"].ToString();
                        dr["IDNo"] = dsPolicy.Tables[0].Rows[i]["id_no"].ToString();
                        dr["BranchName"] = dsPolicy.Tables[0].Rows[i]["branch_name"].ToString();

                        if ((Convert.ToInt32(dsPolicy.Tables[0].Rows[i]["status"].ToString()) != (int)eSTATES.POLICY_MISSING))
                        {
                            totPage = dsImage.Tables[0].Rows[0]["page_count"].ToString();
                        }
                        else
                        {
                            totPage = "0";
                        }
                        dr["ScannedPages"] = totPage;
                        if ((Convert.ToInt32(dsPolicy.Tables[0].Rows[i]["status"].ToString()) != (int)eSTATES.POLICY_MISSING))
                        {
                            totSize = dsImage.Tables[0].Rows[0]["qc_size"].ToString();
                        }
                        else
                        {
                            totSize = string.Empty;
                        }
                        if (totSize != string.Empty)
                        {
                            totSize = Convert.ToString(Math.Round(Convert.ToDouble(totSize), 2));
                        }
                        dr["TotalSize"] = totSize;
                        dr["STATUS"] = dsPolicy.Tables[0].Rows[i]["status"];
                        if ((Convert.ToInt32(dsPolicy.Tables[0].Rows[i]["status"].ToString()) == (int)eSTATES.POLICY_NOT_INDEXED) || (Convert.ToInt32(dsPolicy.Tables[0].Rows[i]["status"].ToString()) == (int)4))
                        {
                            dr["FILESTATUS"] = "Final QC";
                        }
                        if ((Convert.ToInt32(dsPolicy.Tables[0].Rows[i]["status"].ToString()) == (int)eSTATES.POLICY_INDEXED) || (Convert.ToInt32(dsPolicy.Tables[0].Rows[i]["status"].ToString()) == (int)eSTATES.POLICY_FQC))
                        {
                            dr["FILESTATUS"] = "Final QC";
                        }
                        if ((Convert.ToInt32(dsPolicy.Tables[0].Rows[i]["status"].ToString()) == (int)eSTATES.POLICY_ON_HOLD))
                        {
                            dr["FILESTATUS"] = "On hold";
                        }
                        if ((Convert.ToInt32(dsPolicy.Tables[0].Rows[i]["status"].ToString()) == (int)eSTATES.POLICY_MISSING))
                        {
                            dr["FILESTATUS"] = "Missing";
                        }
                        if ((Convert.ToInt32(dsPolicy.Tables[0].Rows[i]["status"].ToString()) == (int)eSTATES.POLICY_EXCEPTION))
                        {
                            dr["FILESTATUS"] = "In exception";
                        }
                        if ((Convert.ToInt32(dsPolicy.Tables[0].Rows[i]["status"].ToString()) == (int)eSTATES.POLICY_CHECKED))
                        {
                            dr["FILESTATUS"] = "Checked";
                        }
                        if ((Convert.ToInt32(dsPolicy.Tables[0].Rows[i]["status"].ToString()) == (int)eSTATES.POLICY_EXPORTED) || (Convert.ToInt32(dsPolicy.Tables[0].Rows[i]["status"].ToString()) == (int)9))
                        {
                            dr["FILESTATUS"] = "Exported";
                        }
                        if ((Convert.ToInt32(dsPolicy.Tables[0].Rows[i]["status"].ToString()) == 2) || (Convert.ToInt32(dsPolicy.Tables[0].Rows[i]["status"].ToString()) == 3) || (Convert.ToInt32(dsPolicy.Tables[0].Rows[i]["status"].ToString()) == (int)eSTATES.POLICY_ON_HOLD))
                        {
                            dr["ScannedPages"] = "0";
                            dr["TotalSize"] = string.Empty;
                            totPage = "0";
                            totSize = string.Empty;
                        }
                        if ((totSize != string.Empty) && (totPage != "0"))
                        {
                            avgSize = Convert.ToDouble(totSize) / Convert.ToDouble(totPage);
                            dr["Avg_Size"] = Convert.ToString(Math.Round(avgSize, 2));
                        }

                        dt.Rows.Add(dr);
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    grdPolicy.DataSource = ds;
                    grdPolicy.DataSource = dt;
                }
                else
                {
                    grdPolicy.DataSource = ds;
                }

                if ((grdPolicy.Rows.Count > 0))
                {
                    for (int l = 0; l < grdPolicy.Rows.Count; l++)
                    {
                        if (Convert.ToInt32(grdPolicy.Rows[l].Cells[44].Value.ToString()) == (int)eSTATES.POLICY_CHECKED)
                        {
                            if (licQAUsers(grdPolicy.Rows[l].Cells[1].Value.ToString()).Rows.Count > 0)
                            {
                                string a1 = licQAUsers(grdPolicy.Rows[l].Cells[1].Value.ToString()).Rows[0][0].ToString();
                                string a2 = licQAUsers(grdPolicy.Rows[l].Cells[1].Value.ToString()).Rows[0][1].ToString();
                                if (a1 != "" && a2 != "")
                                {
                                    grdPolicy.Rows[l].DefaultCellStyle.ForeColor = Color.Black;
                                    grdPolicy.Rows[l].DefaultCellStyle.BackColor = Color.Green;
                                }
                                else
                                {
                                    grdPolicy.Rows[l].DefaultCellStyle.ForeColor = Color.Black;
                                    grdPolicy.Rows[l].DefaultCellStyle.BackColor = Color.Aqua;
                                }
                            }
                        }
                        if ((Convert.ToInt32(grdPolicy.Rows[l].Cells[44].Value.ToString()) == (int)eSTATES.POLICY_EXCEPTION) || (Convert.ToInt32(grdPolicy.Rows[l].Cells[44].Value.ToString()) == (int)eSTATES.POLICY_EXCEPTION))
                        {
                            grdPolicy.Rows[l].DefaultCellStyle.ForeColor = Color.Black;
                            grdPolicy.Rows[l].DefaultCellStyle.BackColor = Color.Red;
                        }
                        if ((Convert.ToInt32(grdPolicy.Rows[l].Cells[44].Value.ToString()) == (int)eSTATES.POLICY_ON_HOLD))
                        {
                            grdPolicy.Rows[l].DefaultCellStyle.ForeColor = Color.Black;
                            grdPolicy.Rows[l].DefaultCellStyle.BackColor = Color.Turquoise;
                        }
                        if ((Convert.ToInt32(grdPolicy.Rows[l].Cells[44].Value.ToString()) == (int)eSTATES.POLICY_MISSING))
                        {
                            grdPolicy.Rows[l].DefaultCellStyle.ForeColor = Color.Black;
                            grdPolicy.Rows[l].DefaultCellStyle.BackColor = Color.Magenta;
                        }
                    }

                }
                if (dt.Rows.Count > 0)
                {
                    grdPolicy.Columns[44].Visible = false;
                    //grdPolicy.Columns[0].Width = 40;
                    //grdPolicy.Columns[1].Width = 70;
                    //grdPolicy.Columns[2].Width = 120;
                    //grdPolicy.Columns[3].Width = 70;
                    //grdPolicy.Columns[4].Width = 70;
                    //grdPolicy.Columns[5].Width = 40;
                    //grdPolicy.Columns[6].Width = 50;
                    //grdPolicy.Columns[7].Width = 60;
                    //grdPolicy.Columns[8].Width = 60;
                    //grdPolicy.Columns[9].Width = 60;
                    //grdPolicy.Columns[10].Width = 30;
                    //grdPolicy.Columns[11].Width = 30;
                    //grdPolicy.Columns[12].Width = 30;
                    //grdPolicy.Columns[13].Width = 30;
                    //grdPolicy.Columns[14].Width = 30;
                    //grdPolicy.Columns[15].Width = 30;
                    //grdPolicy.Columns[16].Width = 30;
                    //grdPolicy.Columns[17].Width = 30;
                    //grdPolicy.Columns[18].Width = 30;
                    //grdPolicy.Columns[19].Width = 30;
                    //grdPolicy.Columns[20].Width = 30;
                    //grdPolicy.Columns[21].Width = 30;
                    //grdPolicy.Columns[22].Width = 30;
                    //grdPolicy.Columns[23].Width = 30;
                    //grdPolicy.Columns[24].Width = 30;
                    //grdPolicy.Columns[25].Width = 30;
                    //grdPolicy.Columns[26].Width = 30;
                }

            }
        }

        private void rdoShowAll_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBox1.Text.Trim()) <= 100)
            {
                if ((selBoxNo.ToString() != string.Empty) && (selBoxNo != 0))
                    PolicyDetails(selBoxNo.ToString());
            }
        }

        private void rdoChecked_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBox1.Text.Trim()) <= 100)
            {
                if ((selBoxNo.ToString() != string.Empty) && (selBoxNo != 0))
                    PolicyDetails(selBoxNo.ToString());
            }
        }

        private void rdoExceptions_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBox1.Text.Trim()) <= 100)
            {
                if ((selBoxNo.ToString() != string.Empty) && (selBoxNo != 0))
                    PolicyDetails(selBoxNo.ToString());
            }
        }

        private void rdoOnHold_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBox1.Text.Trim()) <= 100)
            {
                if ((selBoxNo.ToString() != string.Empty) && (selBoxNo != 0))
                    PolicyDetails(selBoxNo.ToString());
            }
        }

        private void rdoMissing_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBox1.Text.Trim()) <= 100)
            {
                if ((selBoxNo.ToString() != string.Empty) && (selBoxNo != 0))
                    PolicyDetails(selBoxNo.ToString());
            }
        }

        private void rdo150_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBox1.Text.Trim()) <= 100)
            {
                if ((selBoxNo.ToString() != string.Empty) && (selBoxNo != 0))
                    PolicyDetails(selBoxNo.ToString());
            }
        }

        void ClearPicBox()
        {
            pictureBox1.Image = null;
            pictureBox2.Image = null;
            pictureBox3.Image = null;
            pictureBox4.Image = null;
            pictureBox5.Image = null;
            pictureBox6.Image = null;
        }

        private string GetPolicyPath(string policyNo)
        {
            //policyLst = (ListBox)BoxDtls.Controls["lstPolicy"];
            wfeBatch wBatch = new wfeBatch(sqlCon);
            string batchPath = GetPath(Convert.ToInt32(cmbProject.SelectedValue.ToString()), Convert.ToInt32(cmbBatch.SelectedValue.ToString()));
            return batchPath + "\\" + policyNo;
        }
        public string GetPath(int prmProjKey, int prmBatchKey)
        {
            string sqlStr = null;
            DataSet projDs = new DataSet();
            string Path;

            try
            {
                sqlStr = @"select batch_path from batch_master where proj_code=" + prmProjKey + " and batch_key=" + prmBatchKey;
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(projDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();

                exMailLog.Log(ex);
            }
            if (projDs.Tables[0].Rows.Count > 0)
            {
                Path = projDs.Tables[0].Rows[0]["batch_path"].ToString();
            }
            else
                Path = string.Empty;

            return Path;
        }

        public ArrayList GetItems(eITEMS item, string case_file_no)
        {
            OdbcDataAdapter wAdap;
            OdbcTransaction trns = null;
            OdbcCommand oCom = new OdbcCommand();
            string strQuery = null;
            wItemControl wic = null;
            DataSet ds = new DataSet();
            string strQr = string.Empty;
            //wfePolicy queryPolicy = (wfePolicy)wi;
            ArrayList arrItem = new ArrayList();

            if (item == eITEMS.LIC_QA_PAGE)
            {
                strQuery = "select distinct A.proj_key,A.batch_key,A.box_number,A.policy_number,A.page_name,A.page_index_name,A.doc_type from image_master A,metadata_entry B where A.proj_key=B.proj_code and A.batch_key = B.batch_key  and A.policy_number = B.filename and A.photo <> 1 and A.proj_key=" + projCode + " and A.batch_key=" + batchCode + " and  A.policy_number='" + case_file_no + "' and a.status <> 29 and (b.status = 3 or b.status = 4 or b.status = 5 or b.status = 6 or b.status ='7' or b.status = '8' or b.status = '9' or b.status = '30' or b.status = '31' or b.status = '37' or b.status = '40' or b.status = '77') order by a.serial_no";

                oCom.Connection = sqlCon;
                oCom.CommandText = strQuery;
                wAdap = new OdbcDataAdapter(oCom);
                wAdap.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        wic = new CtrlImage(Convert.ToInt32(ds.Tables[0].Rows[i]["proj_key"].ToString()), Convert.ToInt32(ds.Tables[0].Rows[i]["batch_key"].ToString()), "1", ds.Tables[0].Rows[i]["policy_number"].ToString(), ds.Tables[0].Rows[i]["page_name"].ToString(), ds.Tables[0].Rows[i]["doc_type"].ToString());
                        arrItem.Add(wic);
                    }
                }
            }

            return arrItem;
        }

        private void grdPolicy_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ClearPicBox();

                firstDoc = true;
                DataSet expDs = new DataSet();
                clickedIndexValue = e.RowIndex;
                picControl.Image = null;
                lstImage.Items.Clear();

                policyNumber = grdPolicy.Rows[e.RowIndex].Cells[1].Value.ToString();
                policyLen = policyNumber.Length;


                if (grdPolicy.Rows[e.RowIndex].Cells[3].Value.ToString() == "Pension Case File")
                {
                    groupBox4.Visible = true;
                    groupBox3.Visible = false;
                    groupBox6.Visible = false;
                    groupBox10.Visible = false;
                    groupBox11.Visible = false;
                    groupBox12.Visible = false;
                    groupBox14.Visible = false;
                    groupBox15.Visible = false;

                    //pcf
                    deLabel3.Text = "File Name : " + grdPolicy.Rows[e.RowIndex].Cells[1].Value.ToString();
                    deTextBox1.Text = grdPolicy.Rows[e.RowIndex].Cells[4].Value.ToString();
                    deTextBox2.Text = grdPolicy.Rows[e.RowIndex].Cells[5].Value.ToString();
                    deTextBox4.Text = grdPolicy.Rows[e.RowIndex].Cells[6].Value.ToString();
                    deTextBox5.Text = grdPolicy.Rows[e.RowIndex].Cells[7].Value.ToString();
                    textBox2.Text = grdPolicy.Rows[e.RowIndex].Cells[8].Value.ToString();
                    textBox3.Text = grdPolicy.Rows[e.RowIndex].Cells[9].Value.ToString();
                    textBox4.Text = grdPolicy.Rows[e.RowIndex].Cells[11].Value.ToString();
                    deTextBox6.Text = grdPolicy.Rows[e.RowIndex].Cells[12].Value.ToString();
                    deTextBox9.Text = grdPolicy.Rows[e.RowIndex].Cells[13].Value.ToString();
                    deTextBox10.Text = grdPolicy.Rows[e.RowIndex].Cells[14].Value.ToString();
                    deTextBox3.Text = grdPolicy.Rows[e.RowIndex].Cells[15].Value.ToString();
                    deTextBox11.Text = grdPolicy.Rows[e.RowIndex].Cells[16].Value.ToString();
                    deTextBox7.Text = grdPolicy.Rows[e.RowIndex].Cells[18].Value.ToString();
                    deTextBox8.Text = grdPolicy.Rows[e.RowIndex].Cells[19].Value.ToString();
                    deTextBox12.Text = grdPolicy.Rows[e.RowIndex].Cells[20].Value.ToString();
                    deTextBox44.Text = grdPolicy.Rows[e.RowIndex].Cells[10].Value.ToString();
                    deTextBox46.Text = grdPolicy.Rows[e.RowIndex].Cells[32].Value.ToString();
                }
                else if (grdPolicy.Rows[e.RowIndex].Cells[3].Value.ToString() == "Pension Case Registers")
                {
                    groupBox3.Visible = true;
                    groupBox4.Visible = false;
                    groupBox6.Visible = false;
                    groupBox10.Visible = false;
                    groupBox11.Visible = false;
                    groupBox12.Visible = false;
                    groupBox14.Visible = false;
                    groupBox15.Visible = false;

                    //pcr
                    deLabel20.Text = "File Name : " + grdPolicy.Rows[e.RowIndex].Cells[1].Value.ToString();
                    deTextBox13.Text = grdPolicy.Rows[e.RowIndex].Cells[4].Value.ToString();
                    deTextBox14.Text = grdPolicy.Rows[e.RowIndex].Cells[17].Value.ToString();
                    deTextBox19.Text = grdPolicy.Rows[e.RowIndex].Cells[14].Value.ToString();
                    deTextBox16.Text = grdPolicy.Rows[e.RowIndex].Cells[5].Value.ToString();
                    deTextBox15.Text = grdPolicy.Rows[e.RowIndex].Cells[6].Value.ToString();
                    textBox5.Text = grdPolicy.Rows[e.RowIndex].Cells[8].Value.ToString();
                    textBox7.Text = grdPolicy.Rows[e.RowIndex].Cells[9].Value.ToString();
                    textBox8.Text = grdPolicy.Rows[e.RowIndex].Cells[10].Value.ToString();
                    textBox6.Text = grdPolicy.Rows[e.RowIndex].Cells[11].Value.ToString();
                    deTextBox18.Text = grdPolicy.Rows[e.RowIndex].Cells[12].Value.ToString();
                    deTextBox17.Text = grdPolicy.Rows[e.RowIndex].Cells[21].Value.ToString();
                }
                else if (grdPolicy.Rows[e.RowIndex].Cells[3].Value.ToString() == "Pension Rule Files")
                {
                    groupBox6.Visible = true;
                    groupBox4.Visible = false;
                    groupBox3.Visible = false;
                    groupBox10.Visible = false;
                    groupBox11.Visible = false;
                    groupBox12.Visible = false;
                    groupBox14.Visible = false;
                    groupBox15.Visible = false;

                    //prf
                    deLabel31.Text = "File Name : " + grdPolicy.Rows[e.RowIndex].Cells[1].Value.ToString();
                    deTextBox20.Text = grdPolicy.Rows[e.RowIndex].Cells[4].Value.ToString();
                    deTextBox21.Text = grdPolicy.Rows[e.RowIndex].Cells[22].Value.ToString();
                    deTextBox22.Text = grdPolicy.Rows[e.RowIndex].Cells[23].Value.ToString();
                    deTextBox23.Text = grdPolicy.Rows[e.RowIndex].Cells[24].Value.ToString();

                }
                else if (grdPolicy.Rows[e.RowIndex].Cells[3].Value.ToString() == "Ledger Cards")
                {
                    groupBox10.Visible = true;
                    groupBox4.Visible = false;
                    groupBox3.Visible = false;
                    groupBox6.Visible = false;
                    groupBox11.Visible = false;
                    groupBox12.Visible = false;
                    groupBox14.Visible = false;
                    groupBox15.Visible = false;

                    //glc
                    deLabel37.Text = "File Name : " + grdPolicy.Rows[e.RowIndex].Cells[1].Value.ToString();
                    deTextBox24.Text = grdPolicy.Rows[e.RowIndex].Cells[4].Value.ToString();
                    deTextBox26.Text = grdPolicy.Rows[e.RowIndex].Cells[25].Value.ToString();
                    deTextBox25.Text = grdPolicy.Rows[e.RowIndex].Cells[26].Value.ToString();
                    deTextBox27.Text = grdPolicy.Rows[e.RowIndex].Cells[27].Value.ToString();
                    textBox9.Text = grdPolicy.Rows[e.RowIndex].Cells[8].Value.ToString();
                    textBox10.Text = grdPolicy.Rows[e.RowIndex].Cells[28].Value.ToString();

                }
                else if (grdPolicy.Rows[e.RowIndex].Cells[3].Value.ToString() == "Nomination")
                {
                    groupBox11.Visible = true;
                    groupBox4.Visible = false;
                    groupBox3.Visible = false;
                    groupBox6.Visible = false;
                    groupBox10.Visible = false;
                    groupBox12.Visible = false;
                    groupBox14.Visible = false;
                    groupBox15.Visible = false;

                    //gn
                    deLabel49.Text = "File Name : " + grdPolicy.Rows[e.RowIndex].Cells[1].Value.ToString();
                    deTextBox31.Text = grdPolicy.Rows[e.RowIndex].Cells[4].Value.ToString();
                    deTextBox32.Text = grdPolicy.Rows[e.RowIndex].Cells[13].Value.ToString();
                    deTextBox30.Text = grdPolicy.Rows[e.RowIndex].Cells[25].Value.ToString();
                    deTextBox29.Text = grdPolicy.Rows[e.RowIndex].Cells[26].Value.ToString();
                    deTextBox28.Text = grdPolicy.Rows[e.RowIndex].Cells[27].Value.ToString();
                    textBox12.Text = grdPolicy.Rows[e.RowIndex].Cells[8].Value.ToString();
                    textBox11.Text = grdPolicy.Rows[e.RowIndex].Cells[29].Value.ToString();
                    deTextBox34.Text = grdPolicy.Rows[e.RowIndex].Cells[18].Value.ToString();
                    deTextBox33.Text = grdPolicy.Rows[e.RowIndex].Cells[19].Value.ToString();
                }
                else if (grdPolicy.Rows[e.RowIndex].Cells[3].Value.ToString() == "Final Payment Case File")
                {
                    groupBox12.Visible = true;
                    groupBox4.Visible = false;
                    groupBox3.Visible = false;
                    groupBox6.Visible = false;
                    groupBox10.Visible = false;
                    groupBox11.Visible = false;
                    groupBox14.Visible = false;
                    groupBox15.Visible = false;
                    
                    //gfp
                    deLabel62.Text = "File Name : " + grdPolicy.Rows[e.RowIndex].Cells[1].Value.ToString();
                    deTextBox41.Text = grdPolicy.Rows[e.RowIndex].Cells[4].Value.ToString();
                    deTextBox37.Text = grdPolicy.Rows[e.RowIndex].Cells[13].Value.ToString();
                    deTextBox40.Text = grdPolicy.Rows[e.RowIndex].Cells[25].Value.ToString();
                    deTextBox39.Text = grdPolicy.Rows[e.RowIndex].Cells[26].Value.ToString();
                    deTextBox38.Text = grdPolicy.Rows[e.RowIndex].Cells[27].Value.ToString();
                    textBox14.Text = grdPolicy.Rows[e.RowIndex].Cells[8].Value.ToString();
                    textBox13.Text = grdPolicy.Rows[e.RowIndex].Cells[29].Value.ToString();
                    deTextBox36.Text = grdPolicy.Rows[e.RowIndex].Cells[19].Value.ToString();
                    deTextBox35.Text = grdPolicy.Rows[e.RowIndex].Cells[18].Value.ToString();
                    deTextBox43.Text = grdPolicy.Rows[e.RowIndex].Cells[30].Value.ToString();
                    deTextBox42.Text = grdPolicy.Rows[e.RowIndex].Cells[31].Value.ToString();
                }
                else if (grdPolicy.Rows[e.RowIndex].Cells[2].Value.ToString().ToLower() == "ge")
                {
                    groupBox12.Visible = false;
                    groupBox4.Visible = false;
                    groupBox3.Visible = false;
                    groupBox6.Visible = false;
                    groupBox10.Visible = false;
                    groupBox11.Visible = false;
                    groupBox14.Visible = true;
                    groupBox15.Visible = false;
                    //ge
                    deLabel86.Text = "File Name : " + grdPolicy.Rows[e.RowIndex].Cells[1].Value.ToString();
                    deTextBox57.Text = grdPolicy.Rows[e.RowIndex].Cells[4].Value.ToString();
                    deTextBox56.Text = grdPolicy.Rows[e.RowIndex].Cells[5].Value.ToString();
                    deTextBox52.Text = grdPolicy.Rows[e.RowIndex].Cells[35].Value.ToString();
                    deTextBox51.Text = grdPolicy.Rows[e.RowIndex].Cells[33].Value.ToString();
                    deTextBox50.Text = grdPolicy.Rows[e.RowIndex].Cells[36].Value.ToString();
                    deTextBox54.Text = grdPolicy.Rows[e.RowIndex].Cells[34].Value.ToString();
                    deTextBox53.Text = grdPolicy.Rows[e.RowIndex].Cells[37].Value.ToString();
                    textBox15.Text = grdPolicy.Rows[e.RowIndex].Cells[8].Value.ToString();
                    textBox17.Text = grdPolicy.Rows[e.RowIndex].Cells[9].Value.ToString();
                    deTextBox45.Text = grdPolicy.Rows[e.RowIndex].Cells[38].Value.ToString();
                    textBox16.Text = grdPolicy.Rows[e.RowIndex].Cells[11].Value.ToString();


                }
                else if (grdPolicy.Rows[e.RowIndex].Cells[2].Value.ToString().ToLower() == "admin")
                {
                    groupBox12.Visible = false;
                    groupBox4.Visible = false;
                    groupBox3.Visible = false;
                    groupBox6.Visible = false;
                    groupBox10.Visible = false;
                    groupBox11.Visible = false;
                    groupBox14.Visible = false;
                    groupBox15.Visible = true;

                    //admin
                    deLabel94.Text = "File Name : " + grdPolicy.Rows[e.RowIndex].Cells[1].Value.ToString();
                    deTextBox61.Text = grdPolicy.Rows[e.RowIndex].Cells[4].Value.ToString();
                    deTextBox60.Text = grdPolicy.Rows[e.RowIndex].Cells[5].Value.ToString();
                    deTextBox55.Text = grdPolicy.Rows[e.RowIndex].Cells[6].Value.ToString();
                    deTextBox49.Text = grdPolicy.Rows[e.RowIndex].Cells[39].Value.ToString();
                    deTextBox48.Text = grdPolicy.Rows[e.RowIndex].Cells[40].Value.ToString();
                    deTextBox59.Text = grdPolicy.Rows[e.RowIndex].Cells[36].Value.ToString();
                    textBox18.Text = grdPolicy.Rows[e.RowIndex].Cells[8].Value.ToString();
                    textBox20.Text = grdPolicy.Rows[e.RowIndex].Cells[9].Value.ToString();
                    textBox19.Text = grdPolicy.Rows[e.RowIndex].Cells[11].Value.ToString();
                    


                }

                policyRowIndex = e.RowIndex;
                if (Convert.ToDouble(grdPolicy.Rows[e.RowIndex].Cells[41].Value.ToString()) > 0)
                {

                    lblTotFiles.Text = Convert.ToString(Math.Round(Convert.ToDouble(grdPolicy.Rows[e.RowIndex].Cells[41].Value.ToString()), 2));
                    lblAvgSize.Text = Convert.ToString(Math.Round(Convert.ToDouble(grdPolicy.Rows[e.RowIndex].Cells[42].Value.ToString()), 2)) + " KB";
                    lblDock.Text = Convert.ToString(Math.Round(Convert.ToDouble(grdPolicy.Rows[e.RowIndex].Cells[43].Value.ToString()), 2)) + " KB";
                    policyStatus = Convert.ToInt32(grdPolicy.Rows[e.RowIndex].Cells[44].Value.ToString());

                    if (policyStatus == (int)eSTATES.POLICY_EXPORTED || policyStatus == (int)9)
                    {
                        cmdAccepted.Enabled = false;
                        cmdRejected.Enabled = false;
                    }
                    else
                    {
                        cmdAccepted.Enabled = true;
                        cmdRejected.Enabled = true;
                    }
                    //lstImage.Items.Clear();
                    pPolicy = new CtrlPolicy(Convert.ToInt32(cmbProject.SelectedValue.ToString()), Convert.ToInt32(cmbBatch.SelectedValue.ToString()), boxNo, grdPolicy.Rows[e.RowIndex].Cells[1].Value.ToString());
                    wfePolicy policy = new wfePolicy(sqlCon, pPolicy);
                    //policyData = (udtPolicy)policy.LoadValuesFromDB();
                    policyPath = GetPolicyPath(policyNumber); //policyData.policy_path;
                    expDs = policy.GetAllException();
                    if (expDs.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToInt32(expDs.Tables[0].Rows[0]["missing_img_exp"].ToString()) == 1)
                        {
                            chkMissingImg.Checked = true;
                        }
                        else
                        {
                            chkMissingImg.Checked = false;
                        }

                        if (Convert.ToInt32(expDs.Tables[0].Rows[0]["crop_clean_exp"].ToString()) == 1)
                        {
                            chkCropClean.Checked = true;
                        }
                        else
                        {
                            chkCropClean.Checked = false;
                        }

                        if (Convert.ToInt32(expDs.Tables[0].Rows[0]["poor_scan_exp"].ToString()) == 1)
                        {
                            chkPoorScan.Checked = true;
                        }
                        else
                        {
                            chkPoorScan.Checked = false;
                        }
                        if (Convert.ToInt32(expDs.Tables[0].Rows[0]["wrong_indexing_exp"].ToString()) == 1)
                        {
                            //chkIndexing.Checked = true;
                        }
                        else
                        {
                            //chkIndexing.Checked = false;
                        }
                        if (Convert.ToInt32(expDs.Tables[0].Rows[0]["linked_policy_exp"].ToString()) == 1)
                        {
                            chkLinkedPolicy.Checked = true;
                        }
                        else
                        {
                            chkLinkedPolicy.Checked = false;
                        }
                        if (Convert.ToInt32(expDs.Tables[0].Rows[0]["decision_misd_exp"].ToString()) == 1)
                        {
                            chkDesicion.Checked = true;
                        }
                        else
                        {
                            chkDesicion.Checked = false;
                        }
                        if (Convert.ToInt32(expDs.Tables[0].Rows[0]["extra_page_exp"].ToString()) == 1)
                        {
                            chkExtraPage.Checked = true;
                        }
                        else
                        {
                            chkExtraPage.Checked = false;
                        }
                        if (Convert.ToInt32(expDs.Tables[0].Rows[0]["decision_misd_exp"].ToString()) == 1)
                        {
                            chkDesicion.Checked = true;
                        }
                        else
                        {
                            chkDesicion.Checked = false;
                        }
                        if (Convert.ToInt32(expDs.Tables[0].Rows[0]["rearrange_exp"].ToString()) == 1)
                        {
                            chkRearrange.Checked = true;
                        }
                        else
                        {
                            chkRearrange.Checked = false;
                        }
                        if (Convert.ToInt32(expDs.Tables[0].Rows[0]["other_exp"].ToString()) == 1)
                        {
                            chkOther.Checked = true;
                        }
                        else
                        {
                            chkOther.Checked = false;
                        }
                        if (Convert.ToInt32(expDs.Tables[0].Rows[0]["move_to_respective_policy_exp"].ToString()) == 1)
                        {
                            chkMove.Checked = true;
                        }
                        else
                        {
                            chkMove.Checked = false;
                        }
                        txtComments.Text = expDs.Tables[0].Rows[0]["comments"].ToString() + "\r\n";
                        txtComments.SelectionStart = txtComments.Text.Length;
                        txtComments.ScrollToCaret();
                        txtComments.Refresh();
                    }
                    else
                    {
                        chkMissingImg.Checked = false;
                        chkCropClean.Checked = false;
                        chkPoorScan.Checked = false;
                        //chkIndexing.Checked = false;
                        chkLinkedPolicy.Checked = false;
                        chkDesicion.Checked = false;
                        chkExtraPage.Checked = false;
                        chkDesicion.Checked = false;
                        chkRearrange.Checked = false;
                        chkOther.Checked = false;
                        chkMove.Checked = false;
                        txtComments.Text = string.Empty;
                    }

                    ArrayList arrImage = new ArrayList();
                    wQuery pQuery = new ihwQuery(sqlCon);
                    eSTATES[] state = new eSTATES[5];
                    state[0] = eSTATES.POLICY_CHECKED;
                    state[1] = eSTATES.POLICY_FQC;
                    state[2] = eSTATES.POLICY_INDEXED;
                    state[3] = eSTATES.POLICY_EXCEPTION;
                    state[4] = eSTATES.POLICY_EXPORTED;
                    CtrlImage ctrlImage;
                    arrImage = GetItems(eITEMS.LIC_QA_PAGE, policyNumber);
                    for (int i = 0; i < arrImage.Count; i++)
                    {
                        ctrlImage = (CtrlImage)arrImage[i];
                        if (ctrlImage.DocType != string.Empty)
                        {
                            lstImage.Items.Add(ctrlImage.ImageName);
                        }
                        else
                            lstImage.Items.Add(ctrlImage.ImageName);
                    }

                    tabControl1.SelectedIndex = 1;
                    if (lstImage.Items.Count > 0)
                    {
                        lstImage.SelectedIndex = 0;
                        cmdAccepted.Enabled = true;
                        cmdRejected.Enabled = true;
                    }

                }
                else
                {
                    cmdAccepted.Enabled = false;
                    cmdRejected.Enabled = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while getting the information of the selected policy.....");
                exMailLog.Log(ex);
            }
        }

        private bool GetThumbnailImageAbort()
        {
            return false;
        }

        private void ShowThumbImage(string pDocType)
        {
            DataSet ds = new DataSet();
            string imageFileName;
            Image imgNew = null;
            IContainerControl icc = tabControl2.GetContainerControl();

            //tabControl2.SelectedIndex = 1;
            //picBig.Visible = false;
            //panelBig.Visible = false;
            //picBig.Image = null;
            System.Drawing.Image imgThumbNail = null;

            pImage = new CtrlImage(Convert.ToInt32(projCode), Convert.ToInt32(batchCode), boxNo, policyNumber, string.Empty, pDocType);
            wfeImage wImage = new wfeImage(sqlCon, pImage);
            ds = wImage.GetAllIndexedImageName();
            ClearPicBox();
            if (ds.Tables[0].Rows.Count > 0)
            {
                imageName = new string[ds.Tables[0].Rows.Count];
                if (ds.Tables[0].Rows.Count <= 6)
                {
                    pgOne.Visible = true;
                    pgTwo.Visible = false;
                    pgThree.Visible = false;
                }
                if ((ds.Tables[0].Rows.Count > 6) && (ds.Tables[0].Rows.Count <= 12))
                {
                    pgOne.Visible = true;
                    pgTwo.Visible = true;
                    pgThree.Visible = false;
                }
                if ((ds.Tables[0].Rows.Count > 12) && (ds.Tables[0].Rows.Count <= 14))
                {
                    pgOne.Visible = true;
                    pgTwo.Visible = true;
                    pgThree.Visible = true;
                }
                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                {
                    imageFileName = picPath + "\\" + ds.Tables[0].Rows[j][0].ToString();
                    imgAll.LoadBitmapFromFile(imageFileName);

                    if (imgAll.GetBitmap().PixelFormat == PixelFormat.Format24bppRgb)
                    {
                        try
                        {
                            imgAll.GetLZW("tmp.TIF");
                            imgNew = Image.FromFile("tmp.TIF");
                            imgThumbNail = imgNew;
                        }
                        catch (Exception ex)
                        {
                            string err = ex.Message;
                        }
                    }
                    else
                    {
                        imgThumbNail = System.Drawing.Image.FromFile(imageFileName);
                    }
                    imageName[j] = imageFileName;
                    if (!System.IO.File.Exists(imageFileName)) return;
                    //imgThumbNail = Image.FromFile(imageFileName);
                    double scaleX = (double)pictureBox1.Width / (double)imgThumbNail.Width;
                    double scaleY = (double)pictureBox1.Height / (double)imgThumbNail.Height;
                    double Scale = Math.Min(scaleX, scaleY);
                    int w = (int)(imgThumbNail.Width * Scale);
                    int h = (int)(imgThumbNail.Height * Scale);
                    w = w - 5;
                    imgThumbNail = imgThumbNail.GetThumbnailImage(w, h, new System.Drawing.Image.GetThumbnailImageAbort(GetThumbnailImageAbort), IntPtr.Zero);

                    if (j == 0)
                    {
                        pictureBox1.Image = imgThumbNail;
                        pictureBox1.Tag = imageFileName;
                    }
                    if (j == 1)
                    {
                        pictureBox2.Image = imgThumbNail;
                        pictureBox2.Tag = imageFileName;
                    }
                    if (j == 2)
                    {
                        pictureBox3.Image = imgThumbNail;
                        pictureBox3.Tag = imageFileName;
                    }
                    if (j == 3)
                    {
                        pictureBox4.Image = imgThumbNail;
                        pictureBox4.Tag = imageFileName;
                    }
                    if (j == 4)
                    {
                        pictureBox5.Image = imgThumbNail;
                        pictureBox5.Tag = imageFileName;
                    }
                    if (j == 5)
                    {
                        pictureBox6.Image = imgThumbNail;
                        pictureBox6.Tag = imageFileName;
                    }
                    if (imgNew != null)
                    {
                        imgNew.Dispose();
                        imgNew = null;
                        if (File.Exists("tmp.tif"))
                            File.Delete("tmp.TIF");
                    }
                }
            }
            else
            {
                ClearPicBox();
                imageName = null;
            }

        }
        private void ChangeSize()
        {
            Image imgTot = null;
            try
            {
                if (img.IsValid() == true)
                {
                    if (img.GetBitmap().PixelFormat == PixelFormat.Format1bppIndexed)
                    {
                        picControl.Height = tabControl1.Height - 75;
                        picControl.Width = tabControl2.Width - 30;
                        if (!System.IO.File.Exists(imgFileName)) return;
                        Image newImage;
                        imgAll.LoadBitmapFromFile(imgFileName);
                        if (imgAll.GetBitmap().PixelFormat == PixelFormat.Format24bppRgb)
                        {
                            imgAll.GetLZW("tmp1.TIF");
                            imgTot = Image.FromFile("tmp1.TIF");
                            newImage = imgTot;
                            //File.Delete("tmp1.TIF");
                        }
                        else
                        {
                            newImage = System.Drawing.Image.FromFile(imgFileName);
                        }

                        double scaleX = (double)picControl.Width / (double)newImage.Width;
                        double scaleY = (double)picControl.Height / (double)newImage.Height;
                        double Scale = Math.Min(scaleX, scaleY);
                        int w = (int)(newImage.Width * Scale);
                        int h = (int)(newImage.Height * Scale);
                        picControl.Width = w;
                        picControl.Height = h;
                        picControl.Image = newImage.GetThumbnailImage(w, h, new System.Drawing.Image.GetThumbnailImageAbort(GetThumbnailImageAbort), IntPtr.Zero);
                        newImage.Dispose();
                        picControl.Refresh();
                        if (imgTot != null)
                        {
                            imgTot.Dispose();
                            imgTot = null;
                            if (File.Exists("tmp1.tif"))
                                File.Delete("tmp1.TIF");
                        }
                    }
                    else
                    {
                        picControl.Height = tabControl1.Height - 75;
                        picControl.Width = tabControl2.Width - 100;
                        img.LoadBitmapFromFile(imgFileName);
                        picControl.Image = img.GetBitmap();
                        picControl.SizeMode = PictureBoxSizeMode.StretchImage;
                        picControl.Refresh();
                    }
                }
            }
            catch (Exception ex)
            {
                exMailLog.Log(ex);
                MessageBox.Show("Error ..." + ex.Message, "Error");
            }
        }

        private void lstImage_SelectedIndexChanged(object sender, EventArgs e)
        {
            int pos;
            string changedImage = null;
            double fileSize;
            string currntDoc;
            wfeImage wImage = null;
            //string photoImageName=null;

            try
            {
                pos = lstImage.SelectedItem.ToString().IndexOf("-");
                if (pos < 0)
                {
                    changedImage = lstImage.SelectedItem.ToString();
                }
                else
                { changedImage = lstImage.SelectedItem.ToString(); }

                //changedImage=lstImage.SelectedItem.ToString().Substring(0,pos);
                pImage = new CtrlImage(Convert.ToInt32(cmbProject.SelectedValue.ToString()), Convert.ToInt32(cmbBatch.SelectedValue.ToString()), boxNo, policyNumber, changedImage, string.Empty);
                wImage = new wfeImage(sqlCon, pImage);
                changedImage = wImage.GetIndexedImageName();

                if ((policyStatus == (int)4) || (policyStatus == (int)5) || (policyStatus == (int)eSTATES.POLICY_CHECKED) || (policyStatus == (int)eSTATES.POLICY_EXCEPTION) || (policyStatus == (int)eSTATES.POLICY_EXPORTED))
                {
                    if (Directory.Exists(policyPath + "\\" + ihConstants._FQC_FOLDER))
                    {
                        picPath = policyPath + "\\" + ihConstants._FQC_FOLDER;
                        imagePath = policyPath + "\\" + ihConstants._FQC_FOLDER + "\\" + changedImage;
                        if (changedImage.Substring(policyLen, 6) == "_000_A")
                        {
                            imgFileName = policyPath + "\\" + ihConstants._FQC_FOLDER + "\\" + changedImage;
                            if (File.Exists(imgFileName) == false)
                            {
                                imgFileName = policyPath + "\\" + ihConstants._FQC_FOLDER + "\\" + changedImage;
                                picPath = policyPath + "\\" + ihConstants._FQC_FOLDER;
                            }
                            //img.SaveAsTiff(policyPath + "\\" + ihConstants._FQC_FOLDER + "\\" + changedImage, IGRComressionTIFF.JPEG);
                            photoPath = imagePath;
                        }
                        else
                        {
                            imgFileName = policyPath + "\\" + ihConstants._FQC_FOLDER + "\\" + changedImage;
                            if (File.Exists(imgFileName) == true)
                            {
                                imgFileName = policyPath + "\\" + ihConstants._FQC_FOLDER + "\\" + changedImage;
                                picPath = policyPath + "\\" + ihConstants._FQC_FOLDER;
                            }
                        }
                    }
                    else
                    {
                        imagePath = policyPath + "\\" + ihConstants._FQC_FOLDER + "\\" + changedImage;
                        picPath = policyPath + "\\" + ihConstants._FQC_FOLDER;
                        if (changedImage.Substring(policyLen, 6) == "_000_A")
                        {
                            imgFileName = policyPath + "\\" + ihConstants._FQC_FOLDER + "\\" + changedImage;
                            img.LoadBitmapFromFile(imgFileName);
                            //img.SaveAsTiff(policyPath + "\\" + ihConstants._INDEXING_FOLDER + "\\" + changedImage, IGRComressionTIFF.JPEG);
                            photoPath = imagePath;
                        }
                        else
                        {
                            imgFileName = policyPath + "\\" + ihConstants._FQC_FOLDER + "\\" + changedImage;

                        }
                    }

                }
                else
                {
                    picPath = policyPath + "\\" + ihConstants._FQC_FOLDER;
                    imagePath = policyPath + "\\" + ihConstants._FQC_FOLDER + "\\" + changedImage;
                    if (changedImage.Substring(policyLen, 6) == "_000_A")
                    {
                        imgFileName = policyPath + "\\" + ihConstants._FQC_FOLDER + "\\" + changedImage;
                        if (File.Exists(imgFileName) == true)
                        {
                            imgFileName = policyPath + "\\" + ihConstants._FQC_FOLDER + "\\" + changedImage;
                            picPath = policyPath + "\\" + ihConstants._FQC_FOLDER;
                        }
                        //img.SaveAsTiff(policyPath + "\\" + ihConstants._FQC_FOLDER + "\\" + changedImage, IGRComressionTIFF.JPEG);
                        photoPath = imagePath;
                    }
                    else
                    {
                        imgFileName = policyPath + "\\" + ihConstants._FQC_FOLDER + "\\" + changedImage;
                        if (File.Exists(imgFileName) == false)
                        {
                            imgFileName = policyPath + "\\" + ihConstants._FQC_FOLDER + "\\" + changedImage;
                            picPath = policyPath + "\\" + ihConstants._FQC_FOLDER;
                        }
                    }

                }
                System.IO.FileInfo info = new System.IO.FileInfo(imgFileName);

                fileSize = info.Length;
                fileSize = fileSize / 1024;
                lblImageSize.Text = Convert.ToString(Math.Round(fileSize, 2)) + " KB";
                img.LoadBitmapFromFile(imgFileName);
                int dashPos = lstImage.SelectedItem.ToString().IndexOf("-") + 1;
                //currntDoc = lstImage.Items[lstImage.SelectedIndex].ToString().Substring(dashPos);

                //if ((prevDoc != currntDoc))
                //{
                //    ListViewItem lvwItem = lvwDockTypes.FindItemWithText(currntDoc);
                //    lvwDockTypes.Items[lvwItem.Index].Selected = true;
                //}
                //firstDoc = false;
                if (imgFileName != string.Empty)
                {
                    ChangeSize();
                }
                //prevDoc = currntDoc;
                //ChangeSize();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while generating the preview....");
                exMailLog.Log(ex);
            }
        }

        private void cmdNext_Click(object sender, EventArgs e)
        {
            ListViewItem lvwItem;
            if (tabControl2.SelectedIndex == 0)
            {
                if (lstImage.Items.Count > 0)
                {
                    if ((lstImage.Items.Count - 1) != lstImage.SelectedIndex)
                    {
                        lstImage.SelectedIndex = lstImage.SelectedIndex + 1;
                    }
                }
                //if (tabControl2.SelectedIndex == 1)
                //{
                //    if (lstImage.SelectedIndex != 0)
                //    {
                //        int dashPos = lstImage.SelectedItem.ToString().IndexOf("-") + 1;
                //        string currntDoc = lstImage.Items[lstImage.SelectedIndex - 1].ToString().Substring(dashPos);
                //        string prevDoc = lstImage.Items[lstImage.SelectedIndex].ToString().Substring(dashPos);
                //        if (currntDoc != prevDoc)
                //        {
                //            //lvwItem = lvwDockTypes.FindItemWithText(prevDoc);
                //            //lvwDockTypes.Items[lvwItem.Index].Selected = true;
                //            //lvwDockTypes.Focus();
                //            //lstImage.Focus();
                //        }
                //    }
                //}
            }
        }

        private void ThumbnailChangeSize(string fName)
        {
            Image imgTot = null;
            try
            {
                //picBig.Height = tabControl1.Height - 75;
                //picBig.Width = tabControl2.Width - 30;
                //if (!System.IO.File.Exists(fName)) return;
                //Image newImage;
                //imgAll.LoadBitmapFromFile(fName);
                //if (imgAll.GetBitmap().PixelFormat == PixelFormat.Format24bppRgb)
                //{
                //    imgAll.GetLZW("tmp1.TIF");
                //    imgTot = Image.FromFile("tmp1.TIF");
                //    newImage = imgTot;
                //}
                //else
                //{
                //    newImage = System.Drawing.Image.FromFile(fName);
                //}
                //double scaleX = (double)picBig.Width / (double)newImage.Width;
                //double scaleY = (double)picBig.Height / (double)newImage.Height;
                //double Scale = Math.Min(scaleX, scaleY);
                //int w = (int)(newImage.Width * Scale);
                //int h = (int)(newImage.Height * Scale);
                //picBig.Width = w;
                //picBig.Height = h;
                //picBig.Image = newImage.GetThumbnailImage(w, h, new System.Drawing.Image.GetThumbnailImageAbort(GetThumbnailImageAbort), IntPtr.Zero);
                //newImage.Dispose();
                //picBig.Refresh();
                //if (imgTot != null)
                //{
                //    imgTot.Dispose();
                //    imgTot = null;
                //    if (File.Exists("tmp1.tif"))
                //        File.Delete("tmp1.TIF");
                //}
            }
            catch (Exception ex)
            {
                exMailLog.Log(ex);
                MessageBox.Show("Error ..." + ex.Message, "Error");
            }
        }

        private void cmdPrevious_Click(object sender, EventArgs e)
        {
            ListViewItem lvwItem;
            if (tabControl2.SelectedIndex == 0)
            {
                if (lstImage.SelectedIndex != 0)
                {
                    lstImage.SelectedIndex = lstImage.SelectedIndex - 1;
                }
                //if (tabControl2.SelectedIndex == 1)
                //{
                //    if (lstImage.SelectedIndex != 0)
                //    {
                //        int dashPos = lstImage.SelectedItem.ToString().IndexOf("-") + 1;
                //        string currntDoc = lstImage.Items[lstImage.SelectedIndex].ToString().Substring(dashPos);
                //        string prevDoc = lstImage.Items[lstImage.SelectedIndex + 1].ToString().Substring(dashPos);
                //        if (currntDoc != prevDoc)
                //        {
                //            lvwItem = lvwDockTypes.FindItemWithText(currntDoc);
                //            lvwDockTypes.Items[lvwItem.Index].Selected = true;
                //            lvwDockTypes.Focus();
                //        }
                //    }
                //}
            }
        }

        private void cmdZoomIn_Click(object sender, EventArgs e)
        {
            ZoomIn();
        }

        private void cmdZoomOut_Click(object sender, EventArgs e)
        {
            ZoomOut();
        }

        int ZoomIn()
        {
            try
            {
                if (img.IsValid() == true)
                {
                    picControl.Dock = DockStyle.None;
                    //OperationInProgress = ihConstants._OTHER_OPERATION;
                    keyPressed = keyPressed + 1;
                    zoomHeight = Convert.ToInt32(img.GetBitmap().Height * (1.2));
                    zoomWidth = Convert.ToInt32(img.GetBitmap().Width * (1.2));
                    zoomSize.Height = zoomHeight;
                    zoomSize.Width = zoomWidth;

                    picControl.Width = Convert.ToInt32(Convert.ToDouble(picControl.Width) * 1.2);
                    picControl.Height = Convert.ToInt32(Convert.ToDouble(picControl.Height) * 1.2);
                    picControl.Refresh();
                    ChangeZoomSize();

                    //delinsrtBol = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while zooming the image " + ex.Message, "Zoom Error");
                exMailLog.Log(ex);
            }
            return 0;
        }
        int ZoomOut()
        {
            try
            {
                if (keyPressed > 0)
                {
                    picControl.Dock = DockStyle.None;
                    //OperationInProgress = ihConstants._OTHER_OPERATION;
                    keyPressed = keyPressed + 1;
                    zoomHeight = Convert.ToInt32(img.GetBitmap().Height / (1.2));
                    zoomWidth = Convert.ToInt32(img.GetBitmap().Width / (1.2));
                    zoomSize.Height = zoomHeight;
                    zoomSize.Width = zoomWidth;

                    picControl.Width = Convert.ToInt32(Convert.ToDouble(picControl.Width) / 1.2);
                    picControl.Height = Convert.ToInt32(Convert.ToDouble(picControl.Height) / 1.2);
                    picControl.Refresh();
                    ChangeZoomSize();
                    //delinsrtBol = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while zooming the image " + ex.Message, "Zoom Error");
            }
            return 0;
        }

        private void ChangeZoomSize()
        {
            if (!System.IO.File.Exists(imgFileName)) return;
            Image newImage = Image.FromFile(imgFileName);
            double scaleX = (double)picControl.Width / (double)newImage.Width;
            double scaleY = (double)picControl.Height / (double)newImage.Height;
            double Scale = Math.Min(scaleX, scaleY);
            int w = (int)(newImage.Width * Scale);
            int h = (int)(newImage.Height * Scale);
            picControl.Width = w;
            picControl.Height = h;
            picControl.Image = newImage.GetThumbnailImage(w, h, new System.Drawing.Image.GetThumbnailImageAbort(GetThumbnailImageAbort), IntPtr.Zero);
            picControl.Invalidate();
            newImage.Dispose();
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewItem lvwItem;
            string currntDoc = string.Empty;
            if (tabControl2.SelectedIndex == 1)
            {
                firstDoc = false;
                //for (int i = 0; i < lvwDockTypes.Items.Count; i++)
                //{
                //    if (lvwDockTypes.Items[i].Selected == true)
                //    {
                //        currntDoc = lvwDockTypes.Items[i].SubItems[0].Text;
                //        break;
                //    }
                //}
                //if (currntDoc != string.Empty)
                //{
                //    lvwItem = lvwDockTypes.FindItemWithText(currntDoc);
                //    lvwDockTypes.Items[lvwItem.Index].Selected = true;
                //}
            }
            else
            {
                ChangeSize();
            }
            //lvwDockTypes.Focus();
        }

        private void tabControl2_TabIndexChanged(object sender, EventArgs e)
        {
            if (imgFileName != string.Empty)
            {
                if (tabControl2.SelectedIndex == 0)
                    ChangeSize();
                ThumbnailChangeSize(imgFileName);
            }
        }


        private int GetDocTypePos()
        {
            string currntDoc;
            int index = 0;
            string srchStr;



            return index;
        }
        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            //Bitmap bmp;
            //picBig.Image = null;
            if (imageName != null)
            {
                if (imageName.Length >= 1)
                {
                    //ThumbnailChangeSize(pictureBox1.Tag.ToString());

                    int lstIndex;
                    lstIndex = (currntPg * 6) + 0 + GetDocTypePos();

                    if (lstIndex < lstImage.Items.Count)
                    {
                        lstImage.SelectedIndex = lstIndex;
                    }
                    tabControl2.SelectedIndex = 0;

                    //picBig.Visible = true;
                    //panelBig.Visible = true;
                }
            }
        }

        private void pictureBox2_DoubleClick(object sender, EventArgs e)
        {
            //Bitmap bmp;
            //picBig.Image = null;
            if (imageName != null)
            {
                if (imageName.Length >= 2)
                {

                    //ThumbnailChangeSize(pictureBox2.Tag.ToString());
                    int lstIndex;
                    lstIndex = (currntPg * 6) + 1 + GetDocTypePos();
                    if (lstIndex < lstImage.Items.Count)
                    {
                        lstImage.SelectedIndex = lstIndex;
                    }
                    tabControl2.SelectedIndex = 0;

                    //picBig.Visible = true;
                    //panelBig.Visible = true;
                }
            }
        }

        private void pictureBox3_DoubleClick(object sender, EventArgs e)
        {
            //picBig.Image = null;
            if (imageName != null)
            {
                if (imageName.Length >= 3)
                {

                    //ThumbnailChangeSize(pictureBox3.Tag.ToString());
                    int lstIndex;
                    lstIndex = (currntPg * 6) + 2 + GetDocTypePos();
                    if (lstIndex < lstImage.Items.Count)
                    {
                        lstImage.SelectedIndex = lstIndex;
                    }
                    tabControl2.SelectedIndex = 0;

                    //picBig.Visible = true;
                    //panelBig.Visible = true;
                }
            }
        }

        private void pictureBox4_DoubleClick(object sender, EventArgs e)
        {
            //Bitmap bmp;
            //picBig.Image = null;
            if (imageName != null)
            {
                if (imageName.Length >= 4)
                {

                    //ThumbnailChangeSize(pictureBox4.Tag.ToString());
                    int lstIndex;
                    lstIndex = (currntPg * 6) + 3 + GetDocTypePos();
                    if (lstIndex < lstImage.Items.Count)
                    {
                        lstImage.SelectedIndex = lstIndex;
                    }
                    tabControl2.SelectedIndex = 0;

                    //picBig.Visible = true;
                    //panelBig.Visible = true;
                }
            }
        }

        private void pictureBox5_DoubleClick(object sender, EventArgs e)
        {
            //Bitmap bmp;
            //picBig.Image = null;
            if (imageName != null)
            {
                if (imageName.Length >= 5)
                {

                    //ThumbnailChangeSize(pictureBox5.Tag.ToString());
                    int lstIndex;
                    lstIndex = (currntPg * 6) + 4 + GetDocTypePos();
                    if (lstIndex < lstImage.Items.Count)
                    {
                        lstImage.SelectedIndex = lstIndex;
                    }
                    tabControl2.SelectedIndex = 0;

                    //picBig.Visible = true;
                    //panelBig.Visible = true;
                }
            }
        }

        private void pictureBox6_DoubleClick(object sender, EventArgs e)
        {
            //Bitmap bmp;
            //picBig.Image = null;
            if (imageName != null)
            {
                if (imageName.Length >= 6)
                {

                    //ThumbnailChangeSize(pictureBox6.Tag.ToString());
                    int lstIndex;
                    lstIndex = (currntPg * 6) + 5 + GetDocTypePos();
                    if (lstIndex < lstImage.Items.Count)
                    {
                        lstImage.SelectedIndex = lstIndex;
                    }
                    tabControl2.SelectedIndex = 0;
                    //lvwDockTypes.Focus();
                    //picBig.Visible = true;
                    //panelBig.Visible = true;
                }
            }
        }

        private void pgOne_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string imageFileName;
            Image imgNew = null;
            tabControl2.SelectedIndex = 1;

            System.Drawing.Image imgThumbNail = null;
            ClearPicBox();
            for (int i = 0; i < imageName.Length; i++)
            {
                imageFileName = imageName[i];
                if (!System.IO.File.Exists(imageFileName)) return;
                imgAll.LoadBitmapFromFile(imageFileName);

                if (imgAll.GetBitmap().PixelFormat == PixelFormat.Format24bppRgb)
                {
                    try
                    {
                        imgAll.GetLZW("tmp.TIF");
                        imgNew = Image.FromFile("tmp.TIF");
                        imgThumbNail = imgNew;
                    }
                    catch (Exception ex)
                    {
                        string err = ex.Message;
                    }
                }
                else
                {
                    imgThumbNail = System.Drawing.Image.FromFile(imageFileName);
                }
                double scaleX = (double)pictureBox1.Width / (double)imgThumbNail.Width;
                double scaleY = (double)pictureBox1.Height / (double)imgThumbNail.Height;
                double Scale = Math.Min(scaleX, scaleY);
                int w = (int)(imgThumbNail.Width * Scale);
                int h = (int)(imgThumbNail.Height * Scale);
                w = w - 5;
                imgThumbNail = imgThumbNail.GetThumbnailImage(w, h, new System.Drawing.Image.GetThumbnailImageAbort(GetThumbnailImageAbort), IntPtr.Zero);
                currntPg = 0;
                if (i == 0)
                {
                    pictureBox1.Image = imgThumbNail;
                    pictureBox1.Tag = imageFileName;
                }
                if (i == 1)
                {
                    pictureBox2.Image = imgThumbNail;
                    pictureBox2.Tag = imageFileName;
                }
                if (i == 2)
                {
                    pictureBox3.Image = imgThumbNail;
                    pictureBox3.Tag = imageFileName;
                }
                if (i == 3)
                {
                    pictureBox4.Image = imgThumbNail;
                    pictureBox4.Tag = imageFileName;
                }
                if (i == 4)
                {
                    pictureBox5.Image = imgThumbNail;
                    pictureBox5.Tag = imageFileName;
                }
                if (i == 5)
                {
                    pictureBox6.Image = imgThumbNail;
                    pictureBox6.Tag = imageFileName;
                }
                if (imgNew != null)
                {
                    imgNew.Dispose();
                    imgNew = null;
                    if (File.Exists("tmp.tif"))
                        File.Delete("tmp.TIF");
                }
            }
        }

        private void pgTwo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string imageFileName;
            Image imgNew = null;
            tabControl2.SelectedIndex = 1;

            System.Drawing.Image imgThumbNail = null;
            ClearPicBox();
            for (int i = 6; i < imageName.Length; i++)
            {
                imageFileName = imageName[i];
                if (!System.IO.File.Exists(imageFileName)) return;
                imgAll.LoadBitmapFromFile(imageFileName);
                currntPg = 1;
                if (imgAll.GetBitmap().PixelFormat == PixelFormat.Format24bppRgb)
                {
                    try
                    {
                        imgAll.GetLZW("tmp.TIF");
                        imgNew = Image.FromFile("tmp.TIF");
                        imgThumbNail = imgNew;
                    }
                    catch (Exception ex)
                    {
                        string err = ex.Message;
                    }
                }
                else
                {
                    imgThumbNail = System.Drawing.Image.FromFile(imageFileName);
                }
                double scaleX = (double)pictureBox1.Width / (double)imgThumbNail.Width;
                double scaleY = (double)pictureBox1.Height / (double)imgThumbNail.Height;
                double Scale = Math.Min(scaleX, scaleY);
                int w = (int)(imgThumbNail.Width * Scale);
                int h = (int)(imgThumbNail.Height * Scale);
                w = w - 5;
                imgThumbNail = imgThumbNail.GetThumbnailImage(w, h, new System.Drawing.Image.GetThumbnailImageAbort(GetThumbnailImageAbort), IntPtr.Zero);

                if (i == 6)
                {
                    pictureBox1.Image = imgThumbNail;
                    pictureBox1.Tag = imageFileName;
                }
                if (i == 7)
                {
                    pictureBox2.Image = imgThumbNail;
                    pictureBox2.Tag = imageFileName;
                }
                if (i == 8)
                {
                    pictureBox3.Image = imgThumbNail;
                    pictureBox3.Tag = imageFileName;
                }
                if (i == 9)
                {
                    pictureBox4.Image = imgThumbNail;
                    pictureBox4.Tag = imageFileName;
                }
                if (i == 10)
                {
                    pictureBox5.Image = imgThumbNail;
                    pictureBox5.Tag = imageFileName;
                }
                if (i == 11)
                {
                    pictureBox6.Image = imgThumbNail;
                    pictureBox6.Tag = imageFileName;
                }
                if (imgNew != null)
                {
                    imgNew.Dispose();
                    imgNew = null;
                    if (File.Exists("tmp.tif"))
                        File.Delete("tmp.TIF");
                }
            }
        }

        private void pgThree_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string imageFileName;
            Image imgNew = null;
            tabControl2.SelectedIndex = 1;

            System.Drawing.Image imgThumbNail = null;
            ClearPicBox();
            for (int i = 0; i < imageName.Length; i++)
            {
                imageFileName = imageName[i];
                if (!System.IO.File.Exists(imageFileName)) return;
                imgAll.LoadBitmapFromFile(imageFileName);
                currntPg = 2;
                if (imgAll.GetBitmap().PixelFormat == PixelFormat.Format24bppRgb)
                {
                    try
                    {
                        imgAll.GetLZW("tmp.TIF");
                        imgNew = Image.FromFile("tmp.TIF");
                        imgThumbNail = imgNew;
                    }
                    catch (Exception ex)
                    {
                        string err = ex.Message;
                    }
                }
                else
                {
                    imgThumbNail = System.Drawing.Image.FromFile(imageFileName);
                }
                double scaleX = (double)pictureBox1.Width / (double)imgThumbNail.Width;
                double scaleY = (double)pictureBox1.Height / (double)imgThumbNail.Height;
                double Scale = Math.Min(scaleX, scaleY);
                int w = (int)(imgThumbNail.Width * Scale);
                int h = (int)(imgThumbNail.Height * Scale);
                w = w - 5;
                imgThumbNail = imgThumbNail.GetThumbnailImage(w, h, new System.Drawing.Image.GetThumbnailImageAbort(GetThumbnailImageAbort), IntPtr.Zero);

                if (i == 12)
                {
                    pictureBox1.Image = imgThumbNail;
                    pictureBox1.Tag = imageFileName;
                }
                if (i == 13)
                {
                    pictureBox2.Image = imgThumbNail;
                    pictureBox2.Tag = imageFileName;
                }
                if (i == 14)
                {
                    pictureBox3.Image = imgThumbNail;
                    pictureBox3.Tag = imageFileName;
                }
                if (imgNew != null)
                {
                    imgNew.Dispose();
                    imgNew = null;
                    if (File.Exists("tmp.tif"))
                        File.Delete("tmp.TIF");
                }
            }
        }

        private void chkMissingImg_CheckedChanged(object sender, EventArgs e)
        {
            int tifPos;
            string origDoctype = string.Empty;
            if (lstImage.SelectedIndex >= 0)
            {
                tifPos = lstImage.SelectedItem.ToString().IndexOf("-") + 1;
                string imgNumber;
                imgNumber = lstImage.SelectedItem.ToString();
                if (tifPos > 0)
                {
                    origDoctype = lstImage.SelectedItem.ToString();
                }
                else
                {
                    origDoctype = lstImage.SelectedItem.ToString();
                }
                if (chkMissingImg.Checked)
                {
                    txtComments.Text = txtComments.Text + imgNumber + "-" + " Missing image \r\n";
                    txtComments.SelectionStart = txtComments.Text.Length;
                    txtComments.ScrollToCaret();
                    txtComments.Refresh();
                }
                else
                {
                    string strToReplace;
                    strToReplace = imgNumber + "-" + " Missing image \r\n";
                    txtComments.Text = txtComments.Text.Replace(strToReplace, "");
                }
            }
        }

        private void chkPoorScan_CheckedChanged(object sender, EventArgs e)
        {
            int tifPos;
            string origDoctype = string.Empty;
            string imgNumber;
            if (lstImage.SelectedIndex >= 0)
            {
                tifPos = lstImage.SelectedItem.ToString().IndexOf("-") + 1;
                imgNumber = lstImage.SelectedItem.ToString();
                if (tifPos > 0)
                {
                    origDoctype = lstImage.SelectedItem.ToString();
                }
                else
                {
                    origDoctype = lstImage.SelectedItem.ToString();
                }
                if (chkPoorScan.Checked)
                {
                    txtComments.Text = txtComments.Text + imgNumber + "-" + " Poor scan quality \r\n";
                    txtComments.SelectionStart = txtComments.Text.Length;
                    txtComments.ScrollToCaret();
                    txtComments.Refresh();
                }
                else
                {
                    string strToReplace;
                    strToReplace = imgNumber + "-" + " Poor scan quality \r\n";
                    txtComments.Text = txtComments.Text.Replace(strToReplace, "");
                }
            }
        }

        private void chkDesicion_CheckedChanged(object sender, EventArgs e)
        {
            int tifPos;
            string origDoctype = string.Empty;
            string imgNumber;
            if (lstImage.SelectedIndex >= 0)
            {
                tifPos = lstImage.SelectedItem.ToString().IndexOf("-") + 1;
                imgNumber = lstImage.SelectedItem.ToString();
                if (tifPos > 0)
                {
                    origDoctype = lstImage.SelectedItem.ToString();
                }
                else
                { origDoctype = lstImage.SelectedItem.ToString(); }
                if (chkDesicion.Checked)
                {
                    txtComments.Text = txtComments.Text + imgNumber + "-" + " Desicion misd \r\n";
                    txtComments.SelectionStart = txtComments.Text.Length;
                    txtComments.ScrollToCaret();
                    txtComments.Refresh();
                }
                else
                {
                    string strToReplace;
                    strToReplace = imgNumber + "-" + " Desicion misd \r\n";
                    txtComments.Text = txtComments.Text.Replace(strToReplace, "");
                }
            }
        }

        private void chkExtraPage_CheckedChanged(object sender, EventArgs e)
        {
            int tifPos;
            string origDoctype = string.Empty;
            if (lstImage.SelectedIndex >= 0)
            {
                tifPos = lstImage.SelectedItem.ToString().IndexOf("-") + 1;
                string imgNumber;
                imgNumber = lstImage.SelectedItem.ToString();
                if (tifPos > 0)
                {
                    origDoctype = lstImage.SelectedItem.ToString();
                }
                else
                { origDoctype = lstImage.SelectedItem.ToString(); }
                if (chkExtraPage.Checked)
                {
                    txtComments.Text = txtComments.Text + imgNumber + "-" + " Extra page \r\n";
                    txtComments.SelectionStart = txtComments.Text.Length;
                    txtComments.ScrollToCaret();
                    txtComments.Refresh();
                }
                else
                {
                    string strToReplace;
                    strToReplace = imgNumber + "-" + " Extra page \r\n";
                    txtComments.Text = txtComments.Text.Replace(strToReplace, "");
                }
            }
        }

        private void chkMove_CheckedChanged(object sender, EventArgs e)
        {
            int tifPos;
            string origDoctype = string.Empty;
            if (lstImage.SelectedIndex >= 0)
            {
                tifPos = lstImage.SelectedItem.ToString().IndexOf("-") + 1;
                string imgNumber;
                imgNumber = lstImage.SelectedItem.ToString();
                if (tifPos > 0)
                {
                    origDoctype = lstImage.SelectedItem.ToString();
                }
                else
                { origDoctype = lstImage.SelectedItem.ToString(); }
                if (chkMove.Checked)
                {
                    txtComments.Text = txtComments.Text + imgNumber + "-" + " Move to respective file \r\n";
                    txtComments.SelectionStart = txtComments.Text.Length;
                    txtComments.ScrollToCaret();
                    txtComments.Refresh();
                }
                else
                {
                    string strToReplace;
                    strToReplace = imgNumber + "-" + " Move to respective file \r\n";
                    txtComments.Text = txtComments.Text.Replace(strToReplace, "");
                }
            }
        }

        private void chkLinkedPolicy_CheckedChanged(object sender, EventArgs e)
        {
            int tifPos;
            string origDoctype = string.Empty;

            if (lstImage.SelectedIndex >= 0)
            {
                tifPos = lstImage.SelectedItem.ToString().IndexOf("-") + 1;
                string imgNumber;
                imgNumber = lstImage.SelectedItem.ToString();
                if (tifPos > 0)
                {
                    origDoctype = lstImage.SelectedItem.ToString();
                }
                else
                { origDoctype = lstImage.SelectedItem.ToString(); }
                if (chkLinkedPolicy.Checked)
                {
                    txtComments.Text = txtComments.Text + imgNumber + "-" + " Linked file problem \r\n";
                    txtComments.SelectionStart = txtComments.Text.Length;
                    txtComments.ScrollToCaret();
                    txtComments.Refresh();
                }
                else
                {
                    string strToReplace;
                    strToReplace = imgNumber + "-" + " Linked file problem \r\n";
                    txtComments.Text = txtComments.Text.Replace(strToReplace, "");
                }
            }
        }

        private void chkCropClean_CheckedChanged(object sender, EventArgs e)
        {
            int tifPos;
            string origDoctype = string.Empty;
            string imgNumber;
            if (lstImage.SelectedIndex >= 0)
            {
                tifPos = lstImage.SelectedItem.ToString().IndexOf("-") + 1;
                imgNumber = lstImage.SelectedItem.ToString();
                if (tifPos > 0)
                {
                    origDoctype = lstImage.SelectedItem.ToString();
                }
                else
                {
                    origDoctype = lstImage.SelectedItem.ToString();
                }
                if (chkCropClean.Checked)
                {
                    txtComments.Text = txtComments.Text + imgNumber + "-" + " Crop clean problem \r\n";
                    txtComments.SelectionStart = txtComments.Text.Length;
                    txtComments.ScrollToCaret();
                    txtComments.Refresh();
                }
                else
                {
                    string strToReplace;
                    strToReplace = imgNumber + "-" + " Crop clean problem \r\n";
                    txtComments.Text = txtComments.Text.Replace(strToReplace, "");
                }
            }
        }

        private void chkRearrange_CheckedChanged(object sender, EventArgs e)
        {
            int tifPos;
            string origDoctype = string.Empty;
            if (lstImage.SelectedIndex >= 0)
            {
                tifPos = lstImage.SelectedItem.ToString().IndexOf("-") + 1;
                string imgNumber;
                imgNumber = lstImage.SelectedItem.ToString();
                if (tifPos > 0)
                {
                    origDoctype = lstImage.SelectedItem.ToString();
                }
                else
                { origDoctype = lstImage.SelectedItem.ToString(); }
                if (chkRearrange.Checked)
                {
                    txtComments.Text = txtComments.Text + imgNumber + "-" + " Rearrange error \r\n";
                    txtComments.SelectionStart = txtComments.Text.Length;
                    txtComments.ScrollToCaret();
                    txtComments.Refresh();
                }
                else
                {
                    string strToReplace;
                    strToReplace = imgNumber + "-" + " Rearrange error \r\n";
                    txtComments.Text = txtComments.Text.Replace(strToReplace, "");
                }
            }
        }

        private void chkOther_CheckedChanged(object sender, EventArgs e)
        {
            int tifPos;
            string origDoctype = string.Empty;
            string imgNumber;
            if (lstImage.SelectedIndex >= 0)
            {
                tifPos = lstImage.SelectedItem.ToString().IndexOf("-") + 1;
                imgNumber = lstImage.SelectedItem.ToString();
                if (tifPos > 0)
                {
                    origDoctype = lstImage.SelectedItem.ToString();
                }
                else
                { origDoctype = lstImage.SelectedItem.ToString(); }
                if (chkOther.Checked)
                {
                    txtComments.Text = txtComments.Text + imgNumber + "-" + " Other \r\n";
                    txtComments.SelectionStart = txtComments.Text.Length;
                    txtComments.ScrollToCaret();
                    txtComments.Refresh();
                }
                else
                {
                    string strToReplace;
                    strToReplace = imgNumber + "-" + " Other \r\n";
                    txtComments.Text = txtComments.Text.Replace(strToReplace, "");
                }
            }
        }
        public DataSet GetAllException(string policy)
        {
            string sqlStr = null;

            DataSet expDs = new DataSet();

            try
            {
                sqlStr = "select missing_img_exp,crop_clean_exp,poor_scan_exp,wrong_indexing_exp,linked_policy_exp,decision_misd_exp,extra_page_exp,rearrange_exp,other_exp,move_to_respective_policy_exp,comments from lic_qa_log where proj_key=" + cmbProject.SelectedValue.ToString() + " and batch_key=" + cmbBatch.SelectedValue.ToString() + " and policy_number='" + policy + "' ";
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(expDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                //stateLog = new MemoryStream();
                //tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                //stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex);
            }

            return expDs;
        }
        public DataSet GetAllException()
        {
            string sqlStr = null;

            DataSet expDs = new DataSet();

            try
            {
                sqlStr = "select missing_img_exp,crop_clean_exp,poor_scan_exp,wrong_indexing_exp,linked_policy_exp,decision_misd_exp,extra_page_exp,rearrange_exp,other_exp,move_to_respective_policy_exp,comments from lic_qa_log where proj_key=" + cmbProject.SelectedValue.ToString() + " and batch_key=" + cmbBatch.SelectedValue.ToString() + " and policy_number='" + policyNumber + "' ";
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(expDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                //stateLog = new MemoryStream();
                //tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                //stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex);
            }

            return expDs;
        }

        public bool QaExceptionStatus(int prmStatus, int prmExpStatus, string policy)
        {
            string sqlStr = null;
            bool commitBol = true;
            OdbcCommand sqlCmd = new OdbcCommand();
            OdbcTransaction prmTrans;

            try
            {
                prmTrans = sqlCon.BeginTransaction();
                sqlCmd.Connection = sqlCon;
                sqlCmd.Transaction = prmTrans;
                if (currStage == "1")
                {
                    sqlStr = @"update lic_qa_log" +
                    " set solved=" + prmStatus + " where proj_key=" + cmbProject.SelectedValue.ToString() +
                    " and batch_key=" + cmbBatch.SelectedValue.ToString() + " and box_number='" + 1 + "'" +
                    " and policy_number='" + policy + "' and solved <>" + 7;


                    sqlCmd.CommandText = sqlStr;
                    sqlCmd.ExecuteNonQuery();

                    sqlStr = @"update lic_qa_log" +
                    " set qa_status=" + prmExpStatus + ",created_by_phase1 = '" + crd.created_by + "',created_dttm_phase1 = '" + crd.created_dttm + "' where proj_key=" + cmbProject.SelectedValue.ToString() +
                    " and batch_key=" + cmbBatch.SelectedValue.ToString() + " and box_number='" + 1 + "'" +
                    " and policy_number='" + policy + "'";
                }
                else
                {
                    sqlStr = @"update lic_qa_log" +
                    " set solved=" + prmStatus + " where proj_key=" + cmbProject.SelectedValue.ToString() +
                    " and batch_key=" + cmbBatch.SelectedValue.ToString() + " and box_number='" + 1 + "'" +
                    " and policy_number='" + policy + "' and solved <>" + 7;


                    sqlCmd.CommandText = sqlStr;
                    sqlCmd.ExecuteNonQuery();

                    sqlStr = @"update lic_qa_log" +
                    " set qa_status=" + prmExpStatus + ",created_by_phase2 = '" + crd.created_by + "',created_dttm_phase2 = '" + crd.created_dttm + "' where proj_key=" + cmbProject.SelectedValue.ToString() +
                    " and batch_key=" + cmbBatch.SelectedValue.ToString() + " and box_number='" + 1 + "'" +
                    " and policy_number='" + policy + "'";
                }
                sqlCmd.CommandText = sqlStr;
                int i = sqlCmd.ExecuteNonQuery();

                prmTrans.Commit();
                commitBol = true;
            }
            catch (Exception ex)
            {
                commitBol = false;
                sqlCmd.Dispose();
                //stateLog = new MemoryStream();
                //tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                //stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex);
            }
            return commitBol;
        }
        public bool QaExceptionStatus(int prmStatus, int prmExpStatus)
        {
            string sqlStr = null;
            bool commitBol = true;
            OdbcCommand sqlCmd = new OdbcCommand();
            OdbcTransaction prmTrans;

            try
            {
                prmTrans = sqlCon.BeginTransaction();
                sqlCmd.Connection = sqlCon;
                sqlCmd.Transaction = prmTrans;
                if (currStage == "1")
                {
                    sqlStr = @"update lic_qa_log" +
                    " set solved=" + prmStatus + " where proj_key=" + cmbProject.SelectedValue.ToString() +
                    " and batch_key=" + cmbBatch.SelectedValue.ToString() + " and box_number='" + 1 + "'" +
                    " and policy_number='" + policyNumber + "' and solved <>" + 7;


                    sqlCmd.CommandText = sqlStr;
                    sqlCmd.ExecuteNonQuery();

                    sqlStr = @"update lic_qa_log" +
                    " set qa_status=" + prmExpStatus + ",created_by_phase1 = '" + crd.created_by + "',created_dttm_phase1 = '" + crd.created_dttm + "' where proj_key=" + cmbProject.SelectedValue.ToString() +
                    " and batch_key=" + cmbBatch.SelectedValue.ToString() + " and box_number='" + 1 + "'" +
                    " and policy_number='" + policyNumber + "'";
                }
                else
                {
                    sqlStr = @"update lic_qa_log" +
                    " set solved=" + prmStatus + " where proj_key=" + cmbProject.SelectedValue.ToString() +
                    " and batch_key=" + cmbBatch.SelectedValue.ToString() + " and box_number='" + 1 + "'" +
                    " and policy_number='" + policyNumber + "' and solved <>" + 7;


                    sqlCmd.CommandText = sqlStr;
                    sqlCmd.ExecuteNonQuery();

                    sqlStr = @"update lic_qa_log" +
                    " set qa_status=" + prmExpStatus + ",created_by_phase2 = '" + crd.created_by + "',created_dttm_phase2 = '" + crd.created_dttm + "' where proj_key=" + cmbProject.SelectedValue.ToString() +
                    " and batch_key=" + cmbBatch.SelectedValue.ToString() + " and box_number='" + 1 + "'" +
                    " and policy_number='" + policyNumber + "'";
                }
                sqlCmd.CommandText = sqlStr;
                int i = sqlCmd.ExecuteNonQuery();

                prmTrans.Commit();
                commitBol = true;
            }
            catch (Exception ex)
            {
                commitBol = false;
                sqlCmd.Dispose();
                //stateLog = new MemoryStream();
                //tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                //stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex);
            }
            return commitBol;
        }

        private void cmdAccepted_Click(object sender, EventArgs e)
        {
            string pageName;
            try
            {
                if (currStage == "1")
                {
                    if (crd.role == "Audit 1")
                    {
                        if (chkReadyUat.Checked == false)
                        {
                            if (lstImage.Items.Count > 0)
                            {
                                pPolicy = new CtrlPolicy(Convert.ToInt32(cmbProject.SelectedValue.ToString()), Convert.ToInt32(cmbBatch.SelectedValue.ToString()), boxNo, policyNumber);
                                wfePolicy wPolicy = new wfePolicy(sqlCon, pPolicy);
                                UpdateStatus(eSTATES.POLICY_CHECKED, crd);

                                //for (int i = 0; i < lstImage.Items.Count; i++)
                                //{
                                //    pageName = lstImage.Items[i].ToString().Substring(0, lstImage.Items[i].ToString().IndexOf("-"));
                                //    pImage = new CtrlImage(Convert.ToInt32(cmbProject.SelectedValue.ToString()), Convert.ToInt32(cmbBatch.SelectedValue.ToString()), Convert.ToInt32(boxNo), Convert.ToInt32(policyNumber), pageName, string.Empty);
                                //    wfeImage wImage = new wfeImage(sqlCon, pImage);
                                //    wImage.UpdateStatus(eSTATES.PAGE_CHECKED, crd);
                                //}
                                CtrlImage exppImage = new CtrlImage(Convert.ToInt32(cmbProject.SelectedValue.ToString()), Convert.ToInt32(cmbBatch.SelectedValue.ToString()), boxNo, policyNumber, string.Empty, string.Empty);
                                wfeImage expwImage = new wfeImage(sqlCon, exppImage);
                                expwImage.UpdateAllImageStatus(eSTATES.PAGE_CHECKED, crd);

                                if (GetAllException().Tables[0].Rows.Count > 0)
                                { QaExceptionStatus(ihConstants._POLICY_EXCEPTION_SOLVED, ihConstants._LIC_QA_POLICY_CHECKED); }
                                else
                                {
                                    wPolicy.InitiateQaPolicyException1(crd);
                                    QaExceptionStatus(ihConstants._POLICY_EXCEPTION_SOLVED, ihConstants._LIC_QA_POLICY_CHECKED);
                                }
                                if (licQAUsers().Rows.Count > 0)
                                {
                                    string a1 = licQAUsers().Rows[0][0].ToString();
                                    string a2 = licQAUsers().Rows[0][1].ToString();
                                    if (a1 != "" && a2 != "")
                                    {
                                        grdPolicy.Rows[policyRowIndex].DefaultCellStyle.BackColor = Color.Green;
                                    }
                                    else
                                    {
                                        grdPolicy.Rows[policyRowIndex].DefaultCellStyle.BackColor = Color.Aqua;
                                    }
                                }

                                if ((GetPolicyStatus() == (int)eSTATES.POLICY_NOT_INDEXED))
                                {
                                    grdPolicy.Rows[policyRowIndex].Cells["FILESTATUS"].Value = "Final QC";
                                }
                                if ((GetPolicyStatus() == (int)4) || (GetPolicyStatus() == (int)5) || (GetPolicyStatus() == (int)eSTATES.POLICY_FQC))
                                {
                                    grdPolicy.Rows[policyRowIndex].Cells["FILESTATUS"].Value = "Final QC";
                                }
                                if ((GetPolicyStatus() == (int)eSTATES.POLICY_ON_HOLD))
                                {
                                    grdPolicy.Rows[policyRowIndex].Cells["FILESTATUS"].Value = "On hold";
                                }
                                if (GetPolicyStatus() == (int)eSTATES.POLICY_MISSING)
                                {
                                    grdPolicy.Rows[policyRowIndex].Cells["FILESTATUS"].Value = "Missing";
                                }
                                if (GetPolicyStatus() == (int)eSTATES.POLICY_EXCEPTION)
                                {
                                    grdPolicy.Rows[policyRowIndex].Cells["FILESTATUS"].Value = "In exception";
                                }
                                if (GetPolicyStatus() == (int)eSTATES.POLICY_CHECKED)
                                {
                                    grdPolicy.Rows[policyRowIndex].Cells["FILESTATUS"].Value = "Checked";
                                }
                                tabControl1.SelectedIndex = 0;
                                //tabControl2.SelectedIndex = 0;
                                CheckBatchRejection(cmbBatch.SelectedValue.ToString());
                            }
                        }
                        else
                        {
                            MessageBox.Show("This batch is already marked as ready for UAT.....");
                        }
                    }
                    else
                    {

                        MessageBox.Show("You are not authorized to do this.....");
                    }
                }
                else
                {
                    if (crd.role == "Audit 2")
                    {
                        if (chkReadyUat.Checked == false)
                        {
                            if (lstImage.Items.Count > 0)
                            {
                                pPolicy = new CtrlPolicy(Convert.ToInt32(cmbProject.SelectedValue.ToString()), Convert.ToInt32(cmbBatch.SelectedValue.ToString()), boxNo, policyNumber);
                                wfePolicy wPolicy = new wfePolicy(sqlCon, pPolicy);
                                UpdateStatus(eSTATES.POLICY_CHECKED, crd);

                                //for (int i = 0; i < lstImage.Items.Count; i++)
                                //{
                                //    pageName = lstImage.Items[i].ToString().Substring(0, lstImage.Items[i].ToString().IndexOf("-"));
                                //    pImage = new CtrlImage(Convert.ToInt32(cmbProject.SelectedValue.ToString()), Convert.ToInt32(cmbBatch.SelectedValue.ToString()), Convert.ToInt32(boxNo), Convert.ToInt32(policyNumber), pageName, string.Empty);
                                //    wfeImage wImage = new wfeImage(sqlCon, pImage);
                                //    wImage.UpdateStatus(eSTATES.PAGE_CHECKED, crd);
                                //}
                                CtrlImage exppImage = new CtrlImage(Convert.ToInt32(cmbProject.SelectedValue.ToString()), Convert.ToInt32(cmbBatch.SelectedValue.ToString()), boxNo, policyNumber, string.Empty, string.Empty);
                                wfeImage expwImage = new wfeImage(sqlCon, exppImage);
                                expwImage.UpdateAllImageStatus(eSTATES.PAGE_CHECKED, crd);

                                if (GetAllException().Tables[0].Rows.Count > 0)
                                { QaExceptionStatus(ihConstants._POLICY_EXCEPTION_SOLVED, ihConstants._LIC_QA_POLICY_CHECKED); }
                                else
                                {
                                    wPolicy.InitiateQaPolicyException2(crd);
                                    QaExceptionStatus(ihConstants._POLICY_EXCEPTION_SOLVED, ihConstants._LIC_QA_POLICY_CHECKED);
                                }
                                if (licQAUsers().Rows.Count > 0)
                                {
                                    string a1 = licQAUsers().Rows[0][0].ToString();
                                    string a2 = licQAUsers().Rows[0][1].ToString();
                                    if (a1 != "" && a2 != "")
                                    {
                                        grdPolicy.Rows[policyRowIndex].DefaultCellStyle.BackColor = Color.Green;
                                    }
                                    else
                                    {
                                        grdPolicy.Rows[policyRowIndex].DefaultCellStyle.BackColor = Color.Aqua;
                                    }
                                }
                                if ((GetPolicyStatus() == (int)eSTATES.POLICY_NOT_INDEXED))
                                {
                                    grdPolicy.Rows[policyRowIndex].Cells["FILESTATUS"].Value = "Not Indexed";
                                }
                                if ((GetPolicyStatus() == (int)4) || (GetPolicyStatus() == (int)eSTATES.POLICY_FQC))
                                {
                                    grdPolicy.Rows[policyRowIndex].Cells["FILESTATUS"].Value = "Indexed";
                                }
                                if ((GetPolicyStatus() == (int)eSTATES.POLICY_ON_HOLD))
                                {
                                    grdPolicy.Rows[policyRowIndex].Cells["FILESTATUS"].Value = "On hold";
                                }
                                if (GetPolicyStatus() == (int)eSTATES.POLICY_MISSING)
                                {
                                    grdPolicy.Rows[policyRowIndex].Cells["FILESTATUS"].Value = "Missing";
                                }
                                if (GetPolicyStatus() == (int)eSTATES.POLICY_EXCEPTION)
                                {
                                    grdPolicy.Rows[policyRowIndex].Cells["FILESTATUS"].Value = "In exception";
                                }
                                if (GetPolicyStatus() == (int)eSTATES.POLICY_CHECKED)
                                {
                                    grdPolicy.Rows[policyRowIndex].Cells["FILESTATUS"].Value = "Checked";
                                }
                                tabControl1.SelectedIndex = 0;
                                //tabControl2.SelectedIndex = 0;
                                CheckBatchRejection(cmbBatch.SelectedValue.ToString());
                            }
                        }
                        else
                        {
                            MessageBox.Show("This batch is already marked as ready for UAT.....");
                        }
                    }
                    else
                    {

                        MessageBox.Show("You are not authorized to do this.....");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public bool UpdateStatus(eSTATES state, Credentials prmCrd)
        {
            string sqlStr = null;
            OdbcTransaction sqlTrans = null;
            bool commitBol = true;
            OdbcCommand sqlCmd = new OdbcCommand();

            sqlStr = @"update metadata_entry" +
                " set status=" + (int)state + ",modified_by='" + prmCrd.created_by + "',modified_dttm='" + prmCrd.created_dttm + "' where proj_code=" + projCode +
                " and batch_key=" + batchCode + " " +
                " and filename='" + policyNumber + "' and status <> '9' and status<>" + (int)eSTATES.POLICY_EXPORTED;

            try
            {

                sqlTrans = sqlCon.BeginTransaction();
                sqlCmd.Connection = sqlCon;
                sqlCmd.Transaction = sqlTrans;
                sqlCmd.CommandText = sqlStr;
                int i = sqlCmd.ExecuteNonQuery();
                sqlTrans.Commit();
                if (i > 0)
                {
                    commitBol = true;
                }
                else
                {
                    commitBol = false;
                }
            }
            catch (Exception ex)
            {
                commitBol = false;
                sqlTrans.Rollback();
                sqlCmd.Dispose();

                exMailLog.Log(ex);
            }
            return commitBol;
        }
        public int GetPolicyStatus()
        {
            string sqlStr = null;
            DataSet dsImage = new DataSet();
            OdbcDataAdapter sqlAdap = null;

            sqlStr = "select status from metadata_entry " +
                    " where proj_code=" + projCode +
                " and batch_key=" + batchCode + " and filename='" + policyNumber + "'";

            try
            {
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(dsImage);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();

                exMailLog.Log(ex);
            }
            return Convert.ToInt32(dsImage.Tables[0].Rows[0]["status"]);
        }
        public DataTable batchPhases()
        {
            string sqlStr = null;
            DataTable dsImage = new DataTable();
            OdbcDataAdapter sqlAdap = null;

            sqlStr = "select phase1,phase2 from metadata_entry " +
                    " where proj_code=" + projCode +
                " and batch_key=" + batchCode + " ";

            try
            {
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(dsImage);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();

                exMailLog.Log(ex);
            }
            return dsImage;
        }
        public DataTable licQAUsers(string file)
        {
            string sqlStr = null;
            DataTable dsImage = new DataTable();
            OdbcDataAdapter sqlAdap = null;

            sqlStr = "select created_by_phase1,created_by_phase2 from lic_qa_log " +
                    " where proj_key=" + projCode +
                " and batch_key=" + batchCode + " and policy_number='" + file + "' ";

            try
            {
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(dsImage);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();

                exMailLog.Log(ex);
            }
            return dsImage;
        }
        public DataTable licQAUsers()
        {
            string sqlStr = null;
            DataTable dsImage = new DataTable();
            OdbcDataAdapter sqlAdap = null;

            sqlStr = "select created_by_phase1,created_by_phase2 from lic_qa_log " +
                    " where proj_key=" + projCode +
                " and batch_key=" + batchCode + " and policy_number='" + policyNumber + "' ";

            try
            {
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(dsImage);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();

                exMailLog.Log(ex);
            }
            return dsImage;
        }
        public DataTable getAllFiles()
        {
            string sqlStr = null;
            DataTable dsImage = new DataTable();
            OdbcDataAdapter sqlAdap = null;

            sqlStr = "select filename from metadata_entry " +
                    " where proj_code=" + projCode +
                " and batch_key=" + batchCode + " ";

            try
            {
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(dsImage);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();

                exMailLog.Log(ex);
            }
            return dsImage;
        }
        private void cmdRejected_Click(object sender, EventArgs e)
        {
            bool expBol = false;
            policyException udtExp = new policyException();
            NovaNet.Utils.dbCon dbcon = new NovaNet.Utils.dbCon();
            string pageName = null;

            if (currStage == "1")
            {
                if (crd.role == "Audit 1")
                {
                    if (chkReadyUat.Checked == false)
                    {
                        if (lstImage.Items.Count > 0)
                        {
                            pPolicy = new CtrlPolicy(Convert.ToInt32(cmbProject.SelectedValue.ToString()), Convert.ToInt32(cmbBatch.SelectedValue.ToString()), boxNo, policyNumber);
                            wfePolicy policy = new wfePolicy(sqlCon, pPolicy);
                            if (chkCropClean.Checked == true)
                            {
                                udtExp.crop_clean_exp = 1;
                                expBol = true;
                            }
                            else
                            {
                                udtExp.crop_clean_exp = 0;
                            }

                            if (chkDesicion.Checked == true)
                            {
                                udtExp.decision_misd_exp = 1;
                                expBol = true;
                            }
                            else
                            {
                                udtExp.decision_misd_exp = 0;
                            }

                            if (chkExtraPage.Checked == true)
                            {
                                udtExp.extra_page_exp = 1;
                                expBol = true;
                            }
                            else
                            {
                                udtExp.extra_page_exp = 0;
                            }

                            if (chkLinkedPolicy.Checked == true)
                            {
                                udtExp.linked_policy_exp = 1;
                                expBol = true;
                            }
                            else
                            {
                                udtExp.linked_policy_exp = 0;
                            }

                            if (chkMissingImg.Checked == true)
                            {
                                udtExp.missing_img_exp = 1;
                                expBol = true;
                            }
                            else
                            {
                                udtExp.missing_img_exp = 0;
                            }
                            if (chkMove.Checked == true)
                            {
                                udtExp.move_to_respective_policy_exp = 1;
                                expBol = true;
                            }
                            else
                            {
                                udtExp.move_to_respective_policy_exp = 0;
                            }
                            if (chkOther.Checked == true)
                            {
                                udtExp.other_exp = 1;
                                expBol = true;
                            }
                            else
                            {
                                udtExp.other_exp = 0;
                            }

                            if (chkPoorScan.Checked == true)
                            {
                                udtExp.poor_scan_exp = 1;
                                expBol = true;
                            }
                            else
                            {
                                udtExp.poor_scan_exp = 0;
                            }
                            if (chkRearrange.Checked == true)
                            {
                                udtExp.rearrange_exp = 1;
                                expBol = true;
                            }
                            else
                            {
                                udtExp.rearrange_exp = 0;
                            }
                            //if (chkIndexing.Checked == true)
                            //{
                            //    udtExp.wrong_indexing_exp = 1;
                            //    expBol = true;
                            //}
                            //else
                            //{
                            udtExp.wrong_indexing_exp = 0;
                            //}
                            udtExp.comments = txtComments.Text;
                            //udtExp.status = ihConstants._LIC_QA_POLICY_EXCEPTION;
                            if (expBol == true)
                            {
                                udtExp.solved = ihConstants._POLICY_EXCEPTION_NOT_SOLVED;
                                //if(policy.InitiateQaPolicyException(crd))
                                if (GetAllException().Tables[0].Rows.Count > 0)
                                {
                                    if (policy.UpdateQaPolicyException1(crd, udtExp) == true)
                                    {
                                        if (policy.QaExceptionStatus(ihConstants._POLICY_EXCEPTION_NOT_SOLVED, ihConstants._LIC_QA_POLICY_EXCEPTION) == true)
                                        {
                                            UpdateStatus(eSTATES.POLICY_EXCEPTION, crd);

                                            CtrlImage exppImage = new CtrlImage(Convert.ToInt32(cmbProject.SelectedValue.ToString()), Convert.ToInt32(cmbBatch.SelectedValue.ToString()), boxNo, policyNumber, string.Empty, string.Empty);
                                            wfeImage expwImage = new wfeImage(sqlCon, exppImage);
                                            expwImage.UpdateAllImageStatus(eSTATES.PAGE_EXCEPTION, crd);
                                            grdPolicy.Rows[policyRowIndex].DefaultCellStyle.BackColor = Color.Red;
                                            if ((GetPolicyStatus() == (int)eSTATES.POLICY_NOT_INDEXED))
                                            {
                                                grdPolicy.Rows[policyRowIndex].Cells["FILESTATUS"].Value = "Final QC";
                                            }
                                            if ((GetPolicyStatus() == (int)4) || (GetPolicyStatus() == (int)5) || (GetPolicyStatus() == (int)eSTATES.POLICY_FQC))
                                            {
                                                grdPolicy.Rows[policyRowIndex].Cells["FILESTATUS"].Value = "Final QC";
                                            }
                                            if ((GetPolicyStatus() == (int)eSTATES.POLICY_ON_HOLD))
                                            {
                                                grdPolicy.Rows[policyRowIndex].Cells["FILESTATUS"].Value = "On hold";
                                            }
                                            if (GetPolicyStatus() == (int)eSTATES.POLICY_MISSING)
                                            {
                                                grdPolicy.Rows[policyRowIndex].Cells["FILESTATUS"].Value = "Missing";
                                            }
                                            if (GetPolicyStatus() == (int)eSTATES.POLICY_EXCEPTION)
                                            {
                                                grdPolicy.Rows[policyRowIndex].Cells["FILESTATUS"].Value = "In exception";
                                            }
                                            if (GetPolicyStatus() == (int)eSTATES.POLICY_CHECKED)
                                            {
                                                grdPolicy.Rows[policyRowIndex].Cells["FILESTATUS"].Value = "Checked";
                                            }
                                            //box.UpdateStatus(eSTATES.BOX_CONFLICT);
                                        }
                                    }
                                    tabControl1.SelectedIndex = 0;
                                    //tabControl2.SelectedIndex = 0;
                                    CheckBatchRejection(cmbBatch.SelectedValue.ToString());
                                }
                                else
                                {
                                    policy.InitiateQaPolicyException1(crd);
                                    if (policy.UpdateQaPolicyException1(crd, udtExp) == true)
                                    {
                                        if (QaExceptionStatus(ihConstants._POLICY_EXCEPTION_NOT_SOLVED, ihConstants._LIC_QA_POLICY_EXCEPTION) == true)
                                        {
                                            UpdateStatus(eSTATES.POLICY_EXCEPTION, crd);

                                            CtrlImage exppImage = new CtrlImage(Convert.ToInt32(cmbProject.SelectedValue.ToString()), Convert.ToInt32(cmbBatch.SelectedValue.ToString()), boxNo, policyNumber, string.Empty, string.Empty);
                                            wfeImage expwImage = new wfeImage(sqlCon, exppImage);
                                            expwImage.UpdateAllImageStatus(eSTATES.PAGE_EXCEPTION, crd);
                                            grdPolicy.Rows[policyRowIndex].DefaultCellStyle.BackColor = Color.Red;
                                            if ((GetPolicyStatus() == (int)eSTATES.POLICY_NOT_INDEXED))
                                            {
                                                grdPolicy.Rows[policyRowIndex].Cells["FILESTATUS"].Value = "Final QC";
                                            }
                                            if ((GetPolicyStatus() == (int)4) || (GetPolicyStatus() == (int)5) || (GetPolicyStatus() == (int)eSTATES.POLICY_FQC))
                                            {
                                                grdPolicy.Rows[policyRowIndex].Cells["FILESTATUS"].Value = "Final QC";
                                            }
                                            if ((GetPolicyStatus() == (int)eSTATES.POLICY_ON_HOLD))
                                            {
                                                grdPolicy.Rows[policyRowIndex].Cells["FILESTATUS"].Value = "On hold";
                                            }
                                            if (GetPolicyStatus() == (int)eSTATES.POLICY_MISSING)
                                            {
                                                grdPolicy.Rows[policyRowIndex].Cells["FILESTATUS"].Value = "Missing";
                                            }
                                            if (GetPolicyStatus() == (int)eSTATES.POLICY_EXCEPTION)
                                            {
                                                grdPolicy.Rows[policyRowIndex].Cells["FILESTATUS"].Value = "In exception";
                                            }
                                            if (GetPolicyStatus() == (int)eSTATES.POLICY_CHECKED)
                                            {
                                                grdPolicy.Rows[policyRowIndex].Cells["FILESTATUS"].Value = "Checked";
                                            }
                                            //box.UpdateStatus(eSTATES.BOX_CONFLICT);
                                        }
                                    }
                                    tabControl1.SelectedIndex = 0;
                                    //tabControl2.SelectedIndex = 0;
                                    CheckBatchRejection(cmbBatch.SelectedValue.ToString());
                                }

                            }
                            else
                            {
                                MessageBox.Show("Provide atleast one exception type", "B'Zer", MessageBoxButtons.OK);
                            }

                        }
                    }
                    else
                    {
                        MessageBox.Show("This bundle is already marked as ready for UAT.....");
                    }
                }
                else
                {
                    MessageBox.Show("You are not authorized to do this.....");
                }
            }
            else
            {
                if (crd.role == "Audit 2")
                {
                    if (chkReadyUat.Checked == false)
                    {
                        if (lstImage.Items.Count > 0)
                        {
                            pPolicy = new CtrlPolicy(Convert.ToInt32(cmbProject.SelectedValue.ToString()), Convert.ToInt32(cmbBatch.SelectedValue.ToString()), boxNo, policyNumber);
                            wfePolicy policy = new wfePolicy(sqlCon, pPolicy);
                            if (chkCropClean.Checked == true)
                            {
                                udtExp.crop_clean_exp = 1;
                                expBol = true;
                            }
                            else
                            {
                                udtExp.crop_clean_exp = 0;
                            }

                            if (chkDesicion.Checked == true)
                            {
                                udtExp.decision_misd_exp = 1;
                                expBol = true;
                            }
                            else
                            {
                                udtExp.decision_misd_exp = 0;
                            }

                            if (chkExtraPage.Checked == true)
                            {
                                udtExp.extra_page_exp = 1;
                                expBol = true;
                            }
                            else
                            {
                                udtExp.extra_page_exp = 0;
                            }

                            if (chkLinkedPolicy.Checked == true)
                            {
                                udtExp.linked_policy_exp = 1;
                                expBol = true;
                            }
                            else
                            {
                                udtExp.linked_policy_exp = 0;
                            }

                            if (chkMissingImg.Checked == true)
                            {
                                udtExp.missing_img_exp = 1;
                                expBol = true;
                            }
                            else
                            {
                                udtExp.missing_img_exp = 0;
                            }
                            if (chkMove.Checked == true)
                            {
                                udtExp.move_to_respective_policy_exp = 1;
                                expBol = true;
                            }
                            else
                            {
                                udtExp.move_to_respective_policy_exp = 0;
                            }
                            if (chkOther.Checked == true)
                            {
                                udtExp.other_exp = 1;
                                expBol = true;
                            }
                            else
                            {
                                udtExp.other_exp = 0;
                            }

                            if (chkPoorScan.Checked == true)
                            {
                                udtExp.poor_scan_exp = 1;
                                expBol = true;
                            }
                            else
                            {
                                udtExp.poor_scan_exp = 0;
                            }
                            if (chkRearrange.Checked == true)
                            {
                                udtExp.rearrange_exp = 1;
                                expBol = true;
                            }
                            else
                            {
                                udtExp.rearrange_exp = 0;
                            }
                            //if (chkIndexing.Checked == true)
                            //{
                            //    udtExp.wrong_indexing_exp = 1;
                            //    expBol = true;
                            //}
                            //else
                            //{
                            udtExp.wrong_indexing_exp = 0;
                            //}
                            udtExp.comments = txtComments.Text;
                            //udtExp.status = ihConstants._LIC_QA_POLICY_EXCEPTION;
                            if (expBol == true)
                            {
                                udtExp.solved = ihConstants._POLICY_EXCEPTION_NOT_SOLVED;
                                //if(policy.InitiateQaPolicyException(crd))
                                if (GetAllException().Tables[0].Rows.Count > 0)
                                {
                                    if (policy.UpdateQaPolicyException2(crd, udtExp) == true)
                                    {
                                        if (QaExceptionStatus(ihConstants._POLICY_EXCEPTION_NOT_SOLVED, ihConstants._LIC_QA_POLICY_EXCEPTION) == true)
                                        {
                                            UpdateStatus(eSTATES.POLICY_EXCEPTION, crd);

                                            CtrlImage exppImage = new CtrlImage(Convert.ToInt32(cmbProject.SelectedValue.ToString()), Convert.ToInt32(cmbBatch.SelectedValue.ToString()), boxNo, policyNumber, string.Empty, string.Empty);
                                            wfeImage expwImage = new wfeImage(sqlCon, exppImage);
                                            expwImage.UpdateAllImageStatus(eSTATES.PAGE_EXCEPTION, crd);
                                            grdPolicy.Rows[policyRowIndex].DefaultCellStyle.BackColor = Color.Red;
                                            if ((GetPolicyStatus() == (int)eSTATES.POLICY_NOT_INDEXED))
                                            {
                                                grdPolicy.Rows[policyRowIndex].Cells["FILESTATUS"].Value = "Final QC";
                                            }
                                            if ((GetPolicyStatus() == (int)4) || (GetPolicyStatus() == (int)5) || (GetPolicyStatus() == (int)eSTATES.POLICY_FQC))
                                            {
                                                grdPolicy.Rows[policyRowIndex].Cells["FILESTATUS"].Value = "Final QC";
                                            }
                                            if ((GetPolicyStatus() == (int)eSTATES.POLICY_ON_HOLD))
                                            {
                                                grdPolicy.Rows[policyRowIndex].Cells["FILESTATUS"].Value = "On hold";
                                            }
                                            if (GetPolicyStatus() == (int)eSTATES.POLICY_MISSING)
                                            {
                                                grdPolicy.Rows[policyRowIndex].Cells["FILESTATUS"].Value = "Missing";
                                            }
                                            if (GetPolicyStatus() == (int)eSTATES.POLICY_EXCEPTION)
                                            {
                                                grdPolicy.Rows[policyRowIndex].Cells["FILESTATUS"].Value = "In exception";
                                            }
                                            if (GetPolicyStatus() == (int)eSTATES.POLICY_CHECKED)
                                            {
                                                grdPolicy.Rows[policyRowIndex].Cells["FILESTATUS"].Value = "Checked";
                                            }
                                            //box.UpdateStatus(eSTATES.BOX_CONFLICT);
                                        }
                                    }
                                    tabControl1.SelectedIndex = 0;
                                    //tabControl2.SelectedIndex = 0;
                                    CheckBatchRejection(cmbBatch.SelectedValue.ToString());
                                }
                                else
                                {
                                    policy.InitiateQaPolicyException2(crd);
                                    if (policy.UpdateQaPolicyException2(crd, udtExp) == true)
                                    {
                                        if (policy.QaExceptionStatus(ihConstants._POLICY_EXCEPTION_NOT_SOLVED, ihConstants._LIC_QA_POLICY_EXCEPTION) == true)
                                        {
                                            UpdateStatus(eSTATES.POLICY_EXCEPTION, crd);

                                            CtrlImage exppImage = new CtrlImage(Convert.ToInt32(cmbProject.SelectedValue.ToString()), Convert.ToInt32(cmbBatch.SelectedValue.ToString()), boxNo, policyNumber, string.Empty, string.Empty);
                                            wfeImage expwImage = new wfeImage(sqlCon, exppImage);
                                            expwImage.UpdateAllImageStatus(eSTATES.PAGE_EXCEPTION, crd);
                                            grdPolicy.Rows[policyRowIndex].DefaultCellStyle.BackColor = Color.Red;
                                            if ((GetPolicyStatus() == (int)eSTATES.POLICY_NOT_INDEXED))
                                            {
                                                grdPolicy.Rows[policyRowIndex].Cells["FILESTATUS"].Value = "Final QC";
                                            }
                                            if ((GetPolicyStatus() == (int)4) || (GetPolicyStatus() == (int)5) || (GetPolicyStatus() == (int)eSTATES.POLICY_FQC))
                                            {
                                                grdPolicy.Rows[policyRowIndex].Cells["FILESTATUS"].Value = "Final QC";
                                            }
                                            if ((GetPolicyStatus() == (int)eSTATES.POLICY_ON_HOLD))
                                            {
                                                grdPolicy.Rows[policyRowIndex].Cells["FILESTATUS"].Value = "On hold";
                                            }
                                            if (GetPolicyStatus() == (int)eSTATES.POLICY_MISSING)
                                            {
                                                grdPolicy.Rows[policyRowIndex].Cells["FILESTATUS"].Value = "Missing";
                                            }
                                            if (GetPolicyStatus() == (int)eSTATES.POLICY_EXCEPTION)
                                            {
                                                grdPolicy.Rows[policyRowIndex].Cells["FILESTATUS"].Value = "In exception";
                                            }
                                            if (GetPolicyStatus() == (int)eSTATES.POLICY_CHECKED)
                                            {
                                                grdPolicy.Rows[policyRowIndex].Cells["FILESTATUS"].Value = "Checked";
                                            }
                                            //box.UpdateStatus(eSTATES.BOX_CONFLICT);
                                        }
                                    }
                                    tabControl1.SelectedIndex = 0;
                                    //tabControl2.SelectedIndex = 0;
                                    CheckBatchRejection(cmbBatch.SelectedValue.ToString());
                                }

                            }
                            else
                            {
                                MessageBox.Show("Provide atleast one exception type", "B'Zer", MessageBoxButtons.OK);
                            }

                        }
                    }
                    else
                    {
                        MessageBox.Show("This bundle is already marked as ready for UAT.....");
                    }
                }
                else
                {
                    MessageBox.Show("You are not authorized to do this.....");
                }
            }
        }

        public bool PolicyWithLICException(int prmProjKey, int prmBatchKey)
        {
            string sqlStr = null;
            DataSet projDs = new DataSet();

            try
            {
                sqlStr = @"select filename from metadata_entry where proj_code=" + prmProjKey + " and batch_key=" + prmBatchKey + " and status=" + (int)eSTATES.POLICY_EXCEPTION;
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(projDs);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();

                exMailLog.Log(ex);
            }
            if (projDs.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
                return false;
        }

        private void chkReadyUat_Click(object sender, EventArgs e)
        {
            DialogResult dlg;
            wfeBatch wBatch = new wfeBatch(sqlCon);
            ///changed in version 1.0.2
            ///
            if (currStage == "1")
            {
                if (crd.role == "Audit 1")
                {
                    if ((cmbProject.SelectedValue != null) && (cmbBatch.SelectedValue != null))
                    {

                        if ((grdBox.Rows.Count > 0) && (grdPolicy.Rows.Count > 0))
                        {
                            if (PolicyWithLICException(Convert.ToInt32(cmbProject.SelectedValue.ToString()), Convert.ToInt32(cmbBatch.SelectedValue.ToString())) == false)
                            {
                                if (chkReadyUat.Checked == true)
                                {
                                    dlg = MessageBox.Show(this, "Are you sure, this batch is ready for UAT?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (dlg == DialogResult.Yes)
                                    {
                                        int totNO = getAllFiles().Rows.Count;

                                        for (int i = 0; i < totNO; i++)
                                        {
                                            string policy = getAllFiles().Rows[i][0].ToString();
                                            if (GetAllException(policy).Tables[0].Rows.Count > 0)
                                            { QaExceptionStatus(ihConstants._POLICY_EXCEPTION_SOLVED, ihConstants._LIC_QA_POLICY_CHECKED, policy); }
                                            else
                                            {
                                                wPolicy.InitiateQaPolicyException1(crd, policy);
                                                QaExceptionStatus(ihConstants._POLICY_EXCEPTION_SOLVED, ihConstants._LIC_QA_POLICY_CHECKED, policy);
                                            }
                                        }

                                        UpdateStatus(eSTATES.BATCH_READY_FOR_UAT, Convert.ToInt32(cmbProject.SelectedValue.ToString()), Convert.ToInt32(cmbBatch.SelectedValue.ToString()));
                                        chkReadyUat.Checked = true;
                                        chkReadyUat.Enabled = false;
                                        PopulateBatchCombo();
                                    }
                                    else
                                    {
                                        chkReadyUat.Checked = false;
                                        chkReadyUat.Enabled = true;
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("One or more files is in exception stage, clear the exceptions before proceeding....");
                                chkReadyUat.Checked = false;
                                chkReadyUat.Enabled = true;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Populate file details.....");
                        }


                    }
                }
                else
                {
                    MessageBox.Show("You are not authorized to do this.....");
                    chkReadyUat.Checked = false;
                }
            }
            else
            {
                if (crd.role == "Audit 2")
                {
                    if ((cmbProject.SelectedValue != null) && (cmbBatch.SelectedValue != null))
                    {

                        if ((grdBox.Rows.Count > 0) && (grdPolicy.Rows.Count > 0))
                        {
                            if (PolicyWithLICException(Convert.ToInt32(cmbProject.SelectedValue.ToString()), Convert.ToInt32(cmbBatch.SelectedValue.ToString())) == false)
                            {
                                if (chkReadyUat.Checked == true)
                                {
                                    dlg = MessageBox.Show(this, "Are you sure, this bundle is ready for UAT?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (dlg == DialogResult.Yes)
                                    {
                                        int totNO = getAllFiles().Rows.Count;

                                        for (int i = 0; i < totNO; i++)
                                        {
                                            string policy = getAllFiles().Rows[i][0].ToString();
                                            if (GetAllException(policy).Tables[0].Rows.Count > 0)
                                            { QaExceptionStatus(ihConstants._POLICY_EXCEPTION_SOLVED, ihConstants._LIC_QA_POLICY_CHECKED, policy); }
                                            else
                                            {
                                                wPolicy.InitiateQaPolicyException2(crd, policy);
                                                QaExceptionStatus(ihConstants._POLICY_EXCEPTION_SOLVED, ihConstants._LIC_QA_POLICY_CHECKED, policy);
                                            }
                                        }


                                        UpdateStatus(eSTATES.BATCH_READY_FOR_UAT, Convert.ToInt32(cmbProject.SelectedValue.ToString()), Convert.ToInt32(cmbBatch.SelectedValue.ToString()));
                                        chkReadyUat.Checked = true;
                                        chkReadyUat.Enabled = false;
                                        PopulateBatchCombo();
                                    }
                                    else
                                    {
                                        chkReadyUat.Checked = false;
                                        chkReadyUat.Enabled = true;
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("One or more files is in exception stage, clear the exceptions before proceeding....");
                                chkReadyUat.Checked = false;
                                chkReadyUat.Enabled = true;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Populate file details.....");
                        }


                    }
                }
                else
                {
                    MessageBox.Show("You are not authorized to do this.....");
                    chkReadyUat.Checked = false;
                }
            }

        }


        public bool UpdateStatus(eSTATES state, int prmProjKey, int prmBatchKey)
        {
            string sqlStr = null;
            OdbcTransaction sqlTrans = null;
            bool commitBol = true;

            OdbcCommand sqlCmd = new OdbcCommand();

            if (currStage == "1")
            {
                sqlStr = @"update batch_master" +
                " set status=" + (int)state + " , phase1 = 'T' where " +
                " proj_code = '" + prmProjKey + "' and batch_key=" + prmBatchKey + " and status<>" + (int)9;

            }
            else
            {
                sqlStr = @"update batch_master" +
                " set status=" + (int)8 + " , phase2 = 'T' where " +
                " proj_code = '" + prmProjKey + "' and batch_key=" + prmBatchKey + " and status<>" + (int)9;

            }

            try
            {

                sqlTrans = sqlCon.BeginTransaction();
                sqlCmd.Connection = sqlCon;
                sqlCmd.Transaction = sqlTrans;
                sqlCmd.CommandText = sqlStr;
                int i = sqlCmd.ExecuteNonQuery();
                sqlTrans.Commit();
                commitBol = true;
            }
            catch (Exception ex)
            {
                commitBol = false;
                sqlTrans.Rollback();
                sqlCmd.Dispose();

                exMailLog.Log(ex);
            }
            return commitBol;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            selBoxNo = Convert.ToInt32("1");
            if (Convert.ToInt32(textBox1.Text.Trim()) <= 100)
            {
                PolicyDetails("1");
            }
            else
            {
                MessageBox.Show("Cannot search files for this batch over 100 percent");
            }
            grdPolicy.ForeColor = Color.Black;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void chkReadyUat_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
