<%@ Page Title="Customer List" Language="C#" MasterPageFile="~/Layouts/Admin.Master" AutoEventWireup="true" CodeBehind="Customer.aspx.cs" Inherits="RevolutionHotel.Admin.Customer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section>
        <div class="d-flex justify-content-between align-items-center">
            <h2 class="text-capitalize">Revolution hotel customers</h2>
        </div>

        <div class="mt-3">
            <table class="table table-hover" id="example">
               <thead>
                   <tr>
                       <th>#Ref No.</th>
                       <th>Profile</th>
                       <th>Name</th>
                       <th>Date of Birth</th>
                       <th>Gender</th>
                       <th>Country</th>
                       <th>Blocked</th>
                       <th>Actions</th>
                   </tr>
               </thead>
                <tbody>
                   <% =CustomerList() %>
                </tbody>
            </table>
        </div>
    </section>
</asp:Content>
