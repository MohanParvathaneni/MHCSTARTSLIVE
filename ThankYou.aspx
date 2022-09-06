<%@ Page Title="Thank You Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ThankYou.aspx.cs" Inherits="MHCStars.ThankYou" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <table class="table">
        <tr class="success">
            <td>
                <asp:Label ID="lblThankYou" runat="server" Text="Thank you, for your submission. The person you select and there manager will be recieving and email shortly."></asp:Label><br /><br />
            </td>
        </tr>
    </table>
    <p>
        <asp:Button ID="btnAnother" runat="server" CssClass="btn btn-primary" OnClick="btnAnother_Click" Text="Click here to enter another Shooting Star &raquo;" />
    </p>
</asp:Content>
