<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopupAnswerCAPTCHA.aspx.cs" Inherits="AppAdminSite.PopupAnswerCAPTCHA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GCG Simulator</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="style1" style="border-style: solid; width: 300px; text-align: center">
        <tr>
            <td>
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: center;" colspan="2">
                            Please type in the characters you see below.</td>
                    </tr>
                    <tr>
                        <td style="text-align: center;" colspan="2">
                            <asp:Image ID="Image1" runat="server" />
                            <br />
                            Image won&#39;t show up if you didn&#39;t make a Virtual Directory in IIS!</td>
                    </tr>
                    <tr>
                        <td style="width: 50%; text-align: right;">
                            The characters are:</td>
                        <td style="width: 50%; text-align: left;">
                            <asp:TextBox ID="txtCAPTCHA" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
    &nbsp;<table style="width: 100%">
                    <tr>
                        <td style="text-align: center">
                            <asp:Button ID="cmdSubmit" runat="server" Text="Submit" 
            onclick="cmdSubmit_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    </div>
    </form>
</body>
</html>
