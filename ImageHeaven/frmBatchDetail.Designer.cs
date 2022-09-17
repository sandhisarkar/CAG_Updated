
namespace ImageHeaven
{
    partial class frmBatchDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBatchDetail));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.deButtonCancel = new nControls.deButton();
            this.deButtonSave = new nControls.deButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.deTextBox3 = new nControls.deTextBox();
            this.deTextBox4 = new nControls.deTextBox();
            this.deLabel1 = new nControls.deLabel();
            this.deComboBox1 = new nControls.deComboBox();
            this.deLabel2 = new nControls.deLabel();
            this.deLabel3 = new nControls.deLabel();
            this.deTextBox2 = new nControls.deTextBox();
            this.deLabel4 = new nControls.deLabel();
            this.deTextBox1 = new nControls.deTextBox();
            this.deLabel5 = new nControls.deLabel();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(405, 302);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Batch Details : ";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 235);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(399, 64);
            this.panel2.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.deButtonCancel);
            this.groupBox2.Controls.Add(this.deButtonSave);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(399, 64);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // deButtonCancel
            // 
            this.deButtonCancel.BackColor = System.Drawing.Color.White;
            this.deButtonCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("deButtonCancel.BackgroundImage")));
            this.deButtonCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.deButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.deButtonCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.deButtonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deButtonCancel.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deButtonCancel.Location = new System.Drawing.Point(300, 18);
            this.deButtonCancel.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.deButtonCancel.Name = "deButtonCancel";
            this.deButtonCancel.Size = new System.Drawing.Size(87, 35);
            this.deButtonCancel.TabIndex = 84;
            this.deButtonCancel.Text = "A&bort";
            this.deButtonCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.deButtonCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.deButtonCancel.UseCompatibleTextRendering = true;
            this.deButtonCancel.UseVisualStyleBackColor = false;
            this.deButtonCancel.Click += new System.EventHandler(this.deButtonCancel_Click);
            // 
            // deButtonSave
            // 
            this.deButtonSave.BackColor = System.Drawing.Color.White;
            this.deButtonSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("deButtonSave.BackgroundImage")));
            this.deButtonSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.deButtonSave.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.deButtonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deButtonSave.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deButtonSave.Location = new System.Drawing.Point(166, 18);
            this.deButtonSave.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.deButtonSave.Name = "deButtonSave";
            this.deButtonSave.Size = new System.Drawing.Size(90, 35);
            this.deButtonSave.TabIndex = 83;
            this.deButtonSave.Text = "&Save";
            this.deButtonSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.deButtonSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.deButtonSave.UseCompatibleTextRendering = true;
            this.deButtonSave.UseVisualStyleBackColor = false;
            this.deButtonSave.Click += new System.EventHandler(this.deButtonSave_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 21);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(399, 214);
            this.panel1.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.deTextBox3);
            this.groupBox3.Controls.Add(this.deTextBox4);
            this.groupBox3.Controls.Add(this.deLabel1);
            this.groupBox3.Controls.Add(this.deComboBox1);
            this.groupBox3.Controls.Add(this.deLabel2);
            this.groupBox3.Controls.Add(this.deLabel3);
            this.groupBox3.Controls.Add(this.deTextBox2);
            this.groupBox3.Controls.Add(this.deLabel4);
            this.groupBox3.Controls.Add(this.deTextBox1);
            this.groupBox3.Controls.Add(this.deLabel5);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(399, 214);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            // 
            // deTextBox3
            // 
            this.deTextBox3.BackColor = System.Drawing.Color.White;
            this.deTextBox3.Enabled = false;
            this.deTextBox3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deTextBox3.ForeColor = System.Drawing.Color.Black;
            this.deTextBox3.Location = new System.Drawing.Point(146, 97);
            this.deTextBox3.Mandatory = true;
            this.deTextBox3.Name = "deTextBox3";
            this.deTextBox3.Size = new System.Drawing.Size(222, 23);
            this.deTextBox3.TabIndex = 7;
            // 
            // deTextBox4
            // 
            this.deTextBox4.BackColor = System.Drawing.Color.White;
            this.deTextBox4.Enabled = false;
            this.deTextBox4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deTextBox4.ForeColor = System.Drawing.Color.Black;
            this.deTextBox4.Location = new System.Drawing.Point(146, 139);
            this.deTextBox4.Mandatory = true;
            this.deTextBox4.Name = "deTextBox4";
            this.deTextBox4.Size = new System.Drawing.Size(222, 23);
            this.deTextBox4.TabIndex = 8;
            // 
            // deLabel1
            // 
            this.deLabel1.AutoSize = true;
            this.deLabel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deLabel1.Location = new System.Drawing.Point(41, 23);
            this.deLabel1.Name = "deLabel1";
            this.deLabel1.Size = new System.Drawing.Size(90, 15);
            this.deLabel1.TabIndex = 0;
            this.deLabel1.Text = "Batch Number :";
            // 
            // deComboBox1
            // 
            this.deComboBox1.BackColor = System.Drawing.Color.White;
            this.deComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.deComboBox1.ForeColor = System.Drawing.Color.Black;
            this.deComboBox1.FormattingEnabled = true;
            this.deComboBox1.Location = new System.Drawing.Point(146, 177);
            this.deComboBox1.Mandatory = true;
            this.deComboBox1.Name = "deComboBox1";
            this.deComboBox1.Size = new System.Drawing.Size(222, 25);
            this.deComboBox1.TabIndex = 9;
            // 
            // deLabel2
            // 
            this.deLabel2.AutoSize = true;
            this.deLabel2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deLabel2.Location = new System.Drawing.Point(57, 63);
            this.deLabel2.Name = "deLabel2";
            this.deLabel2.Size = new System.Drawing.Size(74, 15);
            this.deLabel2.TabIndex = 1;
            this.deLabel2.Text = "Batch Code :";
            // 
            // deLabel3
            // 
            this.deLabel3.AutoSize = true;
            this.deLabel3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deLabel3.Location = new System.Drawing.Point(46, 100);
            this.deLabel3.Name = "deLabel3";
            this.deLabel3.Size = new System.Drawing.Size(85, 15);
            this.deLabel3.TabIndex = 2;
            this.deLabel3.Text = "Creation Date :";
            // 
            // deTextBox2
            // 
            this.deTextBox2.BackColor = System.Drawing.Color.White;
            this.deTextBox2.Enabled = false;
            this.deTextBox2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deTextBox2.ForeColor = System.Drawing.Color.Black;
            this.deTextBox2.Location = new System.Drawing.Point(146, 59);
            this.deTextBox2.Mandatory = true;
            this.deTextBox2.Name = "deTextBox2";
            this.deTextBox2.Size = new System.Drawing.Size(222, 23);
            this.deTextBox2.TabIndex = 6;
            // 
            // deLabel4
            // 
            this.deLabel4.AutoSize = true;
            this.deLabel4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deLabel4.Location = new System.Drawing.Point(20, 143);
            this.deLabel4.Name = "deLabel4";
            this.deLabel4.Size = new System.Drawing.Size(111, 15);
            this.deLabel4.TabIndex = 3;
            this.deLabel4.Text = "Department Name :";
            this.deLabel4.Click += new System.EventHandler(this.deLabel4_Click);
            // 
            // deTextBox1
            // 
            this.deTextBox1.BackColor = System.Drawing.Color.White;
            this.deTextBox1.Enabled = false;
            this.deTextBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deTextBox1.ForeColor = System.Drawing.Color.Black;
            this.deTextBox1.Location = new System.Drawing.Point(146, 18);
            this.deTextBox1.Mandatory = true;
            this.deTextBox1.Name = "deTextBox1";
            this.deTextBox1.Size = new System.Drawing.Size(222, 23);
            this.deTextBox1.TabIndex = 5;
            // 
            // deLabel5
            // 
            this.deLabel5.AutoSize = true;
            this.deLabel5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deLabel5.Location = new System.Drawing.Point(46, 182);
            this.deLabel5.Name = "deLabel5";
            this.deLabel5.Size = new System.Drawing.Size(86, 15);
            this.deLabel5.TabIndex = 4;
            this.deLabel5.Text = "Sub-Category :";
            // 
            // frmBatchDetail
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(405, 302);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBatchDetail";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Batch Details ";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmBatchDetail_KeyUp);
            this.groupBox1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private nControls.deLabel deLabel1;
        private nControls.deTextBox deTextBox3;
        private nControls.deTextBox deTextBox2;
        private nControls.deTextBox deTextBox1;
        private nControls.deLabel deLabel5;
        private nControls.deLabel deLabel4;
        private nControls.deLabel deLabel3;
        private nControls.deLabel deLabel2;
        private nControls.deComboBox deComboBox1;
        private nControls.deTextBox deTextBox4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private nControls.deButton deButtonCancel;
        private nControls.deButton deButtonSave;
    }
}