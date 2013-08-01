
$(document).ready(function () {
    $.getScript("/Scripts/_Shared/_ClientPopupWindow.js",function() {
        bindIdHoverEvents(clientUniqueIdSelector, clientPlaceholderId, ClientPopupWindow.showClientByUniqueId);
    });
    $.getScript("/Scripts/_Shared/_ProductPopupWindow.js",function() {
        bindIdHoverEvents(productSkuSelector, productPlaceholderId, ProductPopupWindow.showProductBySku);
    });

    var clientUniqueIdSelector = "span.activeClientUniqueId";
    var clientPlaceholderId = "currentClient";
    
    //ClientPopupWindow.showClientByUniqueId
    var productSkuSelector = "span.activeProductSku";
    var productPlaceholderId = "currentProduct";
    
    



    function bindIdHoverEvents(entityIdSelector,entityPlaceholderId,showEntityFunction) {
        var entityPlaceholderSelector = "#" + entityPlaceholderId;
        $(entityIdSelector).mouseover(function () {
            var entityId = this.innerText;
            $(this).after('<div id="' + entityPlaceholderId + '"></div>');
            showEntityFunction(entityId, entityPlaceholderSelector);
        });

        $(entityIdSelector).mouseout(function () {
            $(entityPlaceholderSelector).fadeOut("slow", function () { $(entityPlaceholderSelector).remove(); });

        });
    }

});