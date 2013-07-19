﻿var productPopupWindowSettings = {
    checkmarkImageSelector: "#chooseThisProduct",
    previousEntityLinkId: "previousProduct",
    previousEntityLinkSelector: "#previousProduct",
    nextEntityLinkId: "nextProduct",
    nextEntityLinkSelector: "#nextProduct",
    currentEntityUniqueIdElementId: "currentProductSku",
    entityUniqueIdInputId: "ProductSku",
    uniqueIdValidationUrl: "/Product/ProductSkuExists",
    validationImageSelector: "#skuExists",
    validationNotificationSelector: "#skuNotification",
    placeholderSelector: "#matchingProducts",
    currentEntityPositionElementId: "currentProductPosition",
    totalEntityAmountElementId: "totalProductAmount",
    loadEntityFunction: loadProductByName
};

$(document).ready(function () {
    setPopupWindowSettings(productPopupWindowSettings);
});


//popupWindowSettings = productPopupWindowSettings;