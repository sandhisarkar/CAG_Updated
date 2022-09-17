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

namespace ImageHeaven
{
    public partial class frmBatchDetail : Form
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

        public frmBatchDetail()
        {
            InitializeComponent();
        }

        public frmBatchDetail(string proj, string bundle, OdbcConnection pCon, Credentials pcrd, DataLayerDefs.Mode mode, eSTATES[] prmState)
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        public void init()
        {

            deTextBox1.Text = _GetBundleDetails().Rows[0][0].ToString();
            deTextBox2.Text = _GetBundleDetails().Rows[0][1].ToString();

            deTextBox3.Text = _GetBundleDetails().Rows[0][2].ToString();

            deTextBox4.Text = _GetBundleDetails().Rows[0][3].ToString();

        }

        public DataTable _GetBundleDetails()
        {
            DataTable dt = new DataTable();
            string sql = "select distinct Batch_name ,batch_code,date_format(created_dttm,'%Y-%m-%d'),dept_name from batch_master where proj_code = '" + projKey + "' and batch_key = '" + bundleKey + "'";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        private void populateDeptCat()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            string sql = "select cat_id, cat_name from tbl_category where dept_id IN (select dept_id from tbl_dept where dept_name = '"+deTextBox4.Text.Trim()+"')";

            OdbcDataAdapter odap = new OdbcDataAdapter(sql, sqlCon);
            odap.Fill(dt);


            if (dt.Rows.Count > 0)
            {
                deComboBox1.DataSource = dt;
                deComboBox1.DisplayMember = "cat_name";
                deComboBox1.ValueMember = "cat_id";
            }
            //else
            //{
            //    MessageBox.Show("Add one project first...");
            //}

        }


        private void deLabel4_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            populateDeptCat();

            deComboBox1.Focus();
            deComboBox1.Select();
        }

        private void frmBatchDetail_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void deButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void deButtonSave_Click(object sender, EventArgs e)
        {

            if(_mode == Mode._Add)
            {
                if (deComboBox1.Text != "")
                {

                    batchNumber = deTextBox1.Text;
                    batchCode = deTextBox2.Text;
                    creationDate = deTextBox3.Text;
                    department = deTextBox4.Text;
                    subCategory = deComboBox1.Text.Trim();

                    if (department == "Pension" && subCategory == "Pension Case File")
                    {
                        //pension case file
                        frmPensionCaseFile frm = new frmPensionCaseFile(projKey, bundleKey, sqlCon, crd, Mode._Add, state);
                        frm.Show(this);
                    }
                    else if (department == "Pension" && subCategory == "Pension Case Registers")
                    {
                        //pension case register
                        frmPensionCaseRegister frm = new frmPensionCaseRegister(projKey, bundleKey, sqlCon, crd, Mode._Add, state);
                        frm.Show(this);
                    }
                    else if (department == "Pension" && subCategory == "Pension Rule Files")
                    {
                        //pension rule file
                        frmPensionRule frm = new frmPensionRule(projKey, bundleKey, sqlCon, crd, Mode._Add, state);
                        frm.Show(this);
                    }
                    else if (department == "GPF" && subCategory == "Ledger Cards")
                    {
                        //ledger card
                        frmGPFLCards frm = new frmGPFLCards(projKey, bundleKey, sqlCon, crd, Mode._Add, state);
                        frm.Show(this);
                    }
                    else if (department == "GPF" && subCategory == "Nomination")
                    {
                        //nomination
                        frmNomination frm = new frmNomination(projKey, bundleKey, sqlCon, crd, Mode._Add, state);
                        frm.Show(this);
                    }
                    else if (department == "GPF" && subCategory == "Final Payment Case File")
                    {
                        //final payment
                        frmFPCase frm = new frmFPCase(projKey, bundleKey, sqlCon, crd, Mode._Add, state);
                        frm.Show(this);
                    }
                    //frmNewCase fm = new frmNewCase(projKey, bundleKey, sqlCon, crd, DataLayerDefs.Mode._Add, state);
                    //fm.Show(this);
                }
            }
            

        }
    }
}
