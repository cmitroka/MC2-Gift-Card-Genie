<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="IASystemParamsData.aspx.cs" Inherits="AppAdminSite.IASystemParamsData" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        #Button1
        {
            height: 21px;
            width: 55px;
        }
        #Button1
        {
            width: 205px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <p>
    System Parameters Details - <input id="Button1" type="button" value="Add System Parameters Data" onclick="window.open('PopupIASystemParamsData.aspx?IDIn=-1', null, 'toolbar=no,location=no,directories=no,status=no, menubar=no,scrollbars=no,resizable=no,width=300,height=100');" />

    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="ID" DataSourceID="AccessDataSource1">
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" 
                    ReadOnly="True" SortExpression="ID" />
            <asp:BoundField DataField="Key" HeaderText="Key" SortExpression="Key" />
            <asp:TemplateField HeaderText="Value" SortExpression="Value">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Value") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Value") %>' 
                            Width="250px"></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Modify">
                <ItemTemplate>
                    <input id="Button2" type="button" value="Modify" onclick="window.open('PopupIASystemParamsData.aspx?IDIn=<%# Eval("ID") %>', null, 'toolbar=no,location=no,directories=no,status=no, menubar=no,scrollbars=no,resizable=no,width=300,height=100');" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</p>
<p class="style1">
    <strong>For Log Level, 1 makes it so sensitive info won&#39;t log,</strong></p>
<p>
    <asp:AccessDataSource ID="AccessDataSource1" runat="server" 
            DataFile="~/App_Data/App.mdb" 
            DeleteCommand="DELETE FROM [tblSystemParams] WHERE [ID] = ?" 
            InsertCommand="INSERT INTO [tblSystemParams] ([ID], [Key], [Value]) VALUES (?, ?, ?)" 
            SelectCommand="SELECT * FROM [tblSystemParams]" 
            UpdateCommand="UPDATE [tblSystemParams] SET [Key] = ?, [Value] = ? WHERE [ID] = ?">
        <DeleteParameters>
            <asp:Parameter Name="ID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="ID" Type="Int32" />
            <asp:Parameter Name="Key" Type="String" />
            <asp:Parameter Name="Value" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="Key" Type="String" />
            <asp:Parameter Name="Value" Type="String" />
            <asp:Parameter Name="ID" Type="Int32" />
        </UpdateParameters>
    </asp:AccessDataSource>
</p>
</asp:Content>
