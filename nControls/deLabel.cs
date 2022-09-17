/*
 * Created by SharpDevelop.
 * User: RahulN
 * Date: 1/8/2014
 * Time: 8:59 PM
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
	/// Description of deLabel.
	/// </summary>
	public partial class deLabel : Label
	{
		public deLabel()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		}
	}
}
