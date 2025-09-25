namespace POS_System.Forms
{
    partial class frmItemBatchUpdate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmItemBatchUpdate));
            this.dgvItemUpdate = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbItemCode = new System.Windows.Forms.ComboBox();
            this.cmbGenaricName = new System.Windows.Forms.ComboBox();
            this.cmbItemName = new System.Windows.Forms.ComboBox();
            this.btnHide = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemUpdate)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvItemUpdate
            // 
            this.dgvItemUpdate.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvItemUpdate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItemUpdate.Location = new System.Drawing.Point(15, 201);
            this.dgvItemUpdate.Margin = new System.Windows.Forms.Padding(4);
            this.dgvItemUpdate.Name = "dgvItemUpdate";
            this.dgvItemUpdate.RowHeadersWidth = 51;
            this.dgvItemUpdate.RowTemplate.Height = 24;
            this.dgvItemUpdate.Size = new System.Drawing.Size(580, 480);
            this.dgvItemUpdate.TabIndex = 5;
            this.dgvItemUpdate.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItemUpdate_CellEndEdit);
            this.dgvItemUpdate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvItemUpdate_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 30);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Item Code";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 86);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Genaric Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 142);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Item Name";
            // 
            // cmbItemCode
            // 
            this.cmbItemCode.DropDownHeight = 120;
            this.cmbItemCode.FormattingEnabled = true;
            this.cmbItemCode.IntegralHeight = false;
            this.cmbItemCode.Location = new System.Drawing.Point(124, 26);
            this.cmbItemCode.Margin = new System.Windows.Forms.Padding(4);
            this.cmbItemCode.Name = "cmbItemCode";
            this.cmbItemCode.Size = new System.Drawing.Size(284, 28);
            this.cmbItemCode.TabIndex = 0;
            this.cmbItemCode.SelectedIndexChanged += new System.EventHandler(this.cmbItemCode_SelectedIndexChanged);
            this.cmbItemCode.TextChanged += new System.EventHandler(this.cmbItemCode_TextChanged);
            this.cmbItemCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbItemCode_KeyDown);
            // 
            // cmbGenaricName
            // 
            this.cmbGenaricName.DropDownHeight = 120;
            this.cmbGenaricName.FormattingEnabled = true;
            this.cmbGenaricName.IntegralHeight = false;
            this.cmbGenaricName.Location = new System.Drawing.Point(124, 82);
            this.cmbGenaricName.Margin = new System.Windows.Forms.Padding(4);
            this.cmbGenaricName.Name = "cmbGenaricName";
            this.cmbGenaricName.Size = new System.Drawing.Size(284, 28);
            this.cmbGenaricName.TabIndex = 1;
            this.cmbGenaricName.SelectedIndexChanged += new System.EventHandler(this.cmbGenaricName_SelectedIndexChanged);
            // 
            // cmbItemName
            // 
            this.cmbItemName.DropDownHeight = 120;
            this.cmbItemName.FormattingEnabled = true;
            this.cmbItemName.IntegralHeight = false;
            this.cmbItemName.Location = new System.Drawing.Point(124, 139);
            this.cmbItemName.Margin = new System.Windows.Forms.Padding(4);
            this.cmbItemName.Name = "cmbItemName";
            this.cmbItemName.Size = new System.Drawing.Size(284, 28);
            this.cmbItemName.TabIndex = 2;
            this.cmbItemName.SelectedIndexChanged += new System.EventHandler(this.cmbItemName_SelectedIndexChanged);
            // 
            // btnHide
            // 
            this.btnHide.Location = new System.Drawing.Point(443, 691);
            this.btnHide.Margin = new System.Windows.Forms.Padding(4);
            this.btnHide.Name = "btnHide";
            this.btnHide.Size = new System.Drawing.Size(140, 46);
            this.btnHide.TabIndex = 7;
            this.btnHide.Text = "Hide";
            this.btnHide.UseVisualStyleBackColor = true;
            this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(277, 691);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(140, 46);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(423, 44);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(172, 46);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            this.btnRefresh.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnRefresh_KeyDown);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(423, 100);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(172, 46);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // frmItemBatchUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 749);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnHide);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cmbItemName);
            this.Controls.Add(this.cmbGenaricName);
            this.Controls.Add(this.cmbItemCode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvItemUpdate);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmItemBatchUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Item Batch Update";
            this.Load += new System.EventHandler(this.frmItemBatchUpdate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemUpdate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvItemUpdate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbItemCode;
        private System.Windows.Forms.ComboBox cmbGenaricName;
        private System.Windows.Forms.ComboBox cmbItemName;
        private System.Windows.Forms.Button btnHide;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnClear;
    }
}