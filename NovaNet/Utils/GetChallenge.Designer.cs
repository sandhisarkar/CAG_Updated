/*
 * Created by SharpDevelop.
 * User: ArindamM
 * Date: 5/21/2009
 * Time: 6:18 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace NovaNet
{
	namespace Utils
	{

	partial class GetChallenge
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GetChallenge));
            this.lbPasswd = new System.Windows.Forms.Label();
            this.txtPasswd = new System.Windows.Forms.TextBox();
            this.lbUserID = new System.Windows.Forms.Label();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.btLogin = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbPasswd
            // 
            this.lbPasswd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.lbPasswd.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPasswd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.lbPasswd.Location = new System.Drawing.Point(12, 102);
            this.lbPasswd.Name = "lbPasswd";
            this.lbPasswd.Size = new System.Drawing.Size(77, 17);
            this.lbPasswd.TabIndex = 20;
            this.lbPasswd.Text = "Password:";
            // 
            // txtPasswd
            // 
            this.txtPasswd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.txtPasswd.ForeColor = System.Drawing.Color.White;
            this.txtPasswd.Location = new System.Drawing.Point(115, 99);
            this.txtPasswd.MaxLength = 50;
            this.txtPasswd.Name = "txtPasswd";
            this.txtPasswd.PasswordChar = '*';
            this.txtPasswd.Size = new System.Drawing.Size(298, 20);
            this.txtPasswd.TabIndex = 18;
            // 
            // lbUserID
            // 
            this.lbUserID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.lbUserID.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUserID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.lbUserID.Location = new System.Drawing.Point(12, 59);
            this.lbUserID.Name = "lbUserID";
            this.lbUserID.Size = new System.Drawing.Size(97, 23);
            this.lbUserID.TabIndex = 19;
            this.lbUserID.Text = "User ID:";
            // 
            // txtUserID
            // 
            this.txtUserID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.txtUserID.ForeColor = System.Drawing.SystemColors.Window;
            this.txtUserID.Location = new System.Drawing.Point(115, 59);
            this.txtUserID.MaxLength = 30;
            this.txtUserID.Multiline = true;
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(370, 24);
            this.txtUserID.TabIndex = 17;
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCancel.Location = new System.Drawing.Point(284, 232);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 16;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.CmdCancelClick);
            // 
            // btLogin
            // 
            this.btLogin.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btLogin.ForeColor = System.Drawing.Color.Black;
            this.btLogin.Image = ((System.Drawing.Image)(resources.GetObject("btLogin.Image")));
            this.btLogin.Location = new System.Drawing.Point(419, 97);
            this.btLogin.Name = "btLogin";
            this.btLogin.Size = new System.Drawing.Size(66, 24);
            this.btLogin.TabIndex = 15;
            this.btLogin.UseVisualStyleBackColor = false;
            this.btLogin.Click += new System.EventHandler(this.BtLoginClick);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(49)))), ((int)(((byte)(49)))));
            this.pictureBox2.Image = global::NovaNet.Properties.Resources.oie_306260v4C0xqU7;
            this.pictureBox2.Location = new System.Drawing.Point(6, 6);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(156, 40);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 21;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-1, -1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(502, 135);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // GetChallenge
            // 
            this.AcceptButton = this.btLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(499, 132);
            this.ControlBox = false;
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.lbPasswd);
            this.Controls.Add(this.txtPasswd);
            this.Controls.Add(this.lbUserID);
            this.Controls.Add(this.txtUserID);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.btLogin);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GetChallenge";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "User Login v1.0.0";
            this.Load += new System.EventHandler(this.GetChallengeLoad);
            this.Shown += new System.EventHandler(this.GetChallenge_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.Label lbPasswd;
        private System.Windows.Forms.TextBox txtPasswd;
        private System.Windows.Forms.Label lbUserID;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button btLogin;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
		
		
		
	}
		
		
	}
}

