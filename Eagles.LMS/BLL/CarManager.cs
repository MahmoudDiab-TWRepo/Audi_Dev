using Eagles.LMS.Data;
using Eagles.LMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Eagles.LMS.BLL
{
    public class CarManager:Reposatory<Car>
    {
        //protected EmcNewsContext ctx;

        DbSet<Car> Sets;
        public CarManager(EmcNewsContext ctx) : base(ctx)
        {
            Sets = ctx.Set<Car>();
        }
        public  List<Car> GetCarwithEquipments()
        {
            return Sets.Include(s => s.Equipment).Include(s=>s.ShownImage).ToList();
        }


    }
    }
