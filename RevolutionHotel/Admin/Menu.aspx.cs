using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RevolutionHotel.Admin
{
    public partial class Menu : System.Web.UI.Page
    {
        SqlConnection connection;
        SqlCommand command;
        SqlDataReader reader;
        protected void Page_Load(object sender, EventArgs e)
        {

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
                //link-info text-decoration-none

                if (reader.HasRows)
                {
                    while (reader.Read())
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
                                <td>{5}</td>
                                <td>$ {6}</td>
                                <td>
                                    <a href=""NewMenu.aspx?foodid={7}&foodname={8}&action=update"" class=""btn btn-warning"">Update</a>
                                    <a href=""NewMenu.aspx?foodid={7}&foodname={8}&action=delete"" class=""btn btn-danger"">Delete</a>
                                </td>
                            </tr>",
                            reader["FoodId"].ToString(),
                            reader["FoodImg"].ToString(),
                            reader["FoodName"].ToString(),
                            reader["FoodName"].ToString(),
                            reader["FoodCourse"].ToString(),
                            reader["SoldOut"].ToString(),
                            reader["Price"].ToString(),
                            reader["FoodId"].ToString(),
                            reader["FoodName"].ToString()
                        );
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