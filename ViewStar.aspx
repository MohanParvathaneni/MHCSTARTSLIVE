<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewStar.aspx.cs" Inherits="MHCStars.ViewStar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .box{
            position:relative;
            display:inline-block;
            width:958px;
        }
        .box .image{
            margin-left:70px;
        }
        .box .text{
            position:absolute;
            z-index: 999;
            margin: 0 auto;
            left: 0;
            right:0;
            top:30%;
            /*text-align:center;*/
            width:80%;
        }
    </style>
    <div class="container body-content">
        <asp:PlaceHolder ID="phStarSentIdNotDefined" runat="server" Visible="false">
            The parameter StarSentId is not defined.
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="phBody" runat="server" Visible="true">
        <div class="box">
            <div class="image">
                <img src="Resources/Images/Shooting%20Star.png" />
            </div>
            <div class="text">
                <h2>
                    Shooting Star View
                </h2>
                <div id="contentWrapper">
                    <div id="centeredContent">
                        <table>
                            <tr>
                                <td align="left" colspan="2">
                                    <asp:Label ID="lblRecipient" runat="server" Font-Size="XX-Large" 
                                        ForeColor="#073152"></asp:Label>
                                    <asp:Label ID="lblYourAStar" runat="server" Font-Size="XX-Large" 
                                        ForeColor="#073152" Font-Italic="True" Font-Names="Times New Roman"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblAppreciate" runat="server" Text="I appreciate your dedication to SERVE and ..." Font-Names="Times New Roman" Font-Italic="true" Font-Size="X-Large"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblComments" runat="server" Width="325px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">&nbsp;</td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Label ID="lblName" runat="server" Text="Your Name" Font-Size="X-Large"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">&nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    Recipient Manager:
                                </td>
                                <td>
                                    <asp:Label ID="lblManager" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        </asp:PlaceHolder>
    </div>
</asp:Content>
