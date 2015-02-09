<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopupIAMerchantUploader.aspx.cs" Inherits="AppAdminSite.PopupIAMerchantUploader" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Merchant Uploader</title>
    </head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:FileUpload ID="FileUpload1" runat="server" />
        <br />
        <asp:Button ID="TestFileUpload" runat="server" onclick="TestFileUpload_Click" 
            Text="Upload" />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Status will be displayed here..."></asp:Label>
  
        <br />
    
    </div>
    </form>
</body>
</html>
