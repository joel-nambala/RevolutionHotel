using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RevolutionHotel
{
    public partial class Default : System.Web.UI.Page
    {
        SqlConnection connection;
        SqlCommand command;
        SqlDataReader reader;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["admin"] != null)
            {
                ClearSession();
            }
            if (Session["username"] != null)
            {
                ClearSession();
            }
            txtUsername.Focus();
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txtUsername.Text.Trim();
                string password = txtPassword.Text.Trim();

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    lblMsg.Text = "Username and password cannot be empty!";
                    lblMsg.Visible = true;
                    txtUsername.Focus();
                    return;
                }

                if (username == "Admin")
                {
                    // Login for admin
                    string user = ConfigurationManager.AppSettings["username"];
                    string pass = ConfigurationManager.AppSettings["password"];

                    if (user == username)
                    {
                        if (pass == password)
                        {
                            Session["admin"] = user.ToString();
                            Response.Redirect("Admin/Dashboard.aspx");
                        }
                        else
                        {
                            lblMsg.Text = "Incorrect password";
                            lblMsg.Visible = true;
                            txtPassword.Focus();
                            return;
                        }
                    }
                }

                connection = Components.GetConnectionToBD();
                string query = @"SELECT * FROM Customer WHERE Username = @Username";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                //command.Parameters.AddWithValue("@Password", password);

                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    if (reader["Password"].ToString() == password)
                    {
                        if (reader["Blocked"].ToString() == "No")
                        {
                            string UserName = reader["Username"].ToString();
                            Session["username"] = UserName;
                            Session["customerId"] = reader["CustomerId"].ToString();
                            UpdateOTP(UserName);
                            Response.Redirect($"OTPVerification.aspx?customerid={reader["CustomerId"]}");
                        }
                        else
                        {
                            Session["username"] = reader["Username"].ToString();
                            UserBlockedMessage("Your account has been blocked. Please contact the system administrator.");
                        }
                    }
                    else
                    {
                        lblMsg.Text = "Incorrect password";
                        lblMsg.Visible = true;
                        txtPassword.Focus();
                        return;
                    }
                }
                else
                {
                    ErrorMessage($"Account with the username {username} does not exist in the database. Please sign up to continue...!");
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
        }
        private void UpdateOTP(string username)
        {
            try
            {
                string otp = Components.GenerateOTP();
                connection = Components.GetConnectionToBD();
                string query = @"UPDATE Customer SET OTP = @otp WHERE Username = @Username";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("Username", username);
                command.Parameters.AddWithValue("OTP", otp);

                int r = command.ExecuteNonQuery();
                if (r > 0){}else{}
                connection.Close();
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
        }
        
        protected void lbtnForgot_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            try
            {
                if (string.IsNullOrEmpty(username))
                {
                    lblMsg.Text = "Username cannot be empty!";
                    lblMsg.Visible = true;
                    txtUsername.Focus();
                    return;
                }

                connection = Components.GetConnectionToBD();
                string query = @"SELECT * FROM Customer WHERE Username = @Username";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());

                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    Session["username"] = reader["Username"].ToString();
                    Response.Redirect($"ResetPassword.aspx?customerid={reader["CustomerId"].ToString()}&email={reader["Email"].ToString()}");
                }
                else
                {
                    ErrorMessage($"Account with the username {username} does not exist in the database. Please sign up to continue...!");
                }
                connection.Close();

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                Message($"ERROR: {error}");
                ex.Data.Clear();
            }
        }

        private void ClearSession()
        {
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            //Session.Clear();
        }

        protected void Message(string message)
        {
            string strScript = "<script>alert('" + message + "');</script>";
            ClientScript.RegisterStartupScript(GetType(), "Client Script", strScript.ToString());
        }

        protected void ErrorMessage(string message)
        {
            string myPage = "Register.aspx";
            string strScript = "<script>alert('" + message + "');window.location='" + myPage + "'</script>";
            ClientScript.RegisterStartupScript(GetType(), "Client Script", strScript.ToString());
        }
        protected void UserBlockedMessage(string message)
        {
            string myPage = "Contact.aspx";
            string strScript = "<script>alert('" + message + "');window.location='" + myPage + "'</script>";
            ClientScript.RegisterStartupScript(GetType(), "Client Script", strScript.ToString());
        }
        [WebMethod]
        public static string GetCustomerData()
        {
            string[] data = { "benz", "toyota", "mazda", "isuzu" };
            return "Hello from the server";
        }
    }
}