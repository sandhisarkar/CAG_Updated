/*
 * Created by SharpDevelop.
 * User: RahulN
 * Date: 20/03/2009
 * Time: 3:36 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using NovaNet.wfe;
using LItems;
using System.Data;
using System.Data.Odbc;
using NovaNet.Utils;
//using ImageHeaven;

namespace wSelect
{
	public delegate void NextClickedHandler(object sender, EventArgs e);
	public delegate void PreviousClickedHandler(object sender, EventArgs e);
	public delegate void PolicyChangeHandler(object sender, EventArgs e);
	public delegate void ImageChangeHandler(object sender, EventArgs e);
	public delegate void BoxDetailsLoaded(object sender, EventArgs e);	
	public delegate void LstDelImageKeyPress(object sender, KeyEventArgs e);	
	public delegate void LstImageIndexKeyPress(object sender, KeyPressEventArgs e);
	public delegate void BoxDetailsMouseClick(object sender, MouseEventArgs e);
    public delegate void LstImageClick(object sender, EventArgs e);
    public delegate void LstDelImageClick(object sender, EventArgs e);
    public delegate void LstNextKeyPress(object sender, KeyEventArgs e);
    public delegate void ComboValueChanged(object sender, EventArgs e);
	/// <summary>
	/// Description of UserControl1.
	/// </summary>
	public partial class BoxDetails : UserControl
	{
		wfeBox pBox;
		wfeBatch pBatch;
		wfeProject pProject;
		private OdbcConnection sqlCon;
		private int indexCount=0;
		private eSTATES[] currState;
        private eSTATES[] imageCurrState;
		private int pLeft = 0;
		private int pTop = 0;
        private Credentials crd;
		public BoxDetails()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
        public BoxDetails(wfeBox prmBox, OdbcConnection prmCon, eSTATES[] prmPolicyState, eSTATES[] prmImageState)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			pBox = prmBox;
			sqlCon=prmCon;
			currState=prmPolicyState;
            imageCurrState = prmImageState;
            cmbDesValue.SelectedIndex = 0;
			DisplayValues();
            if ((currState[0] == eSTATES.POLICY_FQC) || (currState[0] == eSTATES.POLICY_EXCEPTION))
            {
                label5.Visible = true;
                cmbBox.Visible = true;
            }
            else
            {
                label5.Visible = false;
                cmbBox.Visible = false;
            }
			PopulatePolicyList();
			if (lstPolicy.Items.Count > 0)
			{
				lstPolicy.SelectedIndex=0;
			}
		}
        public BoxDetails(wfeBox prmBox, OdbcConnection prmCon, eSTATES[] prmPolicyState, eSTATES[] prmImageState,Credentials pCrd)
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();
            pBox = prmBox;
            sqlCon = prmCon;
            currState = prmPolicyState;
            imageCurrState = prmImageState;
            cmbDesValue.SelectedIndex = 0;
            if ((currState[0] == eSTATES.POLICY_FQC) || (currState[0] == eSTATES.POLICY_EXCEPTION))
            {
                label5.Visible = true;
                cmbBox.Visible = true;
            }
            else
            {
                label5.Visible = false;
                cmbBox.Visible = false;
            }
            DisplayValues();
            crd = pCrd;
            PopulatePolicyList();
            if (lstPolicy.Items.Count > 0)
            {
                lstPolicy.SelectedIndex = 0;
            }
        }
		private void PopulatePolicyList()
		{
			lstPolicy.Items.Clear();
			ArrayList arrPolicy=new ArrayList();
			wQuery pQuery=new ihwQuery(sqlCon,crd);
            CtrlPolicy ctrPol;
            
            arrPolicy=pQuery.GetItems(eITEMS.POLICY,currState,pBox);
            for(int i=0;i<arrPolicy.Count;i++)
            {
            	ctrPol = (CtrlPolicy)arrPolicy[i];
            	lstPolicy.Items.Add(ctrPol.PolicyNumber);
            }
            if (arrPolicy.Count > 0)
            {
                lblCount.Text = "Pending: " + arrPolicy.Count;
            }
            else
            {
                lblCount.Text = "0";
                lstImageDel.Items.Clear();
            }
		}
        private void PopulateBoxCombo()
        {
            string batchKey = null;
            DataSet ds = new DataSet();

            wfeBox tmpBox = new wfeBox(sqlCon);
            
            batchKey = pBox.ctrlBox.BatchKey.ToString();
            ds = tmpBox.GetBox(currState,pBox.ctrlBox.ProjectCode, Convert.ToInt32(batchKey));
            if (ds.Tables[0].Rows.Count > 0)
            {
                cmbBox.DataSource = ds.Tables[0];
                cmbBox.DisplayMember = ds.Tables[0].Columns[0].ToString();
            }
        }
        private void PopulateFilteredPolicyList(string pDocType, string pFilterSign, int pCount,eSTATES[] pState)
        {
            lstPolicy.Items.Clear();
            wQuery pQuery = new ihwQuery(sqlCon);
            CtrlPolicy ctrlPolicy;
            wfePolicy policy;
            DataSet pDs = new DataSet();
            string policyNo = string.Empty;

            ctrlPolicy = new CtrlPolicy(pBox.ctrlBox.ProjectCode, pBox.ctrlBox.BatchKey, pBox.ctrlBox.BoxNumber, "0");
            policy = new wfePolicy(sqlCon, ctrlPolicy);
            pDs = policy.GetFilteredPolicy(pDocType, pFilterSign, pCount,pState);
            for (int i = 0; i < pDs.Tables[0].Rows.Count; i++)
            {
                policyNo = pDs.Tables[0].Rows[i]["policy_number"].ToString();
                lstPolicy.Items.Add(policyNo);
            }
            if (pDs.Tables[0].Rows.Count > 0)
            {
                lblCount.Text = "Pending: " + pDs.Tables[0].Rows.Count;
            }
            else
            {
                lblCount.Text = "0";
                lstImageDel.Items.Clear();
            }
        }
        

		//To display values in header
		void DisplayValues()
		{
			pBatch=new wfeBatch(sqlCon);
			pProject=new wfeProject(sqlCon);
			lblProject.Text="Project: " + pProject.GetProjectName(pBox.ctrlBox.ProjectCode);
			lblBatch.Text="Batch: " + pBatch.GetBatchName(pBox.ctrlBox.ProjectCode,pBox.ctrlBox.BatchKey);
			lblBox.Text="Box: " + pBox.ctrlBox.BoxNumber.ToString();
            lblProject.ForeColor = Color.RoyalBlue;
            lblBatch.ForeColor = Color.RoyalBlue;
            lblBox.ForeColor = Color.RoyalBlue;
            lblCount.ForeColor = Color.RoyalBlue;
            if ((currState[0] == eSTATES.POLICY_FQC) || (currState[0] == eSTATES.POLICY_EXCEPTION))
            {
                PopulateBoxCombo();
            }
		}

		private void PopulateImageList(string prmPolicyNo)
		{
			lstImage.Items.Clear();
			CtrlPolicy ctrlPolicy = new CtrlPolicy(pBox.ctrlBox.ProjectCode,pBox.ctrlBox.BatchKey,pBox.ctrlBox.BoxNumber.ToString(),prmPolicyNo);
		    wItem policy = new wfePolicy(sqlCon, ctrlPolicy);
		    //ListViewItem lstView;
			ArrayList arrImage=new ArrayList();
			wQuery pQuery=new ihwQuery(sqlCon);
            CtrlImage ctrlImage;
            arrImage=pQuery.GetItems(eITEMS.PAGE,currState,policy);
            for(int i=0;i<arrImage.Count;i++)
            {
            	ctrlImage = (CtrlImage)arrImage[i];
            	//lstView=lstImage.Items.Add(ctrlImage.ImageName);
            	
            	if(ctrlImage.DocType != string.Empty)
            	{
            		lstImage.Items.Add(ctrlImage.ImageName + "-" + ctrlImage.DocType);
            	}
            	else
            		lstImage.Items.Add(ctrlImage.ImageName);
            }
		}
		public void PopulateDelList(string prmPolicyNo)
		{
			lstImageDel.Items.Clear();
			CtrlPolicy ctrlPolicy = new CtrlPolicy(pBox.ctrlBox.ProjectCode,pBox.ctrlBox.BatchKey,pBox.ctrlBox.BoxNumber.ToString(),prmPolicyNo);
		    wItem policy = new wfePolicy(sqlCon, ctrlPolicy);
		    //ListViewItem lstView;
			ArrayList arrImage=new ArrayList();
			wfeImage wImage=new wfeImage(sqlCon);
			//eSTATES[] policyState=new eSTATES[1];
			eSTATES[] imageState=new eSTATES[1];
//            policyState[0]=currState;
            imageState[0]=eSTATES.PAGE_DELETED;
            //state[1]=eSTATES.POLICY_SCANNED;
            CtrlImage ctrlImage;
            arrImage=wImage.GetDeletedPageList(currState,imageState,policy);
            for(int i=0;i<arrImage.Count;i++)
            {
            	ctrlImage = (CtrlImage)arrImage[i];
            	//lstView=lstImage.Items.Add(ctrlImage.ImageName);
            	lstImageDel.Items.Add(ctrlImage.ImageName);
            }
		}
		
		/// <summary>
		/// When the Next button is clicked
		/// </summary>
		[Category("Action")]
        [Description("Fires when the Next button is clicked.")]
		public event NextClickedHandler NextClicked;		
		void CmdNextClick(object sender, EventArgs e)
		{
			if (NextClicked != null)
			{
				NextClicked(this, e);
			}
		}
		/// <summary>
		/// When the Previous button is clicked
		/// </summary>
		[Category("Action")]
        [Description("Fires when the Previous button is clicked.")]
		public event PreviousClickedHandler PreviousClicked;		
		void CmdPreviousClick(object sender, EventArgs e)
		{
			if((indexCount>0) && (lstImage.Items.Count>0))
			{
				indexCount=indexCount-1;
				lstImage.SelectedIndex=indexCount;
			}
			if((indexCount==0) && (lstImage.Items.Count>0) && (lstPolicy.SelectedIndex != 0))
			{
				indexCount=0;
				lstPolicy.SelectedIndex=lstPolicy.SelectedIndex-1;
			}
			if (PreviousClicked != null)
			{
				PreviousClicked(this, e);
			}
		}
	
        

		/// <summary>
		/// When policy is changed
		/// </summary>
		[Category("Action")]
        [Description("Fires when the Policy is changed.")]
		public event PolicyChangeHandler PolicyChanged;
		void LstPolicySelectedIndexChanged(object sender, EventArgs e)
		{
			PopulateImageList(lstPolicy.SelectedItem.ToString());

            if (lstImage.Items.Count > 0)
            {
                lstImage.SelectedIndex = 0;
            }
			if (PolicyChanged != null)
			{
				PolicyChanged(this, e);
			}
		}
		[Category("Action")]
        [Description("Fires when the Image is changed.")]
		public event ImageChangeHandler ImageChanged;
		void LstImageSelectedIndexChanged(object sender, EventArgs e)
		{
			//PopulateDelList((int)lstPolicy.SelectedItem);
            DateTime st = DateTime.Now;
			indexCount=lstImage.SelectedIndex;
			
			if((ImageChanged != null) && (indexCount >= 0))
			{
				ImageChanged(this,e);
			}
            DateTime end = DateTime.Now;
            TimeSpan duration = end - st;
            //MessageBox.Show(duration.Milliseconds.ToString());
		}		
		
		[Category("Action")]
        [Description("Fires when the control is loaded.")]
		public event BoxDetailsLoaded BoxLoaded;		
		void BoxDetailsLoad(object sender, EventArgs e)
		{
			if(BoxLoaded != null)
			{
				BoxLoaded(this,e);
                System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
                toolTip.SetToolTip(lstImageDel, "Press (Insert) key to insert this deleted image");
			}
		}

		void BoxDetailsParentChanged(object sender, EventArgs e)
		{
			this.Left=Parent.ClientRectangle.Left;
			this.Top = Parent.ClientRectangle.Top;
			pLeft = this.Left;
			pTop = this.Top;
		}
        public bool MovePrevious()
        {
            if ((indexCount > 0) && (lstImage.Items.Count > 0))
            {
                //ClearSelection();
                indexCount = indexCount - 1;
                lstImage.SelectedIndex = indexCount;
            }
            if ((indexCount == 0) && (lstImage.Items.Count > 0) && (lstPolicy.SelectedIndex != 0))
            {
                //ClearSelection();
                indexCount = 0;
                lstPolicy.SelectedIndex = lstPolicy.SelectedIndex - 1;
            }
            return true;
        }
        
        
       private void ClearSelection()
        {
            for (int i = 0; i < lstImage.Items.Count; i++)
            {
                if (lstImage.GetSelected(i))
                {
                    lstImage.SetSelected(i, false);
                }
            }
        }
		public bool MoveNext()
		{
            if (lstImage.Items.Count > 0)
            {
                indexCount = indexCount + 1;
                if ((lstImage.Items.Count - 1) < indexCount)
                {
                    indexCount = 0;
                    if ((lstPolicy.Items.Count - 1) > (lstPolicy.SelectedIndex))
                    {
                        lstPolicy.SelectedIndex = lstPolicy.SelectedIndex + 1;
                    }
                    if (lstImage.Items.Count > 0)
                    {
                        //ClearSelection();
                        lstImage.SelectedIndex = indexCount;
                    }
                    //lstImage.EnsureVisible(indexCount);
                }
                else
                {
                    //					lstImage.Items[indexCount].Selected=true;
                    //					lstImage.EnsureVisible(indexCount);
                    //ClearSelection();
                    lstImage.SelectedIndex = indexCount;
                }
            }
            else
            {
                if ((lstPolicy.Items.Count - 1) > (lstPolicy.SelectedIndex))
                {
                    lstPolicy.SelectedIndex = lstPolicy.SelectedIndex + 1;
                }
            }
            return true;
 		}

        public bool MoveNext(bool prmMove)
        {
            if (prmMove == true)
            {
                if (lstImage.Items.Count > 0)
                {
                    indexCount = indexCount + 1;
                    if ((lstImage.Items.Count - 1) < indexCount)
                    {
                        indexCount = 0;
                        if ((lstPolicy.Items.Count - 1) > (lstPolicy.SelectedIndex))
                        {
                            lstPolicy.SelectedIndex = lstPolicy.SelectedIndex + 1;
                        }
                        if (lstImage.Items.Count > 0)
                        {
                            //ClearSelection();
                            lstImage.SelectedIndex = indexCount;
                        }
                        //lstImage.EnsureVisible(indexCount);
                    }
                    else
                    {
                        //					lstImage.Items[indexCount].Selected=true;
                        //					lstImage.EnsureVisible(indexCount);
                        //ClearSelection();
                        lstImage.SelectedIndex = indexCount;
                    }
                }
                else
                {
                    if ((lstPolicy.Items.Count - 1) > (lstPolicy.SelectedIndex))
                    {
                        lstPolicy.SelectedIndex = lstPolicy.SelectedIndex + 1;
                    }
                }
            }
            return true;
        }
		public bool MoveUp()
		{
			ArrayList arr = new ArrayList();
			CtrlPolicy ctPolicy = new CtrlPolicy(pBox.ctrlBox.ProjectCode,pBox.ctrlBox.BatchKey,pBox.ctrlBox.BoxNumber,lstPolicy.SelectedItem.ToString());
			wfePolicy wp = new wfePolicy(sqlCon,ctPolicy);
			//Can only move up when there are records in the list box of images
			//and the currently selected element is greater than 0
            if (lstImage.Items.Count > 0 && lstImage.SelectedIndex > 0)
            {
            	indexCount = indexCount - 1;
            	object swp = lstImage.Items[lstImage.SelectedIndex];
            	lstImage.Items[lstImage.SelectedIndex] = lstImage.Items[lstImage.SelectedIndex-1];;
            	lstImage.Items[lstImage.SelectedIndex-1] = swp;
            	lstImage.SelectedIndex = lstImage.SelectedIndex - 1;
            	for (int i = 0; i < lstImage.Items.Count; i++)
            	{
            		arr.Add(lstImage.Items[i]);
            	}
            	wp.UpdateSrl(arr);
            }
            
			return true;
 		}
		public bool MoveDown()
		{
			ArrayList arr = new ArrayList();
			CtrlPolicy ctPolicy = new CtrlPolicy(pBox.ctrlBox.ProjectCode,pBox.ctrlBox.BatchKey,pBox.ctrlBox.BoxNumber,lstPolicy.SelectedItem.ToString());
			wfePolicy wp = new wfePolicy(sqlCon,ctPolicy);			
			//Can only move up when there are records in the list box of images
			//and the currently selected element is less than count of items-1
			if (lstImage.Items.Count > 0 && lstImage.SelectedIndex < (lstImage.Items.Count-1))
            {
            	string swp = lstImage.SelectedItem.ToString();
            	lstImage.Items[lstImage.SelectedIndex] = lstImage.Items[lstImage.SelectedIndex+1];
            	lstImage.Items[lstImage.SelectedIndex+1] = swp;
            	lstImage.SelectedIndex = lstImage.SelectedIndex + 1;
            	indexCount = indexCount + 1;
            	for (int i = 0; i < lstImage.Items.Count; i++)
            	{
            		arr.Add(lstImage.Items[i]);
            	}
            	wp.UpdateSrl(arr);

			}
			return true;
 		}
		public bool DeleteNotify(int currIndex)
		{
			lstImage.Items.RemoveAt(currIndex);
			//PopulateImageList((int)lstPolicy.SelectedItem);
			if(lstImage.Items.Count>0)
			{
				if(currIndex != lstImage.Items.Count)
				{
					lstImage.SelectedIndex=currIndex;
					indexCount=lstImage.SelectedIndex;
				}
				else
				{
					if((lstPolicy.SelectedIndex) != (lstPolicy.Items.Count-1))
					{
						lstPolicy.SelectedIndex=lstPolicy.SelectedIndex+1;
					}
					else
					{
						lstImage.SelectedIndex=currIndex-1;
					}
				}
			}
			else
			{
				if((lstPolicy.SelectedIndex) != (lstPolicy.Items.Count-1))
				{
					lstPolicy.SelectedIndex=lstPolicy.SelectedIndex+1;
				}
			}
			PopulateDelList(lstPolicy.SelectedItem.ToString());
//			//MoveNext();
//			lstImage.Refresh();
//			lstImageDel.Refresh();
			return true;
		}
        public void RefreshNotify()
        {
            PopulatePolicyList();
            if (lstPolicy.Items.Count > 0)
            {
                lstPolicy.SelectedIndex = 0;
            }
            //PopulateImageList((int)lstPolicy.SelectedItem);
        }

        public void RefreshNotify(wfeBox prmBox)
        {
            pBox = prmBox;
            PopulatePolicyList();
            if (lstPolicy.Items.Count > 0)
            {
                lstPolicy.SelectedIndex = 0;
            }
            pBatch = new wfeBatch(sqlCon);
            pProject = new wfeProject(sqlCon);
            lblProject.Text = "Project: " + pProject.GetProjectName(pBox.ctrlBox.ProjectCode);
            lblBatch.Text = "Batch: " + pBatch.GetBatchName(pBox.ctrlBox.ProjectCode, pBox.ctrlBox.BatchKey);
            lblBox.Text = "Box: " + pBox.ctrlBox.BoxNumber.ToString();
            lblProject.ForeColor = Color.RoyalBlue;
            lblBatch.ForeColor = Color.RoyalBlue;
            lblBox.ForeColor = Color.RoyalBlue;
            lblCount.ForeColor = Color.RoyalBlue;
            //PopulateImageList((int)lstPolicy.SelectedItem);
        }


        public void RefreshNotify(eSTATES[] prmPolicyState)
        {
            currState = prmPolicyState;
            PopulatePolicyList();
            if (lstPolicy.Items.Count > 0)
            {
                lstPolicy.SelectedIndex = 0;
                PopulateImageList((string)lstPolicy.SelectedItem);
            }
        }
        public void RefreshNotify(string pDocType,string pFilterSign,int pCount,eSTATES[] pState)
        {
            try
            {
                PopulateFilteredPolicyList(pDocType, pFilterSign, pCount,pState);
                if (lstPolicy.Items.Count > 0)
                {
                    lstPolicy.SelectedIndex = 0;
                    PopulateImageList(lstPolicy.SelectedItem.ToString());
                }
            }
            catch(Exception ex)
            {
                string err = ex.Message;
            }
        }

		void Panel1DragEnter(object sender, DragEventArgs e)
		{
			
		}
		
		void Panel1DragOver(object sender, DragEventArgs e)
		{
			
		}
//		[Category("Action")]
//        [Description("Fires when the image from deleted list inserted.")]
//		public event LstDelImageKeyPress LstDelIamgeInsert;	
//		void LstDelImageKeyPress(object sender, KeyPressEventArgs e)
//		{
//			if(LstDelIamgeInsert != null)
//			{
//				LstDelIamgeInsert(this,e);
//			}
//		}
		public bool InsertNotify(int currIndex)
		{
			//lstImage.Items.Remove(delValue);
			PopulateImageList(lstPolicy.SelectedItem.ToString());
			if(lstImage.Items.Count>0)
			{
				if(currIndex != lstImage.Items.Count)
				{
					lstImage.SelectedIndex=currIndex;
					indexCount=lstImage.SelectedIndex;
				}
				else
					lstPolicy.SelectedIndex=lstPolicy.SelectedIndex+1;
			}
			else
			{
				if((lstPolicy.SelectedIndex) != (lstPolicy.Items.Count-1))
				{
					lstPolicy.SelectedIndex=lstPolicy.SelectedIndex+1;
				}
			}
			PopulateDelList(lstPolicy.SelectedItem.ToString());
//			//MoveNext();
//			lstImage.Refresh();
//			lstImageDel.Refresh();
			return true;
		}
		[Category("Action")]
        [Description("Fires when the image from deleted list inserted.")]
		public event LstDelImageKeyPress LstDelIamgeInsert;	
		void LstImageDelKeyDown(object sender, KeyEventArgs e)
		{
			if(LstDelIamgeInsert != null)
			{
				LstDelIamgeInsert(this,e);
			}
		}
		
		[Category("Action")]
        [Description("Fires when the key Pressed for indexing")]
		public event LstImageIndexKeyPress LstImageIndex;	
		void LstImageKeyPress(object sender, KeyPressEventArgs e)
		{
			if(LstImageIndex != null)
			{
				LstImageIndex(this,e);
			}
		}
		public event BoxDetailsMouseClick BoxMouseClick;		
		void BoxDetailsMouseDown(object sender, MouseEventArgs e)
		{
			if(BoxMouseClick != null)
			{
				BoxMouseClick(this,e);
			}
		}
        
        private void lstPolicy_SelectedValueChanged(object sender, EventArgs e)
        {
        
        }
        public event LstImageClick LstImgClick;
        private void lstImage_Click(object sender, EventArgs e)
        {
            if (LstImgClick != null)
            {
                LstImgClick(this, e);
            }

        }
        public event LstNextKeyPress LstNextKey;
        private void cmdNext_KeyUp(object sender, KeyEventArgs e)
        {
            if (LstNextKey != null)
            {
                LstNextKey(this,e);
            }
        }
        public event LstDelImageClick LstDelImgClick;
        private void lstImageDel_Click(object sender, EventArgs e)
        {
            if (LstDelImgClick != null)
            {
                LstDelImgClick(this, e);
            }
        }

        private void lstImageDel_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lblBox_Click(object sender, EventArgs e)
        {

        }
        public event ComboValueChanged cmbChanged;
        private void cmbBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbChanged != null)
            {
                cmbChanged(this, e);
            }
        }

        private void cmbDesValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            int uInt = 0;
            double uDouble = 0;
            if (int.TryParse(e.KeyChar.ToString(), out uInt) || double.TryParse(e.KeyChar.ToString(), out uDouble) || e.KeyChar == (char)Keys.Delete || e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
	}
}
