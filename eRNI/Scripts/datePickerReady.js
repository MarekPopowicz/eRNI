if (!Modernizr.inputtypes.date) {
    $(function () {
        $(".datecontrol").datepicker(
            {
                format: 'yyyy-mm-dd',
                startView: 2,
                weekStart: 1,
                clearBtn: true,
                language: "pl",
                orientation: "bottom right",
                daysOfWeekHighlighted: "0,6",
                autoclose: true,
                todayHighlight: true,
                todayBtn: true
            });
    });
}