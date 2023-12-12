<%@ Page Title="Make an Order" Language="C#" MasterPageFile="~/Layouts/User.Master" AutoEventWireup="true" CodeBehind="Order.aspx.cs" Inherits="RevolutionHotel.User.Order" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="container">
        <div class="row">
            <div class="col-md-6">
                <h2 class="text-dark">Make your Order today</h2>
            </div>
        </div>
        <div class="row mt-3 d-flex align-items-center">
            <div class="col-md-6">
                <p style="font-weight: 600;">Food Image</p>
            </div>
            <div class="col-md-6">
                <asp:Image ID="ImgFood" runat="server" alt="Food Icon" style="width: 10rem; height:10rem; border-radius: 50%;"/>
            </div>
        </div>

        <div class="row mt-3">
            <div class="col-md-6">
                <p style="font-weight: 600;">Food Name</p>
            </div>
            <div class="col-md-6">
                <asp:Label ID="lblFoodName" runat="server" Text="Label"></asp:Label>
            </div>
        </div>

        <div class="row mt-3">
            <div class="col-md-6">
                <p style="font-weight: 600;">Customer Name</p>
            </div>
            <div class="col-md-6">
                <asp:Label ID="lblCustomerName" runat="server" Text="Label"></asp:Label>
            </div>
        </div>

        <div class="row mt-3">
            <div class="col-md-6">
                <p style="font-weight: 600;">Address</p>
            </div>
            <div class="col-md-6">
                <asp:Label ID="lblAddress" runat="server" Text="Label"></asp:Label>
            </div>
        </div>

        <div class="row mt-3">
            <div class="col-md-6">
                <p style="font-weight: 600;">Price</p>
            </div>
            <div class="col-md-6">
                <asp:Label ID="lblPrice" runat="server" Text="Label"></asp:Label>
            </div>
        </div>

        <div class="row mt-3">
            <div class="col-md-6">
                <p style="font-weight: 600;">Quantity</p>
            </div>
            <div class="col-md-6">
                <asp:TextBox ID="txtQuantity" runat="server" placeholder="Quantity requested" required="true" CssClass="form-control"></asp:TextBox>
            </div>
        </div>

        <div class="row mt-4">
            <div class="col-sm-6">
                <asp:Button ID="btnOrder" runat="server" Text="Make Order" CssClass="btn btn-success w-25" OnClick="btnOrder_Click"/>
            </div>
        </div>
    </section>
</asp:Content>
