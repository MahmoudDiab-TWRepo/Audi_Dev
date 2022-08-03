using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eagles.LMS.Models
{
    public class Category
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public  ICollection<Types> Types { get; set; }

        public ICollection<SubItem> SubItem { get; set; }
        public  ICollection<Car> Cars { get; set; }
        public string MainImageOne { get; set; }
        public string MainImageTwo { get; set; }


        public bool ShowInBodyType { get; set; }

        //public List<ServiceImages> ServiceImages { get; set; }

        //public DateTime CreateTime { get; set; }
        //public int UserCreateId { get; set; }

        //public DateTime EditeTime { get; set; }
        //public int UserEditId { get; set; }

        //public DateTime CreateTime { get; set; }
        //public int UserCreateId { get; set; }

        //public DateTime EditeTime { get; set; }
        //public int UserEditId { get; set; }

    }
}