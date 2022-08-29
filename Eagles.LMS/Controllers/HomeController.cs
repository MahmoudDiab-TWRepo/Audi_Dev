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

using System.IO;
using System.ComponentModel.DataAnnotations.Schema;
using Eagles.LMS.Helper;

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
            //return this.PartialView("_SearchKeys", data);
        }
        public ActionResult _Results()
        {
            return PartialView();
        }
        public ActionResult _SearchKeys()
        {
            return PartialView();
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
                    var itemcars = ctx.carManager.GetAllBind().Where(s => s.TypeID == item);
                    if (itemcars != null)
                    {
                        foreach (var itemcar in itemcars)
                        {
                            if (itemcar != null)
                            {
                                cars.Add(itemcar);
                            }
                        }
                    }

                }

            }
            else if (carmodel.CarCategory != null && carmodel.CarCategory.Length > 0)
            {
                foreach (var itemcat in carmodel.CarCategory)
                {
                    var itemcars = ctx.carManager.GetAllBind().Where(s => s.CategoryId == itemcat);
                    if (itemcars != null)
                    {
                        foreach (var itemcar in itemcars)
                        {
                            if (itemcar != null)
                            {
                                cars.Add(itemcar);
                            }
                        }
                    }

                }

            }
            else
            {
                return null;
            }

            var DIstinctcars = cars.ToList().Distinct();

            JavaScriptSerializer jss = new JavaScriptSerializer();
            string CarsList = JsonConvert.SerializeObject(DIstinctcars, Formatting.None, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });


            TempData["mydata"] = DIstinctcars;
            return Json(CarsList, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult CarfilterType(CarModel carmodel)
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
            if (carmodel.CarCategory == null && carmodel.CarType == null)
            {
                DIstinctcars = ctx.carManager.GetCarwithEquipments().ToList();

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
        public ActionResult makeEnquiry()
        {

            return View();
        }

        public ActionResult OrderAudi()
        {
            return View();
        }
        public ActionResult Results()
        {

            return View();
        }




        public ActionResult CarDetails(int? id)
        {
            var _car = new Car();

            bool en = true;

            if (Request.Cookies["Language"] != null)
            {
                en = (Request.Cookies["Language"].Value.ToString() == "en") ? true : false;
            }
            if (en == true)
            {
                _car = new UnitOfWork().carManager.GetAll().Where(s => s.CarName != null).FirstOrDefault(s => s.ID == id);

            }

            // Expression<Func<T, object>> criteria
            else
            {
                _car = new UnitOfWork().carManager.GetAll().Where(s => s.CarName != null).FirstOrDefault(s => s.ID == id);
            }
            if (_car == null)
                return View("NotFound");
            _car.CarImages = new UnitOfWork().CarImagesManager.GetAllBind().Where(s => s.CarId == _car.ID).ToList();

            //return Redirect("/Admission");
            TempData["ID"] = _car.ID;

            ViewBag.colorID = _car.ColorId;
            //TempData["ColorID"] = _car.ColorId;
            //return RedirectToAction("EnquiryForm");
            return View(_car);
            //return result;
            //return View();
        }
        public ActionResult EnquiryForm(int? id)
        {
            if (id != null)
                ViewBag.Id = id;
            return View();
        }
        [HttpPost]
        public ActionResult EnquiryForm(EnquiryRequist enquiryRequist, HttpPostedFileBase uploadattachments)
        {
            ActionResult result = View(enquiryRequist);

            if (ModelState.IsValid)
            {
                RequestStatus requestStatus;
                if (uploadattachments != null)
                {
                    requestStatus = new ManageRequestStatus().GetStatus(Status.GeneralError, "Plz Upload The Attachment");
                }
                else
                {
                    var ctx = new UnitOfWork();
                    enquiryRequist.CreatedTime = DateTime.Now;
                    ctx.EnquiryRequistManager.Add(enquiryRequist);
                    requestStatus = new ManageRequestStatus().GetStatus(Status.Created);

                    try
                    {
                        SendEmail sendEmail = new SendEmail();
                        sendEmail.SendMail(new EmailDTO
                        {
                            To = "To Email",
                            Message = "<h1 style='font-size:25px; line-height:1.5'>New Car Enquiry Request</h1>"
                            + "<p style='font-size:15px; color: #000'>Thank You for Enquiry This Car</p>" + "<br />"
                            + "<b style='font-size:12px; line-height:1.5'>Car Name :</b>" + enquiryRequist.CarID + "<br />"
                            + "<b style='font-size:12px; line-height:1.5'>Car Code :</b>" + enquiryRequist.CarCode + "<br />"
                            + "<b style='font-size:12px; line-height:1.5'>First Name :</b>" + enquiryRequist.FirstName + "<br />"
                            + "<b style='font-size:12px; line-height:1.5'>Last Name :</b>" + enquiryRequist.LastName + "<br />"
                            + "<b style='font-size:12px; line-height:1.5'>Email :</b>" + enquiryRequist.Email + "<br />"
                            + "<b style='font-size:12px; line-height:1.5'>Phone :</b>" + enquiryRequist.Mobile + "<br />"

                            + "<b style='font-size:12px; line-height:1.5'>Message:</b>" + enquiryRequist.Message + "<br />" +
                            "<br />",
                            From = "web@empcnews.com",
                            Subject = "New Contact Us"
                        }, "Contact", enquiryRequist.Email);
                    }
                    catch (Exception ex)
                    {

                    }

                    return Redirect("/Home/ThanksPage");

                }
                TempData["RequestStatus"] = requestStatus;
            }
            return result;


        }


        public ActionResult ThanksPage()
        {

            return View();
        }




        public int GetUserId()
        {
            var userFromSesstion = HttpContext.Session["User_Key"];
            return Convert.ToInt32(userFromSesstion);
        }


        public ActionResult Comparison()
        {

            return View();
        }

        public ActionResult AddComparison()
        {

            return View();
        }
        [HttpPost]
        public ActionResult AddComparison(Comparison addsnews, int carID)
        {

            ActionResult result = View(addsnews);

            if (ModelState.IsValid)
            {

                RequestStatus requestStatus;
                int userId = GetUserId();
                addsnews.UserId = userId;
                addsnews.CarID = carID;

                var ctx = new UnitOfWork();
                addsnews = ctx.ComparisonManager.Add(addsnews);

                var user = ctx.UserManager.GetById(GetUserId());


                requestStatus = new ManageRequestStatus().GetStatus(Status.Created);
                result = RedirectToAction(nameof(Index));

                TempData["RequestStatus"] = requestStatus;



            }
            return result;

        }

        [HttpPost]
        public ActionResult DeleteComparison(int id)
        {
            UnitOfWork ctx = new UnitOfWork();
            var entity = ctx.ComparisonManager.GetBy(id);

            var user = ctx.UserManager.GetById(GetUserId());



            ctx.ComparisonManager.Delete(entity);
            return Json(JsonRequestBehavior.AllowGet);
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