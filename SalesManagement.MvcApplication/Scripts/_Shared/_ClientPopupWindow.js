//console.log("ClientPopupWindow loaded");
ClientPopupWindow = {
    showClientByUniqueId: function (clientUniqueId, placeholderSelector) {
        var url = "/Order/GetClientByUniqueId";
        $(placeholderSelector).load(url, { "uniqueId": clientUniqueId }, function () {
            $(placeholderSelector).fadeIn();
        });
    },
    showClientByfullName: function (position, firstName, lastName, placeHolderSelector) {
        if (!(firstName == "" && lastName == "")) {
            var url = "/Order/GetClientsByFullName";
            $(placeHolderSelector).load(url, { "firstName": firstName, "lastName": lastName, "position": position }, function () {
                $(placeHolderSelector).fadeIn();
            });
        } else {
            $(placeHolderSelector).fadeOut(600);
        }
    },
    loadClientByFullName: function (position, placeHolderSelector) {
        var firstName = document.getElementById("clientFirstName").value;
        var lastName = document.getElementById("clientLastName").value;
        this.showClientByfullName(position, firstName, lastName, placeHolderSelector);
    }
};