/*
 * Created by SharpDevelop.
 * User: ArindamM
 * Date: 5/23/2009
 * Time: 3:59 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace NovaNet
{
	namespace Utils
	{

	partial class AddNewUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddNewUser));
            this.btCancel = new System.Windows.Forms.Button();
            this.btSave = new System.Windows.Forms.Button();
            this.txtConPwd = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbUserPwd = new System.Windows.Forms.Label();
            this.lbUserName = new System.Windows.Forms.Label();
            this.lbUserId = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmboRole = new System.Windows.Forms.ComboBox();
            this.lbRoledes = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btCancel
            // 
            this.btCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btCancel.ForeColor = System.Drawing.Color.Black;
            this.btCancel.Location = new System.Drawing.Point(301, 201);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 7;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.BtCancelClick);
            // 
            // btSave
            // 
            this.btSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btSave.ForeColor = System.Drawing.Color.Black;
            this.btSave.Location = new System.Drawing.Point(220, 201);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(75, 23);
            this.btSave.TabIndex = 6;
            this.btSave.Text = "Save";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.BtSaveClick);
            // 
            // txtConPwd
            // 
            this.txtConPwd.Location = new System.Drawing.Point(147, 150);
            this.txtConPwd.MaxLength = 50;
            this.txtConPwd.Name = "txtConPwd";
            this.txtConPwd.PasswordChar = '*';
            this.txtConPwd.Size = new System.Drawing.Size(135, 20);
            this.txtConPwd.TabIndex = 5;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(147, 116);
            this.txtPassword.MaxLength = 50;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(135, 20);
            this.txtPassword.TabIndex = 4;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(147, 50);
            this.txtUserName.MaxLength = 100;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(212, 20);
            this.txtUserName.TabIndex = 2;
            // 
            // txtUserId
            // 
            this.txtUserId.Location = new System.Drawing.Point(147, 16);
            this.txtUserId.MaxLength = 30;
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(135, 20);
            this.txtUserId.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(24, 150);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Confirm Password:";
            // 
            // lbUserPwd
            // 
            this.lbUserPwd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUserPwd.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbUserPwd.Location = new System.Drawing.Point(24, 116);
            this.lbUserPwd.Name = "lbUserPwd";
            this.lbUserPwd.Size = new System.Drawing.Size(91, 20);
            this.lbUserPwd.TabIndex = 2;
            this.lbUserPwd.Text = "Password:";
            // 
            // lbUserName
            // 
            this.lbUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUserName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbUserName.Location = new System.Drawing.Point(24, 51);
            this.lbUserName.Name = "lbUserName";
            this.lbUserName.Size = new System.Drawing.Size(91, 17);
            this.lbUserName.TabIndex = 1;
            this.lbUserName.Text = "User Name:";
            // 
            // lbUserId
            // 
            this.lbUserId.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUserId.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbUserId.Location = new System.Drawing.Point(24, 16);
            this.lbUserId.Name = "lbUserId";
            this.lbUserId.Size = new System.Drawing.Size(73, 20);
            this.lbUserId.TabIndex = 0;
            this.lbUserId.Text = "User ID:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmboRole);
            this.groupBox1.Controls.Add(this.lbRoledes);
            this.groupBox1.Controls.Add(this.lbUserId);
            this.groupBox1.Controls.Add(this.txtUserId);
            this.groupBox1.Controls.Add(this.txtConPwd);
            this.groupBox1.Controls.Add(this.lbUserName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.txtUserName);
            this.groupBox1.Controls.Add(this.lbUserPwd);
            this.groupBox1.Location = new System.Drawing.Point(5, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(371, 182);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // cmboRole
            // 
            this.cmboRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmboRole.FormattingEnabled = true;
            this.cmboRole.Location = new System.Drawing.Point(147, 82);
            this.cmboRole.Name = "cmboRole";
            this.cmboRole.Size = new System.Drawing.Size(135, 21);
            this.cmboRole.TabIndex = 3;
            // 
            // lbRoledes
            // 
            this.lbRoledes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRoledes.Location = new System.Drawing.Point(24, 83);
            this.lbRoledes.Name = "lbRoledes";
            this.lbRoledes.Size = new System.Drawing.Size(114, 18);
            this.lbRoledes.TabIndex = 8;
            this.lbRoledes.Text = "Role Description:";
            // 
            // AddNewUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(382, 232);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddNewUser";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add New User";
            this.Load += new System.EventHandler(this.AddNewUserLoad);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

		}
		private System.Windows.Forms.Label lbRoledes;
		private System.Windows.Forms.ComboBox cmboRole;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label lbUserId;
		private System.Windows.Forms.Label lbUserName;
		private System.Windows.Forms.Label lbUserPwd;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtUserId;
		private System.Windows.Forms.TextBox txtUserName;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.TextBox txtConPwd;
		private System.Windows.Forms.Button btSave;
		private System.Windows.Forms.Button btCancel;
	}
}
}
