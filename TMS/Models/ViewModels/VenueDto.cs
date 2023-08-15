using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMS.Models.ViewModel
{
    public class VenueDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal BasePrice { get; set; }
        public string Location { get; set; }
        public string Country { get; set; }
    }
}