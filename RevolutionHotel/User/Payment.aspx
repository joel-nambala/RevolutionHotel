<%@ Page Title="Payment" Language="C#" MasterPageFile="~/Layouts/User.Master" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="RevolutionHotel.User.Payment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Payment details</h2>
    <div class="container">
        <div class="row mt-2 mb-3">
            <div class="col-sm-6">
                <p style="font-weight: 600">Food Name</p>
            </div>
            <div class="col-sm-6">
                <asp:Label ID="lblFoodName" runat="server" Text="Label"></asp:Label>
            </div>
        </div>

        <div class="row mt-2 mb-3">
            <div class="col-sm-6">
                <p style="font-weight: 600">Customer Name</p>
            </div>
            <div class="col-sm-6">
                <asp:Label ID="lblCustomerName" runat="server" Text="Label"></asp:Label>
            </div>
        </div>


        <div class="row mt-2 mb-3">
            <div class="col-sm-6">
                <p style="font-weight: 600">Total Price</p>
            </div>
            <div class="col-sm-6">
                <asp:Label ID="lblPrice" runat="server" Text="Label"></asp:Label>
            </div>
        </div>

        <%--<div class="row mt-2 mb-3">
            <div class="col-sm-6">
                <asp:Button ID="btnPayment" runat="server" Text="Proceed" CssClass="btn btn-success" OnClick="btnPayment_Click" />
            </div>
            <div class="col-sm-6"></div>
        </div>--%>
        <!-- Button trigger modal -->
        <button type="button" class="btn btn-success" data-bs-toggle="modal" data-bs-target="#exampleModal">
            Proceed
        </button>
    </div>

    <asp:Panel ID="pnModal" runat="server">
        <!-- Modal -->
        <div class="modal fade" id="exampleModal" tabindex="-1" data-bs-backdrop="static" data-bs-keyboard="false" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h1 class="modal-title fs-5 text-capitalize" id="exampleModalLabel">Complete transaction</h1>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <p>Click o make payment to proceed</p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                        <asp:Button ID="btnPayment" runat="server" CssClass="btn btn-success text-capitalize" Text="Make payment" OnClick="btnPayment_Click"/>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
