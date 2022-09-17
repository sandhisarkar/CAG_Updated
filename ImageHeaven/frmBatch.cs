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

namespace ImageHeaven
{
    public partial class frmBatch : Form
    {
        protected int mode;
        MemoryStream stateLog;
        private udtBatch objBatch;
        private OdbcDataAdapter sqlAdap = null;
        private DataSet dsPath = null;
        private wfeProject objProj = null;
        private INIReader rd = null;
        private KeyValueStruct udtKeyValue;
        public string err = null;
        private int projCode;
        wfeBatch crtBatch = null;
        OdbcConnection sqlCon = null;
        byte[] tmpWrite;
        public static NovaNet.Utils.exLog.Logger exMailLog = new NovaNet.Utils.exLog.emailLogger("./errLog.log", NovaNet.Utils.exLog.LogLevel.Dev, Constants._MAIL_TO, Constants._MAIL_FROM, Constants._SMTP);
        public static NovaNet.Utils.exLog.Logger exTxtLog = new NovaNet.Utils.exLog.txtLogger("./errLog.log", NovaNet.Utils.exLog.LogLevel.Dev);

        string name = frmMain.name;

        DataLayerDefs.Mode _mode = DataLayerDefs.Mode._Edit;

        public string currentDate;
        public string handoverDate;
        Credentials crd = new Credentials();
        string old_path;

        public frmBatch()
        {
            InitializeComponent();
        }

        public frmBatch(wItem prmCmd, OdbcConnection prmCon, DataLayerDefs.Mode mode, Credentials prmCrd)
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();
            //this.Icon = 
            exMailLog.SetNextLogger(exTxtLog);
            _mode = mode;
            crtBatch = (wfeBatch)prmCmd;
            sqlCon = prmCon;
            crd = prmCrd;
            if (crtBatch.GetMode() == Constants._ADDING)
                this.Text = "B'Zer - CAG (Add Bundle)";
            else
                this.Text = "B'Zer - CAG (Edit Bundle)";
            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
        }

        private void frmBatch_Load(object sender, EventArgs e)
        {
            if (_mode == DataLayerDefs.Mode._Add)
            {
                groupBox3.Enabled = false;
                populateProject();
                button2.Enabled = false;
                deComboBox3.Enabled = false;

                currentDate = DateTime.Now.ToString("yyyy-MM-dd");
                handoverDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            }
            //if (_mode == DataLayerDefs.Mode._Edit)
            //{
            //    this.Text = "B'Zer - CAG (Edit Bundle)";
            //    groupBox3.Enabled = true;
            //    populateProject();

            //    populateEstablishment();

            //    cmbProject.Text = getProjectName(frmEntrySummary.projKey).Rows[0][0].ToString();
            //    groupBox2.Enabled = false;

            //    textBox1.Text = getBundleDetails(frmEntrySummary.projKey, frmEntrySummary.bundleKey).Rows[0][2].ToString();
            //    textBox2.Text = getBundleDetails(frmEntrySummary.projKey, frmEntrySummary.bundleKey).Rows[0][3].ToString();
            //    //txtCreateDate.Text = getBundleDetails(frmEntrySummary.projKey, frmEntrySummary.bundleKey).Rows[0][4].ToString();
            //    //dateTimePicker1.Text = getBundleDetails(frmEntrySummary.projKey, frmEntrySummary.bundleKey).Rows[0][5].ToString();

            //    textBox3.Text = getBundleDetails(frmEntrySummary.projKey, frmEntrySummary.bundleKey).Rows[0][0].ToString();
            //    textBox4.Text = getBundleDetails(frmEntrySummary.projKey, frmEntrySummary.bundleKey).Rows[0][1].ToString();

            //    currentDate = getBundleDetails(frmEntrySummary.projKey, frmEntrySummary.bundleKey).Rows[0][4].ToString();
            //    handoverDate = getBundleDetails(frmEntrySummary.projKey, frmEntrySummary.bundleKey).Rows[0][5].ToString();

            //    old_path = getBundleDetails(frmEntrySummary.projKey, frmEntrySummary.bundleKey).Rows[0][6].ToString();

            //    txtCreateDate.Text = currentDate;
            //    txtHandoverDate.Text = handoverDate;
            //    dateTimePicker1.Text = handoverDate;
            //    dateTimePicker1.Format = DateTimePickerFormat.Custom;
            //    dateTimePicker1.CustomFormat = "yyyy-MM-dd";
            //    dateTimePicker1.Value = Convert.ToDateTime(handoverDate.ToString());
            //    dateTimePicker1.Enabled = true;

            //    textBox2.Focus();
            //    textBox2.Select();

            //    deButton1.Enabled = true;
            //    button2.Enabled = false;
            //}
        }

        private void populateDeptCat()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            string sql = "select cat_id, cat_name from tbl_category where dept_id IN (select dept_id from tbl_dept where dept_name = '" + deComboBox1.Text.Trim() + "')";

            OdbcDataAdapter odap = new OdbcDataAdapter(sql, sqlCon);
            odap.Fill(dt);


            if (dt.Rows.Count > 0)
            {
                deComboBox2.DataSource = dt;
                deComboBox2.DisplayMember = "cat_name";
                deComboBox2.ValueMember = "cat_id";
            }
            else
            {
                deComboBox2.DataSource = null;
            }
            //else
            //{
            //    MessageBox.Show("Add one project first...");
            //}

        }

        private void populateProject()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            string sql = "select proj_key, proj_code from project_master ";

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
                MessageBox.Show("Add one project first...");
            }

        }

        public void populateDept()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            string sql = "select dept_id, dept_name from tbl_dept ";

            OdbcDataAdapter odap = new OdbcDataAdapter(sql, sqlCon);
            odap.Fill(dt);


            if (dt.Rows.Count > 0)
            {
                deComboBox1.DataSource = dt;
                deComboBox1.DisplayMember = "dept_name";
                deComboBox1.ValueMember = "dept_id";

                populateDeptCat();
            }
            //else
            //{
            //    MessageBox.Show("Add one project first...");
            //}
        }

        public DataTable getProjectName(string pcode)
        {
            DataTable dt = new DataTable();
            string sql = "select distinct proj_key,proj_code from project_master where proj_key = '" + pcode + "' ";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        private void cmbProject_Leave(object sender, EventArgs e)
        {

            if (cmbProject.Text == "" || cmbProject.Text == null)
            {
                MessageBox.Show("Please select a project name");
                cmbProject.Focus();
                cmbProject.Select();
            }
            else
            {
                //populateEstablishment();

                groupBox3.Enabled = true;

                //textBox1.Focus();
                //textBox1.Select();
                populateDept();
                txtCreateDate.Text = currentDate;
                //txtHandoverDate.Text = handoverDate;
                //dateTimePicker1.Text = handoverDate;
                //dateTimePicker1.Format = DateTimePickerFormat.Custom;
                //dateTimePicker1.CustomFormat = "yyyy-MM-dd";
                //dateTimePicker1.Value = Convert.ToDateTime(handoverDate.ToString());
            }
        }

        private void cmbProject_MouseLeave(object sender, EventArgs e)
        {

            if (cmbProject.Text == "" || cmbProject.Text == null)
            {
                MessageBox.Show("Please select a project name");
                cmbProject.Focus();
                cmbProject.Select();
            }
            else
            {
                //populateEstablishment();

                groupBox3.Enabled = true;

                //textBox1.Focus();
                //textBox1.Select();
                populateDept();
                txtCreateDate.Text = currentDate;
                //txtHandoverDate.Text = handoverDate;
                //dateTimePicker1.Text = handoverDate;
                //dateTimePicker1.Format = DateTimePickerFormat.Custom;
                //dateTimePicker1.CustomFormat = "yyyy-MM-dd";
                //dateTimePicker1.Value = Convert.ToDateTime(handoverDate.ToString());
            }
        }
        /// <summary>
        /// getBundleCount 
        /// </summary>
        /// <param name="dep"></param>
        /// <param name="subCat"></param>
        /// <returns>string</returns>
        private string getBundleCount(string dep, string subCat)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            string sql = "select Count(*) from batch_master where dept_name = '" + dep + "' and category = '" + subCat + "'";

            OdbcDataAdapter odap = new OdbcDataAdapter(sql, sqlCon);
            odap.Fill(dt);

            int count = Convert.ToInt32(dt.Rows[0][0].ToString());

            string getCount = Convert.ToString(count + 1);

            return getCount;
        }

        /// <summary>
        /// code generation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deButton1_Click(object sender, EventArgs e)
        {
            if (_mode == DataLayerDefs.Mode._Add)
            {
                DateTime temp;
                //string isDate = dateTimePicker1.Text;
                string currDate = DateTime.Now.ToString("yyyy-MM-dd");

                if ((txtCreateDate.Text != "" || txtCreateDate.Text != null))
                {

                    if (deComboBox1.Text != null || deComboBox1.Text != "")
                    {
                        //P_CR_5
                        string depAnno = string.Empty;
                        if (deComboBox1.Text.ToLower() == "ge" || deComboBox1.Text.ToLower() == "admin")
                        {
                            depAnno = deComboBox1.Text.ToString().Substring(0, 2).ToUpper(); // GE or AD
                        }
                        else { depAnno = deComboBox1.Text.ToString().Substring(0, 1).ToUpper(); }//P or G 

                        string subCat = string.Empty;
                        if (deComboBox2.Text == "Pension Case File")
                        { subCat = "CF"; }
                        else if (deComboBox2.Text == "Pension Case Registers")
                        { subCat = "CR"; }
                        else if (deComboBox2.Text == "Pension Rule Files")
                        { subCat = "RR"; }
                        else if (deComboBox2.Text == "Ledger Cards")
                        { subCat = "LC"; }
                        else if (deComboBox2.Text == "Nomination")
                        { subCat = "NM"; }
                        else if (deComboBox2.Text == "Final Payment Case File")
                        { subCat = "FP"; }
                        string bundleCount = string.Empty;
                        if (subCat == "CR")
                        {
                            //deComboBox3.Enabled = true;
                            if (deComboBox3.Text != "")
                            {
                                bundleCount = depAnno + "_" + subCat + "_" + deComboBox3.Text + "_" + getBundleCount(deComboBox1.Text, deComboBox2.Text);
                            }
                            else
                            { return; }
                        }
                        else
                        {
                            if (deComboBox1.Text.ToLower() == "ge" || deComboBox1.Text.ToLower() == "admin")
                            {
                                bundleCount = depAnno + "_" + getBundleCount(deComboBox1.Text, deComboBox2.Text);
                            }
                            else
                            {
                                bundleCount = depAnno + "_" + subCat + "_" + getBundleCount(deComboBox1.Text, deComboBox2.Text);
                            }
                        }



                        string bundleCode = bundleCount;

                        textBox3.Text = bundleCode;
                        textBox4.Text = bundleCode;

                        button2.Enabled = true;

                    }

                }

            }
            //if (_mode == DataLayerDefs.Mode._Edit)
            //{
            //    DateTime temp;

            //    string currDate = DateTime.Now.ToString("yyyy-MM-dd");

            //    if ((txtCreateDate.Text != "" || txtCreateDate.Text != null))
            //    {

            //        //string bundleCount = getBundleDetails(frmEntrySummary.projKey, frmEntrySummary.bundleKey).Rows[0][0].ToString();



            //        //string bundleCode = bundleCount.Substring(0, 8) + "_" + textBox2.Text.ToUpper();

            //        //textBox3.Text = bundleCode;
            //        //textBox4.Text = bundleCode;

            //        //button2.Enabled = true;


            //    }


            //}

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public bool KeyCheck(string prmValue)
        {
            string sqlStr = null;
            OdbcCommand cmd = null;
            bool existsBol = true;

            sqlStr = "select bundle_code from bundle_master where bundle_code='" + prmValue.ToUpper() + "'";
            cmd = new OdbcCommand(sqlStr, sqlCon);
            existsBol = cmd.ExecuteReader().HasRows;

            return existsBol;
        }
        private bool Validate(udtBatch cmd)
        {
            bool validateBol = true;
            //errList = new Hashtable();
            if (cmd.batch_code == string.Empty || KeyCheck(cmd.batch_code) == true)
            {
                validateBol = false;
                //errList.Add("Code", Constants.NOT_VALID);
            }

            if (cmd.batch_name == string.Empty)
            {
                validateBol = false;
                //errList.Add("Name", Constants.NOT_VALID);
            }

            if (cmd.Created_By == string.Empty && mode == Constants._ADDING)
            {
                validateBol = false;
                //errList.Add("Created_By", Constants.NOT_VALID);
            }

            if (cmd.Created_DTTM == string.Empty && mode == Constants._ADDING)
            {
                validateBol = false;
                // errList.Add("Created_DTTM", Constants.NOT_VALID);
            }

            ///Required at the time of editing
            if (cmd.Modified_By == string.Empty && mode == Constants._EDITING)
            {
                validateBol = false;
                //errList.Add("Modified_By", Constants.NOT_VALID);
            }

            if (cmd.Modified_DTTM == string.Empty && mode == Constants._EDITING)
            {
                validateBol = false;
                //errList.Add("Modified_DTTM", Constants.NOT_VALID);
            }

            if (cmd.batch_code.Substring(0, 1).ToUpper() != deComboBox1.Text.Substring(0, 1).ToUpper())
            {
                validateBol = false;
            }

            return validateBol;
        }

        public bool Commit_Bundle()
        {
            string sqlStr = null;
            OdbcTransaction sqlTrans = null;
            bool commitBol = true;
            OdbcCommand sqlCmd = new OdbcCommand();
            string scanbatchPath = null;

            //errList = new Hashtable();
            objProj = new wfeProject(sqlCon);

            dsPath = objProj.GetPath(objBatch.proj_code);

            if (dsPath.Tables[0].Rows.Count > 0)
            {
                scanbatchPath = dsPath.Tables[0].Rows[0]["project_Path"] + "\\" + objBatch.batch_code;
            }

            sqlStr = @"insert into batch_master(proj_code,batch_code,dept_name,category,batch_name,created_by" +
                ",Created_DTTM,batch_path) values(" +
                objBatch.proj_code + ",'" + objBatch.batch_code.ToUpper() + "','" + deComboBox1.Text.Trim() + "','" + deComboBox2.Text.Trim() + "','" + objBatch.batch_name + "'," +
                "'" + objBatch.Created_By + "','" + objBatch.Created_DTTM + "','" +
                scanbatchPath.Replace("\\", "\\\\") + "')";
            try
            {
                if (KeyCheck(objBatch.batch_code) == false)
                {
                    sqlTrans = sqlCon.BeginTransaction();
                    sqlCmd.Connection = sqlCon;
                    sqlCmd.Transaction = sqlTrans;
                    sqlCmd.CommandText = sqlStr;
                    sqlCmd.ExecuteNonQuery();

                    if (mode == Constants._ADDING)
                    {
                        if (FileorFolder.CreateFolder(scanbatchPath) == true)
                        {
                            commitBol = true;
                            sqlTrans.Commit();
                        }
                        else
                        {
                            commitBol = false;
                            sqlTrans.Rollback();
                            rd = new INIReader(Constants.EXCEPTION_INI_FILE_PATH);
                            udtKeyValue.Key = Constants.BATCH_FOLDER_CREATE_ERROR.ToString();
                            udtKeyValue.Section = Constants.BATCH_EXCEPTION_SECTION;
                            string ErrMsg = rd.Read(udtKeyValue);
                            throw new CreateFolderException(ErrMsg);
                        }
                    }
                    else
                    {
                        commitBol = true;
                        sqlTrans.Commit();
                    }
                }
                else
                    commitBol = false;
            }
            catch (Exception ex)
            {
                //errList.Add(Constants.DBERRORTYPE, ex.Message);
                commitBol = false;
                sqlTrans.Rollback();
                sqlCmd.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                exMailLog.Log(ex);
            }
            return commitBol;
        }

        public bool TransferValuesBatch(udtCmd cmd)
        {

            objBatch = (udtBatch)(cmd);
            if (KeyCheck(objBatch.batch_code) == false)
            {
                if (Validate(objBatch) == true)
                {

                    if (Commit_Bundle() == true)
                    {
                        return true;
                    }
                    else
                    {
                        rd = new INIReader(Constants.EXCEPTION_INI_FILE_PATH);
                        udtKeyValue.Key = Constants.SAVE_ERROR.ToString();
                        udtKeyValue.Section = Constants.COMMON_EXCEPTION_SECTION;
                        string ErrMsg = rd.Read(udtKeyValue);
                        throw new DbCommitException(ErrMsg);
                    }
                }
                else
                {
                    //throw new ValidationException(Constants.ValidationException) ;
                    return false;
                }
            }
            else
            {
                rd = new INIReader(Constants.EXCEPTION_INI_FILE_PATH);
                udtKeyValue.Key = Constants.DUPLICATE_KEY_CHECK.ToString();
                udtKeyValue.Section = Constants.COMMON_EXCEPTION_SECTION;
                string ErrMsg = rd.Read(udtKeyValue);
                throw new KeyCheckException(ErrMsg);
            }
        }
        private void ClearAllField()
        {

            txtCreateDate.Text = string.Empty;

            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            button2.Enabled = false;
            cmbProject.Focus();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (sqlCon.State == ConnectionState.Closed || sqlCon.State == ConnectionState.Broken)
            {
                sqlCon.Open();
            }
            if (_mode == DataLayerDefs.Mode._Add)
            {
                if (textBox3.Text == null || textBox3.Text == "")
                {
                    MessageBox.Show("Please generate a Batch Code...");
                    deButton1.Focus();
                }
                else
                {


                    NovaNet.Utils.dbCon dbcon = new NovaNet.Utils.dbCon();
                    udtBatch objBatch = new udtBatch();
                    try
                    {
                        statusStrip1.Items.Clear();
                        crtBatch = new wfeBatch(sqlCon);

                        objBatch.proj_code = Convert.ToInt32(cmbProject.SelectedValue);
                        objBatch.batch_code = textBox3.Text;
                        objBatch.batch_name = textBox4.Text;
                        objBatch.Created_By = crd.created_by;
                        objBatch.Created_DTTM = dbcon.GetCurrenctDTTM(1, sqlCon);

                        if (TransferValuesBatch(objBatch) == true)
                        {
                            MessageBox.Show("Batch SucessFully Created");
                            statusStrip1.Items.Add("Status: Data SucessFully Saved");
                            statusStrip1.ForeColor = System.Drawing.Color.Black;
                            ClearAllField();

                        }
                        else
                        {
                            MessageBox.Show(this, "Data Cannot be Saved", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            statusStrip1.Items.Add("Status: Data Cannot be Saved");
                            statusStrip1.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    catch (KeyCheckException ex)
                    {
                        MessageBox.Show(ex.Message, "B'Zer - CAG", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        stateLog = new MemoryStream();
                        tmpWrite = new System.Text.ASCIIEncoding().GetBytes("Batch Key-" + objBatch.batch_key + "\n" + "project Key-" + objBatch.proj_code + "\n");
                        stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                        //exMailLog.Log(ex, this);
                    }
                    catch (DbCommitException dbex)
                    {
                        MessageBox.Show(dbex.Message, "B'Zer - CAG", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        stateLog = new MemoryStream();
                        tmpWrite = new System.Text.ASCIIEncoding().GetBytes("Error while Commit" + "Batch Key-" + objBatch.batch_key + "\n" + "project Key-" + objBatch.proj_code + "\n");
                        stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                        // exMailLog.Log(dbex, this);
                    }
                    catch (CreateFolderException folex)
                    {
                        MessageBox.Show(folex.Message, "B'Zer - CAG", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        stateLog = new MemoryStream();
                        tmpWrite = new System.Text.ASCIIEncoding().GetBytes("Error while Create Folder" + "Batch Key-" + objBatch.batch_key + "\n" + "project Key-" + objBatch.proj_code + "\n");
                        stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                        // exMailLog.Log(folex, this);
                    }
                    catch (DBConnectionException conex)
                    {
                        MessageBox.Show(conex.Message, "B'Zer - CAG", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        stateLog = new MemoryStream();
                        tmpWrite = new System.Text.ASCIIEncoding().GetBytes("Error while Connection error" + "Batch Key-" + objBatch.batch_key + "\n" + "project Key-" + objBatch.proj_code + "\n");
                        stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                        //exMailLog.Log(conex, this);
                    }
                    catch (INIFileException iniex)
                    {
                        MessageBox.Show(iniex.Message, "B'Zer - CAG", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        stateLog = new MemoryStream();
                        tmpWrite = new System.Text.ASCIIEncoding().GetBytes("Error while INI read error" + "Batch Key-" + objBatch.batch_key + "\n" + "project Key-" + objBatch.proj_code + "\n");
                        stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                        //exMailLog.Log(iniex, this);
                    }




                }
            }
            //if (_mode == DataLayerDefs.Mode._Edit)
            //{
            //    if (textBox3.Text == null || textBox3.Text == "")
            //    {
            //        MessageBox.Show("Please generate a Bundle Code...");
            //        textBox1.Focus();
            //        textBox1.Select();
            //    }
            //    else
            //    {
            //        int len = textBox3.Text.Length;
            //        int startInd = textBox3.Text.IndexOf('_') + 1;
            //        int length = len - startInd;
            //        string bundleNo = textBox3.Text.Substring(startInd, length);

            //        if (bundleNo == textBox2.Text)
            //        {
            //            if (_bundleCodeExists(textBox3.Text) == true)
            //            {
            //                NovaNet.Utils.dbCon dbcon = new NovaNet.Utils.dbCon();
            //                udtBatch objBatch = new udtBatch();
            //                try
            //                {
            //                    statusStrip1.Items.Clear();
            //                    crtBatch = new wfeBatch(sqlCon);

            //                    objBatch.proj_code = Convert.ToInt32(cmbProject.SelectedValue);
            //                    objBatch.batch_code = textBox3.Text;
            //                    objBatch.batch_name = textBox4.Text;
            //                    objBatch.Created_By = name;
            //                    objBatch.Created_DTTM = dbcon.GetCurrenctDTTM(1, sqlCon);

            //                    if (TransferValuesBundleEdit(objBatch, textBox1.Text, textBox2.Text, txtCreateDate.Text, dateTimePicker1.Text) == true)
            //                    {
            //                        statusStrip1.Items.Add("Status: Data SucessFully Saved");
            //                        statusStrip1.ForeColor = System.Drawing.Color.Black;
            //                        ClearAllField();


            //                        MessageBox.Show(this, "Batch Updated Successfully", "Batch Updation", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //                        this.Close();
            //                    }
            //                    else
            //                    {
            //                        statusStrip1.Items.Add("Status: Data Can not be Saved");
            //                        statusStrip1.ForeColor = System.Drawing.Color.Red;
            //                    }
            //                }
            //                catch (KeyCheckException ex)
            //                {
            //                    MessageBox.Show(ex.Message, "B'Zer - CAG", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                    stateLog = new MemoryStream();
            //                    tmpWrite = new System.Text.ASCIIEncoding().GetBytes("Batch Key-" + objBatch.batch_key + "\n" + "project Key-" + objBatch.proj_code + "\n");
            //                    stateLog.Write(tmpWrite, 0, tmpWrite.Length);
            //                    //exMailLog.Log(ex, this);
            //                }
            //                catch (DbCommitException dbex)
            //                {
            //                    MessageBox.Show(dbex.Message, "B'Zer - CAG", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                    stateLog = new MemoryStream();
            //                    tmpWrite = new System.Text.ASCIIEncoding().GetBytes("Error while Commit" + "Batch Key-" + objBatch.batch_key + "\n" + "project Key-" + objBatch.proj_code + "\n");
            //                    stateLog.Write(tmpWrite, 0, tmpWrite.Length);
            //                    // exMailLog.Log(dbex, this);
            //                }
            //                catch (CreateFolderException folex)
            //                {
            //                    MessageBox.Show(folex.Message, "B'Zer - CAG", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                    stateLog = new MemoryStream();
            //                    tmpWrite = new System.Text.ASCIIEncoding().GetBytes("Error while Create Folder" + "Batch Key-" + objBatch.batch_key + "\n" + "project Key-" + objBatch.proj_code + "\n");
            //                    stateLog.Write(tmpWrite, 0, tmpWrite.Length);
            //                    // exMailLog.Log(folex, this);
            //                }
            //                catch (DBConnectionException conex)
            //                {
            //                    MessageBox.Show(conex.Message, "B'Zer - CAG", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                    stateLog = new MemoryStream();
            //                    tmpWrite = new System.Text.ASCIIEncoding().GetBytes("Error while Connection error" + "Batch Key-" + objBatch.batch_key + "\n" + "project Key-" + objBatch.proj_code + "\n");
            //                    stateLog.Write(tmpWrite, 0, tmpWrite.Length);
            //                    //exMailLog.Log(conex, this);
            //                }
            //                catch (INIFileException iniex)
            //                {
            //                    MessageBox.Show(iniex.Message, "B'Zer - CAG", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                    stateLog = new MemoryStream();
            //                    tmpWrite = new System.Text.ASCIIEncoding().GetBytes("Error while INI read error" + "Batch Key-" + objBatch.batch_key + "\n" + "project Key-" + objBatch.proj_code + "\n");
            //                    stateLog.Write(tmpWrite, 0, tmpWrite.Length);
            //                    //exMailLog.Log(iniex, this);
            //                }
            //            }


            //        }
            //        else
            //        {
            //            MessageBox.Show("Bundle Number Mismatch", "B'Zer - CAG", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            textBox2.Focus();
            //            textBox2.Select();
            //            return;
            //        }


            //    }
            //}
        }

        private void deComboBox1_Leave(object sender, EventArgs e)
        {
            if (deComboBox1.Text != "" || deComboBox1.Text != null || deComboBox1.Text != string.Empty || !string.IsNullOrEmpty(deComboBox1.Text))
            {
                if (deComboBox1.Text.ToLower() == "ge" || deComboBox1.Text.ToLower() == "admin")
                {
                    deComboBox2.Enabled = false;
                    deComboBox3.Enabled = false;
                }
                else
                {
                    deComboBox2.Enabled = true;
                }
                populateDeptCat();
            }
        }

        private void deComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (deComboBox1.Text.ToLower() == "ge" || deComboBox1.Text.ToLower() == "admin")
            {
                deComboBox2.Enabled = false;
                deComboBox3.Enabled = false;
            }
            else
            {
                deComboBox2.Enabled = true;
            } 
            populateDeptCat();
        }

        private void deComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (deComboBox2.Text.ToLower() == "pension case registers")
            {
                deComboBox3.Enabled = true;
            }
            else
            {
                deComboBox3.Enabled = false;
            }


        }
    }
}
