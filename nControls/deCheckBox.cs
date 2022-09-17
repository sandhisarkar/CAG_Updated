/*
 * Created by SharpDevelop.
 * User: RahulN
 * Date: 1/9/2014
 * Time: 6:21 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace nControls
{
	/// <summary>
	/// Description of deCheckBox.
	/// </summary>
	public partial class deCheckBox : CheckBox
	{
		public deCheckBox()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}

        private void deCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
                SendKeys.Send("{Tab}");
        }


	}
}
