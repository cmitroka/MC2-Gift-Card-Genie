<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopupLogin.aspx.cs" Inherits="AppAdminSite.PopupLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GCG Simulator</title>
</head>
<body>
    <form id="form1" runat="server">
    <center>
       <table style="border-style: solid; width: 300px; text-align: center">
        <tr>
            <td style="text-align: center">
            <center>
                <table style="text-align: center">
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
                    </center>
            </td>
        </tr>
    </table>
</center>
    </form>
</body>
</html>
