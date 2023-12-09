using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RevolutionHotel.Layouts
{
    public partial class Admin : System.Web.UI.MasterPage
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
            }
        }

        public string Messages()
        {
            string htmlStr = string.Empty;
            try
            {
                string status = "unread";
                connection = Components.GetConnectionToBD();
                string query = @"SELECT * FROM Contact WHERE Status=@Status";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Status", status);
                reader = command.ExecuteReader();
                int count = 0;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        count++;
                        htmlStr += string.Format(@"
                            <a class=""dropdown-item underline-bottom"" href=""Message.aspx?id={2}&email={3}"">                               
                                <div class=""item-content flex-grow"">
                                    <h6 class=""ellipsis font-weight-normal"">{0}</h6>
                                    <p class=""font-weight-light small-text text-muted mb-0"">{1}</p>
                                </div>
                            </a>",
                            reader["Name"].ToString(),
                            reader["Subject"].ToString(),
                            reader["Id"].ToString().ToLower(),
                            reader["Email"].ToString()
                        );
                    }
                    if (count > 0)
                    {
                        lblCount.Visible = true;
                    }
                }
                else
                {
                    htmlStr = "<p class=\"text-center\">No Messages</p>";
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return htmlStr;
        }

        protected void lbtnlogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.RemoveAll();
            Session.Clear();
            Response.Redirect("~/Default.aspx");
        }
    }
}