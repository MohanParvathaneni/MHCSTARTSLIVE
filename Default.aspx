<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MHCStars._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../Resources/CSS/Star.css" rel="stylesheet" />
    <style>
        .box {
            position: relative;
            display: inline-block;
            /*width:958px;*/
            width: 1020px;
        }

            .box .image {
                margin-left: 70px;
            }

            .box .text {
                position: absolute;
                z-index: 999;
                margin: 0 auto;
                left: 0;
                right: 0;
                top: 5%;
                /*text-align:center;*/
                width: 80%;
            }
    </style>
    <div class="container body-content">
        <asp:PlaceHolder ID="phChooseOrganization" runat="server" Visible="false">
        <div class="row">
            <div class="col-md-2">
                Organization:
            </div>
            <div class="col-md-6">
                <asp:DropDownList ID="ddlOrganization" runat="server" DataTextField="" DataValueField="" AppendDataBoundItems="true" AutoPostBack="true">
                    <asp:ListItem Text="Choose One" Value ="0"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvddlOrganization" runat="server"
                ControlToValidate="ddlOrganization" CssClass="ErrorText" Display="Dynamic"
                ErrorMessage="Please select a organization" SetFocusOnError="True"
                InitialValue="Choose One"></asp:RequiredFieldValidator>
            </div>
        </div>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="phEmailError" runat="server">
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="phBody" runat="server">
        <div class="box">
            <div class="image">
                <img src="Resources/Images/Shooting%20Star.png" />
            </div>
            <div class="text">
                <h2>Shooting Star
                </h2>
                <div class="row">
                    <div class="col-md-2">
                        Recipient:
                    </div>
                    <div class="col-md-6">
                        <asp:DropDownList ID="ddlRecipient" runat="server" DataTextField="" DataValueField="" AppendDataBoundItems="true">
                            <asp:ListItem Text="Choose one" Value="Choose one"></asp:ListItem>
                        </asp:DropDownList>

                        <asp:RequiredFieldValidator ID="rfvRecipient" runat="server"
                            ControlToValidate="ddlRecipient" CssClass="ErrorText" Display="Dynamic"
                            ErrorMessage="Please select a recipient" SetFocusOnError="True"
                            InitialValue="Choose One"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                    </div>
                    <div class="col-md-6">
                        <p style="font-size: 25pt; font-weight: bold; font-style: italic; font-family: 'Times New Roman', Times, serif; color: #073152;">
                            You're a Star!
                        </p>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-9">
                        <p style="font-family: 'Times New Roman', Times, serif; font-size: 15pt; font-weight: bold; font-style: italic; color: #073152">
                            I appreciate your dedication to SERVE and ...
                        </p>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-9">
                        <asp:TextBox ID="tbComments" runat="server" TextMode="MultiLine" Rows="10" Columns="47" Width="100%"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        Recipient Manager:
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddlManager" runat="server" AppendDataBoundItems="true">
                            <asp:ListItem Value="Choose one" Text="Choose one"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvddlManager" runat="server"
                            ControlToValidate="ddlManager" CssClass="ErrorText" Display="Dynamic"
                            ErrorMessage="Please select a manager" InitialValue="Choose One"
                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                    </div>
                    <div class="col-md-6">
                        <asp:Label ID="lblName" runat="server" Text="Your Name"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                    </div>
                    <div class="col-md-6">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                    </div>
                </div>
                 <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
                </div>
            </div>
            </div>
        </div>
        </asp:PlaceHolder>
    </div>
</asp:Content>
