using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace ScrumPoker.Models
{
    public class Room
    {
        public Room()
        {
            Participants = new Collection<Participant>();
            Issues = new Collection<Issue>();
        }

        // Primary key
        [DataType(DataType.Text), Display(Name = "Room ID"), HiddenInput(DisplayValue = false)]
        public string RoomId { get; set; }

        [DataType(DataType.Text), Display(Name = "Room Admin ID"), HiddenInput(DisplayValue = false)]
        public string RoomAdminId { get; set; }

        [DataType(DataType.Text), Required, Display(Name = "Name")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText), Display(Name = "Description")]
        public string Description { get; set; }

        [DataType(DataType.Text), Required, Display(Name = "Vote sizes", Description = "Comma separated list of allowed vote sizes.")]
        public string VoteSizeSetting { get; set; }

        [DataType(DataType.EmailAddress), Required, Display(Name = "Administrator email")]
        public string AdminEmail { get; set; }

        [DataType(DataType.Text), HiddenInput(DisplayValue = false)]
        public string CurrentIssueId { get; set; }

        // Navigation property
        public virtual ICollection<Participant> Participants { get; set; }
        
        // Navigation property
        public virtual ICollection<Issue> Issues { get; set; }

        public string[] VoteSizes { get { return VoteSizeSetting.Split(',').Select(x => x.Trim()).ToArray(); } }
        public Issue CurrentIssue { get { return Issues.FirstOrDefault(x => x.IssueId == CurrentIssueId); } }
    }
}