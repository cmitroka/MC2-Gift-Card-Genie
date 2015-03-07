<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopupIASystemParamsData.aspx.cs" Inherits="AppAdminSite.PopupIASystemParamsData" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>System Paramater Editor</title>
    </head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:DetailsView 
                ID="DetailsView1" runat="server" AutoGenerateRows="False" DataKeyNames="ID" 
                DataSourceID="AccessDataSource1" Height="50px" Width="250px">
                <Fields>
                    <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" 
                        ReadOnly="True" SortExpression="ID" />
                    <asp:BoundField DataField="Key" HeaderText="Key" SortExpression="Key" />
                    <asp:BoundField DataField="Value" HeaderText="Value" SortExpression="Value" />
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" 
                        ShowInsertButton="True" />
                </Fields>
            </asp:DetailsView>
            <asp:AccessDataSource ID="AccessDataSource1" runat="server" 
                DataFile="~/App_Data/App.mdb" 
                DeleteCommand="DELETE FROM [tblSystemParams] WHERE [ID] = ?" 
                InsertCommand="INSERT INTO [tblSystemParams] ([Key], [Value]) VALUES (?, ?)" 
                SelectCommand="SELECT * FROM [tblSystemParams] WHERE ([ID] = ?)" 
                
                
                
                
                UpdateCommand="UPDATE [tblSystemParams] SET [Key] = ?, [Value] = ? WHERE [ID] = ?" onunload="AccessDataSource1_Unload" 
                onupdated="AccessDataSource1_Updated" 
                oninserted="AccessDataSource1_Inserted" 
                ondeleted="AccessDataSource1_Deleted">
                <DeleteParameters>
                    <asp:Parameter Name="ID" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="Key" Type="String" />
                    <asp:Parameter Name="Value" Type="String" />
                </InsertParameters>
                <SelectParameters>
                    <asp:QueryStringParameter Name="ID" QueryStringField="IDIn" Type="Int32" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Key" Type="String" />
                    <asp:Parameter Name="Value" Type="String" />
                    <asp:Parameter Name="ID" Type="Int32" />
                </UpdateParameters>
            </asp:AccessDataSource>
  
        <br />
    
    </div>
    </form>
</body>
</html>
