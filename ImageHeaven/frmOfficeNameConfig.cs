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
//using AForge.Imaging;
//using AForge;
//using AForge.Imaging.Filters;
using TwainLib;
using Inlite.ClearImageNet;
//using System.Drawing.Bitmap;
//using System.Drawing.Graphics;
//using Graphics.DrawImage;

namespace ImageHeaven
{
    public partial class frmOfficeNameConfig : Form
    {
        public static NovaNet.Utils.exLog.Logger exMailLog = new NovaNet.Utils.exLog.emailLogger("./errLog.log", NovaNet.Utils.exLog.LogLevel.Dev, Constants._MAIL_TO, Constants._MAIL_FROM, Constants._SMTP);
        public static NovaNet.Utils.exLog.Logger exTxtLog = new NovaNet.Utils.exLog.txtLogger("./errLog.log", NovaNet.Utils.exLog.LogLevel.Dev);

        string iniPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Remove(0, 6) + "\\" + "IhConfiguration.ini";
        INIFile ini = new INIFile();

        Credentials crd = new Credentials();

        public frmOfficeNameConfig(Credentials prmCrd)
        {
            InitializeComponent();

            this.Text = "Office / State name configuration ";

            exMailLog.SetNextLogger(exTxtLog);

            crd = prmCrd;

            find_state_name();

        }

        private void frmOfficeNameConfig_Load(object sender, EventArgs e)
        {
            if(crd.role == ihConstants._ADMINISTRATOR_ROLE)
            {
                deTextBox1.Enabled = true;
                deButtonSave.Enabled = true;
            }
            else
            {
                deTextBox1.Enabled = false;
                deButtonSave.Enabled = false;
                MessageBox.Show(this, "You cannot set Office / State name ", "Warning !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
        }

        private void find_state_name()
        {
            if (File.Exists(iniPath) == true)
            {
                string stName = ini.ReadINI("STATE", "STATENAME", string.Empty, iniPath);
               
                if (stName.ToString().Trim() == null || stName.ToString().Trim() == "\0")
                {
                    deTextBox1.Text = string.Empty;
                }
                else
                {
                    deTextBox1.Text = stName.ToString();
                }
            }
        }

        private void deButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void deButtonSave_Click(object sender, EventArgs e)
        {
            if(deTextBox1.Text == "" || deTextBox1.Text == null || deTextBox1.Text == string.Empty || string.IsNullOrEmpty(deTextBox1.Text))
            {
                MessageBox.Show(this, "You cannot save blank office name ", "Error ! ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                string stName = deTextBox1.Text.Trim();
                if (File.Exists(iniPath) == true)
                {
                    int i = ini.WriteINI("STATE", "STATENAME", stName, iniPath);

                    if(i > 0)
                    {
                        MessageBox.Show(this, "Office / state name is ready to use ", "Success !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
    }
}
