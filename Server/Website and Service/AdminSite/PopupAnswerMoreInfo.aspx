<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopupAnswerMoreInfo.aspx.cs" Inherits="AppAdminSite.PopupAnswerMoreInfo" %>

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
                            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center;" colspan="2">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 50%; text-align: right;">
                            The answer is:</td>
                        <td style="width: 50%; text-align: left;">
                            <asp:TextBox ID="txtAnswer" runat="server"></asp:TextBox>
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
