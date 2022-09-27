using Eagles.LMS.Data;
using Eagles.LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eagles.LMS.BLL
{
    public class OrderEnquiryManager : Reposatory<OrderEnquiry>
    {
        public OrderEnquiryManager(EmcNewsContext ctx) : base(ctx)
        {

        }

        public string GetCarModel(int id)
        {
            return ctx.Categories.Where(s => s.ID == id).FirstOrDefault().Name;
        }
        public string GetEngine(int id)
        {
            return ctx.EnginCapacity.Where(s => s.Id == id).FirstOrDefault().Name;
        }

        public List<OrderEnquiry> GetOrderEnquirieswithData()
        {
            List<OrderEnquiry> orderEnquiries = ctx.OrderEnquiry.ToList();
            foreach (var orderEnquiry in orderEnquiries)
            {
                if (orderEnquiry.OldCarModel != null)
                orderEnquiry.OldCarModelName = GetCarModel(Convert.ToInt32(orderEnquiry.OldCarModel));

                if (orderEnquiry.OldEnginCapacity != null)
                    orderEnquiry.OldEnginCapacityName = GetEngine(Convert.ToInt32(orderEnquiry.OldEnginCapacity));

                if (orderEnquiry.CarModel != null)
                    orderEnquiry.CarModelName = GetCarModel(Convert.ToInt32(orderEnquiry.CarModel));

                if (orderEnquiry.EnginCapacity != null)
                    orderEnquiry.EnginCapacityName = GetEngine(Convert.ToInt32(orderEnquiry.EnginCapacity));
            }
            return orderEnquiries;
        }
    }
}

