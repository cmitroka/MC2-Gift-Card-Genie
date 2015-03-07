<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="UUIDPurchaseDetails.aspx.cs" Inherits="AppAdminSite.UUIDPurchaseDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        #Button1
        {
            height: 21px;
            width: 55px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:FormView ID="FormView1" runat="server" DataKeyNames="ID" 
        DataSourceID="AccessDataSource1" DefaultMode="Insert" 
        style="margin-right: 216px">
        <InsertItemTemplate>
            <table width="1">
                <tr>
                    <td width="250">
                        UUID:</td>
                    <td width="150">
                        Date:
                        <input id="Button1" type="button" value="Now" onclick="document.getElementById('MainContent_FormView1_TimeLoggedTextBox').value = '<%# DateTime.Now.ToString() %>'; return false" />
                    </td>
                    <td width="150">
                        Purchase Type</td>
                </tr>
                <tr>
                    <td width="250">
                        <asp:TextBox ID="UUIDTextBox" runat="server" Text='<%# Bind("UUID") %>' 
                            Width="270px" />
                    </td>
                    <td width="150">
                        <asp:TextBox ID="TimeLoggedTextBox" runat="server" 
                            Text='<%# Bind("TimeLogged") %>' Width="180px" />
                    </td>
                    <td width="150">
                        <asp:TextBox ID="PurchaseTypeTextBox" runat="server" 
                            Text='<%# Bind("PurchaseType") %>' Width="100px" />
                    </td>
                </tr>
            </table>
            <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" 
                CommandName="Insert" Text="Insert" />
            &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" 
                CausesValidation="False" CommandName="Cancel" Text="Cancel" />
        </InsertItemTemplate>
    </asp:FormView>
    <br />
    Purchase Details<asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
            CellPadding="3" DataSourceID="AccessDataSource1" AllowSorting="True" 
        DataKeyNames="ID">
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" 
                ShowSelectButton="True" />
            <asp:BoundField DataField="ID" HeaderText="ID" 
                    SortExpression="ID" ReadOnly="True" InsertVisible="False" />
            <asp:BoundField DataField="UUID" HeaderText="UUID" 
                SortExpression="UUID" />
            <asp:BoundField DataField="TimeLogged" HeaderText="TimeLogged" 
                SortExpression="TimeLogged" />
            <asp:BoundField DataField="PurchaseType" HeaderText="PurchaseType" 
                SortExpression="PurchaseType" />
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
    <asp:AccessDataSource ID="AccessDataSource1" runat="server" 
        DataFile="~/App_Data/App.mdb" 
        DeleteCommand="DELETE FROM [tblPurchases] WHERE [ID] = ?" 
        InsertCommand="INSERT INTO [tblPurchases] ([UUID], [TimeLogged], [PurchaseType]) VALUES (?, ?, ?)" 
        SelectCommand="SELECT * FROM [tblPurchases] ORDER BY [ID] DESC" 
        UpdateCommand="UPDATE [tblPurchases] SET [UUID] = ?, [TimeLogged] = ?, [PurchaseType] = ? WHERE [ID] = ?">
        <DeleteParameters>
            <asp:Parameter Name="ID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="UUID" Type="String" />
            <asp:Parameter Name="TimeLogged" Type="DateTime" />
            <asp:Parameter Name="PurchaseType" Type="Int32" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="UUID" Type="String" />
            <asp:Parameter Name="TimeLogged" Type="DateTime" />
            <asp:Parameter Name="PurchaseType" Type="Int32" />
            <asp:Parameter Name="ID" Type="Int32" />
        </UpdateParameters>
    </asp:AccessDataSource>
</asp:Content>
