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
    public partial class frmNewCase : Form
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

        public frmNewCase()
        {
            InitializeComponent();
        }

        public frmNewCase(string proj, string bundle, OdbcConnection pCon, Credentials pcrd, DataLayerDefs.Mode mode)
        {
            InitializeComponent();

            projKey = proj;

            bundleKey = bundle;

            sqlCon = pCon;

            //txn = pTxn;

            crd = pcrd;

            _mode = mode;

            init();
        }

        public frmNewCase(string proj, string bundle, OdbcConnection pCon, Credentials pcrd, DataLayerDefs.Mode mode, eSTATES[] prmState)
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

        public frmNewCase(string proj, string bundle, OdbcConnection pCon, Credentials pcrd, DataLayerDefs.Mode mode, string file,string stage)
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
            
            textBox2.Text = _GetBundleDetails().Rows[0][0].ToString();
            textBox3.Text = _GetBundleDetails().Rows[0][1].ToString();
           
            txtCreateDate.Text = _GetBundleDetails().Rows[0][2].ToString();
      
        }

        public DataTable _GetBundleDetails()
        {
            DataTable dt = new DataTable();
            string sql = "select distinct Batch_name ,batch_code,date_format(created_dttm,'%Y-%m-%d') from batch_master where proj_code = '" + projKey + "' and batch_key = '" + bundleKey + "'";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        private void frmNewCase_KeyUp(object sender, KeyEventArgs e)
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

        private void frmNewCase_Load(object sender, EventArgs e)
        {
            if (_mode == DataLayerDefs.Mode._Add)
            {


                deTextBox1.Text = string.Empty;
                deTextBox2.Text = string.Empty;
                deTextBox3.Text = string.Empty;
                deTextBox4.Text = string.Empty;
                

                deTextBox9.Text = string.Empty;
                deTextBox10.Text = string.Empty;
                deTextBox11.Text = string.Empty;

                deTextBox5.Text = string.Empty;
                deTextBox6.Text = string.Empty;
                deTextBox12.Text = string.Empty;

                deTextBox7.Text = string.Empty;
                deTextBox8.Text = string.Empty;

                deTextBox1.Focus();
                deTextBox1.Select();



            }
            if (_mode == Mode._Edit)
            {
                

                deTextBox1.Text = _GetFileCaseDetailsIndividual(projKey, bundleKey, old_filename).Rows[0][4].ToString();
                deTextBox2.Text = _GetFileCaseDetailsIndividual(projKey, bundleKey, old_filename).Rows[0][5].ToString();
                deTextBox3.Text = _GetFileCaseDetailsIndividual(projKey, bundleKey, old_filename).Rows[0][3].ToString();
                deTextBox4.Text = _GetFileCaseDetailsIndividual(projKey, bundleKey, old_filename).Rows[0][6].ToString();

                string jdate = _GetFileCaseDetailsIndividual(projKey, bundleKey, old_filename).Rows[0][7].ToString();
                if(jdate != "")
                {
                    deTextBox9.Text = jdate.Substring(8, 2);
                    deTextBox10.Text = jdate.Substring(5,2);
                    deTextBox11.Text = jdate.Substring(0, 4);
                }
                string rdate = _GetFileCaseDetailsIndividual(projKey, bundleKey, old_filename).Rows[0][8].ToString();
                if(rdate != "")
                {
                    deTextBox5.Text = rdate.Substring(8, 2);
                    deTextBox6.Text = rdate.Substring(5, 2);
                    deTextBox12.Text = rdate.Substring(0, 4);
                }

                deTextBox7.Text = _GetFileCaseDetailsIndividual(projKey, bundleKey, old_filename).Rows[0][9].ToString();
                deTextBox8.Text = _GetFileCaseDetailsIndividual(projKey, bundleKey, old_filename).Rows[0][10].ToString();

                deTextBox1.Focus();
                deTextBox1.Select();

                
            }
        }
        public DataTable _GetFileCaseDetailsIndividual(string proj, string bundle, string fileName)
        {
            DataTable dt = new DataTable();
            string sql = "select distinct proj_code, batch_Key,item_no,file_no,state_name, emp_name, ppo_rppo_fppo, joining_date,retirement_date,department,acc from metadata_entry where proj_code = '" + proj + "' and batch_key = '" + bundle + "' and file_no = '" + fileName + "' ";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon, txn);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
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
        public bool validate()
        {
            bool retval = false;

            string currDate = DateTime.Now.ToString("yyyy-MM-dd");
            string curYear = DateTime.Now.ToString("yyyy");
            int curIntYear = Convert.ToInt32(curYear);

            if (deTextBox1.Text == "" || deTextBox1.Text == null || String.IsNullOrEmpty(deTextBox1.Text) || String.IsNullOrWhiteSpace(deTextBox1.Text))
            {
                retval = false;
                
                MessageBox.Show("You cannot leave Office / State field blank...", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                deTextBox1.Focus();
                return retval;
            }
            if (deTextBox2.Text == "" || deTextBox2.Text == null || String.IsNullOrEmpty(deTextBox2.Text) || String.IsNullOrWhiteSpace(deTextBox2.Text))
            {
                retval = false;

                MessageBox.Show("You cannot leave employee name field blank...", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                deTextBox2.Focus();
                return retval;
            }
            if (deTextBox3.Text == "" || deTextBox3.Text == null || String.IsNullOrEmpty(deTextBox3.Text) || String.IsNullOrWhiteSpace(deTextBox3.Text))
            {
                retval = false;

                MessageBox.Show("You cannot leave file number field blank...", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                deTextBox3.Focus();
                return retval;
            }
            if (deTextBox9.Text != "" || deTextBox10.Text != "" || deTextBox11.Text != "")
            {
                if (deTextBox11.Text != "")
                {

                    bool res = System.Text.RegularExpressions.Regex.IsMatch(deTextBox11.Text, "[^0-9]");
                    if (res != true && Convert.ToInt32(deTextBox11.Text) <= curIntYear && deTextBox11.Text.Length == 4 && deTextBox11.Text.Substring(0, 1) != "0")
                    {
                        retval = true;
                    }
                    else
                    {
                        retval = false;
                        MessageBox.Show(this, "Please input Valid Year...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        deTextBox11.Focus();
                        return retval;
                    }
                }

                if (deTextBox10.Text != "")
                {

                    bool res1 = System.Text.RegularExpressions.Regex.IsMatch(deTextBox10.Text, "[^0-9]");

                    if (res1 != true && deTextBox10.Text.Length == 2 && Convert.ToInt32(deTextBox10.Text) <= 12 && Convert.ToInt32(deTextBox10.Text) != 0 && deTextBox10.Text != "00")
                    {
                        retval = true;

                    }
                    else
                    {
                        retval = false;
                        MessageBox.Show(this, "Please input Valid Month...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        deTextBox10.Focus();
                        return retval;
                    }
                }
                else
                {
                    retval = false;
                    MessageBox.Show(this, "Please input Valid Month...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    deTextBox10.Focus();
                    return retval;
                }
                if (deTextBox9.Text != "")
                {

                    bool res2 = System.Text.RegularExpressions.Regex.IsMatch(deTextBox9.Text, "[^0-9]");
                    if (res2 != true && deTextBox9.Text.Length == 2 && Convert.ToInt32(deTextBox9.Text) <= 31 && Convert.ToInt32(deTextBox9.Text) != 0 && deTextBox9.Text != "00")
                    {
                        retval = true;

                    }
                    else
                    {
                        retval = false;
                        MessageBox.Show(this, "Please input Valid Date...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        deTextBox9.Focus();
                        return retval;
                    }
                }
                else
                {
                    retval = false;
                    MessageBox.Show(this, "Please input Valid Date...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    deTextBox9.Focus();
                    return retval;
                }

                DateTime temp;
                string isDate = deTextBox11.Text + "-" + deTextBox10.Text + "-" + deTextBox9.Text;
                if (DateTime.TryParse(isDate, out temp) && DateTime.Parse(isDate) <= DateTime.Parse(currDate))
                {
                    retval = true;
                }
                else
                {
                    retval = false;
                    MessageBox.Show(this, "Please select a valid date", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    deTextBox9.Select();
                    return retval;

                }
            }
            else
            {
                retval = true;
            }

            if (deTextBox5.Text != "" || deTextBox6.Text != "" || deTextBox12.Text != "")
            {
                if (deTextBox12.Text != "")
                {

                    bool res = System.Text.RegularExpressions.Regex.IsMatch(deTextBox12.Text, "[^0-9]");
                    if (res != true && Convert.ToInt32(deTextBox12.Text) <= curIntYear && deTextBox12.Text.Length == 4 && deTextBox12.Text.Substring(0, 1) != "0")
                    {
                        //retval = true;
                        retval = true;
                    }
                    else
                    {
                        retval = false;
                        MessageBox.Show(this, "Please input Valid Year...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        deTextBox12.Focus();
                        return retval;
                    }
                }

                if (deTextBox6.Text != "")
                {

                    bool res1 = System.Text.RegularExpressions.Regex.IsMatch(deTextBox6.Text, "[^0-9]");

                    if (res1 != true && deTextBox6.Text.Length == 2 && Convert.ToInt32(deTextBox6.Text) <= 12 && Convert.ToInt32(deTextBox6.Text) != 0 && deTextBox6.Text != "00")
                    {
                        //retval = true;
                        retval = true;
                    }
                    else
                    {
                        retval = false;
                        MessageBox.Show(this, "Please input Valid Month...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        deTextBox6.Focus();
                        return retval;
                    }
                }
                else
                {
                    retval = false;
                    MessageBox.Show(this, "Please input Valid Month...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    deTextBox6.Focus();
                    return retval;
                }

                if (deTextBox5.Text != "")
                {

                    bool res2 = System.Text.RegularExpressions.Regex.IsMatch(deTextBox5.Text, "[^0-9]");
                    if (res2 != true && deTextBox5.Text.Length == 2 && Convert.ToInt32(deTextBox5.Text) <= 31 && Convert.ToInt32(deTextBox5.Text) != 0 && deTextBox5.Text != "00")
                    {
                        //retval = true;
                        retval = true;
                    }
                    else
                    {
                        retval = false;
                        MessageBox.Show(this, "Please input Valid Date...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        deTextBox5.Focus();
                        return retval;
                    }
                }
                else
                {
                    retval = false;
                    MessageBox.Show(this, "Please input Valid Date...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    deTextBox5.Focus();
                    return retval;
                }

                DateTime temp;
                string isDate = deTextBox12.Text + "-" + deTextBox6.Text + "-" + deTextBox5.Text;
                if (DateTime.TryParse(isDate, out temp) && DateTime.Parse(isDate) <= DateTime.Parse(currDate))
                {
                    retval = true;
                }
                else
                {
                    retval = false;
                    MessageBox.Show(this, "Please select a valid date", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    deTextBox5.Select();
                    return retval;

                }
            }
            else
            {
                retval = true;
            }

           



            return retval;
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
        private bool checkFileNotExists(string proj, string bundle, string file)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            bool retval = false;

            string sql = "select file_no from metadata_entry where file_no = '" + file + "' and proj_code = '"+proj+"' and batch_key = '"+bundle+"'  ";

            OdbcDataAdapter odap = new OdbcDataAdapter(sql, sqlCon);
            odap.Fill(dt);


            if (dt.Rows.Count > 0)
            {


                MessageBox.Show("This file number already exists for this batch", "B'Zer - Confirmation !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                retval = false;
                deTextBox3.Focus();
                return retval;
            }
            else
            {
                retval = true;
            }

            return retval;
        }
        private bool insertIntoMeta(int itemno, string state_name, string emp_name,string file_no, string ppono, string jdate, string rdate, string dept, string acc, OdbcTransaction trans)
        {
            bool commitBol = true;

            string sqlStr = string.Empty;

            OdbcCommand sqlCmd = new OdbcCommand();

            //OdbcTransaction sqlTrans = null;

            filename = file_no;

            //int sl = _GetTotalCount();
            //int sl_no = sl + 1;

            //itemno = Convert.ToString(Convert.ToInt32(itemno) + 1);

            if (frmNewCase.state[0] == eSTATES.METADATA_ENTRY)
            {
                sqlStr = @"insert into metadata_entry(proj_code,batch_key,item_no,state_name,emp_name,file_no,ppo_rppo_fppo,joining_date,retirement_date,department,acc,created_by,created_dttm,status) values('" +
                        projKey + "','" + bundleKey + "','" + itemno +
                        "','" + state_name + "','" + emp_name + "','" + file_no + "','" + ppono + "','" + jdate + "','" + rdate + "','" + dept + "','"+acc+"','" + crd.created_by + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',0)";
                //sqlCmd.Connection = sqlCon;
            }
            else
            {
                sqlStr = @"insert into metadata_entry(proj_code,batch_key,item_no,state_name,emp_name,file_no,ppo_rppo_fppo,joining_date,retirement_date,department,acc,created_by,created_dttm,status) values('" +
                       projKey + "','" + bundleKey + "','" + itemno +
                       "','" + state_name + "','" + emp_name + "','" + file_no + "','" + ppono + "','" + jdate + "','" + rdate + "','" + dept + "','" + acc + "','" + crd.created_by + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',5)";
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
        public bool updateBundle()
        {
            bool ret = false;
            if (ret == false)
            {
                _UpdateBundle();

                ret = true;
            }
            return ret;
        }

        public bool _UpdateBundle()
        {
            bool retVal = false;
            string sql = string.Empty;
            string sqlStr = null;

            OdbcCommand sqlCmd = new OdbcCommand();


            sqlStr = "UPDATE batch_master SET status = '1' WHERE proj_code = '" + projKey + "' AND batch_key = '" + bundleKey + "'";
            System.Diagnostics.Debug.Print(sqlStr);
            OdbcCommand cmd = new OdbcCommand(sqlStr, sqlCon);


            if (cmd.ExecuteNonQuery() > 0)
            {
                retVal = true;
            }


            return retVal;
        }

        public bool updateCaseFile()
        {
            bool ret = false;
            if (ret == false)
            {
                _UpdateCaseFile();

                ret = true;
            }
            return ret;
        }
        public bool _UpdateCaseFile()
        {
            string sqlStr = null;

            OdbcCommand sqlCmd = new OdbcCommand();

            bool retVal = false;
            string sql = string.Empty;


            sqlStr = "UPDATE metadata_entry SET status = '1' WHERE proj_code = '" + projKey + "' AND batch_key = '" + bundleKey + "'";
            System.Diagnostics.Debug.Print(sqlStr);
            OdbcCommand cmd = new OdbcCommand(sqlStr, sqlCon);
            if (cmd.ExecuteNonQuery() > 0)
            {
                retVal = true;
            }


            return retVal;
        }
        private bool checkFileNotExistsEdit(string proj, string bundle, string file)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            bool retval = false;

            string sql = "select file_no from metadata_entry where file_no = '" + file + "' and proj_code = '"+proj+"' and batch_key = '"+bundle+"'  ";

            OdbcDataAdapter odap = new OdbcDataAdapter(sql, sqlCon);
            odap.Fill(dt);


            if (dt.Rows.Count > 0)
            {


                MessageBox.Show("This file number already exists for this batch", "B'Zer - Confirmation !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                retval = false;
                deTextBox3.Focus();
                return retval;
            }
            else
            {
                retval = true;
            }

            return retval;
        }
        public bool updateMetaEdit(string st,string emp,string ppo,string jd,string rd,string dep,string ac)
        {
            bool ret = false;
            if (ret == false)
            {
                _UpdateMetaEdit(projKey, bundleKey, old_filename, filename,st,emp,ppo,jd,rd,dep,ac);

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


            sqlStr = "UPDATE custom_exception SET policy_number= '" + fileName + "',modified_by ='" + crd.created_by + "',modified_dttm = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE proj_key = '" + projKey + "' AND batch_key = '" + bundleKey + "' and policy_number = '" + oldFileName + "' ";

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


            sqlStr = "UPDATE lic_qa_log SET policy_number= '" + fileName + "',modified_by ='" + crd.created_by + "',modified_dttm = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE proj_key = '" + projKey + "' AND batch_key = '" + bundleKey + "' and policy_number = '" + oldFileName + "' ";

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
        public bool _UpdateMetaEdit(string projKey, string bundleKey, string oldFileName, string fileName, string st,string emp,string ppo,string jd,string rd, string dep, string acc)
        {
            string sqlStr = null;

            OdbcCommand sqlCmd = new OdbcCommand();

            bool retVal = false;
            string sql = string.Empty;
            string remarks = string.Empty;


            sqlStr = "UPDATE metadata_entry SET file_no = '" + fileName + "',state_name  = '" + st + "',emp_name = '" + emp + "',ppo_rppo_fppo = '" + ppo + "',joining_date = '" + jd + "',retirement_date= '" + rd + "',department='"+dep+"',acc='"+acc+"',modified_by ='" + crd.created_by + "',modified_dttm = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'  WHERE proj_code = '" + projKey + "' AND batch_key = '" + bundleKey + "' and file_no = '" + oldFileName + "' ";

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


            sqlStr = "UPDATE image_master SET policy_number= '" + fileName + "',page_index_name = REPLACE(page_index_name,'" + oldFileName + "','" + fileName + "'),page_name = REPLACE(page_name,'" + oldFileName + "','" + fileName + "'),modified_by ='" + crd.created_by + "',modified_dttm = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'  WHERE proj_key = '" + projKey + "' AND batch_key = '" + bundleKey + "' and policy_number = '" + oldFileName + "' ";

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
                            string joinDate = string.Empty;
                            string rDate = string.Empty;

                            if (checkFileNotExists(projKey, bundleKey, deTextBox3.Text.Trim()) == true)
                            {

                                int item = Convert.ToInt32(itemCount(projKey, bundleKey).Rows[0][0].ToString()) + 1;

                                string statename = deTextBox1.Text.Trim();
                                string emp_name = deTextBox2.Text.Trim();
                                string filenumber = deTextBox3.Text.Trim();
                                string ppo = deTextBox4.Text.Trim();

                                

                                if(deTextBox11.Text != "" && deTextBox10.Text != "" && deTextBox9.Text != "")
                                {
                                    joinDate = deTextBox11.Text + "-" + deTextBox10.Text + "-" + deTextBox9.Text;
                                }
                                else
                                {
                                    joinDate = "";
                                }

                                if (deTextBox12.Text != "" && deTextBox6.Text != "" && deTextBox5.Text != "")
                                {
                                    rDate = deTextBox12.Text + "-" + deTextBox6.Text + "-" + deTextBox5.Text;
                                }
                                else
                                {
                                    rDate = "";
                                }
                                

                                string dept = deTextBox7.Text.Trim();
                                string acc = deTextBox8.Text.Trim();

                                bool insertCase = insertIntoMeta(item,statename,emp_name,filenumber,ppo,joinDate,rDate,dept,acc, sqlTrans);
                                if (insertCase == true)
                                {
                                    if (sqlTrans == null)
                                    {
                                        sqlTrans = sqlCon.BeginTransaction();
                                    }
                                    sqlTrans.Commit();
                                    sqlTrans = null;
                                    MessageBox.Show(this, "Record Saved Successfully...", "B'Zer ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    
                                    if(item < 50)
                                    {
                                        frmNewCase_Load(sender, e);
                                    }
                                    else 
                                    {
                                        bool updatebundle = updateBundle();
                                        bool updatecasefile = updateCaseFile();
                                        if (updatebundle == true && updatecasefile == true)
                                        {
                                            MessageBox.Show("Batch sucessfully uploaded for next stage");
                                            this.Close();
                                        }
                                        
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
                DialogResult result = MessageBox.Show("Do you want to save changes ? ", "B'Zer - Confirmation !", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if(currStage == "Entry")
                    {
                        if (validate() == true)
                        {
                            string joinDate = string.Empty;
                            string rDate = string.Empty;
                            if (deTextBox3.Text != Files.filename)
                            {
                                if (checkFileNotExistsEdit(projKey, bundleKey, deTextBox3.Text.Trim()) == true)
                                {
                                    casefile = deTextBox3.Text;
                                    string statename = deTextBox1.Text.Trim();
                                    string emp_name = deTextBox2.Text.Trim();
                                    string filenumber = deTextBox3.Text.Trim();
                                    string ppo = deTextBox4.Text.Trim();



                                    if (deTextBox11.Text != "" && deTextBox10.Text != "" && deTextBox9.Text != "")
                                    {
                                        joinDate = deTextBox11.Text + "-" + deTextBox10.Text + "-" + deTextBox9.Text;
                                    }
                                    else
                                    {
                                        joinDate = "";
                                    }

                                    if (deTextBox12.Text != "" && deTextBox6.Text != "" && deTextBox5.Text != "")
                                    {
                                        rDate = deTextBox12.Text + "-" + deTextBox6.Text + "-" + deTextBox5.Text;
                                    }
                                    else
                                    {
                                        rDate = "";
                                    }


                                    string dept = deTextBox7.Text.Trim();
                                    string acc = deTextBox8.Text.Trim();


                                    filename = casefile;


                                    bool updateMeta = updateMetaEdit(statename, emp_name, ppo, joinDate, rDate, dept, acc);
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
                                                if (f.Name.Contains(old_filename))
                                                {
                                                    string str1 = f.Name;

                                                    string str2 = f.Name.Replace(old_filename, filename);

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
                                                if (f.Name.Contains(old_filename))
                                                {
                                                    string str1 = f.Name;

                                                    string str2 = f.Name.Replace(old_filename, filename);

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
                                                if (f.Name.Contains(old_filename))
                                                {
                                                    string str1 = f.Name;

                                                    string str2 = f.Name.Replace(old_filename, filename);

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
                                casefile = deTextBox3.Text;
                                string statename = deTextBox1.Text.Trim();
                                string emp_name = deTextBox2.Text.Trim();
                                string filenumber = deTextBox3.Text.Trim();
                                string ppo = deTextBox4.Text.Trim();



                                if (deTextBox11.Text != "" && deTextBox10.Text != "" && deTextBox9.Text != "")
                                {
                                    joinDate = deTextBox11.Text + "-" + deTextBox10.Text + "-" + deTextBox9.Text;
                                }
                                else
                                {
                                    joinDate = "";
                                }

                                if (deTextBox12.Text != "" && deTextBox6.Text != "" && deTextBox5.Text != "")
                                {
                                    rDate = deTextBox12.Text + "-" + deTextBox6.Text + "-" + deTextBox5.Text;
                                }
                                else
                                {
                                    rDate = "";
                                }


                                string dept = deTextBox7.Text.Trim();
                                string acc = deTextBox8.Text.Trim();



                                filename = casefile;


                                bool updateMeta = updateMetaEdit(statename, emp_name, ppo, joinDate, rDate, dept, acc);
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
                                            if (f.Name.Contains(old_filename))
                                            {
                                                string str1 = f.Name;

                                                string str2 = f.Name.Replace(old_filename, filename);

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
                                            if (f.Name.Contains(old_filename))
                                            {
                                                string str1 = f.Name;

                                                string str2 = f.Name.Replace(old_filename, filename);

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
                                            if (f.Name.Contains(old_filename))
                                            {
                                                string str1 = f.Name;

                                                string str2 = f.Name.Replace(old_filename, filename);

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
                    if(currStage == "FQC")
                    {
                        if (validate() == true)
                        {
                            string joinDate = string.Empty;
                            string rDate = string.Empty;
                            if (deTextBox3.Text != aeFQC.filename)
                            {
                                if (checkFileNotExistsEdit(projKey, bundleKey, deTextBox3.Text.Trim()) == true)
                                {
                                    casefile = deTextBox3.Text;
                                    string statename = deTextBox1.Text.Trim();
                                    string emp_name = deTextBox2.Text.Trim();
                                    string filenumber = deTextBox3.Text.Trim();
                                    string ppo = deTextBox4.Text.Trim();



                                    if (deTextBox11.Text != "" && deTextBox10.Text != "" && deTextBox9.Text != "")
                                    {
                                        joinDate = deTextBox11.Text + "-" + deTextBox10.Text + "-" + deTextBox9.Text;
                                    }
                                    else
                                    {
                                        joinDate = "";
                                    }

                                    if (deTextBox12.Text != "" && deTextBox6.Text != "" && deTextBox5.Text != "")
                                    {
                                        rDate = deTextBox12.Text + "-" + deTextBox6.Text + "-" + deTextBox5.Text;
                                    }
                                    else
                                    {
                                        rDate = "";
                                    }


                                    string dept = deTextBox7.Text.Trim();
                                    string acc = deTextBox8.Text.Trim();


                                    filename = casefile;


                                    bool updateMeta = updateMetaEdit(statename, emp_name, ppo, joinDate, rDate, dept, acc);
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
                                                if (f.Name.Contains(old_filename))
                                                {
                                                    string str1 = f.Name;

                                                    string str2 = f.Name.Replace(old_filename, filename);

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
                                                if (f.Name.Contains(old_filename))
                                                {
                                                    string str1 = f.Name;

                                                    string str2 = f.Name.Replace(old_filename, filename);

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
                                                if (f.Name.Contains(old_filename))
                                                {
                                                    string str1 = f.Name;

                                                    string str2 = f.Name.Replace(old_filename, filename);

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
                                casefile = deTextBox3.Text;
                                string statename = deTextBox1.Text.Trim();
                                string emp_name = deTextBox2.Text.Trim();
                                string filenumber = deTextBox3.Text.Trim();
                                string ppo = deTextBox4.Text.Trim();



                                if (deTextBox11.Text != "" && deTextBox10.Text != "" && deTextBox9.Text != "")
                                {
                                    joinDate = deTextBox11.Text + "-" + deTextBox10.Text + "-" + deTextBox9.Text;
                                }
                                else
                                {
                                    joinDate = "";
                                }

                                if (deTextBox12.Text != "" && deTextBox6.Text != "" && deTextBox5.Text != "")
                                {
                                    rDate = deTextBox12.Text + "-" + deTextBox6.Text + "-" + deTextBox5.Text;
                                }
                                else
                                {
                                    rDate = "";
                                }


                                string dept = deTextBox7.Text.Trim();
                                string acc = deTextBox8.Text.Trim();



                                filename = casefile;


                                bool updateMeta = updateMetaEdit(statename, emp_name, ppo, joinDate, rDate, dept, acc);
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
                                            if (f.Name.Contains(old_filename))
                                            {
                                                string str1 = f.Name;

                                                string str2 = f.Name.Replace(old_filename, filename);

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
                                            if (f.Name.Contains(old_filename))
                                            {
                                                string str1 = f.Name;

                                                string str2 = f.Name.Replace(old_filename, filename);

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
                                            if (f.Name.Contains(old_filename))
                                            {
                                                string str1 = f.Name;

                                                string str2 = f.Name.Replace(old_filename, filename);

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
                else
                {
                    return;
                }
            }
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
        private void deTextBox3_Leave(object sender, EventArgs e)
        {
            if (_mode == Mode._Add)
            {
                checkFileNotExists(projKey, bundleKey, deTextBox3.Text.Trim());
            }
            if (_mode == Mode._Edit)
            {
                if(currStage == "Entry")
                {
                    if (deTextBox3.Text != Files.filename)
                    {
                        checkFileNotExistsEdit(projKey, bundleKey, deTextBox3.Text.Trim());
                    }
                }
                if (currStage == "FQC")
                {
                    if (deTextBox3.Text != aeFQC.filename)
                    {
                        checkFileNotExistsEdit(projKey, bundleKey, deTextBox3.Text.Trim());
                    }
                }
            }
        }

        private void deTextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void deTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Regex.IsMatch(e.KeyChar.ToString(), @"^[a-zA-Z0-9\s\b]*$")) || (e.KeyChar.ToString() == "-") || (e.KeyChar.ToString() == "_"))
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

        private void deTextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Regex.IsMatch(e.KeyChar.ToString(), @"^[a-zA-Z0-9\s\b]*$")) || (e.KeyChar.ToString() == "-") || (e.KeyChar.ToString() == "_"))
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

        private void deTextBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Regex.IsMatch(e.KeyChar.ToString(), @"^[a-zA-Z0-9\s\b]*$")) || (e.KeyChar.ToString() == "-") || (e.KeyChar.ToString() == "_"))
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

        private void deTextBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Regex.IsMatch(e.KeyChar.ToString(), @"^[a-zA-Z0-9\s\b]*$")) || (e.KeyChar.ToString() == "-") || (e.KeyChar.ToString() == "_"))
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

        private void deTextBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Regex.IsMatch(e.KeyChar.ToString(), @"^[a-zA-Z0-9\s\b]*$")) || (e.KeyChar.ToString() == "-") || (e.KeyChar.ToString() == "_"))
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

        private void deTextBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Regex.IsMatch(e.KeyChar.ToString(), @"^[a-zA-Z0-9\s\b]*$")) || (e.KeyChar.ToString() == "-") || (e.KeyChar.ToString() == "_"))
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

        private void deTextBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Regex.IsMatch(e.KeyChar.ToString(), @"^[a-zA-Z0-9\s\b]*$")) || (e.KeyChar.ToString() == "-") || (e.KeyChar.ToString() == "_"))
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

        private void deTextBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Regex.IsMatch(e.KeyChar.ToString(), @"^[a-zA-Z0-9\s\b]*$")) || (e.KeyChar.ToString() == "-") || (e.KeyChar.ToString() == "_"))
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

        private void deTextBox12_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Regex.IsMatch(e.KeyChar.ToString(), @"^[a-zA-Z0-9\s\b]*$")) || (e.KeyChar.ToString() == "-") || (e.KeyChar.ToString() == "_"))
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

        private void deTextBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Regex.IsMatch(e.KeyChar.ToString(), @"^[a-zA-Z0-9\s\b]*$")) || (e.KeyChar.ToString() == "-") || (e.KeyChar.ToString() == "_"))
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

        private void deTextBox8_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void deTextBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Regex.IsMatch(e.KeyChar.ToString(), @"^[a-zA-Z0-9\s\b]*$")) || (e.KeyChar.ToString() == "-") || (e.KeyChar.ToString() == "_"))
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }
    }
}
