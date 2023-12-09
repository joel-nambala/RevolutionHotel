<%@ Page Title="Create Account" Language="C#" MasterPageFile="~/Layouts/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="RevolutionHotel.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5 mb-5 p-3" style="background-color: #ffffff; border-radius: 4px; box-shadow: -4px 20px 36px 14px rgba(0,0,0,0.1);">
        <h2>Create Account</h2>

        <div class="mb-3">
            <asp:Label ID="lblMsg" runat="server" Visible="false" CssClass="alert alert-danger"></asp:Label>
        </div>

        <div class="row mb-3">
            <div class="col-sm-6">
                <label class="mb-1">Username</label>
                <asp:TextBox ID="txtUsername" runat="server" placeholder="Enter Username" CssClass="form-control" required="true"></asp:TextBox>
            </div>
            <div class="col-sm-6">
                <label class="mb-1">Postal Address</label>
                <asp:TextBox ID="txtPostalAddress" runat="server" placeholder="Enter Postal Address" CssClass="form-control" required="true"></asp:TextBox>
            </div>
        </div>

        <div class="row mb-3">
            <div class="col-sm-6">
                <label class="mb-1">Password</label>
                <asp:TextBox ID="txtPassword" runat="server" placeholder="Enter Password" CssClass="form-control" required="true" TextMode="Password"></asp:TextBox>
            </div>
            <div class="col-sm-6">
                <label class="mb-1">Confirm Password</label>
                <asp:TextBox ID="txtConfirmPassword" runat="server" placeholder="Confirm Password" CssClass="form-control" required="true" TextMode="Password"></asp:TextBox>
            </div>
        </div>

        <div class="row mb-3">
            <div class="col-sm-12">
                <label class="mb-1">Address</label>
                <asp:TextBox ID="txtAddress" runat="server" placeholder="Enter Address" CssClass="form-control" required="true" TextMode="MultiLine"></asp:TextBox>
            </div>
        </div>

        <div class="row mb-3">
            <div class="col-sm-6">
                <label class="mb-1">Full Name</label>
                <asp:TextBox ID="txtFullName" runat="server" placeholder="Enter Full Name" CssClass="form-control" required="true"></asp:TextBox>
            </div>
            <div class="col-sm-6">
                <label class="mb-1">Date of Birth</label>
                <asp:TextBox ID="txtBirthDate" runat="server" placeholder="Enter Postal Address" CssClass="form-control datepicker" required="true" TextMode="Date"></asp:TextBox>
            </div>
        </div>

        <div class="row mb-3">
            <div class="col-sm-6">
                <label class="mb-1">Phone Number</label>
                <asp:TextBox ID="txtPhone" runat="server" placeholder="Enter Phone Number" CssClass="form-control" required="true"></asp:TextBox>
            </div>
            <div class="col-sm-6">
                <label class="mb-1">Email</label>
                <asp:TextBox ID="txtEmail" runat="server" placeholder="Enter Email" CssClass="form-control" required="true" TextMode="Email"></asp:TextBox>
            </div>
        </div>

        <div class="row mb-3">
            <div class="col-sm-6">
                <label class="mb-1">Gender</label>
                <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control w-100">
                    <asp:ListItem Value="0">Select Gender</asp:ListItem>
                    <asp:ListItem Value="Male">Male</asp:ListItem>
                    <asp:ListItem Value="Female">Female</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-sm-6">
                <label class="mb-1">Profile Picture</label>
                <asp:FileUpload ID="fuProfilePicture" runat="server" CssClass="form-control" ToolTip=".png, .jpeg and .jpg extensions required" />
            </div>
        </div>

        <div class="row mb-3">
            <div class="col-sm-6">
                <label class="mb-1">Country</label>
                <asp:DropDownList ID="ddlCountry" runat="server" class="form-control select2" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            </div>
            <div class="col-sm-6">
                <asp:Panel ID="pnCounty" runat="server">
                    <label class="mb-1">County</label>
                    <asp:DropDownList ID="ddlCounty" runat="server" class="form-control select2"></asp:DropDownList>
                </asp:Panel>
            </div>
        </div>

        <div class="row mb-3">
            <div class="col-sm-6">
                <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn btn-success" OnClick="btnRegister_Click" />
            </div>
        </div>

    </div>
</asp:Content>
