using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NovaNet.Utils;

namespace ImageHeaven
{
    public partial class aeConfiguration : Form
    {
        private ImageConfig config = null;
        private DataRow dr = null;

        public aeConfiguration()
        {
            InitializeComponent();
            this.Text = "B'Zer - Configuration Window";
        }

        
        private void aeConfiguration_Load(object sender, EventArgs e)
        {
            grdImageKeyShrt.DataSource = ImageKeyShortCutTable();
            grdImageKeyShrt.Columns[0].ReadOnly = true;
            grdImageKeyShrt.Columns[2].Visible = false;
            DataGridViewTextBoxColumn cColumn = (DataGridViewTextBoxColumn)grdImageKeyShrt.Columns[1];
            cColumn.MaxInputLength = 1;

            grdImageValue.DataSource = ImageRelatedValue();
            grdImageValue.Columns[0].ReadOnly = true;
            grdImageValue.Columns[2].Visible = false;
            //grdImageValue.Columns[2].f = false;
            DataGridViewTextBoxColumn cImageValue = (DataGridViewTextBoxColumn)grdImageValue.Columns[1];
            cImageValue.MaxInputLength = 3;

            //dgvIndexKeys.DataSource = IndexKeyShortCutTable();
            //dgvIndexKeys.Columns[0].ReadOnly = true;
            //dgvIndexKeys.Columns[2].Visible = false;
            //grdImageValue.Columns[2].f = false;
            DataGridViewTextBoxColumn cIndex = (DataGridViewTextBoxColumn)grdImageValue.Columns[1];
            cIndex.MaxInputLength = 3;

            grdScanning.DataSource = ScanConfig();
            grdScanning.Columns[0].ReadOnly = true;
            grdScanning.Columns[2].Visible = false;
            //grdImageValue.Columns[2].f = false;
            DataGridViewTextBoxColumn cScan = (DataGridViewTextBoxColumn)grdScanning.Columns[1];
            cScan.MaxInputLength = 1;


            
        }

        public DataTable dt = new DataTable();
        public DataTable ImageKeyShortCutTable()
        {
            config = new ImageConfig(ihConstants.CONFIG_FILE_PATH);
            DataTable dt = new DataTable();

            dt.Columns.Add("Key1");
            dt.Columns.Add("Value1");
            dt.Columns.Add("Key2");
            //            dt.Columns.Add("Value2");
            //            dt.Columns.Add("Key3");
            //            dt.Columns.Add("Value3");

            dr = dt.NewRow();
            dr["Key1"] = "Crop";
            dr["Key2"] = "CROP";
            dr["Value1"] = config.GetValue(ihConstants.IMAGE_SHORTCUT_KEY_SECTION, ihConstants.CROP_KEY).Remove(1, 1);
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key1"] = "Skew Right";
            dr["Key2"] = "SKEWRIGHT";
            dr["Value1"] = config.GetValue(ihConstants.IMAGE_SHORTCUT_KEY_SECTION, ihConstants.SKEW_RIGHT_KEY).Remove(1, 1);
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key1"] = "Skew Left";
            dr["Key2"] = "SKEWLEFT";
            dr["Value1"] = config.GetValue(ihConstants.IMAGE_SHORTCUT_KEY_SECTION, ihConstants.SKEW_LEFT_KEY).Remove(1, 1);
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key1"] = "Zoom In";
            dr["Key2"] = "ZOOMIN";
            dr["Value1"] = config.GetValue(ihConstants.IMAGE_SHORTCUT_KEY_SECTION, ihConstants.ZOOM_IN_KEY).Remove(1, 1);
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key1"] = "Zoom Out";
            dr["Key2"] = "ZOOMOUT";
            dr["Value1"] = config.GetValue(ihConstants.IMAGE_SHORTCUT_KEY_SECTION, ihConstants.ZOOM_OUT_KEY).Remove(1, 1);
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key1"] = "Auto Crop";
            dr["Key2"] = "AUTOCROP";
            dr["Value1"] = config.GetValue(ihConstants.IMAGE_SHORTCUT_KEY_SECTION, ihConstants.AUTO_CROP_KEY).Remove(1, 1);
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key1"] = "Rotate Right";
            dr["Key2"] = "ROTATERIGHT";
            dr["Value1"] = config.GetValue(ihConstants.IMAGE_SHORTCUT_KEY_SECTION, ihConstants.ROTATE_RIGHT_KEY).Remove(1, 1);
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key1"] = "Rotate Left";
            dr["Key2"] = "ROTATELEFT";
            dr["Value1"] = config.GetValue(ihConstants.IMAGE_SHORTCUT_KEY_SECTION, ihConstants.ROTATE_LEFT_KEY).Remove(1, 1);
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key1"] = "Noise Remove";
            dr["Key2"] = "NOISEREMOVAL";
            dr["Value1"] = config.GetValue(ihConstants.IMAGE_SHORTCUT_KEY_SECTION, ihConstants.NOISE_REMOVE_KEY).Remove(1, 1);
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key1"] = "Clean";
            dr["Key2"] = "CLEAN";
            dr["Value1"] = config.GetValue(ihConstants.IMAGE_SHORTCUT_KEY_SECTION, ihConstants.CLEAN_KEY).Remove(1, 1);
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key1"] = "Delete";
            dr["Key2"] = "DELETE";
            dr["Value1"] = config.GetValue(ihConstants.IMAGE_SHORTCUT_KEY_SECTION, ihConstants.DELETE_KEY).Remove(1, 1);
            dt.Rows.Add(dr);
            return dt;
        }

        public DataTable IndexKeyShortCutTable()
        {
            config = new ImageConfig(ihConstants.CONFIG_FILE_PATH);
            DataTable dt = new DataTable();

            dt.Columns.Add("Key1");
            dt.Columns.Add("Value1");
            dt.Columns.Add("Key2");

            dr = dt.NewRow();
            dr["Key1"] = "Main Petition";
            dr["Key2"] = "MAINPETITION";
            dr["Value1"] = config.GetValue(ihConstants.INDEX_SHORTCUT_KEY_SECTION, ihConstants.MAINPETITION_KEY).Remove(1, 1);
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key1"] = "Main Petition Annextures";
            dr["Key2"] = "MAINPETITIONANNEXTURES";
            dr["Value1"] = config.GetValue(ihConstants.INDEX_SHORTCUT_KEY_SECTION, ihConstants.MAINPETITIONANNEXTURES_KEY).Remove(1, 1);
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key1"] = "Affidavits with Annextures";
            dr["Key2"] = "AFFIDAVITSWITHANNEXTURES";
            dr["Value1"] = config.GetValue(ihConstants.INDEX_SHORTCUT_KEY_SECTION, ihConstants.AFFIDAVITSWITHANNEXTURES_KEY).Remove(1, 1);
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key1"] = "Written Statement Objection";
            dr["Key2"] = "WRITTENSTATEMENTOBJECTION";
            dr["Value1"] = config.GetValue(ihConstants.INDEX_SHORTCUT_KEY_SECTION, ihConstants.WRITTENSTATEMENTOBJECTION_KEY).Remove(1, 1);
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key1"] = "Connected Applications with Annextures";
            dr["Key2"] = "CONNECTEDAPPLICATIONS";
            dr["Value1"] = config.GetValue(ihConstants.INDEX_SHORTCUT_KEY_SECTION, ihConstants.CONNECTEDAPPLICATIONS_KEY).Remove(1, 1);
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key1"] = "Analogous ConnectedCase";
            dr["Key2"] = "ANALOGOUSANDCONNECTEDCASE";
            dr["Value1"] = config.GetValue(ihConstants.INDEX_SHORTCUT_KEY_SECTION, ihConstants.ANALOGOUSANDCONNECTEDCASE_KEY).Remove(1, 1);
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key1"] = "Vakalatnama Affidavit of";
            dr["Key2"] = "VAKALATNAMAANDWARRENT";
            dr["Value1"] = config.GetValue(ihConstants.INDEX_SHORTCUT_KEY_SECTION, ihConstants.VAKALATNAMAANDWARRENT_KEY).Remove(1, 1);
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key1"] = "Summons Notice Warrent";
            dr["Key2"] = "SUMMONS";
            dr["Value1"] = config.GetValue(ihConstants.INDEX_SHORTCUT_KEY_SECTION, ihConstants.SUMMONS_KEY).Remove(1, 1);
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key1"] = "Witness Action Deposition";
            dr["Key2"] = "WITNESSACTIONDEPOSITION";
            dr["Value1"] = config.GetValue(ihConstants.INDEX_SHORTCUT_KEY_SECTION, ihConstants.WITNESSACTIONDEPOSITION_KEY).Remove(1, 1);
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key1"] = "Issues";
            dr["Key2"] = "ISSUES";
            dr["Value1"] = config.GetValue(ihConstants.INDEX_SHORTCUT_KEY_SECTION, ihConstants.ISSUES_KEY).Remove(1, 1);
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key1"] = "Exhibits";
            dr["Key2"] = "EXHIBITS";
            dr["Value1"] = config.GetValue(ihConstants.INDEX_SHORTCUT_KEY_SECTION, ihConstants.EXHIBITS_KEY).Remove(1, 1);
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key1"] = "Notes of Argument";
            dr["Key2"] = "NOTICEOFARGUMENT";
            dr["Value1"] = config.GetValue(ihConstants.INDEX_SHORTCUT_KEY_SECTION, ihConstants.NOTICEOFARGUMENT_KEY).Remove(1, 1);
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key1"] = "Final Judgement Order";
            dr["Key2"] = "FINALJUDGEMENTORDER";
            dr["Value1"] = config.GetValue(ihConstants.INDEX_SHORTCUT_KEY_SECTION, ihConstants.FINALJUDGEMENTORDER_KEY).Remove(1, 1);
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key1"] = "Engrossed Priliminary";
            dr["Key2"] = "ENGROSSEDPRELIMINARY";
            dr["Value1"] = config.GetValue(ihConstants.INDEX_SHORTCUT_KEY_SECTION, ihConstants.ENGROSSEDPRELIMINARY_KEY).Remove(1, 1);
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key1"] = "Orders Main Case";
            dr["Key2"] = "ORDERSMAINCASE";
            dr["Value1"] = config.GetValue(ihConstants.INDEX_SHORTCUT_KEY_SECTION, ihConstants.ORDERSMAINCASE_KEY).Remove(1, 1);
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key1"] = "Orders Connected Application";
            dr["Key2"] = "ORDERSAPPLICATIONS";
            dr["Value1"] = config.GetValue(ihConstants.INDEX_SHORTCUT_KEY_SECTION, ihConstants.ORDERSAPPLICATIONS_KEY).Remove(1, 1);
            dt.Rows.Add(dr);


            dr = dt.NewRow();
            dr["Key1"] = "Lower Court Records";
            dr["Key2"] = "LOWERCOURTRECORDS";
            dr["Value1"] = config.GetValue(ihConstants.INDEX_SHORTCUT_KEY_SECTION, ihConstants.LOWERCOURTRECORDS_KEY).Remove(1, 1);
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key1"] = "Impugned Judgement Decree Order";
            dr["Key2"] = "IMPUGNEDORDER";
            dr["Value1"] = config.GetValue(ihConstants.INDEX_SHORTCUT_KEY_SECTION, ihConstants.IMPUGNEDORDER_KEY).Remove(1, 1);
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key1"] = "Report";
            dr["Key2"] = "REPORT";
            dr["Value1"] = config.GetValue(ihConstants.INDEX_SHORTCUT_KEY_SECTION, ihConstants.REPORT_KEY).Remove(1, 1);
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key1"] = "Brief of Documents";
            dr["Key2"] = "BRIEF";
            dr["Value1"] = config.GetValue(ihConstants.INDEX_SHORTCUT_KEY_SECTION, ihConstants.BRIEF_KEY).Remove(1, 1);
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key1"] = "Others";
            dr["Key2"] = "OTHERS";
            dr["Value1"] = config.GetValue(ihConstants.INDEX_SHORTCUT_KEY_SECTION, ihConstants.OTHERS_KEY).Remove(1, 1);
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key1"] = "Terms of Settlement";
            dr["Key2"] = "SETTLEMENT";
            dr["Value1"] = config.GetValue(ihConstants.INDEX_SHORTCUT_KEY_SECTION, ihConstants.SETTLEMENT_KEY).Remove(1, 1);
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key1"] = "Rule";
            dr["Key2"] = "RULE";
            dr["Value1"] = config.GetValue(ihConstants.INDEX_SHORTCUT_KEY_SECTION, ihConstants.RULE_KEY).Remove(1, 1);
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key1"] = "Bond";
            dr["Key2"] = "BOND";
            dr["Value1"] = config.GetValue(ihConstants.INDEX_SHORTCUT_KEY_SECTION, ihConstants.BOND_KEY).Remove(1, 1);
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key1"] = "Caveat";
            dr["Key2"] = "CAVEAT";
            dr["Value1"] = config.GetValue(ihConstants.INDEX_SHORTCUT_KEY_SECTION, ihConstants.CAVEAT_KEY).Remove(1, 1);
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key1"] = "Delete";
            dr["Key2"] = "DELETE";
            dr["Value1"] = config.GetValue(ihConstants.INDEX_SHORTCUT_KEY_SECTION, ihConstants.DELETE_KEY).Remove(1, 1);
            dt.Rows.Add(dr);


            return dt;
        }
        public DataTable ImageRelatedValue()
        {

            config = new ImageConfig(ihConstants.CONFIG_FILE_PATH);
            DataTable dt = new DataTable();

            dt.Columns.Add("Key1");
            dt.Columns.Add("Value1");
            dt.Columns.Add("Key2");

            dr = dt.NewRow();
            dr["Key1"] = "Rotate Angle";
            dr["Key2"] = "ROTATEANGLE";
            dr["Value1"] = config.GetValue(ihConstants.IMAGE_RELATED_VALUE_SECTION, ihConstants.ROTATE_ANGLE_KEY).Replace("\0", "");
            dt.Rows.Add(dr);

            //            dr = dt.NewRow();
            //            dr["Key1"] = "Black ";
            //            dr["Key2"] = "SKEWX";
            //            dr["Value1"] = config.GetValue(ihConstants.IMAGE_RELATED_VALUE_SECTION,ihConstants.SKEW_X_KEY).Replace("\0","");
            //            dt.Rows.Add(dr);
            //            
            //            dr = dt.NewRow();
            //            dr["Key1"] = "Skew Angle-Y";
            //            dr["Key2"] = "SKEWY";
            //            dr["Value1"] = config.GetValue(ihConstants.IMAGE_RELATED_VALUE_SECTION,ihConstants.SKEW_Y_KEY).Replace("\0","");
            //            dt.Rows.Add(dr);

            //dr = dt.NewRow();
            //dr["Key1"] = "Zoom To()";
            //dr["Key2"] = "ZOOMFACTOR";
            //dr["Value1"] = config.GetValue(ihConstants.IMAGE_RELATED_VALUE_SECTION, ihConstants.ZOOM_FACTOR_KEY).Replace("\0", "");
            //dt.Rows.Add(dr);

            return dt;
        }
        public DataTable ScanConfig()
        {

            config = new ImageConfig(ihConstants.CONFIG_FILE_PATH);
            DataTable dt = new DataTable();

            dt.Columns.Add("Key1");
            dt.Columns.Add("Value1");
            dt.Columns.Add("Key2");

            dr = dt.NewRow();
            dr["Key1"] = "Page Splitter";
            dr["Key2"] = "PAGESPLITTER";
            dr["Value1"] = config.GetValue(ihConstants._SCAN_SECTION, ihConstants._SCAN_KEY).Replace("\0", "");
            dt.Rows.Add(dr);
            return dt;
        }

        private void grdImageKeyShrt_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            grdImageKeyShrt.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = grdImageKeyShrt.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().ToUpper();
        }

        private void grdImageKeyShrt_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            grdImageKeyShrt.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = grdImageKeyShrt.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().ToUpper();
        }

        private void grdImageKeyShrt_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            bool validBol = true;


            foreach (DataGridViewRow row in grdImageKeyShrt.Rows)
            {
                //string valuei=row.Cells[1].FormattedValue.ToString();
                if (row.Cells[1].FormattedValue.ToString().Contains(e.FormattedValue.ToString().ToUpper()))
                {
                    if (row.Index != grdImageKeyShrt.CurrentCell.RowIndex)
                    {
                        MessageBox.Show("This shortcut key already assigned, try with another", "B'Zer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Cancel = true;
                        grdImageKeyShrt.RefreshEdit();
                        validBol = false;
                        break;
                    }
                    //for (int i = 0; i < dgvIndexKeys.Rows.Count; i++)
                    //{

                    //    if (dgvIndexKeys.Rows[i].Cells[1].Value != null)
                    //    {
                    //        if (row.Cells[1].FormattedValue.ToString().Trim() == dgvIndexKeys.Rows[i].Cells[1].Value.ToString().Trim())
                    //        {
                    //            MessageBox.Show("This shortcut key already assigned, try with another", "B'Zer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //            e.Cancel = true;
                    //            grdImageKeyShrt.RefreshEdit();
                    //            validBol = false;
                    //            break;
                    //        }
                    //    }
                    //}
                }
            }
            if ((validBol == true) && (e.FormattedValue.ToString().Length == 1))
            {
                //MessageBox.Show(grdImageKeyShrt.Rows[e.RowIndex].Cells[2].Value.ToString());
                grdImageKeyShrt.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = grdImageKeyShrt.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().ToUpper();
                config = new ImageConfig(ihConstants.CONFIG_FILE_PATH);
                config.SetValue(ihConstants.IMAGE_SHORTCUT_KEY_SECTION, grdImageKeyShrt.Rows[e.RowIndex].Cells[2].Value.ToString(), e.FormattedValue.ToString());
            }
            if (e.FormattedValue.ToString() == string.Empty)
            {
                grdImageKeyShrt.RefreshEdit();
            }
        }

        private void grdImageValue_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            bool validBol = true;

            if ((validBol == true) && (e.FormattedValue.ToString().Length <= 3))
            {
                //MessageBox.Show(grdImageKeyShrt.Rows[e.RowIndex].Cells[2].Value.ToString());
                grdImageValue.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = grdImageValue.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().ToUpper();
                config = new ImageConfig(ihConstants.CONFIG_FILE_PATH);
                config.SetValue(ihConstants.IMAGE_RELATED_VALUE_SECTION, grdImageValue.Rows[e.RowIndex].Cells[2].Value.ToString(), e.FormattedValue.ToString());
            }
            if (e.FormattedValue.ToString() == string.Empty)
            {
                grdImageValue.RefreshEdit();
            }
        }

        private void grdImageValue_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is TextBox)
            {

                TextBox tb = e.Control as TextBox;

                tb.KeyPress += new KeyPressEventHandler(tb_KeyPress);

            }
        }
        void tb_KeyPress(object sender, KeyPressEventArgs e)
        {

            if ((char.IsDigit(e.KeyChar)))
            {

                if ((e.KeyChar == '\b') || (e.KeyChar == '0') || (e.KeyChar == '1')) //allow the backspace key
                {

                    e.Handled = false;

                }
                else
                {
                    e.Handled = true;
                }
            }
            else
            {
                e.Handled = true;
            }

        }

        private void dgvIndexKeys_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            //bool validBol = true;
            //foreach (DataGridViewRow row in dgvIndexKeys.Rows)
            //{
            //    string valuei = row.Cells[1].FormattedValue.ToString();
            //    if (row.Cells[1].FormattedValue.ToString().Contains(e.FormattedValue.ToString()))
            //    {
            //        if (row.Index != dgvIndexKeys.CurrentCell.RowIndex)
            //        {
            //            MessageBox.Show("This shortcut key already assigned, try with another", "B'Zer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            e.Cancel = true;
            //            dgvIndexKeys.RefreshEdit();
            //            validBol = false;
            //            break;
            //            //grdImageKeyShrt.Rows[e.RowIndex].Cells[e.ColumnIndex].Value=grdImageKeyShrt.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().ToUpper();
            //        }
            //        //for (int i = 0; i < grdImageKeyShrt.Rows.Count; i++)
            //        //{
            //        //    if (grdImageKeyShrt.Rows[i].Cells[1].Value != null)
            //        //    {
            //        //        if (row.Cells[1].FormattedValue.ToString().Trim() == grdImageKeyShrt.Rows[i].Cells[1].Value.ToString().Trim())
            //        //        {
            //        //            MessageBox.Show("This shortcut key already assigned, try with another", "B'Zer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        //            e.Cancel = true;
            //        //            dgvIndexKeys.RefreshEdit();
            //        //            validBol = false;
            //        //            break;
            //        //        }
            //        //    }
            //        //}
            //    }
            //}
            //if ((validBol == true) && (e.FormattedValue.ToString().Length <= 3))
            //{
            //    //MessageBox.Show(grdImageKeyShrt.Rows[e.RowIndex].Cells[2].Value.ToString());
            //    dgvIndexKeys.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = dgvIndexKeys.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().ToUpper();
            //    config = new ImageConfig(ihConstants.CONFIG_FILE_PATH);
            //    config.SetValue(ihConstants.INDEX_SHORTCUT_KEY_SECTION, dgvIndexKeys.Rows[e.RowIndex].Cells[2].Value.ToString(), e.FormattedValue.ToString());
            //}
            //if (e.FormattedValue.ToString() == string.Empty)
            //{
            //    dgvIndexKeys.RefreshEdit();
            //}
        }

        private void dgvIndexKeys_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            //dgvIndexKeys.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = dgvIndexKeys.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().ToUpper();
        }

        private void dgvIndexKeys_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //dgvIndexKeys.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = dgvIndexKeys.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().ToUpper();
        }

        private void grdScanning_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                grdScanning.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = e.FormattedValue;
                config = new ImageConfig(ihConstants.CONFIG_FILE_PATH);
                config.SetValue(ihConstants._SCAN_SECTION, grdScanning.Rows[e.RowIndex].Cells[2].Value.ToString(), e.FormattedValue.ToString());
            }
            if (e.FormattedValue.ToString() == string.Empty)
            {
                grdScanning.RefreshEdit();
            }
        }

        private void grdScanning_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is TextBox)
            {

                TextBox tb = e.Control as TextBox;

                tb.KeyPress += new KeyPressEventHandler(tb_KeyPress);

            }
        }

        private void grdScanning_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            grdScanning.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = grdScanning.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().ToUpper();
        }
    }
}
