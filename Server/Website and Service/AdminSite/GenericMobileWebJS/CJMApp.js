/// <reference path="jquery-1.10.2.js" />
/// <reference path="jquery.mobile-1.4.0.min.js" />
/// <reference path="GCGWebSuppFunctions.js" />
function DoInitEmpty() {

}

function BuildDynamicPage(WebserviceName, TitleOfPage)
{
    $.ajax({
        type: "POST",
        url: "CJMAppWS.asmx/"+WebserviceName,
        dataType: "text",
        success:
            function (xml) {
                var temp1 = PMEncodedHTMLToText(xml);
                var temp2 = RemoveHeader(temp1);
                try {
                    $('#dynamicpage_header').text(TitleOfPage);
                    $('#dynamicpage_table').html(temp2).trigger('create');
                } catch (e) {
                    $('#dynamicpage_table').html(temp2);
                }
                $.mobile.changePage("#dynamicpage", { transition: "slideup" });

            },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}

function BuildDynamicPageV2(pFromWhere, TitleOfPage) {


    $.ajax({
        type: "POST",
        url: "CJMAppWS.asmx/RunQuery",
        dataType: "text",
        data: { pFromWhere: pFromWhere },
        success:
            function (xml) {
                var temp1 = PMEncodedHTMLToText(xml);
                var temp2 = RemoveHeader(temp1);
                try {
                    $('#dynamicpage_header').text(TitleOfPage);
                    $('#dynamicpage_table').html(temp2).trigger('create');
                } catch (e) {
                    $('#dynamicpage_table').html(temp2);
                }
                $.mobile.changePage("#dynamicpage", { transition: "slideup" });

            },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}

function BuildDynamicPageV3(pFromWhere, TitleOfPage) {
    var pUser=document.getElementById('username').value;
    var pPass=document.getElementById('password').value;

    $.ajax({
        type: "POST",
        url: "CJMAppWS.asmx/RunQuery",
        dataType: "text",
        data: { pFromWhere: pFromWhere, pUsername:pUser , pPassword:pPass},
        success:
            function (xml) {
                var temp1 = PMEncodedHTMLToText(xml);
                var temp2 = RemoveHeader(temp1);
                try {
                    $('#dynamicpage_header').text(TitleOfPage);
                    $('#dynamicpage_table').html(temp2).trigger('create');
                } catch (e) {
                    $('#dynamicpage_table').html(temp2);
                }
                $.mobile.changePage("#dynamicpage", { transition: "slideup" });

            },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}
function BuildUserReport00(pUserID, TitleOfPage) {
    var pUser = document.getElementById('username').value;
    var pPass = document.getElementById('password').value;
    $.ajax({
        type: "POST",
        url: "CJMAppWS.asmx/MakeUserReport",
        dataType: "text",
        data: { pUserID: pUserID, pUsername: pUser, pPassword: pPass },
        success:
            function (xml) {
                var temp1 = PMEncodedHTMLToText(xml);
                var temp2 = RemoveHeader(temp1);
                try {
                    $('#dynamicpage_header').text(TitleOfPage);
                    $('#dynamicpage_table').html(temp2).trigger('create');
                } catch (e) {
                    $('#dynamicpage_table').html(temp2);
                }
                $.mobile.changePage("#dynamicpage", { transition: "slideup" });

            },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}


function DoAdminLogin() {
    $.ajax({
        type: "POST",
        url: "CJMAppWS.asmx/DoAdminLogin",
        dataType: "text",
        data: { pGCGLogin: document.getElementById('username').value, pGCGPassword: document.getElementById('password').value },
        success:
            function (xml) {
                var temp1 = EncodedHTMLToText(xml);
                var temp2 = RemoveGCGHeader(temp1);
                if (temp2 == "1234567890") {
                    alert("Invalid username and/or password.");
                    document.getElementById('username').value = "";
                    document.getElementById('password').value = "";
                }
                else {
                    window.location.href = "GCGWeb.htm?Session=" + temp2 + "&Channel=Web";
                    //DoInitGCGWeb();
                }
            },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}
function RemoveHeader(dataIn) {
    var retVal = PMGetSubstring(dataIn, 'CJMApp.mc2techservices.com">', '</string>');
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
