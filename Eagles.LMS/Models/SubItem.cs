using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Eagles.LMS.Models
{
    [Table("SubItem")]
    public class SubItem

    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MainImageOne { get; set; }
        public string MainImageTwo { get; set; }
        public ICollection<Car> Cars { get; set; }
        public int? CategoryID { get; set; }
        [ForeignKey(nameof(CategoryID))]
        public virtual Category Categories { get; set; }
        public int? TypeID { get; set; }
        [ForeignKey(nameof(TypeID))]
        public virtual Types Types { get; set; }


    }

}