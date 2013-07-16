$(document).ready(function () {
    //$("#generateId").click(function () {
    //    var url = "/Order/GenerateId";
    //    $.get(url, null,
    //        function (newId) {
    //            document.getElementById("ClientId").value = newId;
    //        });
    //});
    if ($("#ActionType").attr("value") == "Create") $("#ClientId").focus(getClientId);
});

function getClientId() {
    var url = "/Order/GenerateId";
    $.get(url, null,
        function(newId) {
            document.getElementById("ClientId").value = newId;
        });
}