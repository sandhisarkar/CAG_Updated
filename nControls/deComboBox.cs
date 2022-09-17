using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace nControls
{
    public partial class deComboBox : ComboBox
    {
        private bool _isRequired;
        public deComboBox()
        {
            InitializeComponent();
            _isRequired = true;
        }
        [Description("Whether it needs to be filled up mandatorily or not"), Category("Data")]
        public bool Mandatory
        {
            get { return _isRequired; }
            set { _isRequired = value; }
        }
        private void deComboBox_Leave(object sender, EventArgs e)
        {
            bool retVal = false;
            if (this.Mandatory == false)
            {
            	return;
            }
            //If mandatory is not set just check whether it's null and return
            if (this.Text.Trim().Length <= 0 && this.Mandatory == false)
            {
                return;
            }
            for (int i = 0; i < this.Items.Count; i++)
            {
            	//Check whether the object loaded is from a data source or whether it's a simple combo
            	//and extract the text to match accordingly
            	string toMatch=null;
            	if(this.Items[i].GetType() == typeof(DataRowView))
            	{
            		DataRowView drv = (DataRowView) this.Items[i];
            		toMatch = drv.Row[1].ToString().Trim().ToLower();
            	}
            	else
            	{
            		toMatch = this.Items[i].ToString().Trim().ToLower();
            	}
            	//Now match here, and if it matches, exit straight away
            	if (toMatch == this.Text.ToLower().Trim())
                {
                    this.SelectedIndex = i;
                    retVal = true;
                    break;
                }
            }
            if (retVal == false)
            {
            	//throw new Exception("You are trying to get away without selecting anything");
                this.Focus();
            }
        }
        private void deComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
                SendKeys.Send("{Tab}");
        }
    }
}
