<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="IAMerchantsData.aspx.cs" Inherits="AppAdminSite.IAMerchantsData" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        #Button1
        {
            height: 21px;
            width: 55px;
        }
        .style3
        {
            width: 100%;
        }
        #Button1
        {
            width: 153px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width:100%;">
        <tr>
            <td>
    Merchant Details - <input id="Button1" type="button" value="Add Merchant Data" onclick="window.open('PopupIAMerchantDataEditor.aspx?IDIn=-1', null, 'toolbar=no,location=no,directories=no,status=no, menubar=no,scrollbars=no,resizable=no,width=800,height=380');" />
</td>
            <td style="text-align: right">
                Filter -<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>%</asp:ListItem>
                    <asp:ListItem>A</asp:ListItem>
                    <asp:ListItem>B</asp:ListItem>
                    <asp:ListItem>C</asp:ListItem>
                    <asp:ListItem>D</asp:ListItem>
                    <asp:ListItem>E</asp:ListItem>
                    <asp:ListItem>F</asp:ListItem>
                    <asp:ListItem>G</asp:ListItem>
                    <asp:ListItem>H</asp:ListItem>
                    <asp:ListItem>I</asp:ListItem>
                    <asp:ListItem>J</asp:ListItem>
                    <asp:ListItem>K</asp:ListItem>
                    <asp:ListItem>L</asp:ListItem>
                    <asp:ListItem>M</asp:ListItem>
                    <asp:ListItem>N</asp:ListItem>
                    <asp:ListItem>O</asp:ListItem>
                    <asp:ListItem>P</asp:ListItem>
                    <asp:ListItem>Q</asp:ListItem>
                    <asp:ListItem>R</asp:ListItem>
                    <asp:ListItem>S</asp:ListItem>
                    <asp:ListItem>T</asp:ListItem>
                    <asp:ListItem>U</asp:ListItem>
                    <asp:ListItem>V</asp:ListItem>
                    <asp:ListItem>W</asp:ListItem>
                    <asp:ListItem>X</asp:ListItem>
                    <asp:ListItem>Y</asp:ListItem>
                    <asp:ListItem>Z</asp:ListItem>
          </asp:DropDownList>
            </td>
        </tr>
    </table>
<asp:AccessDataSource ID="AccessDataSource1" runat="server" 
        DataFile="~/App_Data/App.mdb" 
        
        SelectCommand="SELECT * FROM [tblMerchants] WHERE ([CleanName] LIKE ? + '%') ORDER BY [CleanName]">
    <SelectParameters>
        <asp:ControlParameter ControlID="DropDownList1" Name="CleanName" 
            PropertyName="SelectedValue" Type="String" />
    </SelectParameters>
    </asp:AccessDataSource>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="CleanName" DataSourceID="AccessDataSource1" Width="100%">
        <Columns>
           <asp:TemplateField>
            <ItemTemplate>           
<center>
    <table class="style3" style="width: 98%; text-align:left" border="1">
        <tr>
            <td style="width: 50%">
                            ID:
            <asp:Label ID="IDLabel" runat="server" Text='<%# Eval("ID") %>' />
        </td>
            <td>
                Clean Name:
            <asp:Label ID="CleanNameLabel" runat="server" Text='<%# Bind("CleanName") %>' />
            
                    <input id="Button2" type="button" value="Modify Merchant Data" onclick="window.open('PopupIAMerchantDataEditor.aspx?IDIn=<%# Eval("ID") %>', null, 'toolbar=no,location=no,directories=no,status=no, menubar=no,scrollbars=no,resizable=no,width=800,height=380');" />
            </td>
        </tr>
        <tr>
            <td style="width: 50%">
                Clean Phone:
            <asp:Label ID="CleanPhoneLabel" runat="server" 
                Text='<%# Bind("CleanPhone") %>' /></td>
            <td>
                EXE:
            <asp:Label ID="EXELabel" runat="server" Text='<%# Bind("EXE") %>' /></td>
        </tr>
        <tr>
            <td style="width: 50%">
                            Support Code:
            <asp:Label ID="SupportCodeLabel" runat="server" 
                Text='<%# Bind("SupportCode") %>' /></td>
            <td>
                            Timeout:
            <asp:Label ID="TimeoutLabel" runat="server" Text='<%# Bind("Timeout") %>' /></td>
        </tr>
        <tr>
            <td style="width: 50%">
                            Test Card Num:
            <asp:Label ID="TestCardNumLabel" runat="server" 
                Text='<%# Bind("TestCardNum") %>' /></td>
            <td>
                Test Card PIN:
            <asp:Label ID="TestCardPINLabel" runat="server" 
                Text='<%# Bind("TestCardPIN") %>' /></td>
        </tr>
        <tr>
            <td style="width: 50%">
                Test Login:
            <asp:Label ID="TestLoginLabel" runat="server" Text='<%# Bind("TestLogin") %>' /></td>
            <td>
                Test Pass:
            <asp:Label ID="TestPassLabel" runat="server" Text='<%# Bind("TestPass") %>' /></td>
        </tr>
        <tr>
            <td style="width: 50%">
                            Card Number Min Length:
            <asp:Label ID="CardNumMinLabel" runat="server" 
                Text='<%# Bind("CardNumMin") %>' /></td>
            <td>
                PIN Min Length:
            <asp:Label ID="PINMinLabel" runat="server" Text='<%# Bind("PINMin") %>' /></td>
        </tr>
        <tr>
            <td style="width: 50%">
                Requires Registration:
            <asp:CheckBox ID="reqRegCheckBox" runat="server" 
                Checked='<%# Bind("reqReg") %>' Enabled="false" /></td>
            <td>
                            Data Pump:
            <asp:CheckBox ID="DataPumpCheckBox" runat="server" 
                Checked='<%# Bind("DataPump") %>' Enabled="false" /></td>
        </tr>
        <tr>
            <td colspan="2">
                URL:
            <asp:Label ID="URLLabel" runat="server" Text='<%# Bind("URL") %>' /></td>
        </tr>
        <tr>
            <td colspan="2">
                General Note:
            <asp:Label ID="GeneralNoteLabel" runat="server" 
                Text='<%# Bind("GeneralNote") %>' /></td>
        </tr>
        </table>
        </center>

            </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EditRowStyle BorderColor="Red" />
    </asp:GridView>
    <br />
</asp:Content>
