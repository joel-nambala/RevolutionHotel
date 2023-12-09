<%@ Page Title="New Recipe" Language="C#" MasterPageFile="~/Layouts/Admin.Master" AutoEventWireup="true" CodeBehind="NewMenu.aspx.cs" Inherits="RevolutionHotel.Admin.NewMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section>
        <div class="d-flex justify-content-between align-items-center">
            <h2>
                <asp:Label ID="lblHeader" runat="server" Text="New Recipe"></asp:Label></h2>
            <asp:HyperLink ID="hlBack" NavigateUrl="~/Admin/Menu.aspx" runat="server" CssClass="btn btn-primary" Visible="false"><i class="ti-angle-left"></i>&nbsp;Back</asp:HyperLink>
        </div>

        <div class="mt-2 mb-2">
            <asp:Label ID="lblMsg" runat="server" Visible="false" CssClass="alert alert-danger"></asp:Label>
        </div>

        <div class="container mt-3">
            <div class="row mb-3">
                <div class="col-sm-6">
                    <label class="mb-1">Name</label>
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Ex. crusted pizza" required="true"></asp:TextBox>
                </div>
                <div class="col-sm-6">
                    <label class="mb-1">Food Course</label>
                    <asp:DropDownList ID="ddlCourse" CssClass="form-control w-100" runat="server">
                        <asp:ListItem Value="0">--Select Item--</asp:ListItem>
                        <asp:ListItem>Breakfast</asp:ListItem>
                        <asp:ListItem>Lunch</asp:ListItem>
                        <asp:ListItem>Dinner</asp:ListItem>
                        <asp:ListItem>Snack</asp:ListItem>
                        <asp:ListItem>Drink</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-sm-6">
                    <label class="mb-1">Price</label>
                    <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" placeholder="Ex. 55.02" required="true" TextMode="Number"></asp:TextBox>
                </div>
                <div class="col-sm-6">
                    <label class="mb-1">State</label>
                    <asp:DropDownList ID="ddlState" CssClass="form-control w-100" runat="server">
                        <asp:ListItem Value="0">--Select Item--</asp:ListItem>
                        <asp:ListItem>Regular</asp:ListItem>
                        <asp:ListItem>Spicy</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-sm-6">
                    <label class="mb-1">Food Image</label>
                    <asp:FileUpload ID="fuFoodImage" CssClass="form-control" runat="server" ToolTip=".png, .jpeg and .jpg extensions only" />
                </div>
                <div class="col-sm-6">
                    <asp:Panel ID="pnSoldout" runat="server">
                        <label class="mb-1">Sold Out</label>
                        <asp:DropDownList ID="ddlSoldOut" CssClass="form-control w-100" runat="server">
                            <asp:ListItem>No</asp:ListItem>
                            <asp:ListItem>Yes</asp:ListItem>
                        </asp:DropDownList>
                    </asp:Panel>
                </div>
            </div>

            <div class="row">
                <div class="col-sm-6">
                    <asp:Button ID="btnAdd" runat="server" Text="Add Recipe" CssClass="btn btn-success w-25" OnClick="btnAdd_Click" />
                </div>
            </div>
        </div>
    </section>
</asp:Content>
