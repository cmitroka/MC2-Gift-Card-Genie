/// <reference path="jquery-1.10.2.js" />
/// <reference path="jquery.mobile-1.4.0.min.js" />
/// <reference path="GCGWebSuppFunctions.js" />

function DoNothing() {
}

function SSAndLoadMerchNameAndValInfo(pCardAndValInfoIn) {
    var result1 = pCardAndValInfoIn.split("~_~");
    document.getElementById('txtCardType').value = result1[0];
    $("#txtCardType").prop('disabled', true);
    document.getElementById('hdnCardNumMin').value = result1[1];
    document.getElementById('hdnCardNumMax').value = result1[2];
    document.getElementById('hdnCardPINMin').value = result1[3];
    document.getElementById('hdnCardPINMax').value = result1[4];
    if (result1[3] == "") {
        document.getElementById("tridCardPIN").style.display = 'none';
        document.getElementById('hdnCardPINMin').value = '-1';
        document.getElementById('hdnCardPINMax').value = '999';
    }
    else {
        document.getElementById("tridCardPIN").style.display = '';
    }
    if (result1[5] == "0") {
        document.getElementById("tridLogin").style.display = 'none';
        document.getElementById("tridPassword").style.display = 'none';
    }
    else {
        document.getElementById("tridLogin").style.display = '';
        document.getElementById("tridPassword").style.display = '';
    }
    $.mobile.changePage("#testarea5", { transition: "slideup", changeHash: false });

}

function RegisterUserIns() {
    //alert("DoRegisterUserIns");
    $.ajax({
        type: "POST",
        url: "GCGWebWS.asmx/RegisterUserIns",
        dataType: "text",
        data: { pGCGLogin: '', pGCGPassword: '', pUsersName: '', pUsersEmail: '' },
        success:
            function (xml) {
                alert(xml);
            },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}
function DoRegisterUserSel(pUserName, pUserPass) {
    //alert("DoRegisterUserIns");
    $.ajax({
        type: "POST",
        url: "WebService.asmx/RegisterUserSel",
        dataType: "text",
        data: { UserLogin: 'a', UserPass: 'b' },
        success:
            function (xml) {
                alert(xml);
            },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}

function DoShowAddVendor() {
    $.mobile.changePage("#testarea6", { transition: "slideup", changeHash: false });
}
function DoInitGCGWeb() {
    if (document.getElementById('hdnGCGID').value == "") {
        $.mobile.changePage("#UserLogin", { role: "dialog" });
        return;
    }

    
    $.ajax({
        type: "POST",
        url: "WebService.asmx/InitGCGWeb",
        dataType: "text",
        data: null,
        success:
            function (xml) {
                var temp1 = EncodedHTMLToText(xml);
                var temp2 = RemoveGCGHeader(temp1);
                $('#ulidtest6').append(temp2);
            },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }

    });
    $.unblockUI();
}

function DoLogin() {
    if (document.getElementById('hdnGCGID').value == "") {
        $.mobile.changePage("#UserLogin", { role: "dialog" });
    }
    $.ajax({
        type: "POST",
        url: "GCGWebWS.asmx/GCGLogin",
        dataType: "text",
        data: { pGCGLogin: document.getElementById('username').value, pGCGPassword: document.getElementById('password').value },
        success:
            function (xml) {
                var temp1 = EncodedHTMLToText(xml);
                var temp2 = RemoveGCGHeader(temp1);
                if (temp2 == "") {
                    alert("Invalid username and/or password.");
                }
                else {
                    document.getElementById('hdnGCGID').value = temp2;
                    $.mobile.changePage("#main");
                }
            },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }

    });
    $.unblockUI();
}

function RUCardDataSel(pGCGID) {
    $.ajax({
        type: "POST",
        url: "WebService.asmx/RUCardDataSel",
        dataType: "text",
        data: { GCGID: pGCGID },
        async: false,
        success:
            function (xml) {
                var temp1 = EncodedHTMLToText(xml);
                var temp2 = RemoveGCGHeader(temp1);
                $('#Ul1').append(temp2);
                $.mobile.changePage("#testarea4", { transition: "slideup", changeHash: false });
            },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}
function RUCardDataIns() {
    $.ajax({
        type: "POST",
        url: "WebService.asmx/RUCardDataIns",
        dataType: "text",
        data: { GCGID: pGCGID },
        async: false,
        success:
            function (xml) {
                var temp1 = EncodedHTMLToText(xml);
                var temp2 = RemoveGCGHeader(temp1);
                $('#Ul1').append(temp2);
                $.mobile.changePage("#testarea4", { transition: "slideup", changeHash: false });
            },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}
function SSAddModCard(allCardDataIn) {
    var result1 = allCardDataIn.split("~_~");
    var CardID = result1[0];
    var CardType = result1[1];
    var CardNumber = result1[2];
    var CardPIN = result1[3];
    var CardLogin = result1[4];
    var CardPass = result1[5];
    document.getElementById('hdnCardID').value = CardID;
    document.getElementById('txtCardType').value = CardType;
    document.getElementById('txtCardNumber').value = CardNumber;
    document.getElementById('txtCardPIN').value = CardPIN;
    document.getElementById('txtCardLogin').value = CardLogin;
    document.getElementById('txtCardPassword').value = CardPass;
    $.mobile.changePage("#testarea5", { transition: "slideup", changeHash: false });
}

function DoRUCardDataMod(action) {
    //string GCGID, string CardID, string CardType, string CardNumber, string CardPIN, string CardLogin, string CardPass, string LastKnownBalance, string LastKnownBalanceDate
    var pGCGID= document.getElementById('hdnGCGID').value;
    var pCardID = document.getElementById('hdnCardID').value;
    var pCardType = document.getElementById('txtCardType').value;
    var pCardNumber = document.getElementById('txtCardNumber').value;
    var pCardPIN = document.getElementById('txtCardPIN').value;
    var pCardLogin = document.getElementById('txtCardLogin').value;
    var pCardPass = document.getElementById('txtCardPassword').value;
    var pLastKnownBalance = ""; // document.getElementById('LastKnownBalance').value;
    var pLastKnownBalanceDate = ""; // document.getElementById('LastKnownBalanceDate').value;
    var pCardNumMin = document.getElementById('hdnCardNumMin').value;
    var pCardNumMax = document.getElementById('hdnCardNumMax').value;
    var pCardPINMin = document.getElementById('hdnCardPINMin').value;
    var pCardPINMax = document.getElementById('hdnCardPINMax').value;
    if (pCardNumMin == "") pCardNumMin = -1;
    if (pCardNumMax == "") pCardNumMax = 999;
    if (pCardPINMin == "") pCardPINMin = -1;
    if (pCardPINMax == "") pCardPINMax = 999;
    var alertmsg = "";
    if (pCardNumber.length < pCardNumMin) {
        alertmsg ="Can't save; the card number length has to be at least " + pCardNumMin + " numbers - you've entered " + pCardNumber.length;
    }
    if (pCardNumber.length > pCardNumMax) {
        alertmsg = "Can't save; the card number length should be less than " + pCardNumMax + " numbers - you've entered " + pCardNumber.length;
    }
    if (pCardPIN.length < pCardPINMin) {
        alertmsg = "Can't save; the card PIN length has to be at least " + pCardPINMin + " numbers - you've entered " + pCardPIN.length;
    }
    if (pCardPIN.length > pCardPINMax) {
        alertmsg = "Can't save; the card PIN length should be less than " + pCardNumMin + " numbers - you've entered " + pCardPIN.length;
    }
    if (alertmsg != "") {

        jAlert('error', alertmsg, 'Error Saving');
        return;
    }


    if (action == "delete") {
        pCardNumber = "-1";
    }
    $.ajax({
        type: "POST",
        url: "WebService.asmx/RUCardDataMod",
        dataType: "text",
        data: { GCGID: pGCGID, CardID: pCardID, CardType: pCardType, CardNumber: pCardNumber, CardPIN: pCardPIN, CardLogin: pCardLogin, CardPass: pCardPass, LastKnownBalance: pLastKnownBalance, LastKnownBalanceDate: pLastKnownBalanceDate },
        success:
            function (xml) {
                alert(xml);
            },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}
function DoDiagSayHello() {
    $.ajax({
        type: "POST",
        url: "WebService.asmx/DiagSayHello",
        dataType: "text",
        data: null,
        success:
            function (xml) {
                alert(xml);
            },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }

    });
}
function DoAllAlerts() {
    message = "Alert";
    console.error("error: "+message);
    console.log("log: "+message);
    console.warn("warn: " + message); 
    console.info("info: "+ message); 
    alert(message);
}
function ClearMerchants() {
    $('#dynCards').empty();
    $('#dynCards').listview('refresh');
}
function GetAllMerchants() {
    $.ajax({
        type: "POST",
        url: "WebService.asmx/DownloadAllData",
        dataType: "text",
        data: null,

        success:
            function (xml) {
                $('#dynCards').empty();
                var result = GCGGetSubstring(xml, 'gcg.mc2techservices.com">', '</string>');
                var result1 = result.split("^)(");
                for (index1 = 0; index1 < result1.length; ++index1) {
                    var result2 = (result1[index1]);
                    var result3 = result2.split("~_~");
                    $('#dynCards').append('<li><h1>' + result3[0] + '</h1><p>' + result3[0] + '</p></li>');
                }
                //$('#dynCards').listview('refresh');
            },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}
function DoDiagSayHelloToArg(nameInFromWeb) {
    $.ajax({
        type: "POST",
        url: "WebService.asmx/DiagSayHelloTo",
        dataType: "text",
        data: { nameIn: nameInFromWeb },
        success:
            function (xml) {
                alert(xml);
            },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}
function DoDiagSayHelloToWMsg(nameInFromWeb, msgFromWeb) {
    $.ajax({
        type: "POST",
        url: "WebService.asmx/DiagSayHelloToWMsg",
        dataType: "text",
        data: { nameIn: nameInFromWeb, msg: msgFromWeb },
        success:
            function (xml) {
                alert(xml);
            },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}
function SplitTableToCells(result) {
    var retVal = "";
    var result1 = result.split("^)(");
    for (index1 = 0; index1 < result1.length; ++index1) {
        var result2 = (result1[index1]);
        var result3 = result2.split("~_~");
        retVal = retVal+ result3[0] + '</h1><p>' + result3[0] + '</p></li>';
    }
    return retVal;
}
function RemoveGCGHeader(dataIn) {
    var retVal = "";
    if (retVal=="" )
    {
        retVal=GCGGetSubstring(dataIn, 'gcg.mc2techservices.com">', '</string>');
    }
    if (retVal=="" )
    {
        retVal=GCGGetSubstring(dataIn, 'gcg.mc2techservices.com">', '</boolean>');
    }
    return retVal;
}

function htmlEscape(str) {
    return String(str)
            .replace(/&/g, '&amp;')
            .replace(/"/g, '&quot;')
            .replace(/'/g, '&#39;')
            .replace(/</g, '&lt;')
            .replace(/>/g, '&gt;');
}

function EncodedHTMLToText(str) {
    //var ucstr=str.toUpperCase();
    return String(str)
            .replace(/&lt;/g, '<')
            .replace(/&gt;/g, '>')
            .replace(/&amp;/g, '&')
            .replace(/&quot;/g, '"')
            .replace(/&#39;/g, "'");
}
