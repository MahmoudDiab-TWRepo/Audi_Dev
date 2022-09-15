using Eagles.LMS.Data;
using Eagles.LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eagles.LMS.BLL
{
    public class TypeManager:Reposatory<Types>
    {
        public TypeManager(EmcNewsContext ctx) : base(ctx)
        {

        }
        public int GetCount(int id)
        {
            return ctx.cars.Where(s => s.TypeID == id).Count();
        }

        public List<Types> GetTypeswithCount()
        {
            List<Types> types = ctx.types.ToList();
            foreach (var type in types)
            {
                type.Count = GetCount(type.ID);
            }
            return types;
        }
    }
}