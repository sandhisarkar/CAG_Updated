using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using NovaNet.Utils;
using System.Data.Odbc;
using System.Reflection;
using System.Data.OleDb;
using System.Globalization;
using LItems;
using NovaNet;
using NovaNet.wfe;

namespace ImageHeaven
{
    public partial class frmMain : Form
    {
        static wItem wi;
        //NovaNet.Utils.dbCon dbcon;
        frmMain mainForm;
        OdbcConnection sqlCon = null;
        public Credentials crd;
        static int colorMode;
        dbCon dbcon;

        //
        NovaNet.Utils.GetProfile pData;
        NovaNet.Utils.ChangePassword pCPwd;
        NovaNet.Utils.Profile p;
        public static NovaNet.Utils.IntrRBAC rbc;
        private short logincounter;
        //
        OdbcTransaction txn;

        public static string projKey;
        public static string bundleKey;
        public static string projectName = null;
        public static string batchName = null;
        public static string boxNumber = null;
        public static string projectVal = null;
        public static string batchVal = null;

        public static string name;

        public static int height;
        public static int width;

        public frmMain()
        {
            InitializeComponent();
        }

        public frmMain(OdbcConnection pCon)
        {
            InitializeComponent();

            sqlCon = pCon;

            logincounter = 0;

            ImageHeaven.Program.Logout = false;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            height = pictureBox1.Height;
            width = pictureBox1.Width;

            int k;
            dbcon = new NovaNet.Utils.dbCon();
            try
            {
                string dllPaths = string.Empty;

                menuStrip1.Visible = false;


                if (sqlCon.State == ConnectionState.Open)
                {
                    pData = getData;
                    pCPwd = getCPwd;
                    rbc = new NovaNet.Utils.RBAC(sqlCon, dbcon, pData, pCPwd);
                    //string test = sqlCon.Database;
                    GetChallenge gc = new GetChallenge(getData);




                    gc.ShowDialog(this);

                    crd = rbc.getCredentials(p);
                    AssemblyName assemName = Assembly.GetExecutingAssembly().GetName();
                    this.Text = "B'Zer - CAG" + "           Version: " + assemName.Version.ToString() + "    Database name: " + sqlCon.Database.ToString() + "    Logged in user: " + crd.userName;

                    name = crd.userName;
                    if (crd.role == ihConstants._ADMINISTRATOR_ROLE || crd.role == "Supervisor")
                    {
                        menuStrip1.Visible = true;
                        newToolStripMenuItem.Visible = true;
                        newToolStripMenuItem.Enabled = true;
                        projectToolStripMenuItem.Enabled = true;
                        projectToolStripMenuItem.Visible = true;
                        batchToolStripMenuItem.Enabled = true;
                        batchToolStripMenuItem.Visible = true;
                        exitToolStripMenuItem.Visible = true;
                        exitToolStripMenuItem.Enabled = true;

                        transactionsToolStripMenuItem.Visible = true;
                        dataEntryToolStripMenuItem.Visible = true;
                        dataEntryToolStripMenuItem.Enabled = true;
                        batchUploadToolStripMenuItem.Visible = true;
                        batchUploadToolStripMenuItem.Enabled = true;
                        bundleScanToolStripMenuItem.Enabled = false;
                        bundleScanToolStripMenuItem.Visible = false;
                        imageImportToolStripMenuItem.Visible = true;
                        imageImportToolStripMenuItem.Enabled = true;
                        imageQualityControlToolStripMenuItem.Visible = true;
                        imageQualityControlToolStripMenuItem.Enabled = true;
                        qualityControlFinalToolStripMenuItem.Visible = true;
                        qualityControlFinalToolStripMenuItem.Enabled = true;
                        toolStripMenuItem1.Enabled = true;
                        toolStripMenuItem1.Visible = true;
                        exportToolStripMenuItem.Enabled = true;
                        exportToolStripMenuItem.Visible = true;


                        toolsToolStripMenuItem.Enabled = true;
                        toolsToolStripMenuItem.Visible = true;
                        configurationToolStripMenuItem.Visible = true;
                        newPasswordToolStripMenuItem.Visible = true;
                        newUserToolStripMenuItem.Visible = true;
                        onlineUsersToolStripMenuItem.Visible = true;
                        officeNameConfigurationToolStripMenuItem.Visible = true;

                        toolStrip1.Visible = true;
                        toolStripButton1.Visible = false;
                        toolStripButton3.Visible = true;
                        toolStripButton2.Visible = false;
                        toolStripButton4.Visible = true;

                        configurationToolStripMenuItem.Visible = true;

                        helpToolStripMenuItem.Visible = true;

                        aboutToolStripMenuItem.Visible = true;

                        auditToolStripMenuItem.Visible = true;
                        partIIToolStripMenuItem.Visible = true;
                        partIToolStripMenuItem.Visible = true;
                        logoutToolStripMenuItem.Visible = true;

                        reportToolStripMenuItem.Visible = true;
                        dashboardToolStripMenuItem.Visible = true;
                        batchWiseReportToolStripMenuItem.Visible = true;
                        productionReportToolStripMenuItem.Visible = true;
                    }
                    else if(crd.role == "Scan")
                    {

                        menuStrip1.Visible = true;
                        newToolStripMenuItem.Visible = false;
                        newToolStripMenuItem.Enabled = false;
                        projectToolStripMenuItem.Enabled = false;
                        projectToolStripMenuItem.Visible = false;
                        batchToolStripMenuItem.Enabled = false;
                        batchToolStripMenuItem.Visible = false;
                        exitToolStripMenuItem.Visible = false;
                        exitToolStripMenuItem.Enabled = false;

                        transactionsToolStripMenuItem.Visible = true;
                        dataEntryToolStripMenuItem.Visible = false;
                        dataEntryToolStripMenuItem.Enabled = false;
                        batchUploadToolStripMenuItem.Visible = false;
                        batchUploadToolStripMenuItem.Enabled = false;
                        bundleScanToolStripMenuItem.Enabled = false;
                        bundleScanToolStripMenuItem.Visible = false;
                        imageImportToolStripMenuItem.Visible = true;
                        imageImportToolStripMenuItem.Enabled = true;
                        imageQualityControlToolStripMenuItem.Visible = false;
                        imageQualityControlToolStripMenuItem.Enabled = false;
                        qualityControlFinalToolStripMenuItem.Visible = false;
                        qualityControlFinalToolStripMenuItem.Enabled = false;
                        toolStripMenuItem1.Enabled = false;
                        toolStripMenuItem1.Visible = false;
                        exportToolStripMenuItem.Enabled = false;
                        exportToolStripMenuItem.Visible = false;


                        toolsToolStripMenuItem.Enabled = true;
                        toolsToolStripMenuItem.Visible = true;
                        configurationToolStripMenuItem.Visible = false;
                        newPasswordToolStripMenuItem.Visible = true;
                        newUserToolStripMenuItem.Visible = false;
                        onlineUsersToolStripMenuItem.Visible = false;
                        officeNameConfigurationToolStripMenuItem.Visible = false;

                        toolStrip1.Visible = false;
                        toolStripButton1.Visible = false;
                        toolStripButton3.Visible = false;
                        toolStripButton2.Visible = false;
                        toolStripButton4.Visible = false;
                        
                        configurationToolStripMenuItem.Visible = false;

                        helpToolStripMenuItem.Visible = true;

                        aboutToolStripMenuItem.Visible = true;

                        auditToolStripMenuItem.Visible = false;
                        partIIToolStripMenuItem.Visible = false;
                        partIToolStripMenuItem.Visible = false;
                        logoutToolStripMenuItem.Visible = true;

                        reportToolStripMenuItem.Visible = true;
                        dashboardToolStripMenuItem.Visible = true;
                        batchWiseReportToolStripMenuItem.Visible = false;
                        productionReportToolStripMenuItem.Visible = true;
                    }
                    else if(crd.role == "QC")
                    {
                        menuStrip1.Visible = true;
                        newToolStripMenuItem.Visible = false;
                        newToolStripMenuItem.Enabled = false;
                        projectToolStripMenuItem.Enabled = false;
                        projectToolStripMenuItem.Visible = false;
                        batchToolStripMenuItem.Enabled = false;
                        batchToolStripMenuItem.Visible = false;
                        exitToolStripMenuItem.Visible = false;
                        exitToolStripMenuItem.Enabled = false;

                        transactionsToolStripMenuItem.Visible = true;
                        dataEntryToolStripMenuItem.Visible = false;
                        dataEntryToolStripMenuItem.Enabled = false;
                        batchUploadToolStripMenuItem.Visible = false;
                        batchUploadToolStripMenuItem.Enabled = false;
                        bundleScanToolStripMenuItem.Enabled = false;
                        bundleScanToolStripMenuItem.Visible = false;
                        imageImportToolStripMenuItem.Visible = false;
                        imageImportToolStripMenuItem.Enabled = false;
                        imageQualityControlToolStripMenuItem.Visible = true;
                        imageQualityControlToolStripMenuItem.Enabled = true;
                        qualityControlFinalToolStripMenuItem.Visible = false;
                        qualityControlFinalToolStripMenuItem.Enabled = false;
                        toolStripMenuItem1.Enabled = false;
                        toolStripMenuItem1.Visible = false;
                        exportToolStripMenuItem.Enabled = false;
                        exportToolStripMenuItem.Visible = false;


                        toolsToolStripMenuItem.Enabled = true;
                        toolsToolStripMenuItem.Visible = true;
                        configurationToolStripMenuItem.Visible = false;
                        newPasswordToolStripMenuItem.Visible = true;
                        newUserToolStripMenuItem.Visible = false;
                        onlineUsersToolStripMenuItem.Visible = false;
                        officeNameConfigurationToolStripMenuItem.Visible = false;

                        toolStrip1.Visible = true;
                        toolStripButton1.Visible = false;
                        toolStripButton3.Visible = true;
                        toolStripButton2.Visible = false;
                        toolStripButton4.Visible = false;

                        configurationToolStripMenuItem.Visible = false;

                        helpToolStripMenuItem.Visible = true;

                        aboutToolStripMenuItem.Visible = true;

                        auditToolStripMenuItem.Visible = false;
                        partIIToolStripMenuItem.Visible = false;
                        partIToolStripMenuItem.Visible = false;
                        logoutToolStripMenuItem.Visible = true;

                        reportToolStripMenuItem.Visible = true;
                        dashboardToolStripMenuItem.Visible = true;
                        batchWiseReportToolStripMenuItem.Visible = false;
                        productionReportToolStripMenuItem.Visible = true;
                    }
                    else if(crd.role == "Metadata Entry")
                    {
                        menuStrip1.Visible = true;
                        newToolStripMenuItem.Visible = false;
                        newToolStripMenuItem.Enabled = false;
                        projectToolStripMenuItem.Enabled = false;
                        projectToolStripMenuItem.Visible = false;
                        batchToolStripMenuItem.Enabled = false;
                        batchToolStripMenuItem.Visible = false;
                        exitToolStripMenuItem.Visible = false;
                        exitToolStripMenuItem.Enabled = false;

                        transactionsToolStripMenuItem.Visible = true;
                        dataEntryToolStripMenuItem.Visible = true;
                        dataEntryToolStripMenuItem.Enabled = true;
                        batchUploadToolStripMenuItem.Visible = false;
                        batchUploadToolStripMenuItem.Enabled = false;
                        bundleScanToolStripMenuItem.Enabled = false;
                        bundleScanToolStripMenuItem.Visible = false;
                        imageImportToolStripMenuItem.Visible = false;
                        imageImportToolStripMenuItem.Enabled = false;
                        imageQualityControlToolStripMenuItem.Visible = false;
                        imageQualityControlToolStripMenuItem.Enabled = false;
                        qualityControlFinalToolStripMenuItem.Visible = false;
                        qualityControlFinalToolStripMenuItem.Enabled = false;
                        toolStripMenuItem1.Enabled = false;
                        toolStripMenuItem1.Visible = false;
                        exportToolStripMenuItem.Enabled = false;
                        exportToolStripMenuItem.Visible = false;


                        toolsToolStripMenuItem.Enabled = true;
                        toolsToolStripMenuItem.Visible = true;
                        configurationToolStripMenuItem.Visible = false;
                        newPasswordToolStripMenuItem.Visible = true;
                        newUserToolStripMenuItem.Visible = false;
                        onlineUsersToolStripMenuItem.Visible = false;
                        officeNameConfigurationToolStripMenuItem.Visible = false;

                        toolStrip1.Visible = false;
                        toolStripButton1.Visible = false;
                        toolStripButton3.Visible = false;
                        toolStripButton2.Visible = false;
                        toolStripButton4.Visible = false;

                        configurationToolStripMenuItem.Visible = false;

                        helpToolStripMenuItem.Visible = true;

                        aboutToolStripMenuItem.Visible = true;

                        auditToolStripMenuItem.Visible = false;
                        partIIToolStripMenuItem.Visible = false;
                        partIToolStripMenuItem.Visible = false;
                        logoutToolStripMenuItem.Visible = true;

                        reportToolStripMenuItem.Visible = true;
                        dashboardToolStripMenuItem.Visible = true;
                        batchWiseReportToolStripMenuItem.Visible = false;
                        productionReportToolStripMenuItem.Visible = true;
                    }
                    else if(crd.role == "Audit 1")
                    {
                        menuStrip1.Visible = true;
                        newToolStripMenuItem.Visible = false;
                        newToolStripMenuItem.Enabled = false;
                        projectToolStripMenuItem.Enabled = false;
                        projectToolStripMenuItem.Visible = false;
                        batchToolStripMenuItem.Enabled = false;
                        batchToolStripMenuItem.Visible = false;
                        exitToolStripMenuItem.Visible = false;
                        exitToolStripMenuItem.Enabled = false;

                        transactionsToolStripMenuItem.Visible = false;
                        dataEntryToolStripMenuItem.Visible = false;
                        dataEntryToolStripMenuItem.Enabled = false;
                        batchUploadToolStripMenuItem.Visible = false;
                        batchUploadToolStripMenuItem.Enabled = false;
                        bundleScanToolStripMenuItem.Enabled = false;
                        bundleScanToolStripMenuItem.Visible = false;
                        imageImportToolStripMenuItem.Visible = false;
                        imageImportToolStripMenuItem.Enabled = false;
                        imageQualityControlToolStripMenuItem.Visible = false;
                        imageQualityControlToolStripMenuItem.Enabled = false;
                        qualityControlFinalToolStripMenuItem.Visible = false;
                        qualityControlFinalToolStripMenuItem.Enabled = false;
                        toolStripMenuItem1.Enabled = false;
                        toolStripMenuItem1.Visible = false;
                        exportToolStripMenuItem.Enabled = false;
                        exportToolStripMenuItem.Visible = false;


                        toolsToolStripMenuItem.Enabled = true;
                        toolsToolStripMenuItem.Visible = true;
                        configurationToolStripMenuItem.Visible = false;
                        newPasswordToolStripMenuItem.Visible = true;
                        newUserToolStripMenuItem.Visible = false;
                        onlineUsersToolStripMenuItem.Visible = false;
                        officeNameConfigurationToolStripMenuItem.Visible = false;

                        toolStrip1.Visible = false;
                        toolStripButton1.Visible = false;
                        toolStripButton3.Visible = false;
                        toolStripButton2.Visible = false;
                        toolStripButton4.Visible = false;

                        configurationToolStripMenuItem.Visible = false;

                        helpToolStripMenuItem.Visible = true;

                        aboutToolStripMenuItem.Visible = true;

                        auditToolStripMenuItem.Visible = true;
                        partIIToolStripMenuItem.Visible = false;
                        partIToolStripMenuItem.Visible = true;
                        logoutToolStripMenuItem.Visible = true;

                        reportToolStripMenuItem.Visible = true;
                        dashboardToolStripMenuItem.Visible = true;
                        batchWiseReportToolStripMenuItem.Visible = false;
                        productionReportToolStripMenuItem.Visible = true;
                    }
                    else if(crd.role == "Audit 2")
                    {
                        menuStrip1.Visible = true;
                        newToolStripMenuItem.Visible = false;
                        newToolStripMenuItem.Enabled = false;
                        projectToolStripMenuItem.Enabled = false;
                        projectToolStripMenuItem.Visible = false;
                        batchToolStripMenuItem.Enabled = false;
                        batchToolStripMenuItem.Visible = false;
                        exitToolStripMenuItem.Visible = false;
                        exitToolStripMenuItem.Enabled = false;

                        transactionsToolStripMenuItem.Visible = false;
                        dataEntryToolStripMenuItem.Visible = false;
                        dataEntryToolStripMenuItem.Enabled = false;
                        batchUploadToolStripMenuItem.Visible = false;
                        batchUploadToolStripMenuItem.Enabled = false;
                        bundleScanToolStripMenuItem.Enabled = false;
                        bundleScanToolStripMenuItem.Visible = false;
                        imageImportToolStripMenuItem.Visible = false;
                        imageImportToolStripMenuItem.Enabled = false;
                        imageQualityControlToolStripMenuItem.Visible = false;
                        imageQualityControlToolStripMenuItem.Enabled = false;
                        qualityControlFinalToolStripMenuItem.Visible = false;
                        qualityControlFinalToolStripMenuItem.Enabled = false;
                        toolStripMenuItem1.Enabled = false;
                        toolStripMenuItem1.Visible = false;
                        exportToolStripMenuItem.Enabled = false;
                        exportToolStripMenuItem.Visible = false;


                        toolsToolStripMenuItem.Enabled = true;
                        toolsToolStripMenuItem.Visible = true;
                        configurationToolStripMenuItem.Visible = false;
                        newPasswordToolStripMenuItem.Visible = true;
                        newUserToolStripMenuItem.Visible = false;
                        onlineUsersToolStripMenuItem.Visible = false;
                        officeNameConfigurationToolStripMenuItem.Visible = false;

                        toolStrip1.Visible = false;
                        toolStripButton1.Visible = false;
                        toolStripButton3.Visible = false;
                        toolStripButton2.Visible = false;
                        toolStripButton4.Visible = false;

                        configurationToolStripMenuItem.Visible = false;

                        helpToolStripMenuItem.Visible = true;

                        aboutToolStripMenuItem.Visible = true;

                        auditToolStripMenuItem.Visible = true;
                        partIIToolStripMenuItem.Visible = true;
                        partIToolStripMenuItem.Visible = false;

                        logoutToolStripMenuItem.Visible = true;

                        reportToolStripMenuItem.Visible = true;
                        dashboardToolStripMenuItem.Visible = true;
                        batchWiseReportToolStripMenuItem.Visible = false;
                        productionReportToolStripMenuItem.Visible = true;
                    }
                    else if(crd.role == "Fqc")
                    {
                        menuStrip1.Visible = true;
                        newToolStripMenuItem.Visible = false;
                        newToolStripMenuItem.Enabled = false;
                        projectToolStripMenuItem.Enabled = false;
                        projectToolStripMenuItem.Visible = false;
                        batchToolStripMenuItem.Enabled = false;
                        batchToolStripMenuItem.Visible = false;
                        exitToolStripMenuItem.Visible = false;
                        exitToolStripMenuItem.Enabled = false;

                        transactionsToolStripMenuItem.Visible = true;
                        dataEntryToolStripMenuItem.Visible = false;
                        dataEntryToolStripMenuItem.Enabled = false;
                        batchUploadToolStripMenuItem.Visible = false;
                        batchUploadToolStripMenuItem.Enabled = false;
                        bundleScanToolStripMenuItem.Enabled = false;
                        bundleScanToolStripMenuItem.Visible = false;
                        imageImportToolStripMenuItem.Visible = false;
                        imageImportToolStripMenuItem.Enabled = false;
                        imageQualityControlToolStripMenuItem.Visible = false;
                        imageQualityControlToolStripMenuItem.Enabled = false;
                        qualityControlFinalToolStripMenuItem.Visible = true;
                        qualityControlFinalToolStripMenuItem.Enabled = true;
                        toolStripMenuItem1.Enabled = false;
                        toolStripMenuItem1.Visible = false;
                        exportToolStripMenuItem.Enabled = false;
                        exportToolStripMenuItem.Visible = false;


                        toolsToolStripMenuItem.Enabled = true;
                        toolsToolStripMenuItem.Visible = true;
                        configurationToolStripMenuItem.Visible = false;
                        newPasswordToolStripMenuItem.Visible = true;
                        newUserToolStripMenuItem.Visible = false;
                        onlineUsersToolStripMenuItem.Visible = false;
                        officeNameConfigurationToolStripMenuItem.Visible = false;

                        toolStrip1.Visible = true;
                        toolStripButton1.Visible = false;
                        toolStripButton3.Visible = false;
                        toolStripButton2.Visible = false;
                        toolStripButton4.Visible = true;

                        configurationToolStripMenuItem.Visible = false;

                        helpToolStripMenuItem.Visible = true;

                        aboutToolStripMenuItem.Visible = true;

                        auditToolStripMenuItem.Visible = false;
                        partIIToolStripMenuItem.Visible = false;
                        partIToolStripMenuItem.Visible = false;
                        logoutToolStripMenuItem.Visible = true;

                        reportToolStripMenuItem.Visible = true;
                        dashboardToolStripMenuItem.Visible = true;
                        batchWiseReportToolStripMenuItem.Visible = false;
                        productionReportToolStripMenuItem.Visible = true;
                    }
                    //else
                    //{
                    //    menuStrip1.Visible = true;
                    //    newToolStripMenuItem.Visible = false;
                    //    newToolStripMenuItem.Enabled = false;
                    //    projectToolStripMenuItem.Enabled = false;
                    //    projectToolStripMenuItem.Visible = false;
                    //    batchToolStripMenuItem.Enabled = false;
                    //    batchToolStripMenuItem.Visible = false;
                    //    exitToolStripMenuItem.Visible = false;
                    //    exitToolStripMenuItem.Enabled = false;

                    //    transactionsToolStripMenuItem.Visible = false;
                    //    dataEntryToolStripMenuItem.Visible = false;
                    //    dataEntryToolStripMenuItem.Enabled = false;
                    //    batchUploadToolStripMenuItem.Visible = false;
                    //    batchUploadToolStripMenuItem.Enabled = false;
                    //    bundleScanToolStripMenuItem.Enabled = false;
                    //    bundleScanToolStripMenuItem.Visible = false;
                    //    imageImportToolStripMenuItem.Visible = false;
                    //    imageImportToolStripMenuItem.Enabled = false;
                    //    imageQualityControlToolStripMenuItem.Visible = false;
                    //    imageQualityControlToolStripMenuItem.Enabled = false;
                    //    qualityControlFinalToolStripMenuItem.Visible = false;
                    //    qualityControlFinalToolStripMenuItem.Enabled = false;
                    //    toolStripMenuItem1.Enabled = false;
                    //    toolStripMenuItem1.Visible = false;
                    //    exportToolStripMenuItem.Enabled = false;
                    //    exportToolStripMenuItem.Visible = false;


                    //    toolsToolStripMenuItem.Enabled = true;
                    //    toolsToolStripMenuItem.Visible = true;
                    //    configurationToolStripMenuItem.Visible = false;
                    //    newPasswordToolStripMenuItem.Visible = true;
                    //    newUserToolStripMenuItem.Visible = false;
                    //    onlineUsersToolStripMenuItem.Visible = false;

                    //    toolStrip1.Visible = false;
                    //    toolStripButton1.Visible = false;
                    //    toolStripButton3.Visible = false;
                    //    toolStripButton2.Visible = false;
                    //    toolStripButton4.Visible = false;

                    //    configurationToolStripMenuItem.Visible = false;

                    //    helpToolStripMenuItem.Visible = true;

                    //    aboutToolStripMenuItem.Visible = true;

                    //    auditToolStripMenuItem.Visible = false;
                    //    partIIToolStripMenuItem.Visible = false;
                    //    partIToolStripMenuItem.Visible = false;


                    //    logoutToolStripMenuItem.Visible = true;
                    //}
                }
            }
            catch (DBConnectionException dbex)
            {
                //MessageBox.Show(dbex.Message, "Image Heaven", MessageBoxButtons.OK, MessageBoxIcon.Error);
                string err = dbex.Message;
                this.Close();
            }
        }
        void getData(ref NovaNet.Utils.Profile prmp)
        {
            int i;
            p = prmp;
            for (i = 1; i <= 2; i++)
            {
                if (rbc.authenticate(p.UserId, p.Password) == false)
                {
                    if (logincounter == 2)
                    {
                        Application.Exit();
                    }
                    else
                    {
                        logincounter++;
                        GetChallenge ogc = new GetChallenge(getData);
                        ogc.ShowDialog(this);
                    }
                }
                else
                {
                    if (rbc.CheckUserIsLogged(p.UserId))
                    {

                        p = rbc.getProfile();
                        crd = rbc.getCredentials(p);
                        if (crd.role != ihConstants._ADMINISTRATOR_ROLE)
                        {
                            rbc.LockedUser(p.UserId, crd.created_dttm);
                        }
                        break;
                    }
                    else
                    {
                        p.UserId = null;
                        p.UserName = null;
                        GetChallenge ogc = new GetChallenge(getData);
                        AssemblyName assemName = Assembly.GetExecutingAssembly().GetName();
                        this.Text = "B'Zer - Calcutta High Court" + "           Version: " + assemName.Version.ToString() + "    Database name: " + sqlCon.Database.ToString() + "    Logged in user: " + crd.userName;
                        ogc.ShowDialog(this);
                    }
                }
            }
        }
        void getCPwd(ref NovaNet.Utils.Profile prmpwd)
        {
            p = prmpwd;
            rbc.changePassword(p.UserId, p.UserName, p.Password);
        }

        private void projectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmProject dispProject;
                wi = new wfeProject(sqlCon);
                dispProject = new frmProject(wi, sqlCon, crd);
                dispProject.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void batchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmBatch dispProject;
                wi = new wfeBatch(sqlCon);
                dispProject = new frmBatch(wi, sqlCon, DataLayerDefs.Mode._Add, crd);
                dispProject.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rbc.UnLockedUser(crd.created_by.ToString());
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About frm = new About();
            frm.ShowDialog(this);
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AssemblyName assemName = Assembly.GetExecutingAssembly().GetName();
            this.Text = "B'Zer - CAG" + "           Version: " + assemName.Version.ToString() + "    Database name: " + sqlCon.Database.ToString();
            sqlCon.Close();


            sqlCon.Open();

            menuStrip1.Visible = false;
            rbc.UnLockedUser(crd.created_by.ToString());
            frmMain_Load(sender, e);



        }

        private void configurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aeConfiguration csvUploader = new aeConfiguration();
            mainForm = new frmMain();
            csvUploader.ShowDialog(mainForm);
        }

        private void newPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PwdChange pwdCh = new PwdChange(ref p, getCPwd);
            pwdCh.ShowDialog(this);
        }

        private void newUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewUser nwUsr = new AddNewUser(getnwusrData, sqlCon);
            nwUsr.ShowDialog(this);
        }
        void getnwusrData(ref NovaNet.Utils.Profile prmp)
        {
            p = prmp;
            if (rbc.addUser(p.UserId, p.UserName, p.Role_des, p.Password) == false)
            {
                AddNewUser nwUsr = new AddNewUser(getnwusrData, sqlCon);
                nwUsr.ShowDialog(this);
            }
        }

        private void onlineUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLoggedUser loged = new frmLoggedUser(rbc, crd);
            loged.ShowDialog(this);
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.nevaehtech.com/");
        }

        private void dataEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NovaNet.wfe.eSTATES[] state = new NovaNet.wfe.eSTATES[2];
            //state[0] = NovaNet.wfe.eSTATES.POLICY_EXCEPTION;
            state[0] = NovaNet.wfe.eSTATES.METADATA_ENTRY;



            frmEntrySummary fm = new frmEntrySummary(sqlCon, crd, state);

            fm.ShowDialog(this);


        }

        private void batchUploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBundleUpload frm = new frmBundleUpload(sqlCon, crd);
            frm.ShowDialog(this);
        }

        private void imageImportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDataImport data = new frmDataImport(sqlCon, crd);
            data.ShowDialog(this);
        }
        public DataTable _GetBundleStatus(string proj, string bundle)
        {
            DataTable dt = new DataTable();
            string sql = "select distinct status from batch_master where proj_code = '" + proj + "' and batch_key = '" + bundle + "' ";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }
        public string _GetFileCount(string proj_code, string bundle_key)
        {
            DataTable dt = new DataTable();
            string sql = "select COUNT(*) from metadata_entry where proj_code = '" + proj_code + "' and batch_key = '" + bundle_key + "' ";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt.Rows[0][0].ToString();
        }

        
        private void imageQualityControlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eSTATES[] state = new eSTATES[1];
            state[0] = eSTATES.POLICY_SCANNED;
            frmBundleSelect box = new frmBundleSelect(state, sqlCon, txn, crd);
            box.chkPhotoScan.Visible = false;
            box.ShowDialog(this);

            projKey = frmBundleSelect.projKey;
            bundleKey = frmBundleSelect.bundleKey;

            if (projKey != null && bundleKey != null)
            {
                //status check
                if (_GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "2")
                {
                    if (Convert.ToInt32(_GetFileCount(projKey, bundleKey).ToString()) > 0 )
                    {


                        aeImageQc frmQc = new aeImageQc(sqlCon, crd);
                        //frmQc.MdiParent = this;
                        //frmQc.Height = this.ClientRectangle.Height;
                        //frmQc.Width = this.ClientRectangle.Width;
                        frmQc.ShowDialog(this);
                    }
                }
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            eSTATES[] state = new eSTATES[1];
            state[0] = eSTATES.POLICY_SCANNED;
            frmBundleSelect box = new frmBundleSelect(state, sqlCon, txn, crd);
            box.chkPhotoScan.Visible = false;
            box.ShowDialog(this);

            projKey = frmBundleSelect.projKey;
            bundleKey = frmBundleSelect.bundleKey;

            if (projKey != null && bundleKey != null)
            {
                //status check
                if (_GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "2")
                {
                    if (Convert.ToInt32(_GetFileCount(projKey, bundleKey).ToString()) > 0)
                    {
                        aeImageQc frmQc = new aeImageQc(sqlCon, crd);

                        frmQc.ShowDialog(this);
                    }
                }
            }
        }

        private void qualityControlFinalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NovaNet.wfe.eSTATES[] state = new NovaNet.wfe.eSTATES[2];
            //state[0] = NovaNet.wfe.eSTATES.POLICY_EXCEPTION;
            state[0] = NovaNet.wfe.eSTATES.POLICY_INDEXED;
            //state[2] = NovaNet.wfe.eSTATES.POLICY_FQC;
            state[1] = NovaNet.wfe.eSTATES.POLICY_ON_HOLD;
            //state[4] = NovaNet.wfe.eSTATES.POLICY_CHECKED;
            //state[5] = NovaNet.wfe.eSTATES.POLICY_NOT_INDEXED;
            //state[6] = NovaNet.wfe.eSTATES.POLICY_EXPORTED;
            //state[7] = NovaNet.wfe.eSTATES.POLICY_QC;
            //state[8] = NovaNet.wfe.eSTATES.POLICY_SUBMITTED;

            frmBundleSelect box = new frmBundleSelect(state, sqlCon, txn, crd);
            box.chkPhotoScan.Visible = false;
            box.ShowDialog(this);

            projKey = frmBundleSelect.projKey;
            bundleKey = frmBundleSelect.bundleKey;

            if (projKey != null && bundleKey != null)
            {
                //status check
                if (_GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "3" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "2" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "4" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "5" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "6" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "7" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "8" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "9" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "30" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "31" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "37" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "40" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "77")
                {
                    if (Convert.ToInt32(_GetFileCount(projKey, bundleKey).ToString()) > 0 )
                    {
                        aeFQC frm = new aeFQC(sqlCon, crd);
                        frm.ShowDialog(this);
                    }
                }

            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            NovaNet.wfe.eSTATES[] state = new NovaNet.wfe.eSTATES[2];
            //state[0] = NovaNet.wfe.eSTATES.POLICY_EXCEPTION;
            state[0] = NovaNet.wfe.eSTATES.POLICY_INDEXED;
            //state[2] = NovaNet.wfe.eSTATES.POLICY_FQC;
            state[1] = NovaNet.wfe.eSTATES.POLICY_ON_HOLD;
            //state[4] = NovaNet.wfe.eSTATES.POLICY_CHECKED;
            //state[5] = NovaNet.wfe.eSTATES.POLICY_NOT_INDEXED;
            //state[6] = NovaNet.wfe.eSTATES.POLICY_EXPORTED;
            //state[7] = NovaNet.wfe.eSTATES.POLICY_QC;
            //state[8] = NovaNet.wfe.eSTATES.POLICY_SUBMITTED;

            frmBundleSelect box = new frmBundleSelect(state, sqlCon, txn, crd);
            box.chkPhotoScan.Visible = false;
            box.ShowDialog(this);

            projKey = frmBundleSelect.projKey;
            bundleKey = frmBundleSelect.bundleKey;

            if (projKey != null && bundleKey != null)
            {
                //status check
                if (_GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "3" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "2" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "4" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "5" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "6" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "7" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "8" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "9" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "30" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "31" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "37" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "40" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "77")
                {
                    if (Convert.ToInt32(_GetFileCount(projKey, bundleKey).ToString()) > 0)
                    {
                        aeFQC frm = new aeFQC(sqlCon, crd);
                        frm.ShowDialog(this);
                    }
                }

            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmBundleSubmit frm = new frmBundleSubmit(sqlCon, crd);
            frm.ShowDialog(this);
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmExport export = new frmExport(sqlCon, crd);
            export.ShowDialog(this);
        }

        private void partIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aeLicQa frm = new aeLicQa(sqlCon, crd,"1");
            frm.ShowDialog(this);
        }

        private void partIIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aeLicQa frm = new aeLicQa(sqlCon, crd,"2");
            frm.ShowDialog(this);
        }

        private void dashboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmJobDistribution frmjob = new frmJobDistribution(sqlCon);
            frmjob.ShowDialog(this);
        }

        private void officeNameConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmOfficeNameConfig frm = new frmOfficeNameConfig(crd);
            frm.ShowDialog(this);
        }

        private void bundleScanToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void batchWiseReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBatchReport frm = new frmBatchReport(sqlCon);
            frm.ShowDialog(this);
        }

        private void productionReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProduction frm = new frmProduction(sqlCon, crd);
            frm.ShowDialog(this);
        }
    }
}
