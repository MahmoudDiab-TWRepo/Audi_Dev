using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Eagles.LMS.Models
{
    public class Car
    {
        [Key]
        public int ID { get; set; }
        public Color color { get; set; }

        public decimal Price { get; set; }

        public int ModelYear { get; set; }

        public decimal Power { get; set; }

        public Fuel fuel { get; set; }
        public DriveTrain DriveTrain { get; set; }
        public string MainImageOne { get; set; }
        public string MainImageTwo { get; set; }


        public string Description { get; set; }
        public decimal? NearestLocation { get; set; }

        public int CategoryId { get; set; }

        public int TypeID { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public virtual Category Categories { get; set; }
        [ForeignKey(nameof(TypeID))]
        public virtual Types Types { get; set; }
        public List<Equipment> Equipment { get; set; }
        public List<ShownImages> ShownImage { get; set; }



        [NotMapped]
        public int[] Seats { get; set; }
        [NotMapped]
        public int[] Headlights { get; set; }
        [NotMapped]
        public int[] infotainments { get; set; }
        [NotMapped]
        public int[] assistance_Systems { get; set; }

    }
}