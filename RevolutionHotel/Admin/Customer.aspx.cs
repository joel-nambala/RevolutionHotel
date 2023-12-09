using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RevolutionHotel.Admin
{
    public partial class Customer : System.Web.UI.Page
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

        public string CustomerList()
        {
            string htmlStr = string.Empty;
            try
            {
                connection = Components.GetConnectionToBD();
                string query = @"SELECT * FROM Customer";
                command = new SqlCommand(query, connection);
                reader = command.ExecuteReader();
                int counter = 0;

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        counter++;
                        htmlStr += string.Format(@"
                        <tr>
                            <td><a class=""link-primary text-decoration-none"" href=""#"">{0}</a></td>
                            <td>
                                <img src=""{7}"" alt=""{2}"" class=""img-sm"" />
                            </td>
                            <td>{1}</td>
                            <td>{2}</td>
                            <td>{3}</td>
                            <td>{4}</td>
                            <td>{8}</td>
                            <td><a class=""link-info text-decoration-none"" href=""CustomerDetails.aspx?customerid={5}&email={6}"">View</a></td>
                        </tr>",
                        counter,
                        reader["FullName"].ToString(),
                        Convert.ToDateTime(reader["DateOfBirth"]).ToString("dd-MM-yyyy"),
                        reader["Gender"].ToString(),
                        reader["Country"].ToString(),
                        reader["CustomerId"].ToString(),
                        reader["Email"].ToString(),
                        reader["ProfileImage"].ToString(),
                        reader["Blocked"].ToString()
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