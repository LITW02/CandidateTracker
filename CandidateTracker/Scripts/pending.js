﻿$(function() {
    $("#confirm-button").on('click', function() {
        setStatus(1);
    });

    $("#refuse-button").on('click', function () {
        setStatus(2);
    });

    function setStatus(status) {
        var id = $("#candidateId").val();
        $("#buttons button").prop('disabled', true);
        $.post("/home/updatestatus", { id: id, status: status }, function() {
            $.get('/home/getcounts', function(counts) {
                $("#pending-count").text(counts.Pending);
                $("#confirmed-count").text(counts.Confirmed);
                $("#refused-count").text(counts.Refused);
                $("#buttons").hide();
            });
        });
    }
});