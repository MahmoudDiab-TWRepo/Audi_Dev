using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Eagles.LMS.Models
{

    [Table("EnquiryRequist")]

    public class EnquiryRequist
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }

        public string caerrnamw { get; set; }
        public string Message { get; set; }
        public Boolean BookMe { get; set; }
        public string CarID { get; set; }

        public DateTime CreatedTime { get; set; }


    }

}