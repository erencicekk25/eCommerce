// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function getData() {
    $.ajax({
        type: "GET",
        url: "http://localhost:5270/api/Home",
        success: function (data) {
            $(".apiData").empty();
            $(".apiData").append("<ul class='list-group'>");
            $.each(data, function (i, v) {
                $(".apiData").append("<li class='list-group-item'>"+v.name+"</li>");
            });
            $(".apiData").append("</ul>");
        },
        error: function (e) {
            alert(e.responseText)
        }
    });
}
