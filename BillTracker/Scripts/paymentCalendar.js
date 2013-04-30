$(document).ready(function () {
    $('#calendar').fullCalendar({
        weekends: false,
        dayClick: function () {
            alert('a day has been clicked!');
        }
    });
});