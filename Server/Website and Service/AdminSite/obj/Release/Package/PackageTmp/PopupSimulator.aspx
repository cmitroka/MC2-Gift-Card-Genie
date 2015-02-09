<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopupSimulator.aspx.cs" Inherits="AppAdminSite.PopupSimulator" %>

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
            <td style="width: 50%; text-align: right;">
                Card Type:</td>
            <td style="width: 50%; text-align: left;">
        <asp:DropDownList ID="ddlCardType" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="ddlCardType_SelectedIndexChanged">
        </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 50%; text-align: right;">
                Card #</td>
            <td style="width: 50%; text-align: left;">
        <asp:TextBox ID="txtCard" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 50%; text-align: right;">
                PIN #</td>
            <td style="width: 50%; text-align: left;">
        <asp:TextBox ID="txtPIN" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 50%; text-align: right;">
                Login:</td>
            <td style="width: 50%; text-align: left;">
        <asp:TextBox ID="txtLogin" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 50%; text-align: right;">
                Password:</td>
            <td style="width: 50%; text-align: left;">
        <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center">
        <asp:Label ID="Label3" runat="server" Text="*** Not all cards require all this info"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center">
                &nbsp;</td>
        </tr>
    </table>
    &nbsp;<table style="width: 100%">
        <tr>
            <td style="text-align: center">
        <asp:Button ID="cmdClear" runat="server" Text="Clear" onclick="cmdClear_Click"/>
        <asp:Button ID="cmdSubmit" runat="server" Text="Submit" 
            onclick="cmdSubmit_Click" />
            </td>
        </tr>
    </table>
            </td>
        </tr>
    </table>


<p>
        <br />
        <asp:RequiredFieldValidator ID="RequiredFieldValidatorCardNumber" runat="server" 
            ControlToValidate="txtCard" ErrorMessage="Card number is required."></asp:RequiredFieldValidator>
        <br />
    
    </div>
    </form>
</body>
</html>
