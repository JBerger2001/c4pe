// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

//document.getElementById("Overview").addEventListener('click', loadPlaces);

var user = 'unknown';

function LogIn() {
    user = 'Franzi';    // Benutzername
    window.location.replace("http://localhost:58115/Overview");
}
function LogInGuest() {
    user = 'unknown';
    window.location.replace("http://localhost:58115/Overview");
}

function CheckUser() {
    if (user == "Franzi") {
        document.getElementById("btnAddPlace").style.visibility = 'visible';
    }
    else if(user=="unknown"){
        document.getElementById("btnAddPlace").style.visibility = 'hidden';
    }
}
function AddPlace(){
    window.location.replace("http://localhost:58115/AddPlace");
}
function CreateNewPlace() {
    window.location.replace("http://localhost:58115/Overview");
    alert("New place created!")
}