using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RevolutionHotel.User
{
    public partial class OrderDetails : System.Web.UI.Page
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
                LoadOrderDetails();
            }
        }

        private void LoadOrderDetails()
        {
            try
            {
                string orderId = Request.QueryString["orderid"].ToString();
                connection = Components.GetConnectionToBD();
                string query = @"SELECT * FROM Orders WHERE OrderId=@orderId";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@orderId", orderId);

                reader = command.ExecuteReader();
                if(reader.HasRows)
                {
                    reader.Read();
                    ImgFood.ImageUrl = reader["FoodImg"].ToString();
                    lblCustomerName.Text = reader["CustomerName"].ToString();
                    lblFoodName.Text = reader["FoodName"].ToString();
                    lblOrderDescription.Text = reader["OrderDescription"].ToString();
                    lblQuantity.Text = reader["Quantity"].ToString();
                    lblTotalPrice.Text = $"$ {reader["TotalPrice"].ToString()}";
                }
                connection.Close();
            }
            catch(Exception ex)
            {
                ex.Data.Clear();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                CancelOrder();
            }
            catch(Exception ex)
            {
                ex.Data.Clear();
            }
        }

        private void CancelOrder()
        {
            try
            {
                string status = "Cancelled";
                connection = Components.GetConnectionToBD();
                string query = @"UPDATE Orders SET Status=@Status WHERE OrderId=@orderId";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Status", status);
                command.Parameters.AddWithValue("@orderId", Request.QueryString["orderid"].ToString());

                int res = command.ExecuteNonQuery();
                if(res > 0)
                {
                    SuccessMessage("Order has been cancelled succesifully!");
                }
                else
                {
                    Message("Something went wrong!");
                }
                connection.Close ();
            }
            catch(Exception ex)
            {
                ex.Data.Clear();
            }
        }

        protected void btnPayment_Click(object sender, EventArgs e)
        {
            Response.Redirect("Payment.aspx");
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