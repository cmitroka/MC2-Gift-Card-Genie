<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Message.aspx.cs" Inherits="AppAdminSite.Message" %>
<asp:Content ID="titleContent" ContentPlaceHolderID="titleContent" runat="server">
  Request/Responses
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Button ID="Button1" runat="server" 
        onclientclick="window.open('PopupAddMessage.aspx', null, 'toolbar=no,location=no,directories=no,status=no, menubar=no,scrollbars=no,resizable=no,width=400,height=300');" 
        Text="Add New Message" />
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
    AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="ID" 
    DataSourceID="AccessDataSource1" BackColor="White" BorderColor="#999999" 
        BorderStyle="None" BorderWidth="1px" CellPadding="3" PageSize="15">
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
            <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" 
                ReadOnly="True" SortExpression="ID" />
            <asp:TemplateField HeaderText="UUID" SortExpression="UUID">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("UUID") %>'></asp:TextBox>
                </EditItemTemplate>
                    <ItemTemplate>
                        <asp:HyperLink id="hlLink" runat="server" NavigateUrl='<%#"UUIDHistory.aspx?UUID=" + Eval("UUID").ToString() %>' Text='<%# Eval("UUID", "{0}") %>'></asp:HyperLink>
                    </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Message" HeaderText="Message" 
                SortExpression="Message" >
            </asp:BoundField>
            <asp:CheckBoxField DataField="Sent" HeaderText="Sent" SortExpression="Sent" />
            <asp:BoundField DataField="TimeSentToUser" HeaderText="Time Sent To User" 
                SortExpression="TimeSentToUser" />
            <asp:BoundField DataField="TimeLogged" HeaderText="Time Logged" 
                SortExpression="TimeLogged" />
        </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#0000A9" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#000065" />
</asp:GridView>
    <br />
    <asp:AccessDataSource ID="AccessDataSource1" runat="server" 
        DataFile="~/App_Data/App.mdb" 
        
        
        SelectCommand="SELECT * FROM [tblMessages] ORDER BY [ID] DESC" 
    ConflictDetection="CompareAllValues" 
    DeleteCommand="DELETE FROM [tblMessages] WHERE [ID] = ?" 
    InsertCommand="INSERT INTO [tblMessages] ([UUID], [Message],[TimeLogged]) VALUES (?, ?,?)" 
    OldValuesParameterFormatString="original_{0}" 
    
        UpdateCommand="UPDATE [tblMessages] SET [UUID] = ?, [Message] = ?, [Sent] = ?, [TimeSentToUser] = ? WHERE [ID] = ? AND (([UUID] = ?) OR ([UUID] IS NULL AND ? IS NULL)) AND (([Message] = ?) OR ([Message] IS NULL AND ? IS NULL)) AND [Sent] = ? AND (([TimeSentToUser] = ?) OR ([TimeSentToUser] IS NULL AND ? IS NULL))" 
        oninserting="AccessDataSource1_Inserting">
        <DeleteParameters>
            <asp:Parameter Name="original_ID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="UUID" Type="String" />
            <asp:Parameter Name="Message" Type="String" />
            <asp:Parameter Name="TimeLogged" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="UUID" Type="String" />
            <asp:Parameter Name="Message" Type="String" />
            <asp:Parameter Name="Sent" Type="Boolean" />
            <asp:Parameter Name="TimeSentToUser" Type="DateTime" />
            <asp:Parameter Name="original_ID" Type="Int32" />
            <asp:Parameter Name="original_UUID" Type="String" />
            <asp:Parameter Name="original_UUID" Type="String" />
            <asp:Parameter Name="original_Message" Type="String" />
            <asp:Parameter Name="original_Message" Type="String" />
            <asp:Parameter Name="original_Sent" Type="Boolean" />
            <asp:Parameter Name="original_TimeSentToUser" Type="DateTime" />
            <asp:Parameter Name="original_TimeSentToUser" Type="DateTime" />
        </UpdateParameters>
</asp:AccessDataSource>
    <br />
    </asp:Content>
