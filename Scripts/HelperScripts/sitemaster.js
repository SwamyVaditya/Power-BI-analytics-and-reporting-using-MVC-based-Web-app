

window.onload = function () {
    // client side click to embed a selected tile.
    var el = document.getElementById("bEmbedTileAction");
    if (el.addEventListener) {
        el.addEventListener("click", updateEmbedTile, false);
    } else {
        el.attachEvent('onclick', updateEmbedTile);
    }

    //How to navigate from a Power BI Tile to the dashboard
    // listen for message to receive tile click messages.
    if (window.addEventListener) {
        window.addEventListener("message", receiveMessage, false);
    } else {
        window.attachEvent("onmessage", receiveMessage);
    }

    //How to handle server side post backs
    // handle server side post backs, optimize for reload scenarios
    // show embedded tile if all fields were filled in.
    var accessTokenElement = document.getElementById('MainContent_accessTokenTextbox');
    if (null != accessTokenElement) {
        var accessToken = accessTokenElement.value;
        if ("" !== accessToken)
            updateEmbedTile();
    }
};



