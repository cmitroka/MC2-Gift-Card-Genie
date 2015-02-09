<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"><html>
<head>
    <title>GCG Mobile Registration</title>
    <meta name="viewport" content="width=device-width; initial-scale=1.0" />
    <link rel="shortcut icon" href="http://designshack.net/favicon.ico">
    <link rel="icon" href="http://designshack.net/favicon.ico">


    <link href="GCGWebCSS/jquery.mobile-1.4.0.min.css" rel="stylesheet" type="text/css" />
    <link href="GCGWebCSS/jquery.mobile.external-png-1.4.0.min.css" rel="stylesheet"
        type="text/css" />
    <link href="GCGWebCSS/jquery.mobile.inline-png-1.4.0.min.css" rel="stylesheet" type="text/css" />
    <link href="GCGWebCSS/jquery.mobile.inline-svg-1.4.0.min.css" rel="stylesheet" type="text/css" />
    <link href="GCGWebCSS/jquery.mobile.structure-1.4.0.min.css" rel="stylesheet" type="text/css" />
    <link href="GCGWebCSS/jquery.mobile.theme-1.4.0.min.css" rel="stylesheet" type="text/css" />
    <script src="GCGWebJS/jquery.js" type="text/javascript"></script>
    <script src="GCGWebJS/jquery.mobile-1.4.0.min.js" type="text/javascript"></script>

    <script src="GCGWebJS/jquery.json-2.4.min.js" type="text/javascript"></script>
    <script src="GCGWebJS/jquery.blockUI.js" type="text/javascript"></script>
    <script src="GCGWeb.js" type="text/javascript"></script>
    <script src="GCGWebJS/GCGWebSuppFunctions.js" type="text/javascript"></script>
    <script src="GCGWebJS/GCGWebExperimental.js" type="text/javascript"></script>
 
<script type="text/javascript">
    $(document).ajaxStart(function () {
        $.blockUI({ message: '<img src="spinner.gif" />' });
    })
    $(document).ajaxStop($.unblockUI);
    $(document).ready(function () {
    });
    $(window).load(function () {
    });
    function RegisterUserIns() {
        var p1 = FixWhitepace(document.getElementById('username').value);
        var p2 = FixWhitepace(document.getElementById('password').value);
        var p2v = FixWhitepace(document.getElementById('passwordver').value);
        var p3 = FixWhitepace(document.getElementById('fullname').value);
        var p4 = FixWhitepace(document.getElementById('email').value);
        if (p1.length < 4) {
            alert('The username has to be at least 3 characters.');
            return;
        }
        if (p1.length > 40) {
            alert('The username cant be more than 40 characters.');
            return;
        }

        if (p2 != p2v) {
            alert('Sorry, your passwords dont match.  Try re-entering them.');
            return;
        }
        if (p2.length < 4) {
            alert('The password has to be at least 3 characters.');
            return;
        }
        if (p2.length > 40) {
            alert('The password cant be more than 40 characters.');
            return;
        }

        $.ajax({
            type: "POST",
            url: "GCGWebWS.asmx/RegisterUserIns",
            dataType: "text",
            data: { pGCGLogin: p1, pGCGPassword: p2, pUsersName: p3, pUsersEmail: p4},
            success:
            function (xml) {
                alert(xml);
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(errorThrown);
            }
        });
    }
</script>
</head>
<body>
        <form id="form1" runat="server">
        <table style="width:98%;margin:auto; border:1px; border-style: groove"">
                        <tr>
                            <td colspan="2">
    <header data-role="header" data-theme="a">
        <h1>Register User</h1>
    </header>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Username:
                                </label>
                            </td>
                            <td >
                                <input type="text" id="username" tabindex="1">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Password:
                                </label>
                            </td>
                            <td>
                                <input type="password"  id="password" tabindex="2">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Verify<br />Password:
                                </label>
                            </td>
                            <td>
                                <input type="password"  id="passwordver">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <label>
                                    <center>These aren't mandatory, but will be important if we need to contact you.  Neglect at your own risk!</center>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Full Name:
                                </label>
                            </td>
                            <td>
                                <input type="text"  id="fullname">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Email:
                                </label>
                            </td>
                            <td>
                                <input type="text"  id="email" >
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <input type="button" onclick="RegisterUserIns();" value="Register User">
                            </td>
                        </tr>
    </table>
        </form>
</body>
</html>