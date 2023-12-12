<%@ Page Title="Recipes" Language="C#" MasterPageFile="~/Layouts/User.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="RevolutionHotel.User.Menu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section>
    <div>
        <h3 class="text-capitalize">Revolution hotel recipes</h3>
        <!--<a href="NewMenu.aspx" class="btn btn-primary text-white"><i class="ti-pencil-alt"></i>&nbsp;Add Menu</a>-->
    </div>
    <div class="container mt-3">
        <table id="example" class="table table-striped table-hover">
            <thead>
                <tr>
                    <th>Ref No</th>
                    <th>Image</th>
                    <th>Food Name</th>
                    <th>Food Course</th>
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
