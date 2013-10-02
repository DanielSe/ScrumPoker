using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScrumPoker.Models
{
    public class Room
    {
        public Room()
        {
            Participants = new List<Participant>();
        }

        public string RoomId { get; set; }
        public string RoomAdminId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string[] VoteSizes { get; set; }

        public List<Participant> Participants { get; private set; }
        public Issue CurrentIssue { get; set; }
    }
}