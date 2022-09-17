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
using nControls;

namespace ImageHeaven
{
    public partial class Files : Form
    {
        public static int index;

        Credentials crd = new Credentials();
        //Credentials crd = new Credentials();
        //private OdbcConnection sqlCon;
        OdbcTransaction txn;
        string name = frmMain.name;
        OdbcConnection sqlCon = null;
        public static bool _modeBool;

        public static DataLayerDefs.Mode _mode = DataLayerDefs.Mode._Edit;

        public static string projKey;
        public static string bundleKey;
        public static string casefileNo;
        public static string filename;

        public static string dept;
        public static string category;

        public static string item;

        public Files()
        {
            InitializeComponent();
        }
        public DataTable _GetBundleDetails(string proj, string bundle)
        {
            DataTable dt = new DataTable();
            string sql = "select distinct proj_code, batch_Key, batch_name as 'Batch Name', Batch_code as 'Batch Code',dept_name,category from batch_master where proj_code = '" + proj + "' and batch_key = '" + bundle + "' ";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon, txn);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        public DataTable _GetFileCaseInDetails(string proj, string bundle)
        {
            DataTable dt = new DataTable();
            string sql = "select distinct proj_code, batch_Key,item_no,filename,department,subcat,state_name from metadata_entry where proj_code = '" + proj + "' and batch_key = '" + bundle + "' order by item_no";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon, txn);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        public DataTable _GetFileCaseDetailsIndividual(string proj, string bundle, string fileName)
        {
            DataTable dt = new DataTable();
            string sql = "select distinct proj_code, batch_Key,item_no,filename,department,subcat,state_name,emp_name,desg,fileid,family_pensioner,date_format(birth_date,'%Y-%m-%d'),date_format(joining_date,'%Y-%m-%d'),date_format(death_date,'%Y-%m-%d')," +
                "date_format(retirement_date,'%Y-%m-%d'),psa_name,section,pension_file_no,ppo_fppo,gpo_dgpo,ppo_gpo_cpo,mobile,hrms_id,spouce,place_payment,rule_file,vol,subject,series,acc,subscriber_name," +
                "ledger_year,date_format(accept_date,'%Y-%m-%d'),fp_auth_no,date_format(fp_date,'%Y-%m-%d'),ge_no,pen_no,promoted_dep,sub_doc_type,index_no,date_format(promotion_date,'%Y-%m-%d'),id_no,branch_name from metadata_entry where proj_code = '" + proj + "' and batch_key = '" + bundle + "' and filename = '" + fileName + "' ";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon, txn);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        public Files(OdbcConnection pCon, DataLayerDefs.Mode mode, OdbcTransaction pTxn, Credentials prmCrd)
        {
            InitializeComponent();
            sqlCon = pCon;
            crd = prmCrd;

            txn = pTxn;

            projKey = frmEntrySummary.projKey;
            bundleKey = frmEntrySummary.bundleKey;

            dept = _GetBundleDetails(projKey, bundleKey).Rows[0][4].ToString();
            category = _GetBundleDetails(projKey, bundleKey).Rows[0][5].ToString();

            if (mode == DataLayerDefs.Mode._Edit)
            {
                

                deLabel3.Text = _GetBundleDetails(projKey, bundleKey).Rows[0][3].ToString();
                deLabel4.Text = "Department : " +_GetBundleDetails(projKey, bundleKey).Rows[0][4].ToString();
                deLabel5.Text = "Category : " + _GetBundleDetails(projKey, bundleKey).Rows[0][5].ToString();

                int count = _GetFileCaseInDetails(projKey, bundleKey).Rows.Count;

                for (int i = 0; i < count; i++)
                {

                    string filename = _GetFileCaseInDetails(projKey, bundleKey).Rows[i][3].ToString();
                   
                    //add row
                    string[] row = { filename };
                    var listItem = new ListViewItem(row);

                    lstDeeds.Items.Add(listItem);
                }

                

                _mode = mode;
            }
        }

        private void formatForm()
        {

            this.deTextBox1.AutoCompleteCustomSource = GetSuggestions("metadata_entry", "filename", projKey, bundleKey);
            this.deTextBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.deTextBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;

        }

        private void formatEntryForm()
        {

            this.deTextBox1.AutoCompleteCustomSource = GetSuggestions("metadata_entry", "filename", projKey, bundleKey);
            this.deTextBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.deTextBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;

        }
        public AutoCompleteStringCollection GetSuggestions(string tblName, string fldName, string projKey, string bundleKey)
        {
            AutoCompleteStringCollection x = new AutoCompleteStringCollection();
            string sql = "Select distinct " + fldName + " from " + tblName + " where proj_code = '" + projKey + "' AND batch_key = '" + bundleKey + "'";
            DataSet ds = new DataSet();
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon, txn);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    x.Add(ds.Tables[0].Rows[i][0].ToString().Trim());
                }
            }

            return x;
        }
        private void Files_Load(object sender, EventArgs e)
        {
            if (sqlCon.State == ConnectionState.Closed || sqlCon.State == ConnectionState.Broken)
            {
                sqlCon.Open();
            }
            if (_mode == DataLayerDefs.Mode._Add)
            {
                formatForm();
                if (lstDeeds.Items.Count > 0)
                {
                    lstDeeds.Items[0].Selected = true;
                    lstDeeds.Items[0].Focused = true;
                    lstDeeds.Select();
                    lstDeeds.Items[0].EnsureVisible();
                }
            }
            if (_mode == DataLayerDefs.Mode._Edit)
            {
                formatEntryForm();
                if (lstDeeds.Items.Count > 0)
                {
                    lstDeeds.Items[0].Selected = true;
                    lstDeeds.Items[0].Focused = true;
                    lstDeeds.Select();
                    lstDeeds.Items[0].EnsureVisible();
                }
            }
        }

        private void Files_KeyUp(object sender, KeyEventArgs e)
        {
            if (sqlCon.State == ConnectionState.Closed || sqlCon.State == ConnectionState.Broken)
            {
                sqlCon.Open();
            }
            if (e.KeyCode == Keys.Escape)
            {
               
                this.Close();
            }

        }

        private void lstDeeds_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstDeeds.SelectedItems.Count > 0)
            {
                //string casefileno = lstDeeds.SelectedItems[0].SubItems[0].Text;
                //string item = lstDeeds.SelectedItems[0].SubItems[1].Text;

                string filename = lstDeeds.SelectedItems[0].SubItems[0].Text;

                if(category == "Pension Case File")
                {
                    string state = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][6].ToString();
                    string emp_name = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][7].ToString();
                    string desg = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][8].ToString();
                    string fileid = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][9].ToString();
                    string familyPensioner = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][10].ToString();
                    string dob = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][11].ToString();
                    string doj = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][12].ToString();
                    string dod = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][13].ToString();
                    string dor = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][14].ToString();
                    string psa = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][15].ToString();
                    string sec = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][16].ToString();
                    string fileno = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][17].ToString();
                    string ppo = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][18].ToString();
                    string gpo = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][19].ToString();
                    string mobile = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][21].ToString();
                    string hrms = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][22].ToString();
                    string spouce = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][23].ToString();

                    fileRemarks.Text = "State : " + state + "\nEmployee Name : " + emp_name + "\nDesignation : " + desg +
                        "\nFile-ID : " + fileid + "\nFamily Pensioner : "+ familyPensioner + "\nBirth Date :" + dob + "\nJoining Date : " + doj + "\nRetirement Date : " + dor + "\nDeath Date : " + dod + "\nName of PSA : " + psa +
                        "\nSection : " + sec + "\nPension Case File No : " + fileno + "\nPPO/FPPO No : " + ppo + "\nGPO/DGPO No : " + gpo +
                        "\nMobile : " + mobile + "\nHRMS ID : " + hrms + "\nSpouse Name : " + spouce;
                        
                }
                else if(category == "Pension Case Registers")
                {
                    string state = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][6].ToString();
                    string emp_name = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][7].ToString();
                    string desg = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][8].ToString();

                    string dob = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][11].ToString();
                    string doj = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][12].ToString();
                    string dod = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][13].ToString();
                    string dor = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][14].ToString();
                    string psa = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][15].ToString();
                    
                    string fileno = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][17].ToString();
                    string ppo = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][20].ToString();
                    
                    string place = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][24].ToString();

                    fileRemarks.Text = "State : " + state + "\nPPO/GPO/CPO/Item No : " + ppo + "\nPension Case File No : " + fileno + "\nEmployee Name : " + emp_name + "\nDesignation : " + desg +
                        "\nBirth Date :" + dob + "\nJoining Date : " + doj + "\nDeath Date : " + dod + "\nRetirement Date : " + dor + "\nName of PSA : " + psa +
                        "\nPlace of Payment : " + place;
                        

                }
                else if(category == "Pension Rule Files")
                {
                    string state = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][6].ToString();
                    string fileno = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][25].ToString();
                    string vol = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][26].ToString();
                    string subject = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][27].ToString();

                    fileRemarks.Text = "State : " + state + "\nFile Number : " + fileno + "\nVolume No : " + vol + "\nSubject : " + subject;
                      
                }
                else if(category == "Ledger Cards")
                {
                    string state = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][6].ToString();
                    string series = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][28].ToString();
                    string acc = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][29].ToString();
                    string name = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][30].ToString();
                    string dob = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][11].ToString();
                    string year = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][31].ToString();

                    fileRemarks.Text = "State : " + state + "\nSeries : " + series + "\nAccount No : " + acc + "\nSubscriber's Name : " + name +
                        "\nDate of Birth : " + dob + "\nYear : " + year;
                }
                else if(category == "Nomination")
                {
                    string state = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][6].ToString();
                    string sec = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][16].ToString();
                    string series = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][28].ToString();
                    string acc = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][29].ToString();
                    string name = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][30].ToString();
                    string dob = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][11].ToString();
                    string doa = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][32].ToString();
                    string mobile = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][21].ToString();
                    string hrms = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][22].ToString();

                    fileRemarks.Text = "State : " + state + "\nSection : "+ sec + "\nSeries : " + series + "\nAccount No : " + acc + "\nSubscriber's Name : " + name +
                        "\nDate of Birth : " + dob + "\nDate of Acceptance : " + doa + "\nHRMS ID : " + hrms + "\nMobile No : " + mobile;
                }
                else if(category == "Final Payment Case File")
                {
                    string state = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][6].ToString();
                    string sec = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][16].ToString();
                    string series = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][28].ToString();
                    string acc = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][29].ToString();
                    string name = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][30].ToString();
                    string dob = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][11].ToString();
                    string doa = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][32].ToString();
                    string mobile = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][21].ToString();
                    string hrms = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][22].ToString();
                    string authno = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][33].ToString();
                    string fpdate = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][34].ToString();

                    fileRemarks.Text = "State : " + state + "\nSection : " + sec + "\nSeries : " + series + "\nAccount No : " + acc + "\nSubscriber's Name : " + name +
                        "\nDate of Birth : " + dob + "\nDate of Acceptance : " + doa + "\nHRMS ID : " + hrms + "\nMobile No : " + mobile +
                        "\nFP Authority No : " + authno + "\nFP Date : " + fpdate;
                }
                else if (dept.ToLower()=="ge") {

                    string state = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][6].ToString();
                    string emp_name = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][7].ToString();
                    string GE_no = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][35].ToString();
                    string pen_no = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][36].ToString();
                    string promoted_dep = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][37].ToString();
                    string sub_doc_type = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][38].ToString();
                    string index_no = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][39].ToString();
                    string dop = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][40].ToString();
                    string dob = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][11].ToString();
                    string doj = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][12].ToString();
                    string dor = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][14].ToString();

                    fileRemarks.Text = "State : " + state + "\nEmployee Name : " + emp_name + "\nGE Number : " + GE_no + "\nPen No : " + pen_no +
                        "\nPromoted Department : "+promoted_dep + "\nSub Document Type : " + sub_doc_type + "\nIndex No : " + index_no +
                        "\nDate of Birth : " + dob + "\nDate of Joining : " + doj + "\nDate of Promotion : " + dop +
                        "\nDate of Retirement : " + dor;
                }
                else if (dept.ToLower() == "admin") {
                    string state = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][6].ToString();
                    string emp_name = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][7].ToString();
                    string id_no = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][41].ToString();
                    string desgn = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0]["desg"].ToString();
                    string branch = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][42].ToString();
                    string sub_doc_type = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][38].ToString();
                    string dob = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][11].ToString();
                    string doj = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][12].ToString();
                    string dor = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][14].ToString();

                    fileRemarks.Text = "State : " + state + "\nEmployee Name : " + emp_name + "\nID Number : " + id_no + "\nDesignation : " + desgn +
                        "\nSub Document Type : " + sub_doc_type + "\nBranch Name : " + branch +
                        "\nDate of Birth : " + dob + "\nDate of Joining : " + doj + 
                        "\nDate of Retirement : " + dor;
                }
                else
                {
                    fileRemarks.Text = "";
                }
                //string casefileno = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][3].ToString();
                //string state = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][4].ToString();
                //string emp_name = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][5].ToString();
                //string ppo = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][6].ToString();
                //string joining_date = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][7].ToString();
                //string retirement_date = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][8].ToString();
                //string dept = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][9].ToString();
                //string acc = _GetFileCaseDetailsIndividual(projKey, bundleKey, filename).Rows[0][10].ToString();

                //fileRemarks.Text = "File Index Number : " + casefileno + "\nState : " + state + "\nEmployee Name : " + emp_name +
                //    "\nJoining Date : " + joining_date +"\nRetirement Date : "+retirement_date;
            }
            else
            {
                fileRemarks.Text = "";
            }
        }

        private void lstDeeds_KeyUp(object sender, KeyEventArgs e)
        {
            if (lstDeeds.Items.Count > 0)
            {
                if (lstDeeds.SelectedItems.Count > 0)
                {
                    if (e.Control == true && e.KeyCode == Keys.O)
                    {
                        lstDeeds_DoubleClick(sender, e);
                    }
                    if (e.KeyCode == Keys.Enter)
                    {
                        //lstDeeds_DoubleClick(sender, e);
                    }
                    if (e.KeyCode == Keys.Space)
                    {
                        lstDeeds_DoubleClick(sender, e);
                    }
                }
            }
        }

        private void deTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(deTextBox1.Text.ToUpper().Trim()))
                {
                    for (int i = 0; i < lstDeeds.Items.Count; i++)
                    {
                        if (lstDeeds.Items[i].ToString().Contains(deTextBox1.Text.ToUpper().Trim()))
                        {
                            lstDeeds.Items[i].Selected = true;
                            lstDeeds.Items[i].Focused = true;
                            lstDeeds.Select();
                            lstDeeds.Items[i].EnsureVisible();
                            //lstDeeds.SetSelected(i, true);
                        }
                    }
                }
            }
        }

        private void deTextBox1_Enter(object sender, EventArgs e)
        {
            deTextBox1.SelectAll();
        }

        private void lstDeeds_DoubleClick(object sender, EventArgs e)
        {
            if (lstDeeds.SelectedItems.Count > 0)
            {
                index = lstDeeds.FocusedItem.Index;

                filename = lstDeeds.Items[index].SubItems[0].Text;
                //item = lstDeeds.Items[index].SubItems[1].Text;

                //if (_mode == DataLayerDefs.Mode._Add)
                //{
                //    //this.Hide();
                //    //EntryForm frm = new EntryForm(sqlCon, _mode, filename, txn, crd);
                //    //frm.ShowDialog(this);
                //    frmNewCase fm = new frmNewCase(projKey, bundleKey, sqlCon,crd, DataLayerDefs.Mode._Edit,filename,"Entry");
                //    fm.ShowDialog();

                //    lstDeeds.Items.Clear();

                //    int count = _GetFileCaseInDetails(projKey, bundleKey).Rows.Count;

                //    for (int i = 0; i < count; i++)
                //    {

                //        string filename = _GetFileCaseInDetails(projKey, bundleKey).Rows[i][3].ToString();

                //        //add row
                //        string[] row = { filename };
                //        var listItem = new ListViewItem(row);

                //        lstDeeds.Items.Add(listItem);
                //    }
                //}

                if (_mode == DataLayerDefs.Mode._Edit)
                {
                    //this.Hide();
                    //EntryForm frm = new EntryForm(sqlCon, _mode, filename, txn, crd);
                    //frm.ShowDialog(this);
                    this.SetTopLevel(false);
                    if (category == "Pension Case File")
                    {
                        frmPensionCaseFile fm1 = new frmPensionCaseFile(projKey, bundleKey, sqlCon, crd, DataLayerDefs.Mode._Edit, filename, "Entry");
                        fm1.ShowDialog();

                    }
                    else if (category == "Pension Case Registers")
                    {

                        frmPensionCaseRegister fm1 = new frmPensionCaseRegister(projKey, bundleKey, sqlCon, crd, DataLayerDefs.Mode._Edit, filename, "Entry");
                        fm1.ShowDialog();

                    }
                    else if (category == "Pension Rule Files")
                    {
                        frmPensionRule fm1 = new frmPensionRule(projKey, bundleKey, sqlCon, crd, DataLayerDefs.Mode._Edit, filename, "Entry");
                        fm1.ShowDialog();
                    }
                    else if (category == "Ledger Cards")
                    {
                        frmGPFLCards fm1 = new frmGPFLCards(projKey, bundleKey, sqlCon, crd, DataLayerDefs.Mode._Edit, filename, "Entry");
                        fm1.ShowDialog();
                    }
                    else if (category == "Nomination")
                    {
                        frmNomination fm1 = new frmNomination(projKey, bundleKey, sqlCon, crd, DataLayerDefs.Mode._Edit, filename, "Entry");
                        fm1.ShowDialog();
                    }
                    else if (category == "Final Payment Case File")
                    {
                        frmFPCase fm1 = new frmFPCase(projKey, bundleKey, sqlCon, crd, DataLayerDefs.Mode._Edit, filename, "Entry");
                        fm1.ShowDialog();
                    }
                    else if(dept.ToLower()=="ge")
                    {
                        frmGE fm1 = new frmGE(projKey, bundleKey, sqlCon, crd, DataLayerDefs.Mode._Edit, filename, "Entry");
                        fm1.ShowDialog();
                    }
                    else if (dept.ToLower()=="admin")
                    {
                        frmAD fm1 = new frmAD(projKey, bundleKey, sqlCon, crd, DataLayerDefs.Mode._Edit, filename, "Entry");
                        fm1.ShowDialog();
                    }
                    //frmNewCase fm = new frmNewCase(projKey, bundleKey, sqlCon, crd, DataLayerDefs.Mode._Edit,filename,"Entry");
                    //fm.ShowDialog();
                    this.SetTopLevel(true);

                    lstDeeds.Items.Clear();

                    int count = _GetFileCaseInDetails(projKey, bundleKey).Rows.Count;

                    for (int i = 0; i < count; i++)
                    {

                        string filename = _GetFileCaseInDetails(projKey, bundleKey).Rows[i][3].ToString();

                        //add row
                        string[] row = { filename };
                        var listItem = new ListViewItem(row);

                        lstDeeds.Items.Add(listItem);
                    }
                    if (lstDeeds.Items.Count > 0)
                    {
                        lstDeeds.Items[0].Selected = true;
                        lstDeeds.Items[0].Focused = true;
                        lstDeeds.Select();
                        lstDeeds.Items[0].EnsureVisible();
                    }
                    else
                    {
                        fileRemarks.Text = string.Empty;
                    }

                }
            }
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            string text = deTextBox1.Text;

            for (int i = 0; i < lstDeeds.Items.Count; i++)
            {
                if (lstDeeds.Items[i].SubItems[0].Text.Equals(text))
                {

                    lstDeeds.Items[i].Selected = true;
                    lstDeeds.Items[i].Focused = true;
                    lstDeeds.Select();
                    lstDeeds.Items[i].EnsureVisible();
                    return;
                }
                else
                {
                    lstDeeds.Items[i].Selected = false;
                }

            }
        }

        public DataTable _GetFileCaseDetailsIndividualStatus(string proj, string bundle, string fileName)
        {
            DataTable dt = new DataTable();
            string sql = "select distinct status from metadata_entry where proj_code = '" + proj + "' and batch_key = '" + bundle + "' and filename = '" + fileName + "'  ";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon, txn);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        private void lstDeeds_MouseClick(object sender, MouseEventArgs e)
        {
            if (crd.role == ihConstants._ADMINISTRATOR_ROLE)
            {
                if (_GetFileCaseDetailsIndividualStatus(projKey, bundleKey, lstDeeds.SelectedItems[0].Text).Rows[0][0].ToString() == "0")
                {
                    if (e.Button == MouseButtons.Right)
                    {
                        if (lstDeeds.FocusedItem.Bounds.Contains(e.Location) == true)
                        {
                            cmsDeeds.Show(Cursor.Position);
                        }
                    }
                }
            }
        }

        private void updateDeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        public bool deleteMeta(string proj, string bundle, string fileName)
        {
            bool ret = false;
            if (ret == false)
            {
                _deleteMeta(fileName);

                ret = true;
            }
            return ret;
        }
        public bool _deleteMeta(string fileName)
        {
            string sqlStr = null;

            OdbcCommand sqlCmd = new OdbcCommand();

            bool retVal = false;
            string sql = string.Empty;


            sqlStr = "DELETE from metadata_entry WHERE proj_code = '" + projKey + "' AND batch_key = '" + bundleKey + "' and filename = '" + fileName + "' ";
            //sqlCmd.Connection = sqlCon;
            //sqlCmd.Transaction = txn;
            //sqlCmd.CommandText = sqlStr;
            //int j = sqlCmd.ExecuteNonQuery();
            //if (j > 0)
            //{
            //    retVal = true;
            //}
            //else
            //{
            //    retVal = false;
            //}
            System.Diagnostics.Debug.Print(sqlStr);
            OdbcCommand cmd = new OdbcCommand(sqlStr, sqlCon);
            //cmd.Connection = sqlCon;
            //cmd.CommandText = sqlStr;
            if (cmd.ExecuteNonQuery() > 0)
            {
                retVal = true;
                //txn.Commit();
            }
            //return retVal;
            //sqlCmd.Connection = sqlCon;
            //sqlCmd.Transaction = txn;
            //sqlCmd.CommandText = sqlStr;
            //int j = sqlCmd.ExecuteNonQuery();
            //if (j > 0)
            //{
            //    retVal = true;
            //}
            //else
            //{
            //    retVal = false;
            //}

            return retVal;
        }

        public string GetPath(int prmProjKey, int prmBatchKey)
        {
            string sqlStr = null;
            DataSet projDs = new DataSet();
            string Path;
            OdbcDataAdapter sqlAdap;
            try
            {
                sqlStr = @"select batch_path from batch_master where proj_code=" + prmProjKey + " and batch_key=" + prmBatchKey;
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(projDs);
            }
            catch (Exception ex)
            {

            }
            if (projDs.Tables[0].Rows.Count > 0)
            {
                Path = projDs.Tables[0].Rows[0]["batch_path"].ToString();
            }
            else
                Path = string.Empty;

            return Path;
        }
        public bool deleteImage(string proj, string bundle, string fileName)
        {
            bool ret = false;
            if (ret == false)
            {
                _deleteImage(fileName);

                ret = true;
            }
            return ret;
        }
        public bool deleteTrans(string proj, string bundle, string fileName)
        {
            bool ret = false;
            if (ret == false)
            {
                _deleteTrans(fileName);

                ret = true;
            }
            return ret;
        }
        public bool _deleteImage(string fileName)
        {
            string sqlStr = null;

            OdbcCommand sqlCmd = new OdbcCommand();

            bool retVal = false;
            string sql = string.Empty;


            sqlStr = "DELETE from image_master WHERE proj_key = '" + projKey + "' AND batch_key = '" + bundleKey + "' and policy_number = '" + fileName + "' ";
            //sqlCmd.Connection = sqlCon;
            //sqlCmd.Transaction = txn;
            //sqlCmd.CommandText = sqlStr;
            //int j = sqlCmd.ExecuteNonQuery();
            //if (j > 0)
            //{
            //    retVal = true;
            //}
            //else
            //{
            //    retVal = false;
            //}
            System.Diagnostics.Debug.Print(sqlStr);
            OdbcCommand cmd = new OdbcCommand(sqlStr, sqlCon);
            //cmd.Connection = sqlCon;
            //cmd.CommandText = sqlStr;
            if (cmd.ExecuteNonQuery() > 0)
            {
                retVal = true;
                //txn.Commit();
            }
            //return retVal;
            //sqlCmd.Connection = sqlCon;
            //sqlCmd.Transaction = txn;
            //sqlCmd.CommandText = sqlStr;
            //int j = sqlCmd.ExecuteNonQuery();
            //if (j > 0)
            //{
            //    retVal = true;
            //}
            //else
            //{
            //    retVal = false;
            //}

            return retVal;
        }
        public bool _deleteTrans(string fileName)
        {
            string sqlStr = null;

            OdbcCommand sqlCmd = new OdbcCommand();

            bool retVal = false;
            string sql = string.Empty;


            sqlStr = "DELETE from transaction_log WHERE proj_key = '" + projKey + "' AND batch_key = '" + bundleKey + "' and policy_number = '" + fileName + "' ";
            //sqlCmd.Connection = sqlCon;
            //sqlCmd.Transaction = txn;
            //sqlCmd.CommandText = sqlStr;
            //int j = sqlCmd.ExecuteNonQuery();
            //if (j > 0)
            //{
            //    retVal = true;
            //}
            //else
            //{
            //    retVal = false;
            //}
            System.Diagnostics.Debug.Print(sqlStr);
            OdbcCommand cmd = new OdbcCommand(sqlStr, sqlCon);
            //cmd.Connection = sqlCon;
            //cmd.CommandText = sqlStr;
            if (cmd.ExecuteNonQuery() > 0)
            {
                retVal = true;
                //txn.Commit();
            }
            //return retVal;
            //sqlCmd.Connection = sqlCon;
            //sqlCmd.Transaction = txn;
            //sqlCmd.CommandText = sqlStr;
            //int j = sqlCmd.ExecuteNonQuery();
            //if (j > 0)
            //{
            //    retVal = true;
            //}
            //else
            //{
            //    retVal = false;
            //}

            return retVal;
        }
        public bool deleteCusEx(string proj, string bundle, string fileName)
        {
            bool ret = false;
            if (ret == false)
            {
                _deleteCusEx(fileName);

                ret = true;
            }
            return ret;
        }
        public bool _deleteCusEx(string fileName)
        {
            string sqlStr = null;

            OdbcCommand sqlCmd = new OdbcCommand();

            bool retVal = false;
            string sql = string.Empty;


            sqlStr = "DELETE from custom_exception WHERE proj_key = '" + projKey + "' AND batch_key = '" + bundleKey + "' and policy_number = '" + fileName + "' ";
            //sqlCmd.Connection = sqlCon;
            //sqlCmd.Transaction = txn;
            //sqlCmd.CommandText = sqlStr;
            //int j = sqlCmd.ExecuteNonQuery();
            //if (j > 0)
            //{
            //    retVal = true;
            //}
            //else
            //{
            //    retVal = false;
            //}
            System.Diagnostics.Debug.Print(sqlStr);
            OdbcCommand cmd = new OdbcCommand(sqlStr, sqlCon);
            //cmd.Connection = sqlCon;
            //cmd.CommandText = sqlStr;
            if (cmd.ExecuteNonQuery() > 0)
            {
                retVal = true;
                //txn.Commit();
            }
            //return retVal;
            //sqlCmd.Connection = sqlCon;
            //sqlCmd.Transaction = txn;
            //sqlCmd.CommandText = sqlStr;
            //int j = sqlCmd.ExecuteNonQuery();
            //if (j > 0)
            //{
            //    retVal = true;
            //}
            //else
            //{
            //    retVal = false;
            //}

            return retVal;
        }
        public bool deleteQa(string proj, string bundle, string fileName)
        {
            bool ret = false;
            if (ret == false)
            {
                _deleteQa(fileName);

                ret = true;
            }
            return ret;
        }
        public bool _deleteQa(string fileName)
        {
            string sqlStr = null;

            OdbcCommand sqlCmd = new OdbcCommand();

            bool retVal = false;
            string sql = string.Empty;


            sqlStr = "DELETE from lic_qa_log WHERE proj_key = '" + projKey + "' AND batch_key = '" + bundleKey + "' and policy_number = '" + fileName + "' ";

            System.Diagnostics.Debug.Print(sqlStr);
            OdbcCommand cmd = new OdbcCommand(sqlStr, sqlCon);
            //cmd.Connection = sqlCon;
            //cmd.CommandText = sqlStr;
            if (cmd.ExecuteNonQuery() > 0)
            {
                retVal = true;
                //txn.Commit();
            }

            return retVal;
        }


        private void deleteDeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (crd.role == ihConstants._ADMINISTRATOR_ROLE)
            {

                if (_GetFileCaseDetailsIndividualStatus(projKey, bundleKey, lstDeeds.SelectedItems[0].Text).Rows[0][0].ToString() == "0")
                {
                    DialogResult dr = MessageBox.Show(this, "Do you want to delete this file ? ", "B'Zer - CAG ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        bool deletemeta = deleteMeta(projKey, bundleKey, lstDeeds.SelectedItems[0].Text);
                        

                        if (deletemeta == true  == true)
                        {
                            
                            bool delImg = deleteImage(projKey, bundleKey, lstDeeds.SelectedItems[0].Text);
                            bool delTran = deleteTrans(projKey, bundleKey, lstDeeds.SelectedItems[0].Text);
                            bool delCusEx = deleteCusEx(projKey, bundleKey, lstDeeds.SelectedItems[0].Text);
                            bool delQa = deleteQa(projKey, bundleKey, lstDeeds.SelectedItems[0].Text);

                            if (delImg == true && delTran == true && delCusEx == true && delQa == true)
                            {
                                string path1 = GetPath(Convert.ToInt32(projKey), Convert.ToInt32(bundleKey));
                                string path = path1 + "\\" + lstDeeds.SelectedItems[0].Text;
                                if (Directory.Exists(path))
                                {
                                    Directory.Delete(path, true);
                                }
                            }

                            lstDeeds.Items.Clear();

                            int count = _GetFileCaseInDetails(projKey, bundleKey).Rows.Count;

                            for (int i = 0; i < count; i++)
                            {

                                string filename = _GetFileCaseInDetails(projKey, bundleKey).Rows[i][3].ToString();

                                //add row
                                string[] row = { filename };
                                var listItem = new ListViewItem(row);

                                lstDeeds.Items.Add(listItem);
                            }
                            if (lstDeeds.Items.Count > 0)
                            {
                                lstDeeds.Items[0].Selected = true;
                                lstDeeds.Items[0].Focused = true;
                                lstDeeds.Select();
                                lstDeeds.Items[0].EnsureVisible();
                            }
                            else
                            {
                                fileRemarks.Text = string.Empty;
                            }
                            MessageBox.Show(this, "File deleted successfully ...", "B'Zer - CAG !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }


                    }

                }
                else
                {
                    MessageBox.Show(this, "This File is proceed for further process", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }
    }
}
