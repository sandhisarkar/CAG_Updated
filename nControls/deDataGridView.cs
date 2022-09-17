/*
 * Created by SharpDevelop.
 * User: RahulN
 * Date: 1/9/2014
 * Time: 6:20 PM
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
	/// Description of deDataGridView.
	/// </summary>
	public partial class deDataGridView : DataGridView
	{
		public deDataGridView()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
            this.AllowUserToAddRows = false;
            this.AllowUserToDeleteRows = false;
            this.AlternatingRowsDefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.AlternatingRowsDefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.ColumnHeadersDefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.ColumnHeadersDefaultCellStyle.SelectionBackColor = System.Drawing.Color.WhiteSmoke;
            this.ColumnHeadersDefaultCellStyle.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            this.ColumnHeadersDefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Location = new System.Drawing.Point(8, 16);


		}
	}
}
