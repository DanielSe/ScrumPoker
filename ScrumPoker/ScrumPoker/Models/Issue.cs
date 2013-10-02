using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MS.Internal.Xml.XPath;

namespace ScrumPoker.Models
{
    public class Issue
    {
        public Issue()
        {
            CastVotes = new List<CastVote>();
        }

        public string IssueId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public List<CastVote> CastVotes { get; private set; } 
    }
}