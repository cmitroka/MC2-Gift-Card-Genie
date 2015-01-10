<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GCGArchive.aspx.cs" Inherits="AppSite.GCGArchive" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    If your 
a user who is helping to support the project (IE: have your devices registered 
with us for testing/development purposes), you can get versions that are older, 
current, and release candidates.<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:TemplateField HeaderText="Version">
                <HeaderTemplate>
                    Version to Download
                </HeaderTemplate>
                <ItemTemplate>
                    <%#DataBinder.Eval(Container.DataItem, "Version")%></span>
                </ItemTemplate>
                <FooterTemplate>
                </FooterTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </asp:Content>
