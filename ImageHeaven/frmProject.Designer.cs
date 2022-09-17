
namespace ImageHeaven
{
    partial class frmProject
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProject));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new nControls.deLabel();
            this.txtProjectName = new nControls.deTextBox();
            this.CmdCancel = new nControls.deButton();
            this.CmdSave = new nControls.deButton();
            this.CmdBrowseScannLoc = new nControls.deButton();
            this.txtScannedLoc = new nControls.deTextBox();
            this.label2 = new nControls.deLabel();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Location = new System.Drawing.Point(0, 148);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(524, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // panel1
            // 
            this.panel1.AllowDrop = true;
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtProjectName);
            this.panel1.Controls.Add(this.CmdCancel);
            this.panel1.Controls.Add(this.CmdSave);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(524, 170);
            this.panel1.TabIndex = 8;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CmdBrowseScannLoc);
            this.groupBox1.Controls.Add(this.txtScannedLoc);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(4, 54);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(515, 53);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Project Location";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Project Name :";
            // 
            // txtProjectName
            // 
            this.txtProjectName.AcceptsTab = true;
            this.txtProjectName.BackColor = System.Drawing.Color.White;
            this.txtProjectName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProjectName.ForeColor = System.Drawing.Color.Black;
            this.txtProjectName.Location = new System.Drawing.Point(99, 16);
            this.txtProjectName.Mandatory = true;
            this.txtProjectName.Name = "txtProjectName";
            this.txtProjectName.Size = new System.Drawing.Size(418, 23);
            this.txtProjectName.TabIndex = 1;
            this.txtProjectName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtProjectName_KeyPress);
            this.txtProjectName.Leave += new System.EventHandler(this.txtProjectName_Leave);
            this.txtProjectName.MouseLeave += new System.EventHandler(this.txtProjectName_MouseLeave);
            // 
            // CmdCancel
            // 
            this.CmdCancel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.CmdCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.CmdCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.CmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CmdCancel.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmdCancel.Location = new System.Drawing.Point(429, 111);
            this.CmdCancel.Name = "CmdCancel";
            this.CmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.CmdCancel.Size = new System.Drawing.Size(81, 30);
            this.CmdCancel.TabIndex = 6;
            this.CmdCancel.Text = "C&lose";
            this.CmdCancel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.CmdCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.CmdCancel.UseCompatibleTextRendering = true;
            this.CmdCancel.UseVisualStyleBackColor = false;
            this.CmdCancel.Click += new System.EventHandler(this.CmdCancel_Click);
            // 
            // CmdSave
            // 
            this.CmdSave.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.CmdSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.CmdSave.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.CmdSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CmdSave.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText;
            this.CmdSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CmdSave.Location = new System.Drawing.Point(318, 112);
            this.CmdSave.Name = "CmdSave";
            this.CmdSave.Size = new System.Drawing.Size(64, 30);
            this.CmdSave.TabIndex = 5;
            this.CmdSave.Text = "&Save";
            this.CmdSave.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.CmdSave.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.CmdSave.UseCompatibleTextRendering = true;
            this.CmdSave.UseVisualStyleBackColor = false;
            this.CmdSave.Click += new System.EventHandler(this.CmdSave_Click);
            // 
            // CmdBrowseScannLoc
            // 
            this.CmdBrowseScannLoc.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.CmdBrowseScannLoc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.CmdBrowseScannLoc.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.CmdBrowseScannLoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CmdBrowseScannLoc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmdBrowseScannLoc.Location = new System.Drawing.Point(462, 20);
            this.CmdBrowseScannLoc.Name = "CmdBrowseScannLoc";
            this.CmdBrowseScannLoc.Size = new System.Drawing.Size(40, 27);
            this.CmdBrowseScannLoc.TabIndex = 4;
            this.CmdBrowseScannLoc.Text = "...";
            this.CmdBrowseScannLoc.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.CmdBrowseScannLoc.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.CmdBrowseScannLoc.UseCompatibleTextRendering = true;
            this.CmdBrowseScannLoc.UseVisualStyleBackColor = false;
            this.CmdBrowseScannLoc.Click += new System.EventHandler(this.CmdBrowseScannLoc_Click);
            // 
            // txtScannedLoc
            // 
            this.txtScannedLoc.BackColor = System.Drawing.Color.White;
            this.txtScannedLoc.Enabled = false;
            this.txtScannedLoc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtScannedLoc.ForeColor = System.Drawing.Color.Black;
            this.txtScannedLoc.Location = new System.Drawing.Point(96, 22);
            this.txtScannedLoc.Mandatory = true;
            this.txtScannedLoc.Name = "txtScannedLoc";
            this.txtScannedLoc.Size = new System.Drawing.Size(359, 23);
            this.txtScannedLoc.TabIndex = 78;
            // 
            // label2
            // 
            this.label2.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(11, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Folder Name :";
            // 
            // frmProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 170);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmProject";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Project";
            this.Load += new System.EventHandler(this.frmProject_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Panel panel1;
        private nControls.deLabel label1;
        private nControls.deTextBox txtProjectName;
        private nControls.deButton CmdCancel;
        private nControls.deButton CmdSave;
        private System.Windows.Forms.GroupBox groupBox1;
        private nControls.deButton CmdBrowseScannLoc;
        private nControls.deTextBox txtScannedLoc;
        private nControls.deLabel label2;
    }
}