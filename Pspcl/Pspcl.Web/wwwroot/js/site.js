// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).on('click', '.user-role-option', function (event) {
    var chosenRole = $(this).text();   

    $('#choosenUserRole').attr("value", chosenRole);
   
});

$(document).on('submit', '#UserForm', function (event) {
    event.preventDefault();
    var isRoleChoosen = $('#choosenUserRole').val();


    if (isRoleChoosen) {
        this.submit();
    } else {
        alert("Please select the User-Role..!");
    }

});