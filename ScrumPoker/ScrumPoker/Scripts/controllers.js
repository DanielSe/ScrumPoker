function RoomAdminController() {
    this.roomId = ko.observable();
    this.roomAdminId = ko.observable();
    this.participants = ko.observableArray();

    this.addParticipant = function (participant) {
        var self = this;
        
        participant.kick = function() {
            var rid = self.roomAdminId();
            var pid = this.participantId();

            $.getJSON("/api/admin/" + rid + "/kickparticipant?participantId=" + pid).done(function(data) {
                if (data == "OK") {
                    self.removeParticipant(pid);
                }
            });
        };

        this.participants.push(participant);
    };

    this.removeParticipant = function(participantId) {
        this.participants.removeAll(function(item) { return item.participantId == participantId; });
    };
}

function RoomClientController() {
    this.participants = ko.observableArray();
    this.currentIssue = ko.observable();
}


function Participant(simple) {
    this.participantId = ko.observable();
    this.name = ko.observable();
    this.email = ko.observable();
    
    if (simple) {
        this.participantId(simple.ParticipantId);
        this.name(simple.Name);
        this.email(simple.Email);
    }
}

function Issue(simple) {
    this.issueId = ko.observable();
    this.name = ko.observable();
    this.description = ko.observable();
    
    if (simple) {
        this.issueId(simple.IssueId);
        this.name(simple.Name);
        this.description(simple.Description);
    }
}