console.log("_Main.js is loaded");

$(document).ready(function () {
    //$("a.entityStatusLink").click(function(e) {
    //    e.preventDefault();
    //    var url=$(this).attr("href");
    //    $.get(url, null, function () {
    //        console.log($(this).parent());
    //        console.log("sent");
    //    });
    //});





    $.usingScript("/Scripts/_Shared/_ClientPopupWindow.js", function () {
        preparePopupWindow("span.activeClientUniqueId", "currentClient", "span.activeClientName",ClientPopupWindow.showClientByUniqueId,"#clientInfo");
    });
    
    $.usingScript("/Scripts/_Shared/_ProductPopupWindow.js", function () {
        preparePopupWindow("span.activeProductSku", "currentProduct", "span.activeProductName",ProductPopupWindow.showProductBySku,"#productInfo");
    });
    
    function preparePopupWindow(activeIdSelector, currentEntityId, activeNameSelector, showEntityFunction,entityInfoSelector) {
        var currentEntitySelector = "#" + currentEntityId;


        $(activeIdSelector).click(function () {
            openPopupWindow(this, this.innerText, currentEntityId, showEntityFunction, currentEntitySelector, entityInfoSelector);
        });

        $(activeIdSelector).mouseout(function () {
            closePopupWindow(currentEntitySelector);
        });

        $(activeNameSelector).click(function () {
            openPopupWindow(this, $(this).find(activeIdSelector)[0].innerText, currentEntityId, showEntityFunction, currentEntitySelector,entityInfoSelector);
        });

        $(activeNameSelector).mouseout(function () {
            closePopupWindow(currentEntitySelector);
        });

    }

    function openPopupWindow(eventSource, uniqueId, currentEntityId, showEntityFunction, currentEntitySelector, entityInfoSelector) {
        var popupTop = $(eventSource).offset().top;
        var popupLeft = $(eventSource).offset().left;
        $(currentEntitySelector).fadeOut("slow", function () { $(currentEntitySelector).remove(); });
        $("body").prepend('<div id="' + currentEntityId + '" style="display:none;"></div>');
        showEntityFunction(uniqueId, currentEntitySelector, function () {
            $("img.closeEntityWindow").remove();
            $("img.chooseThisEntity").remove();
            $("div.entityInfo p.links").remove();
            $(currentEntitySelector).css("top", popupTop);
            $(currentEntitySelector).css("left", popupLeft);
        }, function() {
            correctPopupWindowPosition(entityInfoSelector, "#content");
        });
    }
    
    function closePopupWindow(currentEntitySelector) {
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

    if (windowRight - popupRight < 0) {
        popup.offset({ left: windowRight - popupWidth });
        //console.log("on the right of window");
    }
    if (containerRight - popupRight < 0) {
        popup.offset({ left: containerRight - popupWidth });
        //console.log("on the left of container");
    }
    if (windowBottom - popupBottom < 0/* && windowBottom < containerBottom - popupHeight*/) {
        popup.offset({ top: windowBottom - popupHeight });
        //console.log("under window");
    }
    if (containerBottom - popupBottom < 0/* && windowBottom >= containerBottom - popupHeight*/) {
        popup.offset({ top: containerBottom - popupHeight });
        //console.log("under container");
    }
    if (popupLeft - windowLeft < 0) {
        popup.offset({ left: windowLeft });
        //console.log("to the left of window");
    }
    if (popupLeft - containerLeft < 0) {
        popup.offset({ left: containerLeft });
        //console.log("to the left of container");
    }
    if (popupTop - windowTop < 0) {
        popup.offset({ top: windowTop });
        //console.log("above window");
    }
    if (popupTop - containerTop < 0) {
        popup.offset({ top: containerTop });
        //console.log("above container");
    }
}