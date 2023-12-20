using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RevolutionHotel.Admin
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
                if (Session["admin"] == null)
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }
                OrderDeliveredOrCancelled();
                LoadOrderDetails();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string orderId = Request.QueryString["orderid"].ToString();
                //string orderStatus = Session["orderStatus"].ToString();
                connection = Components.GetConnectionToBD();
                if (OrderPaid()) // order has been paid for
                {
                    string query = @"UPDATE Orders SET Status=@Status WHERE OrderId=@orderId";
                    command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@orderId", orderId);
                    command.Parameters.AddWithValue("@Status", ddlApprove.SelectedValue);

                    int res = command.ExecuteNonQuery();
                    if (res > 0)
                    {
                        SuccessMessage($"Order {orderId} has been {ddlApprove.SelectedValue} succesifully!");
                    }
                    else
                    {
                        Message("Something went wrong!");
                    }

                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "The order has to be paid for in order to approve";
                    lblMsg.CssClass = "alert alert-danger";
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
        }

        private bool OrderPaid()
        {
            bool b = false;
            try
            {
                string id = Request.QueryString["orderid"].ToString();
                connection = Components.GetConnectionToBD();
                string query = @"SELECT * FROM Orders WHERE OrderId=@id";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    string payment = reader["Payment"].ToString();
                    if (payment == "true")
                    {
                        b = true;
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return b;
        }

        private void OrderDeliveredOrCancelled()
        {
            try
            {
                string orderId = Request.QueryString["orderid"].ToString();
                connection = Components.GetConnectionToBD();
                string query = @"SELECT * FROM Orders WHERE OrderId=@orderId";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@orderId", orderId);

                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    string status = reader["Status"].ToString();

                    if (status == "Delivered" || status == "Cancelled")
                    {
                        ddlApprove.Enabled = false;
                        ddlApprove.SelectedValue = status;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
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
                if (reader.HasRows)
                {
                    reader.Read();
                    //ImgFood.ImageUrl = reader["FoodImg"].ToString();
                    lblCustomerName.Text = reader["CustomerName"].ToString();
                    lblFoodName.Text = reader["FoodName"].ToString();
                    lblOrderDescription.Text = reader["OrderDescription"].ToString();
                    lblQuantity.Text = reader["Quantity"].ToString();
                    lblTotalPrice.Text = $"$ {reader["TotalPrice"].ToString()}";

                    string status = reader["Status"].ToString();
                    ddlApprove.SelectedValue = status;
                    if (status == "Cancelled")
                    {
                        hlBack.Visible = true;
                        btnUpdate.Visible = false;
                    }

                    string payment = reader["Payment"].ToString();
                    if (payment == "false")
                    {
                        lblPayment.Text = "Pending";
                        lblPayment.CssClass = "text-danger";
                    }
                    if (payment == "true")
                    {
                        lblPayment.Text = "Paid";
                        lblPayment.CssClass = "text-success";
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
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
            string strScript = "<script>alert('" + message + "');window.location='" + page + "'</script>";
            ClientScript.RegisterStartupScript(GetType(), "Client Script", strScript.ToString());
        }
    }
}