// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Wait for the document to be ready
$(document).ready(function () {
    // Define a click event for the "Create Appointment" button
    $("#openModalButton").click(function () {
        // Show the modal when the button is clicked
        $("#myModal").modal("show");
    });
});

const passwordInput = document.getElementById('passwordInput');
const showPasswordCheckbox = document.getElementById('showPassword');

showPasswordCheckbox.addEventListener('change', function () {
    if (this.checked) {
        passwordInput.type = 'text';
    } else {
        passwordInput.type = 'password';
    }
});