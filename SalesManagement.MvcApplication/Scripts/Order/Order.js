$(document).ready(function () {
    bindEvents();
    $("#ProductSku").bind("keyup", productSkuChanged);
    $("#ProductSku").bind("change", productSkuChanged);
    $("#DeliveryAddress").focus(getClientAddress);
    $("#ContactPhoneNumber").focus(getClientPhone);
    $("#getClientsByFullName").bind("click",getClients);
});

function bindEvents() {
    $("#ClientUniqueId").bind("keyup", clientUniqueIdChanged);
    $("#ClientUniqueId").bind("change", clientUniqueIdChanged);
}

function unbindEvents () {
    $("#ClientUniqueId").unbind("keyup", clientUniqueIdChanged);
    $("#ClientUniqueId").unbind("change", clientUniqueIdChanged);
}

function clientUniqueIdChanged() {
    validate("/Order/ClientUniqueIdExists", "ClientUniqueId", "#uniqueIdExists", "#uniqueIdNotification",loadClient);
}

function loadClient() {
    unbindEvents();
    var url = "/Order/GetClient";
    var clientUniqueId = document.getElementById("ClientUniqueId").value;
    $("#matchingClients").load(url, { "uniqueId": clientUniqueId },showClient);
}

function showClient() {
    $("#matchingClients").fadeIn();
}

function productSkuChanged() {
    validate("/Product/ProductSkuExists", "ProductSku", "#skuExists", "#skuNotification");
}

function validate(validationUrl, inputId, imgSelector, notificationSelector,callbackIfValid) {
    var parameterValue = document.getElementById(inputId).value;
    var invalidPath = "/Content/Images/cross.png";
    var validPath = "/Content/Images/check.png";
    function getDone(data) {
        if (data.result == false) {
            $(imgSelector).attr("src", invalidPath);
            $(imgSelector).show();
            $(notificationSelector).show();
        } else if (data.result == true) {
            $(imgSelector).attr("src", validPath);
            $(imgSelector).show();
            $(notificationSelector).hide();
            callbackIfValid();
        }
    }
    function getFail() {
        $(imgSelector).attr("src", invalidPath);
        $(imgSelector).show();
        $(notificationSelector).show();
    }
    $.get(validationUrl, { "parameter": parameterValue },null,"json")
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

function getClients() {
    var firstName = document.getElementById("clientFirstName");
    var lastName = document.getElementById("clientLastName");
    var url = "/Order/GetClientsByFullName";
    $("#matchingClients").load(url, { "firstName": firstName, "lastName": lastName },showClient);
}

