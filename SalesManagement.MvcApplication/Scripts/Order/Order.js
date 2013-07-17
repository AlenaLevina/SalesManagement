$(document).ready(function () {
    $("#ClientUniqueId").bind("keyup", clientUniqueIdChanged);
    $("#ClientUniqueId").bind("change", clientUniqueIdChanged);
    $("#ProductSku").bind("keyup", productSkuChanged);
    $("#ProductSku").bind("change", productSkuChanged);
    $("#DeliveryAddress").focus(getClientAddress);
    $("#ContactPhoneNumber").focus(getClientPhone);
});

function clientUniqueIdChanged() {
    var valid = validate("/Order/ClientUniqueIdExists", "ClientUniqueId", "#uniqueIdExists", "#uniqueIdNotification",showClient);
    //console.log(valid);
}

function showClient() {
    console.log("Valid!!!!!!!!");
}

function productSkuChanged() {
    var valid = validate("/Product/ProductSkuExists", "ProductSku", "#skuExists", "#skuNotification");
    //console.log(valid);
}

function validate(validationUrl, inputId, imgSelector, notificationSelector,callbackIfValid) {
    var parameterValue = document.getElementById(inputId).value;
    //var isValid = true;
    var invalidPath = "/Content/Images/cross.png";
    var validPath = "/Content/Images/check.png";
    function getDone(data) {
        if (data == false) {
            $(imgSelector).attr("src", invalidPath);
            $(imgSelector).show();
            $(notificationSelector).show();
        } else if (data == true) {
            $(imgSelector).attr("src", validPath);
            $(imgSelector).show();
            $(notificationSelector).hide();
            callbackIfValid();
        }
        //isValid = data;
    }
    function getFail() {
        $(imgSelector).attr("src", invalidPath);
        $(imgSelector).show();
        $(notificationSelector).show();
        //isValid = false;
    }
    $.get(validationUrl, { "parameter": parameterValue },null,"json")
        .done(getDone)
        .fail(getFail);
    //return isValid;
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