using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Eagles.LMS.Models
{
    [Table("Color")]
    public class Color

    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageColor { get; set; }
        //public Car Car { get; set; }
        //public int? CarId { get; set; }


    }

}