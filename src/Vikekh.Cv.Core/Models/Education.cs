using System;
using System.Collections.Generic;
using System.Text;

namespace Vikekh.Cv.Core.Models
{
    public class Education
    {
        public DateTime? EndDate { get; set; }

        public string Institution { get; set; }

        public DateTime StartDate { get; set; }

        public string StudyType { get; set; }
    }
}
