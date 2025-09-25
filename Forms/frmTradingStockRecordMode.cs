using System;
using System.Collections;
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
    public partial class frmTradingStockRecordMode : Form
    {
        public frmTradingStockRecordMode()
        {
            InitializeComponent();
        }

        //Define variables
        private SqlConnection sqlConnection;
        string passValue;
        int itemKeyInDB;


        private void ClearForm()
        {
            cmbItemCode.SelectedIndex = -1;
            cmbItemName.SelectedIndex = -1;
            txtItemCode.Text = "";
            txtPartNo.Text = "";
            txtItemName.Text = "";
            cmbUnit.SelectedIndex = -1;
            cmbCat1.SelectedIndex = -1;
            cmbCat2.SelectedIndex = -1;
            txtCostPrice.Text = "";
            txtSalesPrice.Text = "";
        }


        private void GetItemDetails(string pItmKy)
        {
            try
            {
                using (SqlConnection sqlConnection = DBConnectionManager.GetConnection())
                {
                    sqlConnection.Open();
                    string query = "SELECT * FROM dbo.vewItmMas WHERE ItmCd = @pItmKy";

                    using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                    {
                        cmd.Parameters.AddWithValue("@pItmKy", pItmKy);


                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                itemKeyInDB =Convert.ToInt16 (reader["ItmKy"]);
                                txtItemCode.Text = reader["ItmCd"].ToString();
                                txtItemName.Text = reader["ItmNm"].ToString();
                                cmbUnit.Text = reader["Unit"].ToString();
                                txtCostPrice.Text = Convert.ToDecimal(reader["CosPri"]).ToString("0.00");
                                txtSalesPrice.Text = Convert.ToDecimal(reader["SlsPri"]).ToString("0.00");
                                txtPartNo.Text = string.IsNullOrEmpty(reader["PartNo"].ToString()) ? "" : reader["PartNo"].ToString();
                                cmbItemName.Text = reader["ItmNm"].ToString();

                                string cat1Ky = reader["ItmCat1Ky"].ToString();
                                if (cat1Ky == "0" || cat1Ky == "-1")
                                {
                                    cmbCat1.Text = "";
                                }
                                else
                                {
                                    cmbCat1.Text = reader["ItmCat1Ky"].ToString();

                                }

                                string cat2Ky = reader["ItmCat2Ky"].ToString();
                                if (cat2Ky == "0" || cat2Ky == "-1")
                                {
                                    cmbCat2.Text = "";
                                }
                                else
                                {
                                    cmbCat2.Text = reader["ItmCat2Ky"].ToString();
                                }

                            }

                            else
                            {
                                MessageBox.Show("No matching item found.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbItemCode.Text))
            {
                DialogResult dialogResult = MessageBox.Show("An item must select to Delete", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbItemCode.Focus();
            }

            else
            {
                string itemCode = cmbItemCode.Text;

                DialogResult result = MessageBox.Show(
                "Do you want to DELETE the selected item ?",
                this.Text,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);

                string query = "SELECT ItmKy FROM vewItmMas WHERE ItmCd = @ItmCd";
                sqlConnection = DBConnectionManager.GetConnection();
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@ItmCd", itemCode);
                object itemCodeResult = cmd.ExecuteScalar();

                if (itemCodeResult != null)
                {
                    itemKeyInDB = Convert.ToInt16(itemCodeResult);
                }

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        query = "UPDATE ItmMas SET fInAct = 1, Status = 'D' WHERE ItmKy = @ItmKy";
                        sqlConnection = DBConnectionManager.GetConnection();
                        cmd = new SqlCommand(query, sqlConnection);
                        sqlConnection.Open();
                        cmd.Parameters.AddWithValue("@ItmKy", itemKeyInDB);
                        cmd.ExecuteNonQuery();

                        ClearForm();
                        cmbItemCode.Focus();
                        cmbItemCode.Refresh();
                        cmbItemName.Refresh();
                        sqlConnection.Close();

                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }           
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            string query;
            long itemKey = 0;
            string itemCode, partNo, itemName;
            long unitKey, cat1Key, cat2Key;
            float costPrice, salesPrice;

            try
            {
                if (string.IsNullOrEmpty(txtItemCode.Text))
                {
                    MessageBox.Show("Item code is must", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItemCode.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtItemName.Text))
                {
                    MessageBox.Show("Item name is must", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItemCode.Focus();
                    return;
                }

                itemCode = txtItemCode.Text.Trim();
                partNo = txtPartNo.Text.Trim();
                itemName = txtItemName.Text.Trim();
                unitKey = Convert.ToInt64(cmbUnit.SelectedIndex);
                cat1Key = Convert.ToInt64(cmbCat1.SelectedIndex);
                cat2Key = Convert.ToInt64(cmbCat2.SelectedIndex);
                costPrice = float.Parse(txtCostPrice.Text);
                salesPrice = float.Parse(txtSalesPrice.Text);

                query = "SELECT ItmKy FROM vewItmMas WHERE ItmCd = @ItmCd";
                sqlConnection = DBConnectionManager.GetConnection();
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                string itemCodeFromCMB = cmbItemCode.Text.Trim();
                cmd.Parameters.AddWithValue("@ItmCd", itemCodeFromCMB);
                object result = cmd.ExecuteScalar();

                //Get itemKey if it exist: otherwise itemKey = 0
                if (result != null)
                {
                    itemKey = Convert.ToInt64(result);
                }
                else
                {
                    itemKey = 0;
                }

                //Insert or Update
                if (itemKey == 0)
                {
                    query = @"INSERT INTO ItmMas 
                    (ItmCd, PartNo, ItmNm, UnitKy, ItmTyp, ItmTypKy, ItmCat1Ky, ItmCat2Ky, CosPri, SlsPri) 
                    VALUES (@itemCode , @partNo , @itemName , @unitKey , 'TRDITM', 89, @cat1Key , @cat2Key , @costPrice , @salesPrice)";
                }
                else
                {
                    query = @"UPDATE ItmMas SET 
                       ItmCd = @itemCode, 
                       PartNo = @PartNo, 
                       ItmNm = @itemName, 
                       UnitKy = @unitKey, 
                       ItmCat1Ky = @cat1Key, 
                       ItmCat2Ky = @cat2Key,
                       CosPri = @costPrice, 
                       SlsPri = @salesPrice
                       WHERE ItmKy = @itemKey";
                }

                sqlConnection = DBConnectionManager.GetConnection();
                sqlConnection.Open();
                cmd = new SqlCommand(query, sqlConnection);

                cmd.Parameters.AddWithValue("@itemCode", itemCode);
                cmd.Parameters.AddWithValue("@partNo", partNo);
                cmd.Parameters.AddWithValue("@itemName", itemName);
                cmd.Parameters.AddWithValue("@unitKey", unitKey);
                cmd.Parameters.AddWithValue("@cat1Key", cat1Key);
                cmd.Parameters.AddWithValue("@cat2Key", cat2Key);
                cmd.Parameters.AddWithValue("@costPrice", costPrice);
                cmd.Parameters.AddWithValue("@salesPrice", salesPrice);

                //for update database;
                if (itemKey != 0)
                {
                    cmd.Parameters.AddWithValue("@itemKey", itemKey);
                }

                cmd.ExecuteNonQuery();

            }

            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }

                ClearForm();
                frmTradingStockRecordMode_Load(this, EventArgs.Empty);
            }
        }


        private void frmTradingStockRecordMode_Load(object sender, EventArgs e)
        {
            cmbItemCode.Focus();

            using (SqlConnection conn = DBConnectionManager.GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT ItmCd, ItmKy FROM vewItmMas ORDER BY ItmCd", conn))

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cmbItemCode.Items.Add(reader["ItmCd"].ToString());
                    }
                }

                using (SqlCommand cmd = new SqlCommand("SELECT ItmNm, ItmKy FROM vewItmMas ORDER BY ItmNm", conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cmbItemName.Items.Add(reader["ItmNm"].ToString());
                    }
                }

                using (SqlCommand cmd = new SqlCommand("SELECT CdNm, CdKy FROM CdMas WHERE ConCD = 'ItmCat1' AND fInact = 0 ORDER BY CdNm", conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cmbCat1.Items.Add(reader["CdNm"].ToString());
                    }
                }

                using (SqlCommand cmd = new SqlCommand("SELECT CdNm, CdKy FROM CdMas WHERE ConCD = 'ItmCat2' AND fInact = 0 ORDER BY CdNm", conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cmbCat2.Items.Add(reader["CdNm"].ToString());
                    }
                }

                using (SqlCommand cmd = new SqlCommand("SELECT Unit, UnitKy FROM vewUnit ORDER BY Unit", conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cmbUnit.Items.Add(reader["Unit"].ToString());
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void frmTradingStockRecordMode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Close();
            }
        }


        private void txtCostPrice_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCostPrice.Text))
                return;

            float costPrice;
            if (!float.TryParse(txtCostPrice.Text, out costPrice))
            {
                MessageBox.Show("Invalid data type", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCostPrice.Text = "0";
            }
            else if (costPrice < 0)
            {
                MessageBox.Show("Cost price can not be less than 0", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCostPrice.Text = "0";
            }
            else
            {
                txtCostPrice.Text = costPrice.ToString("0.00");
            }
        }


        private void txtSalesPrice_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSalesPrice.Text))
                return;

            float salesPrice;
            if (!float.TryParse(txtSalesPrice.Text, out salesPrice))
            {
                MessageBox.Show("Invalid data type", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSalesPrice.Text = "0.00";
            }
            else if (salesPrice < 0)
            {
                MessageBox.Show("Sales price can not be less than 0", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSalesPrice.Text = "0.00";
            }
            else
            {
                txtSalesPrice.Text = salesPrice.ToString("0.00");
            }
        }


        private void cmbItemCode_TextChanged(object sender, EventArgs e)
        {
        }

        private void cmbItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbItemName.Focus();
            }
            else if (e.KeyCode == Keys.Back)
            {
                cmbItemCode.Text = "";
            }
        }


        private void cmbItemCode_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbItemCode.Text)) 
            {
                passValue = cmbItemCode.Text.ToString();
                GetItemDetails(passValue);
            }
        }


        private void cmbItemName_TextChanged(object sender, EventArgs e)
        {
        }


        private void cmbItemName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtItemCode.Focus();
            }
            else if (e.KeyCode == Keys.Back)
            {
                cmbItemName.Text = "";
            }
        }


        private void cmbItemName_Leave(object sender, EventArgs e)
        {
            string itemName = cmbItemName.Text.ToString();
            if (!string.IsNullOrEmpty(itemName))
            {
                try
                {
                    SqlConnection sqlConnection = DBConnectionManager.GetConnection();
                    sqlConnection.Open();
                    string query = "SELECT ItmCd from dbo.vewItmMas WHERE ItmNm = @itemName";
                    SqlCommand cmd = new SqlCommand(query, sqlConnection);
                    cmd.Parameters.AddWithValue("@itemName", itemName);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cmbItemCode.Text = reader["ItmCd"].ToString();
                            cmbItemName.Text = "";
                            passValue = cmbItemCode.Text.ToString();
                            GetItemDetails(passValue);
                        }

                        else
                        {
                            MessageBox.Show("No matching item found.");
                        }
                    }
                }

                catch (Exception ex) 
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        private void cmbUnit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbCat1.Focus();
            }
        }


        private void txtCostPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                txtSalesPrice.Focus();
            }
        }


        private void txtItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPartNo.Focus();
            }
        }


        private void txtItemName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbUnit.Focus();
            }
        }


        private void txtSalesPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSave.Focus();
            }
        }


        private void cmbCat1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbCat2.Focus();
            }
        }


        private void cmbCat2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCostPrice.Focus();
            }
        }

        private void cmbItemCode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtCostPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter) 
            {
                txtSalesPrice.Focus(); 
            }
        }

        private void txtPartNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                txtItemName.Focus();
            }
        }
    }
}
