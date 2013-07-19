var popupWindowSettings;

function setPopupWindowSettings(settingsObject) {
    popupWindowSettings = settingsObject;
}

$(document).ready(function () {
    $(popupWindowSettings.checkmarkImageSelector).bind("click", chooseEntity);
    $(popupWindowSettings.previousEntityLinkSelector).click(switchEntity);
    $(popupWindowSettings.nextEntityLinkSelector).click(switchEntity);
});

function chooseEntity() {
    var chosenPath = "/Content/Images/chosen.png";
    var notchosenPath = "/Content/Images/notchosen.png";
    var entityUniqueId = document.getElementById(popupWindowSettings.currentEntityUniqueIdElementId).innerText;
    var image = $(popupWindowSettings.checkmarkImageSelector);
    if (image.attr("src") == chosenPath) {
        image.attr("src", notchosenPath);
    } else {
        image.attr("src", chosenPath);
    }
    document.getElementById(popupWindowSettings.entityUniqueIdInputId).value = entityUniqueId;
    validate(popupWindowSettings.uniqueIdValidationUrl,
        popupWindowSettings.entityUniqueIdInputId,
        popupWindowSettings.validationImageSelector,
        popupWindowSettings.validationNotificationSelector);
    $(popupWindowSettings.placeholderSelector).fadeOut(600);
}

function switchEntity(e) {
    e.preventDefault();
    var currentPosition = parseInt(document.getElementById(popupWindowSettings.currentEntityPositionElementId).innerText);
    var totalAmount = parseInt(document.getElementById(popupWindowSettings.totalEntityAmountElementId).innerText);
    var position;
    if (e.target.id == popupWindowSettings.previousEntityLinkId) {
        position = currentPosition - 1;
        if (position < 1) position = totalAmount;
    }
    if (e.target.id == popupWindowSettings.nextEntityLinkId) {
        position = currentPosition + 1;
        if (position > totalAmount) position = 1;
    }
    loadClientByFullName(position);

}