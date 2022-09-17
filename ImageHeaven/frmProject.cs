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
using nControls;

namespace ImageHeaven
{
    public partial class frmProject : Form
    {
        wItem objProject;
        OdbcConnection sqlCon = null;
        Credentials crd = new Credentials();


        public frmProject()
        {
            InitializeComponent();
        }

        public frmProject(wItem prmCmd, OdbcConnection prmCon, Credentials prmCrd)
        {

            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();
            //this.BackColor = Color.FromArgb(100, 0, 0, 0);
            sqlCon = prmCon;
            objProject = (wfeProject)prmCmd;
            crd = prmCrd;
            if (objProject.GetMode() == Constants._ADDING)
                this.Text = "B'Zer - CAG(Add Project)";
            else
                this.Text = "B'Zer - CAG(Edit Project)";

            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
        }

        private void frmProject_Load(object sender, EventArgs e)
        {
            //panel1.BackColor = Color.FromArgb(100, 0, 0, 0);
            CmdSave.Enabled = false;

            if (txtProjectName.Text == null || txtProjectName.Text == "")
            {
                groupBox1.Enabled = false;
            }
            else
            {
                CmdBrowseScannLoc.Focus();
                groupBox1.Enabled = true;
            }
        }

        private void CmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CmdBrowseScannLoc_Click(object sender, EventArgs e)
        {
            // Local Drive select
            /* string path = string.Empty;
            int pos = 0;
            FolderBrowserDialog browseForComputer = new FolderBrowserDialog();
            string origIp = string.Empty;
            System.IO.FileStream fs;

            if (browseForComputer.ShowDialog() == DialogResult.OK)
            {
                txtScannedLoc.Text = browseForComputer.SelectedPath;
                path = txtScannedLoc.Text;
                if (path != string.Empty)
                {
                    CmdSave.Enabled = true;
                    pos = path.IndexOf("\\\\");
                    if (pos == -1)
                    {
                        //int posSnd = path.IndexOf("\\", 2);
                        //string compName = path.Substring(pos + 2, posSnd - 2);
                        string compName = path;
                        //string restPath = path.Substring(posSnd);
                        if (compName != string.Empty)
                        {
                            try
                            {
                                //IPHostEntry ip = Dns.GetHostEntry(compName);
                                //IPAddress[] IpA = ip.AddressList;
                                //for (int i = 0; i < IpA.Length; i++)
                                //{
                                //    origIp = IpA[i].ToString();
                                //}
                                //path = "\\\\" + origIp + restPath;
                                fs = new System.IO.FileStream(path + "\\temp.txt", System.IO.FileMode.Append);
                                fs.Close();
                                System.IO.File.Delete(path + "\\temp.txt");
                                CmdSave.Enabled = true;
                                txtScannedLoc.Text = path;
                            }
                            catch (Exception ex)
                            {
                                CmdSave.Enabled = false;
                                MessageBox.Show(ex.Message.ToString());
                            }
                        }
                    }
                    else
                    {
                        CmdSave.Enabled = false;
                        MessageBox.Show("Selected drive is invalid. Please select folder from network drive..");
                    }
                }
            }
            else
            {
                CmdSave.Enabled = false;
            } */


            //Server locatopn Select 
            string path = string.Empty;
            int pos = 0;
            FolderBrowserDialog browseForComputer = new FolderBrowserDialog();
            string origIp = string.Empty;
            System.IO.FileStream fs;

            if (browseForComputer.ShowDialog() == DialogResult.OK)
            {
                txtScannedLoc.Text = browseForComputer.SelectedPath;
                path = txtScannedLoc.Text;
                if (path != string.Empty)
                {
                    CmdSave.Enabled = true;
                    pos = path.IndexOf("\\\\");
                    if (pos != -1)
                    {
                        int posSnd = path.IndexOf("\\", 2);
                        string compName = path.Substring(pos + 2, posSnd - 2);
                        string restPath = path.Substring(posSnd);
                        if (compName != string.Empty)
                        {
                            try
                            {
                                IPHostEntry ip = Dns.GetHostEntry(compName);
                                IPAddress[] IpA = ip.AddressList;
                                for (int i = 0; i < IpA.Length; i++)
                                {
                                    origIp = IpA[i].ToString();
                                }
                                path = "\\\\" + origIp + restPath;
                                fs = new System.IO.FileStream(path + "\\temp.txt", System.IO.FileMode.Append);
                                fs.Close();
                                System.IO.File.Delete(path + "\\temp.txt");
                                CmdSave.Enabled = true;
                                txtScannedLoc.Text = path;
                            }
                            catch (Exception ex)
                            {
                                CmdSave.Enabled = false;
                                MessageBox.Show(ex.Message.ToString());
                            }
                        }
                    }
                    else
                    {
                        CmdSave.Enabled = false;
                        MessageBox.Show("Selected drive is invalid. Please select folder from network drive..");
                    }
                }
            }
            else
            {
                CmdSave.Enabled = false;
            }
        }

        private void txtProjectName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 92) || (e.KeyChar == 39) || (e.KeyChar == 47))
            {
                e.Handled = true;
            }
        }

        private void ClearAllField()
        {
            txtProjectName.Text = string.Empty;
            txtScannedLoc.Text = string.Empty;
        }

        private void txtProjectName_MouseLeave(object sender, EventArgs e)
        {
            if (txtProjectName.Text == null || txtProjectName.Text == "")
            {
                groupBox1.Enabled = false;
            }
            else
            {
                groupBox1.Enabled = true;
                CmdBrowseScannLoc.Focus();
            }
        }

        private void CmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                NovaNet.Utils.dbCon dbcon = new NovaNet.Utils.dbCon();


                wfeProject crtProject = new wfeProject(sqlCon);

                udtProject udtProj = new udtProject();
                udtProj.Code = txtProjectName.Text.ToUpper().Trim();
                udtProj.Project_Path = txtScannedLoc.Text;
                udtProj.Created_By = crd.created_by;
                udtProj.Created_DTTM = dbcon.GetCurrenctDTTM(1, sqlCon);
                if (crtProject.TransferValues(udtProj) == true)
                {
                    statusStrip1.Items.Clear();
                    MessageBox.Show("Project SucessFully Created");
                    statusStrip1.Items.Add("Status: Project SucessFully Created");
                    statusStrip1.ForeColor = System.Drawing.Color.Black;
                    ClearAllField();
                }
                else
                {
                    statusStrip1.Items.Clear();
                    MessageBox.Show(this, "Data Cannot be Saved", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    statusStrip1.Items.Add("Status: Data Cannot be Saved");
                    statusStrip1.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (KeyCheckException ex)
            {
                MessageBox.Show(ex.Message, "B'Zer - CAG", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (DbCommitException dbex)
            {
                MessageBox.Show(dbex.Message, "B'Zer - CAG", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (CreateFolderException folex)
            {
                MessageBox.Show(folex.Message, "B'Zer - CAG", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (DBConnectionException conex)
            {
                MessageBox.Show(conex.Message, "B'Zer - CAG", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (INIFileException iniex)
            {
                MessageBox.Show(iniex.Message, "B'Zer - CAG", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtProjectName_Leave(object sender, EventArgs e)
        {
            if (txtProjectName.Text == null || txtProjectName.Text == "")
            {
                groupBox1.Enabled = false;
            }
            else
            {
                groupBox1.Enabled = true;
                CmdBrowseScannLoc.Focus();
            }
        }
    }
}
