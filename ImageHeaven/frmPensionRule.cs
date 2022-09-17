using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NovaNet.wfe;
using NovaNet.Utils;
using System.Data.Odbc;
using System.Net;
using LItems;
using System.IO;
using System.Collections;
using nControls;
using DataLayerDefs;
using System.Text.RegularExpressions;

namespace ImageHeaven
{
    public partial class frmPensionRule : Form
    {
        public string name = frmMain.name;
        //OdbcConnection sqlCon = null;
        NovaNet.Utils.GetProfile pData;
        NovaNet.Utils.ChangePassword pCPwd;
        NovaNet.Utils.Profile p;
        public static NovaNet.Utils.IntrRBAC rbc;
        //public Credentials crd;
        static wItem wi;
        public static string projKey;
        public static string bundleKey;
        public static string batchNumber;
        public static string batchCode;
        public static string creationDate;
        public static string department;
        public static string subCategory;

        public static string caseStatus = null;
        public static string caseNature = null;
        public static string caseType = null;
        public static string caseYear = null;
        public static string casefile = null;
        public static bool isWith = false;

        public static string filename = null;
        public static string old_filename = null;

        public Credentials crd = new Credentials();
        private OdbcConnection sqlCon;
        OdbcTransaction txn;

        public static string currStage = string.Empty;

        public static DataLayerDefs.Mode _mode = DataLayerDefs.Mode._Edit;

        public static eSTATES[] state;
        //public delegate void OnAccept(DeedDetails retDeed);
        //OnAccept m_OnAccept;
        ////The method to be invoked when the user aborts all operations
        //public delegate void OnAbort();
        OdbcDataAdapter sqlAdap;

        public static NovaNet.Utils.exLog.Logger exMailLog = new NovaNet.Utils.exLog.emailLogger("./errLog.log", NovaNet.Utils.exLog.LogLevel.Dev, Constants._MAIL_TO, Constants._MAIL_FROM, Constants._SMTP);
        public static NovaNet.Utils.exLog.Logger exTxtLog = new NovaNet.Utils.exLog.txtLogger("./errLog.log", NovaNet.Utils.exLog.LogLevel.Dev);

        string iniPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Remove(0, 6) + "\\" + "IhConfiguration.ini";
        INIFile ini = new INIFile();

        public frmPensionRule()
        {
            InitializeComponent();
        }

        public frmPensionRule(string proj, string bundle, OdbcConnection pCon, Credentials pcrd, DataLayerDefs.Mode mode, eSTATES[] prmState)
        {
            InitializeComponent();

            projKey = proj;

            bundleKey = bundle;

            sqlCon = pCon;

            //txn = pTxn;

            crd = pcrd;

            _mode = mode;

            state = prmState;

            init();
        }

        public frmPensionRule(string proj, string bundle, OdbcConnection pCon, Credentials pcrd, DataLayerDefs.Mode mode, string file, string stage)
        {
            InitializeComponent();

            projKey = proj;

            bundleKey = bundle;

            sqlCon = pCon;

            //txn = pTxn;

            crd = pcrd;

            _mode = mode;

            old_filename = file;

            currStage = stage;

            init();
        }

        public void init()
        {
            deTextBox1.Text = _GetBundleDetails().Rows[0][0].ToString();
            deTextBox2.Text = _GetBundleDetails().Rows[0][1].ToString();
            deTextBox3.Text = _GetBundleDetails().Rows[0][2].ToString();
            deTextBox4.Text = _GetBundleDetails().Rows[0][3].ToString();
            deTextBox5.Text = _GetBundleDetails().Rows[0][4].ToString();
        }

        public DataTable _GetBundleDetails()
        {
            DataTable dt = new DataTable();
            string sql = "select distinct Batch_name ,batch_code,date_format(created_dttm,'%Y-%m-%d'),dept_name,category from batch_master where proj_code = '" + projKey + "' and batch_key = '" + bundleKey + "'";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        public DataTable _GetFileCaseDetailsIndividual(string proj, string bundle, string fileName)
        {
            DataTable dt = new DataTable();
            string sql = "select distinct proj_code, batch_Key,item_no,filename,department,subcat,state_name,emp_name,desg,fileid,date_format(birth_date,'%Y-%m-%d'),date_format(joining_date,'%Y-%m-%d'),date_format(death_date,'%Y-%m-%d')," +
                "date_format(retirement_date,'%Y-%m-%d'),psa_name,section,pension_file_no,ppo_fppo,gpo_dgpo,ppo_gpo_cpo,mobile,hrms_id,spouce,place_payment,rule_file,vol,subject,series,acc,subscriber_name," +
                "ledger_year,date_format(accept_date,'%Y-%m-%d'),fp_auth_no,date_format(fp_date,'%Y-%m-%d') from metadata_entry where proj_code = '" + proj + "' and batch_key = '" + bundle + "' and filename = '" + fileName + "' ";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon, txn);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        private void find_state_name()
        {
            if (File.Exists(iniPath) == true)
            {
                string stName = ini.ReadINI("STATE", "STATENAME", string.Empty, iniPath);

                if (stName.ToString().Trim() == null || stName.ToString().Trim() == "\0")
                {
                    MessageBox.Show("Office / state name is not set");
                    if (crd.role == ihConstants._ADMINISTRATOR_ROLE)
                    {
                        frmOfficeNameConfig frm = new frmOfficeNameConfig(crd);
                        frm.ShowDialog(this);
                    }
                    else
                    {
                        this.Close();
                    }

                }
                else
                {
                    deTextBox6.Text = stName.ToString();
                }
            }
        }
        private void frmPensionRule_Load(object sender, EventArgs e)
        {
            if (_mode == Mode._Add)
            {
               
                find_state_name();


                if (deTextBox6.Text == "")
                {
                    this.Close();
                }

                deTextBox7.Text = string.Empty;
                deTextBox8.Text = string.Empty;
                deTextBox9.Text = string.Empty;

                deTextBox7.Focus();
                deTextBox7.Select();

            }
            if(_mode == Mode._Edit)
            {
                string state = _GetFileCaseDetailsIndividual(projKey, bundleKey, old_filename).Rows[0][6].ToString();
                string fileno = _GetFileCaseDetailsIndividual(projKey, bundleKey, old_filename).Rows[0][24].ToString();
                string vol = _GetFileCaseDetailsIndividual(projKey, bundleKey, old_filename).Rows[0][25].ToString();
                string subject = _GetFileCaseDetailsIndividual(projKey, bundleKey, old_filename).Rows[0][26].ToString();

                deTextBox6.Text = state;
                deTextBox7.Text = fileno;
                deTextBox8.Text = vol;
                deTextBox9.Text = subject;

                deTextBox7.Focus();
                deTextBox7.Select();
            }
        }

        private void deButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to Exit ? ", "B'Zer - Confirmation !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                this.Close();
               
            }
            else
            {
                return;
            }
        }

        private void frmPensionRule_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult result = MessageBox.Show("Do you want to Exit ? ", "B'Zer - Confirmation !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    this.Close();
                    
                }
                else
                {
                    return;
                }
            }
        }

        private void deButton2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to Exit ? ", "B'Zer - Confirmation !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No)
            {

                return;
            }
            else
            {
                this.Close();
            }
        }

        private DataTable itemCount(string proj, string bundle)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();



            string sql = "select Count(*) from metadata_entry where  proj_code = '" + proj + "' and batch_key = '" + bundle + "'  ";

            OdbcDataAdapter odap = new OdbcDataAdapter(sql, sqlCon);
            odap.Fill(dt);



            return dt;
        }

        private bool checkFileNotExistsEdit(string file, string dept, string subCat)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            bool retval = false;

            string sql = "select filename,proj_code,batch_key from metadata_entry where filename = '" + file + "' and department = '" + dept + "' and subcat = '" + subCat + "'  ";

            OdbcDataAdapter odap = new OdbcDataAdapter(sql, sqlCon);
            odap.Fill(dt);


            if (dt.Rows.Count > 0)
            {
                DataTable dt1 = new DataTable();

                string sql1 = "select batch_code from batch_master where proj_code = '" + dt.Rows[0][1].ToString() + "' and batch_key = '" + dt.Rows[0][2].ToString() + "'  ";

                OdbcDataAdapter odap1 = new OdbcDataAdapter(sql1, sqlCon);
                odap1.Fill(dt1);

                MessageBox.Show("This pension rule file number already exists for batch - "+ dt1.Rows[0][0].ToString(), "B'Zer - Confirmation !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                retval = false;
                deTextBox7.Focus();
                return retval;
            }
            else
            {
                retval = true;
            }

            return retval;
        }
        private bool checkFileNotExists(string file,string dept, string subCat)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            bool retval = false;

            string sql = "select filename,proj_code,batch_key from metadata_entry where filename = '" + file + "' and department = '" + dept + "' and subcat = '" + subCat + "'  ";

            OdbcDataAdapter odap = new OdbcDataAdapter(sql, sqlCon);
            odap.Fill(dt);


            if (dt.Rows.Count > 0)
            {
                DataTable dt1 = new DataTable();

                string sql1 = "select batch_code from batch_master where proj_code = '" + dt.Rows[0][1].ToString() + "' and batch_key = '" + dt.Rows[0][2].ToString() + "'  ";

                OdbcDataAdapter odap1 = new OdbcDataAdapter(sql1, sqlCon);
                odap1.Fill(dt1);

                MessageBox.Show("This pension rule file number already exists for batch - "+ dt1.Rows[0][0].ToString(), "B'Zer - Confirmation !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                retval = false;
                deTextBox7.Focus();
                return retval;
            }
            else
            {
                retval = true;
            }

            return retval;
        }

        public bool validate()
        {
            bool retval = false;

            string currDate = DateTime.Now.ToString("yyyy-MM-dd");
            string curYear = DateTime.Now.ToString("yyyy");
            int curIntYear = Convert.ToInt32(curYear);

            //if (deTextBox1.Text == "" || deTextBox1.Text == null || String.IsNullOrEmpty(deTextBox1.Text) || String.IsNullOrWhiteSpace(deTextBox1.Text))
            //{
            //    retval = false;

            //    MessageBox.Show("You cannot leave Office / State field blank...", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    deTextBox1.Focus();
            //    return retval;
            //}
            if (deTextBox7.Text == "" || deTextBox7.Text == null || String.IsNullOrEmpty(deTextBox7.Text) || String.IsNullOrWhiteSpace(deTextBox7.Text))
            {
                retval = false;

                MessageBox.Show("You cannot leave file number field blank...", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                deTextBox7.Focus();
                return retval;
            }
            else
            {
                retval = true;
            }

            if (deTextBox8.Text == "" || deTextBox8.Text == null || String.IsNullOrEmpty(deTextBox8.Text) || String.IsNullOrWhiteSpace(deTextBox8.Text))
            {
                retval = false;

                MessageBox.Show("You cannot leave volume number field blank...", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                deTextBox8.Focus();
                return retval;
            }
            else
            {
                retval = true;
            }


            return retval;
        }

        private bool insertIntoMeta(int itemno, string dept, string subCat, string state_name, string filename, string file_no, string vol, string sub, OdbcTransaction trans)
        {
            bool commitBol = true;

            string sqlStr = string.Empty;

            OdbcCommand sqlCmd = new OdbcCommand();

            //OdbcTransaction sqlTrans = null;

            //filename = file_no;

            //int sl = _GetTotalCount();
            //int sl_no = sl + 1;

            //itemno = Convert.ToString(Convert.ToInt32(itemno) + 1);

            if (frmPensionRule.state[0] == eSTATES.METADATA_ENTRY)
            {
                sqlStr = @"insert into metadata_entry(proj_code,batch_key,item_no,department,subcat,state_name,filename,rule_file,vol,subject,created_by,created_dttm,status) values('" +
                        projKey + "','" + bundleKey + "','" + itemno +
                        "','" + dept + "','" + subCat + "','" + state_name + "','" + filename + "','" + file_no + "','" + vol + "','" + sub + "','" + crd.created_by + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',0)";
                //sqlCmd.Connection = sqlCon;
            }
            else
            {
                sqlStr = @"insert into metadata_entry(proj_code,batch_key,item_no,department,subcat,state_name,filename,rule_file,vol,subject,created_by,created_dttm,status) values('" +
                        projKey + "','" + bundleKey + "','" + itemno +
                        "','" + dept + "','" + subCat + "','" + state_name + "','" + filename + "','" + file_no + "','" + vol + "','" + sub + "','" + crd.created_by + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',5)";
                //sqlCmd.Connection = sqlCon;
            }
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

            return commitBol;
        }

        private string GetPolicyPath()
        {
            wfeBatch wBatch = new wfeBatch(sqlCon);
            string batchPath = GetPath(Convert.ToInt32(projKey), Convert.ToInt32(bundleKey));
            return batchPath;
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

            }
            if (projDs.Tables[0].Rows.Count > 0)
            {
                Path = projDs.Tables[0].Rows[0]["batch_path"].ToString();
            }
            else
                Path = string.Empty;

            return Path;
        }

        public bool updateMetaEdit(string dept, string subCat, string state_name, string file_no, string vol, string sub)
        {
            bool ret = false;
            if (ret == false)
            {
                _UpdateMetaEdit(projKey, bundleKey, old_filename, filename, dept, subCat, state_name, file_no, vol, sub);

                ret = true;
            }
            return ret;
        }
        public bool updateImageEdit()
        {
            bool ret = false;
            if (ret == false)
            {
                _UpdateImageEdit(projKey, bundleKey, old_filename, filename);

                ret = true;
            }
            return ret;
        }
        public bool updateTransLogEdit()
        {
            bool ret = false;
            if (ret == false)
            {
                _UpdateTransLogEdit(projKey, bundleKey, old_filename, filename);

                ret = true;
            }
            return ret;
        }
        public bool updateQaEdit()
        {
            bool ret = false;
            if (ret == false)
            {
                _UpdateQaEdit(projKey, bundleKey, old_filename, filename);

                ret = true;
            }
            return ret;
        }
        public bool updateCustExcEdit()
        {
            bool ret = false;
            if (ret == false)
            {
                _UpdateCustExcEdit(projKey, bundleKey, old_filename, filename);

                ret = true;
            }
            return ret;
        }

        public bool _UpdateTransLogEdit(string projKey, string bundleKey, string oldFileName, string fileName)
        {
            string sqlStr = null;

            OdbcCommand sqlCmd = new OdbcCommand();

            bool retVal = false;
            string sql = string.Empty;
            string remarks = string.Empty;


            sqlStr = "UPDATE transaction_log SET policy_number= '" + fileName + "' WHERE proj_key = '" + projKey + "' AND batch_key = '" + bundleKey + "' and policy_number = '" + oldFileName + "' ";

            System.Diagnostics.Debug.Print(sqlStr);
            OdbcCommand cmd = new OdbcCommand(sqlStr, sqlCon);
            //cmd.Connection = sqlCon;
            //cmd.CommandText = sqlStr;
            if (cmd.ExecuteNonQuery() >= 0)
            {
                retVal = true;
                //txn.Commit();
            }

            return retVal;
        }
        public bool _UpdateCustExcEdit(string projKey, string bundleKey, string oldFileName, string fileName)
        {
            string sqlStr = null;

            OdbcCommand sqlCmd = new OdbcCommand();

            bool retVal = false;
            string sql = string.Empty;
            string remarks = string.Empty;


            sqlStr = "UPDATE custom_exception SET policy_number= '" + fileName + "'," +
                "image_name = REPLACE(image_name,'" + oldFileName + "_" + "','" + fileName + "_" + "')," +
                "modified_by ='" + crd.created_by + "',modified_dttm = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE proj_key = '" + projKey + "' AND batch_key = '" + bundleKey + "' and policy_number = '" + oldFileName + "' ";

            System.Diagnostics.Debug.Print(sqlStr);
            OdbcCommand cmd = new OdbcCommand(sqlStr, sqlCon);
            //cmd.Connection = sqlCon;
            //cmd.CommandText = sqlStr;
            if (cmd.ExecuteNonQuery() >= 0)
            {
                retVal = true;
                //txn.Commit();
            }

            return retVal;
        }
        public bool _UpdateQaEdit(string projKey, string bundleKey, string oldFileName, string fileName)
        {
            string sqlStr = null;

            OdbcCommand sqlCmd = new OdbcCommand();

            bool retVal = false;
            string sql = string.Empty;
            string remarks = string.Empty;


            sqlStr = "UPDATE lic_qa_log SET policy_number= '" + fileName + "' WHERE proj_key = '" + projKey + "' AND batch_key = '" + bundleKey + "' and policy_number = '" + oldFileName + "' ";

            System.Diagnostics.Debug.Print(sqlStr);
            OdbcCommand cmd = new OdbcCommand(sqlStr, sqlCon);
            //cmd.Connection = sqlCon;
            //cmd.CommandText = sqlStr;
            if (cmd.ExecuteNonQuery() >= 0)
            {
                retVal = true;
                //txn.Commit();
            }

            return retVal;
        }
        public bool _UpdateMetaEdit(string projKey, string bundleKey, string oldFileName, string fileName,
            string dept, string subCat, string state_name, string file_no, string vol, string sub)
        {
            string sqlStr = null;

            OdbcCommand sqlCmd = new OdbcCommand();

            bool retVal = false;
            string sql = string.Empty;
            string remarks = string.Empty;


            sqlStr = "UPDATE metadata_entry SET filename = '" + fileName + "',department  = '" + dept + "',subcat = '" + subCat + "'," +
                "state_name = '" + state_name + "',rule_file='" + file_no + "',vol = '" + vol + "',subject= '" + sub + "'," +
                "modified_by ='" + crd.created_by + "',modified_dttm = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'  WHERE proj_code = '" + projKey + "' AND batch_key = '" + bundleKey + "' and filename = '" + oldFileName + "' ";

            System.Diagnostics.Debug.Print(sqlStr);
            OdbcCommand cmd = new OdbcCommand(sqlStr, sqlCon);
            //cmd.Connection = sqlCon;
            //cmd.CommandText = sqlStr;
            if (cmd.ExecuteNonQuery() >= 0)
            {
                retVal = true;
                //txn.Commit();
            }

            return retVal;
        }
        public bool _UpdateImageEdit(string projKey, string bundleKey, string oldFileName, string fileName)
        {
            string sqlStr = null;

            OdbcCommand sqlCmd = new OdbcCommand();

            bool retVal = false;
            string sql = string.Empty;
            string remarks = string.Empty;


            sqlStr = "UPDATE image_master SET policy_number= '" + fileName + "',page_index_name = REPLACE(page_index_name,'" + oldFileName + "_" + "','" + fileName + "_" + "'),page_name = REPLACE(page_name,'" + oldFileName + "_" + "','" + fileName + "_" + "'),modified_by ='" + crd.created_by + "',modified_dttm = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'  WHERE proj_key = '" + projKey + "' AND batch_key = '" + bundleKey + "' and policy_number = '" + oldFileName + "' ";

            System.Diagnostics.Debug.Print(sqlStr);
            OdbcCommand cmd = new OdbcCommand(sqlStr, sqlCon);
            //cmd.Connection = sqlCon;
            //cmd.CommandText = sqlStr;
            if (cmd.ExecuteNonQuery() >= 0)
            {
                retVal = true;
                //txn.Commit();
            }

            return retVal;
        }

        private void deButtonSave_Click(object sender, EventArgs e)
        {
            OdbcTransaction sqlTrans = null;
            if (sqlCon.State == ConnectionState.Closed || sqlCon.State == ConnectionState.Broken)
            {
                sqlCon.Open();
            }
            //txn = sqlCon.BeginTransaction();
            if (_mode == Mode._Add)
            {
                DialogResult result = MessageBox.Show("Do you want to save changes ? ", "B'Zer - Confirmation !", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    //if (deTextBox1.Text != "" && deTextBox2.Text != "" && deTextBox3.Text != "")
                    //{
                    if (validate() == true)
                    {
                        

                        string dept = deTextBox4.Text.Trim();
                        string subCat = deTextBox5.Text.Trim();

                        string filenumber = deTextBox7.Text.Trim();
                        string vol = deTextBox8.Text.Trim();
                        filename = filenumber + "_" + vol;

                        if (checkFileNotExists(filename,dept,subCat) == true)
                        {

                            

                            int item = Convert.ToInt32(itemCount(projKey, bundleKey).Rows[0][0].ToString()) + 1;

                            string statename = deTextBox6.Text.Trim();

                            string sub = deTextBox9.Text.Trim();

                            


                            bool insertCase = insertIntoMeta(item, dept, subCat, statename, filename, filenumber, vol, sub, sqlTrans);
                            if (insertCase == true)
                            {
                                if (sqlTrans == null)
                                {
                                    sqlTrans = sqlCon.BeginTransaction();
                                }
                                sqlTrans.Commit();
                                sqlTrans = null;
                                MessageBox.Show(this, "Record Saved Successfully...", "B'Zer ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                if (frmPensionRule.state[0] == eSTATES.METADATA_ENTRY)
                                {
                                    frmPensionRule_Load(sender, e);
                                }
                                else
                                {
                                    this.Close();
                                }
                            }
                            else
                            {

                                MessageBox.Show(this, "Ooops!!! There is an Error - Record not Saved...", "B'Zer - CAG", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                //this.Hide();

                                return;

                            }
                        }
                        else
                        {

                            return;
                        }
                    }
                    //}
                    //else
                    //{
                    //    //MessageBox.Show("You have to fill these fields", "Record Management", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    //deTextBox1.Focus();
                    //    //deTextBox1.Select();
                    //    return;
                    //}

                }
                else
                {

                    return;
                }

            }
            if (_mode == Mode._Edit)
            {
                if (currStage == "Entry")
                {
                    if (validate() == true)
                    {
                        string dept = deTextBox4.Text.Trim();
                        string subCat = deTextBox5.Text.Trim();

                        string filenumber = deTextBox7.Text.Trim();
                        string vol = deTextBox8.Text.Trim();
                        filename = filenumber + "_" + vol;

                        if (filename != Files.filename)
                        {
                            if (checkFileNotExistsEdit(filename,dept,subCat) == true)
                            {
                                casefile = filenumber + "_" + vol;

                                string statename = deTextBox6.Text.Trim();

                                string sub = deTextBox9.Text.Trim();


                                filename = casefile;


                                bool updateMeta = updateMetaEdit(dept, subCat, statename, filenumber, vol, sub);
                                bool updateimageMaster = updateImageEdit();
                                bool updatetransLog = updateTransLogEdit();
                                bool updatecusExc = updateCustExcEdit();
                                bool updateQa = updateQaEdit();


                                if (updateMeta == true && updateimageMaster == true && updatetransLog == true && updatecusExc == true && updateQa == true)
                                {
                                    //if (txn == null || txn.Connection == null)
                                    //{
                                    //    txn = sqlCon.BeginTransaction();
                                    //}
                                    //txn.Commit();
                                    //txn = null;

                                    string pathTemp = GetPolicyPath();

                                    string pathFinal = pathTemp + "\\" + old_filename;
                                    string pathDest = pathTemp + "\\" + filename;
                                    //Directory Rename
                                    if (old_filename != filename)
                                    {
                                        if (Directory.Exists(pathFinal))
                                        {

                                            Directory.Move(pathFinal, pathDest);

                                        }
                                    }

                                    //Scan folder check 
                                    string pathScan = pathTemp + "\\" + filename + "\\Scan";
                                    //Qc folder check 
                                    string pathQc = pathTemp + "\\" + filename + "\\QC";
                                    //Deleted
                                    string pathDeleted = pathScan + "\\" + ihConstants._DELETE_FOLDER;

                                    // Files Rename scan
                                    if (Directory.Exists(pathScan))
                                    {
                                        DirectoryInfo DirInfo = new DirectoryInfo(pathScan);
                                        FileInfo[] names = DirInfo.GetFiles();
                                        foreach (FileInfo f in names)
                                        {
                                            if (f.Name.Contains(old_filename + "_"))
                                            {
                                                string str1 = f.Name;

                                                string str2 = f.Name.Replace(old_filename + "_", filename + "_");

                                                File.Move(pathScan + "\\" + str1, pathScan + "\\" + str2);
                                            }

                                        }
                                    }

                                    //// Files Rename Qc
                                    if (Directory.Exists(pathQc))
                                    {
                                        DirectoryInfo DirInfo = new DirectoryInfo(pathQc);
                                        FileInfo[] names = DirInfo.GetFiles();
                                        foreach (FileInfo f in names)
                                        {
                                            if (f.Name.Contains(old_filename + "_"))
                                            {
                                                string str1 = f.Name;

                                                string str2 = f.Name.Replace(old_filename + "_", filename + "_");

                                                File.Move(pathQc + "\\" + str1, pathQc + "\\" + str2);
                                            }

                                        }
                                    }

                                    //// Files Rename deleted
                                    if (Directory.Exists(pathDeleted))
                                    {
                                        DirectoryInfo DirInfo = new DirectoryInfo(pathDeleted);
                                        FileInfo[] names = DirInfo.GetFiles();
                                        foreach (FileInfo f in names)
                                        {
                                            if (f.Name.Contains(old_filename + "_"))
                                            {
                                                string str1 = f.Name;

                                                string str2 = f.Name.Replace(old_filename + "_", filename + "_");

                                                File.Move(pathDeleted + "\\" + str1, pathDeleted + "\\" + str2);
                                            }

                                        }
                                    }

                                    MessageBox.Show(this, "Record Saved Successfully...", "Record Management", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    this.Close();

                                }
                                else
                                {

                                    MessageBox.Show(this, "Ooops!!! There is an Error - Record not Saved...", "Record Management", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                    return;

                                }
                            }
                        }
                        else
                        {
                            casefile = filenumber + "_" + vol;

                            string statename = deTextBox6.Text.Trim();

                            string sub = deTextBox9.Text.Trim();


                            filename = casefile;


                            bool updateMeta = updateMetaEdit(dept, subCat, statename, filenumber, vol, sub);
                            bool updateimageMaster = updateImageEdit();
                            bool updatetransLog = updateTransLogEdit();
                            bool updatecusExc = updateCustExcEdit();
                            bool updateQa = updateQaEdit();


                            if (updateMeta == true && updateimageMaster == true && updatetransLog == true && updatecusExc == true && updateQa == true)
                            {
                                //if (txn == null || txn.Connection == null)
                                //{
                                //    txn = sqlCon.BeginTransaction();
                                //}
                                //txn.Commit();
                                //txn = null;

                                string pathTemp = GetPolicyPath();

                                string pathFinal = pathTemp + "\\" + old_filename;
                                string pathDest = pathTemp + "\\" + filename;

                                //Directory Rename
                                if (old_filename != filename)
                                {
                                    if (Directory.Exists(pathFinal))
                                    {

                                        Directory.Move(pathFinal, pathDest);

                                    }
                                }


                                //Scan folder check 
                                string pathScan = pathTemp + "\\" + filename + "\\Scan";
                                //Qc folder check 
                                string pathQc = pathTemp + "\\" + filename + "\\QC";
                                //Deleted
                                string pathDeleted = pathScan + "\\" + ihConstants._DELETE_FOLDER;

                                // Files Rename scan
                                if (Directory.Exists(pathScan))
                                {
                                    DirectoryInfo DirInfo = new DirectoryInfo(pathScan);
                                    FileInfo[] names = DirInfo.GetFiles();
                                    foreach (FileInfo f in names)
                                    {
                                        if (f.Name.Contains(old_filename + "_"))
                                        {
                                            string str1 = f.Name;

                                            string str2 = f.Name.Replace(old_filename + "_", filename + "_");

                                            File.Move(pathScan + "\\" + str1, pathScan + "\\" + str2);
                                        }

                                    }
                                }

                                //// Files Rename Qc
                                if (Directory.Exists(pathQc))
                                {
                                    DirectoryInfo DirInfo = new DirectoryInfo(pathQc);
                                    FileInfo[] names = DirInfo.GetFiles();
                                    foreach (FileInfo f in names)
                                    {
                                        if (f.Name.Contains(old_filename + "_"))
                                        {
                                            string str1 = f.Name;

                                            string str2 = f.Name.Replace(old_filename + "_", filename + "_");

                                            File.Move(pathQc + "\\" + str1, pathQc + "\\" + str2);
                                        }

                                    }
                                }

                                //// Files Rename deleted
                                if (Directory.Exists(pathDeleted))
                                {
                                    DirectoryInfo DirInfo = new DirectoryInfo(pathDeleted);
                                    FileInfo[] names = DirInfo.GetFiles();
                                    foreach (FileInfo f in names)
                                    {
                                        if (f.Name.Contains(old_filename + "_"))
                                        {
                                            string str1 = f.Name;

                                            string str2 = f.Name.Replace(old_filename + "_", filename + "_");

                                            File.Move(pathDeleted + "\\" + str1, pathDeleted + "\\" + str2);
                                        }

                                    }
                                }

                                MessageBox.Show(this, "Record Saved Successfully...", "Record Management", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                this.Close();

                            }
                            else
                            {

                                MessageBox.Show(this, "Ooops!!! There is an Error - Record not Saved...", "B'Zer", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                return;

                            }
                        }
                    }
                }
                if (currStage == "FQC")
                {
                    if (validate() == true)
                    {
                        string dept = deTextBox4.Text.Trim();
                        string subCat = deTextBox5.Text.Trim();

                        string filenumber = deTextBox7.Text.Trim();
                        string vol = deTextBox8.Text.Trim();
                        filename = filenumber + "_" + vol;

                        if (filename != aeFQC.filename)
                        {
                            if (checkFileNotExistsEdit(filename,dept,subCat) == true)
                            {
                                casefile = filenumber + "_" + vol;

                                string statename = deTextBox6.Text.Trim();

                                string sub = deTextBox9.Text.Trim();


                                filename = casefile;


                                bool updateMeta = updateMetaEdit(dept, subCat, statename, filenumber, vol, sub);
                                bool updateimageMaster = updateImageEdit();
                                bool updatetransLog = updateTransLogEdit();
                                bool updatecusExc = updateCustExcEdit();
                                bool updateQa = updateQaEdit();


                                if (updateMeta == true && updateimageMaster == true && updatetransLog == true && updatecusExc == true && updateQa == true)
                                {
                                    //if (txn == null || txn.Connection == null)
                                    //{
                                    //    txn = sqlCon.BeginTransaction();
                                    //}
                                    //txn.Commit();
                                    //txn = null;

                                    string pathTemp = GetPolicyPath();

                                    string pathFinal = pathTemp + "\\" + old_filename;
                                    string pathDest = pathTemp + "\\" + filename;
                                    //Directory Rename
                                    if (old_filename != filename)
                                    {
                                        if (Directory.Exists(pathFinal))
                                        {

                                            Directory.Move(pathFinal, pathDest);

                                        }
                                    }

                                    //Scan folder check 
                                    string pathScan = pathTemp + "\\" + filename + "\\Scan";
                                    //Qc folder check 
                                    string pathQc = pathTemp + "\\" + filename + "\\QC";
                                    //Deleted
                                    string pathDeleted = pathScan + "\\" + ihConstants._DELETE_FOLDER;

                                    // Files Rename scan
                                    if (Directory.Exists(pathScan))
                                    {
                                        DirectoryInfo DirInfo = new DirectoryInfo(pathScan);
                                        FileInfo[] names = DirInfo.GetFiles();
                                        foreach (FileInfo f in names)
                                        {
                                            if (f.Name.Contains(old_filename + "_"))
                                            {
                                                string str1 = f.Name;

                                                string str2 = f.Name.Replace(old_filename + "_", filename + "_");

                                                File.Move(pathScan + "\\" + str1, pathScan + "\\" + str2);
                                            }

                                        }
                                    }

                                    //// Files Rename Qc
                                    if (Directory.Exists(pathQc))
                                    {
                                        DirectoryInfo DirInfo = new DirectoryInfo(pathQc);
                                        FileInfo[] names = DirInfo.GetFiles();
                                        foreach (FileInfo f in names)
                                        {
                                            if (f.Name.Contains(old_filename + "_"))
                                            {
                                                string str1 = f.Name;

                                                string str2 = f.Name.Replace(old_filename + "_", filename + "_");

                                                File.Move(pathQc + "\\" + str1, pathQc + "\\" + str2);
                                            }

                                        }
                                    }

                                    //// Files Rename deleted
                                    if (Directory.Exists(pathDeleted))
                                    {
                                        DirectoryInfo DirInfo = new DirectoryInfo(pathDeleted);
                                        FileInfo[] names = DirInfo.GetFiles();
                                        foreach (FileInfo f in names)
                                        {
                                            if (f.Name.Contains(old_filename + "_"))
                                            {
                                                string str1 = f.Name;

                                                string str2 = f.Name.Replace(old_filename + "_", filename + "_");

                                                File.Move(pathDeleted + "\\" + str1, pathDeleted + "\\" + str2);
                                            }

                                        }
                                    }

                                    MessageBox.Show(this, "Record Saved Successfully...", "Record Management", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    this.Close();

                                }
                                else
                                {

                                    MessageBox.Show(this, "Ooops!!! There is an Error - Record not Saved...", "Record Management", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                    return;

                                }
                            }
                        }
                        else
                        {
                            casefile = filenumber + "_" + vol;

                            string statename = deTextBox6.Text.Trim();

                            string sub = deTextBox9.Text.Trim();


                            filename = casefile;


                            bool updateMeta = updateMetaEdit(dept, subCat, statename, filenumber, vol, sub);
                            bool updateimageMaster = updateImageEdit();
                            bool updatetransLog = updateTransLogEdit();
                            bool updatecusExc = updateCustExcEdit();
                            bool updateQa = updateQaEdit();


                            if (updateMeta == true && updateimageMaster == true && updatetransLog == true && updatecusExc == true && updateQa == true)
                            {
                                //if (txn == null || txn.Connection == null)
                                //{
                                //    txn = sqlCon.BeginTransaction();
                                //}
                                //txn.Commit();
                                //txn = null;

                                string pathTemp = GetPolicyPath();

                                string pathFinal = pathTemp + "\\" + old_filename;
                                string pathDest = pathTemp + "\\" + filename;

                                //Directory Rename
                                if (old_filename != filename)
                                {
                                    if (Directory.Exists(pathFinal))
                                    {

                                        Directory.Move(pathFinal, pathDest);

                                    }
                                }


                                //Scan folder check 
                                string pathScan = pathTemp + "\\" + filename + "\\Scan";
                                //Qc folder check 
                                string pathQc = pathTemp + "\\" + filename + "\\QC";
                                //Deleted
                                string pathDeleted = pathScan + "\\" + ihConstants._DELETE_FOLDER;

                                // Files Rename scan
                                if (Directory.Exists(pathScan))
                                {
                                    DirectoryInfo DirInfo = new DirectoryInfo(pathScan);
                                    FileInfo[] names = DirInfo.GetFiles();
                                    foreach (FileInfo f in names)
                                    {
                                        if (f.Name.Contains(old_filename + "_"))
                                        {
                                            string str1 = f.Name;

                                            string str2 = f.Name.Replace(old_filename + "_", filename + "_");

                                            File.Move(pathScan + "\\" + str1, pathScan + "\\" + str2);
                                        }

                                    }
                                }

                                //// Files Rename Qc
                                if (Directory.Exists(pathQc))
                                {
                                    DirectoryInfo DirInfo = new DirectoryInfo(pathQc);
                                    FileInfo[] names = DirInfo.GetFiles();
                                    foreach (FileInfo f in names)
                                    {
                                        if (f.Name.Contains(old_filename + "_"))
                                        {
                                            string str1 = f.Name;

                                            string str2 = f.Name.Replace(old_filename + "_", filename + "_");

                                            File.Move(pathQc + "\\" + str1, pathQc + "\\" + str2);
                                        }

                                    }
                                }

                                //// Files Rename deleted
                                if (Directory.Exists(pathDeleted))
                                {
                                    DirectoryInfo DirInfo = new DirectoryInfo(pathDeleted);
                                    FileInfo[] names = DirInfo.GetFiles();
                                    foreach (FileInfo f in names)
                                    {
                                        if (f.Name.Contains(old_filename + "_"))
                                        {
                                            string str1 = f.Name;

                                            string str2 = f.Name.Replace(old_filename + "_", filename + "_");

                                            File.Move(pathDeleted + "\\" + str1, pathDeleted + "\\" + str2);
                                        }

                                    }
                                }

                                MessageBox.Show(this, "Record Saved Successfully...", "Record Management", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                this.Close();

                            }
                            else
                            {

                                MessageBox.Show(this, "Ooops!!! There is an Error - Record not Saved...", "B'Zer", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                return;

                            }
                        }
                    }
                }
            }
        }

        private void deTextBox8_Leave(object sender, EventArgs e)
        {
            if (_mode == Mode._Add)
            {
                string filenumber = deTextBox7.Text.Trim();
                string vol = deTextBox8.Text.Trim();
                string filename = filenumber + "_" + vol;

                checkFileNotExists(filename, deTextBox4.Text.Trim(), deTextBox5.Text.Trim());
            }
            if (_mode == Mode._Edit)
            {
                if (currStage == "Entry")
                {
                    string filenumber = deTextBox7.Text.Trim();
                    string vol = deTextBox8.Text.Trim();
                    string filename = filenumber + "_" + vol;

                    if (deTextBox7.Text.Trim() + "_" + deTextBox8.Text.Trim() != Files.filename)
                    {
                        checkFileNotExistsEdit(filename, deTextBox4.Text.Trim(), deTextBox5.Text.Trim());
                    }
                }
                if (currStage == "FQC")
                {
                    string filenumber = deTextBox7.Text.Trim();
                    string vol = deTextBox8.Text.Trim();
                    string filename = filenumber + "_" + vol;

                    if (deTextBox7.Text.Trim() + "_" + deTextBox8.Text.Trim() != aeFQC.filename)
                    {
                        checkFileNotExistsEdit(filename, deTextBox4.Text.Trim(), deTextBox5.Text.Trim());
                    }
                }
            }
        }

        private void deTextBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Regex.IsMatch(e.KeyChar.ToString(), @"^[a-zA-Z0-9\s\b\cC\cX\cV\cA]*$")) || (e.KeyChar.ToString() == "-") || (e.KeyChar.ToString() == "_"))
            {
               
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

        private void deTextBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Regex.IsMatch(e.KeyChar.ToString(), @"^[a-zA-Z0-9\s\b\cC\cX\cV\cA]*$")) || (e.KeyChar.ToString() == "-") || (e.KeyChar.ToString() == "_"))
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

        private void deTextBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Regex.IsMatch(e.KeyChar.ToString(), @"^[a-zA-Z0-9\s\b\cC\cX\cV\cA]*$")) || (e.KeyChar.ToString() == "-") || (e.KeyChar.ToString() == "_"))
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }
    }
}
