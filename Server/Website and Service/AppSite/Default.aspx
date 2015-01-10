<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="GCSite._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
    .style1
    {
        width: 200px;
    }
    .style2
    {
        width: 100%;
    }
</style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Welcome to gift card genie!</h2>
    Thanks for stopping by.&nbsp; As you&#39;ve probably noticed, this isn&#39;t 
software written by a company with a team of people, it&#39;s just my hobby project.&nbsp; 
Have a look around and leave us some
<asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Support.aspx">feedback</asp:HyperLink>
&nbsp;if you have comments or suggestions.<br />
<br />
Here&#39;s a little about the app:<br />
<table border="1" class="style2">
    <tr>
        <td style="width: 25px; vertical-align: top;">
            <table border="0" class="style1">
                <tr style="text-align: center">
                    <td>
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Icon.jpg" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <ul class="list">
                            <li class="genre"><span class="label">Category: </span>
                                <a href="http://itunes.apple.com/us/genre/ios-finance/id6015?mt=8">Finance</a></li>
                            <li class="release-date"><span class="label">Released: </span>Jan 16, 2012</li>
                            <li><span class="label">Version: </span>1.0</li>
                            <li><span class="label">Size: </span>0.1 MB</li>
                            <li class="language"><span class="label">Language: </span>English</li>
                            <li><span class="label">Seller: </span>Chris Mitroka</li>
                            <li class="copyright">© 2012 MC2 Tech Services</li>
                            <li class="copyright">
                                <a href="http://itunes.apple.com/WebObjects/MZStore.woa/wa/appRatings">Rated 4+</a></li>
                        </ul>
                        <p>
                            <span class="app-requirements">Requirements: </span>Compatible with iPhone, iPod 
                            touch, and iPad.Requires iOS 3.2 or later</p>
                    </td>
                </tr>
            </table>
        </td>
        <td style="vertical-align: top;">
            <p class="MsoNormal">
                Want to know the balance of your gift card without making a phone call?<o:p></o:p></p>
            <p class="MsoNormal">
                Gift Card Genie (GCG) allows you to check the balance of your gift cards in real 
                time, so you never have to manually keep track.<span style="mso-spacerun: yes">&nbsp;
                </span>The application is simple:<o:p></o:p></p>
            <ol>
                <li>
                    <p class="MsoNormal">
                        Enter you gift card type (this is the mechant)</p>
                </li>
                <li>
                    <p class="MsoNormal">
&nbsp;Enter the card number</p>
                </li>
                <li>
                    <p class="MsoNormal">
                        If the card has a PIN or Access number, enter it as well</p>
                </li>
                <li>
                    <p class="MsoNormal">
                        Get the balance</p>
                </li>
            </ol>
            <p class="MsoNormal">
                <o:p></o:p>
                The best part is GCG can handle gift cards from multiple mechants – no need for 
                dozens of specific apps.<o:p></o:p></p>
            <p class="MsoNormal">
                You can either then save the data so it doesn’t have to be entered again, or 
                just get the balance and exit.<span style="mso-spacerun: yes">&nbsp; </span>If 
                you save the data, it will be titled as the gift cards type, followed by the 
                last 4 digits – making balance retrieval quick and easy.<span 
                    style="mso-spacerun: yes">&nbsp; </span>There’s no limit to the amount of 
                cards you can have saved either.<o:p></o:p></p>
            <p class="MsoNormal">
                We’re adding new mechants every day, and as they become available, GCG will 
                immediately be able to use them.<span style="mso-spacerun: yes">&nbsp; </span>
                Alongside balance, other information (such as expiration date) may also be 
                retrievable if supplied by the merchant.<span style="mso-spacerun: yes">&nbsp;
                </span>Since the content comes from them, it just has to be relayed back to you.<o:p></o:p></p>
            <p class="MsoNormal">
                To find out what mechants are currently supported, please visit our website.<span 
                    style="mso-spacerun: yes">&nbsp; </span>If you would like to see a mechant 
                added, send us a request and we’ll do our best to make it happen.<span 
                    style="mso-spacerun: yes">&nbsp; </span>Unless a mechant specifically asks 
                or prevents us from balance retrieval, new cards can usually be added within a 
                day.<o:p></o:p></p>
        </td>
        <td style="width: 25px; vertical-align: top;">
            <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/App.jpg" 
                Width="212px" />
        </td>
    </tr>
</table>
<br />
<h3>
        &nbsp;</h3>
    </asp:Content>
