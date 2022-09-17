
namespace ImageHeaven
{
    partial class frmJobDistribution
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmJobDistribution));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            this.sfdUAT = new System.Windows.Forms.SaveFileDialog();
            this.deButton20 = new nControls.deButton();
            this.deButton21 = new nControls.deButton();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grdStatus = new System.Windows.Forms.DataGridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lbl = new System.Windows.Forms.Label();
            this.lblTotalImageScanned = new System.Windows.Forms.Label();
            this.cmdSearch = new nControls.deButton();
            this.deButton1 = new nControls.deButton();
            this.textBox2 = new nControls.deTextBox();
            this.deLabel3 = new nControls.deLabel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.deButton2 = new nControls.deButton();
            this.deTextBox1 = new nControls.deTextBox();
            this.deLabel2 = new nControls.deLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.deComboBox1 = new nControls.deComboBox();
            this.deLabel1 = new nControls.deLabel();
            this.dtGrdVol = new nControls.deDataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.trv = new System.Windows.Forms.TreeView();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.groupBox6.SuspendLayout();
            this.panel6.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdStatus)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGrdVol)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel8.SuspendLayout();
            this.SuspendLayout();
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
            this.deButton20.Location = new System.Drawing.Point(9, 15);
            this.deButton20.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.deButton20.Name = "deButton20";
            this.deButton20.Size = new System.Drawing.Size(157, 30);
            this.deButton20.TabIndex = 8;
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
            this.deButton21.Location = new System.Drawing.Point(182, 15);
            this.deButton21.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.deButton21.Name = "deButton21";
            this.deButton21.Size = new System.Drawing.Size(87, 30);
            this.deButton21.TabIndex = 9;
            this.deButton21.Text = "A&bort";
            this.deButton21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.deButton21.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.deButton21.UseCompatibleTextRendering = true;
            this.deButton21.UseVisualStyleBackColor = false;
            this.deButton21.Click += new System.EventHandler(this.deButton21_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.deButton20);
            this.groupBox6.Controls.Add(this.deButton21);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox6.Location = new System.Drawing.Point(337, 0);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(276, 53);
            this.groupBox6.TabIndex = 11;
            this.groupBox6.TabStop = false;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.groupBox6);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(0, 434);
            this.panel6.Margin = new System.Windows.Forms.Padding(2);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(613, 53);
            this.panel6.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.grdStatus);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(613, 403);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "File Status :";
            // 
            // grdStatus
            // 
            this.grdStatus.AllowUserToAddRows = false;
            this.grdStatus.AllowUserToDeleteRows = false;
            this.grdStatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdStatus.Location = new System.Drawing.Point(3, 19);
            this.grdStatus.MultiSelect = false;
            this.grdStatus.Name = "grdStatus";
            this.grdStatus.ReadOnly = true;
            this.grdStatus.RowHeadersWidth = 62;
            this.grdStatus.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdStatus.Size = new System.Drawing.Size(607, 381);
            this.grdStatus.TabIndex = 6;
            this.grdStatus.DoubleClick += new System.EventHandler(this.GrdStatusDoubleClick);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel8);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 31);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(613, 403);
            this.panel4.TabIndex = 8;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel6);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(300, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(613, 487);
            this.panel2.TabIndex = 14;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lbl);
            this.panel3.Controls.Add(this.lblTotalImageScanned);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(613, 31);
            this.panel3.TabIndex = 7;
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.ForeColor = System.Drawing.Color.Navy;
            this.lbl.Location = new System.Drawing.Point(7, 9);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(112, 13);
            this.lbl.TabIndex = 8;
            this.lbl.Text = "Total Image Scanned:";
            // 
            // lblTotalImageScanned
            // 
            this.lblTotalImageScanned.AutoSize = true;
            this.lblTotalImageScanned.ForeColor = System.Drawing.Color.Navy;
            this.lblTotalImageScanned.Location = new System.Drawing.Point(125, 9);
            this.lblTotalImageScanned.Name = "lblTotalImageScanned";
            this.lblTotalImageScanned.Size = new System.Drawing.Size(13, 13);
            this.lblTotalImageScanned.TabIndex = 8;
            this.lblTotalImageScanned.Text = "0";
            // 
            // cmdSearch
            // 
            this.cmdSearch.BackColor = System.Drawing.SystemColors.Control;
            this.cmdSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdSearch.BackgroundImage")));
            this.cmdSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdSearch.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cmdSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSearch.Location = new System.Drawing.Point(80, 51);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(153, 31);
            this.cmdSearch.TabIndex = 11;
            this.cmdSearch.Text = "&Search for Batch";
            this.cmdSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdSearch.UseCompatibleTextRendering = true;
            this.cmdSearch.UseVisualStyleBackColor = false;
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // deButton1
            // 
            this.deButton1.BackColor = System.Drawing.SystemColors.Control;
            this.deButton1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("deButton1.BackgroundImage")));
            this.deButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.deButton1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.deButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deButton1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deButton1.Location = new System.Drawing.Point(199, 12);
            this.deButton1.Name = "deButton1";
            this.deButton1.Size = new System.Drawing.Size(86, 31);
            this.deButton1.TabIndex = 18;
            this.deButton1.Text = "&Search";
            this.deButton1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.deButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.deButton1.UseCompatibleTextRendering = true;
            this.deButton1.UseVisualStyleBackColor = false;
            this.deButton1.Click += new System.EventHandler(this.deButton1_Click);
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.White;
            this.textBox2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.ForeColor = System.Drawing.Color.Black;
            this.textBox2.Location = new System.Drawing.Point(82, 15);
            this.textBox2.Mandatory = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(108, 23);
            this.textBox2.TabIndex = 17;
            // 
            // deLabel3
            // 
            this.deLabel3.AutoSize = true;
            this.deLabel3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deLabel3.Location = new System.Drawing.Point(0, 19);
            this.deLabel3.Name = "deLabel3";
            this.deLabel3.Size = new System.Drawing.Size(71, 15);
            this.deLabel3.TabIndex = 19;
            this.deLabel3.Text = "Batch Wise :";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.deButton1);
            this.groupBox4.Controls.Add(this.textBox2);
            this.groupBox4.Controls.Add(this.deLabel3);
            this.groupBox4.Location = new System.Drawing.Point(3, 88);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(294, 45);
            this.groupBox4.TabIndex = 12;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Search :";
            // 
            // deButton2
            // 
            this.deButton2.BackColor = System.Drawing.SystemColors.Control;
            this.deButton2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("deButton2.BackgroundImage")));
            this.deButton2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.deButton2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.deButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deButton2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deButton2.Location = new System.Drawing.Point(199, 12);
            this.deButton2.Name = "deButton2";
            this.deButton2.Size = new System.Drawing.Size(86, 31);
            this.deButton2.TabIndex = 18;
            this.deButton2.Text = "&Search";
            this.deButton2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.deButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.deButton2.UseCompatibleTextRendering = true;
            this.deButton2.UseVisualStyleBackColor = false;
            this.deButton2.Click += new System.EventHandler(this.deButton2_Click);
            // 
            // deTextBox1
            // 
            this.deTextBox1.BackColor = System.Drawing.Color.White;
            this.deTextBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deTextBox1.ForeColor = System.Drawing.Color.Black;
            this.deTextBox1.Location = new System.Drawing.Point(82, 15);
            this.deTextBox1.Mandatory = true;
            this.deTextBox1.Name = "deTextBox1";
            this.deTextBox1.Size = new System.Drawing.Size(108, 23);
            this.deTextBox1.TabIndex = 17;
            // 
            // deLabel2
            // 
            this.deLabel2.AutoSize = true;
            this.deLabel2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deLabel2.Location = new System.Drawing.Point(19, 19);
            this.deLabel2.Name = "deLabel2";
            this.deLabel2.Size = new System.Drawing.Size(59, 15);
            this.deLabel2.TabIndex = 19;
            this.deLabel2.Text = "File Wise :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.cmdSearch);
            this.groupBox1.Controls.Add(this.deComboBox1);
            this.groupBox1.Controls.Add(this.deLabel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(300, 222);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Selection :";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.deButton2);
            this.groupBox5.Controls.Add(this.deTextBox1);
            this.groupBox5.Controls.Add(this.deLabel2);
            this.groupBox5.Location = new System.Drawing.Point(3, 137);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(294, 45);
            this.groupBox5.TabIndex = 13;
            this.groupBox5.TabStop = false;
            // 
            // deComboBox1
            // 
            this.deComboBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.deComboBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.deComboBox1.BackColor = System.Drawing.Color.White;
            this.deComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            this.deComboBox1.Location = new System.Drawing.Point(60, 20);
            this.deComboBox1.Mandatory = false;
            this.deComboBox1.Name = "deComboBox1";
            this.deComboBox1.Size = new System.Drawing.Size(217, 23);
            this.deComboBox1.TabIndex = 0;
            // 
            // deLabel1
            // 
            this.deLabel1.AutoSize = true;
            this.deLabel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deLabel1.Location = new System.Drawing.Point(15, 24);
            this.deLabel1.Name = "deLabel1";
            this.deLabel1.Size = new System.Drawing.Size(39, 15);
            this.deLabel1.TabIndex = 9;
            this.deLabel1.Text = "Filter :";
            // 
            // dtGrdVol
            // 
            this.dtGrdVol.AllowUserToAddRows = false;
            this.dtGrdVol.AllowUserToDeleteRows = false;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.Color.White;
            this.dtGrdVol.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle13;
            this.dtGrdVol.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dtGrdVol.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dtGrdVol.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            this.dtGrdVol.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dtGrdVol.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.ControlDark;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.ControlDark;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtGrdVol.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.dtGrdVol.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtGrdVol.DefaultCellStyle = dataGridViewCellStyle15;
            this.dtGrdVol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtGrdVol.Location = new System.Drawing.Point(3, 19);
            this.dtGrdVol.MultiSelect = false;
            this.dtGrdVol.Name = "dtGrdVol";
            this.dtGrdVol.ReadOnly = true;
            this.dtGrdVol.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtGrdVol.RowHeadersDefaultCellStyle = dataGridViewCellStyle16;
            this.dtGrdVol.RowHeadersVisible = false;
            this.dtGrdVol.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dtGrdVol.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtGrdVol.Size = new System.Drawing.Size(294, 243);
            this.dtGrdVol.StandardTab = true;
            this.dtGrdVol.TabIndex = 8;
            this.dtGrdVol.DoubleClick += new System.EventHandler(this.dtGrdVol_DoubleClick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dtGrdVol);
            this.groupBox3.Controls.Add(this.trv);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(300, 265);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Batch List :";
            // 
            // trv
            // 
            this.trv.CheckBoxes = true;
            this.trv.Location = new System.Drawing.Point(-199, 281);
            this.trv.Name = "trv";
            this.trv.Size = new System.Drawing.Size(182, 144);
            this.trv.TabIndex = 7;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.groupBox3);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 222);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(300, 265);
            this.panel5.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(7, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 0;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.label1);
            this.panel7.Location = new System.Drawing.Point(3, 186);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(294, 31);
            this.panel7.TabIndex = 14;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel7);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(300, 487);
            this.panel1.TabIndex = 13;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.groupBox2);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(613, 403);
            this.panel8.TabIndex = 1;
            // 
            // frmJobDistribution
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 487);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmJobDistribution";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Dashboard ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmJobDistribution_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmJobDistribution_KeyDown);
            this.groupBox6.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdStatus)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGrdVol)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SaveFileDialog sfdUAT;
        private nControls.deButton deButton20;
        private nControls.deButton deButton21;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView grdStatus;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.Label lblTotalImageScanned;
        private nControls.deButton cmdSearch;
        private nControls.deButton deButton1;
        private nControls.deTextBox textBox2;
        private nControls.deLabel deLabel3;
        private System.Windows.Forms.GroupBox groupBox4;
        private nControls.deButton deButton2;
        private nControls.deTextBox deTextBox1;
        private nControls.deLabel deLabel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox5;
        private nControls.deComboBox deComboBox1;
        private nControls.deLabel deLabel1;
        private nControls.deDataGridView dtGrdVol;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TreeView trv;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel8;
    }
}