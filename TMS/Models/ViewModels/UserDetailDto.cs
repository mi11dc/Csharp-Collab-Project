using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMS.Models
{
    public class UserDetailDto
    {
        public int Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public decimal BasePrice { get; set; }
        public string sDOB { get; set; }
        public DateTime? DOB { get; set; }
        public string Country { get; set; }
        public string UserId { get; set; }
    }
}