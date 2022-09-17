/*
 * Created by SharpDevelop.
 * User: RahulN
 * Date: 19/02/2014
 * Time: 12:46 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data.Odbc;
using System.Data;
using System.Collections.Generic;
using System.IO;
using DgvFilterPopup;

namespace nControls
{
	/// <summary>
	/// The structure with which the queries are to be sent
	/// </summary>
	public struct NamedQuery
	{
		public NamedQuery(string pName, string pQuery)
		{
			_strName = pName;
			_strQuery = pQuery;
		}
		private string _strName;
		/// <summary>
		/// The name of the report
		/// </summary>
		public string StrName {
			get { return _strName; }
		}
		private string _strQuery;
		/// <summary>
		/// The query for the report
		/// </summary>
		public string StrQuery {
			get { return _strQuery; }
		}
		
	}
	/// <summary>
	/// Description of rptViewer.
	/// </summary>
	public partial class rptViewer : UserControl
	{
		private OdbcConnection _dbCon = null;
		private List<NamedQuery> _lstNQ;
		private OdbcTransaction _txn;
		private string _strFilePath;
		
		public rptViewer(OdbcConnection pdbCon)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
            _dbCon = pdbCon;
			_lstNQ = new List<NamedQuery>();
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
            this.lvwQueries.Columns[1].Width = 0;
		}
		[Description("A valid, open ODBC connection"), Category("Data")]
		public OdbcConnection Connection
		{
			get { return _dbCon; }
			set { _dbCon = value; }
		}
		[Description("A valid, open ODBC Transaction"), Category("Data")]
		public OdbcTransaction Transaction
		{
			get { return _txn; }
			set { _txn = value; }
		}
		[Description("A valid list of named queries"), Category("Data")]
		public List<NamedQuery> NamedQueries
		{
			get { return _lstNQ; }
			set 
			{
				_lstNQ = value;
				lvwQueries.Items.Clear();
				foreach (NamedQuery n in _lstNQ)
				{
					lvwQueries.Items.Add(n.StrName);
					lvwQueries.Items[lvwQueries.Items.Count-1].SubItems.Add(n.StrQuery);
				}
			}
		}
		[Description("A valid, file path"), Category("Data")]
		public string FilePath
		{
			get { return _strFilePath; }
			set { _strFilePath = value; }
		}		
		void Panel1Resize(object sender, EventArgs e)
		{
			this.lvwQueries.Left = 0;
			this.lvwQueries.Top = 0;
			this.lvwQueries.Height = this.sptPanel.Panel1.ClientRectangle.Size.Height;
			this.lvwQueries.Width = this.sptPanel.Panel1.ClientRectangle.Size.Width;			
		}
		
		void Panel2Resize(object sender, EventArgs e)
		{
			this.dgvRecords.Left = 0;
			this.dgvRecords.Top = 0;
			this.dgvRecords.Height = this.sptPanel.Panel2.ClientRectangle.Size.Height;
			this.dgvRecords.Width = this.sptPanel.Panel2.ClientRectangle.Size.Width;						
		}
        public DataTable _GetAllDeeds(string pQuery)
        {
            DataTable dt = new DataTable();
            string sql = pQuery;
            OdbcCommand cmd;
            if (_txn != null)
            {
				cmd = new OdbcCommand(sql, _dbCon, _txn);
            }
            else
            {
            	cmd = new OdbcCommand(sql, _dbCon);
            }
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }		
		
		void LvwQueriesDoubleClick(object sender, EventArgs e)
		{
			if (lvwQueries.SelectedItems[0].SubItems[1].Text.Trim().Length > 0)
			{
				stsLblFeedback1.Text = "Retrieving data for " + lvwQueries.SelectedItems[0].SubItems[1].Text.Trim();
				stsFeedback.Invalidate();
				stsFeedback.Refresh();
				DataTable dt = _GetAllDeeds(lvwQueries.SelectedItems[0].SubItems[1].Text.Trim());
				this.dgvRecords.DataSource = dt;
                DgvFilterManager filterManager = new DgvFilterManager(dgvRecords);
                //filterManager.DataGridView = dgvRecords;
				this.dgvRecords.Refresh();
				stsLblFeedback1.Text = "Total records retrieved: " + dt.Rows.Count.ToString();
				stsFeedback.Invalidate();
				stsFeedback.Refresh();
			}
		}
		void Tsr_cmdExportToExcelClick(object sender, EventArgs e)
		{
            SaveFileDialog _FileDlg = new SaveFileDialog();
            _FileDlg.Filter = "CSV files (*.csv)|*.csv";
            _FileDlg.FilterIndex = 2;
            _FileDlg.RestoreDirectory = true;

            //sfdUAT.ShowDialog();
            DialogResult _dlgResult = _FileDlg.ShowDialog(this);
            if (_dlgResult == DialogResult.OK && _FileDlg.FileName.Trim() != string.Empty)
            {
                stsLblFeedback1.Text = "Exporting to file..." + _strFilePath;
                stsFeedback.Invalidate();
                stsFeedback.Refresh();
                writeCSV(dgvRecords, _FileDlg.FileName.Trim());
                stsLblFeedback1.Text = _FileDlg.FileName.Trim() + " is written";
                stsFeedback.Invalidate();
                stsFeedback.Refresh();
            }
            else
            {
                MessageBox.Show("Enter a valid filepath to export the file to", "Export error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
		}
		public void writeCSV(DataGridView gridIn, string outputFile)
		{
			//test to see if the DataGridView has any rows
			if (gridIn.RowCount > 0)
			{
			   string value = "";
			   DataGridViewRow dr = new DataGridViewRow();
			   StreamWriter swOut = new StreamWriter(outputFile);
			
			   //write header rows to csv
			   for (int i = 0; i <= gridIn.Columns.Count - 1; i++)
			   {
			   		stsLblFeedback1.Text = "Writing header " + Convert.ToString(i+1);
					stsFeedback.Invalidate();
					stsFeedback.Refresh();			   	
					if (i > 0)
					{
					 swOut.Write(",");
					}
					swOut.Write(QuoteValue(gridIn.Columns[i].HeaderText));
			   }
			
			   swOut.WriteLine();
			
			   //write DataGridView rows to csv
			   for (int j = 0; j <= gridIn.Rows.Count - 1; j++)
			   {
			   		stsLblFeedback1.Text = "Writing record " + Convert.ToString(j+1);
					stsFeedback.Invalidate();
					stsFeedback.Refresh();				   	
			      if (j > 0)
			      {
			      	swOut.WriteLine();
			      }
			
			      dr = gridIn.Rows[j];
			
			      for (int i = 0; i <= gridIn.Columns.Count - 1; i++)
			      {
			         if (i > 0)
			         {
			            swOut.Write(",");
			         }
			
			         value = QuoteValue(dr.Cells[i].Value.ToString());
			         //replace comma's with spaces
			         //value = value.Replace(',', ' ');
			         //replace embedded newlines with spaces
			         value = value.Replace(Environment.NewLine, " ");
			
			         swOut.Write(value);
			      }
			   }
			   swOut.Close();
			}
		}
		
		private static string QuoteValue(string value) 
		{
		    return String.Concat("\"", value.Replace("\"", "\"\""), "\"");
		}


 	public static List<NamedQuery> ReadTabDelimitedFile(string pFname)
    {
        string line;
        List<NamedQuery> sepList = new List<NamedQuery>();
        // Read the file and display it line by line.
        using (StreamReader file = new StreamReader(pFname))
        {
            while ((line = file.ReadLine()) != null)
            {

                char[] delimiters = new char[] { '\t' };
                string[] parts = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length >= 2)
                {
                	NamedQuery _nq = new NamedQuery(parts[0], parts[1]);
                	sepList.Add(_nq);
                }
            }

            file.Close();
        }
        // Suspend the screen.
        return sepList;     
    }

	}
}
