using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RevolutionHotel.User
{
    public partial class Order : System.Web.UI.Page
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
                MakeOrder();
            }
        }

        private void MakeOrder()
        {
            try
            {
                string foodId = Request.QueryString["foodid"];
                string username = Session["username"].ToString();
                string[] customerDetails = CustomerDetails(username);
                string[] foodDetails = FoodDetails(foodId);
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
        }

        private string[] CustomerDetails(string username)
        {
            string[] customerDetails = new string[5];
            connection = Components.GetConnectionToBD();
            string query = @"SELECT * FROM Customer WHERE Username=@Username";
            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Username", username);
            reader = command.ExecuteReader();
            reader.Read();
            customerDetails[0] = reader["CustomerId"].ToString();
            customerDetails[1] = reader["FullName"].ToString();
            customerDetails[2] = reader["Address"].ToString();
            return customerDetails;
        }

        private string[] FoodDetails(string foodId)
        {
            string[] foodDetails = new string[5];
            connection = Components.GetConnectionToBD();
            string query = @"SELECT * FROM Food WHERE FoodId=@FoodId";
            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@FoodId", foodId);
            reader = command.ExecuteReader();
            reader.Read();
            foodDetails[0] = foodId;
            foodDetails[1] = reader["FoodName"].ToString();
            foodDetails[2] = reader["Price"].ToString();

            return foodDetails;
        }

        private double ConvertToKSH(int usd)
        {
            return usd * 150.00;
        }

        private void Message(string message)
        {
            string strScript = "<script>alert('" + message + "');</script>";
            ClientScript.RegisterStartupScript(GetType(), "Client Script", strScript.ToString());
        }

        private void SuccessMessage(string message)
        {
            string page = "Orders.aspx";
            string strScript = "<script>alert('" + message + "');window.location='" + page + "'</script>";
            ClientScript.RegisterStartupScript(GetType(), "Client Script", strScript.ToString());
        }
    }
}