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
        public  ICollection<Car> Cars { get; set; }
        public string MainImageOne { get; set; }
        public string MainImageTwo { get; set; }
    }
}