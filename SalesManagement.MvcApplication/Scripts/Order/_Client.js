(function() {
    var clientPopupWindowSettings = {
        checkmarkImageSelector: "#chooseThisClient",
        previousEntityLinkId: "previousClient",
        previousEntityLinkSelector: "#previousClient",
        nextEntityLinkId: "nextClient",
        nextEntityLinkSelector: "#nextClient",
        currentEntityUniqueIdElementId: "currentClientUniqueId",
        entityUniqueIdInputId: "ClientUniqueId",
        uniqueIdValidationUrl: "/Order/ClientUniqueIdExists",
        validationImageSelector: "#uniqueIdExists",
        validationNotificationSelector: "#uniqueIdNotification",
        placeholderSelector: "#matchingClients",
        currentEntityPositionElementId: "currentClientPosition",
        totalEntityAmountElementId: "totalClientAmount",
        loadEntityFunction: ClientPopupWindow.loadClientByFullName,
        closeEntityWindowSelector: "#closeClientWindow"
    };

    $(document).ready(function() {
        configuratePopupWindow(clientPopupWindowSettings);
    });
})();



