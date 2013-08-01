console.log("_Main.js is loaded");
(function ($) {
    var loadedScripts = [];
    $.extend(true,
    {
        usingScript: function (path,callback) {
            var found = false;
            for (var i = 0; i < loadedScripts.length; i++)
                if (loadedScripts[i] == path) {
                    found = true;
                    break;
                }

            if (found == false) {
                $.getScript(path,callback);
                loadedScripts.push(path);
            }
        }
    });
})(jQuery);

  


$(document).ready(function () {
    var clientUniqueIdSelector = "span.activeClientUniqueId";
    var clientPlaceholderId = "currentClient";

    var productSkuSelector = "span.activeProductSku";
    var productPlaceholderId = "currentProduct";

    $.usingScript("/Scripts/_Shared/_ClientPopupWindow.js", function () {
        bindIdHoverEvents(clientUniqueIdSelector, clientPlaceholderId, ClientPopupWindow.showClientByUniqueId, function() {
            $("img#chooseThisClient").remove();
            $("img#closeClientWindow").remove();
        });
    });
    
    $.usingScript("/Scripts/_Shared/_ProductPopupWindow.js",function() {
        bindIdHoverEvents(productSkuSelector, productPlaceholderId, ProductPopupWindow.showProductBySku, function () {
            $("img#chooseThisProduct").remove();
            $("img#closeProductWindow").remove();
        });
    });
    
    function bindIdHoverEvents(entityIdSelector,entityPlaceholderId,showEntityFunction,callbackAfterShow) {
        var entityPlaceholderSelector = "#" + entityPlaceholderId;
        $(entityIdSelector).mouseover(function () {
            var top = $(this)[0].offsetTop;
            var left = $(this)[0].offsetLeft;
            var entityId = this.innerText;
            $("body").prepend('<div id="' + entityPlaceholderId + '"></div>');
            showEntityFunction(entityId, entityPlaceholderSelector, function () {
                $("div.entityInfo").css("top", top);
                $("div.entityInfo").css("left", left);
                callbackAfterShow();
            });
            
        });

        $(entityIdSelector).mouseout(function () {
            $(entityPlaceholderSelector).fadeOut("slow", function () { $(entityPlaceholderSelector).remove(); });

        });
    }

});