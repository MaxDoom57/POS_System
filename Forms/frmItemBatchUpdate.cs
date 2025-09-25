using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using POS_System.Classes;

namespace POS_System.Forms
{
    public partial class frmItemBatchUpdate : Form
    {
        public frmItemBatchUpdate()
        {
            InitializeComponent();
        }

        //--------------------------------------------------------------------------------------------------------------------------------------
        //private void LoadItemCodes ()
        //{
        //    using(SqlConnection conn = DBConnectionManager.GetConnection())
        //    {
        //        conn.Open ();
        //        SqlCommand cmd = new SqlCommand("Select ItmCd,ItmNm,ItmKy From vewItmCd order By ItmCd", conn);
        //        using(SqlDataReader reader = cmd.ExecuteReader())
        //        {
        //            while (reader.Read ())
        //            {
        //                cmbItemCode.Items.Add(reader["ItmCd"].ToString ());
        //            }
        //        }
        //    }
        //}

        //private void LoadGenaricNames()
        //{
        //    using (SqlConnection conn = DBConnectionManager.GetConnection())
        //    {
        //        conn.Open();
        //        SqlCommand cmd = new SqlCommand("Select ItmCd,PartNo,ItmKy From ItmMas Order By PartNo", conn);
        //        using (SqlDataReader reader = cmd.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                cmbGenaricName.Items.Add(reader["PartNo"].ToString());
        //            }
        //        }
        //    }
        //}

        //private void LoadItemNames()
        //{
        //    using (SqlConnection conn = DBConnectionManager.GetConnection())
        //    {
        //        conn.Open();
        //        SqlCommand cmd = new SqlCommand("Select ItmCd,ItmNm,ItmKy From vewItmNm Order By ItmNm", conn);
        //        using (SqlDataReader reader = cmd.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                cmbItemName.Items.Add(reader["ItmNm"].ToString());
        //            }
        //        }
        //    }
        //}

        DataTable itemTable = new DataTable();

        public class ItemDetails
        {
            public string itemCode {  get; set; }
            public string genaricName { get; set; }
            public string itemName { get; set; }
            public int itemKey { get; set; }

            public override string ToString()
            {
                return itemCode;
            }
        }

        private void LoadAllItems()
        {
            using (SqlConnection conn = DBConnectionManager.GetConnection())
            {
                conn.Open();
                string query = "SELECT ItmCd, ItmNm, PartNo, ItmKy FROM ItmMas ORDER BY ItmCd";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    itemTable.Clear();
                    adapter.Fill(itemTable);
                }
            }

            cmbItemCode.DataSource = itemTable;
            cmbItemCode.DisplayMember = "ItmCd";
            cmbItemCode.ValueMember = "ItmKy";

            cmbItemName.DataSource = itemTable.Copy();
            cmbItemName.DisplayMember = "ItmNm";
            cmbItemName.ValueMember = "ItmKy";

            cmbGenaricName.DataSource = itemTable;
            cmbGenaricName.DisplayMember = "PartNo";
            cmbGenaricName.ValueMember = "ItmKy";

            cmbItemCode.SelectedIndex = -1;
            cmbItemName.SelectedIndex = -1;
            cmbGenaricName.SelectedIndex = -1;
        }

        private void SetupDgvFormat()
        {
            dgvItemUpdate.Rows.Clear();

            DataGridViewTextBoxColumn colItemKey = new DataGridViewTextBoxColumn();
            colItemKey.Name = "colItemKey";
            colItemKey.HeaderText = "Item Key";
            colItemKey.Visible = false;
            colItemKey.ReadOnly = true;
            dgvItemUpdate.Columns.Add(colItemKey);

            DataGridViewTextBoxColumn colItemBatchKey = new DataGridViewTextBoxColumn();
            colItemBatchKey.Name = "colItemBatchKey";
            colItemBatchKey.HeaderText = "Item Batch Key";
            colItemBatchKey.Visible = false;
            colItemBatchKey.ReadOnly = true;
            dgvItemUpdate.Columns.Add(colItemBatchKey);

            DataGridViewTextBoxColumn colBatchNo = new DataGridViewTextBoxColumn();
            colBatchNo.Name = "colBatchNo";
            colBatchNo.HeaderText = "Batch No";
            dgvItemUpdate.Columns.Add(colBatchNo);

            DataGridViewTextBoxColumn colExpiryDate = new DataGridViewTextBoxColumn();
            colExpiryDate.Name = "colExpiryDate";
            colExpiryDate.HeaderText = "Expiry Date";
            colExpiryDate.DefaultCellStyle.Format = "dd/MM/yyyy";
            colExpiryDate.Width = 110;
            dgvItemUpdate.Columns.Add(colExpiryDate);

            DataGridViewTextBoxColumn colCostPrice = new DataGridViewTextBoxColumn();
            colCostPrice.Name = "colCostPrice";
            colCostPrice.HeaderText = "Cost Price";
            colCostPrice.DefaultCellStyle.Format = "N2";
            colCostPrice.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvItemUpdate.Columns.Add(colCostPrice);

            DataGridViewTextBoxColumn colSalesPrice = new DataGridViewTextBoxColumn();
            colSalesPrice.Name = "colSalesPrice";
            colSalesPrice.HeaderText = "Sales Price";
            colSalesPrice.DefaultCellStyle.Format = "N2";
            colSalesPrice.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colSalesPrice.Width = 110;
            dgvItemUpdate.Columns.Add(colSalesPrice);

            DataGridViewTextBoxColumn colQuantity = new DataGridViewTextBoxColumn();
            colQuantity.Name = "colQuantity";
            colQuantity.HeaderText = "Quantity";
            dgvItemUpdate.Columns.Add(colQuantity);
        }

        //--------------------------------------------------------------------------------------------------------------------------------------


        private void frmItemBatchUpdate_Load(object sender, EventArgs e)
        {
            SetupDgvFormat();
            LoadAllItems();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cmbItemCode.SelectedIndex = -1;
            cmbItemName.SelectedIndex = -1;
            cmbGenaricName.SelectedIndex = -1;
            dgvItemUpdate.Rows.Clear();
            cmbItemCode.Focus();
        }

        private void cmbItemCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbItemCode.SelectedValue != null && cmbItemCode.SelectedValue is int )
            {
                cmbGenaricName.SelectedValue = cmbItemCode.SelectedValue;
                cmbItemName.SelectedValue = cmbItemCode.SelectedValue;
            }
        }

        private void cmbGenaricName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbGenaricName.SelectedValue != null && cmbGenaricName.SelectedValue is int)
            {
                cmbItemCode.SelectedValue = cmbGenaricName.SelectedValue;
                cmbItemName.SelectedValue = cmbGenaricName.SelectedValue;
            }
        }

        private void cmbItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbItemName.SelectedValue != null && cmbItemName.SelectedValue is int)
            {
                cmbGenaricName.SelectedValue = cmbItemName.SelectedValue;
                cmbItemCode.SelectedValue = cmbItemName.SelectedValue;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (cmbItemCode.Text != null)
            {
                try
                {
                    dgvItemUpdate.Rows.Clear();
                    using (SqlConnection conn = DBConnectionManager.GetConnection())
                    {
                        conn.Open();
                        string query = "Select ItmKy,ItmBatchKy,BatchNo,ExpirDt,CosPri,SalePri,Qty from vewItmBatchRpt Where ItmKy=@itemKey And Qty<>0 Order By ExpirDt";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@itemKey", Convert.ToInt32(cmbItemCode.SelectedValue));
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    dgvItemUpdate.Rows.Add(
                                        reader["ItmKy"],
                                        reader["ItmBatchKy"],
                                        reader["BatchNo"],
                                        Convert.ToDateTime(reader["ExpirDt"]).ToString("dd/MM/yyyy"),
                                        Convert.ToDecimal(reader["CosPri"]).ToString("N2"),
                                        Convert.ToDecimal(reader["SalePri"]).ToString("N2"),
                                        reader["Qty"]
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
        }

        private void cmbItemCode_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = cmbItemCode.Text != "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int batchKey, itemKey;
                string batchNo;
                float costPrice, salesPrice, quantity;
                DateTime expiryDate;

                itemKey = Convert.ToInt32(cmbItemCode.SelectedValue);

                using (SqlConnection conn = DBConnectionManager.GetConnection())
                {
                    conn.Open();

                    foreach (DataGridViewRow row in dgvItemUpdate.Rows)
                    {
                        if (row.IsNewRow) continue;

                        string expiryDateText = Convert.ToString(row.Cells["colExpiryDate"].Value);
                        string quantityText = Convert.ToString(row.Cells["colQuantity"].Value);

                        if (!string.IsNullOrWhiteSpace(expiryDateText) || !string.IsNullOrWhiteSpace(quantityText))
                        {
                            batchKey = row.Cells["colItemBatchKey"].Value != null ? Convert.ToInt32(row.Cells["colItemBatchKey"].Value) : 0;
                            batchNo = Convert.ToString(row.Cells["colBatchNo"].Value);

                            bool isValidDate = DateTime.TryParseExact(
                                expiryDateText,
                                "dd/MM/yyyy", // Use the format you're using in the DataGridView (adjust if needed)
                                CultureInfo.InvariantCulture,
                                DateTimeStyles.None,
                                out expiryDate
                            );

                            if (!isValidDate)
                            {
                                MessageBox.Show($"Invalid expiry date in row {row.Index + 1}. Use dd/MM/yyyy format.", "Date Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                continue;
                            }

                            costPrice = Convert.ToSingle(row.Cells["colCostPrice"].Value ?? 0);
                            salesPrice = Convert.ToSingle(row.Cells["colSalesPrice"].Value ?? 0);
                            quantity = Convert.ToSingle(row.Cells["colQuantity"].Value ?? 0);

                            string query;

                            if (batchKey == 0)
                            {
                                query = "INSERT INTO ItmBatch(ItmKy, BatchNo, ExpirDt, CosPri, SalePri, Qty) VALUES (@ItmKy, @BatchNo, @ExpirDt, @CosPri, @SalePri, @Qty)";
                            }
                            else
                            {
                                query = "UPDATE ItmBatch SET BatchNo=@BatchNo, ExpirDt=@ExpirDt, CosPri=@CosPri, SalePri=@SalePri, Qty=@Qty WHERE ItmBatchKy=@ItmBatchKy";
                            }

                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@ItmKy", itemKey);
                                cmd.Parameters.AddWithValue("@BatchNo", batchNo);
                                cmd.Parameters.AddWithValue("@ExpirDt", expiryDate);
                                cmd.Parameters.AddWithValue("@CosPri", costPrice);
                                cmd.Parameters.AddWithValue("@SalePri", salesPrice);
                                cmd.Parameters.AddWithValue("@Qty", quantity);

                                if (batchKey != 0)
                                    cmd.Parameters.AddWithValue("@ItmBatchKy", batchKey);

                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
                MessageBox.Show("Changes saved successfully.");
                cmbItemCode.SelectedIndex = -1;
                cmbItemName.SelectedIndex = -1;
                cmbGenaricName.SelectedIndex = -1;
                dgvItemUpdate.Rows.Clear();
                cmbItemCode.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvItemUpdate_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string colName = dgvItemUpdate.Columns[e.ColumnIndex].Name;
                int row = e.RowIndex;

                if (colName == "colBatchNo")
                {
                    this.BeginInvoke(new MethodInvoker(() =>
                    {
                        dgvItemUpdate.CurrentCell = dgvItemUpdate.Rows[row].Cells["colExpiryDate"];
                    }));
                }
                else if (colName == "colExpiryDate")
                {
                    var value = dgvItemUpdate.Rows[row].Cells["colExpiryDate"].Value;
                    if (value == null || !DateTime.TryParseExact(value.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                    {
                        MessageBox.Show("Invalid format, date is required", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dgvItemUpdate.Rows[row].Cells["colExpiryDate"].Value = "";
                        return;
                    }

                    this.BeginInvoke(new MethodInvoker(() =>
                    {
                        dgvItemUpdate.CurrentCell = dgvItemUpdate.Rows[row].Cells["colCostPrice"];
                    }));
                }
                else if (colName == "colCostPrice")
                {
                    this.BeginInvoke(new MethodInvoker(() =>
                    {
                        dgvItemUpdate.CurrentCell = dgvItemUpdate.Rows[row].Cells["colSalesPrice"];
                    }));
                }
                else if (colName == "colSalesPrice")
                {
                    this.BeginInvoke(new MethodInvoker(() =>
                    {
                        dgvItemUpdate.CurrentCell = dgvItemUpdate.Rows[row].Cells["colQuantity"];
                    }));
                }
                else if (colName == "colQuantity")
                {
                    var qtyValue = dgvItemUpdate.Rows[row].Cells["colQuantity"].Value;
                    if (qtyValue != null && !string.IsNullOrWhiteSpace(qtyValue.ToString()))
                    {
                        if (row == dgvItemUpdate.Rows.Count - 1)
                        {
                            dgvItemUpdate.Rows.Add();
                        }

                       this.BeginInvoke(new MethodInvoker(() =>
                    {
                        dgvItemUpdate.CurrentCell = dgvItemUpdate.Rows[row+1].Cells["colBatchNo"];
                    }));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        //--------------------------------------------------------------------------------------------------------------------------------------

        private void cmbItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnRefresh.Focus();
            }
        }

        private void btnRefresh_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnClear.Focus();
            }
        }

        private void dgvItemUpdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;

                int col = dgvItemUpdate.CurrentCell.ColumnIndex;
                int row = dgvItemUpdate.CurrentCell.RowIndex;

                if (col < dgvItemUpdate.Columns.Count - 1)
                {
                    dgvItemUpdate.CurrentCell = dgvItemUpdate.Rows[row].Cells[col + 1];
                }
                else if (row < dgvItemUpdate.Rows.Count - 1)
                {
                    dgvItemUpdate.CurrentCell = dgvItemUpdate.Rows[row +1].Cells[2];
                }
            }
        }
    }
}
