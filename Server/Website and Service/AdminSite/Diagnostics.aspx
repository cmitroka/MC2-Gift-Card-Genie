<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Diagnostics.aspx.cs" Inherits="GCSite.Diagnostics" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    Global Variable Message:<asp:TextBox ID="txtError" runat="server" Width="515px"></asp:TextBox>
&nbsp;<table border="1" class="style1" 
        style="border-width: thin; border-style: solid;">
        <tr>
            <td>
                BalanceRequestTimeout in web.config</td>
            <td>
                <asp:Label ID="lblBalance" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <span style="font-size:11.0pt;line-height:115%;
font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;mso-ascii-theme-font:minor-latin;mso-fareast-font-family:
Calibri;mso-fareast-theme-font:minor-latin;mso-hansi-theme-font:minor-latin;
mso-bidi-font-family:&quot;Times New Roman&quot;;mso-bidi-theme-font:minor-bidi;
mso-ansi-language:EN-US;mso-fareast-language:EN-US;mso-bidi-language:AR-SA">BalanceRequestPath 
                in web.config</span></td>
            <td>
                <asp:Label ID="lblRequestPath" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                PathToMerchantEXEs in web.config</td>
            <td>
                <asp:Label ID="lblPathToMerchantEXEs" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                PathToRqRs in web.config</td>
            <td>
                <asp:Label ID="lblPathToRqRs" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Always Fail</td>
            <td>
                <asp:Label ID="lblAlwaysFail" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <br />
    <br />
</asp:Content>
