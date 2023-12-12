<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Layouts/Admin.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="RevolutionHotel.Admin.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section>
        <div class="dashboard-header">
            <div class="d-flex justify-content-between align-items-center g-4 flex-wrap">
                <div class="bg-success p-2 flex-fill me-3">
                    <a href="Customer.aspx" class="text-decoration-none text-white">
                        <h5 class="text-capitalize">Total customers</h5>
                        <h4><% =TotalCustomers() %></h4>
                    </a>
                </div>
                <div class="bg-danger p-2 flex-fill me-3">
                    <a href="Orders.aspx" class="text-decoration-none text-white">
                        <h5 class="text-capitalize">Total orders</h5>
                        <h4><% =TotalOrders() %></h4>
                    </a>
                </div>
                <div class="bg-info p-2 flex-fill me-3">
                    <a href="#" class="text-decoration-none text-white">
                        <h5 class="text-capitalize">Total messages</h5>
                       <h4><% =TotalMessages() %></h4>
                    </a>
                </div>
                <div class="bg-warning p-2 flex-fill me-3">
                    <a href="Menu.aspx" class="text-decoration-none text-white">
                        <h5 class="text-capitalize">Total recipes</h5>
                        <h4><% =TotalRecipes() %></h4>
                    </a>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
