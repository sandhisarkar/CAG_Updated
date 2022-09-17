/*
 * Created by SharpDevelop.
 * User: RahulN
 * Date: 19/02/2014
 * Time: 1:13 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace TestComponents
{
	partial class TestReportViewer
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
			this.SuspendLayout();
			// 
			// TestReportViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(371, 418);
			this.KeyPreview = true;
			this.Name = "TestReportViewer";
			this.Text = "TestReportViewer";
			this.Load += new System.EventHandler(this.TestReportViewerLoad);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TestReportViewerKeyUp);
			this.Resize += new System.EventHandler(this.TestReportViewerResize);
			this.ResumeLayout(false);
		}
	}
}
