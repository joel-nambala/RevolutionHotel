using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
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

                    if (user == username && pass == password)
                    {
                        Session["admin"] = user.ToString();
                        Response.Redirect("Admin/Dashboard.aspx");
                    }
                }

                connection = Components.GetConnectionToBD();
                string query = @"SELECT * FROM Customer WHERE Username = @Username AND Password = @Password";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);

                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    if (reader["Blocked"].ToString() == "No")
                    {
                        Session["username"] = reader["Username"].ToString();
                        Session["customerId"] = reader["CustomerId"].ToString();
                        Response.Redirect("User/Dashboard.aspx");
                    }
                    else
                    {
                        Session["username"] = reader["Username"].ToString();
                        UserBlockedMessage("Your account has been blocked. Please contact the system administrator.");
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
    }
}