using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Eagles.LMS.Models
{
    public class Equipment
    {
        [Key]
        public int ID { get; set; }
        public Assistance_systems assistance_Systems { get; set; }
        public Infotainment infotainments { get; set; }
        public Headlights Headlights { get; set; }
        public Seats Seats { get; set; }
        public bool? interior { get; set; }
        public bool? exterior { get; set; }
        public int CarId { get; set; }
        [ForeignKey(nameof(CarId))]
        public virtual Car Car { get; set; }
    }
}