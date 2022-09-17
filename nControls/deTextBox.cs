using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace nControls
{
    public partial class deTextBox : TextBox
    {
        private bool _isRequired;
        public deTextBox()
        {
            InitializeComponent();
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            _isRequired = true;
        }
        public bool Mandatory
        {
            get { return _isRequired; }
            set { _isRequired = value; }
        }
        private void deTextBox_KeyDown(object sender, KeyEventArgs e)
         {
            
            
           if (e.KeyCode == Keys.Return)
            {
                SendKeys.Send("{Tab}");
                e.Handled = true;
            }
           
        }

        private void deTextBox_Enter(object sender, EventArgs e)
        {
        	this.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F1F6");
            this.SelectAll();
        }
        private void deTextBox_Leave(object sender, EventArgs e)
        {
            if (this.Mandatory == false)
            {
                return;
            }
            this.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            
        }

        private void deTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }
    }
}
