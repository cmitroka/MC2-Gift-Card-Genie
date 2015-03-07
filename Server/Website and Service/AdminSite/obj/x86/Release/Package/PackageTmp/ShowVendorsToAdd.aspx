<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ShowVendorsToAdd.aspx.cs" Inherits="AppAdminSite.ShowVendorsToAdd" %>
<asp:Content ID="titleContent" ContentPlaceHolderID="titleContent" runat="server">
  Request/Responses
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" CellPadding="3" 
        DataSourceID="AccessDataSource1" AllowSorting="True" BackColor="White" 
        BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
        DataKeyNames="ID" onrowcommand="GridView1_RowCommand">
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
             <asp:ButtonField ButtonType="button" CommandName="TAB" Text="Archive" />
            <asp:CheckBoxField DataField="CantSupport" HeaderText="CantSupport" 
                SortExpression="CantSupport" />
            <asp:TemplateField HeaderText="OtherInfo" SortExpression="OtherInfo">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("OtherInfo") %>'></asp:TextBox>
                </EditItemTemplate>
                    <ItemTemplate>
                        <asp:HyperLink id="hlLink" runat="server" NavigateUrl='<%#"UUIDHistory.aspx?UUID=" + Eval("OtherInfo").ToString() %>' Text='View UUID'></asp:HyperLink>
                    </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="OtherInfo" HeaderText="UUID" SortExpression="OtherInfo" />
            <asp:BoundField DataField="Merchant" HeaderText="Merchant" 
                SortExpression="Merchant" HtmlEncode="False" />
            <asp:BoundField DataField="URL" HeaderText="URL" SortExpression="URL" 
                ConvertEmptyStringToNull="False" HtmlEncode="False" />
            <asp:BoundField DataField="CardNum" HeaderText="CardNum" 
                SortExpression="CardNum" />
            <asp:BoundField DataField="CardPIN" HeaderText="CardPIN" 
                SortExpression="CardPIN" ConvertEmptyStringToNull="False" />
            <asp:BoundField DataField="TimeLogged" HeaderText="TimeLogged" 
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
        
        
        
        SelectCommand="SELECT * FROM [tblAddMerchantRequest] ORDER BY [TimeLogged] DESC" 
        DeleteCommand="DELETE FROM [tblAddMerchantRequest] WHERE [ID] = ?" 
        InsertCommand="INSERT INTO [tblAddMerchantRequest] ([ID], [Merchant], [URL], [CardNum], [CardPIN], [OtherInfo], [IP], [TimeLogged], [CantSupport]) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)" 
        
        UpdateCommand="UPDATE [tblAddMerchantRequest] SET [Merchant] = ?, [URL] = ?, [CardNum] = ?, [CardPIN] = ?, [OtherInfo] = ?, [IP] = ?, [TimeLogged] = ?, [CantSupport] = ? WHERE [ID] = ?">
        <DeleteParameters>
            <asp:Parameter Name="ID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="ID" Type="Int32" />
            <asp:Parameter Name="Merchant" Type="String" />
            <asp:Parameter Name="URL" Type="String" />
            <asp:Parameter Name="CardNum" Type="String" />
            <asp:Parameter Name="CardPIN" Type="String" />
            <asp:Parameter Name="OtherInfo" Type="String" />
            <asp:Parameter Name="IP" Type="String" />
            <asp:Parameter Name="TimeLogged" Type="DateTime" />
            <asp:Parameter Name="CantSupport" Type="Boolean" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="Merchant" Type="String" />
            <asp:Parameter Name="URL" Type="String" />
            <asp:Parameter Name="CardNum" Type="String" />
            <asp:Parameter Name="CardPIN" Type="String" />
            <asp:Parameter Name="OtherInfo" Type="String" />
            <asp:Parameter Name="IP" Type="String" />
            <asp:Parameter Name="TimeLogged" Type="DateTime" />
            <asp:Parameter Name="CantSupport" Type="Boolean" />
            <asp:Parameter Name="ID" Type="Int32" />
        </UpdateParameters>
    </asp:AccessDataSource>
    <br />
    </asp:Content>
