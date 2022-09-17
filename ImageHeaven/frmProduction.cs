using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.ComponentModel;
using WeifenLuo.WinFormsUI.Docking;
using System.Runtime.InteropServices;
using DockSample.Customization;
using System.IO;
using DockSample;
using NovaNet.Utils;
using NovaNet.wfe;
using System.Data;
using System.Data.Odbc;
using System.Collections;
using LItems;
using TwainLib;
using Inlite.ClearImageNet;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft;
using Microsoft.Office;
using Microsoft.Office.Interop.Excel;

namespace ImageHeaven
{
    public partial class frmProduction : Form
    {
        OdbcConnection sqlCon = null;
        public string stDate;
        public string endDate;
        public Credentials crd = new Credentials();

        public frmProduction(OdbcConnection prmCon, Credentials pcrd)
        {
            InitializeComponent();
            sqlCon = prmCon;
            crd = pcrd;
        }

        public frmProduction()
        {
            InitializeComponent();
        }

        private void populateUserType()
        {
            DataSet ds = new DataSet();
            System.Data.DataTable dt = new System.Data.DataTable();

            string sql = "select role_id, role_description from ac_role where role_id = 1 or role_id = 3 or role_id = 5 or role_id = 8 or role_id = 9 or role_id = 10";

            OdbcDataAdapter odap = new OdbcDataAdapter(sql, sqlCon);
            odap.Fill(dt);


            if (dt.Rows.Count > 0)
            {
                deComboBox1.DataSource = dt;
                deComboBox1.DisplayMember = "role_description";
                deComboBox1.ValueMember = "role_id";
            }

        }

        private void populateUserTypeM()
        {
            DataSet ds = new DataSet();
            System.Data.DataTable dt = new System.Data.DataTable();

            string sql = "select role_id, role_description from ac_role where role_id = 5";

            OdbcDataAdapter odap = new OdbcDataAdapter(sql, sqlCon);
            odap.Fill(dt);


            if (dt.Rows.Count > 0)
            {
                deComboBox1.DataSource = dt;
                deComboBox1.DisplayMember = "role_description";
                deComboBox1.ValueMember = "role_id";
            }

        }

        private void populateUserTypeS()
        {
            DataSet ds = new DataSet();
            System.Data.DataTable dt = new System.Data.DataTable();

            string sql = "select role_id, role_description from ac_role where role_id = 1";

            OdbcDataAdapter odap = new OdbcDataAdapter(sql, sqlCon);
            odap.Fill(dt);


            if (dt.Rows.Count > 0)
            {
                deComboBox1.DataSource = dt;
                deComboBox1.DisplayMember = "role_description";
                deComboBox1.ValueMember = "role_id";
            }

        }

        private void populateUserTypeQ()
        {
            DataSet ds = new DataSet();
            System.Data.DataTable dt = new System.Data.DataTable();

            string sql = "select role_id, role_description from ac_role where role_id = 3";

            OdbcDataAdapter odap = new OdbcDataAdapter(sql, sqlCon);
            odap.Fill(dt);


            if (dt.Rows.Count > 0)
            {
                deComboBox1.DataSource = dt;
                deComboBox1.DisplayMember = "role_description";
                deComboBox1.ValueMember = "role_id";
            }

        }

        private void populateUserTypeA1()
        {
            DataSet ds = new DataSet();
            System.Data.DataTable dt = new System.Data.DataTable();

            string sql = "select role_id, role_description from ac_role where role_id = 8";

            OdbcDataAdapter odap = new OdbcDataAdapter(sql, sqlCon);
            odap.Fill(dt);


            if (dt.Rows.Count > 0)
            {
                deComboBox1.DataSource = dt;
                deComboBox1.DisplayMember = "role_description";
                deComboBox1.ValueMember = "role_id";
            }

        }

        private void populateUserTypeA2()
        {
            DataSet ds = new DataSet();
            System.Data.DataTable dt = new System.Data.DataTable();

            string sql = "select role_id, role_description from ac_role where role_id = 9";

            OdbcDataAdapter odap = new OdbcDataAdapter(sql, sqlCon);
            odap.Fill(dt);


            if (dt.Rows.Count > 0)
            {
                deComboBox1.DataSource = dt;
                deComboBox1.DisplayMember = "role_description";
                deComboBox1.ValueMember = "role_id";
            }

        }

        private void populateUserTypeF()
        {
            DataSet ds = new DataSet();
            System.Data.DataTable dt = new System.Data.DataTable();

            string sql = "select role_id, role_description from ac_role where role_id = 10";

            OdbcDataAdapter odap = new OdbcDataAdapter(sql, sqlCon);
            odap.Fill(dt);


            if (dt.Rows.Count > 0)
            {
                deComboBox1.DataSource = dt;
                deComboBox1.DisplayMember = "role_description";
                deComboBox1.ValueMember = "role_id";
            }

        }

        private void frmProduction_Load(object sender, EventArgs e)
        {
            if (crd.role == ihConstants._ADMINISTRATOR_ROLE || crd.role == "Supervisor")
            {
                populateUserType();

                stDate = DateTime.Now.ToString("yyyy-MM-dd");
                endDate = DateTime.Now.ToString("yyyy-MM-dd");
            }
            if (crd.role == "Metadata Entry")
            {
                populateUserTypeM();

                stDate = DateTime.Now.ToString("yyyy-MM-dd");
                endDate = DateTime.Now.ToString("yyyy-MM-dd");
            }
            if (crd.role == "Scan")
            {
                populateUserTypeS();

                stDate = DateTime.Now.ToString("yyyy-MM-dd");
                endDate = DateTime.Now.ToString("yyyy-MM-dd");
            }
            if (crd.role == "QC")
            {
                populateUserTypeQ();

                stDate = DateTime.Now.ToString("yyyy-MM-dd");
                endDate = DateTime.Now.ToString("yyyy-MM-dd");
            }
            if (crd.role == "Audit 1")
            {
                populateUserTypeA1();

                stDate = DateTime.Now.ToString("yyyy-MM-dd");
                endDate = DateTime.Now.ToString("yyyy-MM-dd");
            }
            if (crd.role == "Audit 2")
            {
                populateUserTypeA2();

                stDate = DateTime.Now.ToString("yyyy-MM-dd");
                endDate = DateTime.Now.ToString("yyyy-MM-dd");
            }
            if (crd.role == "Fqc")
            {
                populateUserTypeF();

                stDate = DateTime.Now.ToString("yyyy-MM-dd");
                endDate = DateTime.Now.ToString("yyyy-MM-dd");
            }
            

            deComboBox1.SelectedIndex = 0;

            dateTimePicker1.Text = stDate;
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy-MM-dd";
            dateTimePicker1.Value = Convert.ToDateTime(stDate.ToString());

            dateTimePicker2.Text = endDate;
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "yyyy-MM-dd";
            dateTimePicker2.Value = Convert.ToDateTime(endDate.ToString());
        }

        public System.Data.DataTable _GetEntries()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string sql = "select distinct date_format(created_dttm,'%Y-%m-%d') as 'Entry Date',created_by as 'Entry User' from metadata_entry where date_format(created_dttm,'%Y-%m-%d') >= '" + dateTimePicker1.Text + "' and date_format(created_dttm,'%Y-%m-%d') <= '" + dateTimePicker2.Text + "' order by created_by asc";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        public System.Data.DataTable _GetEntriesScan()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string sql = "select distinct date_format(scanned_dttm,'%Y-%m-%d') as 'Scanned Date',scanned_user as 'Scanned User' from transaction_log where date_format(scanned_dttm,'%Y-%m-%d') >= '" + dateTimePicker1.Text + "' and date_format(scanned_dttm,'%Y-%m-%d') <= '" + dateTimePicker2.Text + "' order by scanned_user asc";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        public System.Data.DataTable _GetEntriesQC()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string sql = "select distinct date_format(qc_dttm,'%Y-%m-%d') as 'QC Date',qc_user as 'QC User' from transaction_log where date_format(qc_dttm,'%Y-%m-%d') >= '" + dateTimePicker1.Text + "' and date_format(qc_dttm,'%Y-%m-%d') <= '" + dateTimePicker2.Text + "' order by qc_user asc";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        public System.Data.DataTable _GetEntriesAudit1()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string sql = "select distinct date_format(created_dttm_Phase1,'%Y-%m-%d') as 'Audit Date',created_by_Phase1 as 'Audit User' from lic_qa_log where (date_format(created_dttm_Phase1,'%Y-%m-%d') >= '" + dateTimePicker1.Text + "' and date_format(created_dttm_Phase1,'%Y-%m-%d') <= '" + dateTimePicker2.Text + "') or (date_format(modified_dttm_Phase1,'%Y-%m-%d') >= '" + dateTimePicker1.Text + "' and date_format(modified_dttm_Phase1,'%Y-%m-%d') <= '" + dateTimePicker2.Text + "') and (qa_status = 0 or qa_status = 1 or qa_status = 2) order by created_by_Phase1 asc";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        public System.Data.DataTable _GetEntriesAudit2()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string sql = "select distinct date_format(created_dttm_Phase2,'%Y-%m-%d') as 'Audit Date',created_by_Phase2 as 'Audit User' from lic_qa_log where (date_format(created_dttm_Phase2,'%Y-%m-%d') >= '" + dateTimePicker1.Text + "' and date_format(created_dttm_Phase2,'%Y-%m-%d') <= '" + dateTimePicker2.Text + "') or (date_format(modified_dttm_Phase2,'%Y-%m-%d') >= '" + dateTimePicker1.Text + "' and date_format(modified_dttm_Phase2,'%Y-%m-%d') <= '" + dateTimePicker2.Text + "') and (qa_status = 0 or qa_status = 1 or qa_status = 2) order by created_by_Phase2 asc";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        public System.Data.DataTable _GetFileCountAudit1(string date, string user)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string sql = "select distinct proj_key,batch_key,box_number,policy_number from lic_qa_log where (date_format(created_dttm_Phase1,'%Y-%m-%d') = '" + date + "' or date_format(modified_dttm_Phase1,'%Y-%m-%d') = '" + date + "') and (created_by_Phase1 = '" + user + "' or modified_by_Phase1 = '" + user + "') ";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        public System.Data.DataTable _GetFileCountAudit2(string date, string user)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string sql = "select distinct proj_key,batch_key,box_number,policy_number from lic_qa_log where (date_format(created_dttm_Phase2,'%Y-%m-%d') = '" + date + "' or date_format(modified_dttm_Phase2,'%Y-%m-%d') = '" + date + "') and (created_by_Phase2 = '" + user + "' or modified_by_Phase2 = '" + user + "') ";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        public System.Data.DataTable _GetFileDetailsQC(string date, string user)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string sql = "select proj_key, batch_key, policy_number from transaction_log where date_format(qc_dttm,'%Y-%m-%d') = '" + date + "' and qc_user = '" + user + "' ";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        public System.Data.DataTable _GetFileDetailsAudit1(string date, string user)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string sql = "select distinct proj_key, batch_key, policy_number from lic_qa_log where (date_format(created_dttm_Phase1,'%Y-%m-%d') = '" + date + "' or date_format(modified_dttm_Phase1,'%Y-%m-%d') = '" + date + "') and (created_by_Phase1 = '" + user + "' or modified_by_Phase1 = '" + user + "') ";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        public System.Data.DataTable _GetFileDetailsAudit2(string date, string user)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string sql = "select distinct proj_key, batch_key, policy_number from lic_qa_log where (date_format(created_dttm_Phase2,'%Y-%m-%d') = '" + date + "' or date_format(modified_dttm_Phase2,'%Y-%m-%d') = '" + date + "') and (created_by_Phase2 = '" + user + "' or modified_by_Phase2 = '" + user + "') ";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        public System.Data.DataTable _GetEntriesFqc()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string sql = "select distinct date_format(fqc_dttm,'%Y-%m-%d') as 'Fqc Date',fqc_user as 'Fqc User' from transaction_log where date_format(fqc_dttm,'%Y-%m-%d') >= '" + dateTimePicker1.Text + "' and date_format(fqc_dttm,'%Y-%m-%d') <= '" + dateTimePicker2.Text + "' order by fqc_user asc";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        public System.Data.DataTable _GetFileDetailsFqc(string date, string user)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string sql = "select proj_key, batch_key, policy_number from transaction_log where date_format(fqc_dttm,'%Y-%m-%d') = '" + date + "' and fqc_user = '" + user + "' ";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        private void init()
        {
            System.Data.DataTable Dt = new System.Data.DataTable();
            Dt = _GetEntries();

            Dt.Columns.Add("Number of Files");

            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                Dt.Rows[i][2] = _GetFileCount(Dt.Rows[i][0].ToString(), Dt.Rows[i][1].ToString());
                
            }

            grdStatus.DataSource = Dt;


            FormatDataGridView();

            this.grdStatus.Refresh();

            if (Dt.Rows.Count > 0)
            {
                deButton20.Enabled = true;
            }
        }

        private void initScan()
        {
            System.Data.DataTable Dt = new System.Data.DataTable();
            Dt = _GetEntriesScan();

            Dt.Columns.Add("Number of Files");
            Dt.Columns.Add("Number of Images");



            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                Dt.Rows[i][2] = _GetFileCountScan(Dt.Rows[i][0].ToString(), Dt.Rows[i][1].ToString());
                Dt.Rows[i][3] = _GetImageCountScan(Dt.Rows[i][0].ToString(), Dt.Rows[i][1].ToString());
            }

            grdStatus.DataSource = Dt;


            FormatDataGridView();

            this.grdStatus.Refresh();

            if (Dt.Rows.Count > 0)
            {
                deButton20.Enabled = true;
            }
        }

        private void initQC()
        {
            System.Data.DataTable Dt = new System.Data.DataTable();
            Dt = _GetEntriesQC();

            Dt.Columns.Add("Number of Files");
            Dt.Columns.Add("Number of Images");



            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                Dt.Rows[i][2] = _GetFileCountQC(Dt.Rows[i][0].ToString(), Dt.Rows[i][1].ToString());

                int count = 0;
                for (int j = 0; j < _GetFileDetailsQC(Dt.Rows[i][0].ToString(), Dt.Rows[i][1].ToString()).Rows.Count; j++)
                {
                    string pk = _GetFileDetailsQC(Dt.Rows[i][0].ToString(), Dt.Rows[i][1].ToString()).Rows[j][0].ToString();

                    string bk = _GetFileDetailsQC(Dt.Rows[i][0].ToString(), Dt.Rows[i][1].ToString()).Rows[j][1].ToString();

                    string pn = _GetFileDetailsQC(Dt.Rows[i][0].ToString(), Dt.Rows[i][1].ToString()).Rows[j][2].ToString();

                    count = count + Convert.ToInt32(_GetImageCountQC(pk, bk, pn).ToString());

                    Dt.Rows[i][3] = count.ToString();

                }
            }

            grdStatus.DataSource = Dt;


            FormatDataGridView();

            this.grdStatus.Refresh();

            if (Dt.Rows.Count > 0)
            {
                deButton20.Enabled = true;
            }
        }

        private void initAudit1()
        {
            System.Data.DataTable Dt = new System.Data.DataTable();
            Dt = _GetEntriesAudit1();

            Dt.Columns.Add("Number of Files");
            Dt.Columns.Add("Number of Images");



            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                Dt.Rows[i][2] = _GetFileCountAudit1(Dt.Rows[i][0].ToString(), Dt.Rows[i][1].ToString()).Rows.Count;

                int count = 0;
                for (int j = 0; j < _GetFileDetailsAudit1(Dt.Rows[i][0].ToString(), Dt.Rows[i][1].ToString()).Rows.Count; j++)
                {
                    string pk = _GetFileDetailsAudit1(Dt.Rows[i][0].ToString(), Dt.Rows[i][1].ToString()).Rows[j][0].ToString();

                    string bk = _GetFileDetailsAudit1(Dt.Rows[i][0].ToString(), Dt.Rows[i][1].ToString()).Rows[j][1].ToString();

                    string pn = _GetFileDetailsAudit1(Dt.Rows[i][0].ToString(), Dt.Rows[i][1].ToString()).Rows[j][2].ToString();

                    count = count + Convert.ToInt32(_GetImageCountAudit1(pk, bk, pn).ToString());

                    Dt.Rows[i][3] = count.ToString();

                }
            }

            grdStatus.DataSource = Dt;


            FormatDataGridView();

            this.grdStatus.Refresh();

            if (Dt.Rows.Count > 0)
            {
                deButton20.Enabled = true;
            }
        }

        private void initAudit2()
        {
            System.Data.DataTable Dt = new System.Data.DataTable();
            Dt = _GetEntriesAudit2();

            Dt.Columns.Add("Number of Files");
            Dt.Columns.Add("Number of Images");



            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                Dt.Rows[i][2] = _GetFileCountAudit2(Dt.Rows[i][0].ToString(), Dt.Rows[i][1].ToString()).Rows.Count;

                int count = 0;
                for (int j = 0; j < _GetFileDetailsAudit2(Dt.Rows[i][0].ToString(), Dt.Rows[i][1].ToString()).Rows.Count; j++)
                {
                    string pk = _GetFileDetailsAudit2(Dt.Rows[i][0].ToString(), Dt.Rows[i][1].ToString()).Rows[j][0].ToString();

                    string bk = _GetFileDetailsAudit2(Dt.Rows[i][0].ToString(), Dt.Rows[i][1].ToString()).Rows[j][1].ToString();

                    string pn = _GetFileDetailsAudit2(Dt.Rows[i][0].ToString(), Dt.Rows[i][1].ToString()).Rows[j][2].ToString();

                    count = count + Convert.ToInt32(_GetImageCountAudit2(pk, bk, pn).ToString());

                    Dt.Rows[i][3] = count.ToString();

                }
            }

            grdStatus.DataSource = Dt;


            FormatDataGridView();

            this.grdStatus.Refresh();

            if (Dt.Rows.Count > 0)
            {
                deButton20.Enabled = true;
            }
        }

        private void initFqc()
        {
            System.Data.DataTable Dt = new System.Data.DataTable();
            Dt = _GetEntriesFqc();

            Dt.Columns.Add("Number of Files");
            Dt.Columns.Add("Number of Images");



            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                Dt.Rows[i][2] = _GetFileCountFqc(Dt.Rows[i][0].ToString(), Dt.Rows[i][1].ToString());

                int count = 0;
                for (int j = 0; j < _GetFileDetailsFqc(Dt.Rows[i][0].ToString(), Dt.Rows[i][1].ToString()).Rows.Count; j++)
                {
                    string pk = _GetFileDetailsFqc(Dt.Rows[i][0].ToString(), Dt.Rows[i][1].ToString()).Rows[j][0].ToString();

                    string bk = _GetFileDetailsFqc(Dt.Rows[i][0].ToString(), Dt.Rows[i][1].ToString()).Rows[j][1].ToString();

                    string pn = _GetFileDetailsFqc(Dt.Rows[i][0].ToString(), Dt.Rows[i][1].ToString()).Rows[j][2].ToString();

                    count = count + Convert.ToInt32(_GetImageCountFqc(pk, bk, pn).ToString());

                    Dt.Rows[i][3] = count.ToString();

                }
            }

            grdStatus.DataSource = Dt;


            FormatDataGridView();

            this.grdStatus.Refresh();

            if (Dt.Rows.Count > 0)
            {
                deButton20.Enabled = true;
            }
        }

        public string _GetFileCount(string date, string user)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string sql = "select COUNT(*) from metadata_entry where date_format(created_dttm,'%Y-%m-%d') = '" + date + "' and created_by = '" + user + "' ";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt.Rows[0][0].ToString();
        }
        public string _GetFileCountScan(string date, string user)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string sql = "select COUNT(*) from transaction_log where date_format(scanned_dttm,'%Y-%m-%d') = '" + date + "' and scanned_user = '" + user + "' ";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt.Rows[0][0].ToString();
        }
        public string _GetImageCountScan(string date, string user)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string sql = "select COUNT(*) from image_master where date_format(created_dttm,'%Y-%m-%d') = '" + date + "' and created_by = '" + user + "' ";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt.Rows[0][0].ToString();
        }
        public string _GetFileCountQC(string date, string user)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string sql = "select COUNT(*) from transaction_log where date_format(qc_dttm,'%Y-%m-%d') = '" + date + "' and qc_user = '" + user + "' ";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt.Rows[0][0].ToString();
        }
        public string _GetImageCountQC(string projk, string batchK, string file)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string sql = "select COUNT(*) from image_master where proj_key =  '" + projk + "' and batch_key = '" + batchK + "' and policy_number = '" + file + "'   ";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt.Rows[0][0].ToString();
        }
        public string _GetImageCountAudit1(string projk, string batchK, string file)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string sql = "select COUNT(*) from image_master where proj_key =  '" + projk + "' and batch_key = '" + batchK + "' and policy_number = '" + file + "' and status <> 29  ";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt.Rows[0][0].ToString();
        }
        public string _GetImageCountAudit2(string projk, string batchK, string file)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string sql = "select COUNT(*) from image_master where proj_key =  '" + projk + "' and batch_key = '" + batchK + "' and policy_number = '" + file + "' and status <> 29  ";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt.Rows[0][0].ToString();
        }
        public string _GetFileCountFqc(string date, string user)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string sql = "select COUNT(*) from transaction_log where date_format(fqc_dttm,'%Y-%m-%d') = '" + date + "' and fqc_user = '" + user + "' ";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt.Rows[0][0].ToString();
        }
        public string _GetImageCountFqc(string projk, string batchK, string file)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string sql = "select COUNT(*) from image_master where proj_key =  '" + projk + "' and batch_key = '" + batchK + "' and policy_number = '" + file + "' and status <> 29  ";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt.Rows[0][0].ToString();
        }

        private void FormatDataGridView()
        {
            

            //Set Autosize on for all the columns
            for (int i = 0; i < grdStatus.Columns.Count; i++)
            {
                grdStatus.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }


        }

        private void deButton1_Click(object sender, EventArgs e)
        {
            if (deComboBox1.Text == "Metadata Entry")
            {
                init();
            }
            if (deComboBox1.Text == "Scan")
            {
                initScan();
            }
            if (deComboBox1.Text == "QC")
            {
                initQC();
            }
            if (deComboBox1.Text == "Audit 1")
            {
                initAudit1();
            }
            if (deComboBox1.Text == "Audit 2")
            {
                initAudit2();
            }
            if (deComboBox1.Text == "Fqc")
            {
                initFqc();
            }
        }

        private void deButton21_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void deButton20_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);

            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

            app.Visible = false;

            worksheet = (Microsoft.Office.Interop.Excel._Worksheet)workbook.Sheets["Sheet1"];


            worksheet = (Microsoft.Office.Interop.Excel._Worksheet)workbook.ActiveSheet;

            worksheet.Name = "Production Report";

            worksheet.Cells[1, 3] = "Production Report";
            Range range44 = worksheet.get_Range("C1");
            range44.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.YellowGreen);

            worksheet.Rows.AutoFit();
            worksheet.Columns.AutoFit();


            worksheet.Cells[3, 1] = "User Role : " + grdStatus.Columns[1].HeaderText.ToString();
            Range range43 = worksheet.get_Range("A3");
            range43.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
            worksheet.Rows.AutoFit();
            worksheet.Columns.AutoFit();

            worksheet.Cells[4, 1] = "Time : " + dateTimePicker1.Text + " - " + dateTimePicker2.Text;
            Range range33 = worksheet.get_Range("A4");
            range33.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
            worksheet.Rows.AutoFit();
            worksheet.Columns.AutoFit();

            Range range = worksheet.get_Range("A3", "A4");
            range.Borders.Color = ColorTranslator.ToOle(Color.Black);



            if (deComboBox1.Text == "Metadata Entry")
            {
                Range range1 = worksheet.get_Range("A6", "C6");
                range1.Borders.Color = ColorTranslator.ToOle(Color.Black);
                int i;
                for (i = 1; i < grdStatus.Columns.Count + 1; i++)
                {


                    Range range2 = worksheet.get_Range("A6", "C6");
                    range2.Borders.Color = ColorTranslator.ToOle(Color.Black);
                    range2.EntireRow.AutoFit();
                    range2.EntireColumn.AutoFit();
                    worksheet.Cells[6, i] = grdStatus.Columns[i - 1].HeaderText;
                }

                worksheet.Cells[8 + grdStatus.Rows.Count, 2] = "Total";

            }
            else
            {
                Range range1 = worksheet.get_Range("A6", "D6");
                range1.Borders.Color = ColorTranslator.ToOle(Color.Black);
                int i;
                for (i = 1; i < grdStatus.Columns.Count + 1; i++)
                {


                    Range range2 = worksheet.get_Range("A6", "D6");
                    range2.Borders.Color = ColorTranslator.ToOle(Color.Black);
                    range2.EntireRow.AutoFit();
                    range2.EntireColumn.AutoFit();
                    worksheet.Cells[6, i] = grdStatus.Columns[i - 1].HeaderText;
                }

                worksheet.Cells[8 + grdStatus.Rows.Count, 2] = "Total";

            }

            int filecount = 0;
            int imgcount = 0;
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
                if (deComboBox1.Text == "Metadata Entry")
                {
                    filecount = filecount + Convert.ToInt32(grdStatus.Rows[i].Cells[2].Value);
                }
                else
                {
                    filecount = filecount + Convert.ToInt32(grdStatus.Rows[i].Cells[2].Value);
                    imgcount = imgcount + Convert.ToInt32(grdStatus.Rows[i].Cells[3].Value);
                }
            }

            if (deComboBox1.Text == "Metadata Entry")
            {
                Range range3 = worksheet.Cells;
                //range3.Borders.Color = ColorTranslator.ToOle(Color.Black);
                range3.EntireRow.AutoFit();
                range3.EntireColumn.AutoFit();
                worksheet.Cells[8 + grdStatus.Rows.Count, 3] = filecount.ToString();
                worksheet.Cells[8 + grdStatus.Rows.Count, 2].Borders.Color = ColorTranslator.ToOle(Color.Black);
                worksheet.Cells[8 + grdStatus.Rows.Count, 3].Borders.Color = ColorTranslator.ToOle(Color.Black);
            }
            else
            {
                Range range3 = worksheet.Cells;
                //range3.Borders.Color = ColorTranslator.ToOle(Color.Black);
                range3.EntireRow.AutoFit();
                range3.EntireColumn.AutoFit();
                worksheet.Cells[8 + grdStatus.Rows.Count, 3] = filecount.ToString();
                worksheet.Cells[8 + grdStatus.Rows.Count, 4] = imgcount.ToString();
                worksheet.Cells[8 + grdStatus.Rows.Count, 2].Borders.Color = ColorTranslator.ToOle(Color.Black);
                worksheet.Cells[8 + grdStatus.Rows.Count, 3].Borders.Color = ColorTranslator.ToOle(Color.Black);
                worksheet.Cells[8 + grdStatus.Rows.Count, 4].Borders.Color = ColorTranslator.ToOle(Color.Black);
            }

            string namexls = "Production_Report" + ".xls";
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
