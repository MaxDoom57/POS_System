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
    public partial class frmTransactionsConfirmationSetup : Form
    {
        private int childFormNumber = 0;

        public frmTransactionsConfirmationSetup()
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


        //Variables and functions are define here ----------------------------------------------------------------------------------------------
        bool isDGVEdit = false;
        string query;

        private void SetupDgvConfirmationDate()
        {
            dgvConfirmationDate.Columns.Clear();

            DataGridViewTextBoxColumn colCompanyKey = new DataGridViewTextBoxColumn();
            colCompanyKey.Name = "colCompanyKey";
            colCompanyKey.HeaderText = "company Key";
            colCompanyKey.Visible = false;
            dgvConfirmationDate.Columns.Add(colCompanyKey);

            DataGridViewTextBoxColumn colCompay = new DataGridViewTextBoxColumn();
            colCompay.Name = "colCompany";
            colCompay.HeaderText = "Company";
            colCompay.ReadOnly = true;
            dgvConfirmationDate.Columns.Add(colCompay);

            DataGridViewTextBoxColumn colConfirmedDate = new DataGridViewTextBoxColumn();
            colConfirmedDate.Name = "colConfirmedDate";
            colConfirmedDate.HeaderText = "ConfirmedDate";
            dgvConfirmationDate.Columns.Add(colConfirmedDate);
        }
        private void SetupDgvRollingDate ()
        {
            dgvRollingDate.Columns.Clear();

            DataGridViewTextBoxColumn colCdKy = new DataGridViewTextBoxColumn();
            colCdKy.Name = "colCdKy";
            colCdKy.HeaderText = "CdKy";
            colCdKy.Visible = false;
            dgvRollingDate.Columns.Add (colCdKy);

            DataGridViewTextBoxColumn colNo = new DataGridViewTextBoxColumn();
            colNo.Name = "colNo";
            colNo.HeaderText = "";
            colNo.FillWeight = 30;
            colNo.ReadOnly = true;
            dgvRollingDate .Columns.Add(colNo);

            DataGridViewTextBoxColumn colTrnType = new DataGridViewTextBoxColumn();
            colTrnType.Name = "colTrnType";
            colTrnType.HeaderText = "Transaction Type";
            colTrnType.ReadOnly = true;
            colTrnType.FillWeight = 150;
            dgvRollingDate.Columns.Add(colTrnType);

            DataGridViewTextBoxColumn colDaysBackward = new DataGridViewTextBoxColumn();
            colDaysBackward.Name = "colDaysBackward";
            colDaysBackward.HeaderText = "# Days Backward";
            colDaysBackward.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvRollingDate.Columns.Add(colDaysBackward);

            DataGridViewTextBoxColumn colDaysForward = new DataGridViewTextBoxColumn();
            colDaysForward.Name = "colDaysForward";
            colDaysForward.HeaderText = "# Days Forward";
            colDaysForward.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvRollingDate.Columns.Add(colDaysForward);
        }

        private void LoadDgvConfirmationDate()
        {
            try
            {
                dgvConfirmationDate.Rows.Clear ();
                dgvConfirmationDate.AllowUserToAddRows = false;

                query = "SELECT CNm,TrnCnfDt,CKy FROM Company";
                using (SqlConnection conn = DBConnectionManager.GetConnection ())
                {
                    conn.Open ();
                    SqlCommand cmd = new SqlCommand (query, conn);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read ())
                        {
                            dgvConfirmationDate.Rows.Add(reader["CKy"],reader["CNm"], reader["TrnCnfDt"]);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void LoadDgvRollingDate()
        {
            try
            {
                int rowNo = 1;
                dgvRollingDate.Rows.Clear();
                dgvRollingDate.AllowUserToAddRows = false;

                query = "SELECT CdNm,CdNo4,CdNo5,CdKy FROM CdMas Where ConCd = 'TrnTyp' And Cdf2 = 1 And CKy =@companyKey ORDER BY CdNm";
                using (SqlConnection conn = DBConnectionManager.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("companyKey", 1);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dgvRollingDate.Rows.Add(reader["CdKy"], rowNo,  reader["CdNm"], reader["CdNo4"], reader["CdNo5"]);
                            rowNo++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SaveDgvConfirmationDate()
        {
            using (SqlConnection conn = DBConnectionManager.GetConnection())
            {
                conn.Open();

                foreach (DataGridViewRow row in dgvConfirmationDate.Rows)
                {
                    if (row.IsNewRow) continue;

                    var ckyObj = row.Cells["colCompanyKey"].Value;
                    var newDateObj = row.Cells["colConfirmedDate"].Value;

                    if (ckyObj != null && newDateObj != null &&
                        int.TryParse(ckyObj.ToString(), out int cky) &&
                        DateTime.TryParse(newDateObj.ToString(), out DateTime newDate))
                    {
                        // Get current date from database
                        SqlCommand checkCmd = new SqlCommand("SELECT TrnCnfDt FROM Company WHERE Cky = @cky", conn);
                        checkCmd.Parameters.AddWithValue("@cky", cky);

                        object dbDateObj = checkCmd.ExecuteScalar();

                        if (dbDateObj != null && dbDateObj != DBNull.Value)
                        {
                            DateTime dbDate = Convert.ToDateTime(dbDateObj);

                            if (dbDate.Date != newDate.Date)
                            {
                                // Update only if dates differ
                                SqlCommand updateCmd = new SqlCommand(
                                    "UPDATE Company SET TrnCnfDt = @newDate WHERE Cky = @cky", conn);
                                updateCmd.Parameters.AddWithValue("@newDate", newDate);
                                updateCmd.Parameters.AddWithValue("@cky", cky);
                                updateCmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
        }
        private void SaveDgvRollingDate()
        {
            using (SqlConnection conn = DBConnectionManager.GetConnection())
            {
                conn.Open();

                foreach (DataGridViewRow row in dgvRollingDate.Rows)
                {
                    if (row.IsNewRow) continue;

                    int cdKy = Convert.ToInt32(row.Cells["colCdKy"].Value);
                    string daysBackward = row.Cells["colDaysBackward"].Value?.ToString() ?? "0";
                    string daysForward = row.Cells["colDaysForward"].Value?.ToString() ?? "0";

                    SqlCommand cmd = new SqlCommand("UPDATE CdMas SET CdNo4 = @backward, CdNo5 = @forward WHERE CdKy = @cdky", conn);
                    cmd.Parameters.AddWithValue("@backward", daysBackward);
                    cmd.Parameters.AddWithValue("@forward", daysForward);
                    cmd.Parameters.AddWithValue("@cdky", cdKy);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        //Events handle here --------------------------------------------------------------------------------------------------------------------
        private void tabTransactionConfirmation_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void frmTransactionsConfirmationSetup_Load(object sender, EventArgs e)
        {
            SetupDgvConfirmationDate();
            SetupDgvRollingDate();
            LoadDgvConfirmationDate();
            LoadDgvRollingDate();
        }

        private void dgvConfirmationDate_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            isDGVEdit = true;
        }

        private void dgvRollingDate_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            isDGVEdit = true;
        }

        private void frmTransactionsConfirmationSetup_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isDGVEdit)
            {
                DialogResult result = MessageBox.Show("Do you want to save changes?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        SaveDgvConfirmationDate();
                        SaveDgvRollingDate();
                        MessageBox.Show("Data saved successfully!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    e.Cancel = false;
                }
            }
        }

        private void dgvRollingDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;

                int col = dgvRollingDate.CurrentCell.ColumnIndex;
                int row = dgvRollingDate.CurrentCell.RowIndex;

                if (col < dgvRollingDate.Columns.Count - 1)
                {
                    dgvRollingDate.CurrentCell = dgvRollingDate.Rows[row].Cells[col + 1];
                }
                else if (row < dgvRollingDate.Rows.Count - 1)
                {
                    dgvRollingDate.CurrentCell = dgvRollingDate.Rows[row + 1].Cells[2];
                }
            }
        }

        //Keys handle here--------------------------------------------------------------------------------------------------------------------------------------

    }
}
