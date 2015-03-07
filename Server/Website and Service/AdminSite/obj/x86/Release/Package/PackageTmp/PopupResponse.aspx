<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopupResponse.aspx.cs" Inherits="AppAdminSite.PopupResponse" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GCG Simulator</title>
    <style type="text/css">
        .style1
        {
            font-size: xx-large;
        }
        .style3
        {
            font-size: medium;
            width: 100%;
            text-align: left;
            border-style: solid;
            border-width: 1px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table class="style1" style="border-style: solid; width: 300px; text-align: center">
        <tr>
            <td>
                            Your Response
                 <table style="width: 100%; font-size: medium;">
                    <tr>
                        <td style="text-align: center;" colspan="2">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <table class="style3">
                                <tr>
                                    <td>
                                        Response Type:</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblRsType" runat="server" 
                                            style="color: #FF0000; font-weight: 700; font-size: large"></asp:Label>
                                    </td>
                                </tr>
                            </table>

                            <br />
                                                        <table class="style3">
                                <tr>
                                    <td>
                                        Response:</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblRs" runat="server" 
                                            style="color: #FF0000; font-weight: 700; font-size: large"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    </table>
    <table style="width: 100%">
                    <tr>
                        <td style="text-align: center">
                        Thanks for the info!
                        </td>
                    </tr>
                </table>
<table class="style3" style="text-align: center">
                                <tr>
                                    <td>
                                        <asp:HyperLink ID="HyperLink1" runat="server" 
                                            NavigateUrl="~/PopupSimulator.aspx">Back the the simulator</asp:HyperLink>
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
