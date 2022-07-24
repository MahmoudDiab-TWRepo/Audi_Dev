using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Eagles.LMS.BLL;
using Eagles.LMS.DTO;
using Eagles.LMS.Models;
using Newtonsoft.Json;

namespace Eagles.LMS.Controllers
{
    public class HomeController : Controller
    {
        private UnitOfWork ctx = new UnitOfWork();
        // GET: Home
        public ActionResult Index()
        {
            Car data = TempData["mydata"] as Car;
            ViewBag.Allcars = data;
            //return Redirect("/Admission");
            return View();
        }
        [HttpPost]
        public JsonResult Carfilter(CarModel carmodel)
        {
            UnitOfWork ctx = new UnitOfWork();
            List<Car> cars = new List<Car>();
            //Car journalMaster = Serializer.Deserialize<Ac_JournalMaster>(master);
            //List<Ac_JournalDetails> journalDetails = serilizer.Deserialize<List<Ac_JournalDetails>>(details);
            if (carmodel.CarType != null && carmodel.CarType.Length > 0)
            {
                foreach (var item in carmodel.CarType)
                {
                    var itemcar = ctx.carManager.GetAllBind().FirstOrDefault(s => s.TypeID == item);
                    if (itemcar != null)
                    {
                        cars.Add(itemcar);
                    }
                }
            }
           if (carmodel.CarCategory != null && carmodel.CarCategory.Length > 0)
                {
                foreach (var itemcat in carmodel.CarCategory)
                {
                    var itemcar = ctx.carManager.GetAllBind().FirstOrDefault(s => s.CategoryId == itemcat);
                    if (itemcar != null)
                    {
                        cars.Add(itemcar);
                    }
                }
            
            }
            var DIstinctcars = cars.ToList().Distinct();
            if(carmodel.CarCategory==null&&carmodel.CarType==null)
            {
                DIstinctcars  = ctx.carManager.GetCarwithEquipments().ToList();

            }
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string CarsList = JsonConvert.SerializeObject(DIstinctcars, Formatting.None, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });


            TempData["mydata"] = DIstinctcars;
            return Json(CarsList, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Price()
        {

            //return Redirect("/Admission");
            return View();
        }
        public ActionResult Engine()
        {

            //return Redirect("/Admission");
            return View();
        }
        public ActionResult Equipment()
        {

            return View();
        }

        public ActionResult Colour()
        {

            return View();
        }

        public ActionResult Form1()
        {

            return View();
        }
        public ActionResult Results()
        {

            return View();
        }
        public ActionResult CarDetails()
        {

            return View();
        }



        public ActionResult ChangeLanguage(string SelectedLanguage, string redirect)
        {

            var controller = RouteData.Values["controller"].ToString();
            var action = RouteData.Values["action"].ToString(); ;
            if (SelectedLanguage != null)
            {
                Thread.CurrentThread.CurrentCulture =
                    CultureInfo.CreateSpecificCulture(SelectedLanguage);
                Thread.CurrentThread.CurrentUICulture =
                    new CultureInfo(SelectedLanguage);
                var cookie = new HttpCookie("Language");
                cookie.Value = SelectedLanguage;
                Response.Cookies.Add(cookie);
            }
            if (redirect == null)
                redirect = "/";
            return Redirect(redirect);
        }

        public ActionResult Changethems(string redirect)
        {

            var controller = RouteData.Values["controller"].ToString();
            var action = RouteData.Values["action"].ToString(); ;


            var cookie = Request.Cookies["thems"];
            if (cookie == null)
            {
                cookie = new HttpCookie("thems");
                cookie.Value = "default";
            }
            else
            {
                cookie.Value = (cookie.Value == "dark") ? "default" : "dark";
            }
            Response.Cookies.Add(cookie);
            if (redirect == null)
                redirect = "/";
            return Redirect(redirect);
        }





        public ActionResult indextwo()
        {
            return View();
        }

        public ActionResult indexhreee()
        {
            return View();
        }


    }
}