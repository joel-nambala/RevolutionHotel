using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RevolutionHotel
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        SqlConnection connection;
        SqlCommand command;
        SqlDataReader reader;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["username"] == null)
                {
                    Response.Redirect("Default.aspx");
                    return;
                }
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                string customerId = Request.QueryString["customerid"].ToString();
                string customerEmail = Request.QueryString["email"].ToString();
                string newPassword = txtPassword.Text.Trim();
                string confirmNewPass = txtConfirmPassword.Text.Trim();

                if (newPassword != confirmNewPass)
                {
                    Message("Passwords do not match");
                    return;
                }

                if (!Components.ValidPassword(newPassword))
                {
                    Message("Password must have 8 characters, atleast one uppercase letter, one lowercase letter and one special character.");
                    return;
                }

                connection = Components.GetConnectionToBD();
                string query = @"UPDATE Customer SET Password=@Password WHERE CustomerId=@CustomerId AND Email=@Email";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Password", newPassword);
                command.Parameters.AddWithValue("@CustomerId", customerId);
                command.Parameters.AddWithValue("@Email", customerEmail);

                int result = command.ExecuteNonQuery();
                if (result > 0)
                {
                    string recipient = customerEmail;
                    string subject = "Password Reset";
                    string body = $"New password for {GetStaffFullNames()} is <b>{newPassword}</b>. <br/><br/>This password is protected and do not share it with anyone.";
                    Components.SendEmailAlerts(recipient, subject, body);
                    SuccessMessage("Password has been reset sucessifully. A copy of the password has been send to your email!");
                }
                else
                {
                    Message("Something went wrong. Please try again some other time!");
                }
            }
            catch (Exception ex)
            {
                Message(ex.Message);
                ex.Data.Clear();
            }
        }

        protected void lbtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
            Session.Abandon();
            Session.RemoveAll();
            Session.Clear();
        }

        protected string GetStaffFullNames()
        {
            string fullName = string.Empty;
            try
            {
                string customerId = Request.QueryString["customerid"].ToString();
                connection = Components.GetConnectionToBD();
                string query = @"SELECT * FROM Customer WHERE CustomerId = @id";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", customerId);

                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    fullName = reader["FullName"].ToString();
                }
            }
            catch (Exception ex)
            {
                Message(ex.Message);
                ex.Data.Clear();
            }
            return fullName;
        }

        protected void Message(string message)
        {
            string strScript = "<script>alert('" + message + "');</script>";
            ClientScript.RegisterStartupScript(GetType(), "Client Script", strScript.ToString());
        }

        protected void SuccessMessage(string message)
        {
            string newPage = "Default.aspx";
            string strScript = "<script>alert('" + message + "');window.location='" + newPage + "'</script>";
            ClientScript.RegisterStartupScript(GetType(), "Client Script", strScript.ToString());
        }
    }
}