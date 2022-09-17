/*
 * Created by SharpDevelop.
 * User: RahulN
 * Date: 20/03/2009
 * Time: 3:36 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace wSelect
{
	partial class BoxDetails
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblCount = new System.Windows.Forms.Label();
            this.lblBox = new System.Windows.Forms.Label();
            this.lblBatch = new System.Windows.Forms.Label();
            this.lblProject = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lstImageDel = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lstImage = new System.Windows.Forms.ListBox();
            this.cmdNext = new System.Windows.Forms.Button();
            this.cmdPrevious = new System.Windows.Forms.Button();
            this.lstPolicy = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblImageSize = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbBox = new System.Windows.Forms.ComboBox();
            this.lblName = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbDesValue = new System.Windows.Forms.ComboBox();
            this.txtVol = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.txtPgTo = new System.Windows.Forms.TextBox();
            this.txtPgFrom = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblinfo = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblCount);
            this.groupBox1.Controls.Add(this.lblBox);
            this.groupBox1.Controls.Add(this.lblBatch);
            this.groupBox1.Controls.Add(this.lblProject);
            this.groupBox1.Location = new System.Drawing.Point(14, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(221, 56);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCount.Location = new System.Drawing.Point(140, 16);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(37, 13);
            this.lblCount.TabIndex = 5;
            this.lblCount.Text = "Deed";
            // 
            // lblBox
            // 
            this.lblBox.AutoSize = true;
            this.lblBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBox.Location = new System.Drawing.Point(143, 16);
            this.lblBox.Name = "lblBox";
            this.lblBox.Size = new System.Drawing.Size(28, 13);
            this.lblBox.TabIndex = 4;
            this.lblBox.Text = "Box";
            this.lblBox.Visible = false;
            this.lblBox.Click += new System.EventHandler(this.lblBox_Click);
            // 
            // lblBatch
            // 
            this.lblBatch.AutoSize = true;
            this.lblBatch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBatch.Location = new System.Drawing.Point(6, 33);
            this.lblBatch.Name = "lblBatch";
            this.lblBatch.Size = new System.Drawing.Size(40, 13);
            this.lblBatch.TabIndex = 3;
            this.lblBatch.Text = "Batch";
            // 
            // lblProject
            // 
            this.lblProject.AutoSize = true;
            this.lblProject.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProject.Location = new System.Drawing.Point(6, 16);
            this.lblProject.Name = "lblProject";
            this.lblProject.Size = new System.Drawing.Size(47, 13);
            this.lblProject.TabIndex = 2;
            this.lblProject.Text = "Project";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(11, 363);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "Deleted:";
            // 
            // lstImageDel
            // 
            this.lstImageDel.FormattingEnabled = true;
            this.lstImageDel.Location = new System.Drawing.Point(64, 364);
            this.lstImageDel.Name = "lstImageDel";
            this.lstImageDel.Size = new System.Drawing.Size(168, 43);
            this.lstImageDel.TabIndex = 21;
            this.lstImageDel.SelectedIndexChanged += new System.EventHandler(this.lstImageDel_SelectedIndexChanged);
            this.lstImageDel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LstImageDelKeyDown);
            this.lstImageDel.Click += new System.EventHandler(this.lstImageDel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(5, 165);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Image:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(8, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Deed:";
            // 
            // lstImage
            // 
            this.lstImage.Enabled = false;
            this.lstImage.FormattingEnabled = true;
            this.lstImage.HorizontalScrollbar = true;
            this.lstImage.Location = new System.Drawing.Point(11, 181);
            this.lstImage.Name = "lstImage";
            this.lstImage.Size = new System.Drawing.Size(221, 134);
            this.lstImage.TabIndex = 18;
            this.lstImage.SelectedIndexChanged += new System.EventHandler(this.LstImageSelectedIndexChanged);
            this.lstImage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LstImageKeyPress);
            this.lstImage.Click += new System.EventHandler(this.lstImage_Click);
            // 
            // cmdNext
            // 
            this.cmdNext.Location = new System.Drawing.Point(157, 321);
            this.cmdNext.Name = "cmdNext";
            this.cmdNext.Size = new System.Drawing.Size(75, 23);
            this.cmdNext.TabIndex = 19;
            this.cmdNext.Text = "Next >>";
            this.cmdNext.UseVisualStyleBackColor = true;
            this.cmdNext.Click += new System.EventHandler(this.CmdNextClick);
            this.cmdNext.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cmdNext_KeyUp);
            // 
            // cmdPrevious
            // 
            this.cmdPrevious.Location = new System.Drawing.Point(11, 321);
            this.cmdPrevious.Name = "cmdPrevious";
            this.cmdPrevious.Size = new System.Drawing.Size(75, 23);
            this.cmdPrevious.TabIndex = 20;
            this.cmdPrevious.Text = "<< Previous";
            this.cmdPrevious.UseVisualStyleBackColor = true;
            this.cmdPrevious.Click += new System.EventHandler(this.CmdPreviousClick);
            // 
            // lstPolicy
            // 
            this.lstPolicy.Enabled = false;
            this.lstPolicy.FormattingEnabled = true;
            this.lstPolicy.Location = new System.Drawing.Point(49, 92);
            this.lstPolicy.Name = "lstPolicy";
            this.lstPolicy.Size = new System.Drawing.Size(186, 43);
            this.lstPolicy.TabIndex = 17;
            this.lstPolicy.SelectedIndexChanged += new System.EventHandler(this.LstPolicySelectedIndexChanged);
            this.lstPolicy.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LstImageKeyPress);
            this.lstPolicy.SelectedValueChanged += new System.EventHandler(this.lstPolicy_SelectedValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(11, 347);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "Image Size:";
            // 
            // lblImageSize
            // 
            this.lblImageSize.AutoSize = true;
            this.lblImageSize.Location = new System.Drawing.Point(79, 347);
            this.lblImageSize.Name = "lblImageSize";
            this.lblImageSize.Size = new System.Drawing.Size(0, 13);
            this.lblImageSize.TabIndex = 26;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Blue;
            this.label5.Location = new System.Drawing.Point(8, 119);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 27;
            this.label5.Text = "Box:";
            this.label5.Visible = false;
            // 
            // cmbBox
            // 
            this.cmbBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBox.FormattingEnabled = true;
            this.cmbBox.Location = new System.Drawing.Point(49, 116);
            this.cmbBox.Name = "cmbBox";
            this.cmbBox.Size = new System.Drawing.Size(112, 21);
            this.cmbBox.TabIndex = 28;
            this.cmbBox.Visible = false;
            this.cmbBox.SelectedIndexChanged += new System.EventHandler(this.cmbBox_SelectedIndexChanged);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.ForeColor = System.Drawing.Color.Blue;
            this.lblName.Location = new System.Drawing.Point(114, 151);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(0, 13);
            this.lblName.TabIndex = 29;
            this.lblName.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Blue;
            this.label6.Location = new System.Drawing.Point(11, 401);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 13);
            this.label6.TabIndex = 30;
            this.label6.Text = "Despeckle Value:";
            this.label6.Visible = false;
            // 
            // cmbDesValue
            // 
            this.cmbDesValue.FormattingEnabled = true;
            this.cmbDesValue.Items.AddRange(new object[] {
            "5",
            "10",
            "15",
            "20",
            "25",
            "30",
            "35",
            "40",
            "45",
            "50"});
            this.cmbDesValue.Location = new System.Drawing.Point(108, 398);
            this.cmbDesValue.Name = "cmbDesValue";
            this.cmbDesValue.Size = new System.Drawing.Size(66, 21);
            this.cmbDesValue.TabIndex = 31;
            this.cmbDesValue.Visible = false;
            this.cmbDesValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbDesValue_KeyPress);
            // 
            // txtVol
            // 
            this.txtVol.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVol.Location = new System.Drawing.Point(49, 142);
            this.txtVol.Name = "txtVol";
            this.txtVol.ReadOnly = true;
            this.txtVol.Size = new System.Drawing.Size(37, 21);
            this.txtVol.TabIndex = 37;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label30.Location = new System.Drawing.Point(11, 148);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(22, 13);
            this.label30.TabIndex = 36;
            this.label30.Text = "Vol";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label29.Location = new System.Drawing.Point(92, 143);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(37, 13);
            this.label29.TabIndex = 35;
            this.label29.Text = "Pages";
            // 
            // txtPgTo
            // 
            this.txtPgTo.AcceptsTab = true;
            this.txtPgTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPgTo.Location = new System.Drawing.Point(198, 143);
            this.txtPgTo.Name = "txtPgTo";
            this.txtPgTo.ReadOnly = true;
            this.txtPgTo.Size = new System.Drawing.Size(37, 21);
            this.txtPgTo.TabIndex = 33;
            // 
            // txtPgFrom
            // 
            this.txtPgFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPgFrom.Location = new System.Drawing.Point(135, 143);
            this.txtPgFrom.Name = "txtPgFrom";
            this.txtPgFrom.ReadOnly = true;
            this.txtPgFrom.Size = new System.Drawing.Size(37, 21);
            this.txtPgFrom.TabIndex = 32;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label7.Location = new System.Drawing.Point(178, 143);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 13);
            this.label7.TabIndex = 38;
            this.label7.Text = "_";
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.ForeColor = System.Drawing.Color.Blue;
            this.lblSearch.Location = new System.Drawing.Point(8, 62);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(44, 13);
            this.lblSearch.TabIndex = 39;
            this.lblSearch.Text = "Search:";
            this.lblSearch.Visible = false;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(49, 62);
            this.txtSearch.MaxLength = 5;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(143, 20);
            this.txtSearch.TabIndex = 40;
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            this.txtSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyUp);
            this.txtSearch.Enter += new System.EventHandler(this.txtSearch_Enter);
            // 
            // lblinfo
            // 
            this.lblinfo.AutoSize = true;
            this.lblinfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblinfo.ForeColor = System.Drawing.Color.Black;
            this.lblinfo.Location = new System.Drawing.Point(12, 410);
            this.lblinfo.Name = "lblinfo";
            this.lblinfo.Size = new System.Drawing.Size(0, 13);
            this.lblinfo.TabIndex = 41;
            // 
            // BoxDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblinfo);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtVol);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.txtPgTo);
            this.Controls.Add(this.txtPgFrom);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstPolicy);
            this.Controls.Add(this.lstImageDel);
            this.Controls.Add(this.cmbDesValue);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.cmbBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblImageSize);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lstImage);
            this.Controls.Add(this.cmdNext);
            this.Controls.Add(this.cmdPrevious);
            this.Controls.Add(this.groupBox1);
            this.Name = "BoxDetails";
            this.Size = new System.Drawing.Size(250, 429);
            this.Load += new System.EventHandler(this.BoxDetailsLoad);
            this.ParentChanged += new System.EventHandler(this.BoxDetailsParentChanged);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BoxDetailsMouseDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		private System.Windows.Forms.ComboBox cmbBox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ListBox lstImageDel;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox lstImage;
		private System.Windows.Forms.Label lblBox;
		private System.Windows.Forms.Button cmdNext;
		private System.Windows.Forms.Button cmdPrevious;
		private System.Windows.Forms.Label lblBatch;
		private System.Windows.Forms.Label lblProject;
		private System.Windows.Forms.ListBox lstPolicy;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblImageSize;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbDesValue;
        private System.Windows.Forms.TextBox txtVol;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox txtPgTo;
        private System.Windows.Forms.TextBox txtPgFrom;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblinfo;
	}
}
