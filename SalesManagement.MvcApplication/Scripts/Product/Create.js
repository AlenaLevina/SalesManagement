$(document).ready(function () {
    $("#showCharacteristics").click(function () {
        $("#characteristics").load("/Product/GetCharacteristics", null, null);
    });
    $("#generateSku").click(function () {
        var url = "/Product/GenerateSku";
        $.get(url, null,
            function (newSku) {
                document.getElementById("Sku").value = newSku;
            });
    });
    
});