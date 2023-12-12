using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RevolutionHotel.Layouts
{
    public partial class User : System.Web.UI.MasterPage
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
                LoadCustomerDetails();
            }
        }

        private void LoadCustomerDetails()
        {
            try
            {
                string username = Session["username"].ToString();
                string imgUrl = "";
                string fullName = string.Empty;
                string gender = string.Empty;
                connection = Components.GetConnectionToBD();
                string query = @"SELECT * FROM Customer WHERE Username=@Username";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                reader = command.ExecuteReader();
                reader.Read();
                imgUrl = reader["ProfileImage"].ToString();
                fullName = reader["FullName"].ToString();
                gender = reader["Gender"].ToString();

                if (imgUrl == "")
                {
                    if (gender == "Male")
                    {
                        ImgProfilePic.ImageUrl = $"~/Images/profile_m.png";
                    }

                    if (gender == "Female")
                    {
                        ImgProfilePic.ImageUrl = $"~/Images/profile_f.png";
                    }
                }
                else
                {
                    ImgProfilePic.ImageUrl = imgUrl;
                    ImgProfilePic.Attributes.Add("alt", fullName);
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
        }

        protected void lbtnlogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.RemoveAll();
            Session.Clear();
            Response.Redirect("~/Default.aspx");
        }
    }
}