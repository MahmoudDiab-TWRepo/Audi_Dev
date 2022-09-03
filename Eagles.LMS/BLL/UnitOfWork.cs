using Eagles.LMS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eagles.LMS.BLL
{
    public class UnitOfWork
    {


        private readonly EmcNewsContext ctx = new EmcNewsContext();
        public UserManager UserManager { get { return new UserManager(ctx); } }

        public UserForLoginReposatory UserForLoginReposatory { get { return new UserForLoginReposatory(ctx); } }

        public GroupsManager GroupsManager { get { return new GroupsManager(ctx); } }


        public GroupPriviageManager GroupPriviageManager { get { return new GroupPriviageManager(ctx); } }


        public PrivilageManager PrivilageManager { get { return new PrivilageManager(ctx); } }
        public PrivilageRouteManager PrivilageRouteManager { get { return new PrivilageRouteManager(ctx); } }
        public CategoryManager categoryManager { get { return new CategoryManager(ctx); } }
        public TypeManager typeManager { get { return new TypeManager(ctx); } }
        public CarManager carManager { get { return new CarManager(ctx); } }
        public CarImagesManager CarImagesManager { get { return new CarImagesManager(ctx); } }
        public EquipmentManager equipmentManager { get { return new EquipmentManager(ctx); } }
        public EnquiryRequistManager EnquiryRequistManager { get { return new EnquiryRequistManager(ctx); } }
        public ColorManager ColorManager { get { return new ColorManager(ctx); } }
        

        public OrderEnquiryManager OrderEnquiryManager { get { return new OrderEnquiryManager(ctx); } }
        public EnginCapacityManager EnginCapacityManager { get { return new EnginCapacityManager(ctx); } }
        public SubItemManager SubItemManager { get { return new SubItemManager(ctx); } }

        public ComparisonManager ComparisonManager { get { return new ComparisonManager(ctx); } }

    }
}