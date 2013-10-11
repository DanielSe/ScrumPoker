using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MS.Internal.Xml.XPath;
using System.Web.Mvc;

namespace ScrumPoker.Models
{
    public class Issue
    {
        // Primary key
        [DataType(DataType.Text), HiddenInput(DisplayValue = false)]
        public string IssueId { get; set; }

        [DataType(DataType.Text)]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        // Foreign key
        [DataType(DataType.Text), HiddenInput(DisplayValue = false)]
        public string RoomId { get; set; }

        // Navigation property
        public virtual Room Room { get; set; }
    }
}