function DoNothing() {
}

function DoMyProfileSel(anddiplayit) {
    $.ajax({
        type: "POST",
        url: "GCGWebWS.asmx/MyProfileSel",
        dataType: "text",
        data: { pGCGKey: document.getElementById('hdnGCGID').value },
        async: false,
        success:
            function (xml) {
                var temp1 = EncodedHTMLToText(xml);
                var temp2 = RemoveGCGHeader(temp1);
                var temp3 = temp2.split("^)(");
                document.getElementById('txtLookupHistorySuccessful').value = temp3[1];
                document.getElementById('txtLookupHistoryUnsuccessful').value = temp3[2];
                document.getElementById('txtLookupHistoryRemaining').value = temp3[3];
                if (anddiplayit==1) {
                    $.mobile.changePage("#MyProfile", { transition: "slideup" });
                }
            },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            DoCustomPopup02(errorThrown);
        }
    });
}

function DoPleasePurchaseGCG() {
    $.mobile.changePage("#PleasePurchaseGCG", { transition: "slideup" });
}

function DoWebPurchase() {
    $.mobile.changePage("#WebPurchase", { transition: "slideup" });
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

function copyToClipboard() {
    text = document.getElementById('txtCardNumber').value;
    var resp=window.prompt("Copy the card number; your being directed to the merchants site to get the balance.", text);
    if (resp != null)
    {
        DoNewManualRequest();
    }
}
function DoNewManualRequest() {
    var OK = AreValuesInRange("lookup");
    if (OK == true)
    {
        var status = AreLookupsRemaining();
        if (status == true) {
            status = DoNewRequest();
        }
    }
}
function AreLookupsRemaining()
{
    var retVal = true;
    DoMyProfileSel(0);
    if (document.getElementById('txtLookupHistoryRemaining').value < 1) {
        DoPleasePurchaseGCG();
        retVal = false;
    }
return retVal
}
function DoNewRequest() {
    var OK = AreValuesInRange("lookup");
    if (OK==false) {
        return;
    }
    $.ajax({
        type: "POST",
        //url: "https://gcg.mc2techservices.com/GCGWebWS.asmx/NewRequest",
        url: "GCGWebWS.asmx/NewRequest",
        dataType: "text",
        data: { pGCGKey: document.getElementById('hdnGCGID').value, pCardType: document.getElementById('txtCardType').value, pCardNumber: document.getElementById('txtCardNumber').value, pPIN: document.getElementById('txtCardPIN').value},
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
            DoCustomPopup02(errorThrown);
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
            DoCustomPopup02(errorThrown);
        }
    });
}

function DoInitGCGWeb() {
    var DoReg = 0;
    sesvar = getURLParameter('Session');
    channelvar = getURLParameter('Channel');
    //sesvar = '977ABD97C2C236A';
    var SessionOK = IsSessionValid(sesvar);
    document.getElementById('hdnGCGID').value = sesvar;
    var pGCGID = document.getElementById('hdnGCGID').value;
    if (SessionOK) {
        LogUser(channelvar);
        GetSupportedCards();
        MyCardsDataSel();
        //$("#InitScreen").hide();
        //This now happens when setting up and loading 'My Cards'
        //$.mobile.changePage("#MyCards");
        //$("#MyCards").show();
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

function DoLoadAddModCardScreen(pMyCardID, pCardSpecificsID) {
    var pAllInfo=pMyCardID + "-" + pCardSpecificsID
    SSAddModCard(pAllInfo);

    if (pMyCardID == "New Record") {
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
            DoCustomPopup02(errorThrown);
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
            DoCustomPopup02(errorThrown);
            return 0;
        }
    });
}
function MyCardsDataSelThenChange() {
    MyCardsDataSel();
    $.mobile.changePage("#MyCards");
    $.unblockUI();
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
            DoCustomPopup02(errorThrown);
        }
    });
}

function DoChangePasswordScreen() {
    $.mobile.changePage("#ChangePassword", { role: "dialog", transition: "slideup" });

}

function SSAddModCard(pAllInfoIn) {
    var AllInfoInArr = pAllInfoIn.split("-");
    var pMyCardID = AllInfoInArr[0];
    var pCardSpecificsIDIn = AllInfoInArr[1];

    try {
        var CardSpecifics = document.getElementById(pCardSpecificsIDIn).value
        var CardSpecificsArr = CardSpecifics.split("~_~");
    } catch (e) {
        var CardSpecifics = document.getElementById(pMyCardID).value
        var CardSpecificsArr = CardSpecifics.split("~_~");
        var CardSpecifics = CardSpecificsArr[1] + "~_~NOURL~_~1~_~999~_~-1~_~999~_~1";
        var CardSpecificsArr = CardSpecifics.split("~_~");
    }

    try {
        var MyCard = document.getElementById(pMyCardID).value
        var MyCardArr = MyCard.split("~_~");
    } catch (e) {
        //var MyCard = "NewRecord~_~Name of Merch~_~Card Num~_~PIN~_~Bal~_~Bal Date Time~_~1"
        var MyCard = "NewRecord~_~" + CardSpecificsArr[0]+ "~_~~_~~_~~_~"
        var MyCardArr = MyCard.split("~_~");
    }

    if (MyCardArr[0] == "NewRecord") {
        document.getElementById("tridAdjustBalance").style.opacity = 0;
    }
    else
    {
        document.getElementById("tridAdjustBalance").style.opacity = 1;
    }

    document.getElementById('txtCardType').value = CardSpecificsArr[0];
    if (CardSpecificsArr[1] == "NOURL") {
        document.getElementById("txtCardType").removeAttribute("disabled")
        document.getElementById("txtCardType").style.opacity = 1;
    }
    else {
        document.getElementById("txtCardType").setAttribute("disabled", true);
        document.getElementById("txtCardType").style.opacity = 0.5;
    }
    document.getElementById('hdnCardNumMin').value = CardSpecificsArr[2];
    document.getElementById('hdnCardNumMax').value = CardSpecificsArr[3];
    document.getElementById('hdnCardPINMin').value = CardSpecificsArr[4];
    document.getElementById('hdnCardPINMax').value = CardSpecificsArr[5];
    if (CardSpecificsArr[4] == "0") {
        document.getElementById("tridCardPIN").style.display = 'none';
        document.getElementById("tridCardPIN").value = '';
    }
    else {
        document.getElementById("tridCardPIN").style.display = '';
    }

    var CardType = MyCardArr[1];
    var CardURL = CardSpecificsArr[1];
    var CardNumber = MyCardArr[2];
    var CardPIN = MyCardArr[3];
    var LastKnownBal = MyCardArr[4];
    var LastKnownBalDate = MyCardArr[5];
    var IsLookupManual = CardSpecificsArr[6];
    document.getElementById('hdnCardID').value = MyCardArr[0];
    document.getElementById('hdnCardURL').value = CardURL;
    document.getElementById('txtCardType').value = CardType;
    document.getElementById('txtCardNumber').value = CardNumber;
    document.getElementById('txtCardPIN').value = CardPIN;
    document.getElementById('txtCardBalance').value = LastKnownBal;
    //document.getElementById('ta5Lookup').disabled = AllowAutolookup;
    if (IsLookupManual == "1") {
        var ta5Lookup = document.getElementById("ta5Lookup");
        var ta5Save = document.getElementById("ta5Save");
        var ta5Delete = document.getElementById("ta5Delete");
        var txtCardType = document.getElementById('txtCardType');
        if (CardURL=="NOURL") {
            var myhtml = "<nav data-role=\"navbar\">        <ul>          <li><a data-icon=\"save\" id=\"ta5Save\" href=\"javascript:DoRUCardDataModSave()\">Save</a></li>          <li><a data-icon=\"delete\" id=\"ta5Delete\" href=\"javascript:DoDeletePopup()\">Delete</a></li>        </ul>      </nav>";
        }
        else
        {
            var myhtml = "<nav data-role=\"navbar\">        <ul>          <li><a data-icon=\"save\" id=\"ta5Save\" href=\"javascript:DoRUCardDataModSave()\">Save</a></li>          <li><a data-icon=\"search\" id=\"ta5Lookup\" href=\"javascript:copyToClipboard()\"><font color=\"red\">Lookup</font></a></li>          <li><a data-icon=\"delete\" id=\"ta5Delete\" href=\"javascript:DoDeletePopup()\">Delete</a></li>        </ul>      </nav>";
        }
        //$(txtCardType).removeAttr('disabled')
        $('#aaaa').html(myhtml).trigger('create');
        $('#AddModCardHeader').html("").trigger('create');
    }
    else {
        var myhtml = "<nav data-role=\"navbar\">        <ul>          <li><a data-icon=\"save\" id=\"ta5Save\" href=\"javascript:DoRUCardDataModSave()\">Save</a></li>          <li><a data-icon=\"search\" id=\"ta5Lookup\" href=\"javascript:DoNewRequest()\">Lookup</a></li>          <li><a data-icon=\"delete\" id=\"ta5Delete\" href=\"javascript:DoDeletePopup()\">Delete</a></li>        </ul>      </nav>";
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
                //DoCustomPopup02(xml);
            },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            DoCustomPopup02(errorThrown);
        }
    });
}

function DoRUCardDataModBalThenRefresh(change) {
    var pGCGID = document.getElementById('hdnGCGID').value;
    var pCardID = document.getElementById('hdnCardID').value;
    if (pCardID == "NA") {
        alertmsg = "Can't save; this card entry is incomplete.";
        DoCustomPopup02(alertmsg);
        MyCardsDataSel();
        return;
    }
    var pLastKnownBalance = document.getElementById('txtCardBalance').value;
    $.ajax({
        type: "POST",
        url: "GCGWebWS.asmx/RUCardDataMod",
        dataType: "text",
        data: { pGCGKey: pGCGID, CardID: pCardID, CardType: "", CardNumber: "", CardPIN: "", LastKnownBalance: pLastKnownBalance, LastKnownBalanceDate: "", pAction:"UpdateBalance" },
        async: false,
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
            DoCustomPopup02(errorThrown);
        }
    });
}

function AreValuesInRange(action) {
    retVal = true;
    var pGCGID = document.getElementById('hdnGCGID').value;
    var pCardID = document.getElementById('hdnCardID').value;
    var pCardType = document.getElementById('txtCardType').value;
    var pCardNumber = document.getElementById('txtCardNumber').value;
    var pCardPIN = document.getElementById('txtCardPIN').value;
    var pLastKnownBalance = ""; // document.getElementById('LastKnownBalance').value;
    var pLastKnownBalanceDate = ""; // document.getElementById('LastKnownBalanceDate').value;
    var pCardNumMin = document.getElementById('hdnCardNumMin').value;
    var pCardNumMax = document.getElementById('hdnCardNumMax').value;
    var pCardPINMin = document.getElementById('hdnCardPINMin').value;
    var pCardPINMax = document.getElementById('hdnCardPINMax').value;
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

        DoCustomPopup02(alertmsg);
        retVal=false;
    }
    else
    {
        retVal = true;
    }
    return retVal;
}

//This has to do one of the following for action: AddCard, UpdateCard, DeleteCard, UpdateBalance
function DoRUCardDataMod(action) {
    //string GCGID, string CardID, string CardType, string CardNumber, string CardPIN, string CardLogin, string CardPass, string LastKnownBalance, string LastKnownBalanceDate

    var pGCGID= document.getElementById('hdnGCGID').value;
    var pCardID = document.getElementById('hdnCardID').value;
    var pCardType = document.getElementById('txtCardType').value;
    var pCardNumber = document.getElementById('txtCardNumber').value;
    var pCardPIN = document.getElementById('txtCardPIN').value;
    var pLastKnownBalance = ""; // document.getElementById('LastKnownBalance').value;
    var pLastKnownBalanceDate = ""; // document.getElementById('LastKnownBalanceDate').value;
    var pCardNumMin = document.getElementById('hdnCardNumMin').value;
    var pCardNumMax = document.getElementById('hdnCardNumMax').value;
    var pCardPINMin = document.getElementById('hdnCardPINMin').value;
    var pCardPINMax = document.getElementById('hdnCardPINMax').value;
    if (action == "delete") {

        if (pCardID == "NewRecord")
        {
            alertmsg = "Can't delete; this card entry is incomplete.";
            DoCustomPopup02(alertmsg);
            return;
        }
        else
        {
            pAction = "DeleteCard";
        }
    }
    else if (action == "updatebal") {
        pAction = "UpdateBalance";
        var pLastKnownBalance = document.getElementById('txtCardBalance').value;
    }

    else if (action == "save")
    {
        var OK = AreValuesInRange(action);
        if (OK == false)
        {
            return;
        }
        if (pCardID == "NewRecord")
        {
            var pAction = "AddCard"
        }
        else
        {
            var pAction = "UpdateCard"
        }
    }

    $.ajax({
        type: "POST",
        url: "GCGWebWS.asmx/RUCardDataMod",
        dataType: "text",
        data: { pGCGKey: pGCGID, CardID: pCardID, CardType: pCardType, CardNumber: pCardNumber, CardPIN: pCardPIN, LastKnownBalance: pLastKnownBalance, LastKnownBalanceDate: pLastKnownBalanceDate, pAction: pAction},
        async: false,
        success:
            function (xml) {
                var temp1 = EncodedHTMLToText(xml);
                var temp2 = RemoveGCGHeader(temp1);
                if (temp2 == "1") {
                    if (action == "updatebal") action="Update Balance"
                    var mymsg="Complete!";
                }
                else
                {
                    var mymsg = temp2;
                }
                var capaction = action.charAt(0).toUpperCase() + action.slice(1);;
                MyCardsDataSelThenChange();
                DoCustomPopup01("Result", capaction + " " + mymsg);
            },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            DoCustomPopup02(errorThrown);
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
                //DoCustomPopup02(xml);
            },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            DoCustomPopup02(errorThrown);
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
function DoCustomPopup02(msg) {
    //console.log('divclicked');
    if (document.getElementById('hdnTempVar').value=='bounce') {
        document.getElementById('hdnTempVar').value = "";
        $.mobile.changePage("#MyCards");
    }
    $('#CustomPopup02').show();
    $('#popupSpan02msg').html(msg).trigger('create');
    $('#CustomPopup02').popup();
    $('#CustomPopup02').popup('open', { theme: "b", positionTo: "origin", transition: "flip" });
    //fade, pop, flip, turn, flow, slidefade, slide, slideup, slidedown, none
}
function DoCustomPopupYesNo(msg) {
    //console.log('divclicked');
    //$('#CustomPopup01').css('visibility') = 'visible';
    //$('#CustomPopupYN').dialog();
    $('#CustomPopupYN').show();
    $('#popupSpanYNmsg').html(msg).trigger('create');
    $('#CustomPopupYN').popup();
    $('#CustomPopupYN').popup('open', { theme: "b", positionTo: "origin", transition: "flip" });
    //fade, pop, flip, turn, flow, slidefade, slide, slideup, slidedown, none
}

function DoJavaScriptPopup(nothing, msg) {
    //console.log('divclicked');
    DoCustomPopup02(msg);
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
            DoCustomPopup02(errorThrown);
        }
    });
}


function DoAllAlerts() {
    message = "Alert";
    console.error("error: "+message);
    console.log("log: "+message);
    console.warn("warn: " + message); 
    console.info("info: "+ message); 
    DoCustomPopup02(message);
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
            DoCustomPopup02(errorThrown);
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
                //DoCustomPopup02(xml);
            },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            DoCustomPopup02(errorThrown);
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
                DoCustomPopup02(xml);
            },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            DoCustomPopup02(errorThrown);
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

function DoDeletePopup()
{
    DoCustomPopupYesNo('Are you sure you want to delete this entry?');
}
function HandleDeletePopup(YorN) {
    if (YorN == 'Y') {
        document.getElementById('hdnTempVar').value = 'bounce';
        var ChangeScreen = DoRUCardDataMod('delete');
        if (ChangeScreen == 0) {
            return;
        }
        MyCardsDataSel();
    }
    else if (YorN == 'N') {
        $('#CustomPopup02').popup('close');
        //$.mobile.changePage("#MyCards");
    }
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
