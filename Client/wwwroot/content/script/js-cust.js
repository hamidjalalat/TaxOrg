function bootstrapToggleInit() {

    // Tooltips Init
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl)
    })

    // Popover Init
    var popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'))
    var popoverList = popoverTriggerList.map(function (popoverTriggerEl) {
        return new bootstrap.Popover(popoverTriggerEl)
    })

    // Dropdown Init
    var dropdownTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="dropdown"]'))
    var dropdownList = dropdownTriggerList.map(function (dropdownTriggerEl) {
        return new bootstrap.Dropdown(dropdownTriggerEl)
    })
}

function sidebarIconClickedFunction() {
    //alert('Majid');
    var body = $('body');
    if ((body.hasClass('sidebar-toggle-display')) || (body.hasClass('sidebar-absolute'))) {
        body.toggleClass('sidebar-hidden');
    } else {
        body.toggleClass('sidebar-icon-only');
    }
    //alert('Samani');
}

function offcanvasIconClickedFunction() {
    $('.sidebar-offcanvas').toggleClass('active');
}

function modalClose(modalId) {
    $('#' + modalId).modal('hide');
    //$('.modal-backdrop').remove();
}

function setFocusElement(id) {
    $('#' + id).focus();
}

function fileDownloadAsStreamFromUrlFunction(fileName, url) {

    const link = document.createElement('a');
    link.download = fileName ?? '';
    link.href = url;
    document.body.appendChild(link); // Needed for Firefox
    link.click();
    //link.remove();
    document.body.removeChild(link);
}

function fileDownloadAsDataStreamFromApiFunction(fileName, bytesBase64) {

    var link = document.createElement('a');
    link.download = fileName;
    link.href = "data:application/octet-stream;base64," + bytesBase64;
    document.body.appendChild(link); // Needed for Firefox
    link.click();
    document.body.removeChild(link);
}

window.fileDownloadAsFileStreamFromApiFunction = async (fileName, contentStreamReference) => {
    const arrayBuffer = await contentStreamReference.arrayBuffer();
    const blob = new Blob([arrayBuffer]);
    const url = URL.createObjectURL(blob);
    const anchorElement = document.createElement('a');
    anchorElement.href = url;
    anchorElement.download = fileName ?? '';
    anchorElement.click();
    anchorElement.remove();
    URL.revokeObjectURL(url);
}

//fullscreen
function toggleFullScreenClickedFunction() {
    if ((document.fullScreenElement !== undefined && document.fullScreenElement === null) || (document.msFullscreenElement !== undefined && document.msFullscreenElement === null) || (document.mozFullScreen !== undefined && !document.mozFullScreen) || (document.webkitIsFullScreen !== undefined && !document.webkitIsFullScreen)) {
        if (document.documentElement.requestFullScreen) {
            document.documentElement.requestFullScreen();
        } else if (document.documentElement.mozRequestFullScreen) {
            document.documentElement.mozRequestFullScreen();
        } else if (document.documentElement.webkitRequestFullScreen) {
            document.documentElement.webkitRequestFullScreen(Element.ALLOW_KEYBOARD_INPUT);
        } else if (document.documentElement.msRequestFullscreen) {
            document.documentElement.msRequestFullscreen();
        }
    } else {
        if (document.cancelFullScreen) {
            document.cancelFullScreen();
        } else if (document.mozCancelFullScreen) {
            document.mozCancelFullScreen();
        } else if (document.webkitCancelFullScreen) {
            document.webkitCancelFullScreen();
        } else if (document.msExitFullscreen) {
            document.msExitFullscreen();
        }
    }
}

function getElementDimension(id) {
    const dimention = document.querySelector(`${id}`);

    if (dimention) {
        var offsets = dimention.getBoundingClientRect();

        return {
            height: `${dimention.offsetHeight}`,
            width: `${dimention.offsetWidth}`,
            top: `${offsets.top}`,
            left: `${offsets.left}`,
            right: `${offsets.right}`,
        }

    } else {
        return (null);
    }    
}

