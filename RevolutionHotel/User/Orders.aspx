<%@ Page Title="Orders" Language="C#" MasterPageFile="~/Layouts/User.Master" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="RevolutionHotel.User.Orders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section>
        <div>
            <h3 class="text-capitalize">Recent orders</h3>
            <!--<a href="NewMenu.aspx" class="btn btn-primary text-white"><i class="ti-pencil-alt"></i>&nbsp;Add Menu</a>-->
        </div>
        <div class="container mt-3">
            <table id="example" class="table table-striped table-hover">
                <thead>
                    <tr>
                        <th>Customer Name</th>
                        <th>Food Image</th>
                        <th>Food Name</th>
                        <th>Total Price</th>
                        <th>Quantity</th>
                        <th>Status</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    <%=OrderList()%>
                </tbody>
            </table>
        </div>
    </section>
</asp:Content>
