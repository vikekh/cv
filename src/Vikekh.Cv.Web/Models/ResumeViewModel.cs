using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vikekh.Cv.Core.Models;

namespace Vikekh.Cv.Web.Models
{
    public class ResumeViewModel
    {
        public ResumeViewModel(Resume model)
        {
            Basics = model.Basics;
            Education = model.Education;
            Languages = model.Languages;
            Skills = model.Skills;
            Work = model.Work;
        }

        public Basics Basics { get; set; }

        public IEnumerable<Education> Education { get; set; }

        public IEnumerable<Language> Languages { get; set; }

        public IEnumerable<Skill> Skills { get; set; }

        public IEnumerable<Work> Work { get; set; }
    }
}
