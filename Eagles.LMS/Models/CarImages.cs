using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Eagles.LMS.Models
{
    [Table("CarImages")]
    public class CarImages

    {
        public int Id { get; set; }

        public string Path { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }


    }

}