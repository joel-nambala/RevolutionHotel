using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RevolutionHotel
{
    public partial class Register : System.Web.UI.Page
    {
        SqlConnection connection;
        SqlCommand command;
        SqlDataReader reader;
        protected void Page_Load(object sender, EventArgs e)
        {
            pnCounty.Visible = false;

            if (!IsPostBack)
            {
                LoadCountries();
                LoadCounties();
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                string imagePath = string.Empty;
                bool isValidToexecute = false;
                string blocked = "No";

                // Matching passwords
                if (txtPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
                {
                    lblMsg.Text = "Passwords do not match";
                    lblMsg.Visible = true;
                    return;
                }

                string CustomerId = Components.GenerateRandomId();

                connection = Components.GetConnectionToBD();
                string query = @"INSERT INTO Customer VALUES(@CustomerId, @Username, @Password, @Email, @Gender, @Country, @County, @PostalAddress, @Address,
                                @FullName, @PhoneNumber, @DateOfBirth, @ProfileImage, @Blocked)";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CustomerId", CustomerId.ToString());
                command.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());                
                command.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                command.Parameters.AddWithValue("@Gender", ddlGender.SelectedValue);
                command.Parameters.AddWithValue("@Country", ddlCountry.SelectedValue);
                command.Parameters.AddWithValue("@County", ddlCounty.SelectedValue);
                command.Parameters.AddWithValue("@PostalAddress", txtPostalAddress.Text.Trim());
                command.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                command.Parameters.AddWithValue("@FullName", txtFullName.Text.Trim());
                command.Parameters.AddWithValue("@PhoneNumber", txtPhone.Text.Trim());
                command.Parameters.AddWithValue("@DateOfBirth", txtBirthDate.Text.Trim());
                command.Parameters.AddWithValue("@Blocked", blocked);

                if (Components.ValidPassword(txtPassword.Text.Trim()))
                {
                    command.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());
                }
                else
                {
                    Message("Password must have 8 characters, atleast one uppercase letter, one lowercase letter and one special character.");
                    return;
                }

                if (fuProfilePicture.HasFile)
                {

                    if (ValidExtension(fuProfilePicture.FileName))
                    {
                        Guid obj = Guid.NewGuid();
                        imagePath = "/Images/" + obj.ToString() + fuProfilePicture.FileName;
                        fuProfilePicture.PostedFile.SaveAs(Server.MapPath("~/Images/") + obj.ToString() + fuProfilePicture.FileName);
                        command.Parameters.AddWithValue("@ProfileImage", imagePath);
                        isValidToexecute = true;
                    }
                    else
                    {
                        lblMsg.Text = "Upload files with .png, .jpg and .jpeg file extensions only";
                        lblMsg.Visible = true;
                        isValidToexecute = false;
                    }
                }
                else
                {
                    command.Parameters.AddWithValue("@ProfileImage", imagePath);
                    isValidToexecute = true;
                }

                if (isValidToexecute)
                {
                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        SuccessMessage("Account has been created successifully...!");
                    }
                    else
                    {
                        lblMsg.Text = "Something went wrong...!";
                        lblMsg.Visible = true;
                    }
                }
                connection.Close();
            }
            catch(SqlException ex)
            {
                string errorMsg = ex.Message;
                if(errorMsg.Contains("Violation of UNIQUE KEY constraint"))
                {
                    Message($"Username {txtUsername.Text.Trim()} already exists. Please try another one!");
                    return;
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
        }

        private bool ValidExtension(string filename)
        {
            bool b = false;
            try
            {
                string[] fileExtensions = { ".png", ".jpg", ".jpeg" };
                for (int i = 0; i < fileExtensions.Length; i++)
                {
                    if (filename.Contains(fileExtensions[i]))
                    {
                        b = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return b;
        }

        private void LoadCountries()
        {
            try
            {
                ddlCountry.Items.Clear();
                string sqlStmnt = "spGetCountries";
                connection = Components.GetConnectionToBD();
                command = new SqlCommand();
                command.CommandText = sqlStmnt;
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = connection;

                ListItem li = new ListItem("--Select--", "0");
                ddlCountry.Items.Add(li);

                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        li = new ListItem(reader["Name"].ToString(), reader["Name"].ToString());
                        ddlCountry.Items.Add(li);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
        }

        private void LoadCounties()
        {
            try
            {
                ddlCounty.Items.Clear();
                string sqlStmnt = "spGetCounties";
                connection = Components.GetConnectionToBD();
                command = new SqlCommand();
                command.CommandText = sqlStmnt;
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = connection;

                ListItem li = new ListItem("--Select--", "0");
                ddlCounty.Items.Add(li);

                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        li = new ListItem(reader["Name"].ToString(), reader["Name"].ToString());
                        ddlCounty.Items.Add(li);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string country = ddlCountry.SelectedValue.ToString();

                if (country == "Kenya")
                {
                    pnCounty.Visible = true;
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
            string myPage = "Default.aspx";
            string strScript = "<script>alert('" + message + "');window.location='" + myPage + "'</script>";
            ClientScript.RegisterStartupScript(GetType(), "Client Script", strScript.ToString());
        }
    }
}