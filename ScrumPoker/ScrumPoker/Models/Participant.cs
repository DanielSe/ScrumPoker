using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScrumPoker.Models
{
    public class Participant
    {
        // Primary key
        [DataType(DataType.Text), HiddenInput(DisplayValue = false)]
        public string ParticipantId { get; set; }

        // Foreign key
        [DataType(DataType.Text), HiddenInput(DisplayValue = false)]
        public string RoomId { get; set; }

        [DataType(DataType.Text)]
        public string Name { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Text)]
        public string Vote { get; set; }

        // Navigation property
        public virtual Room Room { get; set; }
    }
}