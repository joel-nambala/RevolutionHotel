using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RevolutionHotel.User
{
    public partial class Menu : System.Web.UI.Page
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
            }
        }

        public string RecipeList()
        {
            string htmlStr = string.Empty;
            try
            {
                connection = Components.GetConnectionToBD();
                string query = @"SELECT * FROM Food";
                command = new SqlCommand(query, connection);
                reader = command.ExecuteReader();
                int counter = 0;
                string soldOut = "No";
                //link-info text-decoration-none

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (reader["SoldOut"].ToString() == soldOut)
                        {
                            counter++;
                            htmlStr += string.Format(@"
                            <tr>
                                <td>{0}</td>
                                <td>
                                    <img src=""{1}"" alt=""{2}"" class=""img-sm"" />
                                </td>
                                <td>{3}</td>
                                <td>{4}</td>
                                <td>$ {5}</td>
                                <td>
                                    <a href=""Order.aspx?foodid={6}&foodname={7}"" class=""btn btn-primary"">Order</a>
                                </td>
                            </tr>",
                                reader["FoodId"].ToString(),
                                reader["FoodImg"].ToString(),
                                reader["FoodName"].ToString(),
                                reader["FoodName"].ToString(),
                                reader["FoodCourse"].ToString(),
                                reader["Price"].ToString(),
                                reader["FoodId"].ToString(),
                                reader["FoodName"].ToString()
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return htmlStr;
        }
    }
}