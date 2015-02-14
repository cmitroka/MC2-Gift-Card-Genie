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
        DoRUCardDataModBalThenRefresh(0)
        DoCustomPopup01("Balance", msg);
    }
    else if (resptype == "GCBALANCEERR") {
        DoCustomPopup01("Balance Error", 'Sorry, there was a problem with the Card and/or PIN number.');
        //$.mobile.changePage("#MyCards");
    }
    else if (resptype == "OUTOFLOOKUPS") {
        $.mobile.changePage("#PleasePurchaseGCG");
        //$.mobile.changePage("#PleasePurchaseGCG", { transition: "slideup" });
        //DoCustomPopup01("Purchase?", 'You are out of free lookups.  To continue use, please restart the app and purchase more through the initial screen.');
        //$.mobile.changePage("#MyCards");
    }
    else if (resptype == "POPUPTEST00") {
        DoCustomPopup00();
        //$.mobile.changePage("#MyCards");
    }
    else if (resptype == "POPUPTEST01") {
        $('#popupSpan01title').html("title").trigger('create');
        $('#popupSpan01msg').html("msg").trigger('create');
        $('#CustomPopup01').popup();
        $('#CustomPopup01').popup('open', { theme: "b", positionTo: "origin", transition: "flip" });
        //$.mobile.changePage("#MyCards");
    }
    else if (resptype == "GCCAPTCHA") {
        document.getElementById('hdnContReqFileID').value = respdetails;
        $("#WhatToDoImg01").attr("src", "CAPTCHAs/" + respdetails + ".bmp");
        $("#NeedMoreInfoAboveImg").html("<label>Please enter the letters and numbers seen in the image.</label>");
        $("#NeedMoreInfoBelowImg").html("Click 'Send CAPTCHA' to continue");
        $.mobile.changePage("#NeedMoreInfo", { role: "dialog" });
    }
    else if (resptype == "GCNEEDSMOREINFO") {
        var respdetailsarr = respdetails.split("~_~");
        var rqrs = (respdetailsarr[0]);
        var respdetails1 = (respdetailsarr[1]);
        document.getElementById('hdnContReqFileID').value = rqrs;
        $("#WhatToDoImg01").attr("src", "GCGWebImages/Transparent.gif");  //may have to set to a pixel...
        $("#NeedMoreInfoAboveImg").html("<label>" + respdetails1 + "</label>");
        $("#NeedMoreInfoBelowImg").html("Click 'Send Response' to continue");
        $.mobile.changePage("#NeedMoreInfo", { role: "dialog" });
    }
    else if (resptype == "GCCUSTOM") {
        DoCustomPopup01("Hmm...", respdetails);
        //$.mobile.changePage("#MyCards");
    }
    else if (resptype == "WSERR") {
        DoCustomPopup01("Oops", 'We seem to have a server error; looking into it.');
        //$.mobile.changePage("#MyCards");
    }
    else if (resptype == "WSTIMEOUT") {
        DoCustomPopup01("Timeout", 'The server timed out waiting for a response.');
        //$.mobile.changePage("#MyCards");
    }
    else {
        DoCustomPopup01("Error", 'Some failure occured.');
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