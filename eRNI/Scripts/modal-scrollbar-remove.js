(function ($) {
    $(document)
        .on('hidden.bs.modal', '.modal', function () {
            $(document.body).removeClass('modal-noscrollbar');
        })
        .on('show.bs.modal', '.modal', function () {
            if ($(window).height() >= $(document).height()) {
                $(document.body).addClass('modal-noscrollbar');
            }
        });
})(window.jQuery);