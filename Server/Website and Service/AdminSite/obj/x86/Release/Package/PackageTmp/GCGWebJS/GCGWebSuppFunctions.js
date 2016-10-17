function getURLParameter(name) {
    return decodeURIComponent((new RegExp('[?|&]' + name + '=' + '([^&;]+?)(&|#|;|$)').exec(location.search) || [, ""])[1].replace(/\+/g, '%20')) || null
}

String.prototype.trim = function () {
    return this.replace(/^\s+|\s+$/g, '');
}



function FixWhitepace(myString) {
    //var myString = '  bunch    of <br> string data with<p>trailing</p> and leading space   ';
    var myString2 = myString.trim();
    return myString2;
}

function IsSessionValid(SessionID) {
    //var myString = '  bunch    of <br> string data with<p>trailing</p> and leading space   ';
    var retVal = false;
    try {
        if (SessionID.length == 15) {
            retVal = true;
        }
    } catch (e) {
    }
    return retVal;
}

function GCGGetSubstring(fullString, startString, endString) {
    var UCfullString = fullString.toUpperCase();
    startString = startString.toUpperCase();
    endString = endString.toUpperCase();
    if (endString == "") endString = "NEVERFINDTHISDYHJDYUXKSSDULSJDJ";
    var indexOfStart = UCfullString.indexOf(startString)
    var successStep = 0;
    if (indexOfStart == -1) {
    }
    else {
        successStep = successStep + 1;
    }
    if (successStep==0) return "";
    indexOfStart = indexOfStart + (startString.length-1);

    var adjustedFullString = fullString.substring(indexOfStart, 999999);
    var UCadjustedFullString = adjustedFullString.toUpperCase();
    var indexOfEnd = UCadjustedFullString.indexOf(endString)
    if (indexOfEnd == -1) {
        if (endString == "NEVERFINDTHISDYHJDYUXKSSDULSJDJ") {
            successStep = successStep + 1;
            indexOfEnd = 999999;
        }
    }
    else {
        successStep = successStep + 1;
    }
    if (successStep <2) return "";
    indexOfEnd = indexOfEnd;  //not sure if this is needed, may be trimming the last char...
    var retval = adjustedFullString.substring(1, indexOfEnd);
    return retval;
}
function BackToMain() {
    var retval = "A";
    //$("#NeedMoreInfo").dialog("close");
    //$('#CustomPopup01').popup('close');
    $.mobile.changePage('#MyCards');
}
function GCGHandleResponse(resptype, respdetails, respadditionaldetails) {
    //GCBALANCEERR, GCNEEDSMOREINFO
    //resptype = "POPUPTEST01"
    if (resptype == "GCBALANCE") {
        var msg = "Your balance is <br>" + respdetails;
        msg = "Your balance is\n" + respdetails;
        document.getElementById('txtCardBalance').value = respdetails;
        DoRUCardDataModBalThenRefresh(1);
        DoCustomPopup01("Balance", msg);
    }
    else if (resptype == "REDIRECTING") {  //"MANUALLOOKUP"
        SetMLParams();
    }
    else if (resptype == "OUTOFLOOKUPS") {
        $.mobile.changePage("#PleasePurchaseGCG");
    }
    else if (resptype == "GCCAPTCHA") {
        document.getElementById('txtMoreInfoAnswer').value = "";
        document.getElementById('hdnContReqFileID').value = respdetails;
        $("#WhatToDoImg01").attr("src", "CAPTCHAs/" + respdetails + ".bmp");
        $("#NeedMoreInfoAboveImg").html("<label>Please enter the letters and numbers seen in the image.</label>");
        $("#NeedMoreInfoBelowImg").html("Click 'Send CAPTCHA' to continue.  NOTE: They are CASE SENSTIVE!");
        $.mobile.changePage("#NeedMoreInfo", { role: "dialog" });
    }
    else {
        if (confirm("We couldn't automatically get the balance; would you like to use the alternate lookup method?")) {
            DoNewManualRequest();
        } else {
            DoRUCardDataModBalThenRefresh(1);
        }
        //$.mobile.changePage("#MyCards");
    }
}
function GCGAlert()
{
alert('A');
}
function GCGAlert(whatToSay)
{
alert(whatToSay);
}