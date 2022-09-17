/*
 * Created by SharpDevelop.
 * User: RahulN
 * Date: 19/02/2014
 * Time: 1:13 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using nControls;
using NovaNet.Utils;
using System.Data.Odbc;
using System.Collections.Generic;

namespace TestComponents
{
	/// <summary>
	/// Description of TestReportViewer.
	/// </summary>
	public partial class TestReportViewer : Form
	{
		rptViewer rptV = null;
		dbCon _dbCon;
		OdbcConnection _con;
		List<NamedQuery> _lstNQ;
		public TestReportViewer()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			rptV = new rptViewer();
			this.Controls.Add(rptV);
			InitializeComponent();
			TestReportViewerResize(null, null);
			_dbCon = new dbCon(this);
			_lstNQ = new List<NamedQuery>();
			rptV.FilePath = "test.csv";
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void TestReportViewerKeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				this.Close();
			}
		}
		
		void TestReportViewerResize(object sender, EventArgs e)
		{
			rptV.Left = 0;
			rptV.Top = 0;
			rptV.Height = this.ClientSize.Height;
			rptV.Width = this.ClientSize.Width;
		}
		
		void TestReportViewerLoad(object sender, EventArgs e)
		{
			_con = _dbCon.Connect();
			rptV.Connection = _con;
			rptV.NamedQueries = rptViewer.ReadTabDelimitedFile(@"queries.csv");
		}
	}
}
