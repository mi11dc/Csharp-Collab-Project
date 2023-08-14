using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMS.Models.ViewModel
{
    public class ViewMatch
    {

        public IEnumerable<VenueDto> VenueOptions { get; set; }
        public IEnumerable<TeamDto> TeamId { get; set; }
    }
}