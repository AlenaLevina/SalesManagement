console.log("_ClientPopupWindow.js is loaded");
ClientPopupWindow = {
    showClientByUniqueId: function (clientUniqueId, placeholderSelector,callback) {
        var url = "/Order/GetClientByUniqueId";
        $(placeholderSelector).load(url, { "uniqueId": clientUniqueId }, function () {
            $(placeholderSelector).fadeIn();
            if (callback!=undefined&&callback!=null) callback();
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