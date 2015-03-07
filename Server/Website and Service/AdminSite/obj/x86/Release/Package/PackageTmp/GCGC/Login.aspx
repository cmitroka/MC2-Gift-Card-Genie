<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="GCGC.Login" %>
<asp:Content ID="titleContent" ContentPlaceHolderID="titleContent" runat="server">
  Login
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 250px;
        }
        .style2
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <center>
    <table class="style1">
        <tr>
            <td style="text-align: center">
                <table class="style2">
                    <tr>
                        <td align="left">
                            Login:</td>
                        <td>
                            <asp:TextBox ID="txtLogin" runat="server" Width="150px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            Password:</td>
                        <td>
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="150px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <asp:Button ID="cmdLogin" runat="server" Text="Logon" 
                    onclick="cmdLogin_Click" />
            </td>
        </tr>
    </table>
</center>
</asp:Content>
