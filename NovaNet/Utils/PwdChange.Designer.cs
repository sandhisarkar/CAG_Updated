/*
 * Created by SharpDevelop.
 * User: ArindamM
 * Date: 5/23/2009
 * Time: 3:42 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace NovaNet
{
	namespace Utils
	{

	partial class PwdChange
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PwdChange));
            this.txtConNewPwd = new System.Windows.Forms.TextBox();
            this.txtNewPwd = new System.Windows.Forms.TextBox();
            this.txtOldPwd = new System.Windows.Forms.TextBox();
            this.btCancel = new System.Windows.Forms.Button();
            this.btUpdate = new System.Windows.Forms.Button();
            this.lbConNewPwd = new System.Windows.Forms.Label();
            this.lbNewPwd = new System.Windows.Forms.Label();
            this.lbOldPwd = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtConNewPwd
            // 
            this.txtConNewPwd.Location = new System.Drawing.Point(176, 78);
            this.txtConNewPwd.MaxLength = 50;
            this.txtConNewPwd.Name = "txtConNewPwd";
            this.txtConNewPwd.PasswordChar = '*';
            this.txtConNewPwd.Size = new System.Drawing.Size(177, 20);
            this.txtConNewPwd.TabIndex = 3;
            // 
            // txtNewPwd
            // 
            this.txtNewPwd.Location = new System.Drawing.Point(176, 45);
            this.txtNewPwd.MaxLength = 50;
            this.txtNewPwd.Name = "txtNewPwd";
            this.txtNewPwd.PasswordChar = '*';
            this.txtNewPwd.Size = new System.Drawing.Size(177, 20);
            this.txtNewPwd.TabIndex = 2;
            // 
            // txtOldPwd
            // 
            this.txtOldPwd.Location = new System.Drawing.Point(176, 13);
            this.txtOldPwd.MaxLength = 50;
            this.txtOldPwd.Name = "txtOldPwd";
            this.txtOldPwd.PasswordChar = '*';
            this.txtOldPwd.Size = new System.Drawing.Size(177, 20);
            this.txtOldPwd.TabIndex = 1;
            // 
            // btCancel
            // 
            this.btCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btCancel.ForeColor = System.Drawing.Color.Black;
            this.btCancel.Location = new System.Drawing.Point(296, 125);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 5;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.BtCancelClick);
            // 
            // btUpdate
            // 
            this.btUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btUpdate.ForeColor = System.Drawing.Color.Black;
            this.btUpdate.Location = new System.Drawing.Point(215, 125);
            this.btUpdate.Name = "btUpdate";
            this.btUpdate.Size = new System.Drawing.Size(75, 23);
            this.btUpdate.TabIndex = 4;
            this.btUpdate.Text = "Save";
            this.btUpdate.UseVisualStyleBackColor = true;
            this.btUpdate.Click += new System.EventHandler(this.BtUpdateClick);
            // 
            // lbConNewPwd
            // 
            this.lbConNewPwd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbConNewPwd.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbConNewPwd.Location = new System.Drawing.Point(6, 81);
            this.lbConNewPwd.Name = "lbConNewPwd";
            this.lbConNewPwd.Size = new System.Drawing.Size(166, 17);
            this.lbConNewPwd.TabIndex = 2;
            this.lbConNewPwd.Text = "Confirm New Password:";
            // 
            // lbNewPwd
            // 
            this.lbNewPwd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNewPwd.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbNewPwd.Location = new System.Drawing.Point(6, 48);
            this.lbNewPwd.Name = "lbNewPwd";
            this.lbNewPwd.Size = new System.Drawing.Size(110, 17);
            this.lbNewPwd.TabIndex = 1;
            this.lbNewPwd.Text = "New Password:";
            // 
            // lbOldPwd
            // 
            this.lbOldPwd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbOldPwd.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbOldPwd.Location = new System.Drawing.Point(6, 16);
            this.lbOldPwd.Name = "lbOldPwd";
            this.lbOldPwd.Size = new System.Drawing.Size(140, 17);
            this.lbOldPwd.TabIndex = 0;
            this.lbOldPwd.Text = "Current Password:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtConNewPwd);
            this.groupBox1.Controls.Add(this.lbOldPwd);
            this.groupBox1.Controls.Add(this.txtNewPwd);
            this.groupBox1.Controls.Add(this.txtOldPwd);
            this.groupBox1.Controls.Add(this.lbConNewPwd);
            this.groupBox1.Controls.Add(this.lbNewPwd);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(359, 107);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // PwdChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 165);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btUpdate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "PwdChange";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Change Password";
            this.Load += new System.EventHandler(this.PwdChangeLoad);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

		}
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label lbOldPwd;
		private System.Windows.Forms.Label lbNewPwd;
		private System.Windows.Forms.Label lbConNewPwd;
		private System.Windows.Forms.Button btUpdate;
		private System.Windows.Forms.Button btCancel;
		private System.Windows.Forms.TextBox txtOldPwd;
		private System.Windows.Forms.TextBox txtNewPwd;
		private System.Windows.Forms.TextBox txtConNewPwd;
	}
}
}
