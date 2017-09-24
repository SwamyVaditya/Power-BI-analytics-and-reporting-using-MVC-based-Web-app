$(document).ready(function() {

    $('#dummy').click();

    var projectrow = document.getElementsByClassName("ProjectRow");
    for (var i = 0; i < projectrow.length; i++) {
        $(projectrow[i]).css("top", "" + i * 9 + "%");
    }

    $('.viewreportbtn').click(function() {
        try {
            var reportName = $(this).attr('id');
            document.getElementById('hidden').value = reportName;
            $('.dummyViewPbiReport')[0].click();
        } catch (e) {
            alert(e);
        }
    });

});

function errorFunc(error) {
    alert(" Error : " + error);
}

function loadPbiReport() {
    // check if the embed url was selected
    var embedUrl = document.getElementById('tb_EmbedURL').value;
    if ("" === embedUrl)
        return;
    // to load a report do the following:
    // 1: set the url
    // 2: add a onload handler to submit the auth token
    iframe = document.getElementById('iFrameEmbedReport');
    iframe.src = embedUrl;
    iframe.onload = postActionLoadReport;
}

// post the auth token to the iFrame. 
function postActionLoadReport() {

    // get the access token.
    accessToken = document.getElementById('accesstoken').value;
    if ("" === accessToken)
        return;
    // construct the push message structure
    // this structure also supports setting the reportId, groupId, height, and width.
    // when using a report in a group, you must provide the groupId on the iFrame SRC
    var m = { action: "loadReport", accessToken: accessToken };
    message = JSON.stringify(m);
    // push the message.
    iframe = document.getElementById('iFrameEmbedReport');
    iframe.contentWindow.postMessage(message, "*");;
}