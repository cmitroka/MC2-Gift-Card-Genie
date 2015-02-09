<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Metrics.aspx.cs" Inherits="AppAdminSite.Metrics" %>
<asp:Content ID="titleContent" ContentPlaceHolderID="titleContent" runat="server">
  Metrics
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        Logins Today:
        <asp:Label ID="lblLogins" runat="server"></asp:Label>
    </p>
    <p>
        Total
        App Downloads:
        <asp:Label ID="lblTotalAppDownloads" runat="server"></asp:Label>
    </p>
    <p>
        Request Today:
        <asp:Label ID="lblRequests" runat="server"></asp:Label>
    </p>
    <p>
        Successes Today:
        <asp:Label ID="lblSuccesses" runat="server"></asp:Label>
    </p>
    <p>
        Failures Today:
        <asp:Label ID="lblFailures" runat="server"></asp:Label>
    </p>
    </asp:Content>
