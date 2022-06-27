using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Eagles.LMS.Models
{
    public class Types
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public int? CategoryID { get; set; }
        //public ICollection<DynamicFeatures> DynamicFeatures { get; set; }
        public  ICollection<Car> Cars { get; set; }
        public string MainImageOne { get; set; }
        public string MainImageTwo { get; set; }
        [ForeignKey(nameof(CategoryID))]
        public virtual Category Categories { get; set; }
    }
}