namespace POS_System.Forms
{
    partial class frmPurchaseReturn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPurchaseReturn));
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvGre = new System.Windows.Forms.DataGridView();
            this.roundedPanel5 = new POS_System.Custom_Components.RoundedPanel();
            this.btnHide = new System.Windows.Forms.Button();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.roundedPanel4 = new POS_System.Custom_Components.RoundedPanel();
            this.txtTotalAmount = new System.Windows.Forms.TextBox();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.roundedPanel2 = new POS_System.Custom_Components.RoundedPanel();
            this.txtDocumentNo = new System.Windows.Forms.TextBox();
            this.cmbAddress = new System.Windows.Forms.ComboBox();
            this.cmbAccountName = new System.Windows.Forms.ComboBox();
            this.cmbAccountCode = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.roundedPanel6 = new POS_System.Custom_Components.RoundedPanel();
            this.cmbPaymentTerm = new System.Windows.Forms.ComboBox();
            this.cmbPurchaseAccount = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.lblSalesAccount = new System.Windows.Forms.Label();
            this.roundedPanel3 = new POS_System.Custom_Components.RoundedPanel();
            this.dtpReturnDate = new System.Windows.Forms.DateTimePicker();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.cmbYourReference = new System.Windows.Forms.ComboBox();
            this.cmbReturnNo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGre)).BeginInit();
            this.roundedPanel5.SuspendLayout();
            this.roundedPanel4.SuspendLayout();
            this.roundedPanel2.SuspendLayout();
            this.roundedPanel6.SuspendLayout();
            this.roundedPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.dgvGre);
            this.panel1.Controls.Add(this.roundedPanel5);
            this.panel1.Controls.Add(this.roundedPanel4);
            this.panel1.Controls.Add(this.roundedPanel2);
            this.panel1.Controls.Add(this.roundedPanel6);
            this.panel1.Controls.Add(this.roundedPanel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1328, 679);
            this.panel1.TabIndex = 1;
            // 
            // dgvGre
            // 
            this.dgvGre.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvGre.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGre.Location = new System.Drawing.Point(4, 185);
            this.dgvGre.Name = "dgvGre";
            this.dgvGre.RowHeadersWidth = 51;
            this.dgvGre.RowTemplate.Height = 24;
            this.dgvGre.Size = new System.Drawing.Size(1319, 366);
            this.dgvGre.TabIndex = 10;
            this.dgvGre.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvGre_CellBeginEdit);
            this.dgvGre.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGre_CellEndEdit);
            this.dgvGre.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvGre_KeyDown);
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
            this.roundedPanel5.Location = new System.Drawing.Point(3, 606);
            this.roundedPanel5.Name = "roundedPanel5";
            this.roundedPanel5.Size = new System.Drawing.Size(1323, 70);
            this.roundedPanel5.TabIndex = 10;
            // 
            // btnHide
            // 
            this.btnHide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHide.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHide.Location = new System.Drawing.Point(1200, 17);
            this.btnHide.Name = "btnHide";
            this.btnHide.Size = new System.Drawing.Size(112, 36);
            this.btnHide.TabIndex = 15;
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
            this.btnPreview.TabIndex = 14;
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
            this.btnSave.TabIndex = 13;
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
            this.btnDelete.TabIndex = 12;
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
            this.btnAdd.TabIndex = 11;
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
            this.roundedPanel4.Location = new System.Drawing.Point(4, 557);
            this.roundedPanel4.Name = "roundedPanel4";
            this.roundedPanel4.Size = new System.Drawing.Size(1322, 46);
            this.roundedPanel4.TabIndex = 9;
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalAmount.Location = new System.Drawing.Point(1038, 10);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.Size = new System.Drawing.Size(272, 27);
            this.txtTotalAmount.TabIndex = 11;
            this.txtTotalAmount.TabStop = false;
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
            // roundedPanel2
            // 
            this.roundedPanel2.BackColor = System.Drawing.SystemColors.MenuBar;
            this.roundedPanel2.BorderColor = System.Drawing.SystemColors.GrayText;
            this.roundedPanel2.BorderSize = 0;
            this.roundedPanel2.Controls.Add(this.txtDocumentNo);
            this.roundedPanel2.Controls.Add(this.cmbAddress);
            this.roundedPanel2.Controls.Add(this.cmbAccountName);
            this.roundedPanel2.Controls.Add(this.cmbAccountCode);
            this.roundedPanel2.Controls.Add(this.label7);
            this.roundedPanel2.Controls.Add(this.label6);
            this.roundedPanel2.Controls.Add(this.lblCustomer);
            this.roundedPanel2.CornerRadius = 6;
            this.roundedPanel2.Location = new System.Drawing.Point(31, 53);
            this.roundedPanel2.Name = "roundedPanel2";
            this.roundedPanel2.Size = new System.Drawing.Size(626, 117);
            this.roundedPanel2.TabIndex = 7;
            // 
            // txtDocumentNo
            // 
            this.txtDocumentNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDocumentNo.Location = new System.Drawing.Point(140, 83);
            this.txtDocumentNo.MaxLength = 10;
            this.txtDocumentNo.Name = "txtDocumentNo";
            this.txtDocumentNo.Size = new System.Drawing.Size(128, 22);
            this.txtDocumentNo.TabIndex = 7;
            this.txtDocumentNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDocumentNo_KeyDown);
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
            // cmbAccountName
            // 
            this.cmbAccountName.DropDownHeight = 120;
            this.cmbAccountName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAccountName.FormattingEnabled = true;
            this.cmbAccountName.IntegralHeight = false;
            this.cmbAccountName.Location = new System.Drawing.Point(274, 13);
            this.cmbAccountName.Name = "cmbAccountName";
            this.cmbAccountName.Size = new System.Drawing.Size(342, 24);
            this.cmbAccountName.TabIndex = 5;
            this.cmbAccountName.SelectedIndexChanged += new System.EventHandler(this.cmbAccountName_SelectedIndexChanged);
            this.cmbAccountName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbAccountName_KeyDown);
            // 
            // cmbAccountCode
            // 
            this.cmbAccountCode.DropDownHeight = 120;
            this.cmbAccountCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAccountCode.FormattingEnabled = true;
            this.cmbAccountCode.IntegralHeight = false;
            this.cmbAccountCode.Location = new System.Drawing.Point(140, 13);
            this.cmbAccountCode.Name = "cmbAccountCode";
            this.cmbAccountCode.Size = new System.Drawing.Size(126, 24);
            this.cmbAccountCode.TabIndex = 4;
            this.cmbAccountCode.SelectedIndexChanged += new System.EventHandler(this.cmbAccountCode_SelectedIndexChanged);
            this.cmbAccountCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbAccountCode_KeyDown);
            this.cmbAccountCode.Leave += new System.EventHandler(this.cmbAccountCode_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(7, 83);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(104, 18);
            this.label7.TabIndex = 3;
            this.label7.Text = "Document No";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(7, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 18);
            this.label6.TabIndex = 2;
            this.label6.Text = "Address";
            // 
            // lblCustomer
            // 
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomer.Location = new System.Drawing.Point(7, 15);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(65, 18);
            this.lblCustomer.TabIndex = 1;
            this.lblCustomer.Text = "Account";
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
            this.roundedPanel6.Location = new System.Drawing.Point(733, 53);
            this.roundedPanel6.Name = "roundedPanel6";
            this.roundedPanel6.Size = new System.Drawing.Size(557, 117);
            this.roundedPanel6.TabIndex = 8;
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
            // roundedPanel3
            // 
            this.roundedPanel3.BackColor = System.Drawing.SystemColors.MenuBar;
            this.roundedPanel3.BorderColor = System.Drawing.SystemColors.ScrollBar;
            this.roundedPanel3.BorderSize = 0;
            this.roundedPanel3.Controls.Add(this.dtpReturnDate);
            this.roundedPanel3.Controls.Add(this.cmbLocation);
            this.roundedPanel3.Controls.Add(this.cmbYourReference);
            this.roundedPanel3.Controls.Add(this.cmbReturnNo);
            this.roundedPanel3.Controls.Add(this.label1);
            this.roundedPanel3.Controls.Add(this.label2);
            this.roundedPanel3.Controls.Add(this.label3);
            this.roundedPanel3.Controls.Add(this.label4);
            this.roundedPanel3.CornerRadius = 6;
            this.roundedPanel3.Location = new System.Drawing.Point(4, 5);
            this.roundedPanel3.Name = "roundedPanel3";
            this.roundedPanel3.Size = new System.Drawing.Size(1319, 174);
            this.roundedPanel3.TabIndex = 6;
            // 
            // dtpReturnDate
            // 
            this.dtpReturnDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpReturnDate.Location = new System.Drawing.Point(458, 9);
            this.dtpReturnDate.Name = "dtpReturnDate";
            this.dtpReturnDate.Size = new System.Drawing.Size(166, 22);
            this.dtpReturnDate.TabIndex = 1;
            this.dtpReturnDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpReturnDate_KeyDown);
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
            // cmbYourReference
            // 
            this.cmbYourReference.DropDownHeight = 120;
            this.cmbYourReference.FormattingEnabled = true;
            this.cmbYourReference.IntegralHeight = false;
            this.cmbYourReference.Location = new System.Drawing.Point(826, 9);
            this.cmbYourReference.Name = "cmbYourReference";
            this.cmbYourReference.Size = new System.Drawing.Size(166, 24);
            this.cmbYourReference.TabIndex = 2;
            this.cmbYourReference.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbYourReference_KeyDown);
            // 
            // cmbReturnNo
            // 
            this.cmbReturnNo.DropDownHeight = 120;
            this.cmbReturnNo.FormattingEnabled = true;
            this.cmbReturnNo.IntegralHeight = false;
            this.cmbReturnNo.Location = new System.Drawing.Point(130, 9);
            this.cmbReturnNo.Name = "cmbReturnNo";
            this.cmbReturnNo.Size = new System.Drawing.Size(166, 24);
            this.cmbReturnNo.TabIndex = 0;
            this.cmbReturnNo.Enter += new System.EventHandler(this.cmbReturnNo_Enter);
            this.cmbReturnNo.Leave += new System.EventHandler(this.cmbReturnNo_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1058, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 18);
            this.label1.TabIndex = 3;
            this.label1.Text = "Location";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(700, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Your Reference";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(359, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 18);
            this.label3.TabIndex = 1;
            this.label3.Text = "Return Date";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 18);
            this.label4.TabIndex = 0;
            this.label4.Text = "Return Number";
            // 
            // frmPurchaseReturn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1328, 679);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmPurchaseReturn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Goods Return Entry";
            this.Activated += new System.EventHandler(this.frmPurchaseReturn_Activated);
            this.Load += new System.EventHandler(this.frmPurchaseReturn_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGre)).EndInit();
            this.roundedPanel5.ResumeLayout(false);
            this.roundedPanel4.ResumeLayout(false);
            this.roundedPanel4.PerformLayout();
            this.roundedPanel2.ResumeLayout(false);
            this.roundedPanel2.PerformLayout();
            this.roundedPanel6.ResumeLayout(false);
            this.roundedPanel6.PerformLayout();
            this.roundedPanel3.ResumeLayout(false);
            this.roundedPanel3.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Panel panel1;
        private Custom_Components.RoundedPanel roundedPanel2;
        private System.Windows.Forms.TextBox txtDocumentNo;
        private System.Windows.Forms.ComboBox cmbAddress;
        private System.Windows.Forms.ComboBox cmbAccountName;
        private System.Windows.Forms.ComboBox cmbAccountCode;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblCustomer;
        private Custom_Components.RoundedPanel roundedPanel6;
        private System.Windows.Forms.ComboBox cmbPaymentTerm;
        private System.Windows.Forms.ComboBox cmbPurchaseAccount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblSalesAccount;
        private Custom_Components.RoundedPanel roundedPanel3;
        private System.Windows.Forms.DateTimePicker dtpReturnDate;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.ComboBox cmbYourReference;
        private System.Windows.Forms.ComboBox cmbReturnNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private Custom_Components.RoundedPanel roundedPanel5;
        private System.Windows.Forms.Button btnHide;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdd;
        private Custom_Components.RoundedPanel roundedPanel4;
        private System.Windows.Forms.TextBox txtTotalAmount;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.DataGridView dgvGre;
    }
}



