<%@ Page Title="Customer Details" Language="C#" MasterPageFile="~/Layouts/Admin.Master" AutoEventWireup="true" CodeBehind="CustomerDetails.aspx.cs" Inherits="RevolutionHotel.Admin.CustomerDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section>
        <div class="container">
            <div class="d-flex justify-content-between align-items-center mb-3">
                <h2 class="text-capitalize">Revolution Customer Details</h2>
                <div>
                    <asp:Button ID="btnUpdate" runat="server" Text="Update User" CssClass="btn btn-success" OnClick="btnUpdate_Click" />
                    <a href="Customer.aspx" class="btn btn-primary me-0">Back</a>
                </div>
            </div>
            <div style="border: 1px solid #ddd; border-radius: 4px;" class="p-2">
                <div class="row">
                    <div class="col-sm-4 d-flex justify-content-center align-items-center flex-column">
                        <figure class="d-flex justify-content-center align-items-center">
                            <asp:Image ID="ImgProfilePic" CssClass="img-thumbnail" alt="User Icon" runat="server" Style="width: 60%; height: auto; border-radius: 50%;" />
                        </figure>
                        <h5 class="text-uppercase">
                            <asp:Label ID="txtName" runat="server" Text=""></asp:Label>
                        </h5>
                    </div>
                    <div class="col-sm-8">
                        <h4 class="text-capitalize mb-2">Personal information</h4>
                        <ul style="list-style: none;">
                            <li class="d-flex justify-content-between align-items-center pt-0 pb-0">
                                <b>Customer Id</b>
                                <a href="#" class="text-decoration-none">
                                    <asp:Label ID="txtCustomerId" runat="server" Text=""></asp:Label></a>
                            </li>
                            <hr />
                            <li class="d-flex justify-content-between align-items-center pt-0 pb-0">
                                <b>Phone Number</b>
                                <a href="#" class="text-decoration-none">
                                    <asp:Label ID="txtPhone" runat="server" Text=""></asp:Label></a>
                            </li>
                            <hr />
                            <li class="d-flex justify-content-between align-items-center pt-0 pb-0">
                                <b>Email</b>
                                <a href="#" class="text-decoration-none">
                                    <asp:Label ID="txtEmail" runat="server" Text=""></asp:Label></a>
                            </li>
                            <hr />
                            <li class="d-flex justify-content-between align-items-center pt-0 pb-0">
                                <b>Postal Address</b>
                                <a href="#" class="text-decoration-none">
                                    <asp:Label ID="txtPostalAddress" runat="server" Text=""></asp:Label></a>
                            </li>
                            <hr />
                            <li class="d-flex justify-content-between align-items-center pt-0 pb-0">
                                <b>Country</b>
                                <a href="#" class="text-decoration-none">
                                    <asp:Label ID="txtCountry" runat="server" Text=""></asp:Label></a>
                            </li>
                            <hr />
                            <li>
                                <h6 data-bs-toggle="collapse" href="#collapseExample" role="button" aria-expanded="false" aria-controls="collapseExample">Customer Address</h6>
                                <div class="collapse" id="collapseExample">
                                    <div class="p-3" style="border: 1px solid #ddd;">
                                        <asp:Label ID="txtAddress" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </li>
                        </ul>
                        <div>
                            <label class="mb-1">Block user?</label>
                            <asp:DropDownList ID="ddlBlocked" runat="server" CssClass="form-control w-100">
                                <asp:ListItem>No</asp:ListItem>
                                <asp:ListItem>Yes</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
