/*
 * Created by SharpDevelop.
 * User: RahulN
 * Date: 1/8/2014
 * Time: 9:15 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;

namespace nControls
{
	/// <summary>
	/// Description of deDateBox.
	/// </summary>
	public partial class deDateBox : TextBox
	{
        private bool _isRequired;
        private string _dateString;
		public deDateBox()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
            _isRequired = true;
		}
        [Description("Whether it needs to be filled up mandatorily or not"), Category("Data")]
        public bool Mandatory
        {
            get { return _isRequired; }
            set { _isRequired = value; }
        }
        [Description("Gets the date in yyyy/mm/dd format"), Category("Data")]
        public string DateBritish
        {
            get
            {
                string dateString = this.Text.Trim();
                string format = "ddMMyyyy";
                DateTime dateTime;
                //If mandatory is not set just check whether it's null and return
                if (this.Text.Trim().Length <= 0 && this.Mandatory == false)
                {
                    return _dateString;
                }
                //The block to check whether date is given in right format
                if (DateTime.TryParseExact(dateString, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                {
                    this._dateString = Convert.ToString(dateTime.Year).PadLeft(4, '0') + "/" + Convert.ToString(dateTime.Month).PadLeft(2, '0') + "/" + Convert.ToString(dateTime.Day).PadLeft(2, '0');
                }
                return _dateString;
            }
        }
		private void deDateBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Return)
                SendKeys.Send("{Tab}");
        }
		void deDateBoxLeave(object sender, EventArgs e)
		{
			string dateString = this.Text.Trim();

			string format = "ddMMyyyy";

			DateTime dateTime;
            //If mandatory is not set just check whether it's null and return
            if (this.Text.Trim().Length <= 0 && this.Mandatory == false)
            {
                return;
            }
            //The block to check whether date is given in right format
			if (DateTime.TryParseExact(dateString, format, CultureInfo.InvariantCulture,DateTimeStyles.None, out dateTime))
			{
                this.Text = dateTime.ToString(format);
			}
			else
			{
				this.Focus();
			}
            //End: The block to check whether date is given in right format
		}
    }
}
