using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Vikekh.Cv.Web.Models
{
    public class Basics
    {
        public string Address { get; set; }

        public string City { get; set; }

        public string CountryCode { get; set; }

        public string Email { get; set; }

        public string Label { get; set; }

        public string Image { get; set; }

        public string Location { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string PostalCode { get; set; }

        public IEnumerable<Profile> Profiles { get; set; }

        public string Region { get; set; }

        public string Summary  { get; set; }

        public string Url { get; set; }
    }
}
