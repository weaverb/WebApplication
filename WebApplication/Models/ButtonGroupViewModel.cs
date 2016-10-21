using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class ButtonGroupViewModel
    {
        public string Message { get; set; }
        public string CurrentUser { get; set; }
        public string CurrentRole { get; set; }
        public List<string> Buttons { get; set; }
    }
}