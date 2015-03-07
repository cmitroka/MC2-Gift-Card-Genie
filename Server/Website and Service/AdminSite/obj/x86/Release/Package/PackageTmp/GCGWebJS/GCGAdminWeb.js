/// <reference path="jquery-1.10.2.js" />
/// <reference path="jquery.mobile-1.4.0.min.js" />
/// <reference path="GCGWebSuppFunctions.js" />
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

function DoInitGCGAdminWeb() {
    $.ajax({
        type: "POST",
        url: "GCGAdminWebWS.asmx/GetPurchaseLog",
        dataType: "text",
        success:
            function (xml) {
                var temp1 = EncodedHTMLToText(xml);
                var temp2 = RemoveGCGHeader(temp1);
                $('#purchaselogtbody').append(temp2);
            },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
    $.ajax({
        type: "POST",
        url: "GCGAdminWebWS.asmx/GetRqRsLog",
        dataType: "text",
        success:
            function (xmla) {
                var tempa1 = EncodedHTMLToText(xmla);
                var tempa2 = RemoveGCGHeader(tempa1);
                $('#rqrstbody').append(tempa2);
            },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
    $.ajax({
        type: "POST",
        url: "GCGAdminWebWS.asmx/GetMetricsSummary",
        dataType: "text",
        success:
            function (xmlb) {
                var tempb1 = EncodedHTMLToText(xmlb);
                var tempb2 = RemoveGCGHeader(tempb1);
                $('#metricssummary').append(tempb2);
            },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}

function RemoveGCGHeader(dataIn) {
    var retVal = GCGGetSubstring(dataIn, 'gcg.mc2techservices.com">', '</string>');
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
