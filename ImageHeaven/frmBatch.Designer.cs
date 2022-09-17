
namespace ImageHeaven
{
    partial class frmBatch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBatch));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbProject = new nControls.deComboBox();
            this.deLabel1 = new nControls.deLabel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.deComboBox3 = new nControls.deComboBox();
            this.deLabel3 = new nControls.deLabel();
            this.deComboBox2 = new nControls.deComboBox();
            this.deLabel5 = new nControls.deLabel();
            this.deComboBox1 = new nControls.deComboBox();
            this.deLabel2 = new nControls.deLabel();
            this.deButton1 = new nControls.deButton();
            this.txtCreateDate = new nControls.deTextBox();
            this.deLabel4 = new nControls.deLabel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBox4 = new nControls.deTextBox();
            this.textBox3 = new nControls.deTextBox();
            this.deLabel6 = new nControls.deLabel();
            this.deLabel7 = new nControls.deLabel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.button2 = new nControls.deButton();
            this.button3 = new nControls.deButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbProject);
            this.groupBox2.Controls.Add(this.deLabel1);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(2, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(695, 66);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Select Project";
            // 
            // cmbProject
            // 
            this.cmbProject.BackColor = System.Drawing.Color.White;
            this.cmbProject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProject.ForeColor = System.Drawing.Color.Black;
            this.cmbProject.FormattingEnabled = true;
            this.cmbProject.Location = new System.Drawing.Point(235, 22);
            this.cmbProject.Mandatory = true;
            this.cmbProject.Name = "cmbProject";
            this.cmbProject.Size = new System.Drawing.Size(245, 24);
            this.cmbProject.TabIndex = 1;
            this.cmbProject.Leave += new System.EventHandler(this.cmbProject_Leave);
            this.cmbProject.MouseLeave += new System.EventHandler(this.cmbProject_MouseLeave);
            // 
            // deLabel1
            // 
            this.deLabel1.AutoSize = true;
            this.deLabel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deLabel1.Location = new System.Drawing.Point(140, 27);
            this.deLabel1.Name = "deLabel1";
            this.deLabel1.Size = new System.Drawing.Size(85, 15);
            this.deLabel1.TabIndex = 0;
            this.deLabel1.Text = "Project Name :";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.deComboBox3);
            this.groupBox3.Controls.Add(this.deLabel3);
            this.groupBox3.Controls.Add(this.deComboBox2);
            this.groupBox3.Controls.Add(this.deLabel5);
            this.groupBox3.Controls.Add(this.deComboBox1);
            this.groupBox3.Controls.Add(this.deLabel2);
            this.groupBox3.Controls.Add(this.deButton1);
            this.groupBox3.Controls.Add(this.txtCreateDate);
            this.groupBox3.Controls.Add(this.deLabel4);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(2, 70);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(695, 138);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Batch Details";
            // 
            // deComboBox3
            // 
            this.deComboBox3.BackColor = System.Drawing.Color.White;
            this.deComboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.deComboBox3.ForeColor = System.Drawing.Color.Black;
            this.deComboBox3.FormattingEnabled = true;
            this.deComboBox3.Items.AddRange(new object[] {
            "PPO",
            "GPO",
            "CPO",
            "ITEM",
            "OTHERS"});
            this.deComboBox3.Location = new System.Drawing.Point(488, 60);
            this.deComboBox3.Mandatory = true;
            this.deComboBox3.Name = "deComboBox3";
            this.deComboBox3.Size = new System.Drawing.Size(180, 24);
            this.deComboBox3.TabIndex = 5;
            // 
            // deLabel3
            // 
            this.deLabel3.AutoSize = true;
            this.deLabel3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deLabel3.Location = new System.Drawing.Point(311, 64);
            this.deLabel3.Name = "deLabel3";
            this.deLabel3.Size = new System.Drawing.Size(172, 15);
            this.deLabel3.TabIndex = 13;
            this.deLabel3.Text = "PPO/GPO/CPO/ITEM/OTHERS :";
            // 
            // deComboBox2
            // 
            this.deComboBox2.BackColor = System.Drawing.Color.White;
            this.deComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.deComboBox2.ForeColor = System.Drawing.Color.Black;
            this.deComboBox2.FormattingEnabled = true;
            this.deComboBox2.Location = new System.Drawing.Point(443, 20);
            this.deComboBox2.Mandatory = true;
            this.deComboBox2.Name = "deComboBox2";
            this.deComboBox2.Size = new System.Drawing.Size(220, 24);
            this.deComboBox2.TabIndex = 3;
            this.deComboBox2.SelectedIndexChanged += new System.EventHandler(this.deComboBox2_SelectedIndexChanged);
            // 
            // deLabel5
            // 
            this.deLabel5.AutoSize = true;
            this.deLabel5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deLabel5.Location = new System.Drawing.Point(351, 25);
            this.deLabel5.Name = "deLabel5";
            this.deLabel5.Size = new System.Drawing.Size(86, 15);
            this.deLabel5.TabIndex = 12;
            this.deLabel5.Text = "Sub-Category :";
            // 
            // deComboBox1
            // 
            this.deComboBox1.BackColor = System.Drawing.Color.White;
            this.deComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.deComboBox1.ForeColor = System.Drawing.Color.Black;
            this.deComboBox1.FormattingEnabled = true;
            this.deComboBox1.Location = new System.Drawing.Point(130, 20);
            this.deComboBox1.Mandatory = true;
            this.deComboBox1.Name = "deComboBox1";
            this.deComboBox1.Size = new System.Drawing.Size(196, 24);
            this.deComboBox1.TabIndex = 2;
            this.deComboBox1.SelectedIndexChanged += new System.EventHandler(this.deComboBox1_SelectedIndexChanged);
            this.deComboBox1.Leave += new System.EventHandler(this.deComboBox1_Leave);
            // 
            // deLabel2
            // 
            this.deLabel2.AutoSize = true;
            this.deLabel2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deLabel2.Location = new System.Drawing.Point(48, 24);
            this.deLabel2.Name = "deLabel2";
            this.deLabel2.Size = new System.Drawing.Size(76, 15);
            this.deLabel2.TabIndex = 11;
            this.deLabel2.Text = "Department :";
            // 
            // deButton1
            // 
            this.deButton1.AutoSize = true;
            this.deButton1.BackColor = System.Drawing.SystemColors.Control;
            this.deButton1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.deButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deButton1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deButton1.Image = ((System.Drawing.Image)(resources.GetObject("deButton1.Image")));
            this.deButton1.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.deButton1.Location = new System.Drawing.Point(246, 94);
            this.deButton1.Name = "deButton1";
            this.deButton1.Size = new System.Drawing.Size(224, 41);
            this.deButton1.TabIndex = 10;
            this.deButton1.Text = "         &Generate Batch Code";
            this.deButton1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.deButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.deButton1.UseCompatibleTextRendering = true;
            this.deButton1.UseVisualStyleBackColor = false;
            this.deButton1.Click += new System.EventHandler(this.deButton1_Click);
            // 
            // txtCreateDate
            // 
            this.txtCreateDate.BackColor = System.Drawing.Color.White;
            this.txtCreateDate.Enabled = false;
            this.txtCreateDate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCreateDate.ForeColor = System.Drawing.Color.Black;
            this.txtCreateDate.Location = new System.Drawing.Point(130, 60);
            this.txtCreateDate.Mandatory = true;
            this.txtCreateDate.Name = "txtCreateDate";
            this.txtCreateDate.Size = new System.Drawing.Size(170, 23);
            this.txtCreateDate.TabIndex = 7;
            // 
            // deLabel4
            // 
            this.deLabel4.AutoSize = true;
            this.deLabel4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deLabel4.Location = new System.Drawing.Point(37, 64);
            this.deLabel4.Name = "deLabel4";
            this.deLabel4.Size = new System.Drawing.Size(85, 15);
            this.deLabel4.TabIndex = 6;
            this.deLabel4.Text = "Creation Date :";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.textBox4);
            this.groupBox4.Controls.Add(this.textBox3);
            this.groupBox4.Controls.Add(this.deLabel6);
            this.groupBox4.Controls.Add(this.deLabel7);
            this.groupBox4.Enabled = false;
            this.groupBox4.Location = new System.Drawing.Point(2, 206);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(695, 53);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.Color.White;
            this.textBox4.Enabled = false;
            this.textBox4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox4.ForeColor = System.Drawing.Color.Black;
            this.textBox4.Location = new System.Drawing.Point(478, 18);
            this.textBox4.Mandatory = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(170, 23);
            this.textBox4.TabIndex = 14;
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.Color.White;
            this.textBox3.Enabled = false;
            this.textBox3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox3.ForeColor = System.Drawing.Color.Black;
            this.textBox3.Location = new System.Drawing.Point(139, 18);
            this.textBox3.Mandatory = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(170, 23);
            this.textBox3.TabIndex = 12;
            // 
            // deLabel6
            // 
            this.deLabel6.AutoSize = true;
            this.deLabel6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deLabel6.Location = new System.Drawing.Point(376, 22);
            this.deLabel6.Name = "deLabel6";
            this.deLabel6.Size = new System.Drawing.Size(78, 15);
            this.deLabel6.TabIndex = 11;
            this.deLabel6.Text = "Batch Name :";
            // 
            // deLabel7
            // 
            this.deLabel7.AutoSize = true;
            this.deLabel7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deLabel7.Location = new System.Drawing.Point(46, 21);
            this.deLabel7.Name = "deLabel7";
            this.deLabel7.Size = new System.Drawing.Size(74, 15);
            this.deLabel7.TabIndex = 10;
            this.deLabel7.Text = "Batch Code :";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.button2);
            this.panel4.Controls.Add(this.button3);
            this.panel4.Controls.Add(this.groupBox4);
            this.panel4.Controls.Add(this.groupBox3);
            this.panel4.Controls.Add(this.groupBox2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(705, 318);
            this.panel4.TabIndex = 4;
            // 
            // button2
            // 
            this.button2.AutoSize = true;
            this.button2.BackColor = System.Drawing.SystemColors.Control;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button2.Location = new System.Drawing.Point(443, 266);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(104, 44);
            this.button2.TabIndex = 15;
            this.button2.Text = "      &Save";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button2.UseCompatibleTextRendering = true;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.AutoSize = true;
            this.button3.BackColor = System.Drawing.SystemColors.Control;
            this.button3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button3.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(573, 265);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(118, 45);
            this.button3.TabIndex = 16;
            this.button3.Text = "     C&lose";
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button3.UseCompatibleTextRendering = true;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 318);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(705, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // frmBatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(705, 340);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBatch";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Batch Creation";
            this.Load += new System.EventHandler(this.frmBatch_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private nControls.deLabel deLabel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private nControls.deComboBox cmbProject;
        private System.Windows.Forms.GroupBox groupBox3;
        private nControls.deButton deButton1;
        private nControls.deTextBox txtCreateDate;
        private nControls.deLabel deLabel4;
        private nControls.deLabel deLabel6;
        private nControls.deLabel deLabel7;
        private nControls.deTextBox textBox4;
        private nControls.deButton button2;
        private nControls.deButton button3;
        private System.Windows.Forms.GroupBox groupBox4;
        private nControls.deTextBox textBox3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private nControls.deComboBox deComboBox1;
        private nControls.deLabel deLabel2;
        private nControls.deComboBox deComboBox2;
        private nControls.deLabel deLabel5;
        private nControls.deLabel deLabel3;
        private nControls.deComboBox deComboBox3;
    }
}