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
    public partial class frmGE : Form
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

        public frmGE()
        {
            InitializeComponent();
        }

        public frmGE(string proj, string bundle, OdbcConnection pCon, Credentials pcrd, DataLayerDefs.Mode mode, eSTATES[] prmState)
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

        public frmGE(string proj, string bundle, OdbcConnection pCon, Credentials pcrd, DataLayerDefs.Mode mode, string file, string stage)
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

        public frmGE(string proj, string bundle, OdbcConnection pCon, Credentials pcrd, DataLayerDefs.Mode mode, string file)
        {
            InitializeComponent();

            projKey = proj;

            bundleKey = bundle;

            sqlCon = pCon;

            //txn = pTxn;

            crd = pcrd;

            _mode = mode;

            old_filename = file;

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

        private void frmGE_Load(object sender, EventArgs e)
        {
            if (_mode == Mode._Add)
            {


                find_state_name();
                if (deTextBox6.Text == "")
                {
                    this.Close();
                }
                deTextBox14.Text = string.Empty;
                deTextBox15.Text = string.Empty;
                deTextBox16.Text = string.Empty;
                deTextBox7.Text = string.Empty;
                deTextBox8.Text = string.Empty;
                deTextBox9.Text = string.Empty;

                deTextBox10.Text = string.Empty;
                deTextBox11.Text = string.Empty;
                deTextBox12.Text = string.Empty;

                deTextBox19.Text = string.Empty;
                deTextBox20.Text = string.Empty;
                deTextBox21.Text = string.Empty;

                deTextBox13.Text = string.Empty;
                deTextBox17.Text = string.Empty;
                deTextBox18.Text = string.Empty;

                deTextBox27.Text = string.Empty;
                deTextBox28.Text = string.Empty;
                deTextBox29.Text = string.Empty;


                deTextBox14.Focus();
                deTextBox14.Select();

            }
            if (_mode == Mode._Edit)
            {
                string state = _GetFileCaseDetailsIndividual(projKey, bundleKey, old_filename).Rows[0][6].ToString();
                string emp_name = _GetFileCaseDetailsIndividual(projKey, bundleKey, old_filename).Rows[0][7].ToString();
                string GE_no = _GetFileCaseDetailsIndividual(projKey, bundleKey, old_filename).Rows[0][8].ToString();
                string pen_no = _GetFileCaseDetailsIndividual(projKey, bundleKey, old_filename).Rows[0][9].ToString();
                string promoted_dep = _GetFileCaseDetailsIndividual(projKey, bundleKey, old_filename).Rows[0][10].ToString();
                string sub_doc_type = _GetFileCaseDetailsIndividual(projKey, bundleKey, old_filename).Rows[0][11].ToString();
                string index_no = _GetFileCaseDetailsIndividual(projKey, bundleKey, old_filename).Rows[0][12].ToString();
                string dop = _GetFileCaseDetailsIndividual(projKey, bundleKey, old_filename).Rows[0][16].ToString();
                string dob = _GetFileCaseDetailsIndividual(projKey, bundleKey, old_filename).Rows[0][13].ToString();
                string doj = _GetFileCaseDetailsIndividual(projKey, bundleKey, old_filename).Rows[0][14].ToString();
                string dor = _GetFileCaseDetailsIndividual(projKey, bundleKey, old_filename).Rows[0][15].ToString();


                deTextBox6.Text = state;
                deTextBox14.Text = emp_name;
                deTextBox15.Text = GE_no;
                deTextBox16.Text = pen_no;
                deTextBox7.Text = promoted_dep;
                deTextBox8.Text = sub_doc_type;
                deTextBox9.Text = index_no;


                if (dob != "")
                {
                    deTextBox10.Text = dob.Substring(8, 2);
                    deTextBox11.Text = dob.Substring(5, 2);
                    deTextBox12.Text = dob.Substring(0, 4);
                }
                if (doj != "")
                {
                    deTextBox19.Text = doj.Substring(8, 2);
                    deTextBox20.Text = doj.Substring(5, 2);
                    deTextBox21.Text = doj.Substring(0, 4);
                }
                if (dor != "")
                {
                    deTextBox27.Text = dor.Substring(8, 2);
                    deTextBox28.Text = dor.Substring(5, 2);
                    deTextBox29.Text = dor.Substring(0, 4);
                }
                if (dop != "")
                {
                    deTextBox13.Text = dop.Substring(8, 2);
                    deTextBox17.Text = dop.Substring(5, 2);
                    deTextBox18.Text = dop.Substring(0, 4);
                }


                deTextBox14.Focus();
                deTextBox14.Select();
            }
        }

        public DataTable _GetFileCaseDetailsIndividual(string proj, string bundle, string fileName)
        {
            DataTable dt = new DataTable();
            string sql = "select distinct proj_code, batch_Key,item_no,filename,department,subcat,state_name,emp_name,ge_no,pen_no,promoted_dep,sub_doc_type,index_no,date_format(birth_date,'%Y-%m-%d'),date_format(joining_date,'%Y-%m-%d')," +
                "date_format(retirement_date,'%Y-%m-%d'),date_format(promotion_date,'%Y-%m-%d') " +
                "from metadata_entry where proj_code = '" + proj + "' and batch_key = '" + bundle + "' and filename = '" + fileName + "' ";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon, txn);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
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

        private void deTextBox14_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Regex.IsMatch(e.KeyChar.ToString(), @"^[a-zA-Z0-9\s\b\cC\cX\cV\cA]*$")) || (e.KeyChar.ToString() == "-") || (e.KeyChar.ToString() == "_"))
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

        private void deTextBox15_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Regex.IsMatch(e.KeyChar.ToString(), @"^[a-zA-Z0-9\s\b\cC\cX\cV\cA]*$")) || (e.KeyChar.ToString() == "-") || (e.KeyChar.ToString() == "_"))
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }
        private bool checkFileNotExists(string file, string dept, string subCat)
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

                MessageBox.Show("This GE number already exists for batch - " + dt1.Rows[0][0].ToString(), "B'Zer - Confirmation !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                retval = false;
                deTextBox21.Focus();
                return retval;
            }
            else
            {
                retval = true;
            }

            return retval;
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

                MessageBox.Show("This file number already exists for batch - " + dt1.Rows[0][0].ToString(), "B'Zer - Confirmation !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                retval = false;
                deTextBox21.Focus();
                return retval;
            }
            else
            {
                retval = true;
            }

            return retval;
        }
        private void deTextBox15_Leave(object sender, EventArgs e)
        {
            if (_mode == Mode._Add)
            {
                checkFileNotExists(deTextBox15.Text.Trim(), deTextBox4.Text.Trim(), deTextBox5.Text.Trim());
            }
            if (_mode == Mode._Edit)
            {
                if (currStage == "Entry")
                {
                    if (deTextBox15.Text.Trim() != Files.filename)
                    {
                        checkFileNotExistsEdit(deTextBox15.Text.Trim(), deTextBox4.Text.Trim(), deTextBox5.Text.Trim());
                    }
                }
                if (currStage == "FQC")
                {
                    if (deTextBox15.Text.Trim() != aeFQC.filename)
                    {
                        checkFileNotExistsEdit(deTextBox15.Text.Trim(), deTextBox4.Text.Trim(), deTextBox5.Text.Trim());
                    }
                }
            }
        }

        private void deTextBox16_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Regex.IsMatch(e.KeyChar.ToString(), @"^[a-zA-Z0-9\s\b\cC\cX\cV\cA]*$")) || (e.KeyChar.ToString() == "-") || (e.KeyChar.ToString() == "_"))
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
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

        private void deTextBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Regex.IsMatch(e.KeyChar.ToString(), @"^[0-9\s\b\cC\cX\cV\cA]*$")))
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

        private void deTextBox10_Leave(object sender, EventArgs e)
        {
            if (deTextBox10.Text == "" || deTextBox10.Text == null || String.IsNullOrEmpty(deTextBox10.Text) || String.IsNullOrWhiteSpace(deTextBox10.Text))
            { }
            else
            {
                if (deTextBox10.Text.Length < 2)
                {
                    deTextBox10.Text = deTextBox10.Text.PadLeft(2, '0');
                }
            }
        }

        private void deTextBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Regex.IsMatch(e.KeyChar.ToString(), @"^[0-9\s\b\cC\cX\cV\cA]*$")))
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

        private void deTextBox12_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Regex.IsMatch(e.KeyChar.ToString(), @"^[0-9\s\b\cC\cX\cV\cA]*$")))
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

        private void deTextBox19_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Regex.IsMatch(e.KeyChar.ToString(), @"^[0-9\s\b\cC\cX\cV\cA]*$")))
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

        private void deTextBox20_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Regex.IsMatch(e.KeyChar.ToString(), @"^[0-9\s\b\cC\cX\cV\cA]*$")))
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

        private void deTextBox21_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Regex.IsMatch(e.KeyChar.ToString(), @"^[0-9\s\b\cC\cX\cV\cA]*$")))
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

        private void deTextBox13_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Regex.IsMatch(e.KeyChar.ToString(), @"^[0-9\s\b\cC\cX\cV\cA]*$")))
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

        private void deTextBox17_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Regex.IsMatch(e.KeyChar.ToString(), @"^[0-9\s\b\cC\cX\cV\cA]*$")))
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

        private void deTextBox18_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Regex.IsMatch(e.KeyChar.ToString(), @"^[0-9\s\b\cC\cX\cV\cA]*$")))
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

        private void deTextBox27_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Regex.IsMatch(e.KeyChar.ToString(), @"^[0-9\s\b\cC\cX\cV\cA]*$")))
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

        private void deTextBox28_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Regex.IsMatch(e.KeyChar.ToString(), @"^[0-9\s\b\cC\cX\cV\cA]*$")))
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

        private void deTextBox29_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Regex.IsMatch(e.KeyChar.ToString(), @"^[0-9\s\b\cC\cX\cV\cA]*$")))
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

        private void deTextBox11_Leave(object sender, EventArgs e)
        {
            if (deTextBox11.Text == "" || deTextBox11.Text == null || String.IsNullOrEmpty(deTextBox11.Text) || String.IsNullOrWhiteSpace(deTextBox11.Text))
            { }
            else
            {
                if (deTextBox11.Text.Length < 2)
                {
                    deTextBox11.Text = deTextBox11.Text.PadLeft(2, '0');
                }
            }
        }

        private void deTextBox19_Leave(object sender, EventArgs e)
        {
            if (deTextBox19.Text == "" || deTextBox19.Text == null || String.IsNullOrEmpty(deTextBox19.Text) || String.IsNullOrWhiteSpace(deTextBox19.Text))
            { }
            else
            {
                if (deTextBox19.Text.Length < 2)
                {
                    deTextBox19.Text = deTextBox19.Text.PadLeft(2, '0');
                }
            }
        }

        private void deTextBox13_Leave(object sender, EventArgs e)
        {
            if (deTextBox13.Text == "" || deTextBox13.Text == null || String.IsNullOrEmpty(deTextBox13.Text) || String.IsNullOrWhiteSpace(deTextBox13.Text))
            { }
            else
            {
                if (deTextBox13.Text.Length < 2)
                {
                    deTextBox13.Text = deTextBox13.Text.PadLeft(2, '0');
                }
            }
        }

        private void deTextBox27_Leave(object sender, EventArgs e)
        {
            if (deTextBox27.Text == "" || deTextBox27.Text == null || String.IsNullOrEmpty(deTextBox27.Text) || String.IsNullOrWhiteSpace(deTextBox27.Text))
            { }
            else
            {
                if (deTextBox27.Text.Length < 2)
                {
                    deTextBox27.Text = deTextBox27.Text.PadLeft(2, '0');
                }
            }
        }

        private void deTextBox20_Leave(object sender, EventArgs e)
        {
            if (deTextBox20.Text == "" || deTextBox20.Text == null || String.IsNullOrEmpty(deTextBox20.Text) || String.IsNullOrWhiteSpace(deTextBox20.Text))
            { }
            else
            {
                if (deTextBox20.Text.Length < 2)
                {
                    deTextBox20.Text = deTextBox20.Text.PadLeft(2, '0');
                }
            }
        }

        private void deTextBox17_Leave(object sender, EventArgs e)
        {
            if (deTextBox17.Text == "" || deTextBox17.Text == null || String.IsNullOrEmpty(deTextBox17.Text) || String.IsNullOrWhiteSpace(deTextBox17.Text))
            { }
            else
            {
                if (deTextBox17.Text.Length < 2)
                {
                    deTextBox17.Text = deTextBox17.Text.PadLeft(2, '0');
                }
            }
        }

        private void deTextBox28_Leave(object sender, EventArgs e)
        {
            if (deTextBox28.Text == "" || deTextBox28.Text == null || String.IsNullOrEmpty(deTextBox28.Text) || String.IsNullOrWhiteSpace(deTextBox28.Text))
            { }
            else
            {
                if (deTextBox28.Text.Length < 2)
                {
                    deTextBox28.Text = deTextBox28.Text.PadLeft(2, '0');
                }
            }
        }

        private void frmGE_KeyUp(object sender, KeyEventArgs e)
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
            if (deTextBox14.Text == "" || deTextBox14.Text == null || String.IsNullOrEmpty(deTextBox14.Text) || String.IsNullOrWhiteSpace(deTextBox14.Text))
            {
                retval = false;

                MessageBox.Show("You cannot leave employee name field blank...", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                deTextBox14.Focus();
                return retval;
            }
            if (deTextBox15.Text == "" || deTextBox15.Text == null || String.IsNullOrEmpty(deTextBox15.Text) || String.IsNullOrWhiteSpace(deTextBox15.Text))
            {
                retval = false;

                MessageBox.Show("You cannot leave GE number field blank...", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                deTextBox15.Focus();
                return retval;
            }
            if (deTextBox10.Text != "" || deTextBox11.Text != "" || deTextBox12.Text != "")
            {
                if (deTextBox12.Text != "")
                {

                    bool res = System.Text.RegularExpressions.Regex.IsMatch(deTextBox12.Text, "[^0-9]");
                    if (res != true && Convert.ToInt32(deTextBox12.Text) <= curIntYear && deTextBox12.Text.Length == 4 && deTextBox12.Text.Substring(0, 1) != "0")
                    {
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

                if (deTextBox11.Text != "")
                {

                    bool res1 = System.Text.RegularExpressions.Regex.IsMatch(deTextBox11.Text, "[^0-9]");

                    if (res1 != true && deTextBox11.Text.Length == 2 && Convert.ToInt32(deTextBox11.Text) <= 12 && Convert.ToInt32(deTextBox11.Text) != 0 && deTextBox11.Text != "00")
                    {
                        retval = true;

                    }
                    else
                    {
                        retval = false;
                        MessageBox.Show(this, "Please input Valid Month...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        deTextBox11.Focus();
                        return retval;
                    }
                }
                else
                {
                    retval = false;
                    MessageBox.Show(this, "Please input Valid Month...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    deTextBox11.Focus();
                    return retval;
                }
                if (deTextBox10.Text != "")
                {

                    bool res2 = System.Text.RegularExpressions.Regex.IsMatch(deTextBox10.Text, "[^0-9]");
                    if (res2 != true && deTextBox10.Text.Length == 2 && Convert.ToInt32(deTextBox10.Text) <= 31 && Convert.ToInt32(deTextBox10.Text) != 0 && deTextBox10.Text != "00")
                    {
                        retval = true;

                    }
                    else
                    {
                        retval = false;
                        MessageBox.Show(this, "Please input Valid Date...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        deTextBox10.Focus();
                        return retval;
                    }
                }
                else
                {
                    retval = false;
                    MessageBox.Show(this, "Please input Valid Date...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    deTextBox10.Focus();
                    return retval;
                }

                DateTime temp;
                string isDate = deTextBox12.Text + "-" + deTextBox11.Text + "-" + deTextBox10.Text;
                if (DateTime.TryParse(isDate, out temp) && DateTime.Parse(isDate) < DateTime.Parse(currDate))
                {
                    retval = true;
                }
                else
                {
                    retval = false;
                    MessageBox.Show(this, "Please select a valid date", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    deTextBox10.Select();
                    return retval;

                }
            }
            else
            {
                retval = true;
            }

            if (deTextBox13.Text != "" || deTextBox17.Text != "" || deTextBox18.Text != "")
            {
                if (deTextBox18.Text != "")
                {

                    bool res = System.Text.RegularExpressions.Regex.IsMatch(deTextBox18.Text, "[^0-9]");
                    if (res != true && Convert.ToInt32(deTextBox18.Text) <= curIntYear && deTextBox18.Text.Length == 4 && deTextBox18.Text.Substring(0, 1) != "0")
                    {
                        //retval = true;
                        retval = true;
                    }
                    else
                    {
                        retval = false;
                        MessageBox.Show(this, "Please input Valid Year...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        deTextBox18.Focus();
                        return retval;
                    }
                }

                if (deTextBox17.Text != "")
                {

                    bool res1 = System.Text.RegularExpressions.Regex.IsMatch(deTextBox17.Text, "[^0-9]");

                    if (res1 != true && deTextBox17.Text.Length == 2 && Convert.ToInt32(deTextBox17.Text) <= 12 && Convert.ToInt32(deTextBox17.Text) != 0 && deTextBox17.Text != "00")
                    {
                        //retval = true;
                        retval = true;
                    }
                    else
                    {
                        retval = false;
                        MessageBox.Show(this, "Please input Valid Month...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        deTextBox17.Focus();
                        return retval;
                    }
                }
                else
                {
                    retval = false;
                    MessageBox.Show(this, "Please input Valid Month...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    deTextBox17.Focus();
                    return retval;
                }

                if (deTextBox13.Text != "")
                {

                    bool res2 = System.Text.RegularExpressions.Regex.IsMatch(deTextBox13.Text, "[^0-9]");
                    if (res2 != true && deTextBox13.Text.Length == 2 && Convert.ToInt32(deTextBox13.Text) <= 31 && Convert.ToInt32(deTextBox13.Text) != 0 && deTextBox13.Text != "00")
                    {
                        //retval = true;
                        retval = true;
                    }
                    else
                    {
                        retval = false;
                        MessageBox.Show(this, "Please input Valid Date...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        deTextBox13.Focus();
                        return retval;
                    }
                }
                else
                {
                    retval = false;
                    MessageBox.Show(this, "Please input Valid Date...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    deTextBox13.Focus();
                    return retval;
                }

                DateTime temp;
                string isDate = deTextBox18.Text + "-" + deTextBox17.Text + "-" + deTextBox13.Text;
                if (DateTime.TryParse(isDate, out temp) && DateTime.Parse(isDate) <= DateTime.Parse(currDate))
                {
                    retval = true;
                }
                else
                {
                    retval = false;
                    MessageBox.Show(this, "Please select a valid date", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    deTextBox13.Select();
                    return retval;

                }
            }
            else
            {
                retval = true;
            }

            if (deTextBox19.Text != "" || deTextBox20.Text != "" || deTextBox21.Text != "")
            {
                if (deTextBox21.Text != "")
                {

                    bool res = System.Text.RegularExpressions.Regex.IsMatch(deTextBox21.Text, "[^0-9]");
                    if (res != true && Convert.ToInt32(deTextBox21.Text) <= curIntYear && deTextBox21.Text.Length == 4 && deTextBox21.Text.Substring(0, 1) != "0")
                    {
                        //retval = true;
                        retval = true;
                    }
                    else
                    {
                        retval = false;
                        MessageBox.Show(this, "Please input Valid Year...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        deTextBox21.Focus();
                        return retval;
                    }
                }

                if (deTextBox20.Text != "")
                {

                    bool res1 = System.Text.RegularExpressions.Regex.IsMatch(deTextBox20.Text, "[^0-9]");

                    if (res1 != true && deTextBox20.Text.Length == 2 && Convert.ToInt32(deTextBox20.Text) <= 12 && Convert.ToInt32(deTextBox20.Text) != 0 && deTextBox20.Text != "00")
                    {
                        //retval = true;
                        retval = true;
                    }
                    else
                    {
                        retval = false;
                        MessageBox.Show(this, "Please input Valid Month...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        deTextBox20.Focus();
                        return retval;
                    }
                }
                else
                {
                    retval = false;
                    MessageBox.Show(this, "Please input Valid Month...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    deTextBox20.Focus();
                    return retval;
                }

                if (deTextBox19.Text != "")
                {

                    bool res2 = System.Text.RegularExpressions.Regex.IsMatch(deTextBox19.Text, "[^0-9]");
                    if (res2 != true && deTextBox19.Text.Length == 2 && Convert.ToInt32(deTextBox19.Text) <= 31 && Convert.ToInt32(deTextBox19.Text) != 0 && deTextBox19.Text != "00")
                    {
                        //retval = true;
                        retval = true;
                    }
                    else
                    {
                        retval = false;
                        MessageBox.Show(this, "Please input Valid Date...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        deTextBox19.Focus();
                        return retval;
                    }
                }
                else
                {
                    retval = false;
                    MessageBox.Show(this, "Please input Valid Date...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    deTextBox19.Focus();
                    return retval;
                }

                DateTime temp;
                string isDate = deTextBox21.Text + "-" + deTextBox20.Text + "-" + deTextBox19.Text;
                if (DateTime.TryParse(isDate, out temp) && DateTime.Parse(isDate) <= DateTime.Parse(currDate))
                {
                    retval = true;
                }
                else
                {
                    retval = false;
                    MessageBox.Show(this, "Please select a valid date", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    deTextBox19.Select();
                    return retval;

                }
            }
            else
            {
                retval = true;
            }

            //death date 
            if (deTextBox27.Text != "" || deTextBox28.Text != "" || deTextBox29.Text != "")
            {
                if (deTextBox29.Text != "")
                {

                    bool res = System.Text.RegularExpressions.Regex.IsMatch(deTextBox29.Text, "[^0-9]");
                    if (res != true && Convert.ToInt32(deTextBox29.Text) <= curIntYear && deTextBox29.Text.Length == 4 && deTextBox29.Text.Substring(0, 1) != "0")
                    {
                        //retval = true;
                        retval = true;
                    }
                    else
                    {
                        retval = false;
                        MessageBox.Show(this, "Please input Valid Year...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        deTextBox29.Focus();
                        return retval;
                    }
                }

                if (deTextBox28.Text != "")
                {

                    bool res1 = System.Text.RegularExpressions.Regex.IsMatch(deTextBox28.Text, "[^0-9]");

                    if (res1 != true && deTextBox28.Text.Length == 2 && Convert.ToInt32(deTextBox28.Text) <= 12 && Convert.ToInt32(deTextBox28.Text) != 0 && deTextBox28.Text != "00")
                    {
                        //retval = true;
                        retval = true;
                    }
                    else
                    {
                        retval = false;
                        MessageBox.Show(this, "Please input Valid Month...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        deTextBox28.Focus();
                        return retval;
                    }
                }
                else
                {
                    retval = false;
                    MessageBox.Show(this, "Please input Valid Month...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    deTextBox28.Focus();
                    return retval;
                }

                if (deTextBox27.Text != "")
                {

                    bool res2 = System.Text.RegularExpressions.Regex.IsMatch(deTextBox27.Text, "[^0-9]");
                    if (res2 != true && deTextBox27.Text.Length == 2 && Convert.ToInt32(deTextBox27.Text) <= 31 && Convert.ToInt32(deTextBox27.Text) != 0 && deTextBox27.Text != "00")
                    {
                        //retval = true;
                        retval = true;
                    }
                    else
                    {
                        retval = false;
                        MessageBox.Show(this, "Please input Valid Date...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        deTextBox27.Focus();
                        return retval;
                    }
                }
                else
                {
                    retval = false;
                    MessageBox.Show(this, "Please input Valid Date...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    deTextBox27.Focus();
                    return retval;
                }

                DateTime temp;
                string isDate = deTextBox29.Text + "-" + deTextBox28.Text + "-" + deTextBox27.Text;
                if (DateTime.TryParse(isDate, out temp) && DateTime.Parse(isDate) <= DateTime.Parse(currDate))
                {
                    retval = true;
                }
                else
                {
                    retval = false;
                    MessageBox.Show(this, "Please select a valid date", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    deTextBox27.Select();
                    return retval;

                }
            }
            else
            {
                retval = true;
            }


            DateTime temp1;
            string isBDate = deTextBox12.Text + deTextBox11.Text + deTextBox10.Text;
            string isJDate = deTextBox21.Text + deTextBox20.Text + deTextBox19.Text;
            string isPDate = deTextBox18.Text + deTextBox17.Text + deTextBox13.Text;
            string isRDate = deTextBox29.Text + deTextBox28.Text + deTextBox27.Text;

            if ((isBDate != "") && (isJDate != ""))
            {
                isBDate = deTextBox12.Text + "-" + deTextBox11.Text + "-" + deTextBox10.Text;
                isJDate = deTextBox21.Text + "-" + deTextBox20.Text + "-" + deTextBox19.Text;
                if (DateTime.TryParse(isBDate, out temp1) && DateTime.TryParse(isJDate, out temp1) && DateTime.Parse(isBDate) < DateTime.Parse(isJDate))
                {
                    retval = true;
                }
                else
                {
                    retval = false;
                    MessageBox.Show(this, "Please select a valid birth date or joining date", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    deTextBox10.Select();
                    return retval;

                }
            }
            else
            {
                retval = true;
            }
            if ((isBDate != "") && (isRDate != ""))
            {
                isBDate = deTextBox12.Text + "-" + deTextBox11.Text + "-" + deTextBox10.Text;
                isRDate = deTextBox29.Text + "-" + deTextBox28.Text + "-" + deTextBox27.Text;
                if (DateTime.TryParse(isBDate, out temp1) && DateTime.TryParse(isRDate, out temp1) && DateTime.Parse(isBDate) < DateTime.Parse(isRDate))
                {
                    retval = true;
                }
                else
                {
                    retval = false;
                    MessageBox.Show(this, "Please select a valid birth date or retirement date", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    deTextBox10.Select();
                    return retval;

                }
            }
            else
            {
                retval = true;
            }
            if ((isBDate != "") && (isPDate != ""))
            {
                isBDate = deTextBox12.Text + "-" + deTextBox11.Text + "-" + deTextBox10.Text;
                isPDate = deTextBox18.Text + "-" + deTextBox17.Text + "-" + deTextBox13.Text;
                if (DateTime.TryParse(isBDate, out temp1) && DateTime.TryParse(isPDate, out temp1) && DateTime.Parse(isBDate) < DateTime.Parse(isPDate))
                {
                    retval = true;
                }
                else
                {
                    retval = false;
                    MessageBox.Show(this, "Please select a valid birth date or promotion date", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    deTextBox10.Select();
                    return retval;

                }
            }
            else
            {
                retval = true;
            }
            if ((isJDate != "") && (isRDate != ""))
            {
                isJDate = deTextBox21.Text + "-" + deTextBox20.Text + "-" + deTextBox19.Text;
                isRDate = deTextBox29.Text + "-" + deTextBox28.Text + "-" + deTextBox27.Text;
                if (DateTime.TryParse(isJDate, out temp1) && DateTime.TryParse(isRDate, out temp1) && DateTime.Parse(isJDate) < DateTime.Parse(isRDate))
                {
                    retval = true;
                }
                else
                {
                    retval = false;
                    MessageBox.Show(this, "Please select a valid joining date or retirement date", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    deTextBox19.Select();
                    return retval;

                }
            }
            else
            {
                retval = true;
            }
            if ((isJDate != "") && (isPDate != ""))
            {
                isJDate = deTextBox21.Text + "-" + deTextBox20.Text + "-" + deTextBox19.Text;
                isPDate = deTextBox18.Text + "-" + deTextBox17.Text + "-" + deTextBox13.Text;
                if (DateTime.TryParse(isJDate, out temp1) && DateTime.TryParse(isPDate, out temp1) && DateTime.Parse(isJDate) < DateTime.Parse(isPDate))
                {
                    retval = true;
                }
                else
                {
                    retval = false;
                    MessageBox.Show(this, "Please select a valid joining date or promotion date", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    deTextBox13.Select();
                    return retval;

                }
            }
            else
            {
                retval = true;
            }

            return retval;
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
        private DataTable itemCount(string proj, string bundle)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();



            string sql = "select Count(*) from metadata_entry where  proj_code = '" + proj + "' and batch_key = '" + bundle + "'  ";

            OdbcDataAdapter odap = new OdbcDataAdapter(sql, sqlCon);
            odap.Fill(dt);



            return dt;
        }
        private bool insertIntoMeta(int itemno, string dept, string subCat, string state_name, string filename, string emp_name, string ge_no, string pen, string prom_dep, string sub_doc, string index_no, string bdate, string jdate, string pdate, string rdate, OdbcTransaction trans)
        {
            bool commitBol = true;

            string sqlStr = string.Empty;

            OdbcCommand sqlCmd = new OdbcCommand();

            //OdbcTransaction sqlTrans = null;

            //filename = file_no;

            //int sl = _GetTotalCount();
            //int sl_no = sl + 1;

            //itemno = Convert.ToString(Convert.ToInt32(itemno) + 1);

            if (frmGE.state[0] == eSTATES.METADATA_ENTRY)
            {
                sqlStr = @"insert into metadata_entry(proj_code,batch_key,item_no,department,subcat,state_name,filename,emp_name,ge_no,pen_no,promoted_dep,sub_doc_type,index_no,birth_date,joining_date,promotion_date,retirement_date,created_by,created_dttm,status) values('" +
                        projKey + "','" + bundleKey + "','" + itemno +
                        "','" + dept + "','" + subCat + "','" + state_name + "','" + filename + "','" + emp_name + "','" + ge_no + "','" + pen + "','" + prom_dep + "','" + sub_doc + "','" + index_no + "','" + bdate + "','" + jdate + "','" + pdate + "','" + rdate + "','" + crd.created_by + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',0)";
                //sqlCmd.Connection = sqlCon;
            }
            else
            {
                sqlStr = @"insert into metadata_entry(proj_code,batch_key,item_no,department,subcat,state_name,filename,emp_name,ge_no,pen_no,promoted_dep,sub_doc_type,index_no,birth_date,joining_date,promotion_date,retirement_date,created_by,created_dttm,status) values('" +
                        projKey + "','" + bundleKey + "','" + itemno +
                        "','" + dept + "','" + subCat + "','" + state_name + "','" + filename + "','" + emp_name + "','" + ge_no + "','" + pen + "','" + prom_dep + "','" + sub_doc + "','" + index_no + "','" + bdate + "','" + jdate + "','" + pdate + "','" + rdate + "','" + crd.created_by + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',5)";
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
        public bool updateMetaEdit(string dept, string subCat, string state_name, string emp_name, string ge, string pen, string pdep, string subdoc, string indexno, string bdate, string jdate, string pdate, string rdate)
        {
            bool ret = false;
            if (ret == false)
            {
                _UpdateMetaEdit(projKey, bundleKey, old_filename, filename, dept, subCat, state_name, emp_name, ge, pen, pdep,subdoc,indexno, bdate,
                    jdate,pdate, rdate);

                ret = true;
            }
            return ret;
        }
        public bool _UpdateMetaEdit(string projKey, string bundleKey, string oldFileName, string fileName,
            string dept, string subCat, string state_name, string emp_name, string ge, string pen, string pDep, string subdoc, string indexno, string bdate,
            string jdate,string pdate, string rdate)
        {
            string sqlStr = null;

            OdbcCommand sqlCmd = new OdbcCommand();

            bool retVal = false;
            string sql = string.Empty;
            string remarks = string.Empty;


            sqlStr = "UPDATE metadata_entry SET filename = '" + fileName + "',department  = '" + dept + "',subcat = '" + subCat + "'," +
                "state_name = '" + state_name + "',emp_name = '" + emp_name + "',ge_no= '" + ge + "',pen_no='" + pen + "',promoted_dep='" + pDep + "',sub_doc_type='"+subdoc+"'," +
                "index_no = '"+indexno+"',birth_date='" + bdate + "',joining_date='" + jdate + "',promotion_date='"+pdate+"',retirement_date='" + rdate + "'," +
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
                        string bDate = string.Empty;
                        string joinDate = string.Empty;
                        string rDate = string.Empty;
                        string pDate = string.Empty;

                        string dept = deTextBox4.Text.Trim();
                        string subCat = deTextBox5.Text.Trim();

                        string filenumber = deTextBox21.Text.Trim();
                        filename = filenumber;

                        if (checkFileNotExists(filename, dept, subCat) == true)
                        {

                            int item = Convert.ToInt32(itemCount(projKey, bundleKey).Rows[0][0].ToString()) + 1;

                            string statename = deTextBox6.Text.Trim();
                            string emp_name = deTextBox14.Text.Trim();
                            string GE_no = deTextBox15.Text.Trim();
                            string pen_no = deTextBox16.Text.Trim();
                            string promoted_dep = deTextBox7.Text.Trim();

                            string sub_doc_type = deTextBox8.Text.Trim();
                            string index_no = deTextBox9.Text.Trim();


                            if (deTextBox12.Text != "" && deTextBox11.Text != "" && deTextBox10.Text != "")
                            {
                                bDate = deTextBox12.Text + "-" + deTextBox11.Text + "-" + deTextBox10.Text;
                            }
                            else
                            {
                                bDate = "";
                            }

                            if (deTextBox21.Text != "" && deTextBox20.Text != "" && deTextBox19.Text != "")
                            {
                                joinDate = deTextBox21.Text + "-" + deTextBox20.Text + "-" + deTextBox19.Text;
                            }
                            else
                            {
                                joinDate = "";
                            }

                            if (deTextBox29.Text != "" && deTextBox28.Text != "" && deTextBox27.Text != "")
                            {
                                rDate = deTextBox29.Text + "-" + deTextBox28.Text + "-" + deTextBox27.Text;
                            }
                            else
                            {
                                rDate = "";
                            }
                            if (deTextBox18.Text != "" && deTextBox17.Text != "" && deTextBox13.Text != "")
                            {
                                pDate = deTextBox18.Text + "-" + deTextBox17.Text + "-" + deTextBox13.Text;
                            }
                            else
                            {
                                pDate = "";
                            }

                            filename = GE_no;

                            bool insertCase = insertIntoMeta(item, dept, subCat, statename, filename, emp_name, GE_no, pen_no, promoted_dep, sub_doc_type, index_no, bDate, joinDate, pDate, rDate, sqlTrans);
                            if (insertCase == true)
                            {
                                if (sqlTrans == null)
                                {
                                    sqlTrans = sqlCon.BeginTransaction();
                                }
                                sqlTrans.Commit();
                                sqlTrans = null;
                                MessageBox.Show(this, "Record Saved Successfully...", "B'Zer ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                if (frmGE.state[0] == eSTATES.METADATA_ENTRY)
                                {
                                    frmGE_Load(sender, e);
                                }
                                else
                                { this.Close(); }
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
                        string bDate = string.Empty;
                        string joinDate = string.Empty;
                        string rDate = string.Empty;
                        string pDate = string.Empty;

                        string dept = deTextBox4.Text.Trim();
                        string subCat = deTextBox5.Text.Trim();

                        string filenumber = deTextBox15.Text.Trim();
                        filename = filenumber;

                        if (deTextBox15.Text.Trim() != Files.filename)
                        {
                            if (checkFileNotExistsEdit(deTextBox15.Text.Trim(), dept, subCat) == true)
                            {
                                casefile = deTextBox15.Text.Trim();


                                string statename = deTextBox6.Text.Trim();
                                string emp_name = deTextBox14.Text.Trim();

                                string GE_no = deTextBox15.Text.Trim();
                                string pen_no = deTextBox16.Text.Trim();
                                string promoted_dep = deTextBox7.Text.Trim();
                                string sub_doc_type = deTextBox8.Text.Trim();
                                string index_no = deTextBox9.Text.Trim();
                                string dop = _GetFileCaseDetailsIndividual(projKey, bundleKey, old_filename).Rows[0][16].ToString();
                                string dob = _GetFileCaseDetailsIndividual(projKey, bundleKey, old_filename).Rows[0][13].ToString();
                                string doj = _GetFileCaseDetailsIndividual(projKey, bundleKey, old_filename).Rows[0][14].ToString();
                                string dor = _GetFileCaseDetailsIndividual(projKey, bundleKey, old_filename).Rows[0][15].ToString();

                                if (deTextBox12.Text != "" && deTextBox11.Text != "" && deTextBox10.Text != "")
                                {
                                    bDate = deTextBox12.Text + "-" + deTextBox11.Text + "-" + deTextBox10.Text;
                                }
                                else
                                {
                                    bDate = "";
                                }

                                if (deTextBox21.Text != "" && deTextBox20.Text != "" && deTextBox19.Text != "")
                                {
                                    joinDate = deTextBox21.Text + "-" + deTextBox20.Text + "-" + deTextBox19.Text;
                                }
                                else
                                {
                                    joinDate = "";
                                }

                                if (deTextBox29.Text != "" && deTextBox28.Text != "" && deTextBox27.Text != "")
                                {
                                    rDate = deTextBox29.Text + "-" + deTextBox28.Text + "-" + deTextBox27.Text;
                                }
                                else
                                {
                                    rDate = "";
                                }
                                if (deTextBox18.Text != "" && deTextBox17.Text != "" && deTextBox13.Text != "")
                                {
                                    pDate = deTextBox18.Text + "-" + deTextBox17.Text + "-" + deTextBox13.Text;
                                }
                                else
                                {
                                    pDate = "";
                                }

                                


                                filename = casefile;


                                bool updateMeta = updateMetaEdit(dept, subCat, statename, emp_name, GE_no, pen_no,promoted_dep,sub_doc_type,index_no, bDate, joinDate, pDate,
                                 rDate);
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

                            filename = casefile;

                            casefile = deTextBox15.Text.Trim();


                            string statename = deTextBox6.Text.Trim();
                            string emp_name = deTextBox14.Text.Trim();

                            string GE_no = deTextBox15.Text.Trim();
                            string pen_no = deTextBox16.Text.Trim();
                            string promoted_dep = deTextBox7.Text.Trim();
                            string sub_doc_type = deTextBox8.Text.Trim();
                            string index_no = deTextBox9.Text.Trim();
                            string dop = _GetFileCaseDetailsIndividual(projKey, bundleKey, old_filename).Rows[0][16].ToString();
                            string dob = _GetFileCaseDetailsIndividual(projKey, bundleKey, old_filename).Rows[0][13].ToString();
                            string doj = _GetFileCaseDetailsIndividual(projKey, bundleKey, old_filename).Rows[0][14].ToString();
                            string dor = _GetFileCaseDetailsIndividual(projKey, bundleKey, old_filename).Rows[0][15].ToString();

                            if (deTextBox12.Text != "" && deTextBox11.Text != "" && deTextBox10.Text != "")
                            {
                                bDate = deTextBox12.Text + "-" + deTextBox11.Text + "-" + deTextBox10.Text;
                            }
                            else
                            {
                                bDate = "";
                            }

                            if (deTextBox21.Text != "" && deTextBox20.Text != "" && deTextBox19.Text != "")
                            {
                                joinDate = deTextBox21.Text + "-" + deTextBox20.Text + "-" + deTextBox19.Text;
                            }
                            else
                            {
                                joinDate = "";
                            }

                            if (deTextBox29.Text != "" && deTextBox28.Text != "" && deTextBox27.Text != "")
                            {
                                rDate = deTextBox29.Text + "-" + deTextBox28.Text + "-" + deTextBox27.Text;
                            }
                            else
                            {
                                rDate = "";
                            }
                            if (deTextBox18.Text != "" && deTextBox17.Text != "" && deTextBox13.Text != "")
                            {
                                pDate = deTextBox18.Text + "-" + deTextBox17.Text + "-" + deTextBox13.Text;
                            }
                            else
                            {
                                pDate = "";
                            }




                            filename = casefile;

                            bool updateMeta = updateMetaEdit(dept, subCat, statename, emp_name, GE_no, pen_no, promoted_dep, sub_doc_type, index_no, bDate, joinDate, pDate,
                                 rDate);
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
                        string bDate = string.Empty;
                        string joinDate = string.Empty;
                        string rDate = string.Empty;
                        string pDate = string.Empty;

                        string dept = deTextBox4.Text.Trim();
                        string subCat = deTextBox5.Text.Trim();

                        string filenumber = deTextBox15.Text.Trim();
                        filename = filenumber;

                        if (deTextBox15.Text.Trim() != aeFQC.filename)
                        {
                            if (checkFileNotExistsEdit(deTextBox15.Text.Trim(), dept, subCat) == true)
                            {
                                casefile = deTextBox15.Text.Trim();


                                string statename = deTextBox6.Text.Trim();
                                string emp_name = deTextBox14.Text.Trim();

                                string GE_no = deTextBox15.Text.Trim();
                                string pen_no = deTextBox16.Text.Trim();
                                string promoted_dep = deTextBox7.Text.Trim();
                                string sub_doc_type = deTextBox8.Text.Trim();
                                string index_no = deTextBox9.Text.Trim();
                                string dop = _GetFileCaseDetailsIndividual(projKey, bundleKey, old_filename).Rows[0][16].ToString();
                                string dob = _GetFileCaseDetailsIndividual(projKey, bundleKey, old_filename).Rows[0][13].ToString();
                                string doj = _GetFileCaseDetailsIndividual(projKey, bundleKey, old_filename).Rows[0][14].ToString();
                                string dor = _GetFileCaseDetailsIndividual(projKey, bundleKey, old_filename).Rows[0][15].ToString();

                                if (deTextBox12.Text != "" && deTextBox11.Text != "" && deTextBox10.Text != "")
                                {
                                    bDate = deTextBox12.Text + "-" + deTextBox11.Text + "-" + deTextBox10.Text;
                                }
                                else
                                {
                                    bDate = "";
                                }

                                if (deTextBox21.Text != "" && deTextBox20.Text != "" && deTextBox19.Text != "")
                                {
                                    joinDate = deTextBox21.Text + "-" + deTextBox20.Text + "-" + deTextBox19.Text;
                                }
                                else
                                {
                                    joinDate = "";
                                }

                                if (deTextBox29.Text != "" && deTextBox28.Text != "" && deTextBox27.Text != "")
                                {
                                    rDate = deTextBox29.Text + "-" + deTextBox28.Text + "-" + deTextBox27.Text;
                                }
                                else
                                {
                                    rDate = "";
                                }
                                if (deTextBox18.Text != "" && deTextBox17.Text != "" && deTextBox13.Text != "")
                                {
                                    pDate = deTextBox18.Text + "-" + deTextBox17.Text + "-" + deTextBox13.Text;
                                }
                                else
                                {
                                    pDate = "";
                                }




                                filename = casefile;


                                bool updateMeta = updateMetaEdit(dept, subCat, statename, emp_name, GE_no, pen_no, promoted_dep, sub_doc_type, index_no, bDate, joinDate, pDate,
                                 rDate);
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
                            casefile = deTextBox15.Text.Trim();


                            string statename = deTextBox6.Text.Trim();
                            string emp_name = deTextBox14.Text.Trim();

                            string GE_no = deTextBox15.Text.Trim();
                            string pen_no = deTextBox16.Text.Trim();
                            string promoted_dep = deTextBox7.Text.Trim();
                            string sub_doc_type = deTextBox8.Text.Trim();
                            string index_no = deTextBox9.Text.Trim();
                            string dop = _GetFileCaseDetailsIndividual(projKey, bundleKey, old_filename).Rows[0][16].ToString();
                            string dob = _GetFileCaseDetailsIndividual(projKey, bundleKey, old_filename).Rows[0][13].ToString();
                            string doj = _GetFileCaseDetailsIndividual(projKey, bundleKey, old_filename).Rows[0][14].ToString();
                            string dor = _GetFileCaseDetailsIndividual(projKey, bundleKey, old_filename).Rows[0][15].ToString();

                            if (deTextBox12.Text != "" && deTextBox11.Text != "" && deTextBox10.Text != "")
                            {
                                bDate = deTextBox12.Text + "-" + deTextBox11.Text + "-" + deTextBox10.Text;
                            }
                            else
                            {
                                bDate = "";
                            }

                            if (deTextBox21.Text != "" && deTextBox20.Text != "" && deTextBox19.Text != "")
                            {
                                joinDate = deTextBox21.Text + "-" + deTextBox20.Text + "-" + deTextBox19.Text;
                            }
                            else
                            {
                                joinDate = "";
                            }

                            if (deTextBox29.Text != "" && deTextBox28.Text != "" && deTextBox27.Text != "")
                            {
                                rDate = deTextBox29.Text + "-" + deTextBox28.Text + "-" + deTextBox27.Text;
                            }
                            else
                            {
                                rDate = "";
                            }
                            if (deTextBox18.Text != "" && deTextBox17.Text != "" && deTextBox13.Text != "")
                            {
                                pDate = deTextBox18.Text + "-" + deTextBox17.Text + "-" + deTextBox13.Text;
                            }
                            else
                            {
                                pDate = "";
                            }




                            filename = casefile;


                            bool updateMeta = updateMetaEdit(dept, subCat, statename, emp_name, GE_no, pen_no, promoted_dep, sub_doc_type, index_no, bDate, joinDate, pDate,
                                 rDate);
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
    }
}
