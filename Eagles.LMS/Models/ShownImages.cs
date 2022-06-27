using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Eagles.LMS.Models
{
    public class ShownImages
    {
        [Key]
            public int Id { get; set; }

            public string Path { get; set; }
            public int CarId { get; set; }
        [ForeignKey(nameof(CarId))]
            public virtual Car car { get; set; }
        
    }
}