"use strict";

// Select2 component
$(".select2").select2();

// Allow future dates only, starting one day in the future
$('.datepicker').Zebra_DatePicker();

// Data table
$("#example").DataTable();

// Countdown timer
let secondsRemaining = 120;
const startCountDown = function () {
    secondsRemaining--;

    if (secondsRemaining <= 0) {
        window.location.href = "../Default.aspx";
    }
    //console.log(secondsRemaining);
}

//setInterval(startCountDown, 1000);

