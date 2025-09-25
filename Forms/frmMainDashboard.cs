using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.SqlServer.Server;
using static System.Collections.Specialized.BitVector32;
using System.IO;
using System.Data.SqlClient;
using POS_System.Classes;
using System.Collections;

namespace POS_System.Forms
{
    public partial class frmMainDashboard : Form
    {

        public frmMainDashboard()
        {
            InitializeComponent();
        }

        private void frmMainDashboard_Load(object sender, EventArgs e)
        {
            flpMainMenu.Visible = false;
            flpMenuList.Visible = false;

            LoadMenuItems();
        }

        private void mnuTradingStockItemEntry_Click(object sender, EventArgs e)
        {
            var frmTradingStockItemEntry = new frmTradingStockItemEntry();
            frmTradingStockItemEntry.Show();
        }

        private void mnuTradingStockRecordMode_Click(object sender, EventArgs e)
        {
            var frmTradingStockRecordMode = new frmTradingStockRecordMode();
            frmTradingStockRecordMode.Show();
        }

        private void mnuBankEntry_Click(object sender, EventArgs e)
        {
            var frmBankEntry = new frmBankEntry();
            frmBankEntry.Show();
        }

        private void mnuBranchEntry_Click(object sender, EventArgs e)
        {
            var frmBranchEntry = new frmBranchEntry();
            frmBranchEntry.Show();
        }

        private void mnuUserEntry_Click(object sender, EventArgs e)
        {
            var frmUserEntry = new frmUserEntry();
            frmUserEntry.Show();
        }

        private void mnuUserPermissionEntry_Click(object sender, EventArgs e)
        {
            var frmUserPermissionEntry = new frmUserPermissionEntry();
            frmUserPermissionEntry.Show();
        }

        private void mnuItemBatchUpdate_Click(object sender, EventArgs e)
        {
            var frmItemBatchUpdate = new frmItemBatchUpdate();
            frmItemBatchUpdate.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDate.Text = DateTime.Now.ToShortDateString();
            lblTime.Text = DateTime.Now.ToShortTimeString();
        }
        private void mnuTransactionsConfirmationSetup_Click(object sender, EventArgs e)
        {
            var frmTransactionConfirmationSetup = new frmTransactionsConfirmationSetup();
            frmTransactionConfirmationSetup.Show();
        }

        private void mnuUnitEntry_Click(object sender, EventArgs e)
        {
            var frmUnitEntry = new frmUnitEntry();
            frmUnitEntry.Show();
        }

        private void mnuCodesEntry_Click(object sender, EventArgs e)
        {
            var frmCodesEntry = new frmCodesEntry();
            frmCodesEntry.Show();
        }
        private void mnuCustomerDiscountEntry_Click(object sender, EventArgs e)
        {
            var frmCustomerDiscountEntry = new frmCustomerDiscountEntry();
            frmCustomerDiscountEntry.Show();
        }

        private void mnuManageAccounts_Click(object sender, EventArgs e)
        {
            var frmManageAccounts = new frmManageAccounts();
            frmManageAccounts.Show();
        }
        private void mnuStockDeductioion_Click(object sender, EventArgs e)
        {
            var frmStockDeduction = new frmStockDeductions();
            frmStockDeduction.Show();
        }

        private void mnuJournal_Click(object sender, EventArgs e)
        {
            var frmJournal = new frmJournal();
            frmJournal.Show();
        }


        //Exit function here --------------------------------------------------------------------------------------------------------------------------------------
        private void mnuExit_Click(object sender, EventArgs e)
        {
            DialogResult result =  MessageBox.Show("Do you want close this app?", this.Text, MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                UserSession.Instance.ClearSession();

                File.AppendAllText("Login.txt", $"{UserSession.Instance.UserId} logged out from system @ {DateTime.Now} \n\n");
                foreach (var p in UserSession.Instance.Permissions)
                {
                    Console.WriteLine($"{p.ObjName.ToString(),-30} {p.CanAccess,-7} {p.CanCreateNew,-7} {p.CanUpdate,-7} {p.CanDelete,-7} {p.CanSpecial,-7}");
                }
            }
            Application.Exit();
        }

        private void frmMainDashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show("Do you want close this app?", this.Text, MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    e.Cancel = false;
                    UserSession.Instance.ClearSession();
                    Application.Exit();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }


        //Dashboard Menu setup here -------------------------------------------------------------------------------------------------------------------------------

        private bool isToggle = false;
        private bool isEdit;

        private void LoadMenuItems()
        {
            try
            {
                using (SqlConnection conn = DBConnectionManager.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT MenuKey, MenuTypeKey, MenuName, FormName from MenuItems where Status = 1 AND InAct = 0";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string btnName = reader["MenuKey"].ToString();
                        int category = Convert.ToInt32(reader["MenuTypeKey"]);
                        string btnText =  reader["MenuName"].ToString();
                        string formName = reader["FormName"].ToString();

                        Button btn = new Button();
                        btn.Name = "btn"+btnName;
                        btn.Text = btnText;
                        btn.Tag = formName;
                        btn.Size = new Size(160, 80);
                        btn.Font = new Font("Arial", 10);
                        btn.Click += (s, e) =>
                        {
                            string formClassName = (s as Button).Tag.ToString();
                            string fullFormName = "POS_System.Forms." + formClassName;

                            Type formType = Type.GetType(fullFormName);
                            if (formType != null )
                            {
                                Form form = (Form)Activator.CreateInstance(formType);
                                form.Show();
                            }
                        };

                        if (category == 1)
                        {
                            flpTransaction.Controls.Add(btn);
                        }
                        else if (category == 2)
                        {
                            flpReports.Controls.Add(btn);
                        }
                        else if (category == 3)
                        {
                            flpViews.Controls.Add(btn);
                        }
                        else if (category == 4)
                        {
                            flpSetup.Controls.Add(btn);
                        }
                        else
                        {
                            flpUtility.Controls.Add(btn);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetupDgvMenu()
        {
            dgvMenu.Columns.Clear();

            DataGridViewTextBoxColumn colMenuKey = new DataGridViewTextBoxColumn();
            colMenuKey.Name = "menuKey";
            colMenuKey.HeaderText = "Menu Key";
            colMenuKey.ReadOnly = true;
            colMenuKey.Visible = false;
            dgvMenu.Columns.Add(colMenuKey);

            DataGridViewTextBoxColumn colMenuName = new DataGridViewTextBoxColumn();
            colMenuName.Name = "menuName";
            colMenuName.HeaderText = "Name";
            colMenuName.ReadOnly = true;
            dgvMenu.Columns.Add(colMenuName);

            DataGridViewCheckBoxColumn colStatus = new DataGridViewCheckBoxColumn();
            colStatus.Name = "status";
            colStatus.HeaderText = "Status";
            colStatus.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colStatus.Width = 80;
            dgvMenu.Columns.Add(colStatus);

            DataGridViewTextBoxColumn colUpdate = new DataGridViewTextBoxColumn();
            colUpdate.Name = "update";
            colUpdate.HeaderText = "Update";
            colUpdate.Visible = false;
            dgvMenu.Columns.Add(colUpdate);
        }

        private  void LoadData(string query)
        {
            try
            {
                using(SqlConnection conn = DBConnectionManager.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        dgvMenu.Rows.Add(reader["MenuKey"], reader["MenuName"], reader["Status"], 0);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SaveData()
        {
            try
            {
                foreach (DataGridViewRow row in dgvMenu.Rows)
                {
                    if (row.IsNewRow) continue;

                    if (row.Cells["update"].Value?.ToString() == "1")
                    {
                        using(SqlConnection conn = DBConnectionManager.GetConnection())
                        {
                            conn.Open();
                            string query = "UPDATE MenuItems SET Status = @Status WHERE MenuKey = @MenuKey";
                            SqlCommand cmd = new SqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("Status", Convert.ToInt32(row.Cells["Status"].Value));
                            cmd.Parameters.AddWithValue("MenuKey", Convert.ToInt32(row.Cells["MenuKey"].Value));
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                btnSetting.BackgroundImage = Properties.Resources.imgSetting;
                flpMainMenu.Visible = false;
                flpMenuList.Visible = false;
                isToggle = !isToggle;
                flpTransaction.Controls.Clear();
                flpReports.Controls.Clear();
                flpViews.Controls.Clear();
                flpSetup.Controls.Clear();
                flpUtility.Controls.Clear();
                LoadMenuItems();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {

            if (!isToggle)
            {
                btnSetting.BackgroundImage = Properties.Resources.imgClose;
                flpMainMenu.Visible = true;

            }
            else
            {
                btnSetting.BackgroundImage = Properties.Resources.imgSetting;
                flpMainMenu.Visible = false;
                flpMenuList.Visible = false;
            }
            isToggle = !isToggle;
        }

        private void btnTransaction_Click(object sender, EventArgs e)
        {
            flpMenuList.Visible = true;
            SetupDgvMenu();
            LoadData("SELECT MenuKey, MenuName, Status from MenuItems where MenuTypeKey = 1 AND InAct = 0");
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            flpMenuList.Visible = true;
            SetupDgvMenu();
            LoadData("SELECT MenuKey, MenuName, Status from MenuItems where MenuTypeKey = 2 AND InAct = 0");
        }

        private void btnViews_Click(object sender, EventArgs e)
        {
            flpMenuList.Visible = true;
            SetupDgvMenu();
            LoadData("SELECT MenuKey, MenuName, Status from MenuItems where MenuTypeKey = 3 AND InAct = 0");
        }

        private void btnSetup_Click(object sender, EventArgs e)
        {
            flpMenuList.Visible = true;
            SetupDgvMenu();
            LoadData("SELECT MenuKey, MenuName, Status from MenuItems where MenuTypeKey = 4 AND InAct = 0");
        }

        private void btnUtility_Click(object sender, EventArgs e)
        {
            flpMenuList.Visible = true;
            SetupDgvMenu();
            LoadData("SELECT MenuKey, MenuName, Status from MenuItems where MenuTypeKey = 5 AND InAct = 0");
        }

        private void dgvMenu_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            dgvMenu.Rows[e.RowIndex].Cells["update"].Value = 1;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {

        }
    }
}
