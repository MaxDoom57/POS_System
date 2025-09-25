namespace POS_System.Forms
{
    partial class frmReceiveItems
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReceiveItems));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.dgvSearchItems = new System.Windows.Forms.DataGridView();
            this.txtSearchBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.roundedPanel5 = new POS_System.Custom_Components.RoundedPanel();
            this.btnHide = new System.Windows.Forms.Button();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.roundedPanel4 = new POS_System.Custom_Components.RoundedPanel();
            this.txtTotalAmount = new System.Windows.Forms.TextBox();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.roundedPanel3 = new POS_System.Custom_Components.RoundedPanel();
            this.dgvGrn = new System.Windows.Forms.DataGridView();
            this.colItemKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colItemCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colItemName = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCostPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTranPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSalePrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colItemTranKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUpdate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDelete = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colExpireDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBatchNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colExistQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colExistItemKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colExistExpireDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colExistCostPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colInhand = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSave = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.roundedPanel2 = new POS_System.Custom_Components.RoundedPanel();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.cmbAddress = new System.Windows.Forms.ComboBox();
            this.cmbSupplierName = new System.Windows.Forms.ComboBox();
            this.cmbSupplierCode = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.roundedPanel6 = new POS_System.Custom_Components.RoundedPanel();
            this.cmbPaymentTerm = new System.Windows.Forms.ComboBox();
            this.cmbPurchaseAccount = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.lblSalesAccount = new System.Windows.Forms.Label();
            this.roundedPanel1 = new POS_System.Custom_Components.RoundedPanel();
            this.dtpGrnDate = new System.Windows.Forms.DateTimePicker();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.cmbReference = new System.Windows.Forms.ComboBox();
            this.cmbGrnNo = new System.Windows.Forms.ComboBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.lblYourReferences = new System.Windows.Forms.Label();
            this.lblTranDate = new System.Windows.Forms.Label();
            this.lblInvoiceNo = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchItems)).BeginInit();
            this.roundedPanel5.SuspendLayout();
            this.roundedPanel4.SuspendLayout();
            this.roundedPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrn)).BeginInit();
            this.roundedPanel2.SuspendLayout();
            this.roundedPanel6.SuspendLayout();
            this.roundedPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.pnlSearch);
            this.panel1.Controls.Add(this.roundedPanel5);
            this.panel1.Controls.Add(this.roundedPanel4);
            this.panel1.Controls.Add(this.roundedPanel3);
            this.panel1.Controls.Add(this.roundedPanel2);
            this.panel1.Controls.Add(this.roundedPanel6);
            this.panel1.Controls.Add(this.roundedPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1334, 686);
            this.panel1.TabIndex = 1;
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackColor = System.Drawing.SystemColors.MenuBar;
            this.pnlSearch.Controls.Add(this.dgvSearchItems);
            this.pnlSearch.Controls.Add(this.txtSearchBox);
            this.pnlSearch.Controls.Add(this.label2);
            this.pnlSearch.Controls.Add(this.label1);
            this.pnlSearch.Location = new System.Drawing.Point(5, 256);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(1326, 242);
            this.pnlSearch.TabIndex = 6;
            // 
            // dgvSearchItems
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.dgvSearchItems.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSearchItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSearchItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSearchItems.Location = new System.Drawing.Point(14, 113);
            this.dgvSearchItems.Name = "dgvSearchItems";
            this.dgvSearchItems.RowHeadersWidth = 10;
            this.dgvSearchItems.RowTemplate.Height = 24;
            this.dgvSearchItems.Size = new System.Drawing.Size(1295, 536);
            this.dgvSearchItems.TabIndex = 3;
            this.dgvSearchItems.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvSearchItems_KeyDown);
            // 
            // txtSearchBox
            // 
            this.txtSearchBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearchBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchBox.Location = new System.Drawing.Point(214, 62);
            this.txtSearchBox.Name = "txtSearchBox";
            this.txtSearchBox.Size = new System.Drawing.Size(963, 27);
            this.txtSearchBox.TabIndex = 2;
            this.txtSearchBox.TextChanged += new System.EventHandler(this.txtSearchBox_TextChanged);
            this.txtSearchBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchBox_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(149, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Item";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(625, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Item Search";
            // 
            // roundedPanel5
            // 
            this.roundedPanel5.BackColor = System.Drawing.SystemColors.Menu;
            this.roundedPanel5.BorderColor = System.Drawing.SystemColors.ScrollBar;
            this.roundedPanel5.BorderSize = 0;
            this.roundedPanel5.Controls.Add(this.btnHide);
            this.roundedPanel5.Controls.Add(this.btnPreview);
            this.roundedPanel5.Controls.Add(this.btnSave);
            this.roundedPanel5.Controls.Add(this.btnDelete);
            this.roundedPanel5.Controls.Add(this.btnAdd);
            this.roundedPanel5.CornerRadius = 6;
            this.roundedPanel5.Location = new System.Drawing.Point(2, 612);
            this.roundedPanel5.Name = "roundedPanel5";
            this.roundedPanel5.Size = new System.Drawing.Size(1323, 70);
            this.roundedPanel5.TabIndex = 4;
            // 
            // btnHide
            // 
            this.btnHide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHide.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHide.Location = new System.Drawing.Point(1200, 17);
            this.btnHide.Name = "btnHide";
            this.btnHide.Size = new System.Drawing.Size(112, 36);
            this.btnHide.TabIndex = 16;
            this.btnHide.Text = "Hide";
            this.btnHide.UseVisualStyleBackColor = true;
            this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
            // 
            // btnPreview
            // 
            this.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPreview.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPreview.Location = new System.Drawing.Point(1068, 17);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(112, 36);
            this.btnPreview.TabIndex = 15;
            this.btnPreview.Text = "Preview";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(936, 17);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(112, 36);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(804, 17);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(112, 36);
            this.btnDelete.TabIndex = 13;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(672, 17);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(112, 36);
            this.btnAdd.TabIndex = 12;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // roundedPanel4
            // 
            this.roundedPanel4.BackColor = System.Drawing.SystemColors.Menu;
            this.roundedPanel4.BorderColor = System.Drawing.SystemColors.ScrollBar;
            this.roundedPanel4.BorderSize = 0;
            this.roundedPanel4.Controls.Add(this.txtTotalAmount);
            this.roundedPanel4.Controls.Add(this.lblTotalAmount);
            this.roundedPanel4.CornerRadius = 6;
            this.roundedPanel4.Location = new System.Drawing.Point(3, 563);
            this.roundedPanel4.Name = "roundedPanel4";
            this.roundedPanel4.Size = new System.Drawing.Size(1322, 46);
            this.roundedPanel4.TabIndex = 3;
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalAmount.Location = new System.Drawing.Point(1038, 10);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.Size = new System.Drawing.Size(272, 27);
            this.txtTotalAmount.TabIndex = 11;
            this.txtTotalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAmount.Location = new System.Drawing.Point(916, 14);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(99, 18);
            this.lblTotalAmount.TabIndex = 4;
            this.lblTotalAmount.Text = "Total Amount";
            // 
            // roundedPanel3
            // 
            this.roundedPanel3.BackColor = System.Drawing.SystemColors.Menu;
            this.roundedPanel3.BorderColor = System.Drawing.SystemColors.ScrollBar;
            this.roundedPanel3.BorderSize = 0;
            this.roundedPanel3.Controls.Add(this.dgvGrn);
            this.roundedPanel3.CornerRadius = 6;
            this.roundedPanel3.Location = new System.Drawing.Point(6, 183);
            this.roundedPanel3.Name = "roundedPanel3";
            this.roundedPanel3.Size = new System.Drawing.Size(1319, 360);
            this.roundedPanel3.TabIndex = 2;
            // 
            // dgvGrn
            // 
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.dgvGrn.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvGrn.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvGrn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGrn.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colItemKey,
            this.colItemCode,
            this.colItemName,
            this.colUnit,
            this.colCostPrice,
            this.colTranPrice,
            this.colSalePrice,
            this.colQuantity,
            this.colAmount,
            this.colItemTranKey,
            this.colUpdate,
            this.colDelete,
            this.colExpireDate,
            this.colBatchNo,
            this.colExistQuantity,
            this.colExistItemKey,
            this.colExistExpireDate,
            this.colExistCostPrice,
            this.colInhand,
            this.colSave});
            this.dgvGrn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvGrn.Location = new System.Drawing.Point(0, 0);
            this.dgvGrn.Name = "dgvGrn";
            this.dgvGrn.RowHeadersWidth = 10;
            this.dgvGrn.RowTemplate.Height = 24;
            this.dgvGrn.Size = new System.Drawing.Size(1319, 360);
            this.dgvGrn.TabIndex = 10;
            this.dgvGrn.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvGrn_CellBeginEdit);
            this.dgvGrn.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGrn_CellEndEdit);
            this.dgvGrn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvGrn_KeyDown);
            // 
            // colItemKey
            // 
            this.colItemKey.FillWeight = 40F;
            this.colItemKey.HeaderText = "Item Key";
            this.colItemKey.MaxInputLength = 50;
            this.colItemKey.MinimumWidth = 6;
            this.colItemKey.Name = "colItemKey";
            this.colItemKey.ReadOnly = true;
            this.colItemKey.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colItemKey.Visible = false;
            // 
            // colItemCode
            // 
            this.colItemCode.FillWeight = 91.14907F;
            this.colItemCode.HeaderText = "Item Code";
            this.colItemCode.MaxInputLength = 50;
            this.colItemCode.MinimumWidth = 6;
            this.colItemCode.Name = "colItemCode";
            // 
            // colItemName
            // 
            this.colItemName.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.colItemName.FillWeight = 91.14907F;
            this.colItemName.HeaderText = "Item Name";
            this.colItemName.MinimumWidth = 6;
            this.colItemName.Name = "colItemName";
            // 
            // colUnit
            // 
            this.colUnit.FillWeight = 46.39323F;
            this.colUnit.HeaderText = "Unit";
            this.colUnit.MaxInputLength = 50;
            this.colUnit.MinimumWidth = 6;
            this.colUnit.Name = "colUnit";
            this.colUnit.ReadOnly = true;
            // 
            // colCostPrice
            // 
            this.colCostPrice.HeaderText = "Cost Price";
            this.colCostPrice.MaxInputLength = 20;
            this.colCostPrice.MinimumWidth = 10;
            this.colCostPrice.Name = "colCostPrice";
            this.colCostPrice.Visible = false;
            // 
            // colTranPrice
            // 
            this.colTranPrice.FillWeight = 91.14907F;
            this.colTranPrice.HeaderText = "Tran Price";
            this.colTranPrice.MaxInputLength = 20;
            this.colTranPrice.MinimumWidth = 6;
            this.colTranPrice.Name = "colTranPrice";
            // 
            // colSalePrice
            // 
            this.colSalePrice.FillWeight = 91.14907F;
            this.colSalePrice.HeaderText = "Sale Price";
            this.colSalePrice.MaxInputLength = 20;
            this.colSalePrice.MinimumWidth = 6;
            this.colSalePrice.Name = "colSalePrice";
            // 
            // colQuantity
            // 
            this.colQuantity.FillWeight = 91.14907F;
            this.colQuantity.HeaderText = "Quantity";
            this.colQuantity.MaxInputLength = 15;
            this.colQuantity.MinimumWidth = 6;
            this.colQuantity.Name = "colQuantity";
            // 
            // colAmount
            // 
            this.colAmount.FillWeight = 91.14907F;
            this.colAmount.HeaderText = "Amount";
            this.colAmount.MaxInputLength = 50;
            this.colAmount.MinimumWidth = 6;
            this.colAmount.Name = "colAmount";
            // 
            // colItemTranKey
            // 
            this.colItemTranKey.HeaderText = "Item Tran Key";
            this.colItemTranKey.MaxInputLength = 50;
            this.colItemTranKey.MinimumWidth = 6;
            this.colItemTranKey.Name = "colItemTranKey";
            this.colItemTranKey.Visible = false;
            // 
            // colUpdate
            // 
            this.colUpdate.HeaderText = "Update";
            this.colUpdate.MaxInputLength = 5;
            this.colUpdate.MinimumWidth = 6;
            this.colUpdate.Name = "colUpdate";
            this.colUpdate.Visible = false;
            // 
            // colDelete
            // 
            this.colDelete.HeaderText = "Delete";
            this.colDelete.MaxInputLength = 5;
            this.colDelete.MinimumWidth = 6;
            this.colDelete.Name = "colDelete";
            this.colDelete.Visible = false;
            // 
            // colExpireDate
            // 
            this.colExpireDate.FillWeight = 91.14907F;
            this.colExpireDate.HeaderText = "Expire Date";
            this.colExpireDate.MaxInputLength = 15;
            this.colExpireDate.MinimumWidth = 6;
            this.colExpireDate.Name = "colExpireDate";
            // 
            // colBatchNo
            // 
            this.colBatchNo.FillWeight = 91.14907F;
            this.colBatchNo.HeaderText = "Batch No";
            this.colBatchNo.MaxInputLength = 20;
            this.colBatchNo.MinimumWidth = 6;
            this.colBatchNo.Name = "colBatchNo";
            // 
            // colExistQuantity
            // 
            this.colExistQuantity.HeaderText = "Exist Quantity";
            this.colExistQuantity.MaxInputLength = 20;
            this.colExistQuantity.MinimumWidth = 6;
            this.colExistQuantity.Name = "colExistQuantity";
            this.colExistQuantity.Visible = false;
            // 
            // colExistItemKey
            // 
            this.colExistItemKey.HeaderText = "Exist Item Key";
            this.colExistItemKey.MaxInputLength = 50;
            this.colExistItemKey.MinimumWidth = 6;
            this.colExistItemKey.Name = "colExistItemKey";
            this.colExistItemKey.Visible = false;
            // 
            // colExistExpireDate
            // 
            this.colExistExpireDate.HeaderText = "Exist Expire Date";
            this.colExistExpireDate.MaxInputLength = 15;
            this.colExistExpireDate.MinimumWidth = 6;
            this.colExistExpireDate.Name = "colExistExpireDate";
            this.colExistExpireDate.Visible = false;
            // 
            // colExistCostPrice
            // 
            this.colExistCostPrice.HeaderText = "Exist Cost Price";
            this.colExistCostPrice.MaxInputLength = 20;
            this.colExistCostPrice.MinimumWidth = 6;
            this.colExistCostPrice.Name = "colExistCostPrice";
            this.colExistCostPrice.Visible = false;
            // 
            // colInhand
            // 
            this.colInhand.FillWeight = 91.14907F;
            this.colInhand.HeaderText = "InHand";
            this.colInhand.MaxInputLength = 20;
            this.colInhand.MinimumWidth = 6;
            this.colInhand.Name = "colInhand";
            this.colInhand.ReadOnly = true;
            // 
            // colSave
            // 
            this.colSave.FillWeight = 30F;
            this.colSave.HeaderText = "Save";
            this.colSave.MinimumWidth = 6;
            this.colSave.Name = "colSave";
            this.colSave.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colSave.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // roundedPanel2
            // 
            this.roundedPanel2.BackColor = System.Drawing.SystemColors.Menu;
            this.roundedPanel2.BorderColor = System.Drawing.SystemColors.ScrollBar;
            this.roundedPanel2.BorderSize = 0;
            this.roundedPanel2.Controls.Add(this.txtDescription);
            this.roundedPanel2.Controls.Add(this.cmbAddress);
            this.roundedPanel2.Controls.Add(this.cmbSupplierName);
            this.roundedPanel2.Controls.Add(this.cmbSupplierCode);
            this.roundedPanel2.Controls.Add(this.label7);
            this.roundedPanel2.Controls.Add(this.label6);
            this.roundedPanel2.Controls.Add(this.lblCustomer);
            this.roundedPanel2.CornerRadius = 6;
            this.roundedPanel2.Location = new System.Drawing.Point(34, 51);
            this.roundedPanel2.Name = "roundedPanel2";
            this.roundedPanel2.Size = new System.Drawing.Size(626, 117);
            this.roundedPanel2.TabIndex = 1;
            // 
            // txtDescription
            // 
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescription.Location = new System.Drawing.Point(140, 83);
            this.txtDescription.MaxLength = 100;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(476, 22);
            this.txtDescription.TabIndex = 7;
            this.txtDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDescription_KeyDown);
            // 
            // cmbAddress
            // 
            this.cmbAddress.DropDownHeight = 120;
            this.cmbAddress.FormattingEnabled = true;
            this.cmbAddress.IntegralHeight = false;
            this.cmbAddress.Location = new System.Drawing.Point(140, 47);
            this.cmbAddress.Name = "cmbAddress";
            this.cmbAddress.Size = new System.Drawing.Size(476, 24);
            this.cmbAddress.TabIndex = 6;
            this.cmbAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbAddress_KeyDown);
            // 
            // cmbSupplierName
            // 
            this.cmbSupplierName.DropDownHeight = 120;
            this.cmbSupplierName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSupplierName.FormattingEnabled = true;
            this.cmbSupplierName.IntegralHeight = false;
            this.cmbSupplierName.Location = new System.Drawing.Point(274, 13);
            this.cmbSupplierName.Name = "cmbSupplierName";
            this.cmbSupplierName.Size = new System.Drawing.Size(342, 24);
            this.cmbSupplierName.TabIndex = 5;
            this.cmbSupplierName.SelectedIndexChanged += new System.EventHandler(this.cmbSupplierName_SelectedIndexChanged);
            this.cmbSupplierName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbSupplierName_KeyDown);
            this.cmbSupplierName.Leave += new System.EventHandler(this.cmbSupplierName_Leave);
            // 
            // cmbSupplierCode
            // 
            this.cmbSupplierCode.DropDownHeight = 120;
            this.cmbSupplierCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSupplierCode.FormattingEnabled = true;
            this.cmbSupplierCode.IntegralHeight = false;
            this.cmbSupplierCode.Location = new System.Drawing.Point(140, 13);
            this.cmbSupplierCode.Name = "cmbSupplierCode";
            this.cmbSupplierCode.Size = new System.Drawing.Size(126, 24);
            this.cmbSupplierCode.TabIndex = 4;
            this.cmbSupplierCode.SelectedIndexChanged += new System.EventHandler(this.cmbSupplierCode_SelectedIndexChanged);
            this.cmbSupplierCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbSupplierCode_KeyDown);
            this.cmbSupplierCode.Leave += new System.EventHandler(this.cmbSupplierCode_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(7, 83);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 18);
            this.label7.TabIndex = 3;
            this.label7.Text = "Description";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(7, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(110, 18);
            this.label6.TabIndex = 2;
            this.label6.Text = "Address Name";
            // 
            // lblCustomer
            // 
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomer.Location = new System.Drawing.Point(7, 15);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(68, 18);
            this.lblCustomer.TabIndex = 1;
            this.lblCustomer.Text = "Supplier";
            // 
            // roundedPanel6
            // 
            this.roundedPanel6.BackColor = System.Drawing.SystemColors.Menu;
            this.roundedPanel6.BorderColor = System.Drawing.SystemColors.ScrollBar;
            this.roundedPanel6.BorderSize = 0;
            this.roundedPanel6.Controls.Add(this.cmbPaymentTerm);
            this.roundedPanel6.Controls.Add(this.cmbPurchaseAccount);
            this.roundedPanel6.Controls.Add(this.label8);
            this.roundedPanel6.Controls.Add(this.lblSalesAccount);
            this.roundedPanel6.CornerRadius = 6;
            this.roundedPanel6.Location = new System.Drawing.Point(736, 51);
            this.roundedPanel6.Name = "roundedPanel6";
            this.roundedPanel6.Size = new System.Drawing.Size(557, 117);
            this.roundedPanel6.TabIndex = 5;
            // 
            // cmbPaymentTerm
            // 
            this.cmbPaymentTerm.DropDownHeight = 120;
            this.cmbPaymentTerm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaymentTerm.FormattingEnabled = true;
            this.cmbPaymentTerm.IntegralHeight = false;
            this.cmbPaymentTerm.Location = new System.Drawing.Point(170, 45);
            this.cmbPaymentTerm.Name = "cmbPaymentTerm";
            this.cmbPaymentTerm.Size = new System.Drawing.Size(378, 24);
            this.cmbPaymentTerm.TabIndex = 9;
            this.cmbPaymentTerm.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbPaymentTerm_KeyDown);
            // 
            // cmbPurchaseAccount
            // 
            this.cmbPurchaseAccount.DropDownHeight = 120;
            this.cmbPurchaseAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPurchaseAccount.FormattingEnabled = true;
            this.cmbPurchaseAccount.IntegralHeight = false;
            this.cmbPurchaseAccount.Location = new System.Drawing.Point(170, 11);
            this.cmbPurchaseAccount.Name = "cmbPurchaseAccount";
            this.cmbPurchaseAccount.Size = new System.Drawing.Size(378, 24);
            this.cmbPurchaseAccount.TabIndex = 8;
            this.cmbPurchaseAccount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbPurchaseAccount_KeyDown);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(12, 47);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(108, 18);
            this.label8.TabIndex = 3;
            this.label8.Text = "Payment Term";
            // 
            // lblSalesAccount
            // 
            this.lblSalesAccount.AutoSize = true;
            this.lblSalesAccount.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSalesAccount.Location = new System.Drawing.Point(12, 13);
            this.lblSalesAccount.Name = "lblSalesAccount";
            this.lblSalesAccount.Size = new System.Drawing.Size(135, 18);
            this.lblSalesAccount.TabIndex = 2;
            this.lblSalesAccount.Text = "Purchase Account";
            // 
            // roundedPanel1
            // 
            this.roundedPanel1.BackColor = System.Drawing.SystemColors.MenuBar;
            this.roundedPanel1.BorderColor = System.Drawing.SystemColors.ScrollBar;
            this.roundedPanel1.BorderSize = 0;
            this.roundedPanel1.Controls.Add(this.dtpGrnDate);
            this.roundedPanel1.Controls.Add(this.cmbLocation);
            this.roundedPanel1.Controls.Add(this.cmbReference);
            this.roundedPanel1.Controls.Add(this.cmbGrnNo);
            this.roundedPanel1.Controls.Add(this.lblLocation);
            this.roundedPanel1.Controls.Add(this.lblYourReferences);
            this.roundedPanel1.Controls.Add(this.lblTranDate);
            this.roundedPanel1.Controls.Add(this.lblInvoiceNo);
            this.roundedPanel1.CornerRadius = 6;
            this.roundedPanel1.Location = new System.Drawing.Point(6, 3);
            this.roundedPanel1.Name = "roundedPanel1";
            this.roundedPanel1.Size = new System.Drawing.Size(1319, 174);
            this.roundedPanel1.TabIndex = 0;
            // 
            // dtpGrnDate
            // 
            this.dtpGrnDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpGrnDate.Location = new System.Drawing.Point(431, 9);
            this.dtpGrnDate.Name = "dtpGrnDate";
            this.dtpGrnDate.Size = new System.Drawing.Size(166, 22);
            this.dtpGrnDate.TabIndex = 1;
            this.dtpGrnDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpGrnDate_KeyDown);
            // 
            // cmbLocation
            // 
            this.cmbLocation.DropDownHeight = 120;
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.IntegralHeight = false;
            this.cmbLocation.Location = new System.Drawing.Point(1133, 10);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(166, 24);
            this.cmbLocation.TabIndex = 3;
            this.cmbLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbLocation_KeyDown);
            // 
            // cmbReference
            // 
            this.cmbReference.DropDownHeight = 120;
            this.cmbReference.FormattingEnabled = true;
            this.cmbReference.IntegralHeight = false;
            this.cmbReference.Location = new System.Drawing.Point(808, 9);
            this.cmbReference.Name = "cmbReference";
            this.cmbReference.Size = new System.Drawing.Size(166, 24);
            this.cmbReference.TabIndex = 2;
            this.cmbReference.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbReference_KeyDown);
            // 
            // cmbGrnNo
            // 
            this.cmbGrnNo.DropDownHeight = 120;
            this.cmbGrnNo.FormattingEnabled = true;
            this.cmbGrnNo.IntegralHeight = false;
            this.cmbGrnNo.Location = new System.Drawing.Point(119, 9);
            this.cmbGrnNo.Name = "cmbGrnNo";
            this.cmbGrnNo.Size = new System.Drawing.Size(166, 24);
            this.cmbGrnNo.TabIndex = 0;
            this.cmbGrnNo.Enter += new System.EventHandler(this.cmbGrnNo_Enter);
            this.cmbGrnNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbGrnNo_KeyDown);
            this.cmbGrnNo.Leave += new System.EventHandler(this.cmbGrnNo_Leave);
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocation.Location = new System.Drawing.Point(1058, 13);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(69, 18);
            this.lblLocation.TabIndex = 3;
            this.lblLocation.Text = "Location";
            // 
            // lblYourReferences
            // 
            this.lblYourReferences.AutoSize = true;
            this.lblYourReferences.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYourReferences.Location = new System.Drawing.Point(682, 13);
            this.lblYourReferences.Name = "lblYourReferences";
            this.lblYourReferences.Size = new System.Drawing.Size(120, 18);
            this.lblYourReferences.TabIndex = 2;
            this.lblYourReferences.Text = "Your Reference";
            // 
            // lblTranDate
            // 
            this.lblTranDate.AutoSize = true;
            this.lblTranDate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTranDate.Location = new System.Drawing.Point(346, 12);
            this.lblTranDate.Name = "lblTranDate";
            this.lblTranDate.Size = new System.Drawing.Size(79, 18);
            this.lblTranDate.TabIndex = 1;
            this.lblTranDate.Text = "GRN Date";
            // 
            // lblInvoiceNo
            // 
            this.lblInvoiceNo.AutoSize = true;
            this.lblInvoiceNo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInvoiceNo.Location = new System.Drawing.Point(8, 12);
            this.lblInvoiceNo.Name = "lblInvoiceNo";
            this.lblInvoiceNo.Size = new System.Drawing.Size(102, 18);
            this.lblInvoiceNo.TabIndex = 0;
            this.lblInvoiceNo.Text = "GRN Number";
            // 
            // frmReceiveItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1334, 686);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmReceiveItems";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Good Recieved Note Entry";
            this.Activated += new System.EventHandler(this.frmReceiveItems_Activated);
            this.Load += new System.EventHandler(this.frmReceiveItems_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmReceiveItems_KeyDown);
            this.panel1.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchItems)).EndInit();
            this.roundedPanel5.ResumeLayout(false);
            this.roundedPanel4.ResumeLayout(false);
            this.roundedPanel4.PerformLayout();
            this.roundedPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrn)).EndInit();
            this.roundedPanel2.ResumeLayout(false);
            this.roundedPanel2.PerformLayout();
            this.roundedPanel6.ResumeLayout(false);
            this.roundedPanel6.PerformLayout();
            this.roundedPanel1.ResumeLayout(false);
            this.roundedPanel1.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Panel panel1;
        private Custom_Components.RoundedPanel roundedPanel1;
        private Custom_Components.RoundedPanel roundedPanel2;
        private Custom_Components.RoundedPanel roundedPanel5;
        private Custom_Components.RoundedPanel roundedPanel4;
        private Custom_Components.RoundedPanel roundedPanel3;
        private System.Windows.Forms.Label lblInvoiceNo;
        private System.Windows.Forms.ComboBox cmbReference;
        private System.Windows.Forms.ComboBox cmbGrnNo;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.Label lblYourReferences;
        private System.Windows.Forms.Label lblTranDate;
        private System.Windows.Forms.DateTimePicker dtpGrnDate;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblCustomer;
        private Custom_Components.RoundedPanel roundedPanel6;
        private System.Windows.Forms.ComboBox cmbPaymentTerm;
        private System.Windows.Forms.ComboBox cmbPurchaseAccount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblSalesAccount;
        private System.Windows.Forms.ComboBox cmbAddress;
        private System.Windows.Forms.ComboBox cmbSupplierName;
        private System.Windows.Forms.ComboBox cmbSupplierCode;
        private System.Windows.Forms.DataGridView dgvGrn;
        private System.Windows.Forms.TextBox txtTotalAmount;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Button btnHide;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSearchBox;
        private System.Windows.Forms.DataGridView dgvSearchItems;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItemKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItemCode;
        private System.Windows.Forms.DataGridViewComboBoxColumn colItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCostPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTranPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSalePrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItemTranKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUpdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn colExpireDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBatchNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colExistQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colExistItemKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn colExistExpireDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colExistCostPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInhand;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSave;
    }
}



