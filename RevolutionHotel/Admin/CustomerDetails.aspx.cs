using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RevolutionHotel.Admin
{
    public partial class CustomerDetails : System.Web.UI.Page
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
                LoadCustomerDetails();
            }
        }

        private void LoadCustomerDetails()
        {
            try
            {
                string username = Request.QueryString["customerid"];
                string email = Request.QueryString["email"];

                connection = Components.GetConnectionToBD();
                string query = @"SELECT * FROM Customer WHERE CustomerId=@Username";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);

                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    string imgUrl = string.Empty;
                    string gender = string.Empty;
                    reader.Read();
                    txtName.Text = reader["FullName"].ToString();
                    txtCustomerId.Text = reader["CustomerId"].ToString();
                    txtPhone.Text = reader["PhoneNumber"].ToString();
                    txtEmail.Text = reader["Email"].ToString();
                    txtPostalAddress.Text = reader["PostalAddress"].ToString();
                    txtAddress.Text = reader["Address"].ToString();
                    ddlBlocked.SelectedValue = reader["Blocked"].ToString();
                    txtCountry.Text = reader["Country"].ToString();
                    imgUrl = reader["ProfileImage"].ToString();
                    gender = reader["Gender"].ToString();

                    if (imgUrl == "")
                    {
                        string profilePic = string.Empty;
                        if (gender == "Male")
                        {
                            profilePic = "profile_m";
                        }

                        if (gender == "Female")
                        {
                            profilePic = "profile_f";
                        }
                        ImgProfilePic.ImageUrl = $"~/Images/{profilePic}.png";
                    }
                    else
                    {
                        ImgProfilePic.ImageUrl = imgUrl;
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string username = Request.QueryString["customerid"];
                connection = Components.GetConnectionToBD();
                string query = @"UPDATE Customer SET Blocked=@Blocked WHERE CustomerId=@Username";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Blocked", ddlBlocked.SelectedValue);

                int res = command.ExecuteNonQuery();
                if (res > 0)
                {
                    SuccessMessage("Customer updated successifully");
                }
                else
                {
                    Message("Something went wrong. Please try again!");
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Message(ex.Message);
            }
        }

        protected void Message(string message)
        {
            string strScript = "<script>alert('" + message + "');</script>";
            ClientScript.RegisterStartupScript(GetType(), "Client Script", strScript.ToString());
        }

        protected void SuccessMessage(string message)
        {
            string myPage = "Customer.aspx";
            string strScript = "<script>alert('" + message + "');window.location='" + myPage + "'</script>";
            ClientScript.RegisterStartupScript(GetType(), "Client Script", strScript.ToString());
        }
    }
}