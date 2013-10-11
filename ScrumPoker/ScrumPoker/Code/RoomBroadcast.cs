using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using ScrumPoker.Hubs;
using ScrumPoker.Models;

namespace ScrumPoker.Code
{
    public class RoomBroadcast
    {
        private static Lazy<IHubConnectionContext> _context = new Lazy<IHubConnectionContext>(() => GlobalHost.ConnectionManager.GetHubContext<RoomHub>().Clients);

        private static IHubConnectionContext Clients { get { return _context.Value; } }

        public static void ParticipantJoins(Participant participant)
        {
            Clients.Group(participant.RoomId).participantJoins(new { participant.ParticipantId, participant.Name, participant.Email });
        }

        public static void ParticipantLeaves(Participant participant)
        {
            Clients.Group(participant.RoomId).participantLeaves(participant.ParticipantId);
        }

        public static void NewIssue(Issue issue)
        {
            Clients.Group(issue.RoomId).newIssue(new { issue.IssueId, issue.Name, issue.Description });
        }

        public static void RevealVotes(Room room)
        {
        }
    }
}