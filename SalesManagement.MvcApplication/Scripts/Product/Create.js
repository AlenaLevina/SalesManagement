console.log("Create.js is loaded");
$(document).ready(function () {
    if ($("#ActionType").attr("value")=="Create") $("#Sku").focus(getSku);
    $("#CategoryId").change(function() {
        var categoryId = this.value;
        if (categoryId == "") $("#characteristics").empty();
        else $("#characteristics").load("/Product/GetCharacteristics", { "categoryId": categoryId });
    });
});

function getSku() {
    var url = "/Product/GenerateSku";
    $.get(url, null,
        function (newSku) {
            document.getElementById("Sku").value = newSku;
        });
}