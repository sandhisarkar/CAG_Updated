/*
 * Created by SharpDevelop.
 * User: RahulN
 * Date: 1/8/2014
 * Time: 9:01 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace nControls
{
	partial class deButton
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
			// 
			// deButton
			// 
			this.Name = "deButton";
            this.Leave += new System.EventHandler(this.deButton_Leave);
            this.Enter += new System.EventHandler(this.deButton_Enter);
            this.MouseEnter += new System.EventHandler(this.deButton_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.deButton_MouseLeave);
		}
	}
}
