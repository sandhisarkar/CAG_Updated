namespace ImageHeaven
{
    partial class frmLoggedUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLoggedUser));
            this.lvw = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmdUnlock = new System.Windows.Forms.Button();
            this.cmdRefresh = new System.Windows.Forms.Button();
            this.cmdClose = new System.Windows.Forms.Button();
            this.lblTotUsers = new System.Windows.Forms.Label();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lvw
            // 
            this.lvw.CheckBoxes = true;
            this.lvw.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader4,
            this.columnHeader3});
            this.lvw.GridLines = true;
            this.lvw.HideSelection = false;
            this.lvw.Location = new System.Drawing.Point(2, 26);
            this.lvw.Name = "lvw";
            this.lvw.Size = new System.Drawing.Size(348, 431);
            this.lvw.TabIndex = 3;
            this.lvw.UseCompatibleStateImageBehavior = false;
            this.lvw.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "User Name";
            this.columnHeader1.Width = 90;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "User ID";
            this.columnHeader2.Width = 84;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "DateTime";
            this.columnHeader3.Width = 100;
            // 
            // cmdUnlock
            // 
            this.cmdUnlock.Location = new System.Drawing.Point(194, 463);
            this.cmdUnlock.Name = "cmdUnlock";
            this.cmdUnlock.Size = new System.Drawing.Size(75, 23);
            this.cmdUnlock.TabIndex = 4;
            this.cmdUnlock.Text = "Unlock";
            this.cmdUnlock.UseVisualStyleBackColor = true;
            this.cmdUnlock.Click += new System.EventHandler(this.cmdUnlock_Click);
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.Location = new System.Drawing.Point(113, 463);
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.Size = new System.Drawing.Size(75, 23);
            this.cmdRefresh.TabIndex = 4;
            this.cmdRefresh.Text = "Refresh";
            this.cmdRefresh.UseVisualStyleBackColor = true;
            this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(275, 463);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(75, 23);
            this.cmdClose.TabIndex = 4;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // lblTotUsers
            // 
            this.lblTotUsers.AutoSize = true;
            this.lblTotUsers.Location = new System.Drawing.Point(2, 7);
            this.lblTotUsers.Name = "lblTotUsers";
            this.lblTotUsers.Size = new System.Drawing.Size(0, 13);
            this.lblTotUsers.TabIndex = 5;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "User Role";
            this.columnHeader4.Width = 81;
            // 
            // frmLoggedUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 490);
            this.Controls.Add(this.lblTotUsers);
            this.Controls.Add(this.cmdRefresh);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdUnlock);
            this.Controls.Add(this.lvw);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmLoggedUser";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Online Users";
            this.Load += new System.EventHandler(this.frmLoggedUser_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvw;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button cmdUnlock;
        private System.Windows.Forms.Button cmdRefresh;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Label lblTotUsers;
        private System.Windows.Forms.ColumnHeader columnHeader4;
    }
}