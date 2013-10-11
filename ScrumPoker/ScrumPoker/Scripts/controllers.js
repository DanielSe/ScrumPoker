function RoomAdminController() {
    this.participants = ko.observableArray();
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