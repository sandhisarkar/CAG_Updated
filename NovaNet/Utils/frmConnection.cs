using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.Odbc;
using System.IO;

namespace NovaNet
{
    namespace Utils
    {
        public partial class frmConnection : Form
        {
            string conStr = string.Empty;
            private OdbcConnection dbCon = null;
            private string iniPath = string.Empty;
            public static NovaNet.Utils.exLog.Logger exMailLog = new NovaNet.Utils.exLog.emailLogger("./errLog.log", NovaNet.Utils.exLog.LogLevel.Dev,Constants._MAIL_TO,Constants._MAIL_FROM,Constants._SMTP);
            public static NovaNet.Utils.exLog.Logger exTxtLog = new NovaNet.Utils.exLog.txtLogger("./errLog.log", NovaNet.Utils.exLog.LogLevel.Dev);	
            public frmConnection(string prmIniPath)
            {
                InitializeComponent();
                iniPath = prmIniPath;
                exMailLog.SetNextLogger(exTxtLog);
            }

        
         private void cmdTest_Click(object sender, EventArgs e)
            {
                try
                {
                    if (txtSchema.Text.ToString().Trim() != string.Empty)
                    {
                        conStr = "DRIVER=" + Constants._MYSQL_DRIVER + ";SERVER=" + txtIp.Text + ";PORT=" + txtPort.Text + ";DATABASE=" + txtSchema.Text + ";USER=" + txtUserName.Text + ";PASSWORD=" + txtPassword.Text + ";OPTION=3";
                        dbCon = new OdbcConnection(conStr);
                        dbCon.Open();
                        if (dbCon.State == ConnectionState.Open)
                        {
                            MessageBox.Show("Test connection successfull.....");
                            dbCon.Close();
                        }
                        else
                        {
                            MessageBox.Show("Test connection failed....");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Default schema name cannot be left blank...");
                        txtSchema.Focus();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Test connection failed...." + ex.Message);
                    exMailLog.Log(ex);
                }
            }

            private void cmdOk_Click(object sender, EventArgs e)
            {
                try
                {
                    if (txtSchema.Text.ToString().Trim() != string.Empty)
                    {
                        conStr = "DRIVER=" + Constants._MYSQL_DRIVER + ";SERVER=" + txtIp.Text + ";PORT=" + txtPort.Text + ";DATABASE=" + txtSchema.Text + ";USER=" + txtUserName.Text + ";PASSWORD=" + txtPassword.Text + ";OPTION=3";
                        dbCon = new OdbcConnection(conStr);
                        dbCon.Open();
                        if (dbCon.State == ConnectionState.Open)
                        {
                            int retVal;
                            INIFile rdConfig = new INIFile();

                            retVal = rdConfig.WriteINI(Constants.INI_SECTION, Constants.INI_KEY, conStr, iniPath);
                            this.Close();
                            dbCon.Close();
                        }
                        else
                        {
                            MessageBox.Show("Connection failed....");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Default schema name cannot be left blank...");
                        txtSchema.Focus();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connection failed...." + ex.Message);
                    exMailLog.Log(ex);
                }
            }

            private void cmdCancel_Click(object sender, EventArgs e)
            {
                this.Close();
                this.Dispose();
                if (File.Exists(iniPath))
                {
                    File.Delete(iniPath);
                }
                Application.Exit();
            }

            private void frmConnection_Load(object sender, EventArgs e)
            {

            }
        }
    }
}