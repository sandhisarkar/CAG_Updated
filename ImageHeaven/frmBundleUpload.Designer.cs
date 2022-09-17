
namespace ImageHeaven
{
    partial class frmBundleUpload
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBundleUpload));
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.cmdExport = new nControls.deButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lstImage = new System.Windows.Forms.ListBox();
            this.grdCsv = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmdsearch = new nControls.deButton();
            this.cmbBundle = new nControls.deComboBox();
            this.deLabel2 = new nControls.deLabel();
            this.cmbProject = new nControls.deComboBox();
            this.deLabel1 = new nControls.deLabel();
            this.sfdUAT = new System.Windows.Forms.SaveFileDialog();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdCsv)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.statusStrip1);
            this.panel1.Controls.Add(this.cmdExport);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(667, 384);
            this.panel1.TabIndex = 1;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 362);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(667, 22);
            this.statusStrip1.TabIndex = 19;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // cmdExport
            // 
            this.cmdExport.Enabled = false;
            this.cmdExport.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cmdExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdExport.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExport.Location = new System.Drawing.Point(577, 328);
            this.cmdExport.Name = "cmdExport";
            this.cmdExport.Size = new System.Drawing.Size(75, 26);
            this.cmdExport.TabIndex = 18;
            this.cmdExport.Text = "&Import";
            this.cmdExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdExport.UseCompatibleTextRendering = true;
            this.cmdExport.UseVisualStyleBackColor = true;
            this.cmdExport.Click += new System.EventHandler(this.cmdExport_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.lstImage);
            this.groupBox2.Controls.Add(this.grdCsv);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 61);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(667, 262);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(721, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(702, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "label1";
            // 
            // lstImage
            // 
            this.lstImage.FormattingEnabled = true;
            this.lstImage.Location = new System.Drawing.Point(696, 102);
            this.lstImage.Name = "lstImage";
            this.lstImage.Size = new System.Drawing.Size(120, 95);
            this.lstImage.TabIndex = 17;
            // 
            // grdCsv
            // 
            this.grdCsv.AllowUserToAddRows = false;
            this.grdCsv.AllowUserToDeleteRows = false;
            this.grdCsv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdCsv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCsv.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.grdCsv.Location = new System.Drawing.Point(3, 16);
            this.grdCsv.Name = "grdCsv";
            this.grdCsv.ReadOnly = true;
            this.grdCsv.Size = new System.Drawing.Size(661, 243);
            this.grdCsv.TabIndex = 16;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmdsearch);
            this.groupBox1.Controls.Add(this.cmbBundle);
            this.groupBox1.Controls.Add(this.deLabel2);
            this.groupBox1.Controls.Add(this.cmbProject);
            this.groupBox1.Controls.Add(this.deLabel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(667, 61);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // cmdsearch
            // 
            this.cmdsearch.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cmdsearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdsearch.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdsearch.Location = new System.Drawing.Point(576, 25);
            this.cmdsearch.Name = "cmdsearch";
            this.cmdsearch.Size = new System.Drawing.Size(75, 23);
            this.cmdsearch.TabIndex = 28;
            this.cmdsearch.Text = "&Search";
            this.cmdsearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdsearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdsearch.UseCompatibleTextRendering = true;
            this.cmdsearch.UseVisualStyleBackColor = true;
            this.cmdsearch.Click += new System.EventHandler(this.cmdsearch_Click);
            // 
            // cmbBundle
            // 
            this.cmbBundle.BackColor = System.Drawing.Color.White;
            this.cmbBundle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBundle.ForeColor = System.Drawing.Color.Black;
            this.cmbBundle.FormattingEnabled = true;
            this.cmbBundle.Location = new System.Drawing.Point(365, 28);
            this.cmbBundle.Mandatory = true;
            this.cmbBundle.Name = "cmbBundle";
            this.cmbBundle.Size = new System.Drawing.Size(196, 21);
            this.cmbBundle.TabIndex = 3;
            // 
            // deLabel2
            // 
            this.deLabel2.AutoSize = true;
            this.deLabel2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deLabel2.Location = new System.Drawing.Point(270, 30);
            this.deLabel2.Name = "deLabel2";
            this.deLabel2.Size = new System.Drawing.Size(85, 15);
            this.deLabel2.TabIndex = 2;
            this.deLabel2.Text = "Bundle Name :";
            // 
            // cmbProject
            // 
            this.cmbProject.BackColor = System.Drawing.Color.White;
            this.cmbProject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProject.ForeColor = System.Drawing.Color.Black;
            this.cmbProject.FormattingEnabled = true;
            this.cmbProject.Location = new System.Drawing.Point(63, 27);
            this.cmbProject.Mandatory = true;
            this.cmbProject.Name = "cmbProject";
            this.cmbProject.Size = new System.Drawing.Size(180, 21);
            this.cmbProject.TabIndex = 1;
            this.cmbProject.Leave += new System.EventHandler(this.cmbProject_Leave);
            // 
            // deLabel1
            // 
            this.deLabel1.AutoSize = true;
            this.deLabel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deLabel1.Location = new System.Drawing.Point(7, 29);
            this.deLabel1.Name = "deLabel1";
            this.deLabel1.Size = new System.Drawing.Size(50, 15);
            this.deLabel1.TabIndex = 0;
            this.deLabel1.Text = "Project :";
            // 
            // frmBundleUpload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 384);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBundleUpload";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Batch Upload";
            this.Load += new System.EventHandler(this.frmBundleUpload_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdCsv)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private nControls.deButton cmdExport;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstImage;
        private System.Windows.Forms.DataGridView grdCsv;
        private System.Windows.Forms.GroupBox groupBox1;
        private nControls.deButton cmdsearch;
        private nControls.deComboBox cmbBundle;
        private nControls.deLabel deLabel2;
        private nControls.deComboBox cmbProject;
        private nControls.deLabel deLabel1;
        private System.Windows.Forms.SaveFileDialog sfdUAT;
    }
}