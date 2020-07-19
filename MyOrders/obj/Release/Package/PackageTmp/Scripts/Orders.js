//document.getElementById("myPrivided").addEventListener("click", myProvided);

$(document).ready(function () {
    $('.Edit').click(function (e) {
        e.preventDefault();

        var id = $(this).attr('value');

        var url = $('#More').data('url');

        $.get(url + "/" + id, function (data) {
            $("#More").html(data);
            $("#More").modal('show');
        });
    });
});

$(document).ready(function () {
    $('.Delete').click(function () {

        var id = $(this).attr('value');

        var url = 'Orders/Delete/' + id;
        alert(url);
        $.get(url, function (data) {

           
        });
    });
});

$(function () {
    $('[data-toggle="tooltip"]').tooltip()
})


const newLocal = $(document).ready(function() {
    $('#example').DataTable();
});

function myProvided() {
    var url = window.location.search;
    var params = URLSearchParams()
}

