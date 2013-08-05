console.log("_Main.js is loaded");

$(document).ready(function () {
    
    $.usingScript("/Scripts/_Shared/_ClientPopupWindow.js", function () {
        preparePopupWindow("span.activeClientUniqueId", "currentClient", "span.activeClientName",ClientPopupWindow.showClientByUniqueId,"#clientInfo");
    });
    
    $.usingScript("/Scripts/_Shared/_ProductPopupWindow.js", function () {
        preparePopupWindow("span.activeProductSku", "currentProduct", "span.activeProductName",ProductPopupWindow.showProductBySku,"#productInfo");
    });
    
    function preparePopupWindow(activeIdSelector, currentEntityId, activeNameSelector, showEntityFunction,entityInfoSelector) {
        var dateStarted;
        var dateFinished;
        var holdEnoughName={};
        var holdEnoughId={};
        var secondsToHold = 1;
        var currentEntitySelector = "#" + currentEntityId;


        $(activeIdSelector).mouseover(function () {
            dateStarted = new Date();
            openPopupWindow(holdEnoughId, dateStarted, this, this.innerText, currentEntityId, showEntityFunction, currentEntitySelector, secondsToHold,entityInfoSelector);
        });

        $(activeIdSelector).mouseout(function () {
            closePopupWindow(dateStarted, secondsToHold, holdEnoughId, currentEntitySelector);
        });

        $(activeNameSelector).mouseover(function () {
            dateStarted = new Date();
            openPopupWindow(holdEnoughName, dateStarted, this, $(this).find(activeIdSelector)[0].innerText, currentEntityId, showEntityFunction, currentEntitySelector, secondsToHold,entityInfoSelector);
        });

        $(activeNameSelector).mouseout(function () {
            closePopupWindow(dateStarted, secondsToHold, holdEnoughName, currentEntitySelector);
        });

    }

    function openPopupWindow(holdEnough, dateStarted, eventSource, uniqueId, currentEntityId, showEntityFunction, currentEntitySelector, secondsToHold,entityInfoSelector) {
        holdEnough.value = true;
        var popupTop = $(eventSource).offset().top;
        var popupLeft = $(eventSource).offset().left;
        setTimeout(function () {
            if (holdEnough.value) {
                $(currentEntitySelector).fadeOut("slow", function () { $(currentEntitySelector).remove(); });
                $("body").prepend('<div id="' + currentEntityId + '"></div>');
                showEntityFunction(uniqueId, currentEntitySelector, function () {
                    $("img.closeEntityWindow").remove();
                    $("img.chooseThisEntity").remove();
                    $("div.entityInfo p.links").remove();
                    $(currentEntitySelector).css("top", popupTop);
                    $(currentEntitySelector).css("left", popupLeft);
                }, function() {
                    correctPopupWindowPosition(/*"#clientInfo"*/entityInfoSelector, "#content");
                });
            }
        }, secondsToHold * 1000);
    }
    
    function closePopupWindow(dateStarted, secondsToHold, holdEnough, currentEntitySelector) {
        var dateFinished = new Date();
        var holdCursorTime = dateFinished.getTime() - dateStarted.getTime();
        if (holdCursorTime / 1000 < secondsToHold) holdEnough.value = false;
        $(currentEntitySelector).fadeOut("slow", function () { $(currentEntitySelector).remove(); });
    }

});



(function ($) {
    var loadedScripts = [];
    $.extend(true,
    {
        usingScript: function (path, callback) {
            var found = false;
            for (var i = 0; i < loadedScripts.length; i++)
                if (loadedScripts[i] == path) {
                    found = true;
                    break;
                }

            if (found == false) {
                $.getScript(path, callback);
                loadedScripts.push(path);
            }
        }
    });
})(jQuery);


function correctPopupWindowPosition(popupWindowSelector,popupWindowContainerSelector) {
    var popup = $(popupWindowSelector);
    var container = $(popupWindowContainerSelector);

    var popupWidth=popup.width();
    var popupHeight = popup.height();
    var popupTop = popup.offset().top;
    var popupLeft = popup.offset().left;
    var popupRight = popupLeft + popupWidth;
    var popupBottom = popupTop + popupHeight;
    

    var containerTop = container.offset().top;
    var containerLeft = container.offset().left;
    var containerRight = containerLeft + container.width();
    var containerBottom = containerTop + container.height();
    
    var windowTop = window.pageYOffset;
    var windowRight = window.outerWidth;
    var windowBottom = window.outerHeight;
    var windowLeft = window.pageXOffset;
    
    if (windowRight - popupRight < 0) popup.offset({ left: windowRight - popupWidth });
    if (containerRight - popupRight < 0) popup.offset({ left: containerRight - popupWidth });
    if (windowBottom - popupBottom < 0) popup.offset({ top: windowBottom - popupHeight });
    if (containerBottom - popupBottom < 0) popup.offset({ top: containerBottom - popupHeight });
    if (popupLeft - windowLeft < 0) popup.offset({ left: windowLeft });
    if (popupLeft - containerLeft < 0) popup.offset({ left: containerLeft });
    if (popupTop - windowTop < 0) popup.offset({ top: windowTop });
    if (popupTop - containerTop < 0) popup.offset({ top: containerTop });
}