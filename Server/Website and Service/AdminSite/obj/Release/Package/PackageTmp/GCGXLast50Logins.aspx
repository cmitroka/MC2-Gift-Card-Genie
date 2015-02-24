<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="GCGXLast50Logins.aspx.cs" Inherits="AppAdminSite.GCGXLast50Logins" %>
<asp:Content ID="titleContent" ContentPlaceHolderID="titleContent" runat="server">
  Request/Responses
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" CellPadding="3" DataSourceID="AccessDataSource1" 
        onselectedindexchanged="GridView1_SelectedIndexChanged" PageSize="100" 
        AllowSorting="True" BackColor="White" BorderColor="#999999" 
        BorderStyle="None" BorderWidth="1px" GridLines="Vertical" DataKeyNames="ID">
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
            <asp:TemplateField HeaderText="GCGUsersID" SortExpression="GCGUsersID">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("GCGUsersID") %>'></asp:TextBox>
                </EditItemTemplate>
                    <ItemTemplate>
                        <asp:HyperLink id="hlLink" runat="server" NavigateUrl='<%#"GCGXUserDetails.aspx?GCGUsersID=" + Eval("GCGUsersID").ToString() %>' Text='<%# Eval("GCGUsersID", "{0}") %>'></asp:HyperLink>
                    </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Channel" HeaderText="Channel" 
                SortExpression="Channel" />
            <asp:BoundField DataField="DateLogged" 
                                 HeaderText="DateLogged" SortExpression="DateLogged"  />


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
        DataFile="~/App_Data/GCGApp.mdb"
        
        
        SelectCommand="SELECT TOP 50 * FROM [tblUserLog] ORDER BY [DateLogged] DESC" 
        oninserting="AccessDataSource1_Inserting"></asp:AccessDataSource>
    <br />
    </asp:Content>
