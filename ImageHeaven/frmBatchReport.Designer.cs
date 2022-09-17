
namespace ImageHeaven
{
    partial class frmBatchReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBatchReport));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.grdStatus = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.deButton20 = new nControls.deButton();
            this.deButton21 = new nControls.deButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmdSearch = new nControls.deButton();
            this.deComboBox2 = new nControls.deComboBox();
            this.deLabel2 = new nControls.deLabel();
            this.deComboBox1 = new nControls.deComboBox();
            this.deLabel1 = new nControls.deLabel();
            this.sfdUAT = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdStatus)).BeginInit();
            this.panel2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel3);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(800, 450);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.groupBox3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 73);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(794, 318);
            this.panel3.TabIndex = 2;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.grdStatus);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(794, 318);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "File Status :";
            // 
            // grdStatus
            // 
            this.grdStatus.AllowUserToAddRows = false;
            this.grdStatus.AllowUserToDeleteRows = false;
            this.grdStatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdStatus.Location = new System.Drawing.Point(3, 21);
            this.grdStatus.MultiSelect = false;
            this.grdStatus.Name = "grdStatus";
            this.grdStatus.ReadOnly = true;
            this.grdStatus.RowHeadersWidth = 62;
            this.grdStatus.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdStatus.Size = new System.Drawing.Size(788, 294);
            this.grdStatus.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 391);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(794, 56);
            this.panel2.TabIndex = 1;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.deButton20);
            this.groupBox4.Controls.Add(this.deButton21);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(794, 56);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            // 
            // deButton20
            // 
            this.deButton20.BackColor = System.Drawing.Color.White;
            this.deButton20.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("deButton20.BackgroundImage")));
            this.deButton20.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.deButton20.Enabled = false;
            this.deButton20.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.deButton20.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deButton20.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deButton20.Location = new System.Drawing.Point(526, 16);
            this.deButton20.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.deButton20.Name = "deButton20";
            this.deButton20.Size = new System.Drawing.Size(157, 30);
            this.deButton20.TabIndex = 10;
            this.deButton20.Text = "&Export to Excel";
            this.deButton20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.deButton20.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.deButton20.UseCompatibleTextRendering = true;
            this.deButton20.UseVisualStyleBackColor = false;
            this.deButton20.Click += new System.EventHandler(this.deButton20_Click);
            // 
            // deButton21
            // 
            this.deButton21.BackColor = System.Drawing.Color.White;
            this.deButton21.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("deButton21.BackgroundImage")));
            this.deButton21.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.deButton21.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.deButton21.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deButton21.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deButton21.Location = new System.Drawing.Point(699, 16);
            this.deButton21.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.deButton21.Name = "deButton21";
            this.deButton21.Size = new System.Drawing.Size(87, 30);
            this.deButton21.TabIndex = 11;
            this.deButton21.Text = "A&bort";
            this.deButton21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.deButton21.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.deButton21.UseCompatibleTextRendering = true;
            this.deButton21.UseVisualStyleBackColor = false;
            this.deButton21.Click += new System.EventHandler(this.deButton21_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(794, 57);
            this.panel1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmdSearch);
            this.groupBox2.Controls.Add(this.deComboBox2);
            this.groupBox2.Controls.Add(this.deLabel2);
            this.groupBox2.Controls.Add(this.deComboBox1);
            this.groupBox2.Controls.Add(this.deLabel1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(794, 57);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Search Criteria :";
            // 
            // cmdSearch
            // 
            this.cmdSearch.BackColor = System.Drawing.SystemColors.Control;
            this.cmdSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdSearch.BackgroundImage")));
            this.cmdSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdSearch.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cmdSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSearch.Location = new System.Drawing.Point(621, 17);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(153, 31);
            this.cmdSearch.TabIndex = 14;
            this.cmdSearch.Text = "&Search for Batch";
            this.cmdSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdSearch.UseCompatibleTextRendering = true;
            this.cmdSearch.UseVisualStyleBackColor = false;
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // deComboBox2
            // 
            this.deComboBox2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.deComboBox2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.deComboBox2.BackColor = System.Drawing.Color.White;
            this.deComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.deComboBox2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deComboBox2.ForeColor = System.Drawing.Color.Black;
            this.deComboBox2.FormattingEnabled = true;
            this.deComboBox2.Items.AddRange(new object[] {
            "All Batch",
            "Pending for Metadata Entry",
            "Pending for Upload",
            "Pending for Scan",
            "Pending for QC",
            "Pending for FQC",
            "Pending for Submission",
            "Pending for Certification(Phase-I)",
            "Pending for Certification(Phase-II)",
            "Pending for Export",
            "Exported"});
            this.deComboBox2.Location = new System.Drawing.Point(372, 21);
            this.deComboBox2.Mandatory = false;
            this.deComboBox2.Name = "deComboBox2";
            this.deComboBox2.Size = new System.Drawing.Size(217, 25);
            this.deComboBox2.TabIndex = 12;
            // 
            // deLabel2
            // 
            this.deLabel2.AutoSize = true;
            this.deLabel2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deLabel2.Location = new System.Drawing.Point(327, 25);
            this.deLabel2.Name = "deLabel2";
            this.deLabel2.Size = new System.Drawing.Size(43, 15);
            this.deLabel2.TabIndex = 13;
            this.deLabel2.Text = "Batch :";
            // 
            // deComboBox1
            // 
            this.deComboBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.deComboBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.deComboBox1.BackColor = System.Drawing.Color.White;
            this.deComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.deComboBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deComboBox1.ForeColor = System.Drawing.Color.Black;
            this.deComboBox1.FormattingEnabled = true;
            this.deComboBox1.Items.AddRange(new object[] {
            "All Batch",
            "Pending for Metadata Entry",
            "Pending for Upload",
            "Pending for Scan",
            "Pending for QC",
            "Pending for FQC",
            "Pending for Submission",
            "Pending for Certification(Phase-I)",
            "Pending for Certification(Phase-II)",
            "Pending for Export",
            "Exported"});
            this.deComboBox1.Location = new System.Drawing.Point(88, 22);
            this.deComboBox1.Mandatory = false;
            this.deComboBox1.Name = "deComboBox1";
            this.deComboBox1.Size = new System.Drawing.Size(217, 25);
            this.deComboBox1.TabIndex = 10;
            this.deComboBox1.Leave += new System.EventHandler(this.deComboBox1_Leave);
            // 
            // deLabel1
            // 
            this.deLabel1.AutoSize = true;
            this.deLabel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deLabel1.Location = new System.Drawing.Point(31, 26);
            this.deLabel1.Name = "deLabel1";
            this.deLabel1.Size = new System.Drawing.Size(50, 15);
            this.deLabel1.TabIndex = 11;
            this.deLabel1.Text = "Project :";
            // 
            // frmBatchReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBatchReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Batch Wise Report";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmBatchReport_Load);
            this.groupBox1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdStatus)).EndInit();
            this.panel2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private nControls.deComboBox deComboBox2;
        private nControls.deLabel deLabel2;
        private nControls.deComboBox deComboBox1;
        private nControls.deLabel deLabel1;
        private nControls.deButton cmdSearch;
        private System.Windows.Forms.DataGridView grdStatus;
        private nControls.deButton deButton20;
        private nControls.deButton deButton21;
        private System.Windows.Forms.SaveFileDialog sfdUAT;
    }
}