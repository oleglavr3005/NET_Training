var model = {
    view: ko.observable("welcome"),
    rsvp: {
        clientId: ko.observable(""),
        email: "",
        DateCreated: ko.observable("")
    },
    attendees: ko.observableArray([])
}
var showForm = function () {
    model.view("form");
}
var sendRsvp= function () {
    $.ajax("/api/rsvp", {
        type: "POST",
        data: {
            name: model.rsvp.clientId(),
            email: model.rsvp.email,
            DateCreated: model.rsvp.DateCreated()
        },
        success: function () {
            getAttendees();
        }
    });
}
var getAttendees = function () {
    $.ajax("/api/rsvp", {
        type: "GET",
        success: function (data) {
            model.attendees.removeAll();
            model.attendees.push.apply(model.attendees, data.map(function(rsvp) {
                return rsvp.Name;
            }));
            model.view("thanks");
        }
    });
}
$(document).ready(function () {
    ko.applyBindings();
})