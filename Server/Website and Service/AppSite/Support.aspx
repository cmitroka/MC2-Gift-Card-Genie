<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Support.aspx.cs" Inherits="AppAdminSite.Support" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            vertical-align: top;
            width: 114px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        We need and want your feedback!&nbsp; If you want us to add a vendor - let us 
        know!&nbsp; We&#39;ll need the info for the card you want added, but then we&#39;ll see 
        what we can do!</p>
    <table class="style1">
        <tr>
            <td class="style2">
        Name:</td>
            <td>
                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2">
                Email:</td>
            <td>
                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2">
                Comment:</td>
            <td>
                <asp:TextBox ID="txtComment" runat="server" Height="79px" 
            TextMode="MultiLine" Width="661px"></asp:TextBox>
            </td>
        </tr>
    </table>
    &nbsp;<p>
        <asp:Button ID="cmdSend" runat="server" onclick="cmdSend_Click" Text="Send" 
            Width="117px" />
        <asp:Button ID="cmdCancel" runat="server" Text="Cancel" Width="124px" 
            onclick="cmdCancel_Click" />
</p>
</asp:Content>
