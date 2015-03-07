function DoNothing() {
}

function DoMyProfileSel() {
    $.ajax({
        type: "POST",
        url: "GCGWebWS.asmx/MyProfileSel",
        dataType: "text",
        data: { pGCGKey: document.getElementById('hdnGCGID').value },
        success:
            function (xml) {
                var temp1 = EncodedHTMLToText(xml);
                var temp2 = RemoveGCGHeader(temp1);
                var temp3 = temp2.split("^)(");
                document.getElementById('txtLookupHistorySuccessful').value = temp3[1];
                document.getElementById('txtLookupHistoryUnsuccessful').value = temp3[2];
                document.getElementById('txtLookupHistoryRemaining').value = temp3[3];
                $.mobile.changePage("#MyProfile", { transition: "slideup" });
            },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}

function DoPleasePurchaseGCG() {
    $.mobile.changePage("#PleasePurchaseGCG", { transition: "slideup" });
}

function MulitReqDelAndRefresh() {
    var ChangeScreen = DoRUCardDataMod('delete');
    if (ChangeScreen == 0) {
        return;
    }
    MyCardsDataSel();
}

function MulitReqSaveAndRefresh() {
    DoRUCardDataMod('save');
    MyCardsDataSel();
}


function MulitReqLookupAndRefresh() {
    MyCardsDataSel();
}
         
function MulitReqUpdateBalanceRefresh() {
    DoRUCardDataModBal();
    MyCardsDataSelThenChange();
}

function DoNewRequest() {
    var OK=AreValuesInRange("lookup");
    if (OK==false) {
        return;
    }

    $.ajax({
        type: "POST",
        //url: "https://gcg.mc2techservices.com/GCGWebWS.asmx/NewRequest",
        url: "GCGWebWS.asmx/NewRequest",
        dataType: "text",
        data: { pGCGKey: document.getElementById('hdnGCGID').value, pCardType: document.getElementById('txtCardType').value, pCardNumber: document.getElementById('txtCardNumber').value, pPIN: document.getElementById('txtCardPIN').value, pLogin: document.getElementById('txtCardLogin').value, pPassword: document.getElementById('txtCardPassword').value },
        success:
            function (xml) {
                var temp1 = EncodedHTMLToText(xml);
                var temp2 = RemoveGCGHeader(temp1);
                var temp3 = temp2.split("^)(");
                var resultcode = (temp3[0]);
                var resultmsg = (temp3[1]);
                var resultadtmsg = (temp3[2]);
                GCGHandleResponse(resultcode, resultmsg, resultadtmsg);
            },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}
function ShowMD() {
    $("#dialog-message").dialog({
        modal: true,
        buttons: {
            Ok: function () {
                $(this).dialog("close");
            }
        }
    });
};
function NeedMoreInfoMD() {
    $("#dialog-message").dialog({
        modal: true,
        buttons: {
            Ok: function () {
                $("#NeedMoreInfo").dialog("close");
            }
        }
    });
};


function DoContinueRequest() {
    $.ajax({
        type: "POST",
        url: "GCGWebWS.asmx/ContinueRequest",
        dataType: "text",
        //string pGCGKey, string pIDFileName, string pAnswer
        data: { pGCGKey: document.getElementById('hdnGCGID').value, pIDFileName: document.getElementById('hdnContReqFileID').value, pAnswer: document.getElementById('txtMoreInfoAnswer').value },
        success:
            function (xml) {
                var temp1 = EncodedHTMLToText(xml);
                var temp2 = RemoveGCGHeader(temp1);
                var temp3 = temp2.split("^)(");
                var resultcode = (temp3[0]);
                var resultmsg = (temp3[1]);
                var resultadtmsg = (temp3[2]);
                GCGHandleResponse(resultcode, resultmsg, resultadtmsg);
            },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}

function DoInitGCGWeb() {
    var DoReg = 0;
    sesvar = getURLParameter('Session');
    channelvar = getURLParameter('Channel');
    //ssesvar = '15F5AB4C3CF925B';
    var SessionOK = IsSessionValid(sesvar);
    document.getElementById('hdnGCGID').value = sesvar;
    var pGCGID = document.getElementById('hdnGCGID').value;
    if (SessionOK) {
        LogUser(channelvar);
        GetSupportedCards();
        MyCardsDataSel();
        //$("#InitScreen").hide();
        $.mobile.changePage("#MyCards");
        $("#MyCards").show();
    }
    else {
        $.mobile.changePage("GCGWebLogin.htm");
    }
}






   
function TestHide() {
    $("#InitScreen").hide();
}

function popupDialog() {
    $("#popupDialog").show();
}

function DoLoadAddModCardScreen(pMerchData, pValidationInfo) {
    var ValidationInfoArr = pValidationInfo.split("~_~");
    var MerchDataArr = pMerchData.split("~_~");
    //document.getElementById('txtCardType').value = result1[0];

    SSAndLoadMerchNameAndValInfo(pValidationInfo);
    SSAddModCard(pMerchData);

    if (MerchDataArr[0] == "NA") {
        var myhtml = "    <header data-role=\"header\" data-position=\"fixed\">    <h1>Add A Card</h1><a href=\"javascript:MyCardsDataSelThenChange()\">Back</a>    </header>";
        $('#AddModCardSpan').html(myhtml).trigger('create');
    }
    else {
        var myhtml = "    <header data-role=\"header\" data-position=\"fixed\">    <h1>Modify Card</h1><a href=\"javascript:MyCardsDataSelThenChange()\">Back</a>    </header>";
        $('#AddModCardSpan').html(myhtml).trigger('create');
    }
    $.mobile.changePage("#AddModCard", { transition: "slideup" });

}

function LogUser(channel) {
    $.ajax({
        type: "POST",
        url: "GCGWebWS.asmx/GCGLogUser",
        dataType: "text",
        data: { pGCGKey: document.getElementById('hdnGCGID').value, pChannel: channel },
        async: true,
        success:
                function (xml) {
                    return 1;
                },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            return 0;
        }
    });
}

function GetSupportedCards() {
    $.ajax({
        type: "POST",
        url: "GCGWebWS.asmx/GetSupportedCards",
        dataType: "text",
        data: { pGCGKey: document.getElementById('hdnGCGID').value },
        async: true,
        success:
                function (xml) {
                    var temp1 = EncodedHTMLToText(xml);
                    var temp2 = RemoveGCGHeader(temp1);
                    $('#ulofSupportedCards').append(temp2);
                    $.mobile.changePage("#MyCards");
                    return 1;
                },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
            return 0;
        }
    });
}
function MyCardsDataSel() {
    var DoReg = 0;
    $.ajax({
        type: "POST",
        url: "GCGWebWS.asmx/MyCardsDataSel",
        dataType: "text",
        data: { pGCGKey: document.getElementById('hdnGCGID').value },
        async: false,
        success:
            function (xml) {
                var temp1 = EncodedHTMLToText(xml);
                var temp2 = RemoveGCGHeader(temp1);

                $('#ulofMyCards').empty();
                //$('#ulofMyCards').append(temp2);
                $('#ulofMyCards').html(temp2);
                try {
                    $('#ulofMyCards').listview('refresh');
                } catch (e) {
                }

                return 1;
                //$.mobile.changePage("#testarea4", { transition: "slideup", changeHash: false });
            },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
            return 0;
        }
    });
}
function MyCardsDataSelThenChange() {
    MyCardsDataSel();
    $.mobile.changePage("#MyCards");
    $.unblockUI();
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
function DoChangePassword() {
    if (document.getElementById('txtChangePWDNew').value != document.getElementById('txtChangePWDConfirm').value) {
        DoCustomPopup01("Mismatch", "The passwords you entered did not match.");
        return;
    }
    $.ajax({
        type: "POST",
        url: "GCGWebWS.asmx/ChangePassword",
        dataType: "text",
        data: { pGCGKey: document.getElementById('hdnGCGID').value, pOldPassword: document.getElementById('txtChangePWDOld').value, pNewPassword: document.getElementById('txtChangePWDNew').value },
        async: false,
        success:
            function (xml) {
                var temp1 = EncodedHTMLToText(xml);
                var temp2 = RemoveGCGHeader(temp1);
                var temp3 = temp2.split("~_~");
                var resultcode = (temp3[0]);
                var resultmsg = (temp3[1]);
                if (resultcode == "0") { DoCustomPopup01("Error", resultmsg); }
                else if (resultcode == "1") { $.mobile.changePage("#MyProfile"); }
            },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}

function DoChangePasswordScreen() {
    $.mobile.changePage("#ChangePassword", { role: "dialog", transition: "slideup" });

}

function SSAndLoadMerchNameAndValInfo(pCardAndValInfoIn) {
    var result1 = pCardAndValInfoIn.split("~_~");
    document.getElementById('txtCardType').value = result1[0];
    if (result1[2] == 998) {
        $("#txtCardType").prop('disabled', false);
    }
    else {
        $("#txtCardType").prop('disabled', true);
    }  
    document.getElementById('hdnCardNumMin').value = result1[1];
    document.getElementById('hdnCardNumMax').value = result1[2];
    document.getElementById('hdnCardPINMin').value = result1[3];
    document.getElementById('hdnCardPINMax').value = result1[4];
    if (result1[3] == "") {
        document.getElementById("tridCardPIN").style.display = 'none';
        document.getElementById("tridCardPIN").value = '';
        document.getElementById('hdnCardPINMin').value = '-1';
        document.getElementById('hdnCardPINMax').value = '999';
    }
    else {
        document.getElementById("tridCardPIN").style.display = '';
    }
    if (result1[5] == "0") {
        document.getElementById("tridLogin").style.display = 'none';
        document.getElementById("tridLogin").value = '';
        document.getElementById("tridPassword").style.display = 'none';
        document.getElementById("tridPassword").value = '';
    }
    else {
        http: //localhost:53065/GCGWeb.htm#MyCards
        document.getElementById("tridLogin").style.display = '';
        document.getElementById("tridPassword").style.display = '';
    }
}

function SSAddModCard(allCardDataIn) {
    var result1 = allCardDataIn.split("~_~");
    var CardID = result1[0];
    var CardType = result1[1];
    var CardNumber = result1[2];
    var CardPIN = result1[3];
    var CardLogin = result1[4];
    var CardPass = result1[5];
    var LastKnownBal = result1[6];
    var LastKnownBalDate = result1[7];
    var AllowAutolookup = result1[8];
    document.getElementById('hdnCardID').value = CardID;
    document.getElementById('txtCardType').value = CardType;
    document.getElementById('txtCardNumber').value = CardNumber;
    document.getElementById('txtCardPIN').value = CardPIN;
    document.getElementById('txtCardLogin').value = CardLogin;
    document.getElementById('txtCardPassword').value = CardPass;
    document.getElementById('txtCardBalance').value = LastKnownBal;
    //document.getElementById('ta5Lookup').disabled = AllowAutolookup;
    if (AllowAutolookup == "0") {
        var ta5Lookup = document.getElementById("ta5Lookup");
        var ta5Save = document.getElementById("ta5Save");
        var ta5Delete = document.getElementById("ta5Delete");
        var txtCardType = document.getElementById('txtCardType');
        //$(txtCardType).removeAttr('disabled')
        var myhtml = "<nav data-role=\"navbar\">        <ul>          <li><a data-icon=\"save\" id=\"ta5Save\" href=\"javascript:DoRUCardDataModSave()\">Save</a></li>          <li><a data-icon=\"delete\" id=\"ta5Delete\" href=\"javascript:MulitReqDelAndRefresh()\">Delete</a></li>        </ul>      </nav>";
        $('#aaaa').html(myhtml).trigger('create');
        $('#AddModCardHeader').html("").trigger('create');
    }
    else {
        var myhtml = "<nav data-role=\"navbar\">        <ul>          <li><a data-icon=\"save\" id=\"ta5Save\" href=\"javascript:DoRUCardDataModSave()\">Save</a></li>          <li><a data-icon=\"search\" id=\"ta5Lookup\" href=\"javascript:DoNewRequest()\">Lookup</a></li>          <li><a data-icon=\"delete\" id=\"ta5Delete\" href=\"javascript:MulitReqDelAndRefresh()\">Delete</a></li>        </ul>      </nav>";
        $('#aaaa').html(myhtml).trigger('create');
        $('#AddModCardHeader').html("").trigger('create');
        }
}
function DoRUCardDataModSave()
{
    DoRUCardDataMod('save');
}
function DoRUCardDataModDelete()
{
    DoRUCardDataMod('delete');
}

function DoRUCardDataModBal() {
    var pGCGID = document.getElementById('hdnGCGID').value;
    var pCardID = document.getElementById('hdnCardID').value;
    if (pCardID == "NA") {
        return;
    }
    var pLastKnownBalance = document.getElementById('txtCardBalance').value;
    $.ajax({
        type: "POST",
        url: "GCGWebWS.asmx/RUCardDataMod",
        dataType: "text",
        data: { pGCGKey: pGCGID, CardID: pCardID, CardType: "", CardNumber: "", CardPIN: "", CardLogin: "", CardPass: "", LastKnownBalance: pLastKnownBalance, LastKnownBalanceDate: "" },
        success:
            function (xml) {
                //alert(xml);
            },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}

function DoRUCardDataModBalThenRefresh(change) {
    var pGCGID = document.getElementById('hdnGCGID').value;
    var pCardID = document.getElementById('hdnCardID').value;
    if (pCardID == "NA") {
        alertmsg = "Can't save; this card entry is incomplete.";
        alert(alertmsg);
        MyCardsDataSel();
        return;
    }
    var pLastKnownBalance = document.getElementById('txtCardBalance').value;
    $.ajax({
        type: "POST",
        url: "GCGWebWS.asmx/RUCardDataMod",
        dataType: "text",
        data: { pGCGKey: pGCGID, CardID: pCardID, CardType: "", CardNumber: "", CardPIN: "", CardLogin: "", CardPass: "", LastKnownBalance: pLastKnownBalance, LastKnownBalanceDate: "" },
        success:
            function (xml) {
                if (change == "1") {
                    MyCardsDataSelThenChange();
                }
                else {
                    MyCardsDataSel();
                }
            }
                    ,
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}

function AreValuesInRange(action) {
    var pGCGID = document.getElementById('hdnGCGID').value;
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
    if (pCardType.length < 1) {
        alertmsg = "Can't " + action + " - you need to have something for the Card Type.";
    }
    if (pCardNumber.length < pCardNumMin) {
        alertmsg = "Can't " + action + "; the card number length has to be at least " + pCardNumMin + " numbers - you've entered " + pCardNumber.length;
    }
    if (pCardNumber.length > pCardNumMax) {
        alertmsg = "Can't " + action + "; the card number length should be less than " + pCardNumMax + " numbers - you've entered " + pCardNumber.length;
    }
    if (pCardPIN.length < pCardPINMin) {
        alertmsg = "Can't " + action + "; the card PIN length has to be at least " + pCardPINMin + " numbers - you've entered " + pCardPIN.length;
    }
    if (pCardPIN.length > pCardPINMax) {
        alertmsg = "Can't " + action + "; the card PIN length should be less than " + pCardPINMax + " numbers - you've entered " + pCardPIN.length;
    }
    if (alertmsg != "") {

        alert(alertmsg);
        return false;
    }
    else
    {
        return true;
    }
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
    if (action == "delete") {

        if (pCardID == "NA")
        {
            alertmsg = "Can't delete; this card entry is incomplete.";
            alert(alertmsg);
            return;
        }

        var r = confirm("Are you sure you want to delete this data?");
        if (r == true) {
            pCardNumber = "-1";
            pCardNumMin = "";
            pCardNumMax = "";
            pCardPINMin = "";
            pCardPINMax = "";
        } else {
            return 0;
        }
    }
    else
    {
        var OK = AreValuesInRange(action);
        if (OK==false)
        {
            return;
        }
    }

    $.ajax({
        type: "POST",
        url: "GCGWebWS.asmx/RUCardDataMod",
        dataType: "text",
        data: { pGCGKey: pGCGID, CardID: pCardID, CardType: pCardType, CardNumber: pCardNumber, CardPIN: pCardPIN, CardLogin: pCardLogin, CardPass: pCardPass, LastKnownBalance: pLastKnownBalance, LastKnownBalanceDate: pLastKnownBalanceDate },
        async: false,
        success:
            function (xml) {
                var temp1 = EncodedHTMLToText(xml);
                var temp2 = RemoveGCGHeader(temp1);
                if (temp2 == "1") {
                    var mymsg="Complete!";
                }
                var capaction = action.charAt(0).toUpperCase() + action.slice(1);;
                MyCardsDataSelThenChange();
                DoCustomPopup01("Result", capaction + " " + mymsg);
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
                //alert(xml);
            },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }

    });
}
function DoCustomPopup00() {
    //console.log('divclicked');
    var myhtml = "Your balance is <br> $10.00";
    $('#popupSpan2').html(myhtml).trigger('create');

    $('#CustomPopup00').popup();
    $('#CustomPopup00').popup('open', { theme: "b", positionTo: "origin", transition: "flip" });
    //fade, pop, flip, turn, flow, slidefade, slide, slideup, slidedown, none
}
function DoCustomPopup01(title,msg) {
    //console.log('divclicked');
    //$('#CustomPopup01').css('visibility') = 'visible';
    $('#CustomPopup01').show();
    $('#popupSpan01title').html(title).trigger('create');
    $('#popupSpan01msg').html(msg).trigger('create');
    $('#CustomPopup01').popup();
    $('#CustomPopup01').popup('open', { theme: "b", positionTo: "origin", transition: "flip" });
    //fade, pop, flip, turn, flow, slidefade, slide, slideup, slidedown, none
}
function DoCustomPopupYesNo(msg) {
    //console.log('divclicked');
    //$('#CustomPopup01').css('visibility') = 'visible';
    $('#CustomPopupYN').show();
    $('#popupSpanYNmsg').html(msg).trigger('create');
    $('#CustomPopupYN').popup();
    $('#CustomPopupYN').popup('open', { theme: "b", positionTo: "origin", transition: "flip" });
    //fade, pop, flip, turn, flow, slidefade, slide, slideup, slidedown, none
}

function DoJavaScriptPopup(nothing, msg) {
    //console.log('divclicked');
    alert(msg);
}

function DoCardBalSummarySel() {
    $.ajax({
        type: "POST",
        url: "GCGWebWS.asmx/CardBalSummarySel",
        dataType: "text",
        data: { pGCGKey: document.getElementById('hdnGCGID').value },
        success:
            function (xml) {
                var temp1 = EncodedHTMLToText(xml);
                var temp2 = RemoveGCGHeader(temp1);
                try {
                    $('#ulMyCardsBalSum').html(temp2).listview('refresh').trigger('create');
                } catch (e) {
                    var a =1;
                    $('#ulMyCardsBalSum').html(temp2);
                }
                $.mobile.changePage("#MyCardsBalSum", { transition: "slideup" });
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
                //alert(xml);
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
