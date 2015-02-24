<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="GCGXUserDetails.aspx.cs" Inherits="AppAdminSite.GCGXUserDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
        <asp:Button ID="Button1" runat="server" 
        Text="Add New Message" />
    <hr />    
        Purchased<br />
        <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
            CellPadding="3" DataSourceID="adsPurchased" 
            GridLines="Vertical">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:BoundField DataField="GCGUsersID" HeaderText="GCGUsersID" SortExpression="GCGUsersID" />
                <asp:BoundField DataField="SumOfPurchaseType" HeaderText="SumOfPurchaseType" SortExpression="SumOfPurchaseType" />
                <asp:BoundField DataField="MinOfDateLogged" HeaderText="MinOfDateLogged" SortExpression="MinOfDateLogged" />
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
    <hr />    
    Downloaded<asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
            CellPadding="3" DataSourceID="adsAboutUser">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:BoundField DataField="GCGLogin" HeaderText="GCGLogin" SortExpression="GCGLogin" />
                <asp:BoundField DataField="DateLogged" HeaderText="DateLogged" SortExpression="DateLogged" />
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
    <hr />    
    Lookup History<asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
            CellPadding="3" DataSourceID="adsLookups">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:BoundField DataField="GCGUsersID" HeaderText="GCGUsersID" SortExpression="GCGUsersID" />
                <asp:BoundField DataField="CardType" HeaderText="CardType" 
                    SortExpression="CardType" />
                <asp:BoundField DataField="CardNumber" HeaderText="CardNumber" 
                    SortExpression="CardNumber" />
                <asp:BoundField DataField="PIN" HeaderText="PIN" SortExpression="PIN" />
                <asp:BoundField DataField="Login" HeaderText="Login" 
                    SortExpression="Login" />
                <asp:BoundField DataField="Password" HeaderText="Password" 
                    SortExpression="Password" />
                <asp:BoundField DataField="ResponseType" HeaderText="ResponseType" 
                    SortExpression="ResponseType" />
                <asp:BoundField DataField="Response" HeaderText="Response" SortExpression="Response" />
                <asp:BoundField DataField="TimeLogged" HeaderText="TimeLogged" SortExpression="TimeLogged" />
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
    <hr />    
    Login History<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
            CellPadding="3" DataSourceID="adsLogins" DataKeyNames="ID">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:BoundField DataField="GCGUsersID" HeaderText="GCGUsersID" SortExpression="GCGUsersID" />
                <asp:BoundField DataField="Channel" HeaderText="Channel" 
                    SortExpression="Channel" />
                <asp:BoundField DataField="DateLogged" HeaderText="DateLogged" SortExpression="DateLogged" />
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
    <hr />    
        Message History<br />
    <asp:AccessDataSource ID="adsAboutUser" runat="server" 
        DataFile="~/App_Data/GCGApp.mdb" 
        
            SelectCommand="SELECT [GCGLogin], [DateLogged] FROM [tblGCGUsers] WHERE ([GCGUsersID] = ?)">
        <SelectParameters>
            <asp:QueryStringParameter Name="GCGUsersID" QueryStringField="GCGUsersID" Type="String" />
        </SelectParameters>
    </asp:AccessDataSource>
    <asp:AccessDataSource ID="adsLookups" runat="server" 
        DataFile="~/App_Data/GCGApp.mdb" 
        SelectCommand="SELECT * FROM [qryReportRqRsResults] WHERE ([GCGUsersID] = ?) ORDER BY [TimeLogged] DESC">
        <SelectParameters>
            <asp:QueryStringParameter Name="GCGUsersID" QueryStringField="GCGUsersID" Type="String" />
        </SelectParameters>
    </asp:AccessDataSource>
    <asp:AccessDataSource ID="adsLogins" runat="server" 
        DataFile="~/App_Data/GCGApp.mdb" 
        SelectCommand="SELECT * FROM [tblUserLog]  WHERE ([GCGUsersID] = ?) ORDER BY [DateLogged] DESC">
        <SelectParameters>
            <asp:QueryStringParameter Name="GCGUsersID" QueryStringField="GCGUsersID" Type="String" />
        </SelectParameters>

    </asp:AccessDataSource>


    <asp:AccessDataSource ID="adsPurchased" runat="server" 
        DataFile="~/App_Data/GCGApp.mdb" 
            SelectCommand="SELECT * FROM [qryPurchased] WHERE ([GCGUsersID] = ?)">
        <SelectParameters>
            <asp:QueryStringParameter Name="GCGUsersID" QueryStringField="GCGUsersID" Type="String" />
        </SelectParameters>
    </asp:AccessDataSource>

</asp:Content>
