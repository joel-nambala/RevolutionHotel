using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RevolutionHotel.Admin
{
    public partial class Orders : System.Web.UI.Page
    {
        SqlConnection connection;
        SqlCommand command;
        SqlDataReader reader;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["admin"] ==  null)
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }
            }
        }

        public string OrderList()
        {
            string htmString = string.Empty;
            try
            {
                connection = Components.GetConnectionToBD();
                string query = @"SELECT * FROM Orders";
                command = new SqlCommand(query, connection);

                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        htmString += string.Format(@"
                        <tr>
                            <td>{0}</td>
                            <td>
                                <img src=""{1}"" alt=""{2}"" class=""img-sm"" />
                            </td>
                            <td>{2}</td>
                            <td>$ {3}</td>                            
                            <td>{4}</td>
                            <td>{5}</td>
                            <td>
                                <a href=""OrderDetails.aspx?orderid={6}"" class=""link-primary text-decoration-none"">View</a>
                            </td>
                        </tr>""",
                        reader["CustomerName"].ToString(),
                        reader["FoodImg"].ToString(),
                        reader["FoodName"].ToString(),
                        reader["TotalPrice"].ToString(),
                        reader["Quantity"].ToString(),
                        reader["Status"].ToString(),
                        reader["OrderId"].ToString()
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }

            return htmString;
        }
    }
}