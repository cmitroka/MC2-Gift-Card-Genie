<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AllUUIDsWVersion.aspx.cs" Inherits="AppAdminSite.AllUUIDsWVersion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    All UUIDs and they version they&#39;re using<asp:GridView ID="GridView1" 
        runat="server" AutoGenerateColumns="False" BackColor="White" 
        BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
        DataSourceID="adsAllUUIDsWVersion" GridLines="Vertical">
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
            <asp:BoundField DataField="UDID" HeaderText="UDID" SortExpression="UDID" />
            <asp:BoundField DataField="Version" HeaderText="Version" 
                SortExpression="Version" />
            <asp:BoundField DataField="MaxOfTimeLogged" HeaderText="MaxOfTimeLogged" 
                SortExpression="MaxOfTimeLogged" />
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
    <asp:AccessDataSource ID="adsAllUUIDsWVersion" runat="server" 
        DataFile="~/App_Data/App.mdb" 
        SelectCommand="SELECT * FROM [qryAllDownloadersAndVersionTheyHave]">
    </asp:AccessDataSource>
</asp:Content>
