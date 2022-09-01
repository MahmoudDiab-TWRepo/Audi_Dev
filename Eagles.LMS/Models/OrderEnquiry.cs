using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Eagles.LMS.Models
{

    [Table("OrderEnquiry")]

    public class OrderEnquiry
    {
        public int Id { get; set; }

        public Boolean ImAnOudiowner { get; set; }
        public Boolean IWantToBeAnOudiowner { get; set; }

        public string FullName { get; set; }
        public string MobileNumber { get; set; }
        public string EmailAddress { get; set; }
        public string OldCarModel { get; set; }
        public string OldEnginCapacity { get; set; }
        public string OldModelYear { get; set; }
        public string OldMileage { get; set; }
        public string OldComment { get; set; }
        public string ChassisNumber { get; set; }



        public string CarModel { get; set; }
        public string EnginCapacity { get; set; }
        public string ModelYear { get; set; }
        public string Mileage { get; set; }
        public string Comment { get; set; }



        public string CarCode { get; set; }
        public string CarID { get; set; }
        public DateTime Sendtime { get; set; }


    }

}