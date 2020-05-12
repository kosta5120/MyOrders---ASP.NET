$(document).ready(function () {
    $('.Edit').click(function () {
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