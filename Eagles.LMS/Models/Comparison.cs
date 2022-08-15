using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eagles.LMS.Models
{
    [Table("Comparison")]
    public class Comparison
    {
        public int Id { get; set; }
        public int CarID { get; set; }

        //public List<Car> Car { get; set; }
        public int UserId { get; set; }
    }
}