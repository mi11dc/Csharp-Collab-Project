using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMS.Models
{
    public class UpdateMatch
    {
        public MatchDto SelectedMatch { get; set; }

        public IEnumerable<VenueDto> VenueOptions { get; set; }
    }
}