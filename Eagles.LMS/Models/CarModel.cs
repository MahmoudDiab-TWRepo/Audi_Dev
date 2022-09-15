using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eagles.LMS.Models
{
    public class CarModel
    {
        public int[] CarCategory { get; set; }
        public int[] CarSubItems { get; set; }
        public int[] CarType { get; set; }
        public int[] EngineType { get; set; }
        public int[] CarEquipments { get; set; }
        public int[] CarColours { get; set; }

    }
}