<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="UUIDLookupsAndPurchases.aspx.cs" Inherits="AppAdminSite.UUIDLookupsAndPurchases" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    Purchasers<asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
            CellPadding="3" DataSourceID="adsPurchasers" AllowSorting="True">
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
            <asp:BoundField DataField="Download" HeaderText="Download" 
                    SortExpression="Download" ReadOnly="True" />
            <asp:TemplateField HeaderText="UUID" SortExpression="UUID">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("UUID") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:HyperLink id="hlLink" runat="server" NavigateUrl='<%#"UUIDHistory.aspx?UUID=" + Eval("UUID").ToString() %>' Text='<%# Eval("UUID", "{0}") %>'></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="MinOfTimeLogged" HeaderText="MinOfTimeLogged" 
                SortExpression="MinOfTimeLogged" />
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
    <br />Successful Lookup Count<asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
            CellPadding="3" DataSourceID="adsLookups" AllowSorting="True">
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
            <asp:BoundField DataField="SuccessfulLookups" HeaderText="SuccessfulLookups" 
                    SortExpression="SuccessfulLookups" />
            <asp:TemplateField HeaderText="UDID" SortExpression="UDID">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("UDID") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:HyperLink id="hlLink0" runat="server" 
                        NavigateUrl='<%#"UUIDHistory.aspx?UUID=" + Eval("UDID").ToString() %>' 
                        Text='<%# Eval("UDID", "{0}") %>'></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:BoundField DataField="Purchased" HeaderText="Purchased" 
                SortExpression="Purchased" />
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
        <asp:AccessDataSource ID="adsPurchasers" runat="server" 
        DataFile="~/App_Data/App.mdb" 
        SelectCommand="SELECT * FROM [qryPurchasedPt2]"></asp:AccessDataSource>
        <asp:AccessDataSource ID="adsLookups" runat="server" 
        DataFile="~/App_Data/App.mdb" 
        SelectCommand="SELECT * FROM [qryUDIDBalanceLookups] ORDER BY [SuccessfulLookups] DESC">
    </asp:AccessDataSource>
        </asp:Content>
