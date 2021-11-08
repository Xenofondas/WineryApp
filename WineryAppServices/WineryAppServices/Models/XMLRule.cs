using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml;

namespace WineryAppServices.Models
{
    public class XMLRule
    {
        //Primary key for XMLRule Table
        [Key]
        public int Id { get; set; }
        
        public String RuleEditorId { get; set; }

        public String xmlRule { get; set; }
    }
} 