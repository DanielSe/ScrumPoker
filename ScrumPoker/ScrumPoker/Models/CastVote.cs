using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScrumPoker.Models
{
    public class CastVote
    {
        public string IssueId { get; set; }
        public string ParticipantId { get; set; }

        public string Vote { get; set; }
    }
}