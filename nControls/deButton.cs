/*
 * Created by SharpDevelop.
 * User: RahulN
 * Date: 1/8/2014
 * Time: 9:01 PM
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
	/// Description of deButton.
	/// </summary>
	public partial class deButton : Button
	{
        System.Drawing.Color swpColor;
        System.Drawing.Color swpHColor;
		public deButton()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
            this.FlatAppearance.BorderSize = 1;
            this.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.UseCompatibleTextRendering = true;
            this.UseVisualStyleBackColor = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		}
        private void deButton_Enter(object sender, EventArgs e)
        {
            //this.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F1F6");
            //this.FlatAppearance.BorderSize = 3;
            swpColor = this.FlatAppearance.BorderColor;
            this.FlatAppearance.BorderColor = Color.DarkKhaki;
        }
        private void deButton_Leave(object sender, EventArgs e)
        {
            //this.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F1F6");
            this.FlatAppearance.BorderSize = 1;
            this.FlatAppearance.BorderColor = swpColor;
        }
        private void deButton_MouseEnter(object sender, EventArgs e)
        {
            //this.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F1F6");
            this.FlatAppearance.BorderSize = 1;
            swpHColor = this.FlatAppearance.BorderColor;
            this.FlatAppearance.BorderColor = Color.DarkGoldenrod;
        }
        private void deButton_MouseLeave(object sender, EventArgs e)
        {
            //this.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F1F6");
            //this.FlatAppearance.BorderSize = 1;
            //this.FlatAppearance.BorderColor = swpHColor;
        }
    }
}
