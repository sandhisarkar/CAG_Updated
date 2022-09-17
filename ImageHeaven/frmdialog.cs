using System;
using System.Drawing;
using System.Windows.Forms;
using NovaNet.Utils;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using LItems;
using NovaNet.wfe;
using System.IO;

namespace ImageHeaven
{
    public partial class frmdialog : Form
    {
        NovaNet.Utils.dbCon dbcon;
        //Credentials udtCrd;
        ControlInfo udtInfo;
        MemoryStream stateLog;
        byte[] tmpWrite;
        OdbcConnection sqlCon = null;
        Point start_point;
        Credentials crd = new Credentials();
        public frmdialog(OdbcConnection prmCon, Credentials prmCrd)
        {
            InitializeComponent();
            sqlCon = prmCon;
            crd = prmCrd;
        }
        public frmdialog(OdbcConnection prmCon, Credentials prmCrd, DataSet ds, Point pt)
        {
            InitializeComponent();
            start_point = pt;
            sqlCon = prmCon;
            crd = prmCrd;
            txtdeedno.Text = ds.Tables[0].Rows[0][0].ToString();
            txtdeedyear.Text = ds.Tables[0].Rows[0][1].ToString();
            lblFirst.Text = ds.Tables[0].Rows[0][2].ToString();
            txtfirst.Text = ds.Tables[0].Rows[0][3].ToString();

            if (ds.Tables[0].Rows.Count >= 2)
            {
                lblsecond.Text = ds.Tables[0].Rows[1][2].ToString();
                txtsecond.Text = ds.Tables[0].Rows[1][3].ToString();
            }
        }
        public frmdialog()
        {
            InitializeComponent();
        }

        private void frmdialog_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Top = start_point.Y;
            this.Left = start_point.X;
            //this.Top = 600;
            //this.Left = 1000;
        }

        private void frmdialog_LocationChanged(object sender, EventArgs e)
        {
            Point curr_point = new Point();
            curr_point.X = this.Location.X;
            curr_point.Y = this.Location.Y;
            PopupPoint.pnt = curr_point;
        }

        private void frmdialog_DragDrop(object sender, DragEventArgs e)
        {
            //curr_point.X = e.X;
            //curr_point.Y = e.Y;
            //PopupPoint.pnt = curr_point;
        }

        private void frmdialog_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
