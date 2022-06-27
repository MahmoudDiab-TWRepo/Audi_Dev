using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eagles.LMS.BLL;
using Eagles.LMS.DTO;
using Eagles.LMS.Security;
using System;

namespace Eagles.LMS.Areas.Admission.Controllers
{

    [RouteArea("Admission")]
    [AuthorizeFilterAttribute]
    public class CategorysController : Controller
    {
        // GET: Admission/Category
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Edit()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
    }
}