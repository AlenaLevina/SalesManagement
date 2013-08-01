//console.log("ProductPopupWindow loaded");
ProductPopupWindow = {
    showProductBySku: function (productSku, placeholderSelector) {
        var url = "/Product/GetProductBySku";
        $(placeholderSelector).load(url, { "sku": productSku }, function () {
            $(placeholderSelector).fadeIn();
        });
    },
    showProductByName: function (position, name, placeholderSelector) {
        if (name != "") {
            var url = "/Product/GetProductsByName";
            $(placeholderSelector).load(url, { "name": name, "position": position }, function () {
                $(placeholderSelector).fadeIn();
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