using System;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eagles.LMS.Models
{
    public class Car
    {
        [Key]
        public int ID { get; set; }
        public EntityStatus Status { get; set; }
        public Color color { get; set; }
        public int? ColorId { get; set; }
        public string MainImage { get; set; }

        public string CarName { get; set; }

        public string Distance { get; set; }
        public string Transmission { get; set; }
        public Fuel fuel { get; set; }
        public string EnginSize { get; set; }

        public string NumSeats { get; set; }
        [NotMapped]
        public int[] Seats { get; set; }
        public string Body { get; set; }

        public string Coulor { get; set; }

        public string Price { get; set; }

        public string ShortDiscription { get; set; }

        [AllowHtml]
        public string Description { get; set; }
        [AllowHtml]
        public string Features { get; set; }


        //public string ModelYears { get; set; }




        public int ModelYear { get; set; }

        public string Power { get; set; }

        
        public DriveTrain DriveTrain { get; set; }
        public string MainImageOne { get; set; }
        public string MainImageTwo { get; set; }




        public decimal? NearestLocation { get; set; }

        public int CategoryId { get; set; }

        public int TypeID { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public virtual Category Categories { get; set; }
        [ForeignKey(nameof(TypeID))]
        public virtual Types Types { get; set; }
        public List<Equipment> Equipment { get; set; }
        //public List<ShownImages> ShownImage { get; set; }

        public List<CarImages> CarImages { get; set; }


        //public List<Color> Color { get; set; }

        [NotMapped]
        public int[] Headlights { get; set; }
        [NotMapped]
        public int[] infotainments { get; set; }
        [NotMapped]
        public int[] assistance_Systems { get; set; }



        //[Required]
        //public int Order { get; set; }

        public DateTime CreateTime { get; set; }
        public int UserCreateId { get; set; }

        public DateTime EditeTime { get; set; }
        public int UserEditId { get; set; }

    }
}