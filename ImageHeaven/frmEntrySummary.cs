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

namespace ImageHeaven
{
    public partial class frmEntrySummary : Form
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
        public static string bundleNo;
        public Credentials crd = new Credentials();
        private OdbcConnection sqlCon;
        OdbcTransaction txn;

        public static string delpath;
        eSTATES[] state;
        frmEntrySummary entrySum;

        public frmEntrySummary()
        {
            InitializeComponent();
        }

        public frmEntrySummary(OdbcConnection pCon, Credentials pcrd, eSTATES[] prmState)
        {
            InitializeComponent();

            sqlCon = pCon;

            crd = pcrd;

            state = prmState;

            init();
        }
        private void init()
        {
            DataTable Dt = new DataTable();
            Dt = _GetEntries();

          

            dtGrdVol.DataSource = Dt;


            FormatDataGridView();

            this.dtGrdVol.Refresh();
            
            this.textBox2.Text = "";
            this.textBox2.Focus();

            ArrayList lst = GetTotalDaily(crd.created_by);
            for (int i = 0; i < lst.Count; i++)
            {
                deLabel1.Text = "Today You Have Entered: " + lst[0].ToString() + " Files";
            }
        }

        public DataTable _GetEntries()
        {
            DataTable dt = new DataTable();
            string sql = "select distinct a.proj_code,a.batch_key,a.batch_code as 'Batch Name',a.batch_name as 'Batch Number',count(*) as 'Number of Files' from batch_master a,metadata_entry b where a.proj_code = b.proj_code and a.batch_key = b.batch_key group by a.proj_code,a.batch_key";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        private void FormatDataGridView()
        {
            //Format the Data Grid View
            dtGrdVol.Columns[0].Visible = false;
            dtGrdVol.Columns[1].Visible = false;
            //dtGrdVol.Columns[2].Visible = false;
            //Format Colors


            //Set Autosize on for all the columns
            for (int i = 0; i < dtGrdVol.Columns.Count; i++)
            {
                dtGrdVol.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }


        }

        public ArrayList GetTotalDaily(string name)
        {
            ArrayList totList = new ArrayList();
            string sql = "Select proj_code from metadata_entry where date_format(created_DTTM,'%Y-%m-%d')=date_format(now(),'%Y-%m-%d') and created_by = '" + name + "'";
            //string sql = "Select district_code from deed_details where created_DTTM like now() and created_by = '" + crd.created_by + "'";
            DataSet ds = new DataSet();
            OdbcDataAdapter odap = new OdbcDataAdapter(sql, sqlCon);
            odap.Fill(ds);
            if (ds.Tables.Count > 0)
            { totList.Add(ds.Tables[0].Rows.Count); }
            else { totList.Add("0"); }



            return totList;
        }
        public AutoCompleteStringCollection GetSuggestions(string tblName, string fldName)
        {
            AutoCompleteStringCollection x = new AutoCompleteStringCollection();
            string sql = "Select distinct " + fldName + " from " + tblName;
            DataSet ds = new DataSet();
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    x.Add(ds.Tables[0].Rows[i][0].ToString().Trim());
                }
            }
            //x.Add("Others");
            //x.Add("NA");
            return x;
        }
        private void frmEntrySummary_Load(object sender, EventArgs e)
        {

            this.textBox2.AutoCompleteCustomSource = GetSuggestions("batch_master", "batch_name");
            this.textBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.textBox2.AutoCompleteSource = AutoCompleteSource.CustomSource;

            ArrayList lst = GetTotalDaily(crd.created_by);
            for (int i = 0; i < lst.Count; i++)
            {
                deLabel1.Text = "Today You Have Entered: " + lst[0].ToString() + " Files";
            }
        }

        private void frmEntrySummary_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                init();
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        public DataTable _GetResultBundle(string bundle_no)
        {
            DataTable dt = new DataTable();

            string sql = "select distinct a.proj_code,a.batch_key,a.batch_code as 'Batch Name',a.batch_name as 'Batch Number',count(*) as 'Number of Files' from batch_master a,metadata_entry b where a.proj_code = b.proj_code and a.batch_key = b.batch_key and a.batch_name like '%"+bundle_no+"%' group by a.proj_code,a.batch_key";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            
            string bundle_no = textBox2.Text;

            dtGrdVol.DataSource = null;
            DataTable Dt = new DataTable();

            if (bundle_no != null)
            {
                Dt = _GetResultBundle(bundle_no);

                dtGrdVol.DataSource = Dt;


                FormatDataGridView();

                this.dtGrdVol.Refresh();
                this.textBox2.Focus();

                if (dtGrdVol.Rows.Count > 0)
                {
                    dtGrdVol.Rows[0].Selected = true;
                    dtGrdVol.Focus();

                    return;

                }
                

            }
            else
            {
                //init();

                dtGrdVol.DataSource = null;
            }
        }

        private void cmdReset_Click(object sender, EventArgs e)
        {
            init();
        }

        private void cmdnew_Click(object sender, EventArgs e)
        {
            //this.Hide();
            this.SetTopLevel(false);

            eSTATES[] state = new eSTATES[1];
            state[0] = NovaNet.wfe.eSTATES.METADATA_ENTRY;

            frmBundleSelect frm = new frmBundleSelect(state, sqlCon, txn, crd);
            frm.chkPhotoScan.Visible = false;
            frm.ShowDialog(this);

            projKey = frmBundleSelect.projKey;
            bundleKey = frmBundleSelect.bundleKey;




            if (projKey != null && bundleKey != null)
            {
                if (_GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "0")
                {


                    //frmNewCase fm = new frmNewCase(projKey, bundleKey, sqlCon, crd, DataLayerDefs.Mode._Add, state);
                    //fm.ShowDialog();
                    //this.Close();
                    this.SetTopLevel(true);
                    string dept = _GetBundleStatus(projKey, bundleKey).Rows[0][1].ToString();
                    string category = _GetBundleStatus(projKey, bundleKey).Rows[0][2].ToString();

                    if(category.ToString() == "Pension Case File")
                    {
                        this.SetTopLevel(false);
                        //pension case file

                        frmPensionCaseFile fr = new frmPensionCaseFile(projKey, bundleKey, sqlCon, crd, DataLayerDefs.Mode._Add, state);
                        fr.ShowDialog(this);
                    }
                    else if(category.ToString() == "Pension Case Registers")
                    {
                        this.SetTopLevel(false);
                        //pension case register
                        frmPensionCaseRegister fr = new frmPensionCaseRegister(projKey, bundleKey, sqlCon, crd, DataLayerDefs.Mode._Add, state);
                        fr.ShowDialog(this);
                    }
                    else if (category.ToString() == "Pension Rule Files")
                    {
                        this.SetTopLevel(false);
                        //pension rule file

                        frmPensionRule fr = new frmPensionRule(projKey, bundleKey, sqlCon, crd, DataLayerDefs.Mode._Add, state);
                        fr.ShowDialog(this);
                    }
                    else if (category.ToString() == "Ledger Cards")
                    {
                        this.SetTopLevel(false);

                        //ledger card
                        frmGPFLCards fr = new frmGPFLCards(projKey, bundleKey, sqlCon, crd, DataLayerDefs.Mode._Add, state);
                        fr.ShowDialog(this);
                    }
                    else if (category.ToString() == "Nomination")
                    {
                        this.SetTopLevel(false);
                        //nomination
                        frmNomination fr = new frmNomination(projKey, bundleKey, sqlCon, crd, DataLayerDefs.Mode._Add, state);
                        fr.ShowDialog(this);
                    }
                    else if (category.ToString() == "Final Payment Case File")
                    {
                        this.SetTopLevel(false);
                        //final payment
                        frmFPCase fr = new frmFPCase(projKey, bundleKey, sqlCon, crd, DataLayerDefs.Mode._Add, state);
                        fr.ShowDialog(this);
                    }
                    else if (dept.ToLower().ToString() == "ge")
                    {
                        this.SetTopLevel(false);
                        //ge
                        frmGE fr = new frmGE(projKey, bundleKey, sqlCon, crd, DataLayerDefs.Mode._Add, state);
                        fr.ShowDialog(this);
                    }
                    else if (dept.ToLower().ToString() == "admin")
                    {
                        this.SetTopLevel(false);
                        //admin
                        frmAD fr = new frmAD(projKey, bundleKey, sqlCon, crd, DataLayerDefs.Mode._Add, state);
                        fr.ShowDialog(this);
                    }
                    //frmBatchDetail frmB = new frmBatchDetail(projKey, bundleKey, sqlCon, crd, DataLayerDefs.Mode._Add, state);
                    ////frmB.TopLevel = true;
                    //frmB.ShowDialog(this);
                    //frmB.TopLevel = true;
                    //init();
                    //}
                    //this.Close();
                    //this.SetTopLevel(true);
                    //}
                    this.SetTopLevel(true);
                }
                else
                {
                    MessageBox.Show(this, "This Bundle has been uploaded for the further process...", "B'Zer - CAG", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //frmEntrySummary fm = new frmEntrySummary(sqlCon,crd);
                    //fm.ShowDialog(this);
                    this.SetTopLevel(true);
                    return;
                }

            }
            else
            {
                
                this.SetTopLevel(true);
                return;
            }
        }

        private void dtGrdVol_DoubleClick(object sender, EventArgs e)
        {
            //this.Hide();

            projKey = dtGrdVol.SelectedRows[0].Cells[0].Value.ToString();
            bundleKey = dtGrdVol.SelectedRows[0].Cells[1].Value.ToString();

            //_GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "0" ||---> status check

            if (crd.role == ihConstants._ADMINISTRATOR_ROLE || crd.role == "Supervisor" || crd.role == "Metadata Entry")
            {
                //int fileCount = Convert.ToInt32(dtGrdVol.SelectedRows[0].Cells[4].Value.ToString());
                int entryCount = Convert.ToInt32(dtGrdVol.SelectedRows[0].Cells[4].Value.ToString());
                if (entryCount > 0)
                {

                    
                    if (projKey != null && bundleKey != null)
                    {

                        Form activeChild = this.ActiveMdiChild;
                        if (activeChild == null)
                        {
                            this.SetTopLevel(false);
                            Files fm = new Files(sqlCon, DataLayerDefs.Mode._Edit, txn, crd);

                            fm.ShowDialog();
                        }
                    }
                    else
                    {
                        //frmEntrySummary fm = new frmEntrySummary(sqlCon,crd);
                        //fm.ShowDialog(this);
                        return;
                    }
                    this.SetTopLevel(true);
                }
                else
                {
                    MessageBox.Show(this, "No file is enterd for this Bundle...", "B'Zer - CAG - Entry Check !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //frmEntrySummary fm = new frmEntrySummary(sqlCon,crd);
                    //fm.ShowDialog(this);
                    return;
                }
            }
            else
            {
                MessageBox.Show(this, "You are not authorized to do so ...", "B'Zer - CAG", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //MessageBox.Show(this, "This Bundle has been uploaded for the further process...", "B'Zer - Calcutta High Court", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //frmEntrySummary fm = new frmEntrySummary(sqlCon,crd);
                //fm.ShowDialog(this);
                return;
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

        private void dtGrdVol_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control == true && e.KeyCode == Keys.O)
            {
                dtGrdVol_DoubleClick(sender, e);
            }
            if (e.KeyCode == Keys.Enter)
            {
                //dtGrdVol_DoubleClick(sender, e);
            }
        }
    }
}
