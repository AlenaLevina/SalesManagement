$(document).ready(function () {
    $("#ClientUniqueId").bind("keyup", showClient);//change(showClient);
});

function showClient() {
    var url = "/Order/ClientUniqueIdExists";
    var clientUniqueId = document.getElementById("ClientUniqueId").value;
    var successfull = false;
    var dontExistsPath = "/Content/Images/cross.png";
    var existsPath = "/Content/Images/check.png";
    $.get(url, { "uniqueId": clientUniqueId }, function (data) {
        successfull = true;
        if (data == false) {
            $("#uniqueIdExists").show();
            $("#uniqueIdExists").attr("src", dontExistsPath);
        } else {
            $("#uniqueIdExists").show();
            $("#uniqueIdExists").attr("src", existsPath);
        }
    }, "json");
    if (!successfull) {
        $("#uniqueIdExists").show();
        $("#uniqueIdExists").attr("src", dontExistsPath);
    }
}