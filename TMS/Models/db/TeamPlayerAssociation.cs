using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TMS.Models
{
    public class TeamPlayerAssociation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Foriegn Key
        public int TeamId { get; set; }
        [ForeignKey("TeamId")]
        public virtual Team TeamDetail { get; set; }
        public int PlayerId { get; set; }
        [ForeignKey("PlayerId")]
        public virtual UserDetail PlayerDetail { get; set; }
    }
}