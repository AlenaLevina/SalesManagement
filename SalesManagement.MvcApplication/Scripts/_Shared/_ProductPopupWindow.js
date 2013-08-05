console.log("_ProductPopupWindow.js is loaded");

ProductPopupWindow = {
    showProductBySku: function (productSku, placeholderSelector,callbackBefore,callbackAfter) {
        var url = "/Product/GetProductBySku";
        $(placeholderSelector).load(url, { "sku": productSku }, function () {
            if (callbackBefore != undefined && callbackBefore != null) callbackBefore();
            $(placeholderSelector).fadeIn();
            if (callbackAfter != undefined && callbackAfter != null) callbackAfter();
        });
    },
    showProductByName: function (position, name, placeholderSelector,callback) {
        if (name != "") {
            var url = "/Product/GetProductsByName";
            $(placeholderSelector).load(url, { "name": name, "position": position }, function () {
                $(placeholderSelector).fadeIn();
                if (callback != undefined && callback != null) callback();
            });
        } else {
            $(placeholderSelector).fadeOut(600);
        }
    },
    loadProductByName: function (position, placeholderSelector) {
        var name = document.getElementById("productName").value;
        this.showProductByName(position, name, placeholderSelector);
    }
};