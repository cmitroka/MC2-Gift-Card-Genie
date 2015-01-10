<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddMerchantRequest.aspx.cs" Inherits="AppAdminSite.AddMerchantRequest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            vertical-align: top;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        Our goal is to have as many gift cards as possible in Gift Card Genie, but one 
        problem we run into is that we don&#39;t always have the information we need.&nbsp; 
        This is where we can use your help.&nbsp; If you can provide us with the 
        information you would normally use to check your balance online, we can but an 
        interface to do it automatically.&nbsp; Long story short, if you want to help 
        us, give us as much information as you can about the card and we&#39;ll do the rest.&nbsp; </p>
    <table class="style1">
        <tr>
            <td class="style2">
                Merchant:</td>
            <td>
                <asp:TextBox ID="txtMerchant" runat="server" Width="234px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2">
                URL:</td>
            <td>
                <asp:TextBox ID="txtURL" runat="server" Width="523px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2">
                Card Number:</td>
            <td>
                <asp:TextBox ID="txtCardNum" runat="server" Width="180px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2">
                Card PIN:</td>
            <td>
                <asp:TextBox ID="txtCardPIN" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2" colspan="2">
                For some places, you have to log in first.&nbsp; If that&#39;s the case with your 
                card, put that information here:</td>
        </tr>
        <tr>
            <td class="style2" colspan="2">
                <asp:TextBox ID="txtOtherInfo" runat="server" Height="79px" 
            TextMode="MultiLine" Width="661px"></asp:TextBox>
            </td>
        </tr>
    </table>
    <p>
        <asp:Button ID="cmdSend" runat="server" onclick="cmdSend_Click" Text="Send" 
            Width="117px" />
        <asp:Button ID="cmdCancel" runat="server" Text="Cancel" Width="124px" 
            onclick="cmdCancel_Click" />
</p>
</asp:Content>
