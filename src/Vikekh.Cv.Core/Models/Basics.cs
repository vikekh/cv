using System;
using System.Collections.Generic;
using System.Text;

namespace Vikekh.Cv.Core.Models
{
    public class Basics
    {
        public string Email { get; set; }

        public string Image { get; set; }

        public Location Location { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public IEnumerable<Profile> Profiles { get; set; }
    }
}
