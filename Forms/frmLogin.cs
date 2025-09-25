using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using POS_System.Classes;
using POS_System.Modules;

namespace POS_System.Forms
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        //Function and Variable define here ----------------------------------------------------------------------------------------------------
        string query;

        //Events are handle here ---------------------------------------------------------------------------------------------------------------
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = DBConnectionManager.GetConnection())
                {
                    conn.Open();
                    query = "Select * from UsrMas where UsrId =@userId and fInAct=0";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("userId", txtUserID.Text.Trim());
                    cmd.CommandTimeout = 30;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var session = UserSession.Instance;
                            string hashedPassword;

                            session.UserId = reader["UsrId"].ToString();
                            session.UserKey = reader["usrKy"].ToString();
                            hashedPassword = reader["Pwd"].ToString();
                            reader.Close();

                            if (SecurityModules.VerifyPassword(txtPassword.Text, hashedPassword) || hashedPassword == "")
                            {
                                // Add log
                                File.AppendAllText("Login.txt", $"{session.UserId} logged in into system @ {DateTime.Now} \n");

                                // Load user permissions
                                using (SqlConnection conn2 = DBConnectionManager.GetConnection())
                                {
                                    conn2.Open();
                                    query = "SELECT objMas.objNm, " +
                                        "usrObj.fAcs, " +
                                        "usrObj.fNew, " +
                                        "usrObj.fUpdt, " +
                                        "usrObj.fDel, " +
                                        "usrObj.fSp " +
                                        "FROM UsrObj usrObj " +
                                        "INNER JOIN ObjMas objMas ON usrObj.objKy = objMas.objKy " +
                                        "WHERE usrObj.usrKy = @userKey";

                                    cmd = new SqlCommand(query, conn2);
                                    cmd.Parameters.AddWithValue("@userKey", session.UserKey);
                                    using (SqlDataReader permissionReader = cmd.ExecuteReader())
                                    {
                                        while (permissionReader.Read())
                                        {
                                            session.Permissions.Add(new FeaturePermission
                                            {
                                                ObjName = permissionReader["ObjNm"].ToString(),
                                                CanAccess = Convert.ToBoolean(permissionReader["fAcs"]),
                                                CanCreateNew = Convert.ToBoolean(permissionReader["fNew"]),
                                                CanUpdate = Convert.ToBoolean(permissionReader["fUpdt"]),
                                                CanDelete = Convert.ToBoolean(permissionReader["fDel"]),
                                                CanSpecial = Convert.ToBoolean(permissionReader["fSp"]),
                                            });
                                        }
                                        permissionReader.Close();
                                    }
                                    conn2.Close();
                                }

                                // Set company
                                using (SqlConnection conn3 = DBConnectionManager.GetConnection())
                                {
                                    conn3.Open();
                                    SqlCommand getCompanyCmd = new SqlCommand("SELECT CKy, CNm FROM Company", conn3);
                                    using (SqlDataReader companyReader = getCompanyCmd.ExecuteReader())
                                    {
                                        while (companyReader.Read())
                                        {
                                            session.CompanyName = companyReader["CNm"].ToString();
                                            session.CompanyKey = Convert.ToInt32(companyReader["CKy"]);
                                        }
                                        companyReader.Close();
                                    }
                                    conn3.Close();
                                }

                                // Set branch
                                using (SqlConnection conn4 = DBConnectionManager.GetConnection())
                                {
                                    conn4.Open();
                                    SqlCommand getBranchCmd = new SqlCommand("SELECT LocCd, LocNm FROM vewLocCd", conn4);
                                    using (SqlDataReader branchReader = getBranchCmd.ExecuteReader())
                                    {
                                        while (branchReader.Read())
                                        {
                                            session.BranchName = branchReader["LocNm"].ToString();
                                            session.BranchCode = branchReader["LocCd"].ToString();
                                        }
                                        branchReader.Close();
                                    }
                                    conn4.Close();
                                }

                                // Set Windows user name
                                session.WindowsUserName = Environment.UserName.ToString();

                                // Show dashboard
                                var frmMainDashboard = new frmMainDashboard();
                                frmMainDashboard.Show();
                                this.Hide();

                                // Debug output
                                foreach (var p in session.Permissions)
                                {
                                    Console.WriteLine($"{p.ObjName,-30} {p.CanAccess,-7} {p.CanCreateNew,-7} {p.CanUpdate,-7} {p.CanDelete,-7} {p.CanSpecial,-7}");
                                }

                                Console.WriteLine("===================");
                                Console.WriteLine(session.CompanyName);
                                Console.WriteLine(session.CompanyKey);
                                Console.WriteLine("===================");
                                Console.WriteLine(session.BranchName);
                                Console.WriteLine(session.BranchCode);
                            }
                            else
                            {
                                MessageBox.Show("Invalid password");
                                txtPassword.Text = "";
                                txtPassword.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Invalid user ID");
                            txtUserID.Text = "";
                            txtPassword.Text = "";
                            txtUserID.Focus();
                        }
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        //Keys and forcus are handle here ------------------------------------------------------------------------------------------------------

        private void txtUserID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPassword.Focus();
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.Focus();
            }
        }
    }
}
