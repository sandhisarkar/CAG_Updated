/*
 * Created by SharpDevelop.
 * User: RahulN
 * Date: 19/02/2014
 * Time: 1:12 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace TestComponents
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
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
			this.cmdRptViewer = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// cmdRptViewer
			// 
			this.cmdRptViewer.Location = new System.Drawing.Point(31, 34);
			this.cmdRptViewer.Name = "cmdRptViewer";
			this.cmdRptViewer.Size = new System.Drawing.Size(118, 43);
			this.cmdRptViewer.TabIndex = 0;
			this.cmdRptViewer.Text = "View Report Viewer";
			this.cmdRptViewer.UseVisualStyleBackColor = true;
			this.cmdRptViewer.Click += new System.EventHandler(this.CmdRptViewerClick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(492, 459);
			this.Controls.Add(this.cmdRptViewer);
			this.KeyPreview = true;
			this.Name = "MainForm";
			this.Text = "TestComponents";
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainFormKeyUp);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Button cmdRptViewer;
	}
}
