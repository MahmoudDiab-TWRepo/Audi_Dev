using Eagles.LMS.Data;
using Eagles.LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eagles.LMS.BLL
{
    public class SubItemManager : Reposatory<SubItem>
    {
        public SubItemManager(EmcNewsContext ctx) : base(ctx)
        {

        }
        public int GetCount(int id)
        {
            return ctx.cars.Where(s => s.SubItem_Id == id).Count();
        }

        public List<SubItem> GetSubItemswithCount()
        {
            List<SubItem> subItems = ctx.SubItem.ToList();
            foreach (var subItem in subItems)
            {
                subItem.Count = GetCount(subItem.Id);
            }
            return subItems;
        }
    }
}