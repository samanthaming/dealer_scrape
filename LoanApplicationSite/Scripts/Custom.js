$('.remove-check').on('click', function (e) {
    e.preventDefault();
    $("form input:checkbox").removeAttr('checked');
});