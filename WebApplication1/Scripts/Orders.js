var model = {
    view: ko.observable(""),
    rsvp: {
        Id: ko.observable(""),
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
    $.ajax("/Home/Index", {
        type: "POST",
        data: {
            Id: model.rsvp.Id(),
            clientId: model.rsvp.clientId(),
            email: model.rsvp.email,
            DateCreated: model.rsvp.DateCreated()
        },
        success: function () {
            getAttendees();
        }
    });
}
var getAttendees = function () {
    $.ajax("/Home/Index", {
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