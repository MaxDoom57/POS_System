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
using CrystalDecisions.CrystalReports.Engine;

namespace POS_System.Forms
{
    public partial class frmCodesEntry : Form
    {
        private int childFormNumber = 0;

        public frmCodesEntry()
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

        //Functions are define here ------------------------------------------------------------------------------------------------------------
        string query;
        int codeTypeKey;
        int ConKy;
        bool isCellEdited = false;

        private void LoadUnitTypes()
        {
            try
            {
                using (SqlConnection conn = DBConnectionManager.GetConnection())
                {
                    conn.Open();
                    query = "Select ConNm from Control where fInAct = '0' and fUsrAcs = '1' Order By ConNm";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbCodeType.Items.Add(reader["ConNm"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool isCodeNameExist(string codeName)
        {
            try
            {
                using (SqlConnection conn = DBConnectionManager.GetConnection())
                {
                    conn.Open();
                    query = "Select ConNm from Control where fInAct = '0' and fUsrAcs = '1' Order By ConNm";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader["ConNm"].ToString().Equals(codeName, StringComparison.OrdinalIgnoreCase))
                            return true ;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool isCdKyExist (string CdKy, int codeTypeKey)
        {
            try
            {
                using (SqlConnection conn = DBConnectionManager.GetConnection())
                {
                    conn.Open();
                    query = "SELECT CdKy FROM CdMasQry WHERE ConKy = @codeTypeKey";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@codeTypeKey", codeTypeKey);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader["CdKy"].ToString().Equals(CdKy, StringComparison.OrdinalIgnoreCase))
                                return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private void SetupCodeTypeDataGridView()
        {
            dgvCodesEntry.Columns.Clear();

            DataGridViewTextBoxColumn colCodeTypeKey = new DataGridViewTextBoxColumn();
            colCodeTypeKey.Name = "colCodeTypeKey";
            colCodeTypeKey.HeaderText = "Code Type Key";
            colCodeTypeKey.Visible = false;
            dgvCodesEntry.Columns.Add(colCodeTypeKey);

            DataGridViewTextBoxColumn colCode = new DataGridViewTextBoxColumn();
            colCode.Name = "colCode";
            colCode.HeaderText = "Code";
            dgvCodesEntry.Columns.Add(colCode);

            DataGridViewTextBoxColumn colName = new DataGridViewTextBoxColumn();
            colName.Name = "colName";
            colName.HeaderText = "Name";
            dgvCodesEntry.Columns.Add(colName);

            DataGridViewCheckBoxColumn colDelete = new DataGridViewCheckBoxColumn();
            colDelete.Name = "colDelete";
            colDelete.HeaderText = "Delete";
            dgvCodesEntry.Columns.Add(colDelete);

            foreach (DataGridViewColumn col in dgvCodesEntry.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.Automatic;
            }
        }

        private void LoadCodeTypesDataGridView (int codeTypeKey)
        {
            dgvCodesEntry.Rows.Clear();
            try
            {
                using (SqlConnection conn = DBConnectionManager.GetConnection())
                {
                    conn.Open();
                    query = "Select Code, CdNm, CdKy, fInAct from CdMasQry where ConKy = @codeTypeKey And CKy = @companyKey";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@codeTypeKey", codeTypeKey);
                    cmd.Parameters.AddWithValue("@companyKey", 1);//UserSession.Instance.CompanyKey
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dgvCodesEntry.Rows.Add(
                                reader["CdKy"],
                                reader["Code"],
                                reader["CdNm"],
                                Convert.ToBoolean(reader["fInAct"])
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        //Events are handle here ---------------------------------------------------------------------------------------------------------------
        private void frmCodesEntry_Load(object sender, EventArgs e)
        {
            LoadUnitTypes();
            SetupCodeTypeDataGridView();

        }

        private void cmbCodeType_TextChanged(object sender, EventArgs e)
        {
            if (isCodeNameExist(cmbCodeType.Text))
            {
                //get conKy related to the codeType
                try
                {
                    using (SqlConnection conn = DBConnectionManager.GetConnection())
                    {
                        conn.Open();
                        query = "Select ConKy, ConCd from Control where ConNm = @codeType";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@codeType",cmbCodeType.Text);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            reader.Read();
                            codeTypeKey = Convert.ToInt32(reader["ConKy"]);
                            ConKy = Convert.ToInt32(reader["ConKy"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                LoadCodeTypesDataGridView(codeTypeKey);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cmbCodeType.Text = "";
            cmbCodeType.Focus();
            dgvCodesEntry.Rows.Clear();
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvCodesEntry.CurrentRow != null && !dgvCodesEntry.CurrentRow.IsNewRow)
            {
                DialogResult result = MessageBox.Show("Do you want to DELETE this line?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    int rowIndex = dgvCodesEntry.CurrentRow.Index;

                    dgvCodesEntry.Rows[rowIndex].Cells[3].Value = 1;
                }
            }
        }

        private void dgvCodesEntry_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            isCellEdited = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbCodeType.Text == "")
            {
                return;
            }
            else
            {
                if(isCellEdited)
                {
                    foreach (DataGridViewRow row in dgvCodesEntry.Rows)
                    {
                        if (row.IsNewRow) continue;

                        if (isCdKyExist(row.Cells["colCodeTypeKey"].Value.ToString(), codeTypeKey))
                        {
                            try
                            {
                                using (SqlConnection conn = DBConnectionManager.GetConnection())
                                {
                                    conn.Open();
                                    string updateQuery = @"UPDATE CdMas 
                                                           SET Code = @Code, CdNm = @CdNm, FinAct = @FinAct 
                                                           WHERE CdKy = @CdKy";
                                    using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                                    {
                                        updateCmd.Parameters.AddWithValue("@Code", row.Cells["colCode"].Value.ToString());
                                        updateCmd.Parameters.AddWithValue("@CdNm", row.Cells["colName"].Value.ToString());
                                        updateCmd.Parameters.AddWithValue("@FinAct", Convert.ToBoolean(row.Cells["colDelete"].Value) ? 1 : 0);
                                        updateCmd.Parameters.AddWithValue("@CdKy", row.Cells["colCodeTypeKey"].Value.ToString());
                                        updateCmd.ExecuteNonQuery();
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Update Error: " + ex.Message);
                            }
                        }
                        else
                        {
                            try
                            {
                                using (SqlConnection conn = DBConnectionManager.GetConnection())
                                {
                                    conn.Open();
                                    string insertQuery = @"INSERT INTO CdMas (CKy, Code, CdNm, ConCd, ConKy) 
                                                            VALUES (@CKy, @Code, @CdNm, @ConCd, @ConKy)";
                                    using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                                    {
                                        insertCmd.Parameters.AddWithValue("@CKy", UserSession.Instance.CompanyKey);
                                        insertCmd.Parameters.AddWithValue("@Code", row.Cells["colCode"].Value?.ToString() ?? "");
                                        insertCmd.Parameters.AddWithValue("@CdNm", row.Cells["colName"].Value?.ToString() ?? "");
                                        insertCmd.Parameters.AddWithValue("@ConCd", ConKy);
                                        insertCmd.Parameters.AddWithValue("@ConKy", codeTypeKey); // assuming you have this variable set
                                        insertCmd.ExecuteNonQuery();
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Insert Error: " + ex.Message);
                            }

                        }
                    }
                    btnCancel_Click(sender, e);
                }
            }
        }


        //Keys are handle here -----------------------------------------------------------------------------------------------------------------

        private void cmbCodeType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dgvCodesEntry.Focus();
            }
        }

        private void dgvCodesEntry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;

                int col = dgvCodesEntry.CurrentCell.ColumnIndex;
                int row = dgvCodesEntry.CurrentCell.RowIndex;

                if (col < dgvCodesEntry.Columns.Count - 1)
                {
                    dgvCodesEntry.CurrentCell = dgvCodesEntry.Rows[row].Cells[col + 1];
                }
                else if (row < dgvCodesEntry.Rows.Count - 1)
                {
                    dgvCodesEntry.CurrentCell = dgvCodesEntry.Rows[row + 1].Cells[1];
                }
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            query = "SELECT * FROM CdMasRptQry WHERE ConNm = @ConNm";

            DataTable dt = new DataTable();
            using (SqlConnection conn = DBConnectionManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ConNm", cmbCodeType.Text);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            ReportDocument report = new ReportDocument();
            report.Load(@"E:\POS System\Reports\rptCodeEntry.rpt");

            report.SetDataSource(dt);
            report.SetParameterValue("CNm", UserSession.Instance.CompanyName);   //UserSession.Instance.CompanyName
            report.SetParameterValue("curUsrID", UserSession.Instance.UserId);   //UserSession.Instance.UserId

            frmReportViewer frmReportViewer = new frmReportViewer();
            frmReportViewer.LoadCodeEntryReport(report);
            frmReportViewer.Show();
        }
    }
}
