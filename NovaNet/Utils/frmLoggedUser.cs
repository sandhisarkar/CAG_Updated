using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NovaNet.Utils;

namespace ImageHeaven
{
    public partial class frmLoggedUser : Form
    {
        public static NovaNet.Utils.IntrRBAC rbc;
        public static Credentials crd;
        public frmLoggedUser(IntrRBAC pRbac,Credentials pCrd)
        {
            rbc = pRbac;
            crd = pCrd;
            InitializeComponent();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void PopulateOnlineUsersList()
        {
            ListViewItem lvwItem;
            Color shaded1 = Color.LemonChiffon;
            Color shaded2 = Color.LightCyan;
            int i = 0;
            DataSet ds = rbc.GetOnlineUsersList(crd);
            lvw.Items.Clear();
            if (ds != null)
            {
                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                {
                    lvwItem = lvw.Items.Add(ds.Tables[0].Rows[j][0].ToString());
                    lvwItem.SubItems.Add(ds.Tables[0].Rows[j][1].ToString());
                    lvwItem.SubItems.Add(ds.Tables[0].Rows[j][2].ToString());
                    lvwItem.SubItems.Add(ds.Tables[0].Rows[j][3].ToString());
                    if (i++ % 2 == 1)
                    {
                        lvwItem.BackColor = shaded1;
                        lvwItem.UseItemStyleForSubItems = true;
                    }
                    else
                    {
                        lvwItem.BackColor = shaded2;
                        lvwItem.UseItemStyleForSubItems = true;
                    }
                }
            }
            lblTotUsers.Text = "Total online users : " + lvw.Items.Count;
        }

        private void cmdUnlock_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lvw.Items.Count; i++)
            {
                if (lvw.Items[i].Checked == true)
                {
                    rbc.UnLockedUser(lvw.Items[i].SubItems[1].Text.ToString());
                }
            }
            PopulateOnlineUsersList();
        }

        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            PopulateOnlineUsersList();
            
        }

        private void frmLoggedUser_Load(object sender, EventArgs e)
        {
            PopulateOnlineUsersList();
        }

    }
}
