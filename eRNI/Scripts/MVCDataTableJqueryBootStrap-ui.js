MVCDataTableJqueryBootStrap = {

    init: function () {
        this.initDataTable();
    },

    initDataTable: function () {
        var table = $('.data-Table').DataTable({
            "language": {
                "processing": "Przetwarzanie...",
                "search": "Szukaj:",
                "lengthMenu": "Pokaż _MENU_ pozycji",
                "info": "Pozycje: _START_ - _END_ / _TOTAL_",
                "infoEmpty": "Pozycji 0 z 0 dostępnych",
                "infoFiltered": "(filtrowanie spośród _MAX_ dostępnych pozycji)",
                "infoPostFix": "",
                "loadingRecords": "Wczytywanie...",
                "zeroRecords": "Nie znaleziono pasujących pozycji",
                "emptyTable": "Brak danych",
                "paginate": {
                    "first": "Pierwsza",
                    "previous": "Poprzednia",
                    "next": "Następna",
                    "last": "Ostatnia"
                }
            },
            "columnDefs": [{ "orderable": false, "targets": -1 }],
            "order": [[0, "desc"]]
        });

        MVCDataTableJqueryBootStrap.returnDataTable = function () {
            return table;
        }
    },
};

$(document).ready(function () {
    MVCDataTableJqueryBootStrap.init();
});