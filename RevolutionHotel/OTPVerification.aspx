<%@ Page Title="OTP Verifcation" Language="C#" MasterPageFile="~/Layouts/Site.Master" AutoEventWireup="true" CodeBehind="OTPVerification.aspx.cs" Inherits="RevolutionHotel.OTPVerification" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="login p-4">
        <div>
            <p class="text-uppercase text-decoration-underline mt-3 mb-4 text-center" style="font-weight: 700;">Revolution Hotel</p>
        </div>

        <asp:Label ID="lblMsg" runat="server" Visible="false" CssClass="glow mb-3"></asp:Label>

        <div class="login-form mt-1">
            <div class="form-group mb-3 form-group-password">
                <label class="mb-1">OTP</label>
                <asp:TextBox ID="txtOTP" runat="server" CssClass="form-control password-input" placeholder="Verify OTP" TextMode="Password" required="true"></asp:TextBox>
                <%--<button class="show-password">
                    <i class="fa-solid fa-eye"></i>
                </button>--%>
            </div>
        </div>

        <div class="mt-4 mb-2 d-flex justify-content-center align-items-center">
            <asp:Button ID="btnOTP" runat="server" Text="Verify OTP" CssClass="btn btn-danger w-75 text-uppercase" OnClick="btnOTP_Click" />
        </div>
    </div>
</asp:Content>
