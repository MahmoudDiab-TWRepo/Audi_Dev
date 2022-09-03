using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Eagles.LMS.Models
{

    [Table("EnginCapacity")]

    public class EnginCapacity
    {
        public int Id { get; set; }


        public string Name { get; set; }
        public int CategoryID { get; set; }

    }

}