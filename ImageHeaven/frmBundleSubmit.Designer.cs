
namespace ImageHeaven
{
    partial class frmBundleSubmit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBundleSubmit));
            this.cmdCancel = new System.Windows.Forms.Button();
            this.grpControl = new System.Windows.Forms.GroupBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.lblBatchSelected = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvbatch = new System.Windows.Forms.DataGridView();
            this.txtMsg = new System.Windows.Forms.TextBox();
            this.dgvImagePath = new System.Windows.Forms.DataGridView();
            this.dgvPlot = new System.Windows.Forms.DataGridView();
            this.cmdExport = new System.Windows.Forms.Button();
            this.tblExp = new System.Windows.Forms.TabControl();
            this.label2 = new System.Windows.Forms.Label();
            this.btnPopulate = new System.Windows.Forms.Button();
            this.cmbProject = new System.Windows.Forms.ComboBox();
            this.chkSelectNone = new System.Windows.Forms.CheckBox();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.cmbBundle = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grpControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvbatch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvImagePath)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlot)).BeginInit();
            this.tblExp.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdCancel
            // 
            this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdCancel.Location = new System.Drawing.Point(96, 13);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 2;
            this.cmdCancel.Text = "Close";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // grpControl
            // 
            this.grpControl.Controls.Add(this.btnSubmit);
            this.grpControl.Controls.Add(this.cmdCancel);
            this.grpControl.Location = new System.Drawing.Point(116, 394);
            this.grpControl.Name = "grpControl";
            this.grpControl.Size = new System.Drawing.Size(177, 41);
            this.grpControl.TabIndex = 34;
            this.grpControl.TabStop = false;
            // 
            // btnSubmit
            // 
            this.btnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmit.Location = new System.Drawing.Point(6, 13);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 5;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // lblBatchSelected
            // 
            this.lblBatchSelected.AutoSize = true;
            this.lblBatchSelected.Location = new System.Drawing.Point(61, 397);
            this.lblBatchSelected.Name = "lblBatchSelected";
            this.lblBatchSelected.Size = new System.Drawing.Size(0, 13);
            this.lblBatchSelected.TabIndex = 33;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 397);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 32;
            this.label6.Text = "Selected: ";
            // 
            // Column1
            // 
            this.Column1.FalseValue = "false";
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            this.Column1.TrueValue = "true";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvbatch);
            this.tabPage1.Controls.Add(this.txtMsg);
            this.tabPage1.Controls.Add(this.dgvImagePath);
            this.tabPage1.Controls.Add(this.dgvPlot);
            this.tabPage1.Controls.Add(this.cmdExport);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(290, 263);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "File";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvbatch
            // 
            this.dgvbatch.AllowUserToAddRows = false;
            this.dgvbatch.AllowUserToDeleteRows = false;
            this.dgvbatch.AllowUserToResizeColumns = false;
            this.dgvbatch.AllowUserToResizeRows = false;
            this.dgvbatch.BackgroundColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.dgvbatch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvbatch.ColumnHeadersVisible = false;
            this.dgvbatch.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.dgvbatch.Location = new System.Drawing.Point(9, 16);
            this.dgvbatch.Name = "dgvbatch";
            this.dgvbatch.RowHeadersVisible = false;
            this.dgvbatch.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvbatch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvbatch.Size = new System.Drawing.Size(275, 255);
            this.dgvbatch.TabIndex = 12;
            this.dgvbatch.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvbatch_CellContentClick);
            this.dgvbatch.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvbatch_CellValueChanged);
            // 
            // txtMsg
            // 
            this.txtMsg.BackColor = System.Drawing.Color.LightGray;
            this.txtMsg.Location = new System.Drawing.Point(446, 16);
            this.txtMsg.Multiline = true;
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMsg.Size = new System.Drawing.Size(227, 252);
            this.txtMsg.TabIndex = 1;
            this.txtMsg.Visible = false;
            // 
            // dgvImagePath
            // 
            this.dgvImagePath.AllowUserToAddRows = false;
            this.dgvImagePath.AllowUserToDeleteRows = false;
            this.dgvImagePath.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvImagePath.Location = new System.Drawing.Point(-11, 22);
            this.dgvImagePath.Name = "dgvImagePath";
            this.dgvImagePath.ReadOnly = true;
            this.dgvImagePath.Size = new System.Drawing.Size(13, 22);
            this.dgvImagePath.TabIndex = 11;
            this.dgvImagePath.Visible = false;
            // 
            // dgvPlot
            // 
            this.dgvPlot.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPlot.Location = new System.Drawing.Point(32, 178);
            this.dgvPlot.Name = "dgvPlot";
            this.dgvPlot.Size = new System.Drawing.Size(72, 56);
            this.dgvPlot.TabIndex = 9;
            this.dgvPlot.Visible = false;
            // 
            // cmdExport
            // 
            this.cmdExport.Location = new System.Drawing.Point(58, 211);
            this.cmdExport.Name = "cmdExport";
            this.cmdExport.Size = new System.Drawing.Size(75, 23);
            this.cmdExport.TabIndex = 2;
            this.cmdExport.Text = "Export";
            this.cmdExport.UseVisualStyleBackColor = true;
            this.cmdExport.Visible = false;
            // 
            // tblExp
            // 
            this.tblExp.Controls.Add(this.tabPage1);
            this.tblExp.Location = new System.Drawing.Point(0, 104);
            this.tblExp.Name = "tblExp";
            this.tblExp.SelectedIndex = 0;
            this.tblExp.Size = new System.Drawing.Size(298, 289);
            this.tblExp.TabIndex = 31;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Batch:";
            // 
            // btnPopulate
            // 
            this.btnPopulate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPopulate.Location = new System.Drawing.Point(261, 45);
            this.btnPopulate.Name = "btnPopulate";
            this.btnPopulate.Size = new System.Drawing.Size(32, 23);
            this.btnPopulate.TabIndex = 4;
            this.btnPopulate.Text = "...";
            this.btnPopulate.UseVisualStyleBackColor = true;
            this.btnPopulate.Click += new System.EventHandler(this.btnPopulate_Click);
            // 
            // cmbProject
            // 
            this.cmbProject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbProject.FormattingEnabled = true;
            this.cmbProject.Location = new System.Drawing.Point(61, 13);
            this.cmbProject.Name = "cmbProject";
            this.cmbProject.Size = new System.Drawing.Size(193, 21);
            this.cmbProject.TabIndex = 1;
            this.cmbProject.Leave += new System.EventHandler(this.cmbProject_Leave);
            // 
            // chkSelectNone
            // 
            this.chkSelectNone.AutoSize = true;
            this.chkSelectNone.Location = new System.Drawing.Point(154, 78);
            this.chkSelectNone.Name = "chkSelectNone";
            this.chkSelectNone.Size = new System.Drawing.Size(85, 17);
            this.chkSelectNone.TabIndex = 6;
            this.chkSelectNone.Text = "Select None";
            this.chkSelectNone.UseVisualStyleBackColor = true;
            this.chkSelectNone.CheckedChanged += new System.EventHandler(this.chkSelectNone_CheckedChanged);
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Location = new System.Drawing.Point(61, 78);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(70, 17);
            this.chkSelectAll.TabIndex = 5;
            this.chkSelectAll.Text = "Select All";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // cmbBundle
            // 
            this.cmbBundle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBundle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbBundle.FormattingEnabled = true;
            this.cmbBundle.Location = new System.Drawing.Point(62, 46);
            this.cmbBundle.Name = "cmbBundle";
            this.cmbBundle.Size = new System.Drawing.Size(193, 21);
            this.cmbBundle.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Project:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkSelectNone);
            this.groupBox1.Controls.Add(this.chkSelectAll);
            this.groupBox1.Controls.Add(this.cmbBundle);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnPopulate);
            this.groupBox1.Controls.Add(this.cmbProject);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(-1, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(298, 101);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            // 
            // frmBundleSubmit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(297, 437);
            this.Controls.Add(this.grpControl);
            this.Controls.Add(this.lblBatchSelected);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tblExp);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBundleSubmit";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Batch Submit";
            this.Load += new System.EventHandler(this.frmBundleSubmit_Load);
            this.grpControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvbatch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvImagePath)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlot)).EndInit();
            this.tblExp.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.GroupBox grpControl;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label lblBatchSelected;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dgvbatch;
        private System.Windows.Forms.TextBox txtMsg;
        private System.Windows.Forms.DataGridView dgvImagePath;
        private System.Windows.Forms.DataGridView dgvPlot;
        private System.Windows.Forms.Button cmdExport;
        private System.Windows.Forms.TabControl tblExp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnPopulate;
        private System.Windows.Forms.ComboBox cmbProject;
        private System.Windows.Forms.CheckBox chkSelectNone;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.ComboBox cmbBundle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}