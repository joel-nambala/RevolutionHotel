<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Layouts/Admin.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="RevolutionHotel.Admin.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section>
        <div class="dashboard-header">
            <div class="row">
                <div class="col-sm-3 bg-success pt-1 pb-1">
                    <a href="Customer.aspx" class="text-decoration-none text-white">
                        <h5 class="text-capitalize">Total customers</h5>
                        <% =TotalCustomers() %>
                    </a>
                </div>
                <div class="col-sm-3 bg-danger pt-1 pb-1">
                    <a href="Orders.aspx" class="text-decoration-none text-white">
                        <p class="text-capitalize">Total orders</p>
                        <asp:Label ID="lblOrders" runat="server" Text="10"></asp:Label>
                    </a>
                </div>
                <div class="col-sm-3 bg-info pt-1 pb-1">
                    <a href="#" class="text-decoration-none text-white">
                        <p class="text-capitalize">Total messages</p>
                        <asp:Label ID="lblMessages" runat="server" Text="10"></asp:Label>
                    </a>
                </div>
                <div class="col-sm-3 bg-warning pt-1 pb-1">
                    <a href="Menu.aspx" class="text-decoration-none text-white">
                        <p class="text-capitalize">Total recipes</p>
                        <asp:Label ID="lblRecipes" runat="server" Text="10"></asp:Label>
                    </a>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
