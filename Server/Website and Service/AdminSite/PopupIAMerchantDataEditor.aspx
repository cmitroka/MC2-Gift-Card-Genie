<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopupIAMerchantDataEditor.aspx.cs" Inherits="AppAdminSite.PopupIAMerchantDataEditor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Merchant Editor</title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:DetailsView 
                ID="DetailsView1" runat="server" AutoGenerateRows="False" DataKeyNames="ID" 
                DataSourceID="AccessDataSource1" Width="100%" DefaultMode="Edit">
                <Fields>
                    <asp:TemplateField>
                        <EditItemTemplate>
    <table class="style3" style="width: 100%; text-align:left" border="1">
        <tr>
            <td style="width: 50%">
                            ID:
            <asp:Label ID="IDLabel" runat="server" Text='<%# Eval("ID") %>' />
        </td>
            <td>
                Clean Name:
            <asp:TextBox ID="CleanNameLabel" runat="server" Text='<%# Bind("CleanName") %>' /></td>
        </tr>
        <tr>
            <td style="width: 50%">
                Clean Phone:
            <asp:TextBox ID="CleanPhoneLabel" runat="server" 
                Text='<%# Bind("CleanPhone") %>' /></td>
            <td>
                EXE:
            <asp:TextBox  ID="EXELabel" runat="server" Text='<%# Bind("EXE") %>' /></td>
        </tr>
        <tr>
            <td style="width: 50%">
                            Support Code:

        <asp:DropDownList ID="DropDownList1" runat="server" AppendDataBoundItems="True" 
            DataSourceID="AccessDataSource2" DataTextField="Value" DataValueField="Value" Text='<%# Bind("SupportCode") %>' >
            <asp:ListItem></asp:ListItem>
        </asp:DropDownList>
            </td>
            <td>
                            Timeout:
            <asp:TextBox  ID="TimeoutLabel" runat="server" Text='<%# Bind("Timeout") %>' /></td>
        </tr>
        <tr>
            <td style="width: 50%">
                            Test Card Num:
            <asp:TextBox  ID="TestCardNumLabel" width="190px" runat="server" 
                Text='<%# Bind("TestCardNum") %>' /></td>
            <td>
                Test Card PIN:
            <asp:TextBox  ID="TestCardPINLabel" runat="server" 
                Text='<%# Bind("TestCardPIN") %>' /></td>
        </tr>
        <tr>
            <td style="width: 50%">
                Test Login:
            <asp:TextBox  ID="TestLoginLabel" runat="server" Text='<%# Bind("TestLogin") %>' /></td>
            <td>
                Test Pass:
            <asp:TextBox  ID="TestPassLabel" runat="server" Text='<%# Bind("TestPass") %>' /></td>
        </tr>
        <tr>
            <td style="width: 50%">
                            Card Number Min Length:
            <asp:TextBox ID="CardNumMinLabel" runat="server" 
                Text='<%# Bind("CardNumMin") %>' /></td>
            <td>
                PIN Min Length:
            <asp:TextBox  ID="PINMinLabel" runat="server" Text='<%# Bind("PINMin") %>' /></td>
        </tr>
        <tr>
            <td style="width: 50%">
                Requires Registration:
            <asp:CheckBox ID="reqRegCheckBox" runat="server" 
                Checked='<%# Bind("reqReg") %>' Enabled="true" /></td>
            <td>
                            Data Pump:
            <asp:CheckBox ID="DataPumpCheckBox" runat="server" 
                Checked='<%# Bind("DataPump") %>' Enabled="true" /></td>
        </tr>
        <tr>
            <td colspan="2">
                URL:<br />
            <asp:TextBox ID="URLLabel" Width="98%" runat="server" Text='<%# Bind("URL") %>' /></td>
        </tr>
        <tr>
            <td colspan="2">
                General Note:<br />
            <asp:TextBox ID="GeneralNoteLabel" Width="98%" runat="server" 
                Text='<%# Bind("GeneralNote") %>' /></td>
        </tr>
                        <tr>
                    <td colspan="2">

                    <table class="style1">
                        <tr>
                            <td align="right">
                    <asp:LinkButton ID="LinkButton2" runat="server" onclick="LinkButton1_Click" 
                                    Text="Delete this Record"></asp:LinkButton>

                            </td>
                        </tr>
                    </table>

                    </td>
                </tr>

        </table>
                        </EditItemTemplate>
                        </asp:TemplateField>
                    <asp:CommandField ShowEditButton="True" ShowInsertButton="True" 
                        ShowDeleteButton="True" />
                    <asp:CommandField ShowDeleteButton="True" />
                </Fields>
            </asp:DetailsView>
        <p>
        &nbsp;<asp:AccessDataSource ID="AccessDataSource1" runat="server" 
                DataFile="~/App_Data/App.mdb" 
                DeleteCommand="DELETE FROM [tblMerchants] WHERE [ID] = ?" 
                InsertCommand="INSERT INTO [tblMerchants] ([CleanName], [CleanPhone], [URL], [EXE], [SupportCode], [Timeout], [TestCardNum], [TestCardPIN], [TestLogin], [TestPass], [CardNumMin], [PINMin], [reqReg], [GeneralNote], [DataPump]) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)" 
                SelectCommand="SELECT * FROM [tblMerchants] WHERE ([ID] = ?)" 
                
                
                UpdateCommand="UPDATE [tblMerchants] SET [CleanName] = ?, [CleanPhone] = ?, [URL] = ?, [EXE] = ?, [SupportCode] = ?, [Timeout] = ?, [TestCardNum] = ?, [TestCardPIN] = ?, [TestLogin] = ?, [TestPass] = ?, [CardNumMin] = ?, [PINMin] = ?, [reqReg] = ?, [GeneralNote] = ?, [DataPump] = ? WHERE [ID] = ?" onunload="AccessDataSource1_Unload" 
                onupdated="AccessDataSource1_Updated" 
                oninserted="AccessDataSource1_Inserted">
                <DeleteParameters>
                    <asp:Parameter Name="ID" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="CleanName" Type="String" />
                    <asp:Parameter Name="CleanPhone" Type="String" />
                    <asp:Parameter Name="URL" Type="String" />
                    <asp:Parameter Name="EXE" Type="String" />
                    <asp:Parameter Name="SupportCode" Type="String" />
                    <asp:Parameter Name="Timeout" Type="Int32" />
                    <asp:Parameter Name="TestCardNum" Type="String" />
                    <asp:Parameter Name="TestCardPIN" Type="String" />
                    <asp:Parameter Name="TestLogin" Type="String" />
                    <asp:Parameter Name="TestPass" Type="String" />
                    <asp:Parameter Name="CardNumMin" Type="Int32" />
                    <asp:Parameter Name="PINMin" Type="Int32" />
                    <asp:Parameter Name="reqReg" Type="Boolean" />
                    <asp:Parameter Name="GeneralNote" Type="String" />
                    <asp:Parameter Name="DataPump" Type="Boolean" />
                </InsertParameters>
                <SelectParameters>
                    <asp:QueryStringParameter Name="ID" QueryStringField="IDin" Type="Int32" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="CleanName" Type="String" />
                    <asp:Parameter Name="CleanPhone" Type="String" />
                    <asp:Parameter Name="URL" Type="String" />
                    <asp:Parameter Name="EXE" Type="String" />
                    <asp:Parameter Name="SupportCode" Type="String" />
                    <asp:Parameter Name="Timeout" Type="Int32" />
                    <asp:Parameter Name="TestCardNum" Type="String" />
                    <asp:Parameter Name="TestCardPIN" Type="String" />
                    <asp:Parameter Name="TestLogin" Type="String" />
                    <asp:Parameter Name="TestPass" Type="String" />
                    <asp:Parameter Name="CardNumMin" Type="Int32" />
                    <asp:Parameter Name="PINMin" Type="Int32" />
                    <asp:Parameter Name="reqReg" Type="Boolean" />
                    <asp:Parameter Name="GeneralNote" Type="String" />
                    <asp:Parameter Name="DataPump" Type="Boolean" />
                    <asp:Parameter Name="ID" Type="Int32" />
                </UpdateParameters>
            </asp:AccessDataSource>
  
            <asp:AccessDataSource ID="AccessDataSource2" runat="server" 
                DataFile="~/App_Data/App.mdb" 
                SelectCommand="SELECT [Value] FROM [tblSystemParams] WHERE ([Key] = ?) ORDER BY [Value]">
                <SelectParameters>
                    <asp:Parameter DefaultValue="SCLookup" Name="Key" Type="String" />
                </SelectParameters>
            </asp:AccessDataSource>
  
        <br />
    
    </div>
    </form>
</body>
</html>
