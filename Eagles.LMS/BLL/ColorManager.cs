using Eagles.LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eagles.LMS.BLL
{
    public class ColorManager : Reposatory<Color>
    {
        public ColorManager(LMS.Data.EmcNewsContext ctx) : base(ctx) { }

        public int GetCount(int id)
        {
            return ctx.cars.Where(s => s.ColorId == id).Count();
        }

        public List<Color> GetColorswithCount()
        {
            List<Color> colors = ctx.Color.ToList();
            foreach (var color in colors)
            {
                color.Count = GetCount(color.Id);
            }
            return colors;
        }
    }
}