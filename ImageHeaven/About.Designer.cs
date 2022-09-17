
namespace ImageHeaven
{
    partial class About
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            this.lvwAsm = new System.Windows.Forms.ListView();
            this.FullName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Version = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblVersion = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDetails = new System.Windows.Forms.TextBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // lvwAsm
            // 
            this.lvwAsm.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.FullName,
            this.Version,
            this.FPath});
            this.lvwAsm.GridLines = true;
            this.lvwAsm.HideSelection = false;
            this.lvwAsm.Location = new System.Drawing.Point(12, 217);
            this.lvwAsm.Name = "lvwAsm";
            this.lvwAsm.Size = new System.Drawing.Size(501, 143);
            this.lvwAsm.TabIndex = 16;
            this.lvwAsm.UseCompatibleStateImageBehavior = false;
            this.lvwAsm.View = System.Windows.Forms.View.Details;
            // 
            // FullName
            // 
            this.FullName.Text = "Full Name";
            this.FullName.Width = 200;
            // 
            // Version
            // 
            this.Version.Text = "Version";
            this.Version.Width = 100;
            // 
            // FPath
            // 
            this.FPath.Text = "Full Path";
            this.FPath.Width = 200;
            // 
            // lblVersion
            // 
            this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.Location = new System.Drawing.Point(6, 227);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(102, 20);
            this.lblVersion.TabIndex = 21;
            this.lblVersion.Text = "Version : ";
            this.lblVersion.Visible = false;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label4.Location = new System.Drawing.Point(9, 439);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 26);
            this.label4.TabIndex = 20;
            this.label4.Text = "www.nevaehtech.com";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 416);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(363, 23);
            this.label3.TabIndex = 19;
            this.label3.Text = "Designed && Developed by Nevaeh Technology Pvt. Ltd.";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDetails
            // 
            this.txtDetails.BackColor = System.Drawing.SystemColors.Control;
            this.txtDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDetails.Location = new System.Drawing.Point(12, 366);
            this.txtDetails.Multiline = true;
            this.txtDetails.Name = "txtDetails";
            this.txtDetails.ReadOnly = true;
            this.txtDetails.Size = new System.Drawing.Size(502, 47);
            this.txtDetails.TabIndex = 18;
            this.txtDetails.Text = resources.GetString("txtDetails.Text");
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(12, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(501, 209);
            this.pictureBox2.TabIndex = 17;
            this.pictureBox2.TabStop = false;
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 466);
            this.Controls.Add(this.lvwAsm);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDetails);
            this.Controls.Add(this.pictureBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "About";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About B\'Zer - CAG";
            this.Load += new System.EventHandler(this.About_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvwAsm;
        private System.Windows.Forms.ColumnHeader FullName;
        private System.Windows.Forms.ColumnHeader Version;
        private System.Windows.Forms.ColumnHeader FPath;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDetails;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}