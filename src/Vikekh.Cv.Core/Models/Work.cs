using System;
using System.Collections.Generic;

namespace Vikekh.Cv.Core.Models
{
    public class Work
    {
        public string Description { get; set; }

        public DateTime? EndDate { get; set; }

        public IEnumerable<string> Highlights { get; set; }

        public string Location { get; set; }

        public string Name { get; set; }

        public string Position { get; set; }

        public DateTime StartDate { get; set; }

        public string Summary { get; set; }
    }
}
