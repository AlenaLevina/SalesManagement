$(document).ready(function () {
    bindEventsToClientUniqueIdInput();
    bindEventsToClientFullNameInputs();
    bindEventsToProductSkuInput();
    bindEventsToProductNameInput();
    $(document).keydown(keyPressed);
});

function bindEventsToClientUniqueIdInput() {
    $("#ClientUniqueId").bind("keyup", clientUniqueIdChanged);
}

function bindEventsToClientInfoInputs () {
    $("#DeliveryAddress").focus(getClientAddress);
    $("#ContactPhoneNumber").focus(getClientPhone);
}

function unbindEventsToClientInfoInputs() {
    $("#DeliveryAddress").unbind("focus",getClientAddress);
    $("#ContactPhoneNumber").unbind("focus",getClientPhone);
}

function bindEventsToClientFullNameInputs() {
    $("#clientFirstName").bind("keyup", getClients);
    $("#clientLastName").bind("keyup", getClients);
}

function bindEventsToProductSkuInput() {
    $("#ProductSku").bind("keyup", productSkuChanged);
}

function bindEventsToProductNameInput () {
    $("#productName").bind("keyup", getProducts);
}

function clientUniqueIdChanged(e) {
    if (e.keyCode != 27) {
        //setPopupWindowSettings(clientPopupWindowSettings);
        validate("/Order/ClientUniqueIdExists", "ClientUniqueId", "#uniqueIdExists", "#uniqueIdNotification","#matchingClients", loadClientByUniqueId);
    }
}

function loadClientByUniqueId() {
    var url = "/Order/GetClientByUniqueId";
    var clientUniqueId = document.getElementById("ClientUniqueId").value;
    $("#matchingClients").load(url, { "uniqueId": clientUniqueId },showClient);
}

function showClient() {
    $("#matchingClients").fadeIn();
}

function productSkuChanged(e) {
    if (e.keyCode != 27) {
        //setPopupWindowSettings(productPopupWindowSettings);
        validate("/Product/ProductSkuExists", "ProductSku", "#skuExists", "#skuNotification","#matchingProducts",loadProductBySku);
    }
}

function loadProductBySku() {
    var url = "/Product/GetProductBySku";
    var productSku = document.getElementById("ProductSku").value;
    $("#matchingProducts").load(url, { "sku": productSku }, showProduct);
}

function showProduct() {
    $("#matchingProducts").fadeIn();
}

function validate(validationUrl, inputId, imgSelector, notificationSelector,placeholderSelector,callbackIfValid) {
    var parameterValue = document.getElementById(inputId).value;
    var invalidPath = "/Content/Images/cross.png";
    var validPath = "/Content/Images/check.png";
    function getDone(data) {
        if (data.result == false) {
            validationFailed();
        } else if (data.result == true) {
            $(imgSelector).attr("src", validPath);
            $(imgSelector).show();
            $(notificationSelector).hide();
            if (inputId == "ClientUniqueId") bindEventsToClientInfoInputs();
            if (callbackIfValid!=undefined) callbackIfValid();
        }
    }
    function validationFailed() {
        $(imgSelector).attr("src", invalidPath);
        $(imgSelector).show();
        $(notificationSelector).show();
        $(placeholderSelector).fadeOut(600);
        if (inputId == "ClientUniqueId") unbindEventsToClientInfoInputs();
    }
    function getFail() {
        validationFailed();
    }
    $.get(validationUrl, { "parameter": parameterValue }, null, "json")
        .done(getDone)
        .fail(getFail);
}

function getClientAddress() {
    var clientUniqueId = document.getElementById("ClientUniqueId").value;
    var url = "/Order/GetClientAddress";
    $.get(url, { "uniqueId": clientUniqueId }, function (data) {
        document.getElementById("DeliveryAddress").value = data;
    }, "json");
}

function getClientPhone() {
    var clientUniqueId = document.getElementById("ClientUniqueId").value;
    var url = "/Order/GetClientPhone";
    $.get(url, { "uniqueId": clientUniqueId }, function (data) {
        document.getElementById("ContactPhoneNumber").value = data;
    }, "json");
}

function getClients(e) {
    if (e.keyCode != 27) {
        //setPopupWindowSettings(clientPopupWindowSettings);
        loadClientByFullName(1);
    }
}

function keyPressed(e) {
    if (e.keyCode == 27) {
        $("#matchingClients").fadeOut(600/*, finishChooseClient*/);
        $("#matchingProducts").fadeOut(600/*, finishChooseClient*/);
    }
}

function getProducts(e) {
    if (e.keyCode != 27) {
        //setPopupWindowSettings(productPopupWindowSettings);
        loadProductByName(1);
    }
}

function loadClientByFullName(position) {
    var firstName = document.getElementById("clientFirstName").value;
    var lastName = document.getElementById("clientLastName").value;
    var url = "/Order/GetClientsByFullName";
    $("#matchingClients").load(url, { "firstName": firstName, "lastName": lastName, "position": position }, showClient);
}

function loadProductByName(position) {
    var name = document.getElementById("productName").value;
    var url = "/Product/GetProductsByName";
    $("#matchingProducts").load(url, { "name": name, "position": position }, showProduct);
}
