
namespace ImageHeaven
{
    partial class frmEntrySummary
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEntrySummary));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmdSearch = new nControls.deButton();
            this.textBox2 = new nControls.deTextBox();
            this.deLabel3 = new nControls.deLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dtGrdVol = new nControls.deDataGridView();
            this.cmdnew = new nControls.deButton();
            this.cmdReset = new nControls.deButton();
            this.deLabel1 = new nControls.deLabel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGrdVol)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmdSearch);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.deLabel3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(578, 57);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search Criteria";
            // 
            // cmdSearch
            // 
            this.cmdSearch.BackColor = System.Drawing.SystemColors.Control;
            this.cmdSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdSearch.BackgroundImage")));
            this.cmdSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdSearch.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cmdSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSearch.Location = new System.Drawing.Point(466, 18);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(94, 31);
            this.cmdSearch.TabIndex = 3;
            this.cmdSearch.Text = "&Search";
            this.cmdSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdSearch.UseCompatibleTextRendering = true;
            this.cmdSearch.UseVisualStyleBackColor = false;
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.White;
            this.textBox2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.ForeColor = System.Drawing.Color.Black;
            this.textBox2.Location = new System.Drawing.Point(330, 23);
            this.textBox2.Mandatory = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(108, 23);
            this.textBox2.TabIndex = 2;
            // 
            // deLabel3
            // 
            this.deLabel3.AutoSize = true;
            this.deLabel3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deLabel3.Location = new System.Drawing.Point(257, 27);
            this.deLabel3.Name = "deLabel3";
            this.deLabel3.Size = new System.Drawing.Size(62, 15);
            this.deLabel3.TabIndex = 16;
            this.deLabel3.Text = "Batch No :";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dtGrdVol);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(3, 52);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(573, 438);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Entry Details ";
            // 
            // dtGrdVol
            // 
            this.dtGrdVol.AllowUserToAddRows = false;
            this.dtGrdVol.AllowUserToDeleteRows = false;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White;
            this.dtGrdVol.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dtGrdVol.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dtGrdVol.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dtGrdVol.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            this.dtGrdVol.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dtGrdVol.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.ControlDark;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlDark;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtGrdVol.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dtGrdVol.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtGrdVol.DefaultCellStyle = dataGridViewCellStyle7;
            this.dtGrdVol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtGrdVol.Location = new System.Drawing.Point(3, 25);
            this.dtGrdVol.MultiSelect = false;
            this.dtGrdVol.Name = "dtGrdVol";
            this.dtGrdVol.ReadOnly = true;
            this.dtGrdVol.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtGrdVol.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dtGrdVol.RowHeadersVisible = false;
            this.dtGrdVol.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dtGrdVol.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtGrdVol.Size = new System.Drawing.Size(567, 410);
            this.dtGrdVol.StandardTab = true;
            this.dtGrdVol.TabIndex = 4;
            this.dtGrdVol.DoubleClick += new System.EventHandler(this.dtGrdVol_DoubleClick);
            this.dtGrdVol.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dtGrdVol_KeyUp);
            // 
            // cmdnew
            // 
            this.cmdnew.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdnew.BackgroundImage")));
            this.cmdnew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdnew.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cmdnew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdnew.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdnew.Location = new System.Drawing.Point(456, 496);
            this.cmdnew.Name = "cmdnew";
            this.cmdnew.Size = new System.Drawing.Size(115, 33);
            this.cmdnew.TabIndex = 8;
            this.cmdnew.Text = "&New Entry";
            this.cmdnew.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdnew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdnew.UseCompatibleTextRendering = true;
            this.cmdnew.UseVisualStyleBackColor = true;
            this.cmdnew.Click += new System.EventHandler(this.cmdnew_Click);
            // 
            // cmdReset
            // 
            this.cmdReset.BackColor = System.Drawing.SystemColors.Control;
            this.cmdReset.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdReset.BackgroundImage")));
            this.cmdReset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdReset.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cmdReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdReset.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdReset.Location = new System.Drawing.Point(9, 496);
            this.cmdReset.Name = "cmdReset";
            this.cmdReset.Size = new System.Drawing.Size(96, 33);
            this.cmdReset.TabIndex = 7;
            this.cmdReset.Text = "&Reset";
            this.cmdReset.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdReset.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdReset.UseCompatibleTextRendering = true;
            this.cmdReset.UseVisualStyleBackColor = false;
            this.cmdReset.Click += new System.EventHandler(this.cmdReset_Click);
            // 
            // deLabel1
            // 
            this.deLabel1.AutoSize = true;
            this.deLabel1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deLabel1.Location = new System.Drawing.Point(122, 508);
            this.deLabel1.Name = "deLabel1";
            this.deLabel1.Size = new System.Drawing.Size(0, 15);
            this.deLabel1.TabIndex = 9;
            // 
            // frmEntrySummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 537);
            this.Controls.Add(this.deLabel1);
            this.Controls.Add(this.cmdnew);
            this.Controls.Add(this.cmdReset);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEntrySummary";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Entry Summary";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmEntrySummary_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmEntrySummary_KeyUp);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtGrdVol)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private nControls.deButton cmdSearch;
        private nControls.deTextBox textBox2;
        private nControls.deLabel deLabel3;
        private System.Windows.Forms.GroupBox groupBox2;
        private nControls.deDataGridView dtGrdVol;
        private nControls.deButton cmdnew;
        private nControls.deButton cmdReset;
        private nControls.deLabel deLabel1;
    }
}