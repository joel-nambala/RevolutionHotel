<%@ Page Title="Menu" Language="C#" MasterPageFile="~/Layouts/Admin.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="RevolutionHotel.Admin.Menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section>
        <div class="d-flex justify-content-between align-items-center">
            <h3 class="text-capitalize">Revolution hotel recipes</h3>
            <a href="NewMenu.aspx" class="btn btn-primary text-white"><i class="ti-pencil-alt"></i>&nbsp;Add Menu</a>
        </div>
        <div class="container mt-3">
            <table id="example" class="table table-striped table-hover">
                <thead>
                    <tr>
                        <th>Ref No</th>
                        <th>Image</th>
                        <th>Food Name</th>
                        <th>Food Course</th>
                        <th>Sold Out</th>
                        <th>Price</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    <%=RecipeList()%> 
                </tbody>
            </table>
        </div>
    </section>
</asp:Content>
