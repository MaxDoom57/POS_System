namespace POS_System.Forms
{
    partial class frmTransactionsConfirmationSetup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTransactionsConfirmationSetup));
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabTransactionConfirmation = new System.Windows.Forms.TabControl();
            this.tpConfirmationDate = new System.Windows.Forms.TabPage();
            this.dgvConfirmationDate = new System.Windows.Forms.DataGridView();
            this.tpRollingDate = new System.Windows.Forms.TabPage();
            this.dgvRollingDate = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.tabTransactionConfirmation.SuspendLayout();
            this.tpConfirmationDate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConfirmationDate)).BeginInit();
            this.tpRollingDate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRollingDate)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabTransactionConfirmation);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(703, 642);
            this.panel1.TabIndex = 1;
            // 
            // tabTransactionConfirmation
            // 
            this.tabTransactionConfirmation.Controls.Add(this.tpConfirmationDate);
            this.tabTransactionConfirmation.Controls.Add(this.tpRollingDate);
            this.tabTransactionConfirmation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabTransactionConfirmation.Location = new System.Drawing.Point(0, 0);
            this.tabTransactionConfirmation.Margin = new System.Windows.Forms.Padding(4);
            this.tabTransactionConfirmation.Name = "tabTransactionConfirmation";
            this.tabTransactionConfirmation.SelectedIndex = 0;
            this.tabTransactionConfirmation.Size = new System.Drawing.Size(703, 642);
            this.tabTransactionConfirmation.TabIndex = 0;
            this.tabTransactionConfirmation.SelectedIndexChanged += new System.EventHandler(this.tabTransactionConfirmation_SelectedIndexChanged);
            // 
            // tpConfirmationDate
            // 
            this.tpConfirmationDate.Controls.Add(this.dgvConfirmationDate);
            this.tpConfirmationDate.Location = new System.Drawing.Point(4, 29);
            this.tpConfirmationDate.Margin = new System.Windows.Forms.Padding(4);
            this.tpConfirmationDate.Name = "tpConfirmationDate";
            this.tpConfirmationDate.Padding = new System.Windows.Forms.Padding(4);
            this.tpConfirmationDate.Size = new System.Drawing.Size(695, 609);
            this.tpConfirmationDate.TabIndex = 0;
            this.tpConfirmationDate.Text = "Transaction Confirmation Date";
            this.tpConfirmationDate.UseVisualStyleBackColor = true;
            // 
            // dgvConfirmationDate
            // 
            this.dgvConfirmationDate.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvConfirmationDate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvConfirmationDate.Location = new System.Drawing.Point(5, 10);
            this.dgvConfirmationDate.Margin = new System.Windows.Forms.Padding(4);
            this.dgvConfirmationDate.Name = "dgvConfirmationDate";
            this.dgvConfirmationDate.RowHeadersWidth = 51;
            this.dgvConfirmationDate.RowTemplate.Height = 24;
            this.dgvConfirmationDate.Size = new System.Drawing.Size(676, 593);
            this.dgvConfirmationDate.TabIndex = 0;
            this.dgvConfirmationDate.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvConfirmationDate_CellEndEdit);
            // 
            // tpRollingDate
            // 
            this.tpRollingDate.Controls.Add(this.dgvRollingDate);
            this.tpRollingDate.Location = new System.Drawing.Point(4, 29);
            this.tpRollingDate.Margin = new System.Windows.Forms.Padding(4);
            this.tpRollingDate.Name = "tpRollingDate";
            this.tpRollingDate.Padding = new System.Windows.Forms.Padding(4);
            this.tpRollingDate.Size = new System.Drawing.Size(695, 609);
            this.tpRollingDate.TabIndex = 1;
            this.tpRollingDate.Text = "Transactions-Rolling Dates";
            this.tpRollingDate.UseVisualStyleBackColor = true;
            // 
            // dgvRollingDate
            // 
            this.dgvRollingDate.AllowUserToAddRows = false;
            this.dgvRollingDate.AllowUserToOrderColumns = true;
            this.dgvRollingDate.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRollingDate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRollingDate.Location = new System.Drawing.Point(8, 8);
            this.dgvRollingDate.Margin = new System.Windows.Forms.Padding(4);
            this.dgvRollingDate.Name = "dgvRollingDate";
            this.dgvRollingDate.RowHeadersWidth = 51;
            this.dgvRollingDate.RowTemplate.Height = 24;
            this.dgvRollingDate.Size = new System.Drawing.Size(675, 596);
            this.dgvRollingDate.TabIndex = 0;
            this.dgvRollingDate.TabStop = false;
            this.dgvRollingDate.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRollingDate_CellEndEdit);
            this.dgvRollingDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvRollingDate_KeyDown);
            // 
            // frmTransactionsConfirmationSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(703, 642);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "frmTransactionsConfirmationSetup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transactions Confirmation Setup";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTransactionsConfirmationSetup_FormClosing);
            this.Load += new System.EventHandler(this.frmTransactionsConfirmationSetup_Load);
            this.panel1.ResumeLayout(false);
            this.tabTransactionConfirmation.ResumeLayout(false);
            this.tpConfirmationDate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvConfirmationDate)).EndInit();
            this.tpRollingDate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRollingDate)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabTransactionConfirmation;
        private System.Windows.Forms.TabPage tpConfirmationDate;
        private System.Windows.Forms.TabPage tpRollingDate;
        private System.Windows.Forms.DataGridView dgvConfirmationDate;
        private System.Windows.Forms.DataGridView dgvRollingDate;
    }
}



