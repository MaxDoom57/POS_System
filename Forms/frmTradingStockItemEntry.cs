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
using Microsoft.ReportingServices.Diagnostics.Internal;
using POS_System.Classes;

namespace POS_System.Forms
{
    public partial class frmTradingStockItemEntry : Form
    {
        public frmTradingStockItemEntry()
        {
            InitializeComponent();
        }

        //-------------------------------------------------------------------------------------------------------------------------
        string query;
        DataTable unitTable;
        DataTable itemCat1Table;
        DataTable itemCat2Table;
        string saveStatus;
        int updatedRowValue;

        private void LoadUnitComboBox()
        {
            unitTable = new DataTable();

            using(SqlConnection conn = DBConnectionManager.GetConnection())
            {
                conn.Open();
                query = "SELECT Unit, UnitKy FROM vewUnit ORDER BY Unit";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(unitTable);
            }
        }
        
        private void LoadItemCat1ComboBox()
        {
            itemCat1Table = new DataTable();
            using(SqlConnection conn = DBConnectionManager.GetConnection())
            {
                conn.Open();
                query = "SELECT Code, CdKy FROM CdMas WHERE ConCD = 'ItmCat1' AND fInact = 0 ORDER BY CdNm";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(itemCat1Table);
            }
        }

        private void LoadItemCat2ComboBox()
        {
            itemCat2Table = new DataTable();
            using (SqlConnection conn = DBConnectionManager.GetConnection())
            {
                conn.Open();
                query = "SELECT Code, CdKy FROM CdMas WHERE ConCD = 'ItmCat2' AND fInact = 0 ORDER BY CdNm";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(itemCat2Table);
            }
        }

        private void SetupDataGridView()
        {
            dgvStockItem.Columns.Clear();

            DataGridViewTextBoxColumn colUpdate = new DataGridViewTextBoxColumn();
            colUpdate.Name = "update";
            colUpdate.HeaderText = "Update";
            colUpdate.Visible = false;
            dgvStockItem.Columns.Add(colUpdate);

            DataGridViewTextBoxColumn colItemKey = new DataGridViewTextBoxColumn();
            colItemKey.Name = "itemKey";
            colItemKey.HeaderText = "Item Key";
            colItemKey.DataPropertyName = "itemKey";
            colItemKey.Visible = false;
            dgvStockItem.Columns.Add(colItemKey);

            DataGridViewTextBoxColumn colItemCat4 = new DataGridViewTextBoxColumn();
            colItemCat4.Name = "itemCat4";
            colItemCat4.HeaderText = "Item Cat4";
            colItemCat4.DataPropertyName = "itemCat4Key";
            colItemCat4.Visible = false;
            dgvStockItem.Columns.Add(colItemCat4);

            DataGridViewTextBoxColumn colBUnit = new DataGridViewTextBoxColumn();
            colBUnit.Name = "BUnit";
            colBUnit.HeaderText = "BUnit";
            colBUnit.DataPropertyName = "BUnitKey";
            colBUnit.Visible = false;
            dgvStockItem.Columns.Add(colBUnit);

            DataGridViewTextBoxColumn colSalePrice2 = new DataGridViewTextBoxColumn();
            colSalePrice2.Name = "salePrice2";
            colSalePrice2.HeaderText = "Sale Price2";
            colSalePrice2.DataPropertyName = "salePrice2";
            colSalePrice2.DefaultCellStyle.Format = "N2";
            colSalePrice2.Visible = false;
            colSalePrice2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvStockItem.Columns.Add(colSalePrice2);

            DataGridViewTextBoxColumn colItmPriCat = new DataGridViewTextBoxColumn();
            colItmPriCat.Name = "itmPriCat";
            colItmPriCat.HeaderText = "ItmPriCat";
            colItmPriCat.DataPropertyName = "itemPriceCatKey";
            colItmPriCat.Visible = false;
            dgvStockItem.Columns.Add(colItmPriCat);

            DataGridViewTextBoxColumn colSerialNo = new DataGridViewTextBoxColumn();
            colSerialNo.Name = "serialNo";
            colSerialNo.HeaderText = "Serial No";
            colSerialNo.DataPropertyName = "serialNoKey";
            colSerialNo.Visible = false;
            dgvStockItem.Columns.Add(colSerialNo);

            DataGridViewTextBoxColumn colWaranty = new DataGridViewTextBoxColumn();
            colWaranty.Name = "waranty";
            colWaranty.HeaderText = "Waranty";
            colWaranty.DataPropertyName = "warantyKey";
            colWaranty.Visible = false;
            dgvStockItem.Columns.Add(colWaranty);

            DataGridViewTextBoxColumn colOuterRack = new DataGridViewTextBoxColumn();
            colOuterRack.Name = "outerRack";
            colOuterRack.HeaderText = "Outer Rack";
            colOuterRack.DataPropertyName = "outerRackId";
            colOuterRack.Visible = false;
            dgvStockItem.Columns.Add(colOuterRack);

            DataGridViewTextBoxColumn colInnerRack = new DataGridViewTextBoxColumn();
            colInnerRack.Name = "innerRack";
            colInnerRack.HeaderText = "Inner Rack";
            colInnerRack.DataPropertyName = "innerRackId";
            colInnerRack.Visible = false;
            dgvStockItem.Columns.Add(colInnerRack);

            DataGridViewTextBoxColumn colOuterRackKey = new DataGridViewTextBoxColumn();
            colOuterRackKey.Name = "outerRackKey";
            colOuterRackKey.HeaderText = "Outer Rack Key";
            colOuterRackKey.DataPropertyName = "outerRackKeyId";
            colOuterRackKey.Visible = false;
            dgvStockItem.Columns.Add(colOuterRackKey);

            DataGridViewTextBoxColumn colInnerRackKey = new DataGridViewTextBoxColumn();
            colInnerRackKey.Name = "innerRackKey";
            colInnerRackKey.HeaderText = "Inner Rack Key";
            colInnerRackKey.DataPropertyName = "innerRackKeyId";
            colInnerRackKey.Visible = false;
            dgvStockItem.Columns.Add(colInnerRackKey);


            DataGridViewTextBoxColumn colItemCode = new DataGridViewTextBoxColumn();
            colItemCode.Name = "itemCode";
            colItemCode.HeaderText = "Item Code";
            colItemCode.DataPropertyName = "itemCodeKey";
            colItemCode.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colItemCode.Width = 70;
            dgvStockItem.Columns.Add(colItemCode);

            DataGridViewTextBoxColumn colItemName = new DataGridViewTextBoxColumn();
            colItemName.Name = "itemName";
            colItemName.HeaderText = "Item Name";
            colItemName.DataPropertyName = "itemNameKey";
            colItemName.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colItemName.Width = 270;
            dgvStockItem.Columns.Add(colItemName);

            DataGridViewTextBoxColumn colPartNo = new DataGridViewTextBoxColumn();
            colPartNo.Name = "partNo";
            colPartNo.HeaderText = "Part No";
            colPartNo.DataPropertyName = "partNoKey";
            dgvStockItem.Columns.Add(colPartNo);

            DataGridViewComboBoxColumn colUnitCombo = new DataGridViewComboBoxColumn();
            colUnitCombo.Name = "unit";
            colUnitCombo.HeaderText = "Unit";
            colUnitCombo.DataSource = unitTable;
            colUnitCombo.DisplayMember = "Unit";
            colUnitCombo.ValueMember = "UnitKy";
            colUnitCombo.DataPropertyName = "UnitKey";
            colUnitCombo.SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvStockItem.Columns.Add(colUnitCombo);

            DataGridViewComboBoxColumn colItemCat1Combo = new DataGridViewComboBoxColumn();
            colItemCat1Combo.Name = "itemCat1";
            colItemCat1Combo.HeaderText = "Item Cat1";
            colItemCat1Combo.DataSource = itemCat1Table;
            colItemCat1Combo.DisplayMember = "Code";
            colItemCat1Combo.ValueMember = "CdKy";
            colItemCat1Combo.DataPropertyName = "itemcat1Key";
            colItemCat1Combo.SortMode= DataGridViewColumnSortMode.NotSortable;
            dgvStockItem.Columns.Add(colItemCat1Combo);

            DataGridViewComboBoxColumn colItemCat2Combo = new DataGridViewComboBoxColumn();
            colItemCat2Combo.Name = "itemCat2";
            colItemCat2Combo.HeaderText = "Item Cat2";
            colItemCat2Combo.DataSource = itemCat2Table;
            colItemCat2Combo.DisplayMember = "Code";
            colItemCat2Combo.ValueMember = "CdKy";
            colItemCat2Combo.DataPropertyName = "itemcat2Key";
            colItemCat2Combo.SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvStockItem.Columns.Add(colItemCat2Combo);

            DataGridViewTextBoxColumn colItemCat3 = new DataGridViewTextBoxColumn();
            colItemCat3.Name = "itemCat3";
            colItemCat3.HeaderText = "Item Cat3";
            colItemCat3.DataPropertyName = "itemCat3Key";
            colItemCat3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colItemCat3.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colItemCat3.Width = 50;
            dgvStockItem.Columns.Add(colItemCat3);

            DataGridViewTextBoxColumn colCostPrice = new DataGridViewTextBoxColumn();
            colCostPrice.Name = "costPrice";
            colCostPrice.HeaderText = "Cost Price";
            colCostPrice.DataPropertyName = "costPriceKey";
            colCostPrice.DefaultCellStyle.Format = "N2";
            colCostPrice.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvStockItem.Columns.Add(colCostPrice);

            DataGridViewTextBoxColumn colSalePrice = new DataGridViewTextBoxColumn();
            colSalePrice.Name = "salePrice";
            colSalePrice.HeaderText = "Sale Price";
            colSalePrice.DataPropertyName = "salePriceKey";
            colSalePrice.DefaultCellStyle.Format = "N2";
            colSalePrice.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvStockItem.Columns.Add(colSalePrice);

            DataGridViewTextBoxColumn colReOrdLvl = new DataGridViewTextBoxColumn();
            colReOrdLvl.Name = "reOrdLvl";
            colReOrdLvl.HeaderText = "ReOrdLvl";
            colReOrdLvl.DataPropertyName = "redOrdLvlKey";
            colReOrdLvl.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colReOrdLvl.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colReOrdLvl.Width = 60;
            dgvStockItem.Columns.Add(colReOrdLvl);

            DataGridViewTextBoxColumn colReOrdQty = new DataGridViewTextBoxColumn();
            colReOrdQty.Name = "reOrdQty";
            colReOrdQty.HeaderText = "ReOrdQty";
            colReOrdQty.DataPropertyName = "reOrdQtyKey";
            colReOrdQty.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colReOrdQty.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colReOrdQty.Width = 60;
            dgvStockItem.Columns.Add(colReOrdQty);

            DataGridViewCheckBoxColumn colInAct = new DataGridViewCheckBoxColumn();
            colInAct.Name = "fInAct";
            colInAct.HeaderText = "InAct";
            colInAct.DataPropertyName = "inActKey";
            colInAct.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colInAct.Width = 50;
            dgvStockItem.Columns.Add(colInAct);

            DataGridViewTextBoxColumn colMarketPrice = new DataGridViewTextBoxColumn();
            colMarketPrice.Name = "marketPrice";
            colMarketPrice.HeaderText = "Market Price";
            colMarketPrice.DataPropertyName = "marketPriceKey";
            colMarketPrice.DefaultCellStyle.Format = "N2";
            colMarketPrice.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvStockItem.Columns.Add(colMarketPrice);

            DataGridViewTextBoxColumn colDiscount = new DataGridViewTextBoxColumn();
            colDiscount.Name = "discount";
            colDiscount.HeaderText = "Discount";
            colDiscount.DataPropertyName = "discountKey";
            colDiscount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colDiscount.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colDiscount.Width = 70;
            dgvStockItem.Columns.Add(colDiscount);


            foreach (DataGridViewColumn col in dgvStockItem.Columns)
            {
                if (col is DataGridViewComboBoxColumn)
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
                else
                    col.SortMode = DataGridViewColumnSortMode.Automatic;
            }

        }

        private void LoadData()
        {
            dgvStockItem.Rows.Clear();

            using (SqlConnection conn = DBConnectionManager.GetConnection())
            {
                conn.Open();
                string query = "SELECT ItmKy, ItmCd, ItmNm, PartNo, Unit, ItmCat1, ItmCat2, ItmCat3, ItmCat4, BUCd, CosPri, SlsPri, SlsPri2," +
                    "ItmPriCatCd, fSrlNo, Wrnty, ReOrdLvl, ReOrdQty, fInAct, SLSPri2, Rac1Cd, Rac2Cd, Rac1Ky, Rac2Ky, DisPer FROM vewItmMasVsf";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string itemKey = reader["ItmKy"].ToString();
                    string itemCodeKey = reader["ItmCd"].ToString();
                    string itemNameKey = reader["ItmNm"].ToString();
                    string partNoKey = reader["PartNo"].ToString();

                    string unitName = reader["Unit"] == DBNull.Value ? null : reader["Unit"].ToString();
                    object unitKey = DBNull.Value;
                    if (!string.IsNullOrEmpty(unitName))
                    {
                        DataRow[] matchingUnitRows = unitTable.Select($"Unit = '{unitName.Replace("'", "''")}'");
                        if (matchingUnitRows.Length > 0)
                        {
                            unitKey = matchingUnitRows[0]["UnitKy"];
                        }
                    }

                    string itemCat1Name = reader["ItmCat1"] == DBNull.Value ? null : reader["ItmCat1"].ToString() ;
                    object itemCat1Key = DBNull.Value;
                    if (!string.IsNullOrEmpty (itemCat1Name))
                    {
                        DataRow[] matchingItemCat1Row = itemCat1Table.Select($"Code = '{itemCat1Name.Replace("'", "''")}'");
                        if(matchingItemCat1Row.Length > 0)
                        {
                            itemCat1Key = matchingItemCat1Row[0]["CdKy"];
                        }
                    }

                    string itemCat2Name = reader["ItmCat2"] == DBNull.Value ? null : reader["ItmCat2"].ToString();
                    object itemCat2Key = DBNull.Value;
                    if (!string.IsNullOrEmpty(itemCat2Name))
                    {
                        DataRow[] matchingItemCat2Row = itemCat2Table.Select($"Code = '{itemCat2Name.Replace("'", "''")}'");
                        if (matchingItemCat2Row.Length > 0)
                        {
                            itemCat2Key = matchingItemCat2Row[0]["CdKy"];
                        }
                    }

                    string itemCat3Key = reader["ItmCat3"].ToString();
                    string itemCat4Key = reader["ItmCat4"].ToString();
                    string BUnitKey = reader["BUCd"].ToString();
                    double costPriceKey = Convert.ToDouble(reader["CosPri"]);
                    double salePriceKey = Convert.ToDouble(reader["SlsPri"]);
                    double salePrice2Key = Convert.ToDouble(reader["SlsPri2"]);
                    string itmPriCatKey = reader["ItmPriCatCd"].ToString();
                    int serialNoKey = Convert.ToInt32(reader["fSrlNo"]);
                    string warantyKey = reader["Wrnty"].ToString();
                    string reOrdLvlKey = reader["ReOrdLvl"].ToString();
                    string reOrdQtyKey = reader["ReOrdQty"].ToString();
                    bool inActKey = Convert.ToBoolean(reader["fInAct"]);
                    double marketPriceKey = Convert.ToDouble(reader["SLSPri2"]);
                    string outerRackId = reader["Rac1Cd"].ToString();
                    string innerRackId = reader["Rac2Cd"].ToString();
                    string outerRackKeyId = reader["Rac1Ky"].ToString();
                    string innerRackKeyId = reader["Rac2Ky"].ToString();
                    string discountKey = reader["DisPer"].ToString();


                    int rowIndex = dgvStockItem.Rows.Add();
                    DataGridViewRow row = dgvStockItem.Rows[rowIndex];
                    //row.Cells["update"].Value = 0;
                    row.Cells["itemKey"].Value = itemKey;
                    row.Cells["itemCode"].Value = itemCodeKey;
                    row.Cells["itemName"].Value = itemNameKey;
                    row.Cells["partNo"].Value = partNoKey;
                    row.Cells["unit"].Value = unitKey;
                    row.Cells["itemCat1"].Value = itemCat1Key;
                    row.Cells["itemCat2"].Value = itemCat2Key;
                    row.Cells["itemCat3"].Value = itemCat3Key;
                    row.Cells["itemCat4"].Value = itemCat4Key;
                    row.Cells["BUnit"].Value = BUnitKey;
                    row.Cells["costPrice"].Value = costPriceKey;
                    row.Cells["salePrice"].Value = salePriceKey;
                    row.Cells["salePrice2"].Value = salePrice2Key;
                    row.Cells["itmPriCat"].Value = itmPriCatKey;
                    row.Cells["serialNo"].Value = serialNoKey;
                    row.Cells["waranty"].Value = warantyKey;
                    row.Cells["reOrdLvl"].Value = reOrdLvlKey;
                    row.Cells["reOrdQty"].Value = reOrdQtyKey;
                    row.Cells["fInAct"].Value = inActKey;
                    row.Cells["marketPrice"].Value = marketPriceKey;
                    row.Cells["outerRack"].Value = outerRackId;
                    row.Cells["innerRack"].Value = innerRackId;
                    row.Cells["outerRackKey"].Value = outerRackKeyId;
                    row.Cells["innerRackKey"].Value = innerRackKeyId;
                    row.Cells["discount"].Value = discountKey;
                }
                foreach (DataGridViewRow row in dgvStockItem.Rows)
                {
                    if (!row.IsNewRow) // Avoid the empty new row at the bottom
                    {
                        row.Cells["update"].Value = 0;
                    }
                }

                reader.Close();
            }
        }

        private void SetParameters(SqlCommand cmd, DataGridViewRow row)
        {
            int varUnitKey = 0;
            object unitNameObj = row.Cells["unit"].Value;

            if (unitNameObj != null && int.TryParse(unitNameObj.ToString(), out int parsedValueUnit))
            {
                varUnitKey = parsedValueUnit;
            }


            int varItmCat1Ky = 0;
            object cat1CodeObj = row.Cells["itemCat1"].Value;
            if (cat1CodeObj != null && int.TryParse(cat1CodeObj.ToString(), out int parsedValueItemCat1))
            {
                varItmCat1Ky = parsedValueItemCat1;
            }


            int varItmCat2Ky = 0;
            object cat2CodeObj = row.Cells["itemCat2"].Value;

            if (cat2CodeObj != null && int.TryParse(cat2CodeObj.ToString(), out int parsedValueItemCat2))
            {
                varItmCat2Ky = parsedValueItemCat2;
            }


            cmd.Parameters.AddWithValue("@ItmCd", row.Cells["itemCode"].Value ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ItmNm", row.Cells["itemName"].Value ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@PartNo", row.Cells["partNo"].Value ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UnitKy", varUnitKey); 
            cmd.Parameters.AddWithValue("@BUCd", row.Cells["BUnit"].Value ?? 0); 

            cmd.Parameters.AddWithValue("@ItmCat1", varItmCat1Ky); 
            cmd.Parameters.AddWithValue("@ItmCat2", varItmCat2Ky);
            cmd.Parameters.AddWithValue("@ItmCat3", row.Cells["itemCat3"].Value ?? 0);
            cmd.Parameters.AddWithValue("@ItmCat4", row.Cells["itemCat4"].Value ?? 0);

            cmd.Parameters.AddWithValue("@CosPri", row.Cells["costPrice"].Value ?? 0.00);
            cmd.Parameters.AddWithValue("@SlsPri", row.Cells["salePrice"].Value ?? 0.00);
            cmd.Parameters.AddWithValue("@SlsPri2", row.Cells["salePrice2"].Value ?? 0.00); 

            cmd.Parameters.AddWithValue("@ItmTypKy", 89); 
            cmd.Parameters.AddWithValue("@ItmTyp", "TRDITM");

            cmd.Parameters.AddWithValue("@ItmPriCatCd", row.Cells["itmPriCat"].Value ?? 0); 

            cmd.Parameters.AddWithValue("@fSrlNo", row.Cells["serialNo"].Value ?? 0);
            cmd.Parameters.AddWithValue("@Wrnty", row.Cells["waranty"].Value ?? 0); 
            cmd.Parameters.AddWithValue("@ReOrdLvl", row.Cells["reOrdLvl"].Value ?? 0);
            cmd.Parameters.AddWithValue("@ReOrdQty", row.Cells["reOrdQty"].Value ?? 0);

            cmd.Parameters.AddWithValue("@fApr", 1); 

            cmd.Parameters.AddWithValue("@Rac1Cd", row.Cells["outerRack"].Value ?? 0); 
            cmd.Parameters.AddWithValue("@Rac2Cd", row.Cells["innerRack"].Value ?? 0);
            cmd.Parameters.AddWithValue("@Rac1Ky", row.Cells["outerRackKey"].Value ?? 0);
            cmd.Parameters.AddWithValue("@Rac2Ky", row.Cells["innerRackKey"].Value ?? 0);

            cmd.Parameters.AddWithValue("@DisPer", row.Cells["discount"].Value ?? 0);
            cmd.Parameters.AddWithValue("@fInAct", row.Cells["fInAct"].Value ?? false); 

        }



        //-------------------------------------------------------------------------------------------------------------------------
        private void frmTradingStockItemEntry_Load(object sender, EventArgs e)
        {
            LoadUnitComboBox();
            LoadItemCat1ComboBox();
            LoadItemCat2ComboBox();
            SetupDataGridView();
            LoadData();
            btnSave.Enabled = false;
        }

        private void dgvStockItem_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in dgvStockItem.Rows)
            {
                if (row.Cells["unit"].Value == DBNull.Value || row.Cells["unit"].Value == null)
                {
                    row.Cells["unit"].Value = DBNull.Value;
                }

                if (row.Cells["itemCat1"].Value == DBNull.Value || row.Cells["itemCat1"].Value == null)
                {
                    row.Cells["itemCat1"].Value = DBNull.Value;
                }

                if (row.Cells["itemCat2"].Value == DBNull.Value || row.Cells["itemCat2"].Value == null)
                {
                    row.Cells["itemCat2"].Value = DBNull.Value;
                }
            }
        }

        private void dgvStockItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;

                int col = dgvStockItem.CurrentCell.ColumnIndex;
                int row = dgvStockItem.CurrentCell.RowIndex;

                if (col < dgvStockItem.Columns.Count - 1)
                {
                    dgvStockItem.CurrentCell = dgvStockItem.Rows[row].Cells[col + 1];
                }
                else if (row < dgvStockItem.Rows.Count - 1)
                {
                    dgvStockItem.CurrentCell = dgvStockItem.Rows[row + 1].Cells[12];
                }
            }
        }

        private void dgvStockItem_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            int rowIndex = e.Row.Index - 1; 
            if (rowIndex >= 0 && rowIndex < dgvStockItem.Rows.Count)
            {
                dgvStockItem.Rows[rowIndex].Cells["update"].Value = 1;
                btnSave.Enabled = true;
            }
        }

        private void dgvStockItem_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (dgvStockItem.Columns[e.ColumnIndex].Name != "update")
                {
                    dgvStockItem.Rows[e.RowIndex].Cells["update"].Value = 1;
                    btnSave.Enabled = true;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want save changes?", this.Text, MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                using (SqlConnection conn = DBConnectionManager.GetConnection())
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        foreach (DataGridViewRow row in dgvStockItem.Rows)
                        {
                            if (Convert.ToInt32(row.Cells["update"].Value) == 1)
                            {
                                if (row.IsNewRow) continue;

                                string itemKey = row.Cells["itemKey"].Value?.ToString() ?? "";

                                if (string.IsNullOrEmpty(itemKey)) // INSERT new item
                                {
                                    string insertQuery = @"
                                                    INSERT INTO ItmMas (
                                                        ItmCd, ItmNm, PartNo, UnitKy, BuKy, ItmCat1Ky, ItmCat2Ky, ItmCat3Ky, ItmCat4Ky,
                                                        CosPri, SlsPri, SlsPri2, ItmTypKy, ItmTyp, ItmPriCatKy, fSrlNo, Wrnty,
                                                        ReOrdLvl, ReOrdQty, fApr, Rac1Ky, Rac2Ky, DisPer
                                                    )
                                                    VALUES (
                                                        @ItmCd, @ItmNm, @PartNo, @UnitKy, @BUCd, @ItmCat1, @ItmCat2, @ItmCat3, @ItmCat4,
                                                        @CosPri, @SlsPri, @SlsPri2, @ItmTypKy, @ItmTyp, @ItmPriCatCd, @fSrlNo, @Wrnty,
                                                        @ReOrdLvl, @ReOrdQty, @fApr, @Rac1Ky, @Rac2Ky, @DisPer
                                                    )";

                                    SqlCommand insertCmd = new SqlCommand(insertQuery, conn, transaction);
                                    SetParameters(insertCmd, row);
                                    insertCmd.ExecuteNonQuery();
                                }
                                else // UPDATE existing item
                                {
                                    string updateQuery = @"UPDATE ItmMas SET 
                                                    ItmCd = @ItmCd, 
                                                    ItmNm = @ItmNm, 
                                                    PartNo = @PartNo, 
                                                    UnitKy = @UnitKy, 
                                                    ItmCat1Ky = @ItmCat1, 
                                                    ItmCat2Ky = @ItmCat2, 
                                                    ItmCat3Ky = @ItmCat3, 
                                                    ItmCat4Ky = @ItmCat4, 
                                                    BUKy = @BUCd, 
                                                    CosPri = @CosPri, 
                                                    SlsPri = @SlsPri, 
                                                    SlsPri2 = @SlsPri2, 
                                                    ItmPriCatKy = @ItmPriCatCd, 
                                                    fSrlNo = @fSrlNo, 
                                                    Wrnty = @Wrnty, 
                                                    ReOrdLvl = @ReOrdLvl, 
                                                    ReOrdQty = @ReOrdQty, 
                                                    fInAct = @fInAct, 
                                                    Rac1Ky = @Rac1Ky, 
                                                    Rac2Ky = @Rac2Ky, 
                                                    DisPer = @DisPer 
                                                    WHERE ItmKy = @ItmKy";

                                    SqlCommand updateCmd = new SqlCommand(updateQuery, conn, transaction);
                                    updateCmd.Parameters.AddWithValue("@ItmKy", itemKey);
                                    SetParameters(updateCmd, row);
                                    updateCmd.ExecuteNonQuery();
                                }
                            }
                        }

                        transaction.Commit();
                        MessageBox.Show("Changes saved successfully.");
                        btnSave.Enabled = false;
                        foreach(DataGridViewRow row in dgvStockItem.Rows)
                        {
                            row.Cells["update"].Value = 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Error saving data: " + ex.Message);
                    }
                }
            }
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        //-------------------------------------------------------------------------------------------------------------------------
    }
}
