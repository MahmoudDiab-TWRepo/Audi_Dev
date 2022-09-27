using Eagles.LMS.BLL;
using Eagles.LMS.Models;
using Eagles.LMS.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eagles.LMS.Areas.Admission.Controllers
{

    [RouteArea("Admission")]
    [AuthorizeFilterAttribute]
    public class OrderEnquiryController : Controller
    {
        // GET: Admission/OrderEnquiry
        public ActionResult Index()
        {
            return View();

        }
        public ActionResult indexNewClient()
        {
            return View();

        }

        
    }
}