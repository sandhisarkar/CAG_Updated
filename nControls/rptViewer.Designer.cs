/*
 * Created by SharpDevelop.
 * User: RahulN
 * Date: 19/02/2014
 * Time: 12:46 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace nControls
{
	partial class rptViewer
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the control.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(rptViewer));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			this.stsFeedback = new System.Windows.Forms.StatusStrip();
			this.tsrFunctions = new System.Windows.Forms.ToolStrip();
			this.tsr_cmdExportToExcel = new System.Windows.Forms.ToolStripButton();
			this.sptPanel = new System.Windows.Forms.SplitContainer();
			this.lvwQueries = new System.Windows.Forms.ListView();
			this.colNames = new System.Windows.Forms.ColumnHeader();
			this.colQueries = new System.Windows.Forms.ColumnHeader();
			this.dgvRecords = new nControls.deDataGridView();
			this.stsLblFeedback1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.stsFeedback.SuspendLayout();
			this.tsrFunctions.SuspendLayout();
			this.sptPanel.Panel1.SuspendLayout();
			this.sptPanel.Panel2.SuspendLayout();
			this.sptPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvRecords)).BeginInit();
			this.SuspendLayout();
			// 
			// stsFeedback
			// 
			this.stsFeedback.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.stsLblFeedback1});
			this.stsFeedback.Location = new System.Drawing.Point(0, 475);
			this.stsFeedback.Name = "stsFeedback";
			this.stsFeedback.Size = new System.Drawing.Size(604, 22);
			this.stsFeedback.TabIndex = 0;
			this.stsFeedback.Text = "Test";
			// 
			// tsrFunctions
			// 
			this.tsrFunctions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.tsr_cmdExportToExcel});
			this.tsrFunctions.Location = new System.Drawing.Point(0, 0);
			this.tsrFunctions.Name = "tsrFunctions";
			this.tsrFunctions.Size = new System.Drawing.Size(604, 25);
			this.tsrFunctions.TabIndex = 1;
			// 
			// tsr_cmdExportToExcel
			// 
			this.tsr_cmdExportToExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsr_cmdExportToExcel.Image = ((System.Drawing.Image)(resources.GetObject("tsr_cmdExportToExcel.Image")));
			this.tsr_cmdExportToExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsr_cmdExportToExcel.Name = "tsr_cmdExportToExcel";
			this.tsr_cmdExportToExcel.Size = new System.Drawing.Size(23, 22);
			this.tsr_cmdExportToExcel.Text = "Export to Excel";
			this.tsr_cmdExportToExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.tsr_cmdExportToExcel.Click += new System.EventHandler(this.Tsr_cmdExportToExcelClick);
			// 
			// sptPanel
			// 
			this.sptPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.sptPanel.Location = new System.Drawing.Point(0, 25);
			this.sptPanel.Name = "sptPanel";
			// 
			// sptPanel.Panel1
			// 
			this.sptPanel.Panel1.Controls.Add(this.lvwQueries);
			this.sptPanel.Panel1.Resize += new System.EventHandler(this.Panel1Resize);
			// 
			// sptPanel.Panel2
			// 
			this.sptPanel.Panel2.Controls.Add(this.dgvRecords);
			this.sptPanel.Panel2.Resize += new System.EventHandler(this.Panel2Resize);
			this.sptPanel.Size = new System.Drawing.Size(604, 450);
			this.sptPanel.SplitterDistance = 170;
			this.sptPanel.TabIndex = 2;
			// 
			// lvwQueries
			// 
			this.lvwQueries.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.lvwQueries.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.colNames,
									this.colQueries});
			this.lvwQueries.Location = new System.Drawing.Point(22, 32);
			this.lvwQueries.Name = "lvwQueries";
			this.lvwQueries.Size = new System.Drawing.Size(124, 224);
			this.lvwQueries.TabIndex = 0;
			this.lvwQueries.UseCompatibleStateImageBehavior = false;
			this.lvwQueries.View = System.Windows.Forms.View.Details;
			this.lvwQueries.DoubleClick += new System.EventHandler(this.LvwQueriesDoubleClick);
			// 
			// colNames
			// 
			this.colNames.Text = "Queries";
			this.colNames.Width = 76;
			// 
			// colQueries
			// 
			this.colQueries.Text = "Command";
			// 
			// dgvRecords
			// 
			this.dgvRecords.AllowUserToAddRows = false;
			this.dgvRecords.AllowUserToDeleteRows = false;
			dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
			dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White;
			this.dgvRecords.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
			this.dgvRecords.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dgvRecords.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlDark;
			dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.WhiteSmoke;
			dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvRecords.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
			this.dgvRecords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvRecords.Location = new System.Drawing.Point(29, 111);
			this.dgvRecords.Name = "dgvRecords";
			this.dgvRecords.Size = new System.Drawing.Size(73, 80);
			this.dgvRecords.TabIndex = 0;
			// 
			// stsLblFeedback1
			// 
			this.stsLblFeedback1.Name = "stsLblFeedback1";
			this.stsLblFeedback1.Size = new System.Drawing.Size(38, 17);
			this.stsLblFeedback1.Text = "Ready";
			// 
			// rptViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.sptPanel);
			this.Controls.Add(this.tsrFunctions);
			this.Controls.Add(this.stsFeedback);
			this.Name = "rptViewer";
			this.Size = new System.Drawing.Size(604, 497);
			this.stsFeedback.ResumeLayout(false);
			this.stsFeedback.PerformLayout();
			this.tsrFunctions.ResumeLayout(false);
			this.tsrFunctions.PerformLayout();
			this.sptPanel.Panel1.ResumeLayout(false);
			this.sptPanel.Panel2.ResumeLayout(false);
			this.sptPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgvRecords)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.ToolStripStatusLabel stsLblFeedback1;
		private System.Windows.Forms.ToolStripButton tsr_cmdExportToExcel;
		private System.Windows.Forms.ColumnHeader colQueries;
		private System.Windows.Forms.ColumnHeader colNames;
		private nControls.deDataGridView dgvRecords;
		private System.Windows.Forms.ListView lvwQueries;
		private System.Windows.Forms.SplitContainer sptPanel;
		private System.Windows.Forms.ToolStrip tsrFunctions;
		private System.Windows.Forms.StatusStrip stsFeedback;
	}
}
