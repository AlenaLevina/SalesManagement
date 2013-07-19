$(document).ready(function () {
    $("#chooseThisClient").bind("click", chooseClient);
    $(document).keydown(keyPressed);
    $("#previousClient").click(switchClient);
    $("#nextClient").click(switchClient);
});

function chooseClient() {
    var chosenPath = "/Content/Images/chosen.png";
    var notchosenPath = "/Content/Images/notchosen.png";
    var clientUniqueId = document.getElementById("currentClientUniqueId").innerText;
    var image = $("#chooseThisClient");
    if (image.attr("src") == chosenPath) {
        image.attr("src", notchosenPath);
    } else{
        image.attr("src", chosenPath);
    }
    document.getElementById("ClientUniqueId").value = clientUniqueId;
    validate("/Order/ClientUniqueIdExists", "ClientUniqueId", "#uniqueIdExists", "#uniqueIdNotification");
    $("#matchingClients").fadeOut(600, finishChooseClient);
}

function finishChooseClient() {
    var image = $("#chooseThisClient");
    var notchosenPath = "/Content/Images/notchosen.png";
    image.attr("src", notchosenPath);
}

function keyPressed(e) {
    if (e.keyCode == 27) {
        $("#matchingClients").fadeOut(600,finishChooseClient);
    }
}

function switchClient(e) {
    e.preventDefault();
    var currentPosition = parseInt(document.getElementById("currentClientPosition").innerText);
    var totalAmount = parseInt(document.getElementById("totalClientAmount").innerText);
    var position;
    console.log(e.target.id);
    if (e.target.id == "previousClient") {
        position = currentPosition - 1;
        if (position < 1) position = totalAmount;
    }
    if (e.target.id == "nextClient") {
        position = currentPosition + 1;
        if (position > totalAmount) position = 1;
    }
    var firstName = document.getElementById("clientFirstName").value;
    var lastName = document.getElementById("clientLastName").value;
    var url = "/Order/GetClientsByFullName";
    $("#matchingClients").load(url, { "firstName": firstName, "lastName": lastName, "position": position }, showClient);
}