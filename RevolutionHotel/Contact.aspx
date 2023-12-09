<%@ Page Title="Contact Us" Language="C#" MasterPageFile="~/Layouts/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="RevolutionHotel.Contact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section>
        <div class="container bg-white p-3 mt-5 mb-5" style="max-width: 40rem; margin: 0 auto;">
            <h3 class="text-center mb-3">Send us a message</h3>
            <div class="form-group mb-2">
                <label class="mb-1">Full Name</label>
                <asp:TextBox ID="txtFullname" runat="server" CssClass="form-control" placeholder="Enter Full Names"></asp:TextBox>
            </div>
            <div class="form-group mb-2">
                <label class="mb-1">Email</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter Email"></asp:TextBox>
            </div>
            <div class="form-group mb-2">
                <label class="mb-1">Subject</label>
                <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control" placeholder="Enter Subject"></asp:TextBox>
            </div>
            <div class="form-group mb-2">
                <label class="mb-1">Message</label>
                <asp:TextBox ID="txtMessage" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="Type your message here..."></asp:TextBox>
            </div>
            <div class="form-group mt-3">
                <asp:Button ID="btnSend" runat="server" Text="Send" CssClass="btn btn-success w-25" OnClick="btnSend_Click" />
            </div>
        </div>
    </section>
</asp:Content>
