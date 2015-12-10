﻿function changeTab(pageId) { //redo this ugly mess
    var tabCtrl = document.getElementById('tabControl');
    var pageToActivate = document.getElementById(pageId);
    for (var i = 0; i < tabCtrl.childNodes.length; i++) {
        var node = tabCtrl.childNodes[i];
        if (node.nodeType == 1) {
            if (node == pageToActivate) {
                switch ($(node).attr('id')) {
                    case 'functionTab':
                        $('#functionTabLink').removeClass('unselectedTab');
                        $('#controlTabLink').addClass('unselectedTab');
                        $('#functionTab').show();
                        $('#controlTab').hide();
                        break;
                    case 'controlTab':
                        $('#controlTabLink').removeClass('unselectedTab');
                        $('#functionTabLink').addClass('unselectedTab');
                        $('#controlTab').show();
                        $('#functionTab').hide();
                        break;
                }
            }
        }
    }
}

$(document).ready(function () {

    FilterControl.Init('#target-frame');
    IframeControl.Init('#target-frame');
    Loading.End();

    var timer;
    var typingInterval = 1000;

    $('#website').keyup(function () {
        clearTimeout(timer);
        timer = setTimeout(function () {
            IframeControl.ChangeSrcDoc($('#website').val())
        }, typingInterval);
    });

    $('#website').keydown(function () {
        $('#website').css('border', '1px solid orange');
        clearTimeout(timer);
    });
});