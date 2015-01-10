<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="TestPage.aspx.cs" Inherits="AppAdminSite.TestPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div>
    
        TEST PAGE<br />
        <asp:Button ID="TestLogUser" runat="server" onclick="TestLogUser_Click" 
            Text="Test Log User" />
        <asp:TextBox ID="TextBox1" runat="server" Width="312px"></asp:TextBox>
        <br />
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
            Text="Restore Cards" />
        <br />
        <asp:Button ID="TestFileUpload" runat="server" onclick="TestFileUpload_Click" 
            Text="Test File Upload" />
        <asp:FileUpload ID="FileUpload1" runat="server" />
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Button ID="URLfromEXE" runat="server" onclick="URLfromEXE_Click" 
            Text="Test Get URL from EXE" />
        <asp:Button ID="Button2" runat="server" onclick="Button2_Click" 
            Text="Save URL from EXE" />
        <br />
        <asp:Button ID="SimpleWebserviceCall" runat="server" onclick="SimpleWebserviceCall_Click" 
            Text="Simple Webservice Call" />
        <asp:Button ID="SaveByteData2WebserviceCall" runat="server" onclick="SaveByteData2WebserviceCall_Click" 
            Text="ImageToOCR Webservice Call" />
        <asp:TextBox ID="TextBox2" runat="server" Width="336px">C:\CardNum.jpg</asp:TextBox>
        <br />
        <asp:FileUpload ID="FileUpload2" runat="server" />
        <asp:Button ID="Send" runat="server" onclick="Send_Click" 
            Text="Send" />
        <br />
        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
        <asp:Button ID="Button3" runat="server" onclick="Button3_Click" 
            Text="Merchant Update" />
        <br />
        <asp:Button ID="Button4" runat="server" onclick="Button4_Click" 
            Text="Merchant Get" />
        <br />
    </div></asp:Content>
