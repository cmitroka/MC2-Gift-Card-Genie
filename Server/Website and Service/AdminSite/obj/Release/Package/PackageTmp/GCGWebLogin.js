function DoLogin() {
    $.ajax({
        type: "POST",
        url: "GCGWebWS.asmx/GCGLogin",
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
function DoDemo() {
    RegisterUserInsDemo();
}

function RegisterUserInsDemo() {
    //alert("DoRegisterUserIns");
    $.ajax({
        type: "POST",
        url: "GCGWebWS.asmx/DemoGCG",
        dataType: "text",
        data: { pIP: '' },
        success:
            function (xml) {
                var temp1 = EncodedHTMLToText(xml);
                var temp2 = RemoveGCGHeader(temp1);
                var session = temp2;
                window.location.href = "GCGWeb.htm?Session=" + session;

            },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}
