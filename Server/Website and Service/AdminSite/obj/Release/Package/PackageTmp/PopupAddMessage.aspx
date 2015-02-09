<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopupAddMessage.aspx.cs" Inherits="AppAdminSite.PopupAddMessage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GCG Add Message</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table class="style1" style="border-style: solid; width: 300px; text-align: center">
        <tr>
            <td>
                <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" 
                    DataKeyNames="ID" DataSourceID="AccessDataSource1" DefaultMode="Insert" 
                    Height="50px" Width="531px">
                    <Fields>
                        <asp:TemplateField HeaderText="UUID" SortExpression="UUID">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("UUID") %>' 
                                    style="margin-left: 0px" Width="500px"></asp:TextBox>
                            </EditItemTemplate>
                            <InsertItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("UUID") %>' 
                                     Width="500px"></asp:TextBox>
                            </InsertItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("UUID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Message" SortExpression="Message">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Message") %>'
                                style="margin-left: 0px" Width="500px"></asp:TextBox>
                            </EditItemTemplate>
                            <InsertItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Message") %>' 
                                    Width="500px"></asp:TextBox>
                            </InsertItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("Message") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowInsertButton="True" />
                    </Fields>
                </asp:DetailsView>
            </td>
        </tr>
    </table>


    </div>
    <asp:AccessDataSource ID="AccessDataSource1" runat="server" 
        DataFile="~/App_Data/App.mdb" 
        InsertCommand="INSERT INTO [tblMessages] ([UUID], [Message],[TimeLogged]) VALUES (?, ?,?)" 
        SelectCommand="SELECT * FROM [tblMessages]" 
        oninserting="AccessDataSource1_Inserting">
        <InsertParameters>
            <asp:Parameter Name="UUID" Type="String" />
            <asp:Parameter Name="Message" Type="String" />
            <asp:Parameter Name="TimeLogged" Type="String" />
        </InsertParameters>
    </asp:AccessDataSource>
    </form>
</body>
</html>
