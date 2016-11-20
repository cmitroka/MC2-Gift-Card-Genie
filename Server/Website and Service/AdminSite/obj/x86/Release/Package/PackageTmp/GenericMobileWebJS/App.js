/// <reference path="jquery-1.10.2.js" />
/// <reference path="jquery.mobile-1.4.0.min.js" />
/// <reference path="GCGWebSuppFunctions.js" />
function DoDiagSayHello() {
    $.ajax({
        type: "POST",
        url: "BCAppWS.asmx/TestConnect",
        dataType: "text",
        data: { pGCGKey: document.getElementById('hdnGCGID').value },
        success:
            function (xml) {
                alert(xml);
            },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }

    });
}
function DoInitEmpty() {

}

function DoClearScreen() {
    var z = document.getElementById('txtCodeIn').value;
    document.getElementById('txtCodeIn').value = "";
    document.getElementById('txtOverrideCode').value = "";
    
}

function DoGetOverrideCode() {
    $.ajax({
        type: "POST",
        url: "BCAppWS.asmx/GetOverrideCode",
        dataType: "text",
        data: { pCodeIn: document.getElementById('txtCodeIn').value, pKeyIn: document.getElementById('txtEncKey').value },
        success:
            function (xml) {
                //alert(xml);
                var temp1 = PMEncodedHTMLToText(xml);
                var temp2 = RemoveHeaderB(temp1);
                document.getElementById('txtOverrideCode').value = temp2;
            },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }

    });
}


function DoInit() {
    $.ajax({
        type: "POST",
        url: "BCAppWS.asmx/GetReportData",
        dataType: "text",
        success:
            function (xml) {
                var temp1 = PMEncodedHTMLToText(xml);
                var temp2 = RemoveHeader(temp1);
                $('#allrequeststbody').append(temp2);
            },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
    $.ajax({
        type: "POST",
        url: "BCAppWS.asmx/GetReportDataForToday",
        dataType: "text",
        success:
            function (xml) {
                var temp1 = PMEncodedHTMLToText(xml);
                var temp2 = RemoveHeader(temp1);
                $('#allrequeststodaytbody').append(temp2);
            },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
    $.ajax({
        type: "POST",
        url: "BCAppWS.asmx/GetSummary",
        dataType: "text",
        success:
            function (xml) {
                var temp1 = PMEncodedHTMLToText(xml);
                var temp2 = RemoveHeader(temp1);
                $('#summarytbody').append(temp2);
            },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}

function RemoveHeaderB(dataIn) {
    var retVal = PMGetSubstring(dataIn, 'webservice.mc2techservices.com">', '</string>');
    return retVal;
}

function PMGetSubstring(fullString, startString, endString) {
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
    if (successStep == 0) return "";
    indexOfStart = indexOfStart + (startString.length - 1);

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
    if (successStep < 2) return "";
    indexOfEnd = indexOfEnd;  //not sure if this is needed, may be trimming the last char...
    var retval = adjustedFullString.substring(1, indexOfEnd);
    return retval;
}

function PMHTMLEscape(str) {
    return String(str)
            .replace(/&/g, '&amp;')
            .replace(/"/g, '&quot;')
            .replace(/'/g, '&#39;')
            .replace(/</g, '&lt;')
            .replace(/>/g, '&gt;');
}

function PMEncodedHTMLToText(str) {
    //var ucstr=str.toUpperCase();
    return String(str)
            .replace(/&lt;/g, '<')
            .replace(/&gt;/g, '>')
            .replace(/&amp;/g, '&')
            .replace(/&quot;/g, '"')
            .replace(/&#39;/g, "'");
}
