console.log("_ClientPopupWindow.js is loaded");
ClientPopupWindow = {
    showClientByUniqueId: function (clientUniqueId, placeholderSelector,callbackBefore,callbackAfter) {
        var url = "/Order/GetClientByUniqueId";
        $(placeholderSelector).load(url, { "uniqueId": clientUniqueId }, function () {
            if (callbackBefore != undefined && callbackBefore != null) callbackBefore();
            $(placeholderSelector).fadeIn();
            if (callbackAfter != undefined && callbackAfter != null) callbackAfter();
        });
    },
    showClientByfullName: function (position, firstName, lastName, placeHolderSelector,callback) {
        if (!(firstName == "" && lastName == "")) {
            var url = "/Order/GetClientsByFullName";
            $(placeHolderSelector).load(url, { "firstName": firstName, "lastName": lastName, "position": position }, function () {
                $(placeHolderSelector).fadeIn();
                if (callback != undefined && callback != null) callback();
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