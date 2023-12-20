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
                LoadOrderDetails();
            }
        }

        private void LoadOrderDetails()
        {
            try
            {
                string foodId = Request.QueryString["foodid"];
                string username = Session["username"].ToString();
                string[] customerDetails = CustomerDetails(username);
                string[] foodDetails = FoodDetails(foodId);

                ImgFood.ImageUrl = foodDetails[3];
                ImgFood.Attributes.Add("alt", foodDetails[1]);
                lblFoodName.Text = foodDetails[1];
                lblPrice.Text = $"$ {foodDetails[2]}";
                lblCustomerName.Text = customerDetails[1];
                lblAddress.Text = customerDetails[2];

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
            foodDetails[3] = reader["FoodImg"].ToString();

            return foodDetails;
        }

        protected void btnOrder_Click(object sender, EventArgs e)
        {
            try
            {
                string foodId = Request.QueryString["foodid"];
                string username = Session["username"].ToString();

                string[] customerDetails = CustomerDetails(username);
                string[] foodDetails = FoodDetails(foodId);

                string customerId = customerDetails[0].ToString();
                string customerName = customerDetails[1].ToString();
                string customerAddress = customerDetails[2].ToString();
                string foodName = foodDetails[1].ToString();
                string price = foodDetails[2].ToString();
                string foodImg = foodDetails[3].ToString();

                string orderId = Components.GenerateRandomId();
                string status = "Pending"; // Pending, Approved, Cancelled;
                string payment = "false"; // true, false
                string quantityRequested = txtQuantity.Text.Trim();
                int totalPrice = Convert.ToInt32(price) * Convert.ToInt32(quantityRequested);
                DateTime time = DateTime.Now;

                string description = $"The food {foodName} will be delivered to {customerName} at {customerAddress} within the next 25 hours.";

                connection = Components.GetConnectionToBD();
                string query = @"INSERT INTO Orders VALUES(@OrderId, @FoodId, @FoodName, @FoodImg, @CustomerId, @CustomerName, @Address, 
                                @TotalPrice, @OrderDescription, @Quantity, @Status,  @CreatedTime, @Payment)";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@OrderId", orderId);
                command.Parameters.AddWithValue("@CustomerId", customerId);
                command.Parameters.AddWithValue("@FoodId", foodId);
                command.Parameters.AddWithValue("@CustomerName", customerName);
                command.Parameters.AddWithValue("@FoodName", foodName);
                command.Parameters.AddWithValue("@Address", customerAddress);
                command.Parameters.AddWithValue("@TotalPrice", totalPrice);
                command.Parameters.AddWithValue("@OrderDescription", description);
                command.Parameters.AddWithValue("@Quantity", txtQuantity.Text.Trim());
                command.Parameters.AddWithValue("@Status", status);
                command.Parameters.AddWithValue("@Payment", payment);
                command.Parameters.AddWithValue("@FoodImg", foodImg);
                command.Parameters.AddWithValue("@CreatedTime", time);

                int result = command.ExecuteNonQuery();
                if (result > 0)
                {
                    SuccessMessage($"Order {orderId} has been placed succesifully! The food will be delivered to {customerName} at {customerAddress} within the next 24 hours.");
                    SendNotification(customerId, customerName, customerAddress, foodName);
                }
                else
                {
                    Message("Something went wrong!");
                }
                connection.Close();
            }
            catch (SqlException ex)
            {
                Message(ex.Message);
            }
            catch (Exception ex)
            {
                Message(ex.Message);
                ex.Data.Clear();
            }
        }

        private void SendNotification(string customerId, string customerName, string customerAddress, string foodName)
        {
            try
            {
                string id = Components.GenerateRandomId();
                DateTime time = DateTime.Now;
                string status = "unread"; // unread, read
                string description = $"{customerName} has placed an order of {foodName} to be delivered at {customerAddress}.";

                connection = Components.GetConnectionToBD();
                string query = @"INSERT INTO Notification VALUES(@Id, @Description, @SenderId, @SenderName, @Status, @CreatedAt)";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@Description", description);
                command.Parameters.AddWithValue("@SenderId", customerId);
                command.Parameters.AddWithValue("@SenderName", customerName);
                command.Parameters.AddWithValue("@Status", status);
                command.Parameters.AddWithValue("@CreatedAt", time);

                int result = command.ExecuteNonQuery();
                if(result > 0)
                {

                }
                else
                {

                }
                //connection.Close();
            }
            catch (Exception ex)
            {
                Message(ex.Message);
                ex.Data.Clear();
            }
        }

        private void Message(string message)
        {
            string strScript = "<script>alert('" + message + "');</script>";
            ClientScript.RegisterStartupScript(GetType(), "Client Script", strScript.ToString());
        }

        private void SuccessMessage(string message)
        {
            string page = "Orders.aspx";
            string strScript = "<script>alert('" + message + "');window.location='" + page + "';</script>";
            ClientScript.RegisterStartupScript(GetType(), "Client Script", strScript.ToString());
        }
    }
}