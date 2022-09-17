/*
 * Created by SharpDevelop.
 * User: RahulN
 * Date: 19/02/2014
 * Time: 1:12 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TestComponents
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void CmdRptViewerClick(object sender, EventArgs e)
		{
			TestReportViewer frmRptViewer = new TestReportViewer();
			frmRptViewer.ShowDialog(this);
		}
		
		void MainFormKeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				this.Close();
			}
		}
	}
}
