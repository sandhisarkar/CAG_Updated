
namespace ImageHeaven
{
    partial class aeConfiguration
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(aeConfiguration));
            this.tabConfigure = new System.Windows.Forms.TabControl();
            this.ImageKey = new System.Windows.Forms.TabPage();
            this.grdImageKeyShrt = new System.Windows.Forms.DataGridView();
            this.ImageValue = new System.Windows.Forms.TabPage();
            this.grdImageValue = new System.Windows.Forms.DataGridView();
            this.tblScan = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.grdScanning = new System.Windows.Forms.DataGridView();
            this.tabConfigure.SuspendLayout();
            this.ImageKey.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdImageKeyShrt)).BeginInit();
            this.ImageValue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdImageValue)).BeginInit();
            this.tblScan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdScanning)).BeginInit();
            this.SuspendLayout();
            // 
            // tabConfigure
            // 
            this.tabConfigure.Controls.Add(this.ImageKey);
            this.tabConfigure.Controls.Add(this.ImageValue);
            this.tabConfigure.Controls.Add(this.tblScan);
            this.tabConfigure.Location = new System.Drawing.Point(10, 14);
            this.tabConfigure.Name = "tabConfigure";
            this.tabConfigure.SelectedIndex = 0;
            this.tabConfigure.Size = new System.Drawing.Size(390, 488);
            this.tabConfigure.TabIndex = 4;
            // 
            // ImageKey
            // 
            this.ImageKey.Controls.Add(this.grdImageKeyShrt);
            this.ImageKey.Location = new System.Drawing.Point(4, 22);
            this.ImageKey.Name = "ImageKey";
            this.ImageKey.Padding = new System.Windows.Forms.Padding(3);
            this.ImageKey.Size = new System.Drawing.Size(382, 462);
            this.ImageKey.TabIndex = 0;
            this.ImageKey.Text = "Image Key Shortcut";
            this.ImageKey.UseVisualStyleBackColor = true;
            // 
            // grdImageKeyShrt
            // 
            this.grdImageKeyShrt.AllowUserToAddRows = false;
            this.grdImageKeyShrt.AllowUserToDeleteRows = false;
            this.grdImageKeyShrt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdImageKeyShrt.ColumnHeadersVisible = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdImageKeyShrt.DefaultCellStyle = dataGridViewCellStyle1;
            this.grdImageKeyShrt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdImageKeyShrt.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.grdImageKeyShrt.Location = new System.Drawing.Point(3, 3);
            this.grdImageKeyShrt.MultiSelect = false;
            this.grdImageKeyShrt.Name = "grdImageKeyShrt";
            this.grdImageKeyShrt.ShowCellToolTips = false;
            this.grdImageKeyShrt.Size = new System.Drawing.Size(376, 456);
            this.grdImageKeyShrt.TabIndex = 2;
            this.grdImageKeyShrt.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdImageKeyShrt_CellEndEdit);
            this.grdImageKeyShrt.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.grdImageKeyShrt_CellValidating);
            this.grdImageKeyShrt.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdImageKeyShrt_RowLeave);
            // 
            // ImageValue
            // 
            this.ImageValue.Controls.Add(this.grdImageValue);
            this.ImageValue.Location = new System.Drawing.Point(4, 22);
            this.ImageValue.Name = "ImageValue";
            this.ImageValue.Padding = new System.Windows.Forms.Padding(3);
            this.ImageValue.Size = new System.Drawing.Size(382, 462);
            this.ImageValue.TabIndex = 1;
            this.ImageValue.Text = "Image Editing";
            this.ImageValue.UseVisualStyleBackColor = true;
            // 
            // grdImageValue
            // 
            this.grdImageValue.AllowUserToAddRows = false;
            this.grdImageValue.AllowUserToDeleteRows = false;
            this.grdImageValue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdImageValue.ColumnHeadersVisible = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdImageValue.DefaultCellStyle = dataGridViewCellStyle2;
            this.grdImageValue.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.grdImageValue.Location = new System.Drawing.Point(8, 6);
            this.grdImageValue.MultiSelect = false;
            this.grdImageValue.Name = "grdImageValue";
            this.grdImageValue.ShowCellToolTips = false;
            this.grdImageValue.Size = new System.Drawing.Size(368, 450);
            this.grdImageValue.TabIndex = 3;
            this.grdImageValue.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.grdImageValue_CellValidating);
            this.grdImageValue.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.grdImageValue_EditingControlShowing);
            // 
            // tblScan
            // 
            this.tblScan.Controls.Add(this.label1);
            this.tblScan.Controls.Add(this.grdScanning);
            this.tblScan.Location = new System.Drawing.Point(4, 22);
            this.tblScan.Name = "tblScan";
            this.tblScan.Size = new System.Drawing.Size(382, 462);
            this.tblScan.TabIndex = 3;
            this.tblScan.Text = "Scanning";
            this.tblScan.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(11, 423);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(362, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Use 0 for black page separator, 1 for barcode separator";
            // 
            // grdScanning
            // 
            this.grdScanning.AllowUserToAddRows = false;
            this.grdScanning.AllowUserToDeleteRows = false;
            this.grdScanning.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdScanning.ColumnHeadersVisible = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdScanning.DefaultCellStyle = dataGridViewCellStyle3;
            this.grdScanning.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.grdScanning.Location = new System.Drawing.Point(5, 6);
            this.grdScanning.MultiSelect = false;
            this.grdScanning.Name = "grdScanning";
            this.grdScanning.ShowCellToolTips = false;
            this.grdScanning.Size = new System.Drawing.Size(372, 401);
            this.grdScanning.TabIndex = 5;
            this.grdScanning.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.grdScanning_CellValidating);
            this.grdScanning.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.grdScanning_EditingControlShowing);
            this.grdScanning.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdScanning_RowLeave);
            // 
            // aeConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 516);
            this.Controls.Add(this.tabConfigure);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "aeConfiguration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "aeConfiguration";
            this.Load += new System.EventHandler(this.aeConfiguration_Load);
            this.tabConfigure.ResumeLayout(false);
            this.ImageKey.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdImageKeyShrt)).EndInit();
            this.ImageValue.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdImageValue)).EndInit();
            this.tblScan.ResumeLayout(false);
            this.tblScan.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdScanning)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabConfigure;
        private System.Windows.Forms.TabPage ImageKey;
        private System.Windows.Forms.DataGridView grdImageKeyShrt;
        private System.Windows.Forms.TabPage ImageValue;
        private System.Windows.Forms.DataGridView grdImageValue;
        private System.Windows.Forms.TabPage tblScan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView grdScanning;
    }
}