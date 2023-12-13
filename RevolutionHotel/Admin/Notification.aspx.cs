using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RevolutionHotel.Admin
{
    public partial class Notification : System.Web.UI.Page
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
                ReadNotification();
            }
        }

        private void ReadNotification()
        {
            try
            {
                string id = Request.QueryString["id"].ToString();
                string status = "read";
                connection = Components.GetConnectionToBD();
                string query = @"UPDATE Notification SET Status=@status WHERE Id=@id";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@status", status);
                command.Parameters.AddWithValue("@id", id);

                int res = command.ExecuteNonQuery();
                if(res > 0)
                {

                }
                else
                {

                }
                connection.Close();
            }
            catch(Exception ex)
            {
                ex.Data.Clear();
            }
        }
    }
}