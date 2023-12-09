<%@ Page Title="Reset Password" Language="C#" MasterPageFile="~/Layouts/Site.Master" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="RevolutionHotel.ResetPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="login p-3">
        <div class="form-group mb-3">
            <label class="mb-1">New Password</label>
            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Enter Password"></asp:TextBox>
        </div>
        <div class="form-group mb-3">
            <label class="mb-1">Confirm New Password</label>
            <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Confirm Password"></asp:TextBox>
        </div>

        <div class="d-flex justify-content-between align-items-center">
            <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-success" OnClick="btnReset_Click"/>
            <asp:LinkButton ID="lbtnBack" runat="server" CssClass="btn btn-warning" OnClick="lbtnBack_Click">Back</asp:LinkButton>
        </div>
    </div>
</asp:Content>
