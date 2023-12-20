<%@ Page Title="Order Details" Language="C#" MasterPageFile="~/Layouts/User.Master" AutoEventWireup="true" CodeBehind="OrderDetails.aspx.cs" Inherits="RevolutionHotel.User.OrderDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="container">
        <div class="mb-3">
            <h2 class="text-capitalize">Order
            <asp:Label ID="lblOrderHeader" runat="server" Text="Label"></asp:Label>
                details</h2>
        </div>

        <div class="row mt-3 d-flex align-items-center">
            <div class="col-md-6">
                <p style="font-weight: 600;">Food Image</p>
            </div>
            <div class="col-md-6">
                <asp:Image ID="ImgFood" runat="server" alt="Food Icon" Style="width: 10rem; height: 10rem; border-radius: 50%;" />
            </div>
        </div>

        <div class="row mb-2 mt-3">
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
                <p style="font-weight: 600;">Status</p>
            </div>
            <div class="col-md-6">
                <asp:Label ID="lblStatus" runat="server" Text="Label"></asp:Label>
            </div>
        </div>

        <div class="row mt-4">
            <div class="col-md-6">
                <asp:Button ID="btnCancel" runat="server" Text="Cancel order" CssClass="btn btn-danger w-25 text-capitalize" OnClick="btnCancel_Click" />
            </div>
            <div class="col-md-6">
                <asp:Button ID="btnPayment" runat="server" Text="Make payment" CssClass="btn btn-info w-25 text-capitalize" OnClick="btnPayment_Click" />
            </div>
        </div>

    </section>
</asp:Content>
