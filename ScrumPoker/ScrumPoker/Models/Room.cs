using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace ScrumPoker.Models
{
    public class Room
    {
        public Room()
        {
            Participants = new List<Participant>();
            Issues = new List<Issue>();
        }

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

        public List<Participant> Participants { get; private set; }
        public Issue CurrentIssue { get; set; }
        public List<Issue> Issues { get; private set; }

        public string[] VoteSizes { get { return VoteSizeSetting.Split(',').Select(x => x.Trim()).ToArray(); } }
    }
}