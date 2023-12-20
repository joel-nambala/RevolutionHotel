<%@ Page Title="Login" Language="C#" MasterPageFile="~/Layouts/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RevolutionHotel.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="login p-4">
        <div class="login-photo d-flex justify-content-center align-items-center">
            <img src="assets/img/logo.jpg" class="login-img" />
        </div>
        <%--<div class="progress">
            <div class="progress-bar" role="progressbar" style="width: 100%" aria-valuenow="100" aria-valuemin="0" aria-valuemax="100"></div>
        </div>--%>
        <div>
            <p class="text-uppercase text-decoration-underline mt-3 mb-4 text-center" style="font-weight: 700;">Revolution Hotel</p>
        </div>

        <asp:Label ID="lblMsg" runat="server" Visible="false" CssClass="glow mb-3"></asp:Label>

        <div class="login-form mt-1">
            <div class="form-group mb-3">
                <label class="mb-1">Username</label>
                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Username"></asp:TextBox>
            </div>
            <div class="form-group mb-3 form-group-password">
                <label class="mb-1">Password</label>
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control password-input" placeholder="Password" TextMode="Password"></asp:TextBox>
                <button class="show-password">
                    <i class="fa-solid fa-eye"></i>
                </button>
            </div>
        </div>

        <div class="mt-4 mb-2 d-flex justify-content-center align-items-center">
            <asp:Button ID="Button1" runat="server" Text="Login" CssClass="btn btn-danger w-75 text-uppercase" OnClick="btnLogin_Click" />
        </div>

        <div class="d-flex justify-content-center align-items-center mt-3">
            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="lbtnForgot_Click">Forgot Password?</asp:LinkButton>
        </div>

        <div class="d-flex justify-content-center align-items-center mt-4">
            <p>Do not have an account? <a href="Register.aspx">Sign Up</a></p>
        </div>
    </div>
</asp:Content>
