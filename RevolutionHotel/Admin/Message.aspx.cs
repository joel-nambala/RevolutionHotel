using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RevolutionHotel.Admin
{
    public partial class Message : System.Web.UI.Page
    {
        SqlConnection connection;
        SqlCommand command;
        SqlDataReader reader;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["admin"] == null)
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }
                LoadMessageBody();
                ReadMessage();
            }
        }
        private void LoadMessageBody()
        {
            try
            {
                string messageId = Request.QueryString["id"].ToString().ToUpper();
                string email = Request.QueryString["email"].ToString();
                connection = Components.GetConnectionToBD();
                string query = @"SELECT * FROM Contact WHERE Id=@Id AND Email=@Email";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", messageId);
                command.Parameters.AddWithValue("@Email", email);

                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    string senderName = reader["Name"].ToString();
                    string senderEmail = reader["Email"].ToString();
                    string subject = reader["Subject"].ToString();
                    string body = reader["Message"].ToString();
                    lblMsg.Text = body;
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
        }
        private void ReadMessage()
        {
            try
            {
                string messageId = Request.QueryString["id"].ToString();
                string status = "read";
                connection = Components.GetConnectionToBD();
                string query = @"UPDATE Contact SET Status=@Status WHERE Id=@Id";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Status", status);
                command.Parameters.AddWithValue("@Id", messageId);

                int r = command.ExecuteNonQuery();
                if (r > 0)
                {

                }
                else
                {

                }
                connection.Close();
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
        }
    }
}