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
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.lblBox);
			this.groupBox1.Controls.Add(this.lblBatch);
			this.groupBox1.Controls.Add(this.lblProject);
			this.groupBox1.Location = new System.Drawing.Point(14, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(221, 56);
			this.groupBox1.TabIndex = 9;
			this.groupBox1.TabStop = false;
			// 
			// lblBox
			// 
			this.lblBox.AutoSize = true;
			this.lblBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblBox.Location = new System.Drawing.Point(158, 33);
			this.lblBox.Name = "lblBox";
			this.lblBox.Size = new System.Drawing.Size(28, 13);
			this.lblBox.TabIndex = 4;
			this.lblBox.Text = "Box";
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
			this.label3.Location = new System.Drawing.Point(11, 386);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(47, 13);
			this.label3.TabIndex = 24;
			this.label3.Text = "Deleted:";
			// 
			// lstImageDel
			// 
			this.lstImageDel.FormattingEnabled = true;
			this.lstImageDel.Location = new System.Drawing.Point(11, 402);
			this.lstImageDel.Name = "lstImageDel";
			this.lstImageDel.Size = new System.Drawing.Size(221, 43);
			this.lstImageDel.TabIndex = 23;
			this.lstImageDel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LstImageDelKeyDown);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.ForeColor = System.Drawing.Color.Blue;
			this.label2.Location = new System.Drawing.Point(11, 126);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(39, 13);
			this.label2.TabIndex = 22;
			this.label2.Text = "Image:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.ForeColor = System.Drawing.Color.Blue;
			this.label1.Location = new System.Drawing.Point(11, 64);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(38, 13);
			this.label1.TabIndex = 21;
			this.label1.Text = "Policy:";
			// 
			// lstImage
			// 
			this.lstImage.Enabled = false;
			this.lstImage.FormattingEnabled = true;
			this.lstImage.Location = new System.Drawing.Point(13, 142);
			this.lstImage.MultiColumn = true;
			this.lstImage.Name = "lstImage";
			this.lstImage.Size = new System.Drawing.Size(221, 212);
			this.lstImage.TabIndex = 20;
			this.lstImage.SelectedIndexChanged += new System.EventHandler(this.LstImageSelectedIndexChanged);
			this.lstImage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LstImageKeyPress);
			// 
			// cmdNext
			// 
			this.cmdNext.Location = new System.Drawing.Point(160, 360);
			this.cmdNext.Name = "cmdNext";
			this.cmdNext.Size = new System.Drawing.Size(75, 23);
			this.cmdNext.TabIndex = 18;
			this.cmdNext.Text = "Next >>";
			this.cmdNext.UseVisualStyleBackColor = true;
			this.cmdNext.Click += new System.EventHandler(this.CmdNextClick);
			// 
			// cmdPrevious
			// 
			this.cmdPrevious.Location = new System.Drawing.Point(11, 360);
			this.cmdPrevious.Name = "cmdPrevious";
			this.cmdPrevious.Size = new System.Drawing.Size(75, 23);
			this.cmdPrevious.TabIndex = 19;
			this.cmdPrevious.Text = "<< Previous";
			this.cmdPrevious.UseVisualStyleBackColor = true;
			this.cmdPrevious.Click += new System.EventHandler(this.CmdPreviousClick);
			// 
			// lstPolicy
			// 
			this.lstPolicy.Enabled = false;
			this.lstPolicy.FormattingEnabled = true;
			this.lstPolicy.Location = new System.Drawing.Point(14, 80);
			this.lstPolicy.Name = "lstPolicy";
			this.lstPolicy.Size = new System.Drawing.Size(221, 43);
			this.lstPolicy.TabIndex = 17;
			this.lstPolicy.SelectedIndexChanged += new System.EventHandler(this.LstPolicySelectedIndexChanged);
			this.lstPolicy.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LstImageKeyPress);
			// 
			// BoxDetails
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.label3);
			this.Controls.Add(this.lstImageDel);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lstImage);
			this.Controls.Add(this.cmdNext);
			this.Controls.Add(this.cmdPrevious);
			this.Controls.Add(this.lstPolicy);
			this.Controls.Add(this.groupBox1);
			this.Name = "BoxDetails";
			this.Size = new System.Drawing.Size(251, 455);
			this.Load += new System.EventHandler(this.BoxDetailsLoad);
			this.ParentChanged += new System.EventHandler(this.BoxDetailsParentChanged);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BoxDetailsMouseDown);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
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
	}
}
