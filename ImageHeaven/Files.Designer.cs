
namespace ImageHeaven
{
    partial class Files
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Files));
            this.deLabel1 = new nControls.deLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.deTextBox1 = new nControls.deTextBox();
            this.cmdSearch = new nControls.deButton();
            this.deLabel10 = new nControls.deLabel();
            this.deLabel3 = new nControls.deLabel();
            this.deLabel2 = new nControls.deLabel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.fileRemarks = new nControls.deLabel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lstDeeds = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.deLabel4 = new nControls.deLabel();
            this.deLabel5 = new nControls.deLabel();
            this.cmsDeeds = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteDeedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.cmsDeeds.SuspendLayout();
            this.SuspendLayout();
            // 
            // deLabel1
            // 
            this.deLabel1.AutoSize = true;
            this.deLabel1.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deLabel1.Location = new System.Drawing.Point(7, 9);
            this.deLabel1.Name = "deLabel1";
            this.deLabel1.Size = new System.Drawing.Size(50, 25);
            this.deLabel1.TabIndex = 1;
            this.deLabel1.Text = "Files";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.deTextBox1);
            this.groupBox2.Controls.Add(this.cmdSearch);
            this.groupBox2.Controls.Add(this.deLabel10);
            this.groupBox2.Controls.Add(this.deLabel3);
            this.groupBox2.Controls.Add(this.deLabel2);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(4, 42);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(605, 59);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "File Deatils :";
            // 
            // deTextBox1
            // 
            this.deTextBox1.BackColor = System.Drawing.Color.White;
            this.deTextBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deTextBox1.ForeColor = System.Drawing.Color.Black;
            this.deTextBox1.Location = new System.Drawing.Point(415, 22);
            this.deTextBox1.Mandatory = true;
            this.deTextBox1.Name = "deTextBox1";
            this.deTextBox1.Size = new System.Drawing.Size(99, 23);
            this.deTextBox1.TabIndex = 1;
            this.deTextBox1.Enter += new System.EventHandler(this.deTextBox1_Enter);
            this.deTextBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.deTextBox1_KeyUp);
            // 
            // cmdSearch
            // 
            this.cmdSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdSearch.BackgroundImage")));
            this.cmdSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdSearch.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cmdSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdSearch.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSearch.Location = new System.Drawing.Point(520, 17);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(81, 29);
            this.cmdSearch.TabIndex = 2;
            this.cmdSearch.Text = "&Search";
            this.cmdSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdSearch.UseCompatibleTextRendering = true;
            this.cmdSearch.UseVisualStyleBackColor = true;
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // deLabel10
            // 
            this.deLabel10.AutoSize = true;
            this.deLabel10.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deLabel10.Location = new System.Drawing.Point(364, 26);
            this.deLabel10.Name = "deLabel10";
            this.deLabel10.Size = new System.Drawing.Size(53, 15);
            this.deLabel10.TabIndex = 8;
            this.deLabel10.Text = "File No : ";
            // 
            // deLabel3
            // 
            this.deLabel3.AutoSize = true;
            this.deLabel3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deLabel3.Location = new System.Drawing.Point(210, 27);
            this.deLabel3.Name = "deLabel3";
            this.deLabel3.Size = new System.Drawing.Size(0, 15);
            this.deLabel3.TabIndex = 1;
            // 
            // deLabel2
            // 
            this.deLabel2.AutoSize = true;
            this.deLabel2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deLabel2.Location = new System.Drawing.Point(125, 27);
            this.deLabel2.Name = "deLabel2";
            this.deLabel2.Size = new System.Drawing.Size(78, 15);
            this.deLabel2.TabIndex = 0;
            this.deLabel2.Text = "Batch Name :";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.fileRemarks);
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(179, 100);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(428, 463);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = " File Summary";
            // 
            // fileRemarks
            // 
            this.fileRemarks.AutoSize = true;
            this.fileRemarks.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileRemarks.Location = new System.Drawing.Point(34, 42);
            this.fileRemarks.Name = "fileRemarks";
            this.fileRemarks.Size = new System.Drawing.Size(0, 25);
            this.fileRemarks.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lstDeeds);
            this.groupBox4.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(7, 101);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(165, 461);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "List of Files";
            // 
            // lstDeeds
            // 
            this.lstDeeds.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lstDeeds.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstDeeds.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstDeeds.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lstDeeds.FullRowSelect = true;
            this.lstDeeds.GridLines = true;
            this.lstDeeds.HideSelection = false;
            this.lstDeeds.Location = new System.Drawing.Point(3, 23);
            this.lstDeeds.MultiSelect = false;
            this.lstDeeds.Name = "lstDeeds";
            this.lstDeeds.Size = new System.Drawing.Size(159, 435);
            this.lstDeeds.TabIndex = 0;
            this.lstDeeds.UseCompatibleStateImageBehavior = false;
            this.lstDeeds.View = System.Windows.Forms.View.Details;
            this.lstDeeds.SelectedIndexChanged += new System.EventHandler(this.lstDeeds_SelectedIndexChanged);
            this.lstDeeds.DoubleClick += new System.EventHandler(this.lstDeeds_DoubleClick);
            this.lstDeeds.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lstDeeds_KeyUp);
            this.lstDeeds.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lstDeeds_MouseClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "File Number";
            this.columnHeader1.Width = 128;
            // 
            // deLabel4
            // 
            this.deLabel4.AutoSize = true;
            this.deLabel4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deLabel4.ForeColor = System.Drawing.SystemColors.Highlight;
            this.deLabel4.Location = new System.Drawing.Point(348, 9);
            this.deLabel4.Name = "deLabel4";
            this.deLabel4.Size = new System.Drawing.Size(0, 15);
            this.deLabel4.TabIndex = 8;
            // 
            // deLabel5
            // 
            this.deLabel5.AutoSize = true;
            this.deLabel5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deLabel5.ForeColor = System.Drawing.SystemColors.Highlight;
            this.deLabel5.Location = new System.Drawing.Point(348, 30);
            this.deLabel5.Name = "deLabel5";
            this.deLabel5.Size = new System.Drawing.Size(0, 15);
            this.deLabel5.TabIndex = 9;
            // 
            // cmsDeeds
            // 
            this.cmsDeeds.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteDeedToolStripMenuItem});
            this.cmsDeeds.Name = "cmsDeeds";
            this.cmsDeeds.Size = new System.Drawing.Size(129, 26);
            // 
            // deleteDeedToolStripMenuItem
            // 
            this.deleteDeedToolStripMenuItem.Name = "deleteDeedToolStripMenuItem";
            this.deleteDeedToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.deleteDeedToolStripMenuItem.Text = "&Delete File";
            this.deleteDeedToolStripMenuItem.Click += new System.EventHandler(this.deleteDeedToolStripMenuItem_Click);
            // 
            // Files
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 565);
            this.Controls.Add(this.deLabel5);
            this.Controls.Add(this.deLabel4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.deLabel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "Files";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Files";
            this.Load += new System.EventHandler(this.Files_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Files_KeyUp);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.cmsDeeds.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private nControls.deLabel deLabel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private nControls.deTextBox deTextBox1;
        private nControls.deButton cmdSearch;
        private nControls.deLabel deLabel10;
        private nControls.deLabel deLabel3;
        private nControls.deLabel deLabel2;
        private System.Windows.Forms.GroupBox groupBox3;
        private nControls.deLabel fileRemarks;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ListView lstDeeds;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private nControls.deLabel deLabel4;
        private nControls.deLabel deLabel5;
        private System.Windows.Forms.ContextMenuStrip cmsDeeds;
        private System.Windows.Forms.ToolStripMenuItem deleteDeedToolStripMenuItem;
    }
}