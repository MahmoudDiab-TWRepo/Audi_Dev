using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Eagles.LMS.Models;
using Eagles.LMS.DTO;

namespace Eagles.LMS.Data
{
    public class EmcNewsContext : DbContext
    {
        public EmcNewsContext() : base("EmcNewsConnection")
        {

        }

 

        public DbSet<User> Users { get; set; }
        public DbSet<Privilage> Privilages { get; set; }    
        public DbSet<PrivilageRoute> PrivilageRoutes { get; set; }
        public DbSet<GroupPriviage> GroupPriviages { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Types> types { get; set; }

        public DbSet<Car> cars { get; set; }
        public DbSet<Color> Color { get; set; }

        public DbSet<SubItem> SubItem { get; set; }

        public DbSet<Comparison> Comparison { get; set; }

        public DbSet<EnquiryRequist> EnquiryRequist { get; set; }
        public DbSet<CarImages> CarImages { get; set; }
        public DbSet<Equipment> Equipments { get; set; }







        public DbSet<Group> Groups { get; set; }

        public DbSet<UserForLogin> UserForLogins { get; set; }







    }
}