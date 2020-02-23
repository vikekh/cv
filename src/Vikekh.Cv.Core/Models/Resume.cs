using System;
using System.Collections.Generic;
using System.Text;

namespace Vikekh.Cv.Core.Models
{
    public class Resume
    {
        public Basics Basics { get; set; }

        public IEnumerable<Education> Education { get; set; }

        public IEnumerable<Language> Languages { get; set; }

        public IEnumerable<Skill> Skills { get; set; }

        public IEnumerable<Work> Work { get; set; }
    }
}
