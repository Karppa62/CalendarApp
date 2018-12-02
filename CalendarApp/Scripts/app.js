var ViewModel = function () {
    var self = this;
    self.events = ko.observable();
    self.error = ko.observable();
    self.success = ko.observable();

    self.newEvent = {
        EventName: ko.observable(),
        DateAndTime: ko.observable(),
        Duration: ko.observable()
    }

    //self.searchValues = {
    //    Year: ko.observable()
    //}


    //self.searchEvents = function (formElement) {
    //    var search = {
    //        Year: self.searchValues.Year()
    //    };

    //    ajaxHelper(eventUri, 'GET', search).done(function (data) {
    //        self.events(data)
    //    });
    //}

    var eventUri = '/api/event/';


    self.addEvent = function (formElement) {
        var event = {
            EventName: self.newEvent.EventName(),
            DateAndTime: self.newEvent.DateAndTime(),
            Duration: self.newEvent.Duration()
        };

        ajaxHelper(eventUri, 'POST', event).done(function (jqXHR, textStatus) {
            self.success("New event added")
            getEvents();
        });
    }

    function getEvents() {
        ajaxHelper(eventUri, 'GET').done(function (data) {
            self.events(data);
        });
    }

    function ajaxHelper(uri, method, data) {
        self.error(''); // Clear error message
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }

    getEvents();
};

ko.applyBindings(new ViewModel());