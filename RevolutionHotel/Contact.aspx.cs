using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RevolutionHotel
{
    public partial class Contact : System.Web.UI.Page
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
                LoadCustomerDetails();
                txtFullname.Enabled = false;
                txtEmail.Enabled = false;
            }
        }

        private void LoadCustomerDetails()
        {
            try
            {
                string username = Session["username"].ToString();
                connection = Components.GetConnectionToBD();
                string query = @"SELECT * FROM Customer WHERE Username=@Username";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    txtFullname.Text = reader["FullName"].ToString();
                    txtEmail.Text = reader["Email"].ToString();
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                string deliveryReport = "unread";
                string username = Session["username"].ToString();
                DateTime time = DateTime.Now;
                string id = Components.GenerateRandomId();
                connection = Components.GetConnectionToBD();
                string query = @"INSERT INTO Contact VALUES(@Id, @Name, @Email, @Subject, @Message, @Status, @CreatedTime)";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@Name", txtFullname.Text);
                command.Parameters.AddWithValue("@Email", txtEmail.Text);
                command.Parameters.AddWithValue("@Subject", txtSubject.Text.Trim());
                command.Parameters.AddWithValue("@Message", txtMessage.Text.Trim());
                command.Parameters.AddWithValue("@CreatedTime", time);
                command.Parameters.AddWithValue("@Status", deliveryReport);

                int res = command.ExecuteNonQuery();
                if (res > 0)
                {
                    SuccessMessage("Message submitted succesifully. We will look into your query and get back to you with 24 hours via email. Please check your email after 24 hours.");
                }
                else
                {
                    Message("Could not send the message. Please try again later!");
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
        }

        protected void Message(string message)
        {
            string strScript = "<script>alert('" + message + "');</script>";
            ClientScript.RegisterStartupScript(GetType(), "Client Script", strScript.ToString());
        }

        protected void SuccessMessage(string message)
        {
            string myPage = "Default.aspx";
            string strScript = "<script>alert('" + message + "');window.location='" + myPage + "'</script>";
            ClientScript.RegisterStartupScript(GetType(), "Client Script", strScript.ToString());
        }
    }
}