using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using POS_System.Classes;

namespace POS_System.Forms
{
    public partial class frmManageAccounts : Form
    {
        private int childFormNumber = 0;

        public frmManageAccounts()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }


        //functions are define here ----------------------------------------------------------------------------------------
        string query;
        DataTable accTypes;
        DataTable bankCodes;
        DataTable bUnits;
        DataTable ctrlAccCd;

        private void LoadComboBoxAccType()
        {
            try
            {
                accTypes = new DataTable();
                using(SqlConnection conn = DBConnectionManager.GetConnection())
                {
                    conn.Open();
                    query = "Select AccTypCd,AccTypNm From vewAccTypCd Where CKy='1' Order By AccTypCd";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(accTypes);
                    accTypes.Columns.Add("DisplayAccType", typeof(string), "AccTypCd + ' - ' + AccTypNm");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadComboboxBankCode()
        {
            try
            {
                bankCodes = new DataTable();
                using (SqlConnection conn = DBConnectionManager.GetConnection())
                {
                    conn.Open();
                    query = "Select BnkCd,BnkNm from vewBnkCd";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(bankCodes);
                    bankCodes.Columns.Add("DisplayBankCode", typeof(string), "BnkCd + ' - ' + BnkNm");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadComboBoxBUnit()
        {
            try
            {
                bUnits = new DataTable();
                using(SqlConnection conn = DBConnectionManager.GetConnection())
                {
                    conn.Open();
                    query = "Select BUCd,BUNm,BUKy from vewBUCd";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(bUnits);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Not load ctrlAccCd
        private void LoadComboBoxCtrlAccCd()
        {
            try
            {
                ctrlAccCd = new DataTable();
                using (SqlConnection conn = DBConnectionManager.GetConnection())
                {
                    conn.Open();
                    query = "Select CrnCd,CrnNm from vewCrnCd";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ctrlAccCd);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetupDgvAccountManage ()
        {
            dgvManageAccount.Columns.Clear();

            DataGridViewTextBoxColumn colAccKey = new DataGridViewTextBoxColumn();
            colAccKey.Name = "accKey";
            colAccKey.HeaderText = "AccKey";
            colAccKey.DataPropertyName = "accKey";
            dgvManageAccount.Columns.Add(colAccKey);

            DataGridViewTextBoxColumn colAccCode = new DataGridViewTextBoxColumn();
            colAccCode.Name = "accCode";
            colAccCode.HeaderText = "Acc Code";
            colAccCode.DataPropertyName = "accCode";
            dgvManageAccount.Columns.Add(colAccCode);

            DataGridViewTextBoxColumn colAccName = new DataGridViewTextBoxColumn();
            colAccName.Name = "accName";
            colAccName.HeaderText = "Acc Name";
            colAccName.DataPropertyName = "accName";
            dgvManageAccount.Columns.Add(colAccName);

            DataGridViewComboBoxColumn colAccType = new DataGridViewComboBoxColumn();
            colAccType.Name = "accType";
            colAccType.HeaderText = "Acc Type";
            colAccType.DataPropertyName = "accType";
            colAccType.DataSource = accTypes;
            colAccType.DisplayMember = "DisplayAccType";
            colAccType.ValueMember = "AccTypCd";
            dgvManageAccount.Columns.Add(colAccType);

            DataGridViewCheckBoxColumn colCusSup = new DataGridViewCheckBoxColumn();
            colCusSup.Name = "cusSup";
            colCusSup.HeaderText = "Cus & Sup";
            colCusSup.DataPropertyName = "cusSup";
            dgvManageAccount.Columns.Add(colCusSup);

            DataGridViewCheckBoxColumn colCtrlAcc = new DataGridViewCheckBoxColumn();
            colCtrlAcc.Name = "ctrlAcc";
            colCtrlAcc.HeaderText = "CtrlAcc";
            colCtrlAcc.DataPropertyName = "ctrlAcc";
            dgvManageAccount.Columns.Add(colCtrlAcc);

            DataGridViewComboBoxColumn colCtrlAccCode = new DataGridViewComboBoxColumn();//not set data load for this
            colCtrlAccCode.Name = "ctrlAccCode";
            colCtrlAccCode.HeaderText = "Ctrl Acc Code";
            colCtrlAccCode.DataPropertyName = "ctrlAccCode";
            colCtrlAccCode.DataSource = ctrlAccCd;
            colCtrlAccCode.DisplayMember = "CrnNm";
            colCtrlAccCode.ValueMember = "CrnCd";
            dgvManageAccount.Columns.Add(colCtrlAccCode);

            DataGridViewComboBoxColumn colBankCode = new DataGridViewComboBoxColumn();
            colBankCode.Name = "bankCode";
            colBankCode.HeaderText = "Bank Code";
            colBankCode.DataPropertyName = "bankCode";
            colBankCode.DataSource = bankCodes;
            colBankCode.DisplayMember = "DisplayBankCode";
            colBankCode.ValueMember = "BnkCd";
            dgvManageAccount.Columns.Add(colBankCode);

            DataGridViewTextBoxColumn colBranchCode = new DataGridViewTextBoxColumn();
            colBranchCode.Name = "branchCode";
            colBranchCode.HeaderText = "Branch Code";
            colBranchCode.DataPropertyName = "branchCode";
            dgvManageAccount.Columns.Add(colBranchCode);

            DataGridViewTextBoxColumn colAccNo = new DataGridViewTextBoxColumn();
            colAccNo.Name = "accNo";
            colAccNo.HeaderText = "Acc No";
            colAccNo.DataPropertyName = "accNo";
            dgvManageAccount.Columns.Add(colAccNo);

            DataGridViewComboBoxColumn colBUnit = new DataGridViewComboBoxColumn();
            colBUnit.Name = "bUnit";
            colBUnit.HeaderText = "B.Unit";
            colBUnit.DataPropertyName = "bUnit";
            colBUnit.DataSource = bUnits;
            colBUnit.DisplayMember = "BUNm";
            colBUnit.ValueMember = "BUCd";
            dgvManageAccount.Columns.Add(colBUnit);

            DataGridViewTextBoxColumn colCrnCd = new DataGridViewTextBoxColumn();
            colCrnCd.Name = "crnCd";
            colCrnCd.HeaderText = "CrnCd";
            colCrnCd.DataPropertyName = "crnCd";
            dgvManageAccount.Columns.Add(colCrnCd);

            DataGridViewTextBoxColumn colCrLmt = new DataGridViewTextBoxColumn();
            colCrLmt.Name = "crLmt";
            colCrLmt.HeaderText = "Cr.Limit";
            colCrLmt.DataPropertyName = "crLmt";
            dgvManageAccount.Columns.Add(colCrLmt);

            DataGridViewTextBoxColumn colCrDays = new DataGridViewTextBoxColumn();
            colCrDays.Name = "crDays";
            colCrDays.HeaderText = "Cr.Days";
            colCrDays.DataPropertyName = "crDays";
            dgvManageAccount.Columns.Add(colCrDays);

            DataGridViewCheckBoxColumn colMltiAdr = new DataGridViewCheckBoxColumn();
            colMltiAdr.Name = "mltiAdr";
            colMltiAdr.HeaderText = "Multi Adr";
            colMltiAdr.DataPropertyName = "mltiAdr";
            dgvManageAccount.Columns.Add(colMltiAdr);

            DataGridViewCheckBoxColumn colInAct = new DataGridViewCheckBoxColumn();
            colInAct.Name = "inAct";
            colInAct.HeaderText = "InAct";
            colInAct.DataPropertyName = "inAct";
            dgvManageAccount.Columns.Add(colInAct);

            DataGridViewTextBoxColumn colDel = new DataGridViewTextBoxColumn();
            colDel.Name = "del";
            colDel.HeaderText = "Delete";
            colDel.DataPropertyName = "del";
            dgvManageAccount.Columns.Add(colDel);

            DataGridViewTextBoxColumn colUpdate= new DataGridViewTextBoxColumn();
            colUpdate.Name = "update";
            colUpdate.HeaderText = "Update";
            colUpdate.DataPropertyName = "update";
            dgvManageAccount.Columns.Add(colUpdate);

            DataGridViewCheckBoxColumn colBlkLst = new DataGridViewCheckBoxColumn();
            colBlkLst.Name = "blockList";
            colBlkLst.HeaderText = "Block List";
            colBlkLst.DataPropertyName = "blockList";
            dgvManageAccount.Columns.Add(colBlkLst);

            foreach (DataGridViewColumn col in dgvManageAccount.Columns)
            {
                if (col is DataGridViewComboBoxColumn)
                {
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                else
                {
                    col.SortMode = DataGridViewColumnSortMode.Automatic;
                }
            }

        }

        private void LoadData()
        {
            dgvManageAccount.Rows.Clear();
            try
            {
                using (SqlConnection conn = DBConnectionManager.GetConnection())
                {
                    conn.Open();
                    query = "Select AccKy, AccCd, AccNm, AccTypCd, fCusSup, fCtrlAcc, CtrlAccCd, BnkCd, BrnCd, BnkAccNo, BuCd, CrnCd, CrLmt, CrDays, fMultiAdr, fInact, fBlckList From vewMngAccVsf Order By AccCd";
                    SqlCommand cmd  = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string accTypeName = reader["AccTypCd"] == DBNull.Value? null : reader["AccTypCd"].ToString();
                        object accTypeKey = DBNull.Value;
                        if(!string.IsNullOrEmpty(accTypeName))
                        {
                            DataRow[] matchingAccTypeRow = accTypes.Select($"AccTypCd = '{accTypeName.Replace("'", "''")}'");
                            if (matchingAccTypeRow.Length > 0 )
                            {
                                accTypeKey = matchingAccTypeRow[0]["AccTypCd"];
                            }
                        }




                        int rowIndex = dgvManageAccount.Rows.Add();
                        DataGridViewRow row = dgvManageAccount.Rows[rowIndex];

                        row.Cells["accKey"].Value = reader["AccKy"].ToString();
                        row.Cells["accCode"].Value = reader["AccCd"].ToString();
                        row.Cells["accName"].Value = reader["AccNm"].ToString();
                        row.Cells["accType"].Value = accTypeKey;
                        row.Cells["cusSup"].Value = Convert.ToBoolean(reader["fCusSup"]);
                        row.Cells["ctrlAcc"].Value = Convert.ToBoolean(reader["fCtrlAcc"]);
                        row.Cells["bankCode"].Value = reader["BnkCd"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //------------------------------------------------------------------------------------------------------------------
        private void frmManageAccounts_Load(object sender, EventArgs e)
        {
            LoadComboBoxAccType();
            LoadComboboxBankCode();
            LoadComboBoxBUnit();
            LoadComboBoxCtrlAccCd();
            SetupDgvAccountManage();
            LoadData();
        }
    }
}
