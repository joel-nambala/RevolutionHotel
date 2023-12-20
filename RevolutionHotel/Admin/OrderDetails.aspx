<%@ Page Title="Order Details" Language="C#" MasterPageFile="~/Layouts/Admin.Master" AutoEventWireup="true" CodeBehind="OrderDetails.aspx.cs" Inherits="RevolutionHotel.Admin.OrderDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="container">
        <div class="mt-3 mb-3">
            <asp:Label ID="lblMsg" runat="server" Visible="false" Text="Label"></asp:Label>
        </div>
        <div class="mb-3">
            <h2 class="text-capitalize">Order
                <asp:Label ID="lblOrderHeader" runat="server" Text="Label"></asp:Label>
                details</h2>
        </div>

        <div class="row mb-2">
            <div class="col-md-6">
                <p style="font-weight: 600;">Customer Name</p>
            </div>
            <div class="col-md-6">
                <asp:Label ID="lblCustomerName" runat="server" Text="Label"></asp:Label>
            </div>
        </div>

        <div class="row mb-2">
            <div class="col-md-6">
                <p style="font-weight: 600;">Food Name</p>
            </div>
            <div class="col-md-6">
                <asp:Label ID="lblFoodName" runat="server" Text="Label"></asp:Label>
            </div>
        </div>

        <div class="row mb-2">
            <div class="col-md-6">
                <p style="font-weight: 600;">Description</p>
            </div>
            <div class="col-md-6">
                <asp:Label ID="lblOrderDescription" runat="server" Text="Label"></asp:Label>
            </div>
        </div>

        <div class="row mb-2">
            <div class="col-md-6">
                <p style="font-weight: 600;">Quantity</p>
            </div>
            <div class="col-md-6">
                <asp:Label ID="lblQuantity" runat="server" Text="Label"></asp:Label>
            </div>
        </div>

        <div class="row mb-2">
            <div class="col-md-6">
                <p style="font-weight: 600;">Total Price</p>
            </div>
            <div class="col-md-6">
                <asp:Label ID="lblTotalPrice" runat="server" Text="Label"></asp:Label>
            </div>
        </div>

        <div class="row mb-2">
            <div class="col-md-6">
                <p style="font-weight: 600;">Payment Status</p>
            </div>
            <div class="col-md-6">
                <asp:Label ID="lblPayment" runat="server" Text="Label"></asp:Label>
            </div>
        </div>

        <div class="row mb-3">
            <div class="col-md-6">
                <p style="font-weight: 600;">Approve</p>
            </div>
            <div class="col-md-6">
                <asp:DropDownList ID="ddlApprove" runat="server" CssClass="form-control w-100">
                    <asp:ListItem>Pending</asp:ListItem>
                    <asp:ListItem>Approved</asp:ListItem>
                    <asp:ListItem>Delivered</asp:ListItem>
                    <asp:ListItem>Cancelled</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>

        <div class="row">
            <div class="col-md-6">
                <asp:Button ID="btnUpdate" runat="server" Text="Update order" CssClass="btn btn-success w-25 text-capitalize" OnClick="btnUpdate_Click" />
                <asp:HyperLink ID="hlBack" runat="server" CssClass="btn btn-primary" NavigateUrl="~/Admin/Orders.aspx" Visible="false">Back</asp:HyperLink>
            </div>
        </div>

    </section>
</asp:Content>
