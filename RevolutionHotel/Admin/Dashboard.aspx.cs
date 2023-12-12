using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RevolutionHotel.Admin
{
    public partial class Dashboard : System.Web.UI.Page
    {
        SqlConnection connection;
        SqlDataReader reader;
        SqlCommand command;
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

        public int TotalCustomers()
        {
            int customers = 0;
            try
            {
                connection = Components.GetConnectionToBD();
                string query = @"SELECT * FROM Customer";
                command = new SqlCommand(query, connection);
                reader = command.ExecuteReader();
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        customers++;
                    }
                }
            }
            catch(Exception ex)
            {
                ex.Data.Clear();
            }

            return customers;
        }

        public int TotalOrders()
        {
            int orders = 0;
            try
            {
                connection = Components.GetConnectionToBD();
                string query = @"SELECT * FROM Orders";
                command = new SqlCommand(query, connection);
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        orders++;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }

            return orders;
        }

        public int TotalMessages()
        {
            int messages = 0;
            try
            {
                connection = Components.GetConnectionToBD();
                string query = @"SELECT * FROM Contact";
                command = new SqlCommand(query, connection);
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        messages++;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }

            return messages;
        }

        public int TotalRecipes()
        {
            int recipes = 0;
            try
            {
                connection = Components.GetConnectionToBD();
                string query = @"SELECT * FROM Food";
                command = new SqlCommand(query, connection);
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        recipes++;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }

            return recipes;
        }
    }
}