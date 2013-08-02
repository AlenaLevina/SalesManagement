console.log("_Main.js is loaded");

$(document).ready(function () {
    
    /***** Client popup windows *****/
    
    $.usingScript("/Scripts/_Shared/_ClientPopupWindow.js", function () {
        var dateStarted;
        var dateFinished;
        var holdEnough;
        var secondsToHold = 1;
        $("span.activeClientUniqueId").mouseover(function () {
            holdEnough = true;
            dateStarted = new Date();
            var eventSource = this;
            var popupTop = $(eventSource).offset().top;
            var popupLeft = $(eventSource).offset().left;
            setTimeout(function () {
                if (holdEnough) {
                    var uniqueId = eventSource.innerText;
                    $("body").prepend('<div id="' + "currentClient" + '"></div>');
                    ClientPopupWindow.showClientByUniqueId(uniqueId, "#currentClient", function() {
                        $("img.closeEntityWindow").remove();
                        $("img.chooseThisEntity").remove();
                        $("#currentClient").css("top", popupTop);
                        $("#currentClient").css("left", popupLeft);
                    });
                }
            }, secondsToHold * 1000);
        });
        
        $("span.activeClientUniqueId").mouseout(function () {
            dateFinished = new Date();
            var holdCursorTime = dateFinished.getTime() - dateStarted.getTime();
            if (holdCursorTime / 1000 < secondsToHold) holdEnough = false;
            $("#currentClient").fadeOut("slow", function () { $("#currentClient").remove(); });

        });

        $("span.activeClientName").mouseover(function () {
            holdEnough = true;
            dateStarted = new Date();
            var eventSource = this;
            var popupTop = $(eventSource).offset().top;
            var popupLeft = $(eventSource).offset().left;
            setTimeout(function() {
                if (holdEnough) {
                    var uniqueId = $(eventSource).find("span.activeClientUniqueId")[0].innerText;
                    $("body").prepend('<div id="' + "currentClient" + '"></div>');
                    ClientPopupWindow.showClientByUniqueId(uniqueId, "#currentClient", function() {
                        $("img.closeEntityWindow").remove();
                        $("img.chooseThisEntity").remove();
                        $("#currentClient").css("top", popupTop);
                        $("#currentClient").css("left", popupLeft);
                    });
                }
            }, secondsToHold * 1000);
        });
        
        $("span.activeClientName").mouseout(function () {
            dateFinished = new Date();
            var holdCursorTime = dateFinished.getTime() - dateStarted.getTime();
            if (holdCursorTime / 1000 < secondsToHold) holdEnough = false;
            $("#currentClient").fadeOut("slow", function () { $("#currentClient").remove(); });

        });

        

    });
    
    /***** Product popup windows *****/

    $.usingScript("/Scripts/_Shared/_ProductPopupWindow.js", function () {
        var dateStarted;
        var dateFinished;
        var holdEnough;
        var secondsToHold = 1;
        $("span.activeProductSku").mouseover(function () {
            holdEnough = true;
            dateStarted = new Date();
            var eventSource = this;
            var popupTop = $(eventSource).offset().top;
            var popupLeft = $(eventSource).offset().left;
            setTimeout(function() {
                if (holdEnough) {
                    var sku = eventSource.innerText;
                    $("body").prepend('<div id="' + "currentProduct" + '"></div>');
                    ProductPopupWindow.showProductBySku(sku, "#currentProduct", function() {
                        $("img.closeEntityWindow").remove();
                        $("img.chooseThisEntity").remove();
                        $("#currentProduct").css("top", popupTop);
                        $("#currentProduct").css("left", popupLeft);
                    });
                }
            }, secondsToHold * 1000);
        });

        $("span.activeProductSku").mouseout(function () {
            dateFinished = new Date();
            var holdCursorTime = dateFinished.getTime() - dateStarted.getTime();
            if (holdCursorTime / 1000 < secondsToHold) holdEnough = false;
            $("#currentProduct").fadeOut("slow", function () { $("#currentProduct").remove(); });
        });

        $("span.activeProductName").mouseover(function () {
            holdEnough = true;
            dateStarted = new Date();
            var eventSource = this;
            var popupTop = $(eventSource).offset().top;
            var popupLeft = $(eventSource).offset().left;
            setTimeout(function() {
                if (holdEnough) {
                    var sku = $(eventSource).find("span.activeProductSku")[0].innerText;
                    $("body").prepend('<div id="' + "currentProduct" + '"></div>');
                    ProductPopupWindow.showProductBySku(sku, "#currentProduct", function() {
                        $("img.closeEntityWindow").remove();
                        $("img.chooseThisEntity").remove();
                        $("#currentProduct").css("top", popupTop);
                        $("#currentProduct").css("left", popupLeft);
                    });
                }
            }, secondsToHold * 1000);
        });
        
        $("span.activeProductName").mouseout(function () {
            dateFinished = new Date();
            var holdCursorTime = dateFinished.getTime() - dateStarted.getTime();
            if (holdCursorTime / 1000 < secondsToHold) holdEnough = false;
            $("#currentProduct").fadeOut("slow", function () { $("#currentProduct").remove(); });

        });
    });
    
    
    

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
    
    if (containerRight - popupRight < 0) popup.offset({ left: containerRight - popupWidth });
    if (windowRight - popupRight < 0) popup.offset({ left: windowRight - popupWidth });
    if (containerBottom - popupBottom < 0) popup.offset({ top: containerBottom - popupHeight });
    if (windowBottom - popupBottom < 0) popup.offset({ top: windowBottom - popupHeight });
    if (popupLeft - containerLeft < 0) popup.offset({ left: containerLeft });
    if (popupLeft - windowLeft < 0) popup.offset({ left: windowLeft });
    if (popupTop - containerTop < 0) popup.offset({ top: containerTop });
    if (popupTop - windowTop < 0) popup.offset({ top: windowTop });
}