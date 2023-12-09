using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RevolutionHotel.Admin
{
    public partial class NewMenu : System.Web.UI.Page
    {
        SqlConnection connection;
        SqlCommand command;
        SqlDataReader reader;
        string foodId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["admin"] == null)
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }
                pnSoldout.Visible = false;

                if (Request.QueryString["foodid"] != null)
                {
                    string action = Request.QueryString["action"].ToString();
                    FillInputFields();
                    if (action == "update")
                    {
                        // Update
                        pnSoldout.Visible = true;
                        lblHeader.Text = "Update Recipe";
                        btnAdd.Text = "Update Recipe";
                        btnAdd.CssClass = "btn btn-warning";
                        hlBack.Visible = true;
                        pnSoldout.Visible = true;
                    }

                    if (action == "delete")
                    {
                        // Detete
                        lblHeader.Text = "Delete Recipe";
                        btnAdd.Text = "Delete Recipe";
                        btnAdd.CssClass = "btn btn-danger";
                        hlBack.Visible = true;
                        pnSoldout.Visible = true;
                        txtName.ReadOnly = true;
                        txtPrice.ReadOnly = true;
                        ddlCourse.Enabled = false;
                        ddlState.Enabled = false;
                        ddlSoldOut.Enabled = false;
                        fuFoodImage.Enabled = false;
                    }
                }
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

            // Validate food course
            if (ddlCourse.SelectedValue == "0")
            {
                Message("Food Course cannot be empty");
                return;
            }

            // Validate state
            if (ddlState.SelectedValue == "0")
            {
                Message("State cannot be null");
                return;
            }

            try
            {
                string imagePath = string.Empty;
                bool isValidToexecute = false;
                string msg = string.Empty;

                if (Request.QueryString["foodid"] != null)
                {
                    string action = Request.QueryString["action"].ToString();
                    foodId = Request.QueryString["foodid"].ToString();

                    if (action == "update")
                    {
                        // Update the recipe
                        connection = Components.GetConnectionToBD();
                        string query = @"UPDATE Food SET FoodName=@FoodName, FoodCourse=@FoodCourse, Price=@Price, State=@State, SoldOut=@SoldOut WHERE FoodId=@FoodId";
                        command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@FoodName", txtName.Text.Trim());
                        command.Parameters.AddWithValue("@FoodCourse", ddlCourse.SelectedValue);
                        command.Parameters.AddWithValue("@Price", txtPrice.Text.Trim());
                        command.Parameters.AddWithValue("@State", ddlState.SelectedValue);
                        command.Parameters.AddWithValue("@SoldOut", ddlSoldOut.SelectedValue);
                        command.Parameters.AddWithValue("@FoodId", foodId);
                        msg = "updated";
                        isValidToexecute = true;
                    }

                    if (action == "delete")
                    {
                        // Delete the recipe
                        isValidToexecute = true;
                    }
                }
                else
                {
                    string soldOut = "No";
                    string foodId = Components.GenerateRandomId();
                    DateTime time = DateTime.Now;

                    connection = Components.GetConnectionToBD();
                    string query = @"INSERT INTO Food VALUES(@FoodId, @FoodName, @FoodCourse, @Price, @SoldOut, @State, @FoodImg, @CreatedTime)";
                    command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@FoodId", foodId);
                    command.Parameters.AddWithValue("@FoodName", txtName.Text.Trim());
                    command.Parameters.AddWithValue("@FoodCourse", ddlCourse.SelectedValue);
                    command.Parameters.AddWithValue("@Price", txtPrice.Text.Trim());
                    command.Parameters.AddWithValue("@SoldOut", soldOut);
                    command.Parameters.AddWithValue("@State", ddlState.SelectedValue);
                    command.Parameters.AddWithValue("@CreatedTime", time);
                    msg = "created";

                    if (fuFoodImage.HasFile)
                    {
                        if (ValidExtension(fuFoodImage.FileName))
                        {
                            Guid obj = Guid.NewGuid();
                            imagePath = "/Images/" + obj.ToString() + fuFoodImage.FileName;
                            fuFoodImage.PostedFile.SaveAs(Server.MapPath("~/Images/") + obj.ToString() + fuFoodImage.FileName);
                            command.Parameters.AddWithValue("@FoodImg", imagePath);
                            isValidToexecute = true;
                        }
                        else
                        {
                            Message("Food Image accepts files with .png, .jpg and .jpeg extensions only!");
                            isValidToexecute = false;
                        }
                    }
                    else
                    {
                        Message("Please upload the recipe image. It cannot be null or empty");
                        return;
                    }
                }

                if (isValidToexecute)
                {
                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        SuccessMessage($"Food item has been {msg} successifully!");
                    }
                    else
                    {
                        lblMsg.Text = "Something went wrong. Please try again the next time!";
                        lblMsg.Visible = true;
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Message(ex.Message);
            }
        }

        private void FillInputFields()
        {
            try
            {
                foodId = Request.QueryString["foodid"];
                connection = Components.GetConnectionToBD();
                string query = @"SELECT * FROM Food WHERE FoodId=@FoodId";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FoodId", foodId);

                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    txtName.Text = reader["FoodName"].ToString();
                    txtPrice.Text = reader["Price"].ToString();
                    ddlCourse.SelectedValue = reader["FoodCourse"].ToString();
                    ddlState.SelectedValue = reader["State"].ToString();
                    ddlSoldOut.SelectedValue = reader["SoldOut"].ToString();
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Message(ex.Message);
            }
        }

        protected bool ValidExtension(string filename)
        {
            bool b = false;
            string[] fileExtension = { ".png", ".jpg", ".jpeg" };
            for (int i = 0; i < fileExtension.Length; i++)
            {
                if (fileExtension.Contains(fileExtension[i]))
                {
                    b = true;
                    break;
                }
            }
            return b;
        }

        protected void Message(string message)
        {
            string strScript = "<script>alert('" + message + "');</script>";
            ClientScript.RegisterStartupScript(GetType(), "Client Script", strScript.ToString());
        }

        protected void SuccessMessage(string message)
        {
            string myPage = "Menu.aspx";
            string strScript = "<script>alert('" + message + "');window.location='" + myPage + "'</script>";
            ClientScript.RegisterStartupScript(GetType(), "Client Script", strScript.ToString());
        }
    }
}