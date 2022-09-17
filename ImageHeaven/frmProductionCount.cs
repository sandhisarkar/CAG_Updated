using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageHeaven
{
    public partial class frmProductionCount : Form
    {
        private int count = 0;
        public frmProductionCount(int pCount)
        {
            InitializeComponent();
            count = pCount;
        }
        public frmProductionCount()
        {
            InitializeComponent();
        }

        private void frmProductionCount_Load(object sender, EventArgs e)
        {
            lblCount.Text = "Today you have done - " + count.ToString();
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
