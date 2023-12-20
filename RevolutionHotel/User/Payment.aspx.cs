using RevolutionHotel.Admin;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RevolutionHotel.User
{
    public partial class Payment : System.Web.UI.Page
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
                if (Session["orderid"] == null)
                {
                    Response.Redirect("Orders.aspx");
                    return;
                }
                string orderId = Session["orderid"].ToString();
                LoadOrderDetails(orderId);
            }
        }

        private void LoadOrderDetails(string orderId)
        {
            try
            {
                string foodId = GetOrderDetails(orderId)[0];
                string foodName = GetOrderDetails(orderId)[1];
                string foodImg = GetOrderDetails(orderId)[2];
                string customerId = GetOrderDetails(orderId)[3];
                string customerName = GetOrderDetails(orderId)[4];
                string totalPrice = GetOrderDetails(orderId)[5];

                lblFoodName.Text = foodName;
                lblCustomerName.Text = customerName;
                lblPrice.Text = $"$ {totalPrice}";
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
        }

        private string[] GetOrderDetails(string orderId)
        {
            string[] orderArr = new string[6];
            try
            {
                connection = Components.GetConnectionToBD();
                string query = @"SELECT * FROM Orders WHERE OrderId = @id";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", orderId);

                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    orderArr[0] = reader["FoodId"].ToString();
                    orderArr[1] = reader["FoodName"].ToString();
                    orderArr[2] = reader["FoodImg"].ToString();
                    orderArr[3] = reader["CustomerId"].ToString();
                    orderArr[4] = reader["CustomerName"].ToString();
                    orderArr[5] = reader["TotalPrice"].ToString();
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return orderArr;
        }

        private void UpdateOrdersTable()
        {
            try
            {
                string payment = "true";
                string orderId = Session["orderid"].ToString();
                connection = Components.GetConnectionToBD();
                string query = @"UPDATE Orders SET Payment=@Payment WHERE OrderId=@orderId";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Payment", payment);
                command.Parameters.AddWithValue("@orderId", orderId);

                int r=command.ExecuteNonQuery();
                if (r > 0)
                {

                }
                else
                {

                }
            }
            catch (Exception ex) { ex.Data.Clear(); }
        }

        protected void btnPayment_Click(object sender, EventArgs e)
        {
            try
            {
                string orderId = Session["orderid"].ToString();
                string foodId = GetOrderDetails(orderId)[0];
                string foodName = GetOrderDetails(orderId)[1];
                string foodImg = GetOrderDetails(orderId)[2];
                string customerId = GetOrderDetails(orderId)[3];
                string customerName = GetOrderDetails(orderId)[4];
                string totalPrice = GetOrderDetails(orderId)[5];
                DateTime time = DateTime.Now;
                string transactionCode = Components.GenerateRandomTransactionId(customerName);
                connection = Components.GetConnectionToBD();
                string query = @"INSERT INTO Transactions VALUES(@TransactionCode, @CustomerId, @CustomerName, @FoodId, @FoodName, @AmountPaid, @CreatedTime)";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TransactionCode", transactionCode);
                command.Parameters.AddWithValue("@CustomerId", customerId);
                command.Parameters.AddWithValue("@CustomerName", customerName);
                command.Parameters.AddWithValue("@FoodId", foodId);
                command.Parameters.AddWithValue("@FoodName", foodName);
                command.Parameters.AddWithValue("@AmountPaid", totalPrice);
                command.Parameters.AddWithValue("@CreatedTime", time);

                int result = command.ExecuteNonQuery();
                if (result > 0)
                {
                    SuccessMessage("Transaction successiful.");
                    UpdateOrdersTable();
                }
                else
                {
                    Message("Something went wrong");
                }
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
            string myPage = "Orders.aspx";
            string strScript = "<script>alert('" + message + "');window.location='" + myPage + "'</script>";
            ClientScript.RegisterStartupScript(GetType(), "Client Script", strScript.ToString());
        }
    }
}