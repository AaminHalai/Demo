
/*
 * Non-blocking screen messages
 */

function displayToastr() {
    //toastr.options.showMethod = 'slideDown';
    //toastr.options.positionClass = 'toast-top-center';
    toastr.options.closeButton = false;
    toastr.options.showDuration = 100;
    toastr.options.hideDuration = 100;
    toastr.options.progressBar = true;
    toastr.options.timeOut = 3000;
    //toastr.options.extendedTimeOut = 1000;
    //toastr.options.showEasing = 'swing';

    $('[data-km-toastr-info]').each(function () {
        toastr.info($(this).data('km-toastr-info'), 'Info');
    });
    $('[data-km-toastr-success]').each(function () {
        toastr.success($(this).data('km-toastr-success'), 'Success!');
    });
    $('[data-km-toastr-warning]').each(function () {
        toastr.warning($(this).data('km-toastr-warning'), 'Warning!');
    });
    $('[data-km-toastr-error]').each(function () {
        toastr.error($(this).data('km-toastr-error'), 'Error!');
    });
}

/*
 * Initialize
 */

$(document).ready(function () {
    displayToastr();
});

