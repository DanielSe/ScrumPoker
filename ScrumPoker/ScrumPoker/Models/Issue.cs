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
        public Issue()
        {
        }

        [DataType(DataType.Text), HiddenInput(DisplayValue = false)]
        public string IssueId { get; set; }

        [DataType(DataType.Text)]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public Room Room { get; set; }
    }
}