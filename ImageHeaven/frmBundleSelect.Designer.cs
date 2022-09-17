
namespace ImageHeaven
{
    partial class frmBundleSelect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBundleSelect));
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkPhotoScan = new System.Windows.Forms.CheckBox();
            this.btnCancel = new nControls.deButton();
            this.btnOK = new nControls.deButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbBundle = new nControls.deComboBox();
            this.labelBundle = new nControls.deLabel();
            this.cmbProject = new nControls.deComboBox();
            this.labelProj = new nControls.deLabel();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkPhotoScan);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(422, 150);
            this.panel1.TabIndex = 1;
            // 
            // chkPhotoScan
            // 
            this.chkPhotoScan.AutoSize = true;
            this.chkPhotoScan.Enabled = false;
            this.chkPhotoScan.Location = new System.Drawing.Point(37, 120);
            this.chkPhotoScan.Name = "chkPhotoScan";
            this.chkPhotoScan.Size = new System.Drawing.Size(82, 17);
            this.chkPhotoScan.TabIndex = 6;
            this.chkPhotoScan.Text = "Photo Scan";
            this.chkPhotoScan.UseVisualStyleBackColor = true;
            this.chkPhotoScan.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(251, 115);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 26);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cance&l";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseCompatibleTextRendering = true;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(135, 116);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 25);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "&Ok";
            this.btnOK.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOK.UseCompatibleTextRendering = true;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbBundle);
            this.groupBox1.Controls.Add(this.labelBundle);
            this.groupBox1.Controls.Add(this.cmbProject);
            this.groupBox1.Controls.Add(this.labelProj);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(2, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(418, 107);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Group Selection";
            // 
            // cmbBundle
            // 
            this.cmbBundle.BackColor = System.Drawing.Color.White;
            this.cmbBundle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBundle.ForeColor = System.Drawing.Color.Black;
            this.cmbBundle.FormattingEnabled = true;
            this.cmbBundle.Location = new System.Drawing.Point(136, 64);
            this.cmbBundle.Mandatory = true;
            this.cmbBundle.Name = "cmbBundle";
            this.cmbBundle.Size = new System.Drawing.Size(211, 25);
            this.cmbBundle.TabIndex = 3;
            // 
            // labelBundle
            // 
            this.labelBundle.AutoSize = true;
            this.labelBundle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBundle.Location = new System.Drawing.Point(52, 69);
            this.labelBundle.Name = "labelBundle";
            this.labelBundle.Size = new System.Drawing.Size(78, 15);
            this.labelBundle.TabIndex = 2;
            this.labelBundle.Text = "Batch Name :";
            // 
            // cmbProject
            // 
            this.cmbProject.BackColor = System.Drawing.Color.White;
            this.cmbProject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProject.ForeColor = System.Drawing.Color.Black;
            this.cmbProject.FormattingEnabled = true;
            this.cmbProject.Location = new System.Drawing.Point(136, 26);
            this.cmbProject.Mandatory = true;
            this.cmbProject.Name = "cmbProject";
            this.cmbProject.Size = new System.Drawing.Size(209, 25);
            this.cmbProject.TabIndex = 1;
            this.cmbProject.Leave += new System.EventHandler(this.cmbProject_Leave);
            // 
            // labelProj
            // 
            this.labelProj.AutoSize = true;
            this.labelProj.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProj.Location = new System.Drawing.Point(45, 33);
            this.labelProj.Name = "labelProj";
            this.labelProj.Size = new System.Drawing.Size(85, 15);
            this.labelProj.TabIndex = 0;
            this.labelProj.Text = "Project Name :";
            // 
            // frmBundleSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(422, 150);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBundleSelect";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Batch Select";
            this.Load += new System.EventHandler(this.frmBundleSelect_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.CheckBox chkPhotoScan;
        private nControls.deButton btnCancel;
        private nControls.deButton btnOK;
        private System.Windows.Forms.GroupBox groupBox1;
        private nControls.deComboBox cmbBundle;
        private nControls.deLabel labelBundle;
        private nControls.deComboBox cmbProject;
        private nControls.deLabel labelProj;
    }
}