function callWebMethodOfMyService() {
    // Page 536 of [1] shows a template of how to call the 
    // jQuery .ajax method.  Here is my own interpretation
    // of Snell's and Northrup's thoughts.
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json",
        url: "http://localhost:55555/WebService.asmx/DownloadAllData",
        data: null,
        success:
					function (data) {
					    var theDateBox =
							document.getElementById('theDateBox');
					    theDateBox.value = data.d;
					},
        error: function () {
            alert("Could not call Web Services.  Try Again.");
        }
    });
}