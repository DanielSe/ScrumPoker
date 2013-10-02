using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScrumPoker.Models
{
    public class Participant
    {
        public string ParticipantId { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
    }
}