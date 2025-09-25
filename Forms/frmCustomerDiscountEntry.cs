using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using POS_System.Classes;

namespace POS_System.Forms
{
    public partial class frmCustomerDiscountEntry : Form
    {
        private int childFormNumber = 0;

        public frmCustomerDiscountEntry()
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

        //--------------------------------------------------------------------------------------------------------------------
        string query;
        int itemTypeKey, addressPriceCategoryKey, itemAddressRelTypeKey;

        private void LoadItemTypeCombo()
        {
            using (SqlConnection conn = DBConnectionManager.GetConnection())
            {
                conn.Open();
                query = "Select ItmTypKy,ItmTypCd from vewItmTypCd Order By ItmTypCd";
                SqlCommand cmd = new SqlCommand(query, conn);
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cmbItemType.Items.Add(reader["ItmTypCd"]);
                    }
                }
                conn.Close();
            }
        }

        private void LoadAddressPriceTypeCombo()
        {
            using (SqlConnection conn = DBConnectionManager.GetConnection())
            {
                conn.Open();
                query = "Select AdrPriCatKy,AdrPriCatCd from vewAdrPriCatCd";
                SqlCommand cmd = new SqlCommand(query, conn);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cmbAddressPriceCategory.Items.Add(reader["AdrPriCatCd"]);
                    }
                }
                conn.Close();
            }
        }

        private void LoadAddressRelTypeCombo()
        {
            using (SqlConnection conn = DBConnectionManager.GetConnection())
            {
                conn.Open();
                query = "Select ItmAdrPriCatTypKy,ItmAdrPriCatTypCd from vewItmAdrPriCatTypCd";
                SqlCommand cmd = new SqlCommand(query, conn);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cmbAddressRelType.Items.Add(reader["ItmAdrPriCatTypCd"]);
                    }
                }
                conn.Close();
            }
        }

        private void SetupDataGridview()
        {
            dgvDiscountEntry.Columns.Clear();

            DataGridViewTextBoxColumn colItemKey = new DataGridViewTextBoxColumn();
            colItemKey.Name = "itemKey";
            colItemKey.HeaderText = "Item Key";
            colItemKey.ReadOnly = true;
            colItemKey.Visible = false;
            dgvDiscountEntry.Columns.Add(colItemKey);

            DataGridViewTextBoxColumn colItmAdrPriCatKy = new DataGridViewTextBoxColumn();
            colItmAdrPriCatKy.Name = "itmAdrPriCatKy";
            colItmAdrPriCatKy.HeaderText = "ItmAdrPriCatKy";
            colItmAdrPriCatKy.ReadOnly = true;
            colItmAdrPriCatKy.Visible = false;
            dgvDiscountEntry.Columns.Add(colItmAdrPriCatKy);

            DataGridViewTextBoxColumn colUpdate = new DataGridViewTextBoxColumn();
            colUpdate.Name = "update";
            colUpdate.HeaderText = "Update";
            colUpdate.Visible = false;
            dgvDiscountEntry.Columns.Add(colUpdate);

            DataGridViewTextBoxColumn colDelete = new DataGridViewTextBoxColumn();
            colDelete.Name = "delete";
            colDelete.HeaderText = "Delete";
            colDelete.Visible = false;
            dgvDiscountEntry.Columns.Add(colDelete);

            DataGridViewTextBoxColumn colItemCode = new DataGridViewTextBoxColumn();
            colItemCode.Name = "itemCode";
            colItemCode.HeaderText = "Item Code";
            colItemCode.ReadOnly = true;
            colItemCode.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colItemCode.Width = 65;
            dgvDiscountEntry.Columns.Add(colItemCode);

            DataGridViewTextBoxColumn colItemName = new DataGridViewTextBoxColumn();
            colItemName.Name = "itemName";
            colItemName.HeaderText = "Item Name";
            colItemName.ReadOnly = true;
            colItemName.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colItemName.Width = 200;
            dgvDiscountEntry.Columns.Add(colItemName);

            DataGridViewTextBoxColumn colCosPrice = new DataGridViewTextBoxColumn();
            colCosPrice.Name = "cosPrice";
            colCosPrice.HeaderText = "Cost price";
            colCosPrice.ReadOnly = true;
            colCosPrice.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colCosPrice.DefaultCellStyle.Format = "N2";
            dgvDiscountEntry.Columns.Add(colCosPrice);

            DataGridViewTextBoxColumn colSalePrice = new DataGridViewTextBoxColumn();
            colSalePrice.Name = "salePrice";
            colSalePrice.HeaderText = "Sale Price";
            colSalePrice.ReadOnly = true;
            colSalePrice.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colSalePrice.DefaultCellStyle.Format = "N2";
            dgvDiscountEntry.Columns.Add(colSalePrice);

            DataGridViewTextBoxColumn colValue = new DataGridViewTextBoxColumn();
            colValue.Name = "value";
            colValue.HeaderText = "Value";
            dgvDiscountEntry.Columns.Add(colValue);

            DataGridViewTextBoxColumn colUnit = new DataGridViewTextBoxColumn();
            colUnit.Name = "unit";
            colUnit.HeaderText = "Unit";
            colUnit.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colUnit.Width = 50;
            colUnit.ReadOnly = true;
            dgvDiscountEntry.Columns.Add(colUnit);

            foreach (DataGridViewColumn col in dgvDiscountEntry.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.Automatic;
            }

        }

        private void LoadData()
        {
            int CompanyKey = 1; //UserSession.Instance.CompanyKeys
            try
            {
                //Get item type key selected in combobox
                if (!string.IsNullOrEmpty(cmbItemType.Text))
                {
                    using (SqlConnection conn = DBConnectionManager.GetConnection())
                    {
                        conn.Open();
                        query = "Select ItmTypKy from vewItmTypCd where ItmTypCd = @itemType";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@itemType", cmbItemType.Text);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                itemTypeKey = Convert.ToInt32(reader["ItmTypKy"]);
                            }
                        }
                        conn.Close();
                    }
                }
                else
                {
                    itemTypeKey= 0;
                }

                //Get address price category key selected in combobox
                if (!string.IsNullOrEmpty(cmbAddressPriceCategory.Text))
                {
                    using (SqlConnection conn = DBConnectionManager.GetConnection())
                    {
                        conn.Open();
                        query = "Select AdrPriCatKy from vewAdrPriCatCd WHERE AdrPriCatCd = @addressPriceCategory";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@addressPriceCategory", cmbAddressPriceCategory.Text);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                addressPriceCategoryKey = Convert.ToInt32(reader["AdrPriCatKy"]);
                            }
                        }
                        conn.Close();
                    }
                }
                else
                {
                    addressPriceCategoryKey = 0;
                }

                //Get address rel type type key selected in combobox
                if (!string.IsNullOrEmpty(cmbAddressPriceCategory.Text))
                {
                    using (SqlConnection conn = DBConnectionManager.GetConnection())
                    {
                        conn.Open();
                        query = "Select ItmAdrPriCatTypKy from vewItmAdrPriCatTypCd WHERE ItmAdrPriCatTypCd = @addressRelType";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@addressRelType", cmbAddressRelType.Text);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                itemAddressRelTypeKey = Convert.ToInt32(reader["ItmAdrPriCatTypKy"]);
                            }
                        }
                        conn.Close();
                    }
                }
                else
                {
                    itemAddressRelTypeKey = 0;
                }

                using (SqlConnection conn = DBConnectionManager.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sprItmAdrPriCatDtUpdt", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@pCKy", CompanyKey);
                        cmd.Parameters.AddWithValue("@pItmAdrPriCatTypKy", itemAddressRelTypeKey);
                        cmd.Parameters.AddWithValue("@pAdrPriCatKy", addressPriceCategoryKey);
                        cmd.Parameters.AddWithValue("@pItmTypKy", itemTypeKey);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            dgvDiscountEntry.Rows.Clear();

                            while (reader.Read())
                            {
                                dgvDiscountEntry.Rows.Add(
                                    reader["ItmKy"],
                                    reader["ItmAdrPriCatDtKy"],
                                    0,
                                    0,
                                    reader["ItmCd"],
                                    reader["ItmNm"],
                                    reader["CosPri"],
                                    reader["SlsPri"],
                                    reader["RelVal"],
                                    reader["Unit"]
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }

        //--------------------------------------------------------------------------------------------------------------------

        private void frmCustomerDiscountEntry_Load(object sender, EventArgs e)
        {
            LoadItemTypeCombo();
            LoadAddressPriceTypeCombo();
            LoadAddressRelTypeCombo();
            SetupDataGridview();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dgvDiscountEntry_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int updatedRowValue = e.RowIndex;
            if (updatedRowValue >= 0)
            {
                dgvDiscountEntry.Rows[updatedRowValue].Cells["update"].Value = 1;
            }
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cmbAddressPriceCategory.Text))
            {
                MessageBox.Show("Address Price Category is a must");
                cmbAddressPriceCategory.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(cmbAddressRelType.Text))
            {
                MessageBox.Show("Item Address Relation Type is a must");
                cmbAddressRelType.Focus();
                return;
            }

            try
            {
                using (SqlConnection conn = DBConnectionManager.GetConnection())
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            foreach (DataGridViewRow row in dgvDiscountEntry.Rows)
                            {
                                if (row.IsNewRow) continue;

                                var updateFlag = row.Cells["update"].Value;
                                if (updateFlag == null || Convert.ToInt32(updateFlag) != 1) continue;

                                string itmAdrPriCatDtKy = row.Cells["itmAdrPriCatKy"].Value?.ToString();
                                string itmKy = row.Cells["itemKey"].Value?.ToString();
                                string relVal = row.Cells["value"].Value?.ToString();

                                if (string.IsNullOrWhiteSpace(itmKy) || string.IsNullOrWhiteSpace(relVal))
                                    continue; 

                                string query = "";

                                bool isUpdate = !string.IsNullOrEmpty(itmAdrPriCatDtKy);

                                if (isUpdate)
                                {
                                    query = @"UPDATE ItmAdrPriCatDt 
                                      SET AdrPriCatKy = @AdrPriCatKy,
                                          ItmKy = @ItmKy,
                                          ItmAdrPriCatTypKy = @ItmAdrPriCatTypKy,
                                          RelVal = @RelVal
                                      WHERE ItmAdrPriCatDtKy = @ItmAdrPriCatDtKy";
                                }
                                else
                                {
                                    query = @"INSERT INTO ItmAdrPriCatDt 
                                      (AdrPriCatKy, ItmKy, ItmAdrPriCatTypKy, RelVal, EftvDt)
                                      VALUES 
                                      (@AdrPriCatKy, @ItmKy, @ItmAdrPriCatTypKy, @RelVal, '1900-01-01')";
                                }

                                using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@AdrPriCatKy", addressPriceCategoryKey);
                                    cmd.Parameters.AddWithValue("@ItmKy", itmKy);
                                    cmd.Parameters.AddWithValue("@ItmAdrPriCatTypKy", itemAddressRelTypeKey);
                                    cmd.Parameters.AddWithValue("@RelVal", relVal);

                                    if (isUpdate)
                                        cmd.Parameters.AddWithValue("@ItmAdrPriCatDtKy", itmAdrPriCatDtKy);

                                    cmd.ExecuteNonQuery();
                                }
                            }

                            transaction.Commit();
                            MessageBox.Show("Save Successful.");
                            dgvDiscountEntry.Rows.Clear();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Failed: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cmbItemType.Text = "";
            cmbAddressPriceCategory.Text = "";
            cmbAddressRelType.Text = "";

            dgvDiscountEntry.Rows.Clear();
        }


        //--------------------------------------------------------------------------------------------------------------------
        private void cmbItemType_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                cmbAddressPriceCategory.Focus();
            }
        }

        private void dgvDiscountEntry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;

                int col = dgvDiscountEntry.CurrentCell.ColumnIndex;
                int row = dgvDiscountEntry.CurrentCell.RowIndex;

                if (col < dgvDiscountEntry.Columns.Count - 1)
                {
                    dgvDiscountEntry.CurrentCell = dgvDiscountEntry.Rows[row].Cells[col + 1];
                }
                else if (row < dgvDiscountEntry.Rows.Count - 1)
                {
                    dgvDiscountEntry.CurrentCell = dgvDiscountEntry.Rows[row + 1].Cells[4];
                }
            }
        }

        private void cmbAddressPriceCategory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbAddressRelType.Focus();
            }
        }

        private void cmbAddressRelType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnRefresh.Focus();
            }
        }

       

    }
}
