using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RevolutionHotel
{
    public partial class OTPVerification : System.Web.UI.Page
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
                    Response.Redirect("~/Default.aspx");
                    return;
                }
                SendOTP();
            }
        }

        protected void btnOTP_Click(object sender, EventArgs e)
        {
            try
            {
                VerifyOTP();
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
        }
        private void SendOTP()
        {
            try
            {
                string customerId = Request.QueryString["customerid"].ToString();
                connection = Components.GetConnectionToBD();
                string query = @"SELECT * FROM Customer WHERE CustomerId = @customerId";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("CustomerId", customerId);

                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    string subject = "OTP Verification";
                    string email = reader["Email"].ToString();
                    string body = $"Your secret OTP is <b>{reader["OTP"]}</b>. <br/><br/>Please do not share this OTP with anyone else.";
                    Components.SendEmailAlerts(email, subject, body);
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
        }

        private void VerifyOTP()
        {
            try
            {
                string otp = txtOTP.Text.Trim();
                string customerId = Request.QueryString["customerid"].ToString();
                connection = Components.GetConnectionToBD();
                string query = @"SELECT * FROM Customer WHERE CustomerId = @CustomerId";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("CustomerId", customerId);

                reader = command.ExecuteReader();
                if(reader.HasRows)
                {
                    reader.Read();
                    string otpFromDB = reader["OTP"].ToString();
                    if(otpFromDB == otp)
                    {
                        Response.Redirect("User/Dashboard.aspx");
                    }
                    else
                    {
                        Message("OTP does not macth. Please try again later!");
                        txtOTP.Focus();
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
        }

        public void Message(string message)
        {
            string strScript = "<script>alert('" + message + "');</script>";
            ClientScript.RegisterStartupScript(GetType(), "Client Script", strScript.ToString());
        }
    }
}