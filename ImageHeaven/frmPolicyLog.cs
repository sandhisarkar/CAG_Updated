using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.Odbc;

namespace ImageHeaven
{
    public partial class frmPolicyLog : Form
    {
        private DataSet ds;
        private DataSet ds1;
        private string policyNo;

        OdbcDataAdapter sqlAdap;
        //public static NovaNet.Utils.exLog.Logger exMailLog = new NovaNet.Utils.exLog.emailLogger("./errLog.log", NovaNet.Utils.exLog.LogLevel.Dev, Constants._MAIL_TO, Constants._MAIL_FROM, Constants._SMTP);
        //public static NovaNet.Utils.exLog.Logger exTxtLog = new NovaNet.Utils.exLog.txtLogger("./errLog.log", NovaNet.Utils.exLog.LogLevel.Dev);
        OdbcConnection sqlCon;

        public frmPolicyLog()
        {
            InitializeComponent();
        }

        public frmPolicyLog(DataSet pDs, string pPolicyNo)
        {
            policyNo = pPolicyNo;
            ds = pDs;
            //pCon = sqlCon;
            InitializeComponent();
        }

        public frmPolicyLog(DataSet pDs, DataSet pDs1, string pPolicyNo)
        {
            policyNo = pPolicyNo;
            ds = pDs;
            ds1 = pDs1;
            //pCon = sqlCon;
            InitializeComponent();
        }

        private void frmPolicyLog_Load(object sender, EventArgs e)
        {
            ListViewItem lvwItem;
            lblPolicyNumber.Text = policyNo;
            //Color shaded1 = Color.LemonChiffon;
            //Color shaded2 = Color.LightCyan;
            //int i = 0;
            lvw.Items.Clear();
            if (ds != null)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (j == 0)
                    {
                        lvwItem = lvw.Items.Add("Metadata Entry User");
                        lvwItem.SubItems.Add(ds1.Tables[0].Rows[0][0].ToString());
                    }

                    if (j == 1)
                    {
                        lvwItem = lvw.Items.Add("Scanned User");
                        lvwItem.SubItems.Add(ds.Tables[0].Rows[0][0].ToString());
                    }

                    if (j == 2)
                    {
                        lvwItem = lvw.Items.Add("QC User");
                        lvwItem.SubItems.Add(ds.Tables[0].Rows[0][1].ToString());
                    }
                    //if (j == 3)
                    //{
                    //    lvwItem = lvw.Items.Add("Index User");
                    //    lvwItem.SubItems.Add(ds.Tables[0].Rows[0][2].ToString());
                    //}
                    if (j == 3)
                    {
                        lvwItem = lvw.Items.Add("FQC User");
                        lvwItem.SubItems.Add(ds.Tables[0].Rows[0][3].ToString());
                    }
                    //lvwItem.SubItems.Add(ds.Tables[0].Rows[j][2].ToString());
                    //if (i++ % 2 == 1)
                    //{
                    //    lvwItem.BackColor = shaded1;
                    //    lvwItem.UseItemStyleForSubItems = true;
                    //}
                    //else
                    //{
                    //    lvwItem.BackColor = shaded2;
                    //    lvwItem.UseItemStyleForSubItems = true;
                    //}
                }
            }
        }
    }
}
