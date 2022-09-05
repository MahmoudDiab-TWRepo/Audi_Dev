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
using System.Web.DynamicData;

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

            Session["leftRange"] = 2005;
            Session["rightRange"] = 2022;
           

            return View();
        }
        public ActionResult _Results()
        {
            return PartialView();
        }
        public ActionResult _SearchKeys()
        {
            //CarModel dataModel = TempData["carModel"] as CarModel;
            //ViewBag.dataModelFillter = dataModel;

            CarModel carmodel = (CarModel)Session["carModel"];
            ViewData["dataModelFillter"] = carmodel;

            ViewData["leftRange"] = Session["leftRange"];
            ViewData["rightRange"] = Session["rightRange"];

            ViewData["EnginleftRange"] = Session["EnginleftRange"];
            ViewData["EnginrightRange"] = Session["EnginrightRange"];

            return PartialView();
        }

        [HttpPost]
        public JsonResult SetLeftRange(int? left)
        {
            if (left != null)
            {
                Session["leftRange"] = left;

                UnitOfWork ctx = new UnitOfWork();
                CarModel carmodel = (CarModel)Session["carModel"];
                List<Car> cars = new List<Car>();

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
                    cars = ctx.carManager.GetCarwithEquipments();
                }
                int _rightRange = (int)Session["rightRange"];
                var DIstinctcars = cars.Where(a => left <= a.ModelYear && a.ModelYear <= _rightRange).ToList().Distinct();


                string CarsList = JsonConvert.SerializeObject(DIstinctcars, Formatting.None, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                TempData["mydata"] = DIstinctcars;


                return Json(CarsList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }

        }

        [HttpPost]
        public JsonResult SetRightRange(int? right)
        {
            if (right != null)
            {
                Session["rightRange"] = right;

                UnitOfWork ctx = new UnitOfWork();
                CarModel carmodel = (CarModel)Session["carModel"];
                List<Car> cars = new List<Car>();

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
                    cars = ctx.carManager.GetCarwithEquipments();
                }

                int _leftRange = (int)Session["leftRange"];
                var DIstinctcars = cars.Where(a => _leftRange <= a.ModelYear && a.ModelYear <= right).ToList().Distinct();

                string CarsList = JsonConvert.SerializeObject(DIstinctcars, Formatting.None, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                TempData["mydata"] = DIstinctcars;
                Session["SessionCars"] = DIstinctcars;

                return Json(CarsList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }

        }




        [HttpPost]
        public JsonResult EnginSetLeftRange(int? left)
        {
            if (left != null)
            {
                Session["EnginleftRange"] = left;

                UnitOfWork ctx = new UnitOfWork();
                CarModel carmodel = (CarModel)Session["carModel"];
                List<Car> cars = new List<Car>();

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
                    cars = ctx.carManager.GetCarwithEquipments();
                }
                int _rightRange = (int)Session["EnginrightRange"];
                var DIstinctcars = cars.Where(a => left <= Convert.ToInt64(a.Power) && Convert.ToInt64(a.Power) <= _rightRange).ToList().Distinct();


                string CarsList = JsonConvert.SerializeObject(DIstinctcars, Formatting.None, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                TempData["mydata"] = DIstinctcars;


                return Json(CarsList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }

        }

        [HttpPost]
        public JsonResult EnginSetRightRange(int? right)
        {
            if (right != null)
            {
                Session["EnginrightRange"] = right;

                UnitOfWork ctx = new UnitOfWork();
                CarModel carmodel = (CarModel)Session["carModel"];
                List<Car> cars = new List<Car>();

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
                    cars = ctx.carManager.GetCarwithEquipments();
                }

                int _leftRange = (int)Session["EnginleftRange"];
                var DIstinctcars = cars.Where(a => _leftRange <= Convert.ToInt64(a.Power) && Convert.ToInt64(a.Power) <= right).ToList().Distinct();

                string CarsList = JsonConvert.SerializeObject(DIstinctcars, Formatting.None, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                TempData["mydata"] = DIstinctcars;
                Session["SessionCars"] = DIstinctcars;

                return Json(CarsList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }

        }




        [HttpPost]
        public JsonResult Carfilter(CarModel carmodel)
        {
            UnitOfWork ctx = new UnitOfWork();
            List<Car> cars = new List<Car>();
            Session["carModel"] = carmodel;

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


            string CarsList = JsonConvert.SerializeObject(DIstinctcars, Formatting.None, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });


            TempData["mydata"] = DIstinctcars;
            Session["SessionCars"] = DIstinctcars;
            TempData["carModel"] = carmodel;


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
            Session["EnginleftRange"] = 110;
            Session["EnginrightRange"] = 600;
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

        public ActionResult OrderAudi(int? id)
        {
            if (id != null)
                ViewBag.Id = id;
            return View();
        }
        [HttpPost]
        public ActionResult OrderAudi(OrderEnquiry orderEnquiry, HttpPostedFileBase uploadattachments)
        {
            ActionResult result = View(orderEnquiry);

            if (ModelState.IsValid)
            {
                RequestStatus requestStatus;
                if (uploadattachments != null && !uploadattachments.ContentType.CheckImageExtention())
                {
                    //requestStatus = new ManageRequestStatus().GetStatus(Status.GeneralError, "Plz Upload The Attachment");
                    requestStatus = new ManageRequestStatus().GetStatus(Status.GeneralError, "Attachment not supported ,Plz Upload Image Only");

                }
                else
                {
                    if (uploadattachments != null)
                    {

                        var fileName = Guid.NewGuid() + Path.GetFileName(uploadattachments.FileName);

                        var path = Path.Combine(Server.MapPath("~/attachments/Orders"), fileName);
                        uploadattachments.SaveAs(path);
                        orderEnquiry.ChassisNumber = $"/attachments/Orders/{fileName}";

                    }

                    var ctx = new UnitOfWork();
                    orderEnquiry.Sendtime = DateTime.Now;
                    ctx.OrderEnquiryManager.Add(orderEnquiry);
                    requestStatus = new ManageRequestStatus().GetStatus(Status.Created);

                    try
                    {
                        SendEmail sendEmail = new SendEmail();
                        sendEmail.SendMail(new EmailDTO
                        {
                            To = "To Email",
                            Message = "<h1 style='font-size:25px; line-height:1.5'>New Car Enquiry Request</h1>"
                            + "<p style='font-size:15px; color: #000'>Thank You for Enquiry This Car</p>" + "<br />"
                            + "<b style='font-size:12px; line-height:1.5'>Car Name :</b>" + orderEnquiry.CarID + "<br />"
                            + "<b style='font-size:12px; line-height:1.5'>Car Code :</b>" + orderEnquiry.CarCode + "<br />"
                            + "<b style='font-size:12px; line-height:1.5'>First Name :</b>" + orderEnquiry.FullName + "<br />"
                            + "<b style='font-size:12px; line-height:1.5'>Last Name :</b>" + orderEnquiry.EmailAddress + "<br />"
                            + "<b style='font-size:12px; line-height:1.5'>Email :</b>" + orderEnquiry.MobileNumber + "<br />"
                            + "<b style='font-size:12px; line-height:1.5'>Phone :</b>" + orderEnquiry.CarModel + "<br />"

                            + "<b style='font-size:12px; line-height:1.5'>Message:</b>" + orderEnquiry.Comment + "<br />" +
                            "<br />",
                            From = "web@empcnews.com",
                            Subject = "New Contact Us"
                        }, "Contact", orderEnquiry.EmailAddress);
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
        [HttpPost]
        public JsonResult GetEnginCapacity(int? Id)
        {
            if (Id != null)
            {
    

                UnitOfWork ctx = new UnitOfWork();

                List<EnginCapacity> enginCapacity = new List<EnginCapacity>();
                enginCapacity = ctx.EnginCapacityManager.GetAllBind().Where(s => s.CategoryID == Id).ToList();
                ViewData["EnginCapacityData"] = enginCapacity;

                string EnginCapacityList = JsonConvert.SerializeObject(enginCapacity, Formatting.None, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                return Json(EnginCapacityList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }

        }
        [HttpPost]
        public JsonResult GetEnginCapacityNew(int? Id)
        {
            if (Id != null)
            {


                UnitOfWork ctx = new UnitOfWork();

                List<EnginCapacity> enginCapacityNew = new List<EnginCapacity>();
                enginCapacityNew = ctx.EnginCapacityManager.GetAllBind().Where(s => s.CategoryID == Id).ToList();
                ViewData["EnginCapacityDataNew"] = enginCapacityNew;

                string EnginCapacityListNew = JsonConvert.SerializeObject(enginCapacityNew, Formatting.None, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                return Json(EnginCapacityListNew, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }

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