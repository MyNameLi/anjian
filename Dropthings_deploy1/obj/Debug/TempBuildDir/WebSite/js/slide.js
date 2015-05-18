//menu
$(document).ready(function() {

    $('li.menu_item').mousemove(function() {
        $(this).find('div').slideDown(50);
    });
    $('li.menu_item').mouseleave(function() {
        $(this).find('div').slideUp(50);
    });

    $('#website').mousemove(function() {
        $(this).find('div').slideDown();
    });
    $('#website').mouseleave(function() {
        $(this).find('div').slideUp("fast");
    });

});
