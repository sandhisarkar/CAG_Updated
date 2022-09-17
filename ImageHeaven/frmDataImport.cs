using System;
using System.Drawing;
using System.Windows.Forms;
using NovaNet.Utils;
using NovaNet.wfe;
using LItems;
using System.Data;
using System.Data.Odbc;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Drawing.Imaging;

namespace ImageHeaven
{
    public partial class frmDataImport : Form
    {
        MemoryStream stateLog;
        byte[] tmpWrite;
        NovaNet.Utils.dbCon dbcon;
        int pos = 0;
        int posAdd = 0;
        int resize = 10;
        OdbcConnection sqlCon = null;
        eSTATES[] state;
        wfeProject tmpProj = null;
        DataSet ds = null;
        private double ZOOMFACTOR = 1.10;   // = 25% smaller or larger
        private int MINMAX = 5;
        Point mouseDown = new Point();
        private Size ImageSize = new Size(100, 200);
        Credentials crd = new Credentials();
        //OdbcTransaction sqlTrans = null;
        private Dictionary<string, ListViewItem> ListViewItems = new Dictionary<string, ListViewItem>();
        private Dictionary<string, ListViewItem> ListViewItems1 = new Dictionary<string, ListViewItem>();

        public static string category;
        public static string dept;

        public frmDataImport()
        {
            InitializeComponent();
        }

        public frmDataImport(OdbcConnection prmCon, Credentials prmCrd)
        {
            dbcon = new NovaNet.Utils.dbCon();
            sqlCon = prmCon;
            crd = prmCrd;
            InitializeComponent();
            tabControl1.TabPages.Remove(tabPage3);

        }
        private void KeyEvent(object sender, KeyEventArgs e) //Keyup Event 
        {
            if (tabControl1.SelectedIndex == 0 && lstPolicy.SelectedItems.Count > 0 && e.KeyCode == Keys.Add)
            {
                cmdAdd_Click(this, e);
            }
            if (tabControl1.SelectedIndex == 0 && (e.KeyCode == Keys.PageUp || e.KeyCode == Keys.PageDown))
            {
                tabControl1.SelectedIndex = 0;
            }
            if (tabControl1.SelectedIndex == 1 && (e.KeyCode == Keys.PageUp || e.KeyCode == Keys.PageDown))
            {
                tabControl1.SelectedIndex = 0;
            }
            if (tabControl1.SelectedIndex == 0 && lstSelImg.Items.Count > 0 && lstSelImg.SelectedItems.Count > 0 && e.KeyCode == Keys.Subtract)
            {
                cmdRemove_Click(this, e);
            }
            if (tabControl1.SelectedIndex == 0 && e.KeyCode == Keys.F5)
            {
                cmdImport_Click(this, e);
            }
            if (tabControl1.SelectedIndex == 1 && lstCheckDeed.SelectedItems.Count > 0 && e.KeyCode == Keys.Add)
            {
                cmdadd1_Click(this, e);
            }
            if (tabControl1.SelectedIndex == 1 && lstSelectedImg.Items.Count > 0 && lstSelectedImg.SelectedItems.Count > 0 && e.KeyCode == Keys.Subtract)
            {
                cmdremove1_Click(this, e);
            }
            if (tabControl1.SelectedIndex == 1 && lstCheckDeed.SelectedItems.Count > 0 && e.KeyCode == Keys.F5)
            {
                CmdFinalSave_Click(this, e);
            }
        }
        private void frmDataImport_Load(object sender, EventArgs e)
        {
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(KeyEvent);
            PopulateProjectCombo();
            cmdBrowse.Enabled = false;
        }
        private void PopulateProjectCombo()
        {
            DataSet ds = new DataSet();


            DataTable dt = new DataTable();

            string sql = "select proj_key,proj_code from project_master order by proj_code";

            OdbcDataAdapter odap = new OdbcDataAdapter(sql, sqlCon);
            odap.Fill(dt);


            if (dt.Rows.Count > 0)
            {
                cmbProject.DataSource = dt;
                cmbProject.DisplayMember = "proj_code";
                cmbProject.ValueMember = "proj_key";

            }
            else
            {
                cmbProject.Text = "";
                MessageBox.Show("Add one project first...");

            }
        }

        private void cmbProject_Leave(object sender, EventArgs e)
        {
            PopulateBatchCombo();
        }

        private void PopulateBatchCombo()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            string sql = "select DISTINCT a.batch_key, a.batch_code from batch_master a, project_master b,metadata_entry c where a.proj_code = c.proj_code and a.batch_key =c.batch_key and a.proj_code = b.proj_key and a.status = '1' and a.proj_code = '" + cmbProject.SelectedValue.ToString() + "' order by a.batch_key";

            OdbcDataAdapter odap = new OdbcDataAdapter(sql, sqlCon);
            odap.Fill(dt);


            if (dt.Rows.Count > 0)
            {
                cmbBatch.DataSource = dt;
                cmbBatch.DisplayMember = "batch_code";
                cmbBatch.ValueMember = "batch_key";
                cmdBrowse.Enabled = true;
                txtPath.Enabled = true;
            }
            else
            {
                //cmbBatch.Text = "";
                //MessageBox.Show("No Batch found for this project...","Add Batch");
                cmbBatch.Text = string.Empty;
                cmbBatch.DataSource = null;
                cmbBatch.DisplayMember = "";
                cmbBatch.ValueMember = "";
                cmbProject.Select();
                cmdBrowse.Enabled = false;
                txtPath.Enabled = false;
            }
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
            PopulateBox();
        }
        private void PopulateBox()
        {
            string batchKey = null;
            DataSet ds = new DataSet();


            dbcon = new NovaNet.Utils.dbCon();

            int policyCount;

            if (cmbBatch.SelectedValue != null)
            {
                batchKey = cmbBatch.SelectedValue.ToString();
                //CtrlPolicy pPolicy = new CtrlPolicy(Convert.ToInt32(cmbProject.SelectedValue.ToString()), Convert.ToInt32(cmbBatch.SelectedValue.ToString()), 1, "0");
                CtrlPolicy pPolicy = new CtrlPolicy(Convert.ToInt32(cmbProject.SelectedValue.ToString()), Convert.ToInt32(cmbBatch.SelectedValue.ToString()), "1", "0");
                wfePolicy wPolicy = new wfePolicy(sqlCon, pPolicy);

                //eSTATES[] state = new eSTATES[1];
                //state[0] = eSTATES.POLICY_CREATED;
                ds = wPolicy.GetPolicyListImport(cmbProject.SelectedValue.ToString(), cmbBatch.SelectedValue.ToString());

                dept = _GetBundleStatus(cmbProject.SelectedValue.ToString(), cmbBatch.SelectedValue.ToString()).Rows[0][1].ToString();
                category = _GetBundleStatus(cmbProject.SelectedValue.ToString(), cmbBatch.SelectedValue.ToString()).Rows[0][2].ToString();

                if (ds.Tables.Count > 0)
                {
                    cmdBrowse.Enabled = true;
                    lstPolicy.Items.Clear();
                    lstCheckDeed.Items.Clear();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        cmbFile.DataSource = ds.Tables[0];
                        cmbFile.DisplayMember = ds.Tables[0].Columns[0].ToString();
                        cmbFile.ValueMember = ds.Tables[0].Columns[0].ToString();
                    }
                    //lstPolicy.Columns.Add("policy");
                    for (int z = 0; z < ds.Tables[0].Rows.Count; z++)
                    {
                        lstPolicy.Items.Add(ds.Tables[0].Rows[z][0].ToString());
                        lstCheckDeed.Items.Add(ds.Tables[0].Rows[z][0].ToString());
                        lstAddlPages.Items.Add(ds.Tables[0].Rows[z][0].ToString());
                    }
                    if (lstPolicy.Items.Count > 0)
                    {
                        lstPolicy.Items[0].Selected = true;
                        lstCheckDeed.Items[0].Selected = true;
                        GetIndexDetails(lstPolicy.Items[0].Text, cmbProject.SelectedValue.ToString(), cmbBatch.SelectedValue.ToString());
                        //GetDeedVolume(lstPolicy.Items[0].Text);
                    }
                    else
                    {

                        lblinfo.Text = "No More File Name Found...";

                        //pcf
                        lblfirstparty.Text = "";
                        lblsecondParty.Text = "";
                        txtDeedno.Text = "";
                        txtDeedYear.Text = "";
                        Txtfirstparty.Text = "";
                        txtsecondparty.Text = "";
                        txtVol.Text = "";
                        textBox1.Text = "";
                        //txtPgFrom.Text = "";
                        //txtPgTo.Text = "";

                        //pcr
                        textBox8.Text = "";
                        textBox4.Text = "";
                        textBox5.Text = "";
                        textBox3.Text = "";
                        textBox6.Text = "";
                        textBox9.Text = "";
                        textBox7.Text = "";

                        //pr
                        textBox17.Text = "";
                        textBox18.Text = "";
                        textBox19.Text = "";
                        textBox20.Text = "";

                        //gl + nom + fp
                        textBox25.Text = "";
                        textBox26.Text = "";
                        textBox28.Text = "";
                        textBox27.Text = "";
                        textBox33.Text = "";

                    }
                }
            }
        }
        private bool insertIntoDB(string image, int imageNo, OdbcTransaction trans, string PolicyNo)
        {
            bool commitBol = true;
            if (lstCheckDeed.Items.Count > 0)
            {
                string sqlStr = string.Empty;

                OdbcCommand sqlCmd = new OdbcCommand();
                sqlStr = @"insert into image_master(proj_key,batch_key,box_number, policy_number,created_by,created_dttm,Page_name,status,photo,serial_no,page_index_name) values('" +
                            cmbProject.SelectedValue.ToString() + "','" + cmbBatch.SelectedValue.ToString() + "','1','" + PolicyNo +
                            "','" + crd.created_by + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + image + "','24','0','" + imageNo + "','" + image + "')";

                /* sqlStr = @"insert into image_import(proj_key,batch_key, filename,created_by,created_dttm,Page_name,status,photo,serial_no,page_index_name) values('" +
                             cmbProject.SelectedValue.ToString() + "','" + cmbBatch.SelectedValue.ToString() + "','" + PolicyNo +
                             "','" + frmMain.name + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + image + "','0','0','" + imageNo + "','" + image + "')"; */
                sqlCmd.Connection = sqlCon;
                sqlCmd.Transaction = trans;
                sqlCmd.CommandText = sqlStr;
                int i = sqlCmd.ExecuteNonQuery();
                if (i > 0)
                {
                    commitBol = true;
                }
                else
                {
                    commitBol = false;
                }
            }

            return commitBol;

        }
        private bool UpdateCaseFile(OdbcTransaction trans, string policyNo)
        {
            OdbcCommand sqlCmd = new OdbcCommand();
            bool commitBol = true;
            if (lstCheckDeed.Items.Count > 0)
            {
                string sqlStr = "update metadata_entry set status = '2' where filename = '" + policyNo + "' and proj_code = '" + cmbProject.SelectedValue.ToString() + "' and batch_key = '" + cmbBatch.SelectedValue.ToString() + "'";
                sqlCmd.Connection = sqlCon;
                sqlCmd.Transaction = trans;
                sqlCmd.CommandText = sqlStr;
                int i = sqlCmd.ExecuteNonQuery();
                if (i > 0)
                {
                    commitBol = true;
                }
                else
                {
                    commitBol = false;
                }
            }
            return commitBol;
        }
        private void GetIndexDetails(string deed_no, string projKey, string batchKey)
        {
            wQuery pQuery = new ihwQuery(sqlCon);
            DataSet dsVol = new DataSet();
            DataTable dt = new DataTable();
            string IMGName = deed_no;
            //IMGName = IMGName.Split(new char[] { '[', ']' })[1];
            projKey = cmbProject.SelectedValue.ToString();
            batchKey = cmbBatch.SelectedValue.ToString();
            dt = GetInd(IMGName, projKey, batchKey);
            dsVol = GetIndex(IMGName, projKey, batchKey);

            //pcf
            string state = dsVol.Tables[0].Rows[0][6].ToString();
            string emp_name = dsVol.Tables[0].Rows[0][7].ToString();
            string desg = dsVol.Tables[0].Rows[0][8].ToString();
            string fileid = dsVol.Tables[0].Rows[0][9].ToString();
            string dob = dsVol.Tables[0].Rows[0][10].ToString();
            string doj = dsVol.Tables[0].Rows[0][11].ToString();
            //string dod = dsVol.Tables[0].Rows[0][12].ToString();
            string dor = dsVol.Tables[0].Rows[0][13].ToString();
            string psa = dsVol.Tables[0].Rows[0][14].ToString();
            string sec = dsVol.Tables[0].Rows[0][15].ToString();
            string fileno = dsVol.Tables[0].Rows[0][16].ToString();
            string ppo = dsVol.Tables[0].Rows[0][17].ToString();
            string gpo = dsVol.Tables[0].Rows[0][18].ToString();
            string mobile = dsVol.Tables[0].Rows[0][20].ToString();
            string hrms = dsVol.Tables[0].Rows[0][21].ToString();
            string spouce = dsVol.Tables[0].Rows[0][22].ToString();
            string familyPensioner = dsVol.Tables[0].Rows[0][34].ToString();
            //pcr
            string dod = dsVol.Tables[0].Rows[0][12].ToString();
            string ppogpocpo = dsVol.Tables[0].Rows[0][19].ToString();
            string place = dsVol.Tables[0].Rows[0][23].ToString();
            //prf
            string rulefileno = dsVol.Tables[0].Rows[0][24].ToString();
            string vol = dsVol.Tables[0].Rows[0][25].ToString();
            string subject = dsVol.Tables[0].Rows[0][26].ToString();
            //glc
            string series = dsVol.Tables[0].Rows[0][27].ToString();
            string acc = dsVol.Tables[0].Rows[0][28].ToString();
            string subname_l = dsVol.Tables[0].Rows[0][29].ToString();
            string year = dsVol.Tables[0].Rows[0][30].ToString();
            //nomination
            string doa = dsVol.Tables[0].Rows[0][31].ToString();
            //fp
            string authno = dsVol.Tables[0].Rows[0][32].ToString();
            string fpdate = dsVol.Tables[0].Rows[0][33].ToString();
            //ge
            string geno = dsVol.Tables[0].Rows[0][35].ToString();
            string penno = dsVol.Tables[0].Rows[0][36].ToString();
            string promoteddep = dsVol.Tables[0].Rows[0][37].ToString();
            string subdoctype = dsVol.Tables[0].Rows[0][38].ToString();
            string indexno = dsVol.Tables[0].Rows[0][39].ToString();
            string promotiondate = dsVol.Tables[0].Rows[0][40].ToString();
            //admin
            string idno = dsVol.Tables[0].Rows[0][41].ToString();
            string branch = dsVol.Tables[0].Rows[0][42].ToString();

            if (dsVol.Tables[0].Rows.Count >= 1)
            {
                if (category.ToString() == "Pension Case File")
                {
                    groupBox13.Visible = true;
                    groupBox14.Visible = true;

                    groupBox15.Visible = false;
                    groupBox16.Visible = false;

                    groupBox17.Visible = false;
                    groupBox18.Visible = false;

                    groupBox19.Visible = false;
                    groupBox20.Visible = false;

                    groupBox21.Visible = false;
                    groupBox23.Visible = false;

                    groupBox22.Visible = false;
                    groupBox24.Visible = false;

                    txtVol.Text = fileno;
                    Txtfirstparty.Text = state;
                    txtsecondparty.Text = emp_name;
                    textBox1.Text = dob;
                    txtDeedYear.Text = doj;
                    txtDeedno.Text = dor;
                    textBox35.Text = dod;
                    textBox36.Text = familyPensioner;
                    //txtsecondparty.Text = dsVol.Tables[0].Rows[0][5].ToString();
                    //textBoxIssueDate.Text = dsVol.Tables[0].Rows[0][6].ToString();


                    txtVol1.Text = fileno;
                    Txtfirstparty1.Text = state;
                    txtsecondparty1.Text = emp_name;
                    textBox2.Text = dob;
                    txtDeedYear1.Text = doj;
                    txtDeedno1.Text = dor;
                    //txtsecondparty1.Text = dsVol.Tables[0].Rows[0][5].ToString();
                    //textBoxIssueDate1.Text = dsVol.Tables[0].Rows[0][6].ToString();
                }
                else if (category.ToString() == "Pension Case Registers")
                {
                    groupBox13.Visible = false;
                    groupBox14.Visible = false;

                    groupBox15.Visible = true;
                    groupBox16.Visible = true;

                    groupBox17.Visible = false;
                    groupBox18.Visible = false;

                    groupBox19.Visible = false;
                    groupBox20.Visible = false;

                    groupBox21.Visible = false;
                    groupBox23.Visible = false;

                    groupBox22.Visible = false;
                    groupBox24.Visible = false;

                    textBox8.Text = fileno;
                    textBox4.Text = state;
                    textBox5.Text = emp_name;
                    textBox3.Text = dob;
                    textBox6.Text = doj;
                    textBox9.Text = dod;
                    textBox7.Text = dor;

                    textBox15.Text = fileno;
                    textBox11.Text = state;
                    textBox12.Text = emp_name;
                    textBox10.Text = dob;
                    textBox13.Text = doj;
                    textBox16.Text = dod;
                    textBox14.Text = dor;
                }
                else if (category.ToString() == "Pension Rule Files")
                {
                    groupBox13.Visible = false;
                    groupBox14.Visible = false;

                    groupBox15.Visible = false;
                    groupBox16.Visible = false;

                    groupBox17.Visible = true;
                    groupBox18.Visible = true;

                    groupBox19.Visible = false;
                    groupBox20.Visible = false;

                    groupBox21.Visible = false;
                    groupBox23.Visible = false;

                    groupBox22.Visible = false;
                    groupBox24.Visible = false;

                    textBox17.Text = rulefileno;
                    textBox18.Text = state;
                    textBox19.Text = vol;
                    textBox20.Text = subject;

                    textBox24.Text = rulefileno;
                    textBox23.Text = state;
                    textBox22.Text = vol;
                    textBox21.Text = subject;

                }
                else if (category.ToString() == "Ledger Cards")
                {
                    groupBox13.Visible = false;
                    groupBox14.Visible = false;

                    groupBox15.Visible = false;
                    groupBox16.Visible = false;

                    groupBox17.Visible = false;
                    groupBox18.Visible = false;

                    groupBox19.Visible = true;
                    groupBox20.Visible = true;

                    groupBox21.Visible = false;
                    groupBox23.Visible = false;

                    groupBox22.Visible = false;
                    groupBox24.Visible = false;

                    textBox25.Text = series;
                    textBox26.Text = acc;
                    textBox28.Text = state;
                    textBox27.Text = subname_l;
                    textBox33.Text = dob;

                    textBox32.Text = series;
                    textBox31.Text = acc;
                    textBox30.Text = state;
                    textBox29.Text = subname_l;
                    textBox34.Text = dob;

                }
                else if (category.ToString() == "Nomination")
                {
                    groupBox13.Visible = false;
                    groupBox14.Visible = false;

                    groupBox15.Visible = false;
                    groupBox16.Visible = false;

                    groupBox17.Visible = false;
                    groupBox18.Visible = false;

                    groupBox19.Visible = true;
                    groupBox20.Visible = true;

                    groupBox21.Visible = false;
                    groupBox23.Visible = false;

                    groupBox22.Visible = false;
                    groupBox24.Visible = false;

                    textBox25.Text = series;
                    textBox26.Text = acc;
                    textBox28.Text = state;
                    textBox27.Text = subname_l;
                    textBox33.Text = dob;

                    textBox32.Text = series;
                    textBox31.Text = acc;
                    textBox30.Text = state;
                    textBox29.Text = subname_l;
                    textBox34.Text = dob;
                }
                else if (category.ToString() == "Final Payment Case File")
                {
                    groupBox13.Visible = false;
                    groupBox14.Visible = false;

                    groupBox15.Visible = false;
                    groupBox16.Visible = false;

                    groupBox17.Visible = false;
                    groupBox18.Visible = false;

                    groupBox19.Visible = true;
                    groupBox20.Visible = true;

                    groupBox21.Visible = false;
                    groupBox23.Visible = false;

                    groupBox22.Visible = false;
                    groupBox24.Visible = false;

                    textBox25.Text = series;
                    textBox26.Text = acc;
                    textBox28.Text = state;
                    textBox27.Text = subname_l;
                    textBox33.Text = dob;

                    textBox32.Text = series;
                    textBox31.Text = acc;
                    textBox30.Text = state;
                    textBox29.Text = subname_l;
                    textBox34.Text = dob;
                }
                else if (dept.ToLower() == "ge")
                {
                    groupBox13.Visible = false;
                    groupBox14.Visible = false;

                    groupBox15.Visible = false;
                    groupBox16.Visible = false;

                    groupBox17.Visible = false;
                    groupBox18.Visible = false;

                    groupBox19.Visible = false;
                    groupBox20.Visible = false;

                    groupBox21.Visible = true;
                    groupBox23.Visible = true;

                    groupBox22.Visible = false;
                    groupBox24.Visible = false;

                    textBox44.Text = geno;
                    textBox45.Text = penno;
                    textBox46.Text = indexno;
                    textBox40.Text = state;
                    textBox41.Text = emp_name;
                    textBox37.Text = promoteddep;
                    textBox39.Text = dob;
                    textBox47.Text = subdoctype;
                    textBox42.Text = doj;
                    textBox43.Text = dor;
                    textBox38.Text = promotiondate;

                    textBox62.Text = geno;
                    textBox52.Text = state;
                    textBox59.Text = emp_name;
                    textBox50.Text = dob;
                    textBox60.Text = doj;
                    textBox63.Text = dor;
                    textBox61.Text = promotiondate;
                }
                else if (dept.ToLower() == "admin")
                {
                    groupBox13.Visible = false;
                    groupBox14.Visible = false;

                    groupBox15.Visible = false;
                    groupBox16.Visible = false;

                    groupBox17.Visible = false;
                    groupBox18.Visible = false;

                    groupBox19.Visible = false;
                    groupBox20.Visible = false;

                    groupBox21.Visible = false;
                    groupBox23.Visible = false;

                    groupBox22.Visible = true;
                    groupBox24.Visible = true;

                    textBox58.Text = idno;
                    textBox54.Text = state;
                    textBox55.Text = emp_name;
                    textBox49.Text = desg;
                    textBox51.Text = branch;
                    textBox48.Text = subdoctype;
                    textBox53.Text = dob;
                    textBox56.Text = doj;
                    textBox57.Text = dor;

                    textBox70.Text = idno;
                    textBox66.Text = state;
                    textBox67.Text = emp_name;
                    textBox65.Text = dob;
                    textBox68.Text = doj;
                    textBox69.Text = dor;
                }
            }
            if (dsVol.Tables[0].Rows.Count >= 2)
            {
                if (category.ToString() == "Pension Case File")
                {
                    groupBox13.Visible = true;
                    groupBox14.Visible = true;

                    groupBox15.Visible = false;
                    groupBox16.Visible = false;

                    groupBox17.Visible = false;
                    groupBox18.Visible = false;

                    groupBox19.Visible = false;
                    groupBox20.Visible = false;

                    groupBox21.Visible = false;
                    groupBox23.Visible = false;

                    groupBox22.Visible = false;
                    groupBox24.Visible = false;

                    txtVol.Text = fileno;
                    Txtfirstparty.Text = state;
                    txtsecondparty.Text = emp_name;
                    textBox1.Text = dob;
                    txtDeedYear.Text = doj;
                    txtDeedno.Text = dor;
                    textBox35.Text = dod;
                    textBox36.Text = familyPensioner;
                    //txtsecondparty.Text = dsVol.Tables[0].Rows[0][5].ToString();
                    //textBoxIssueDate.Text = dsVol.Tables[0].Rows[0][6].ToString();


                    txtVol1.Text = fileno;
                    Txtfirstparty1.Text = state;
                    txtsecondparty1.Text = emp_name;
                    textBox2.Text = dob;
                    txtDeedYear1.Text = doj;
                    txtDeedno1.Text = dor;
                    //txtsecondparty1.Text = dsVol.Tables[0].Rows[0][5].ToString();
                    //textBoxIssueDate1.Text = dsVol.Tables[0].Rows[0][6].ToString();
                }
                else if (category.ToString() == "Pension Case Registers")
                {
                    groupBox13.Visible = false;
                    groupBox14.Visible = false;

                    groupBox15.Visible = true;
                    groupBox16.Visible = true;

                    groupBox17.Visible = false;
                    groupBox18.Visible = false;

                    groupBox19.Visible = false;
                    groupBox20.Visible = false;

                    groupBox21.Visible = false;
                    groupBox23.Visible = false;

                    groupBox22.Visible = false;
                    groupBox24.Visible = false;

                    textBox8.Text = fileno;
                    textBox4.Text = state;
                    textBox5.Text = emp_name;
                    textBox3.Text = dob;
                    textBox6.Text = doj;
                    textBox9.Text = dod;
                    textBox7.Text = dor;

                    textBox15.Text = fileno;
                    textBox11.Text = state;
                    textBox12.Text = emp_name;
                    textBox10.Text = dob;
                    textBox13.Text = doj;
                    textBox16.Text = dod;
                    textBox14.Text = dor;
                }
                else if (category.ToString() == "Pension Rule Files")
                {
                    groupBox13.Visible = false;
                    groupBox14.Visible = false;

                    groupBox15.Visible = false;
                    groupBox16.Visible = false;

                    groupBox17.Visible = true;
                    groupBox18.Visible = true;

                    groupBox19.Visible = false;
                    groupBox20.Visible = false;

                    groupBox21.Visible = false;
                    groupBox23.Visible = false;

                    groupBox22.Visible = false;
                    groupBox24.Visible = false;

                    textBox17.Text = rulefileno;
                    textBox18.Text = state;
                    textBox19.Text = vol;
                    textBox20.Text = subject;

                    textBox24.Text = rulefileno;
                    textBox23.Text = state;
                    textBox22.Text = vol;
                    textBox21.Text = subject;

                }
                else if (category.ToString() == "Ledger Cards")
                {
                    groupBox13.Visible = false;
                    groupBox14.Visible = false;

                    groupBox15.Visible = false;
                    groupBox16.Visible = false;

                    groupBox17.Visible = false;
                    groupBox18.Visible = false;

                    groupBox19.Visible = true;
                    groupBox20.Visible = true;

                    groupBox21.Visible = false;
                    groupBox23.Visible = false;

                    groupBox22.Visible = false;
                    groupBox24.Visible = false;

                    textBox25.Text = series;
                    textBox26.Text = acc;
                    textBox28.Text = state;
                    textBox27.Text = subname_l;
                    textBox33.Text = dob;

                    textBox32.Text = series;
                    textBox31.Text = acc;
                    textBox30.Text = state;
                    textBox29.Text = subname_l;
                    textBox34.Text = dob;

                }
                else if (category.ToString() == "Nomination")
                {
                    groupBox13.Visible = false;
                    groupBox14.Visible = false;

                    groupBox15.Visible = false;
                    groupBox16.Visible = false;

                    groupBox17.Visible = false;
                    groupBox18.Visible = false;

                    groupBox19.Visible = true;
                    groupBox20.Visible = true;

                    groupBox21.Visible = false;
                    groupBox23.Visible = false;

                    groupBox22.Visible = false;
                    groupBox24.Visible = false;

                    textBox25.Text = series;
                    textBox26.Text = acc;
                    textBox28.Text = state;
                    textBox27.Text = subname_l;
                    textBox33.Text = dob;

                    textBox32.Text = series;
                    textBox31.Text = acc;
                    textBox30.Text = state;
                    textBox29.Text = subname_l;
                    textBox34.Text = dob;
                }
                else if (category.ToString() == "Final Payment Case File")
                {
                    groupBox13.Visible = false;
                    groupBox14.Visible = false;

                    groupBox15.Visible = false;
                    groupBox16.Visible = false;

                    groupBox17.Visible = false;
                    groupBox18.Visible = false;

                    groupBox19.Visible = true;
                    groupBox20.Visible = true;

                    groupBox21.Visible = false;
                    groupBox23.Visible = false;

                    groupBox22.Visible = false;
                    groupBox24.Visible = false;

                    textBox25.Text = series;
                    textBox26.Text = acc;
                    textBox28.Text = state;
                    textBox27.Text = subname_l;
                    textBox33.Text = dob;

                    textBox32.Text = series;
                    textBox31.Text = acc;
                    textBox30.Text = state;
                    textBox29.Text = subname_l;
                    textBox34.Text = dob;
                }
                else if (dept.ToLower() == "ge")
                {
                    groupBox13.Visible = false;
                    groupBox14.Visible = false;

                    groupBox15.Visible = false;
                    groupBox16.Visible = false;

                    groupBox17.Visible = false;
                    groupBox18.Visible = false;

                    groupBox19.Visible = false;
                    groupBox20.Visible = false;

                    groupBox21.Visible = true;
                    groupBox23.Visible = true;

                    groupBox22.Visible = false;
                    groupBox24.Visible = false;

                    textBox44.Text = geno;
                    textBox45.Text = penno;
                    textBox46.Text = indexno;
                    textBox40.Text = state;
                    textBox41.Text = emp_name;
                    textBox37.Text = promoteddep;
                    textBox39.Text = dob;
                    textBox47.Text = subdoctype;
                    textBox42.Text = doj;
                    textBox43.Text = dor;
                    textBox38.Text = promotiondate;

                    textBox62.Text = geno;
                    textBox52.Text = state;
                    textBox59.Text = emp_name;
                    textBox50.Text = dob;
                    textBox60.Text = doj;
                    textBox63.Text = dor;
                    textBox61.Text = promotiondate;
                }
                else if (dept.ToLower() == "admin")
                {
                    groupBox13.Visible = false;
                    groupBox14.Visible = false;

                    groupBox15.Visible = false;
                    groupBox16.Visible = false;

                    groupBox17.Visible = false;
                    groupBox18.Visible = false;

                    groupBox19.Visible = false;
                    groupBox20.Visible = false;

                    groupBox21.Visible = false;
                    groupBox23.Visible = false;

                    groupBox22.Visible = true;
                    groupBox24.Visible = true;

                    textBox58.Text = idno;
                    textBox54.Text = state;
                    textBox55.Text = emp_name;
                    textBox49.Text = desg;
                    textBox51.Text = branch;
                    textBox48.Text = subdoctype;
                    textBox53.Text = dob;
                    textBox56.Text = doj;
                    textBox57.Text = dor;

                    textBox70.Text = idno;
                    textBox66.Text = state;
                    textBox67.Text = emp_name;
                    textBox65.Text = dob;
                    textBox68.Text = doj;
                    textBox69.Text = dor;
                }
            }
            if (dsVol.Tables[0].Rows.Count > 1)
            {

            }
        }

        public DataTable GetInd(string deed_no, string projKey, string batchKey)
        {
            string sqlStr = null;
            DataTable dsVol = new DataTable();
            OdbcDataAdapter sqlAdap = null;

            sqlStr = "select letter_no,issue_from,issue_to,sub_cat,sub_name,doc_type,issue_date from metadata_entry where filename = '" + deed_no + "' and proj_key = '" + projKey + "' and batch_key = '" + batchKey + "' ";


            try
            {
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(dsVol);
            }
            catch (Exception ex)
            {
                string e = ex.Message;
                sqlAdap.Dispose();
            }
            return dsVol;
        }

        public DataSet GetIndex(string deed_no, string projKey, string batchKey)
        {
            string sqlStr = null;
            DataSet dsVol = new DataSet();
            OdbcDataAdapter sqlAdap = null;

            sqlStr = "select distinct proj_code, batch_Key,item_no,filename,department,subcat,state_name,emp_name,desg,fileid,date_format(birth_date,'%Y-%m-%d'),date_format(joining_date,'%Y-%m-%d'),date_format(death_date,'%Y-%m-%d')," +
                "date_format(retirement_date,'%Y-%m-%d'),psa_name,section,pension_file_no,ppo_fppo,gpo_dgpo,ppo_gpo_cpo,mobile,hrms_id,spouce,place_payment,rule_file,vol,subject,series,acc,subscriber_name," +
                "ledger_year,date_format(accept_date,'%Y-%m-%d'),fp_auth_no,date_format(fp_date,'%Y-%m-%d'),family_pensioner,ge_no,pen_no,promoted_dep,sub_doc_type,index_no,date_format(promotion_date,'%Y-%m-%d'),id_no,branch_name from metadata_entry where proj_code = '" + projKey + "' and batch_key = '" + batchKey + "' and filename = '" + deed_no + "' ";


            try
            {
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(dsVol);
            }
            catch (Exception ex)
            {
                string e = ex.Message;
                sqlAdap.Dispose();
            }
            return dsVol;
        }

        private void cmbBatch_Validated(object sender, EventArgs e)
        {
            string batchKey = null;
            if (cmbBatch.SelectedIndex >= 0 && cmbBatch.SelectedValue != null)
            {
                batchKey = cmbBatch.SelectedValue.ToString();
                //CtrlPolicy pPolicy = new CtrlPolicy(Convert.ToInt32(cmbProject.SelectedValue.ToString()), Convert.ToInt32(cmbBatch.SelectedValue.ToString()), 1, "0");
                CtrlPolicy pPolicy = new CtrlPolicy(Convert.ToInt32(cmbProject.SelectedValue.ToString()), Convert.ToInt32(cmbBatch.SelectedValue.ToString()), "1", "0");
                wfePolicy wPolicy = new wfePolicy(sqlCon, pPolicy);

                //eSTATES[] state = new eSTATES[1];
                //state[0] = eSTATES.POLICY_CREATED;
                ds = wPolicy.GetPolicyListImport(cmbProject.SelectedValue.ToString(), cmbBatch.SelectedValue.ToString());

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count <= 0)
                    {
                        DialogResult dr = MessageBox.Show(this, "There's no filename for this batch, You sure to continue?", "Nothing Left", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        if (dr == DialogResult.No)
                        {
                            cmbBatch.Focus();
                            return;
                        }
                    }
                }
            }
        }

        //public void ResizeImg(Image img, string filename)
        //{
        //    Bitmap bitmap = (Bitmap)img;
        //    bitmap.SetResolution(200, 200);
        //    //bitmap.
        //    //Bitmap bmp = (Bitmap)img;
        //    //bmp.SetResolution(200, 200);
        //    bitmap.Save(txtPath.Text + "\\" + filename, ImageFormat.Jpeg);
        //    bitmap.Dispose();
        //    //Graphics graphics = Graphics.FromImage((Image)bitmap);
        //    //using (Graphics graphics = Graphics.FromImage((Image)bitmap))
        //    //{
        //    //    graphics.DrawImage(img, 0, 0, 1674, 2771);
        //    //}
        //    ////graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
        //    ////graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
        //    ////graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        //    ////graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

        //    //img.Save(txtPath.Text + "\\" + filename, ImageFormat.Jpeg);
        //}
        public void ResizeImg(string filename, Image img, int size)
        {
            EncoderParameter qualityParam = new EncoderParameter(Encoder.Quality, size);
            ImageCodecInfo encoderInfo = GetEncoderInfo(ImageFormat.Jpeg);
            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;
            img.Save(txtPath.Text + "\\" + filename, encoderInfo, encoderParams);
            //img.Save(txtPath.Text,ImageCodecInfo.;
        }
        public ImageCodecInfo GetEncoderInfo(ImageFormat imageFormat)
        {
            // Get image codecs for all image formats 
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == imageFormat.Guid)
                    return codec;
            }

            // Find the correct image codec 
            //for (int i = 0; i < codecs.Length; i++)
            //    if (codecs[i].MimeType == mimeType)
            //        return codecs[i];

            return null;
        }

        private void cmdBrowse_Click(object sender, EventArgs e)
        {
            try
            {

                lblinfo.Text = "";
                List<string> fileNames = new List<string>();
                List<string> tempPath = new System.Collections.Generic.List<string>(1000);


                fbdPath.ShowDialog();
                txtPath.Text = fbdPath.SelectedPath;
                DirectoryInfo selectedPath = new DirectoryInfo(txtPath.Text);

                int len = cmbBatch.Text.Length;
                int len1 = cmbBatch.Text.IndexOf('_') + 1;

                // string zyx1 = cmbBatch.Text.Substring(cmbBatch.Text.IndexOf('_') + 1, cmbBatch.Text.Length - cmbBatch.Text.IndexOf('_') + 1);

                if (Path.GetFileName(txtPath.Text.ToLower()) == cmbBatch.Text.ToLower())
                {
                    cmdImport.Enabled = true;
                    CmdFinalSave.Enabled = true;
                }
                else
                {

                    cmdImport.Enabled = false;
                    CmdFinalSave.Enabled = false;
                    MessageBox.Show(this, "Please select proper image folder", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                if (Directory.Exists(txtPath.Text + "\\Backup"))
                {
                    Directory.Delete(txtPath.Text + "\\Backup", true);
                    Directory.CreateDirectory(txtPath.Text + "\\Backup");
                    txtPath.Text = txtPath.Text + "\\Backup";
                    DirectoryInfo selectedPath1 = new DirectoryInfo(txtPath.Text);
                    foreach (FileInfo file in selectedPath.GetFiles())
                    {
                        if (file.Extension == ".TIF" || file.Extension == ".tif")
                        {
                            file.CopyTo(txtPath.Text + "\\" + file.Name);
                        }
                        else if (file.Extension.ToLower() == ".jpg" || file.Extension.ToLower() == "jpeg")
                        {
                            Image img = Image.FromFile(file.FullName);

                            string filename = Path.GetFileNameWithoutExtension(file.FullName) + ".TIF";
                            try
                            {
                                ResizeImg(filename, img, resize);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(this, "Error!! Filename - " + file.FullName, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }

                        }
                    }
                }
                else
                {
                    Directory.CreateDirectory(txtPath.Text + "\\Backup");
                    txtPath.Text = txtPath.Text + "\\Backup";
                    DirectoryInfo selectedPath1 = new DirectoryInfo(txtPath.Text);
                    foreach (FileInfo file in selectedPath.GetFiles())
                    {
                        if (file.Extension == ".TIF" || file.Extension == ".tif")
                        {
                            file.CopyTo(txtPath.Text + "\\" + file.Name);
                        }
                        else if (file.Extension.ToLower() == ".jpg" || file.Extension.ToLower() == ".jpeg")
                        {
                            Image img = Image.FromFile(file.FullName);

                            string filename = Path.GetFileNameWithoutExtension(file.FullName) + ".TIF";
                            try
                            {
                                ResizeImg(filename, img, resize);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(this, "Error!! Filename - " + file.FullName, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }

                        }
                    }
                }

                if (selectedPath.GetFiles().Length > 0)
                {

                    string BackupPath = selectedPath + "\\backup";
                    foreach (string file in Directory.GetFiles(BackupPath))
                    {
                        if (Path.GetExtension(file).ToLower() == ".tif")
                        {
                            fileNames.Add(file);
                            tempPath.Add(file);
                        }
                    }
                }

                //lvwItem.SubItems.Add(CLAIMS.ToString());
                //lvwItem.SubItems.Add("0");
                ListViewItems.Clear();
                ListViewItems1.Clear();
                lstImage.Items.Clear();
                //lstTotalImage.Items.Clear();
                lstImage.BeginUpdate();
                //lstTotalImage.BeginUpdate();

                foreach (string fileName in fileNames)
                {

                    ListViewItem lvi = lstImage.Items.Add(System.IO.Path.GetFileNameWithoutExtension(fileName));
                    //ListViewItem lvi1 = lstTotalImage.Items.Add(System.IO.Path.GetFileNameWithoutExtension(fileName));
                    lvi.Tag = fileName;
                    //lvi1.Tag = fileName;
                    ListViewItems.Add(fileName, lvi);
                    lstTotalImage.Rows.Add();
                    lstTotalImage.Rows[pos].Cells[0].Value = System.IO.Path.GetFileNameWithoutExtension(fileName);

                    pos = pos + 1;
                    //ListViewItems1.Add(fileName, lvi1);
                }
                //foreach (string fileName in fileNames)
                //{
                //    ListViewItem lvi1 = lstTotalImage.Items.Add(System.IO.Path.GetFileNameWithoutExtension(fileName));
                //    lvi1.Tag = fileName;
                //    ListViewItems.Add(fileName, lvi1);
                //}
                lstImage.EndUpdate();
                // lstTotalImage.EndUpdate();
                if (lstPolicy.Items.Count > 0)
                {
                    lstPolicy.Items[0].Selected = true;
                }
                groupBox2.Enabled = false;

                if (lstImage.Items.Count > 0)
                {

                    lstImage.Items[0].Focused = true;
                    lstImage.Items[0].Selected = true;
                    picMain.Height = 647;
                    picMain.Width = 625;
                    picMain.Refresh();
                    picMain.ImageLocation = null;
                    string imgPath = txtPath.Text + "\\" + lstImage.Items[0].Text + ".TIF";
                    picMain.ImageLocation = imgPath;


                    Image newImage = Image.FromFile(imgPath);
                    picMain.Height = Convert.ToInt32(picMain.Height * 1.1);
                    picMain.Width = Convert.ToInt32(picMain.Height * newImage.Width / newImage.Height);

                    picMain.SizeMode = PictureBoxSizeMode.StretchImage;
                    //picMain.Width = Convert.ToInt32(picMain.Width * 1);
                    //picMain.Height = Convert.ToInt32(picMain.Height * 1);
                    picMain.Refresh();
                    newImage.Dispose();
                    // GC.Collect();
                    picMain.MouseWheel += new MouseEventHandler(picMain_MouseWheel);
                    //picMain.MouseHover += new EventHandler(picMain_MouseHover);
                    lstImage.Select();
                }
                else
                {
                    picMain.ImageLocation = null;
                    lstImage.Select();
                }
                //lstImage.Select();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void cmdImport_Click(object sender, EventArgs e)
        {
            OdbcTransaction sqlTrans = null;

            if (cmdImport.Enabled == true)
            {
                try
                {
                    if (lstPolicy.Items.Count > 0)
                    {
                        if (lstSelImg.Items.Count == 0)
                        {
                            DialogResult dr = MessageBox.Show(this, "No Images selected for this Filename... Are you sure to continue?", "Selected no Image", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                            if (dr == DialogResult.Yes)
                            {
                                if (lstPolicy.Items.Count > 0)
                                {
                                    lstPolicy.Items[0].Selected = true;

                                    if (lstPolicy.SelectedItems.Count > 0)
                                    {
                                        for (int i = 0; i < lstSelImg.Items.Count; i++)
                                        {
                                            //lstSelImg.Items[i].Text
                                            for (int j = 0; j < lstTotalImage.Rows.Count; j++)
                                            {
                                                if (lstTotalImage.Rows[j].Cells[0].Value != null)
                                                {
                                                    if (lstSelImg.Items[i].Text == lstTotalImage.Rows[j].Cells[0].Value.ToString())
                                                    {
                                                        lstTotalImage.Rows[j].Cells[1].Value = lstPolicy.Items[0].Text;
                                                    }
                                                }
                                            }

                                        }

                                    }
                                    lstSelImg.Items.Clear();
                                    lstPolicy.SelectedItems[0].Remove();
                                    if (lstPolicy.Items.Count > 0)
                                    {
                                        lstPolicy.Items[0].Selected = true;

                                    }
                                }
                            }

                            else
                            {
                                return;
                            }
                        }
                        ////
                        else
                        {
                            if (lstPolicy.Items.Count > 0)
                            {
                                lstPolicy.Items[0].Selected = true;

                                if (lstPolicy.SelectedItems.Count > 0)
                                {
                                    for (int i = 0; i < lstSelImg.Items.Count; i++)
                                    {
                                        for (int j = 0; j < lstTotalImage.Rows.Count; j++)
                                        {
                                            if (lstTotalImage.Rows[j].Cells[0].Value != null)
                                            {
                                                if (lstSelImg.Items[i].Text == lstTotalImage.Rows[j].Cells[0].Value.ToString())
                                                {
                                                    lstTotalImage.Rows[j].Cells[1].Value = lstPolicy.Items[0].Text;
                                                }
                                            }
                                        }

                                    }

                                }
                                lstSelImg.Items.Clear();
                                lstPolicy.SelectedItems[0].Remove();
                                if (lstPolicy.Items.Count > 0)
                                {
                                    lstPolicy.Items[0].Selected = true;

                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(this, "There's no file present in Import Tab...", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }

                    if (lstPolicy.Items.Count > 0)
                    {
                        lstPolicy.Items[0].Selected = true;
                        GetIndexDetails(lstPolicy.SelectedItems[0].Text, cmbProject.SelectedValue.ToString(), cmbBatch.SelectedValue.ToString());
                        //GetDeedVolume(lstPolicy.SelectedItems[0].Text);
                    }
                    if (lstImage.Items.Count > 0)
                    {

                        lstImage.Items[0].Focused = true;
                        lstImage.Items[0].Selected = true;
                        picMain.Height = 647;
                        picMain.Width = 625;
                        picMain.Refresh();
                        picMain.ImageLocation = null;
                        string imgPath = txtPath.Text + "\\" + lstImage.Items[0].Text + ".TIF";
                        picMain.ImageLocation = imgPath;


                        Image newImage = Image.FromFile(imgPath);
                        picMain.Height = Convert.ToInt32(picMain.Height * 1.1);
                        picMain.Width = Convert.ToInt32(picMain.Height * newImage.Width / newImage.Height);

                        picMain.SizeMode = PictureBoxSizeMode.StretchImage;
                        //picMain.Width = Convert.ToInt32(picMain.Width * 1);
                        //picMain.Height = Convert.ToInt32(picMain.Height * 1);
                        picMain.Refresh();
                        newImage.Dispose();
                        // GC.Collect();
                        picMain.MouseWheel += new MouseEventHandler(picMain_MouseWheel);
                        picMain.MouseHover += new EventHandler(picMain_MouseHover);
                        lstImage.Select();
                    }
                    else
                    {
                        picMain.ImageLocation = null;
                        lstImage.Select();
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                    lblinfo.Text = "Error...";
                    sqlTrans.Rollback();
                }
            }
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            picMain.Image = null;

            try
            {
                for (int i = 0; i < lstImage.Items.Count; i++)
                {
                    if (lstImage.Items[i].Selected == true)
                    {
                        lstSelImg.Items.Add(lstImage.Items[i].Text.ToString());
                    }
                }
                foreach (ListViewItem eachItem in lstImage.SelectedItems)
                {
                    lstImage.Items.Remove(eachItem);
                }
                if (lstImage.Items.Count > 0)
                {

                    lstImage.Items[0].Focused = true;
                    lstImage.Items[0].Selected = true;
                    picMain.Height = 647;
                    picMain.Width = 625;
                    picMain.Refresh();
                    picMain.ImageLocation = null;
                    string imgPath = txtPath.Text + "\\" + lstImage.Items[0].Text + ".TIF";
                    picMain.ImageLocation = imgPath;


                    Image newImage = Image.FromFile(imgPath);
                    picMain.Height = Convert.ToInt32(picMain.Height * 1.1);
                    picMain.Width = Convert.ToInt32(picMain.Height * newImage.Width / newImage.Height);

                    picMain.SizeMode = PictureBoxSizeMode.StretchImage;
                    //picMain.Width = Convert.ToInt32(picMain.Width * 1);
                    //picMain.Height = Convert.ToInt32(picMain.Height * 1);
                    picMain.Refresh();
                    newImage.Dispose();
                    // GC.Collect();
                    picMain.MouseWheel += new MouseEventHandler(picMain_MouseWheel);
                    picMain.MouseHover += new EventHandler(picMain_MouseHover);
                    lstImage.Select();
                }
                else
                {
                    picMain.ImageLocation = null;
                    lstImage.Select();
                }
                lstImage.Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void cmdRemove_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < lstSelImg.Items.Count; i++)
                {
                    if (lstSelImg.Items[i].Selected == true)
                    {
                        lstImage.Items.Add(lstSelImg.Items[i].Text.ToString());
                    }
                }
                foreach (ListViewItem eachItem in lstSelImg.SelectedItems)
                {
                    lstSelImg.Items.Remove(eachItem);
                }
                //if (lstImage.Items.Count > 0)
                //{

                //    lstImage.Items[0].Focused = true;
                //    lstImage.Items[0].Selected = true;
                //    picMain.Height = 647;
                //    picMain.Width = 625;
                //    picMain.Refresh();
                //    picMain.ImageLocation = null;
                //    string imgPath = txtPath.Text + "\\" + lstImage.Items[0].Text + ".TIF";
                //    picMain.ImageLocation = imgPath;


                //    Image newImage = Image.FromFile(imgPath);
                //    //picMain.Height = Convert.ToInt32(picMain.Height * 1.1);
                //    //picMain.Width = Convert.ToInt32(picMain.Height * newImage.Width / newImage.Height);

                //    picMain.SizeMode = PictureBoxSizeMode.StretchImage;
                //    //picMain.Width = Convert.ToInt32(picMain.Width * 1);
                //    //picMain.Height = Convert.ToInt32(picMain.Height * 1);
                //    picMain.Refresh();
                //    newImage.Dispose();
                //    // GC.Collect();
                //    //picMain.MouseWheel += new MouseEventHandler(picMain_MouseWheel);
                //    //picMain.MouseHover += new EventHandler(picMain_MouseHover);
                //    //lstImage.Select();
                //}
                //else
                //{
                //    picMain.ImageLocation = null;
                //    //lstImage.Select();
                //}
                if (lstImage.Items.Count > 0)
                {
                    lstImage.Select();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void lstImage_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (lstImage.Items.Count > 0)
                {

                    if (lstImage.SelectedItems.Count > 0)
                    {

                        //lstImage.SelectedItems[0].Focused = true;
                        //lstImage.SelectedItems[0].Selected = true;
                        picMain.Height = 647;
                        picMain.Width = 625;
                        picMain.Refresh();
                        picMain.ImageLocation = null;
                        string imgPath = txtPath.Text + "\\" + lstImage.SelectedItems[0].Text + ".TIF";
                        picMain.ImageLocation = imgPath;


                        Image newImage = Image.FromFile(imgPath);
                        //picMain.Height = Convert.ToInt32(picMain.Height * 1.1);
                        //picMain.Width = Convert.ToInt32(picMain.Height * newImage.Width / newImage.Height);

                        picMain.SizeMode = PictureBoxSizeMode.StretchImage;
                        //picMain.Width = Convert.ToInt32(picMain.Width * 1);
                        //picMain.Height = Convert.ToInt32(picMain.Height * 1);
                        picMain.Refresh();
                        newImage.Dispose();
                        // GC.Collect();
                        //picMain.MouseWheel += new MouseEventHandler(picMain_MouseWheel);
                        //picMain.MouseHover += new EventHandler(picMain_MouseHover);
                        //lstImage.Select();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                GC.Collect();
            }
        }
        void picMain_MouseWheel(object sender, MouseEventArgs e)
        {
            //if (e.Delta < 0)
            //{
            //    ZoomIn();
            //}
            //else
            //{
            //    ZoomOut();
            //}

        }
        protected override void OnMouseWheel(MouseEventArgs mea)
        {

            //if (picMain.Image != null)
            //{
            //    if (mea.Delta > 0)
            //    {
            //        if ((picMain.Width < (15 * this.Width)) && (picMain.Height < (15 * this.Height)))
            //        {
            //            picMain.Width = (int)(picMain.Width * 1.25);
            //            picMain.Height = (int)(picMain.Height * 1.25);
            //        }
            //    }

            //    else
            //    {
            //        // Check if the pictureBox dimensions are in range (15 is the minimum and maximum zoom level)
            //        if ((picMain.Width > (this.Width / 15)) && (picMain.Height > (this.Height / 15)))
            //        {
            //            // Change the size of the picturebox, divide it by the ZOOM FACTOR
            //            picMain.Width = (int)(picMain.Width / 1.25);
            //            picMain.Height = (int)(picMain.Height / 1.25);
            //        }
            //    }
            //}
            //picMain.Refresh();
        }
        private void ZoomOut()
        {
            if (picMain.Height > (panel1.Height))
            {
                panel1.Width = picMain.Width;
                picMain.SizeMode = PictureBoxSizeMode.StretchImage;
                picMain.Width = Convert.ToInt32(picMain.Width / ZOOMFACTOR);
                picMain.Height = Convert.ToInt32(picMain.Height / ZOOMFACTOR);
                picMain.Refresh();
            }
        }
        private void ZoomIn()
        {
            if ((picMain.Width < (MINMAX * panel1.Width)) &&
                (picMain.Height < (MINMAX * panel1.Height)))
            {
                picMain.Width = Convert.ToInt32(picMain.Width * ZOOMFACTOR);
                picMain.Height = Convert.ToInt32(picMain.Height * ZOOMFACTOR);
                picMain.SizeMode = PictureBoxSizeMode.StretchImage;
                picMain.Refresh();
            }
        }

        private void lstImage_Leave(object sender, EventArgs e)
        {
            if (lstSelImg.Items.Count > 0)
            {

                lstSelImg.Items[0].Focused = true;
                lstSelImg.Items[0].Selected = true;
                picMain.Height = 647;
                picMain.Width = 625;
                picMain.Refresh();
                picMain.ImageLocation = null;
                string imgPath = txtPath.Text + "\\" + lstSelImg.Items[0].Text + ".TIF";
                picMain.ImageLocation = imgPath;


                Image newImage = Image.FromFile(imgPath);
                picMain.Height = Convert.ToInt32(picMain.Height * 1.1);
                picMain.Width = Convert.ToInt32(picMain.Height * newImage.Width / newImage.Height);

                picMain.SizeMode = PictureBoxSizeMode.StretchImage;
                //picMain.Width = Convert.ToInt32(picMain.Width * 1);
                //picMain.Height = Convert.ToInt32(picMain.Height * 1);
                picMain.Refresh();
                newImage.Dispose();
                // GC.Collect();
                picMain.MouseWheel += new MouseEventHandler(picMain_MouseWheel);
                picMain.MouseHover += new EventHandler(picMain_MouseHover);
                //lstSelImg.Select();
            }
            else
            {
                picMain.ImageLocation = null;
                //lstSelImg.Select();
            }
        }

        private void lstImage_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstImage_MouseClick(sender, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
            //if(lstImage.Items.Count>0)
            //{
            //    if(lstImage.SelectedItems.Count>0)
            //    {
            //        string imagename = txtPath.Text + "\\" + lstImage.SelectedItems[0].Text + ".TIF";
            //        //picMain.Height = 647;
            //        //picMain.Width = 625;
            //        //picMain.Refresh();
            //        picMain.ImageLocation = null;
            //        picMain.Image = Image.FromFile(imagename);
            //        picMain.SizeMode = PictureBoxSizeMode.StretchImage;
            //        //picMain.Width = Convert.ToInt32(picMain.Width * 1);
            //        //picMain.Height = Convert.ToInt32(picMain.Height * 1);
            //        //picMain.Refresh();
            //    }
            //}
        }

        private void picMain_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                ZoomIn();
            if (e.Button == MouseButtons.Right)
                ZoomOut();
        }

        private void picMain_MouseEnter(object sender, EventArgs e)
        {
            if (picMain.Focused == false)
            {
                //picMain.Focus();
            }
        }

        private void picMain_MouseHover(object sender, EventArgs e)
        {

            // picMain.Focus();
            //lstImage.Select();
        }

        private void picMain_MouseMove(object sender, MouseEventArgs e)
        {
            MouseEventArgs mouse = e as MouseEventArgs;

            //if (mouse.Button == MouseButtons.Left)
            //{
            //    Point mousePosNow = mouse.Location;

            //    int deltaX = mousePosNow.X - mouseDown.X;
            //    int deltaY = mousePosNow.Y - mouseDown.Y;

            //    int newX = picMain.Location.X + deltaX;
            //    int newY = picMain.Location.Y + deltaY;

            //    picMain.Location = new Point(newX, newY);
            //}
            //lstImage.Select();
        }

        private void lstSelImg_MouseClick(object sender, MouseEventArgs e)
        {
            if (lstSelImg.Items.Count > 0)
            {
                if (lstSelImg.SelectedItems.Count > 0)
                {

                    lstSelImg.SelectedItems[0].Focused = true;
                    lstSelImg.SelectedItems[0].Selected = true;

                    string imgPath = txtPath.Text + "\\" + lstSelImg.SelectedItems[0].Text + ".TIF";
                    picMain.Height = 647;
                    picMain.Width = 625;
                    picMain.Refresh();
                    picMain.ImageLocation = null;

                    picMain.ImageLocation = imgPath;


                    Image newImage = Image.FromFile(imgPath);
                    picMain.Height = Convert.ToInt32(picMain.Height * 1.1);
                    picMain.Width = Convert.ToInt32(picMain.Height * newImage.Width / newImage.Height);

                    picMain.SizeMode = PictureBoxSizeMode.StretchImage;
                    //picMain.Width = Convert.ToInt32(picMain.Width * 1);
                    //picMain.Height = Convert.ToInt32(picMain.Height * 1);
                    picMain.Refresh();
                    newImage.Dispose();
                    // GC.Collect();
                }
                lstSelImg.Select();
            }
        }

        private void lstSelImg_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstSelImg_MouseClick(sender, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
        }

        private void lstTotalImage_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstTotalImage.Rows.Count > 0)
                {

                    string imgPath = txtPath.Text + "\\" + lstTotalImage.CurrentRow.Cells[0].Value.ToString() + ".TIF";
                    picMain.Height = 647;
                    picMain.Width = 625;
                    picMain.Refresh();
                    picMain.ImageLocation = null;

                    picMain.ImageLocation = imgPath;


                    Image newImage = Image.FromFile(imgPath);
                    //picMain.Height = Convert.ToInt32(picMain.Height * 1.1);
                    //picMain.Width = Convert.ToInt32(picMain.Height * newImage.Width / newImage.Height);

                    picMain.SizeMode = PictureBoxSizeMode.StretchImage;
                    //picMain.Width = Convert.ToInt32(picMain.Width * 1);
                    //picMain.Height = Convert.ToInt32(picMain.Height * 1);
                    //picMain.Refresh();
                    newImage.Dispose();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message.ToString());
                GC.Collect();
            }
        }

        private void lstTotalImage_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (lstTotalImage.Rows.Count > 0)
                {

                    string imgPath = txtPath.Text + "\\" + lstTotalImage.CurrentRow.Cells[0].Value.ToString() + ".TIF";
                    picMain.Height = 647;
                    picMain.Width = 625;
                    picMain.Refresh();
                    picMain.ImageLocation = null;

                    picMain.ImageLocation = imgPath;


                    Image newImage = Image.FromFile(imgPath);
                    //picMain.Height = Convert.ToInt32(picMain.Height * 1.1);
                    //picMain.Width = Convert.ToInt32(picMain.Height * newImage.Width / newImage.Height);

                    picMain.SizeMode = PictureBoxSizeMode.StretchImage;
                    //picMain.Width = Convert.ToInt32(picMain.Width * 1);
                    //picMain.Height = Convert.ToInt32(picMain.Height * 1);
                    //picMain.Refresh();
                    newImage.Dispose();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message.ToString());
                GC.Collect();
            }
        }

        private void lstTotalImage_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (lstTotalImage.Rows.Count > 0)
                {

                    string imgPath = txtPath.Text + "\\" + lstTotalImage.CurrentRow.Cells[0].Value.ToString() + ".TIF";
                    picMain.Height = 647;
                    picMain.Width = 625;
                    picMain.Refresh();
                    picMain.ImageLocation = null;

                    picMain.ImageLocation = imgPath;


                    Image newImage = Image.FromFile(imgPath);
                    //picMain.Height = Convert.ToInt32(picMain.Height * 1.1);
                    //picMain.Width = Convert.ToInt32(picMain.Height * newImage.Width / newImage.Height);

                    picMain.SizeMode = PictureBoxSizeMode.StretchImage;
                    //picMain.Width = Convert.ToInt32(picMain.Width * 1);
                    //picMain.Height = Convert.ToInt32(picMain.Height * 1);
                    //picMain.Refresh();
                    newImage.Dispose();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message.ToString());
                GC.Collect();
            }
        }
        public bool updateBatch()
        {
            bool ret = false;
            if (ret == false)
            {
                _UpdateBatch();

                ret = true;
            }
            return ret;
        }
        public bool _UpdateBatch()
        {
            bool retVal = false;
            string sql = string.Empty;


            sql = "UPDATE batch_master SET status = '2' WHERE proj_code = '" + cmbProject.SelectedValue.ToString() + "' AND batch_key = '" + cmbBatch.SelectedValue.ToString() + "'";
            System.Diagnostics.Debug.Print(sql);
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            if (cmd.ExecuteNonQuery() > 0)
            {
                retVal = true;
            }


            return retVal;
        }


        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            picMain.Image = null;
            picMain.Refresh();
            if (lstCheckDeed.Items.Count > 0)
            {
                //lstPolicy.Items[0].Selected = true;
                lstCheckDeed.Items[0].Selected = true;
                lstCheckDeed.Items[0].Checked = true;
                lstCheckDeed.Items[0].Focused = true;
                lstCheckDeed.Select();
                //GetIndexDetails(lstPolicy.Items[0].Text, cmbProject.SelectedValue.ToString(), cmbBatch.SelectedValue.ToString());
                //GetDeedVolume(lstPolicy.Items[0].Text);
            }
        }

        private void lstTotalImage_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstTotalImage_MouseClick(sender, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
        }

        private void lstSelectedImg_MouseClick(object sender, MouseEventArgs e)
        {
            if (lstSelectedImg.Items.Count > 0)
            {
                if (lstSelectedImg.SelectedItems.Count > 0)
                {



                    string imgPath = txtPath.Text + "\\" + lstSelectedImg.SelectedItems[0].Text + ".TIF";
                    picMain.Height = 647;
                    picMain.Width = 625;
                    picMain.Refresh();
                    picMain.ImageLocation = null;

                    picMain.ImageLocation = imgPath;


                    Image newImage = Image.FromFile(imgPath);
                    //picMain.Height = Convert.ToInt32(picMain.Height * 1.1);
                    //picMain.Width = Convert.ToInt32(picMain.Height * newImage.Width / newImage.Height);

                    picMain.SizeMode = PictureBoxSizeMode.StretchImage;
                    //picMain.Width = Convert.ToInt32(picMain.Width * 1);
                    //picMain.Height = Convert.ToInt32(picMain.Height * 1);
                    picMain.Refresh();
                    newImage.Dispose();

                    lstSelectedImg.SelectedItems[0].Focused = true;
                }
            }
        }

        private void lstCheckDeed_MouseClick(object sender, MouseEventArgs e)
        {
            lstSelectedImg.Items.Clear();
            for (int i = 0; i < lstTotalImage.Rows.Count; i++)
            {
                if (lstTotalImage.Rows[i].Cells[1].Value != null)
                {
                    if (lstTotalImage.Rows[i].Cells[1].Value.ToString() == lstCheckDeed.SelectedItems[0].Text)
                    {
                        lstSelectedImg.Items.Add(lstTotalImage.Rows[i].Cells[0].Value.ToString());
                    }
                }
            }
            //if (lstCheckDeed.Items.Count > 0)
            //{
            //    //lstPolicy.Items[0].Selected = true;
            //    lstCheckDeed.Items[0].Selected = true;
            //    //GetIndexDetails(lstPolicy.Items[0].Text, cmbProject.SelectedValue.ToString(), cmbBatch.SelectedValue.ToString());
            //    //GetDeedVolume(lstPolicy.Items[0].Text);
            //}
        }

        private void lstCheckDeed_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmdremove1_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < lstSelectedImg.Items.Count; i++)
                {
                    if (lstSelectedImg.Items[i].Selected == true)
                    {
                        for (int j = 0; j < lstTotalImage.Rows.Count - 1; j++)
                        {
                            if (lstTotalImage.Rows[j].Cells[0].Value != null)
                            {
                                if (lstTotalImage.Rows[j].Cells[0].Value.ToString() == lstSelectedImg.Items[i].Text)
                                {
                                    lstTotalImage.Rows[j].Cells[1].Value = null;
                                    lstTotalImage.Rows[j].DefaultCellStyle.BackColor = Color.Yellow;
                                }
                            }
                        }
                    }
                }
                foreach (ListViewItem eachItem in lstSelectedImg.SelectedItems)
                {
                    lstSelectedImg.Items.Remove(eachItem);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        public bool UpdateTransactionLog(eSTATES state, Credentials prmCrd, int pk, int bk, string box, string pol, OdbcTransaction sqlTrans)
        {
            string sqlStr = null;
            // OdbcTransaction sqlTrans = null;
            bool commitBol = true;
            OdbcCommand sqlCmd = new OdbcCommand();
            bool flagTrans = false;
            switch (state)
            {
                case eSTATES.POLICY_SCANNED:
                    {
                        sqlStr = @"insert into transaction_log (proj_key,Batch_Key,Box_number,Policy_number,Scanned_user,scanned_dttm,fqc_user)" +
                        " values(" + pk + "," + bk + ",'" + box + "','" + pol + "','" + prmCrd.created_by + "','" + prmCrd.created_dttm + "','')";
                        break;
                    }
                case eSTATES.POLICY_QC:
                    {
                        sqlStr = @"update transaction_log" +
                        " set QC_User='" + prmCrd.created_by + "',Qc_DTTM='" + prmCrd.created_dttm + "' where proj_key=" + pk +
                        " and batch_key=" + bk + " and box_number='" + box + "'" +
                        " and policy_number='" + pol + "'";
                        break;
                    }
                case eSTATES.POLICY_INDEXED:
                    {
                        sqlStr = @"update transaction_log" +
                        " set Index_User='" + prmCrd.created_by + "',Index_DTTM='" + prmCrd.created_dttm + "' where proj_key=" + pk +
                        " and batch_key=" + bk + " and box_number='" + box + "'" +
                        " and policy_number='" + pol + "'";
                        break;
                    }
                case eSTATES.POLICY_FQC:
                    {
                        sqlStr = @"update transaction_log" +
                        " set Fqc_User='" + prmCrd.created_by + "',fqc_DTTM='" + prmCrd.created_dttm + "' where proj_key=" + pk +
                        " and batch_key=" + bk + " and box_number='" + box + "'" +
                        " and policy_number='" + pol + "'";
                        break;
                    }
            }
            try
            {
                if (sqlTrans == null)
                {
                    sqlTrans = sqlCon.BeginTransaction();
                    flagTrans = true;
                }
                sqlCmd.Connection = sqlCon;
                sqlCmd.Transaction = sqlTrans;
                sqlCmd.CommandText = sqlStr;
                int i = sqlCmd.ExecuteNonQuery();
                if (flagTrans)
                {
                    sqlTrans.Commit();
                }
                commitBol = true;
            }
            catch (Exception ex)
            {
                commitBol = false;
                sqlTrans.Rollback();
                sqlCmd.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n" + "Wfe State--" + Convert.ToString(Convert.ToInt32(state)) + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);

            }
            return commitBol;
        }
        private void CmdFinalSave_Click(object sender, EventArgs e)
        {
            if (CmdFinalSave.Enabled == true)
            {
                OdbcTransaction sqlTrans = null;
                string path = string.Empty;
                string oldFilename = string.Empty;
                string newFilename = string.Empty;
                wfeProject wfep = new wfeProject(sqlCon);
                DirectoryInfo selectedPath = new DirectoryInfo(txtPath.Text);
                try
                {
                    int filecou = lstPolicy.Items.Count;
                    int totImgcou = lstImage.Items.Count;
                    if (filecou > 0)
                    {
                        MessageBox.Show("You have one or more files there... ", "Check Again !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tabControl1.SelectedIndex = 0;
                        lstPolicy.Select();
                        return;
                    }
                    //if (totImgcou > 0)
                    //{
                    //    DialogResult del = MessageBox.Show("There's one or more images left...Want to Delete?", "Check Again !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    //    if (del == DialogResult.Yes)
                    //    {
                    //        path = wfep.GetPath(Convert.ToInt32(cmbProject.SelectedValue.ToString())).Tables[0].Rows[0][0].ToString();

                    //        string delFolder = path + "\\" + cmbBatch.Text + "\\" + "Delete";
                    //        if (!Directory.Exists(delFolder))
                    //        {
                    //            Directory.CreateDirectory(delFolder);
                    //        }
                    //        else
                    //        {
                    //            Directory.Delete(delFolder);
                    //            Directory.CreateDirectory(delFolder);
                    //        }

                    //        List<string> fileNames1 = new List<string>();


                    //        for (int i = 0; i < lstImage.Items.Count - 1; i++)
                    //        {
                    //            string f = lstImage.Items[i].Text;
                    //            string newFilename1 = delFolder + "\\" + cmbBatch.Text + "_" + (i+1).ToString().PadLeft(3, '0') + ".TIF";
                    //            string imageName1 = cmbBatch.Text + "_" + (i+1).ToString().PadLeft(3, '0') + ".TIF";
                    //            File.Copy(txtPath.Text + "\\" + f + ".TIF",newFilename1,true);
                    //        }
                    //        foreach (ListViewItem eachItem in lstImage.Items)
                    //        {
                    //            lstImage.Items.Remove(eachItem);
                    //        }
                    //    }
                    //    else
                    //    {
                    //        return;
                    //    }

                    //}
                    DialogResult dr = MessageBox.Show(this, "Images are Ready to Import.Transaction Cannot be Rollback.Continue ?", "Importing Images", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (dr == DialogResult.Yes)
                    {
                        lstTotalImage.Enabled = false;
                        lstSelectedImg.Enabled = false;
                        lstCheckDeed.Enabled = false;
                        cmdadd1.Enabled = false;
                        cmdremove1.Enabled = false;
                        CmdFinalSave.Enabled = false;
                        //path = wfep.GetPath(Convert.ToInt32(cmbProject.SelectedValue.ToString()), sqlTrans).Tables[0].Rows[0][0].ToString();
                        path = wfep.GetPath(Convert.ToInt32(cmbProject.SelectedValue.ToString())).Tables[0].Rows[0][0].ToString();
                        sqlTrans = sqlCon.BeginTransaction();
                        if (lstCheckDeed.Items.Count > 0)
                        {


                            for (int i = 0; i < lstCheckDeed.Items.Count; i++)
                            {

                                //if (sqlTrans.Connection == null)
                                //{
                                //    sqlTrans = sqlCon.BeginTransaction();
                                //}
                                string scanFolder = path + "\\" + cmbBatch.Text + "\\" + lstCheckDeed.Items[i].Text + "\\Scan";
                                if (!Directory.Exists(scanFolder))
                                {
                                    Directory.CreateDirectory(scanFolder);
                                }

                                List<string> fileNames = new List<string>();
                                int sequence = 1;

                                for (int j = 0; j < lstTotalImage.Rows.Count; j++)
                                {
                                    if (lstTotalImage.Rows[j].Cells[1].Value != null)
                                    {
                                        if (lstCheckDeed.Items[i].Text == lstTotalImage.Rows[j].Cells[1].Value.ToString())
                                        {
                                            fileNames.Add(lstTotalImage.Rows[j].Cells[0].Value.ToString() + ".TIF");
                                            newFilename = scanFolder + "\\" + lstCheckDeed.Items[i].Text + "_" + sequence.ToString().PadLeft(5, '0') + ".TIF";
                                            string imageName = lstCheckDeed.Items[i].Text + "_" + sequence.ToString().PadLeft(5, '0') + ".TIF";
                                            File.Copy(txtPath.Text + "\\" + lstTotalImage.Rows[j].Cells[0].Value.ToString() + ".TIF", newFilename, true);
                                            //if (sqlTrans.Connection == null)
                                            //{
                                            //    sqlTrans = sqlCon.BeginTransaction();
                                            //}
                                            insertIntoDB(imageName, sequence, sqlTrans, lstCheckDeed.Items[i].Text);
                                            sequence = sequence + 1;

                                        }
                                    }

                                }
                                if (UpdateCaseFile(sqlTrans, lstCheckDeed.Items[i].Text))
                                {

                                }
                                //if (sqlTrans.Connection == null)
                                //{
                                //    sqlTrans = sqlCon.BeginTransaction();
                                //}
                                wfePolicy wfe = new wfePolicy(sqlCon, new CtrlPolicy(Convert.ToInt32(cmbProject.SelectedValue.ToString()), Convert.ToInt32(cmbBatch.SelectedValue.ToString()), "1", lstCheckDeed.Items[i].Text));
                                wfe.UpdateTransactionLog(eSTATES.POLICY_SCANNED, crd, sqlTrans);
                                //UpdateTransactionLog(eSTATES.POLICY_SCANNED, crd, Convert.ToInt32(cmbProject.SelectedValue.ToString()), Convert.ToInt32(cmbBatch.SelectedValue.ToString()), "1", lstCheckDeed.Items[i].Text, sqlTrans);
                            }
                        }
                        //if(sqlTrans.Connection == null)
                        //{
                        //    sqlTrans = sqlCon.BeginTransaction();
                        //}
                        sqlTrans.Commit();
                        bool updatebatch = updateBatch();
                        //bool updatemeta = updateMeta();
                        MessageBox.Show(this, "Images Successfully Imported", "Import Images", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    }
                    else
                    {
                        return;
                    }
                }
                catch (Exception ex)
                {
                    sqlTrans.Rollback();
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void cmdadd1_Click(object sender, EventArgs e)
        {
            int indexRow = 0;
            try
            {
                for (int j = 0; j < lstTotalImage.Rows.Count - 1; j++)
                {
                    if (lstCheckDeed.SelectedItems[0].Selected == true)
                    {
                        if (lstTotalImage.CurrentRow.Cells[1].Value == null)
                        {
                            lstTotalImage.CurrentRow.Cells[1].Value = lstCheckDeed.SelectedItems[0].Text;
                            lstTotalImage.SelectedRows[j].DefaultCellStyle.BackColor = Color.GreenYellow;
                            lstSelectedImg.Items.Add(lstTotalImage.CurrentRow.Cells[0].Value.ToString());
                        }

                    }
                }






            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int indexRow = 0;
            try
            {
                //for (int j = 0; j < lstTotalImage.Rows.Count - 1; j++)
                //{
                //    if (lstAddlPages.SelectedItems[0].Selected == true)
                //    {
                //        lstAddlallpages.CurrentRow.Cells[1].Value = lstAddlPages.SelectedItems[0].Text;

                //    }
                //}
                lstSelectedAddl.Items.Add(lstAddlallpages.CurrentRow.Cells[0].Value.ToString());
                lstAddlallpages.CurrentRow.DefaultCellStyle.BackColor = Color.Yellow;



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < lstSelectedAddl.Items.Count; i++)
                {
                    if (lstSelectedAddl.Items[i].Selected == true)
                    {
                        for (int j = 0; j < lstAddlallpages.Rows.Count - 1; j++)
                        {
                            if (lstAddlallpages.Rows[j].Cells[0].Value != null)
                            {
                                if (lstAddlallpages.Rows[j].Cells[0].Value.ToString() == lstSelectedAddl.Items[i].Text)
                                {
                                    lstAddlallpages.Rows[j].Cells[1].Value = null;
                                    lstAddlallpages.Rows[j].DefaultCellStyle.BackColor = Color.White;
                                }
                            }
                        }
                    }
                }
                foreach (ListViewItem eachItem in lstSelectedAddl.SelectedItems)
                {
                    lstSelectedAddl.Items.Remove(eachItem);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void cmdAddl_Click(object sender, EventArgs e)
        {
            try
            {
                lblinfo.Text = "";
                List<string> fileNames = new List<string>();
                List<string> tempPath = new System.Collections.Generic.List<string>(1000);
                fbdPathAddl.ShowDialog();
                txtAddlPages.Text = fbdPathAddl.SelectedPath;
                DirectoryInfo selectedPath = new DirectoryInfo(txtAddlPages.Text);
                if (Directory.Exists(txtAddlPages.Text + "\\Backup"))
                {

                }
                else
                {
                    Directory.CreateDirectory(txtAddlPages.Text + "\\Backup");
                    DirectoryInfo selectedPath1 = new DirectoryInfo(txtAddlPages.Text);
                    foreach (FileInfo file in selectedPath.GetFiles())
                    {
                        if (file.Extension == ".TIF" || file.Extension == ".tif")
                        {
                            file.CopyTo(txtAddlPages.Text + "\\Backup\\" + file.Name);
                        }
                    }
                }

                if (selectedPath.GetFiles().Length > 0)

                    foreach (FileInfo file in selectedPath.GetFiles())
                    {
                        if (file.Extension.Equals(".tif") || file.Extension.Equals(".TIF"))
                        {
                            fileNames.Add(file.FullName);
                            tempPath.Add(txtPath.Text + "\\" + file.ToString());
                        }
                    }


                foreach (string fileName in fileNames)
                {

                    lstAddlallpages.Rows.Add();
                    lstAddlallpages.Rows[posAdd].Cells[0].Value = System.IO.Path.GetFileNameWithoutExtension(fileName);

                    posAdd = posAdd + 1;

                }
                if (lstAddlPages.Items.Count > 0)
                {
                    lstAddlPages.Items[0].Selected = true;
                }
                groupBox2.Enabled = false;

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void lstPolicy_Click(object sender, EventArgs e)
        {
            if (lstPolicy.Items.Count > 0)
            {
                //lstPolicy.Items[0].Selected = true;
                GetIndexDetails(lstPolicy.SelectedItems[0].Text, cmbProject.SelectedValue.ToString(), cmbBatch.SelectedValue.ToString());
                //GetDeedVolume(lstPolicy.SelectedItems[0].Text);
            }
        }

        private void lstCheckDeed_Click(object sender, EventArgs e)
        {
            if (lstCheckDeed.Items.Count > 0)
            {
                //lstPolicy.Items[0].Selected = true;
                GetIndexDetails(lstCheckDeed.SelectedItems[0].Text, cmbProject.SelectedValue.ToString(), cmbBatch.SelectedValue.ToString());
                //GetDeedVolume(lstCheckDeed.SelectedItems[0].Text);
            }
        }

        private void cmdImportAddladd_Click(object sender, EventArgs e)
        {
            try
            {
                for (int j = 0; j < lstTotalImage.Rows.Count - 1; j++)
                {
                    if (lstAddlPages.SelectedItems[0].Selected == true)
                    {
                        lstAddlallpages.CurrentRow.Cells[1].Value = lstAddlPages.SelectedItems[0].Text;
                    }
                }

                lstSelectedAddl.Items.Clear();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void lstAddlPages_MouseClick(object sender, MouseEventArgs e)
        {
            lstSelectedAddl.Items.Clear();
            for (int i = 0; i < lstAddlallpages.Rows.Count - 1; i++)
            {
                if (lstAddlallpages.Rows[i].Cells[1].Value != null)
                {
                    if (lstAddlallpages.Rows[i].Cells[1].Value.ToString() == lstAddlPages.SelectedItems[0].Text)
                    {
                        lstSelectedAddl.Items.Add(lstAddlallpages.Rows[i].Cells[0].Value.ToString());
                    }
                }
            }
        }

        private void cmdaddlFinalImp_Click(object sender, EventArgs e)
        {
            OdbcTransaction sqlTrans = null;
            string path = string.Empty;
            string oldFilename = string.Empty;
            string newFilename = string.Empty;
            string qcFilename = string.Empty;
            wfeProject wfep = new wfeProject(sqlCon);
            DirectoryInfo selectedPath = new DirectoryInfo(txtAddlPages.Text);
            try
            {
                DialogResult dr = MessageBox.Show(this, "Ready to move Additional Images??,Transaction cannot be rolled back, You sure to continue?", "Importing Images", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.Yes)
                {
                    //lstTotalImage.Enabled = false;
                    //lstSelectedImg.Enabled = false;
                    //lstCheckDeed.Enabled = false;
                    //cmdadd1.Enabled = false;
                    //cmdremove1.Enabled = false;
                    //CmdFinalSave.Enabled = false;
                    path = wfep.GetPath(Convert.ToInt32(cmbProject.SelectedValue.ToString()), sqlTrans).Tables[0].Rows[0][0].ToString();
                    sqlTrans = sqlCon.BeginTransaction();
                    if (lstAddlPages.Items.Count > 0)
                    {

                        path = wfep.GetPath(Convert.ToInt32(cmbProject.SelectedValue.ToString())).Tables[0].Rows[0][0].ToString();
                        //string scanFolder = path + "\\" + cmbBatch.Text + "\\1\\" + lstCheckDeed.SelectedItems[0].Text + "\\Scan";
                        //if (Directory.Exists(path))
                        //{
                        //    Directory.CreateDirectory(scanFolder);
                        //}
                        //else
                        //{
                        //    Directory.CreateDirectory(scanFolder);
                        //}


                        for (int i = 0; i < lstAddlPages.Items.Count; i++)
                        {


                            string scanFolder = path + "\\" + cmbBatch.Text + "\\1\\" + lstAddlPages.Items[i].Text + "\\Scan";
                            string qcFolder = path + "\\" + cmbBatch.Text + "\\1\\" + lstAddlPages.Items[i].Text + "\\QC";
                            //if (!Directory.Exists(scanFolder))
                            //{
                            //    Directory.CreateDirectory(scanFolder);
                            //}

                            List<string> fileNames = new List<string>();
                            int sequence = Convert.ToInt32(wfep.GetMaxImageSerial(lstAddlPages.Items[i].Text.ToString()).Tables[0].Rows[0][0].ToString());
                            sequence = sequence + 1;
                            for (int j = 0; j < lstAddlallpages.Rows.Count - 1; j++)
                            {
                                if (lstAddlPages.Items[i].Text == lstAddlallpages.Rows[j].Cells[1].Value.ToString())
                                {
                                    fileNames.Add(lstTotalImage.Rows[j].Cells[0].Value.ToString() + ".TIF");
                                    newFilename = scanFolder + "\\" + lstAddlPages.Items[i].Text + "_" + sequence.ToString().PadLeft(5, '0') + "_A.TIF";
                                    string imageName = lstAddlPages.Items[i].Text + "_" + sequence.ToString().PadLeft(5, '0') + "_A.TIF";
                                    File.Copy(txtPath.Text + "\\" + lstAddlallpages.Rows[j].Cells[0].Value.ToString() + ".TIF", newFilename, true);
                                    insertIntoDB(imageName, sequence, sqlTrans, lstAddlPages.Items[i].Text);
                                    sequence = sequence + 1;

                                }

                            }
                        }
                    }

                    sqlTrans.Commit();
                    MessageBox.Show(this, "Images Successfully Imported", "Import Images", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                sqlTrans.Rollback();
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void lstPolicy_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox12_Enter(object sender, EventArgs e)
        {

        }
    }
}
