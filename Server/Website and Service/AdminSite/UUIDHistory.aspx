<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="UUIDHistory.aspx.cs" Inherits="AppAdminSite.UUIDHistory" %>
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
                <asp:BoundField DataField="Download" HeaderText="Download" 
                    SortExpression="Download" ReadOnly="True" />
                <asp:BoundField DataField="UUID" HeaderText="UUID" 
                    SortExpression="UUID" />
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
    <hr />    
    Downloaded<asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
            CellPadding="3" DataSourceID="adsDownloaded">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:BoundField DataField="UDID" HeaderText="UDID" SortExpression="UDID" />
                <asp:BoundField DataField="Version" HeaderText="Version" 
                    SortExpression="Version" />
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
    <hr />    
    Lookup History<asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
            CellPadding="3" DataSourceID="adsLookups">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:BoundField DataField="UDID" HeaderText="UDID" SortExpression="UDID" />
                <asp:BoundField DataField="CardType" HeaderText="CardType" 
                    SortExpression="CardType" />
                <asp:BoundField DataField="CardNumber" HeaderText="CardNumber" 
                    SortExpression="CardNumber" />
                <asp:BoundField DataField="PIN" HeaderText="PIN" SortExpression="PIN" />
                <asp:BoundField DataField="ResponseType" HeaderText="ResponseType" 
                    SortExpression="ResponseType" />
                <asp:BoundField DataField="Response" HeaderText="Response" 
                    SortExpression="Response" />
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
    <hr />    
    Login History<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
            CellPadding="3" DataSourceID="adsLogins" DataKeyNames="ID">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" 
                    ReadOnly="True" SortExpression="ID" />
                <asp:BoundField DataField="UDID" HeaderText="UDID" SortExpression="UDID" />
                <asp:BoundField DataField="Version" HeaderText="Version" 
                    SortExpression="Version" />
                <asp:BoundField DataField="IP" HeaderText="IP" SortExpression="IP" />
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
    <hr />    
        Message History<asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
            CellPadding="3" DataSourceID="adsMessages" DataKeyNames="ID">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                <asp:BoundField DataField="UUID" HeaderText="UUID" SortExpression="UUID" />
                <asp:BoundField DataField="Message" HeaderText="Message" 
                    SortExpression="Message" />
                <asp:CheckBoxField DataField="Sent" HeaderText="Sent" SortExpression="Sent" />
                <asp:BoundField DataField="TimeSentToUser" HeaderText="TimeSentToUser" 
                    SortExpression="TimeSentToUser" />
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
    <asp:AccessDataSource ID="adsDownloaded" runat="server" 
        DataFile="~/App_Data/App.mdb" 
        
            SelectCommand="SELECT * FROM [qryUUIDDownloadHistory] WHERE ([UDID] = ?) ORDER BY [MinOfTimeLogged] DESC">
        <SelectParameters>
            <asp:QueryStringParameter Name="UDID" QueryStringField="UUID" Type="String" />
        </SelectParameters>
    </asp:AccessDataSource>
    <asp:AccessDataSource ID="adsLookups" runat="server" 
        DataFile="~/App_Data/App.mdb" 
        SelectCommand="SELECT * FROM [qryReportRqRsResults] WHERE ([UDID] = ?) ORDER BY [TimeLogged] DESC">
        <SelectParameters>
            <asp:QueryStringParameter Name="UDID" QueryStringField="UUID" Type="String" />
        </SelectParameters>
    </asp:AccessDataSource>
    <asp:AccessDataSource ID="adsLogins" runat="server" 
        DataFile="~/App_Data/App.mdb" 
        SelectCommand="SELECT * FROM [tblUserLog] WHERE ([UDID] = ?) ORDER BY [TimeLogged] DESC">
        <SelectParameters>
            <asp:QueryStringParameter Name="UDID" QueryStringField="UUID" Type="String" />
        </SelectParameters>
    </asp:AccessDataSource>
    <asp:AccessDataSource ID="adsMessages" runat="server" 
        DataFile="~/App_Data/App.mdb" 
        
            SelectCommand="SELECT * FROM [tblMessages] WHERE ([UUID] = ?) ORDER BY [TimeLogged] DESC" 
            DeleteCommand="DELETE FROM [tblMessages] WHERE [ID] = ?" 
            InsertCommand="INSERT INTO [tblMessages] ([ID], [UUID], [Message], [Sent], [TimeSentToUser], [TimeLogged]) VALUES (?, ?, ?, ?, ?, ?)" 
            UpdateCommand="UPDATE [tblMessages] SET [UUID] = ?, [Message] = ?, [Sent] = ?, [TimeSentToUser] = ?, [TimeLogged] = ? WHERE [ID] = ?">
        <DeleteParameters>
            <asp:Parameter Name="ID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="ID" Type="Int32" />
            <asp:Parameter Name="UUID" Type="String" />
            <asp:Parameter Name="Message" Type="String" />
            <asp:Parameter Name="Sent" Type="Boolean" />
            <asp:Parameter Name="TimeSentToUser" Type="DateTime" />
            <asp:Parameter Name="TimeLogged" Type="DateTime" />
        </InsertParameters>
        <SelectParameters>
            <asp:QueryStringParameter Name="UUID" QueryStringField="UUID" Type="String" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="UUID" Type="String" />
            <asp:Parameter Name="Message" Type="String" />
            <asp:Parameter Name="Sent" Type="Boolean" />
            <asp:Parameter Name="TimeSentToUser" Type="DateTime" />
            <asp:Parameter Name="TimeLogged" Type="DateTime" />
            <asp:Parameter Name="ID" Type="Int32" />
        </UpdateParameters>
    </asp:AccessDataSource>
    <asp:AccessDataSource ID="adsPurchased" runat="server" 
        DataFile="~/App_Data/App.mdb" 
        
            
            SelectCommand="SELECT * FROM [qryPurchasedPt2] WHERE ([UUID] = ?)">
        <SelectParameters>
            <asp:QueryStringParameter Name="UUID" QueryStringField="UUID" Type="String" />
        </SelectParameters>
    </asp:AccessDataSource>

</asp:Content>
