<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ShowDatagrids.aspx.cs" Inherits="AppAdminSite.ShowDatagrids" %>
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
        BorderStyle="None" BorderWidth="1px" GridLines="Vertical">
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
            <asp:TemplateField HeaderText="UUID" SortExpression="UDID">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("UDID") %>'></asp:TextBox>
                </EditItemTemplate>
                    <ItemTemplate>
                        <asp:HyperLink id="hlLink" runat="server" NavigateUrl='<%#"UUIDHistory.aspx?UUID=" + Eval("UDID").ToString() %>' Text='<%# Eval("UDID", "{0}") %>'></asp:HyperLink>
                    </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="CardType" SortExpression="CardType">
                    <ItemTemplate>
                        <asp:HyperLink id="hlLink" runat="server" NavigateUrl='<%#"ShowDatagrids.aspx?CardType=" + Eval("CardType").ToString() %>' Text='<%# Eval("CardType", "{0}") %>'></asp:HyperLink>
                    </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="CardNumber" HeaderText="Card Number" 
                SortExpression="CardNumber" >
            </asp:BoundField>
            <asp:BoundField DataField="PIN" HeaderText="PIN" 
                SortExpression="PIN" >
            </asp:BoundField>
            <asp:BoundField DataField="ResponseType" HeaderText="Response Type" 
                SortExpression="ResponseType" />
         <asp:TemplateField HeaderText="Response" ItemStyle-HorizontalAlign="center" SortExpression="Response">
            <ItemTemplate>
            <%# ValidateString(Eval("Response").ToString())%>
            </ItemTemplate>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
        </asp:TemplateField>


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
        
        
        SelectCommand="SELECT * FROM [qryReportRqRsResults] ORDER BY [TimeLogged] desc" 
        oninserting="AccessDataSource1_Inserting"></asp:AccessDataSource>
    <br />
    </asp:Content>
