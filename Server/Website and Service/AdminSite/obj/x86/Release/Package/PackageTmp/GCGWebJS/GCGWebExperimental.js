/// <reference path="jquery.json-2.4.min.js" />
function GCGExp1() {
    var myData = { id: "li1234", myInt: [100, 200], data: { blaBla: "Hahhh!", iii: [10, 20, 30]} }
    var myDataForjQuery = { input: $.toJSON(myData) };
    alert('ok');
    $.ajax({
        type: "POST",
        url: "/WebService.asmx/AjaxGetMore", // + idAsJson,
        data: myDataForjQuery, // idAsJson, //myData,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (msg) {
            // var msg = {__type: "Testportal.outputData", id: "li1234", message: "it's work!", myInt:101}
            alert("message=" + msg.d.message + ", id=" + msg.d.id + ", myInt=" + msg.d.myInt);
        },
        error: function (res, status) {
            if (status === "error") {
                // errorMessage can be an object with 3 string properties: ExceptionType, Message and StackTrace
                var errorMessage = $.parseJSON(res.responseText);
                alert(errorMessage.Message);
            }
        }
    });
}

