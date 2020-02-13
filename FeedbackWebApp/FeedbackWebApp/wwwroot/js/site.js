// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

//document.getElementById("Overview").addEventListener('click', loadPlaces);

function loadPlaces() {
    var url = "http://77.244.251.110/api/Places";
    var xhr = new XMLHttpRequest();
    xhr.open('GET', url);
    //xhr.setRequestHeader("Content-Type", "application/json");
    xhr.send();
    xhr.onload = function () {
        //arr = xhr.response;
        alert(xhr.response);
    }
    //arr.forEach(item => console.log(item));
}
function LogIn() {
    location.href = location.href + '/Overview';
}