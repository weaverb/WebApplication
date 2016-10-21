using System.Collections.Generic;

namespace WebApplication.Models
{
    public class FancyButtonGroupViewModel
    {
        public string Message { get; set; }
        public string CurrentUser { get; set; }
        public string CurrentRole { get; set; }
        public List<Button> Buttons { get; set; }
    }

    public class Button
    {
        public string Name { get; set; }
        public string Style { get; set; }

    }
}