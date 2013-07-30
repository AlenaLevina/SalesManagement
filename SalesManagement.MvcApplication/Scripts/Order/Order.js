$(document).ready(function () {
    bindEventsToClientUniqueIdInput();
    bindEventsToClientFullNameInputs();
    bindEventsToProductSkuInput();
    bindEventsToProductNameInput();
    $("#DeliveryAddress").keypress(function() {
        $("#DeliveryAddress").unbind("focus", getClientAddress);
    });
    $("#ContactPhoneNumber").keypress(function () {
        $("#ContactPhoneNumber").unbind("focus", getClientPhone);
    });
    $("#DeliveryDate").datepicker({ dateFormat: "dd/mm/yy" });
    $("#fakeSubmit").click(summary);
});

function bindEventsToClientUniqueIdInput() {
    $("#ClientUniqueId").bind("keyup", clientUniqueIdChanged);
    
    function clientUniqueIdChanged(e) {
        if (e.keyCode != 27) {
            validate("/Order/ClientUniqueIdExists", "ClientUniqueId", "#uniqueIdExists", "#uniqueIdNotification", "#matchingClients", loadClientByUniqueId);
        }

        function loadClientByUniqueId() {
            var url = "/Order/GetClientByUniqueId";
            var clientUniqueId = document.getElementById("ClientUniqueId").value;
            $("#matchingClients").load(url, { "uniqueId": clientUniqueId }, showClient);
        }
    }
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
    

    function getClients(e) {
        if (e.keyCode != 27) {
            //setPopupWindowSettings(clientPopupWindowSettings);
            loadClientByFullName(1);
        }
    }
}

function bindEventsToProductSkuInput() {
    $("#ProductSku").bind("keyup", productSkuChanged);
    
    function productSkuChanged(e) {
        if (e.keyCode != 27) {
            validate("/Product/ProductSkuExists", "ProductSku", "#skuExists", "#skuNotification", "#matchingProducts", loadProductBySku);
            hideAmountAll();

            function loadProductBySku() {
                var url = "/Product/GetProductBySku";
                var productSku = document.getElementById("ProductSku").value;
                $("#matchingProducts").load(url, { "sku": productSku }, showProduct);
            }
        }
    }
}

function bindEventsToProductNameInput () {
    $("#productName").bind("keyup", getProducts);
    function getProducts(e) {
        if (e.keyCode != 27) {
            loadProductByName(1);
        }
    }
}

function bindEventsToAmountInput() {
    var availableAmount;
    $("#Amount").focus(function () {
        var enteredValue = document.getElementById("Amount").value;
        if (enteredValue == "") {
            var url = "/Product/GetProductAmount";
            var productSku = document.getElementById("ProductSku").value;
            $.get(url, { "sku": productSku }, function(data) {
                availableAmount = data.amount;
                showAmountWarning();
            }, "json");
        }
    });
    $("#Amount").keyup(function () {
        var enteredValue = document.getElementById("Amount").value;
        if (enteredValue == "") hideAmountAll();
        else {
            var chosenAmount = parseInt(enteredValue);
            if (isNaN(chosenAmount)) {
                showAmountError("Amount should be a number");
            } else if (availableAmount < chosenAmount) {
                showAmountError("Max available " + availableAmount + " items");
            } else {
                showAmountOk();
            }
        }
    });
    
    function showAmountWarning() {
        $("#amountLeftWarning").attr("src", "/Content/Images/attention.png");
        $("#amountLeftWarning").show();
        document.getElementById("amountLeftNumber").innerText = availableAmount;
        $("#amountLeftNotification").show();
        $("#amountWrongNotification").hide();
    }

    function showAmountError(message) {
        var invalidPath = "/Content/Images/cross.png";
        $("#amountLeftWarning").attr("src", invalidPath);
        $("#amountLeftWarning").show();
        document.getElementById("amountWrongNotification").innerText = message;
        $("#amountWrongNotification").show();
        $("#amountLeftNotification").hide();
    }

    function showAmountOk() {
        var validPath = "/Content/Images/check.png";
        $("#amountLeftWarning").attr("src", validPath);
        $("#amountLeftWarning").show();
        $("#amountWrongNotification").hide();
        $("#amountLeftNotification").hide();
    }
}

function unbindEventsToAmountInput() {
    $("#Amount").unbind("focus");
    $("#Amount").unbind("keyup");
}

function hideAmountAll() {
    $("#amountLeftWarning").hide();
    $("#amountWrongNotification").hide();
    $("#amountLeftNotification").hide();
}

function showClient() {
    $("#matchingClients").fadeIn();
}

function showProduct() {
    $("#matchingProducts").fadeIn();
}

function validate(validationUrl, inputId, imgSelector, notificationSelector, placeholderSelector, callbackIfValid) {
    var parameterValue = document.getElementById(inputId).value;
    var invalidPath = "/Content/Images/cross.png";
    var validPath = "/Content/Images/check.png";
    if (parameterValue == "") {
        validationFailed();
    } else {
        $.get(validationUrl, { "parameter": parameterValue }, null, "json")
            .done(getDone)
            .fail(getFail);
    }
    function getDone(data) {
        if (data.result == false) {
            validationFailed();
        } else if (data.result == true) {
            $(imgSelector).attr("src", validPath);
            $(imgSelector).show();
            $(notificationSelector).hide();
            if (inputId == "ClientUniqueId") bindEventsToClientInfoInputs();
            if (inputId == "ProductSku") bindEventsToAmountInput();
            if (callbackIfValid != undefined) callbackIfValid();
        }
    }
    function validationFailed() {
        $(imgSelector).attr("src", invalidPath);
        $(imgSelector).show();
        $(notificationSelector).show();
        $(placeholderSelector).fadeOut(600);
        if (inputId == "ClientUniqueId") unbindEventsToClientInfoInputs();
        if (inputId == "ProductSku") unbindEventsToAmountInput();
    }
    function getFail() {
        validationFailed();
    }
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






function loadClientByFullName(position) {
    var firstName = document.getElementById("clientFirstName").value;
    var lastName = document.getElementById("clientLastName").value;
    if (!(firstName == "" && lastName == "")) {
        var url = "/Order/GetClientsByFullName";
        $("#matchingClients").load(url, { "firstName": firstName, "lastName": lastName, "position": position }, showClient);
    } else {
        $("#matchingClients").fadeOut(600);
    }
}

function loadProductByName(position) {
    var name = document.getElementById("productName").value;
    if (name != "") {
        var url = "/Product/GetProductsByName";
        $("#matchingProducts").load(url, { "name": name, "position": position }, showProduct);
    } else {
        $("#matchingProducts").fadeOut(600);
    }
}

//function delPressed(e) {
//    $("#" + e.target.id).unbind();
//    if (e.keyCode==46) {
//        unbindEventsToClientInfoInputs();
//    }
//}

function summary() {
    
    var validartionUrl = "/Order/ValidateOrderModel";
    
    var formInputs = $("form .model");
    var orderModel = {};
    for (var i = 0; i < formInputs.length; i++) {
        orderModel[formInputs[i].id] = formInputs[i].value;
    }
    $.get(validartionUrl, orderModel, function (errors) {
        var valid = true;
        for (var i = 0; i < errors.length;i++) {
            var error = errors[i];
            var selector = "[data-valmsg-for='" + error.elementId + "']";
            var notificationElement = $(selector);
            notificationElement.removeClass("field-validation-valid").addClass("field-validation-error");
            var notification = notificationElement[0];
            if (notification != undefined) {
                var message = error.message;
                notification.innerText =message;
                if (message != "") {
                    notificationElement.show();
                    valid = false;
                } else {
                    notificationElement.hide();
                }
            }
        }
        if (valid) showSummary();
    }, "json");

    function showSummary() {
        var summaryUrl = "/Order/GetOrderSummary";
        $("#popupSummaryWrapper").load(summaryUrl, orderModel, function () {
            $("#summary-button-ok").click(function () {
                $("#submit").click();
            });
            $("#summary-button-cancel").click(function () {
                $("#popupSummaryWrapper").hide("slow");
            });
            $("#popupSummaryWrapper").show(400, function () {
                $("div.summary").slideDown();
            });
        });
    }
}