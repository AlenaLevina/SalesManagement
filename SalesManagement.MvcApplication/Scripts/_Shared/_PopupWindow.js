function configuratePopupWindow(popupWindowSettings) {
    $(document).ready(function () {
        $(popupWindowSettings.checkmarkImageSelector).bind("click", chooseEntity);
        $(popupWindowSettings.previousEntityLinkSelector).click(function(e) {
            e.preventDefault();
            switchEntity(directionType.previous);
        });
        $(popupWindowSettings.nextEntityLinkSelector).click(function(e) {
            e.preventDefault();
            switchEntity(directionType.next);
        });
        $(popupWindowSettings.closeEntityWindowSelector).click(closePopupWindow);
        $(document).keydown(keyPressed);
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
        //$(popupWindowSettings.placeholderSelector).fadeOut(600);
        closePopupWindow();
    }

    function switchEntity(direction) {
        
        //console.log("target id: "+e.target.id);
        //e.preventDefault();
        var currentPosition = parseInt(document.getElementById(popupWindowSettings.currentEntityPositionElementId).innerText);
        //console.log("current position: " + currentPosition);
        var totalAmount = parseInt(document.getElementById(popupWindowSettings.totalEntityAmountElementId).innerText);
        //console.log("total amount:" + totalAmount);
        var position;
        switch (direction) {
            case directionType.previous:
                position = currentPosition - 1;
                if (position < 1) position = totalAmount;
                break;
            case directionType.next:
                position = currentPosition + 1;
                if (position > totalAmount) position = 1;
                break;
        }
       //console.log("next position: " + position);
        popupWindowSettings.loadEntityFunction(position);
    }

    function closePopupWindow() {
        $(popupWindowSettings.placeholderSelector).fadeOut(600);
    }
    
    function keyPressed(e) {
        //console.log(e.keyCode);
        switch(e.keyCode) {
            case 27:
                closePopupWindow();
                break;
            //case 37:
            //    switchEntity(directionType.previous);
            //    break;
            //case 39:
            //    switchEntity(directionType.next);
            //    break;
        }
    }

    var directionType = {
        next: 0,
        previous: 1
    };
}
