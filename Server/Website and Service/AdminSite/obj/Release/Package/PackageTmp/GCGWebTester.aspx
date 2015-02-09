<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GCGWebTester.aspx.cs" Inherits="AppAdminSite.GCGWebTester" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        SessionID<asp:TextBox ID="txtSession" runat="server"></asp:TextBox>
    
    </div>
    <asp:Button ID="Button3" runat="server" onclick="Button3_Click" 
        Text="Convert" />
    <br />
    Key<asp:TextBox ID="txtKey" runat="server"></asp:TextBox>
    </form>
</body>
</html>
