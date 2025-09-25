namespace POS_System.Forms
{
    partial class frmJournal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmJournal));
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbOurRef = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpVoucherdate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbVoucherNo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.roundedPanel4 = new POS_System.Custom_Components.RoundedPanel();
            this.btnHide = new System.Windows.Forms.Button();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.roundedPanel3 = new POS_System.Custom_Components.RoundedPanel();
            this.lblDifferenceAmount = new System.Windows.Forms.Label();
            this.lblCrSumAmount = new System.Windows.Forms.Label();
            this.lblDrSumAmount = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.roundedPanel2 = new POS_System.Custom_Components.RoundedPanel();
            this.dgvJournal = new System.Windows.Forms.DataGridView();
            this.roundedPanel1 = new POS_System.Custom_Components.RoundedPanel();
            this.panel1.SuspendLayout();
            this.roundedPanel4.SuspendLayout();
            this.roundedPanel3.SuspendLayout();
            this.roundedPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJournal)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(48)))), ((int)(((byte)(65)))));
            this.panel1.Controls.Add(this.roundedPanel4);
            this.panel1.Controls.Add(this.roundedPanel3);
            this.panel1.Controls.Add(this.roundedPanel2);
            this.panel1.Controls.Add(this.cmbOurRef);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.dtpVoucherdate);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cmbVoucherNo);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.roundedPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(883, 570);
            this.panel1.TabIndex = 5;
            // 
            // cmbOurRef
            // 
            this.cmbOurRef.FormattingEnabled = true;
            this.cmbOurRef.Location = new System.Drawing.Point(760, 19);
            this.cmbOurRef.Name = "cmbOurRef";
            this.cmbOurRef.Size = new System.Drawing.Size(99, 26);
            this.cmbOurRef.TabIndex = 2;
            this.cmbOurRef.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbOurRef_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(680, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 18);
            this.label3.TabIndex = 4;
            this.label3.Text = "Our Ref :";
            // 
            // dtpVoucherdate
            // 
            this.dtpVoucherdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpVoucherdate.Location = new System.Drawing.Point(429, 20);
            this.dtpVoucherdate.Name = "dtpVoucherdate";
            this.dtpVoucherdate.Size = new System.Drawing.Size(142, 24);
            this.dtpVoucherdate.TabIndex = 1;
            this.dtpVoucherdate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpVoucherdate_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(305, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Voucher Date :";
            // 
            // cmbVoucherNo
            // 
            this.cmbVoucherNo.FormattingEnabled = true;
            this.cmbVoucherNo.Location = new System.Drawing.Point(137, 19);
            this.cmbVoucherNo.Name = "cmbVoucherNo";
            this.cmbVoucherNo.Size = new System.Drawing.Size(99, 26);
            this.cmbVoucherNo.TabIndex = 0;
            this.cmbVoucherNo.Enter += new System.EventHandler(this.cmbVoucherNo_Enter);
            this.cmbVoucherNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbVoucherNo_KeyDown);
            this.cmbVoucherNo.Leave += new System.EventHandler(this.cmbVoucherNo_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(26, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Voucher No :";
            // 
            // roundedPanel4
            // 
            this.roundedPanel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.roundedPanel4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.roundedPanel4.BorderSize = 1;
            this.roundedPanel4.Controls.Add(this.btnHide);
            this.roundedPanel4.Controls.Add(this.btnPreview);
            this.roundedPanel4.Controls.Add(this.btnSave);
            this.roundedPanel4.Controls.Add(this.btnDelete);
            this.roundedPanel4.Controls.Add(this.btnAdd);
            this.roundedPanel4.CornerRadius = 8;
            this.roundedPanel4.Location = new System.Drawing.Point(306, 505);
            this.roundedPanel4.Name = "roundedPanel4";
            this.roundedPanel4.Size = new System.Drawing.Size(570, 54);
            this.roundedPanel4.TabIndex = 10;
            // 
            // btnHide
            // 
            this.btnHide.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHide.Image = ((System.Drawing.Image)(resources.GetObject("btnHide.Image")));
            this.btnHide.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHide.Location = new System.Drawing.Point(461, 10);
            this.btnHide.Name = "btnHide";
            this.btnHide.Size = new System.Drawing.Size(99, 33);
            this.btnHide.TabIndex = 9;
            this.btnHide.Text = "  Hide";
            this.btnHide.UseVisualStyleBackColor = true;
            this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
            // 
            // btnPreview
            // 
            this.btnPreview.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPreview.Image = ((System.Drawing.Image)(resources.GetObject("btnPreview.Image")));
            this.btnPreview.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPreview.Location = new System.Drawing.Point(348, 10);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(99, 33);
            this.btnPreview.TabIndex = 8;
            this.btnPreview.Text = "    Preview";
            this.btnPreview.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(235, 10);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(99, 33);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "    Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(122, 10);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(99, 33);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Text = "    Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(9, 10);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(99, 33);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "   Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // roundedPanel3
            // 
            this.roundedPanel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.roundedPanel3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.roundedPanel3.BorderSize = 1;
            this.roundedPanel3.Controls.Add(this.lblDifferenceAmount);
            this.roundedPanel3.Controls.Add(this.lblCrSumAmount);
            this.roundedPanel3.Controls.Add(this.lblDrSumAmount);
            this.roundedPanel3.Controls.Add(this.label6);
            this.roundedPanel3.Controls.Add(this.label5);
            this.roundedPanel3.Controls.Add(this.txtRemark);
            this.roundedPanel3.Controls.Add(this.label4);
            this.roundedPanel3.CornerRadius = 8;
            this.roundedPanel3.Location = new System.Drawing.Point(6, 408);
            this.roundedPanel3.Name = "roundedPanel3";
            this.roundedPanel3.Size = new System.Drawing.Size(870, 89);
            this.roundedPanel3.TabIndex = 8;
            // 
            // lblDifferenceAmount
            // 
            this.lblDifferenceAmount.AutoSize = true;
            this.lblDifferenceAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDifferenceAmount.ForeColor = System.Drawing.Color.White;
            this.lblDifferenceAmount.Location = new System.Drawing.Point(121, 59);
            this.lblDifferenceAmount.Name = "lblDifferenceAmount";
            this.lblDifferenceAmount.Size = new System.Drawing.Size(0, 20);
            this.lblDifferenceAmount.TabIndex = 9;
            // 
            // lblCrSumAmount
            // 
            this.lblCrSumAmount.AutoSize = true;
            this.lblCrSumAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCrSumAmount.ForeColor = System.Drawing.Color.White;
            this.lblCrSumAmount.Location = new System.Drawing.Point(245, 11);
            this.lblCrSumAmount.Name = "lblCrSumAmount";
            this.lblCrSumAmount.Size = new System.Drawing.Size(0, 20);
            this.lblCrSumAmount.TabIndex = 8;
            // 
            // lblDrSumAmount
            // 
            this.lblDrSumAmount.AutoSize = true;
            this.lblDrSumAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDrSumAmount.ForeColor = System.Drawing.Color.White;
            this.lblDrSumAmount.Location = new System.Drawing.Point(121, 13);
            this.lblDrSumAmount.Name = "lblDrSumAmount";
            this.lblDrSumAmount.Size = new System.Drawing.Size(0, 20);
            this.lblDrSumAmount.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(20, 59);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 18);
            this.label6.TabIndex = 6;
            this.label6.Text = "Difference :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(20, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 18);
            this.label5.TabIndex = 5;
            this.label5.Text = "Total :";
            // 
            // txtRemark
            // 
            this.txtRemark.BackColor = System.Drawing.Color.LightGray;
            this.txtRemark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemark.Location = new System.Drawing.Point(514, 11);
            this.txtRemark.MaxLength = 120;
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(347, 66);
            this.txtRemark.TabIndex = 4;
            this.txtRemark.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRemark_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(436, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 18);
            this.label4.TabIndex = 1;
            this.label4.Text = "Remark :";
            // 
            // roundedPanel2
            // 
            this.roundedPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.roundedPanel2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.roundedPanel2.BorderSize = 1;
            this.roundedPanel2.Controls.Add(this.dgvJournal);
            this.roundedPanel2.CornerRadius = 12;
            this.roundedPanel2.Location = new System.Drawing.Point(6, 63);
            this.roundedPanel2.Name = "roundedPanel2";
            this.roundedPanel2.Size = new System.Drawing.Size(870, 339);
            this.roundedPanel2.TabIndex = 7;
            // 
            // dgvJournal
            // 
            this.dgvJournal.AllowUserToOrderColumns = true;
            this.dgvJournal.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvJournal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvJournal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvJournal.Location = new System.Drawing.Point(0, 0);
            this.dgvJournal.Name = "dgvJournal";
            this.dgvJournal.RowHeadersWidth = 51;
            this.dgvJournal.RowTemplate.Height = 24;
            this.dgvJournal.Size = new System.Drawing.Size(870, 339);
            this.dgvJournal.TabIndex = 3;
            this.dgvJournal.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvJournal_CellEndEdit);
            this.dgvJournal.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvJournal_CellValidated);
            this.dgvJournal.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvJournal_CellValidating);
            this.dgvJournal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvJournal_KeyDown);
            // 
            // roundedPanel1
            // 
            this.roundedPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.roundedPanel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.roundedPanel1.BorderSize = 1;
            this.roundedPanel1.CornerRadius = 8;
            this.roundedPanel1.Location = new System.Drawing.Point(6, 8);
            this.roundedPanel1.Name = "roundedPanel1";
            this.roundedPanel1.Size = new System.Drawing.Size(870, 49);
            this.roundedPanel1.TabIndex = 6;
            // 
            // frmJournal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(883, 570);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmJournal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Journal";
            this.Activated += new System.EventHandler(this.frmJournal_Activated);
            this.Load += new System.EventHandler(this.frmJournal_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.roundedPanel4.ResumeLayout(false);
            this.roundedPanel3.ResumeLayout(false);
            this.roundedPanel3.PerformLayout();
            this.roundedPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvJournal)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dtpVoucherdate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbVoucherNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbOurRef;
        private System.Windows.Forms.Label label3;
        private Custom_Components.RoundedPanel roundedPanel1;
        private Custom_Components.RoundedPanel roundedPanel3;
        private Custom_Components.RoundedPanel roundedPanel2;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label label4;
        private Custom_Components.RoundedPanel roundedPanel4;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnHide;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.DataGridView dgvJournal;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblDrSumAmount;
        private System.Windows.Forms.Label lblDifferenceAmount;
        private System.Windows.Forms.Label lblCrSumAmount;
    }
}



