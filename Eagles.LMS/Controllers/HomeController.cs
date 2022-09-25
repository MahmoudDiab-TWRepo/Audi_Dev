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

            Session["EnginleftRange"] = 110;
            Session["EnginrightRange"] = 600;

            ViewData["leftRange"] = Session["leftRange"];
            ViewData["rightRange"] = Session["rightRange"];

            ViewData["EnginleftRange"] = Session["EnginleftRange"];
            ViewData["EnginrightRange"] = Session["EnginrightRange"];

            GetCount();
            ViewBag.AllCounts = Session["Counts"]; ;

            return View();
        }

        public ActionResult _Results()
        {
            return PartialView();
        }

        public ActionResult _SearchKeys()
        {
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
                List<Car> cars = (List<Car>)Session["Cars"];

                if (cars == null || cars.Count == 0)
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
                List<Car> cars = (List<Car>)Session["Cars"];

                if (cars == null || cars.Count == 0)
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
                List<Car> cars = (List<Car>)Session["Cars"];

                if (cars == null || cars.Count == 0)
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
                UnitOfWork ctx = new UnitOfWork();
                List<Car> cars = (List<Car>)Session["Cars"];

                if (cars == null || cars.Count == 0)
                {
                    cars = ctx.carManager.GetCarwithEquipments();
                }

                Session["EnginrightRange"] = right;
                int _leftRange = (int)Session["EnginleftRange"];
                var DIstinctcars = cars.Where(a => _leftRange <= Convert.ToInt64(a.Power) && Convert.ToInt64(a.Power) <= right).ToList().Distinct();

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
            else if (carmodel.CarSubItems != null && carmodel.CarSubItems.Length > 0)
            {
                foreach (var itemcat in carmodel.CarSubItems)
                {
                    var itemcars = ctx.carManager.GetAllBind().Where(s => s.SubItem_Id == itemcat);
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
            else if (carmodel.EngineType != null && carmodel.EngineType.Length > 0)
            {
                foreach (var item in carmodel.EngineType)
                {
                    if (item == 1000)
                    {
                        var itemcars = ctx.carManager.GetAllBind().Where(s => s.FWD == true);
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
                    else if (item == 2000)
                    {
                        var itemcars = ctx.carManager.GetAllBind().Where(s => s.Quattro == true);
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

            }
            else if (carmodel.CarEquipments != null && carmodel.CarEquipments.Length > 0)
            {
                foreach (var item in carmodel.CarEquipments)
                {
                    if (item == 100)
                    {
                        var itemcars = ctx.carManager.GetAllBind().Where(s => s.Cruise_Control == true);
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
                    else if (item == 200)
                    {
                        var itemcars = ctx.carManager.GetAllBind().Where(s => s.Hill_descent_control == true);
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
                    else if (item == 300)
                    {
                        var itemcars = ctx.carManager.GetAllBind().Where(s => s.Hold_assist == true);
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
                    else if (item == 400)
                    {
                        var itemcars = ctx.carManager.GetAllBind().Where(s => s.Head_up_display == true);
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
                    else if (item == 500)
                    {
                        var itemcars = ctx.carManager.GetAllBind().Where(s => s.Rear_view_camera == true);
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
                    else if (item == 600)
                    {
                        var itemcars = ctx.carManager.GetAllBind().Where(s => s.Rear_Eid_Pluss == true);
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
                    else if (item == 700)
                    {
                        var itemcars = ctx.carManager.GetAllBind().Where(s => s.Rear_parking_Eid == true);
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
                    else if (item == 800)
                    {
                        var itemcars = ctx.carManager.GetAllBind().Where(s => s.Park_assist == true);
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
                    else if (item == 900)
                    {
                        var itemcars = ctx.carManager.GetAllBind().Where(s => s.Pre_sense == true);
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
                    else if (item == 110)
                    {
                        var itemcars = ctx.carManager.GetAllBind().Where(s => s.LED == true);
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
                    else if (item == 111)
                    {
                        var itemcars = ctx.carManager.GetAllBind().Where(s => s.Matrix_LED == true);
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
                    else if (item == 112)
                    {
                        var itemcars = ctx.carManager.GetAllBind().Where(s => s.Audi_smartphone_interface == true);
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
                    else if (item == 113)
                    {
                        var itemcars = ctx.carManager.GetAllBind().Where(s => s.Standard == true);
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
                    else if (item == 114)
                    {
                        var itemcars = ctx.carManager.GetAllBind().Where(s => s.Sport_seats == true);
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

            }
            else if (carmodel.CarColours != null && carmodel.CarColours.Length > 0)
            {
                foreach (var item in carmodel.CarColours)
                {
                    var itemcars = ctx.carManager.GetAllBind().Where(s => s.ColorId == item);
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
            //serializer.MaxJsonLength = Int32.MaxValue;

            string CarsList = JsonConvert.SerializeObject(DIstinctcars, Formatting.None, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });


            TempData["mydata"] = DIstinctcars;
            Session["Cars"] = cars;


            return Json(CarsList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Sort(int? sort)
        {
            if (sort != null)
            {

                UnitOfWork ctx = new UnitOfWork();
                List<Car> cars = (List<Car>)Session["Cars"];

                if (cars == null || cars.Count == 0)
                {
                    cars = ctx.carManager.GetCarwithEquipments();
                }

                if (sort == 1)
                {
                    cars = cars.OrderBy(a => a.ModelYear).ToList();
                }
                else if (sort == 2)
                {
                    cars = cars.OrderByDescending(a => a.ModelYear).ToList();
                }
                else if (sort == 3)
                {
                    cars = cars.OrderBy(a => a.Distance).ToList();
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

                Session["Cars"] = cars;


                return Json(CarsList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }

        }

        [HttpPost]
        public JsonResult ClearMethod()
        {
            Session["carModel"] = null;
            Session["SessionCars"] = null;
            Session["Cars"] = null;

            return Json(true);
        }

        [HttpPost]
        public void GetCount()
        {
            UnitOfWork ctx = new UnitOfWork();
            List<Car> cars = ctx.carManager.GetCarwithEquipments();
            Count count = new Count();

            foreach (Car car in cars)
            {
                if (car.FWD == true)
                    count.FWD = count.FWD + 1;

                if (car.Quattro == true)
                    count.Quattro = count.Quattro + 1;

                if (car.Cruise_Control == true)
                    count.Cruise_Control = count.Cruise_Control + 1;

                if (car.Hill_descent_control == true)
                    count.Hill_descent_control = count.Hill_descent_control + 1;

                if (car.Hold_assist == true)
                    count.Hold_assist = count.Hold_assist + 1;

                if (car.Head_up_display == true)
                    count.Head_up_display = count.Head_up_display + 1;

                if (car.Rear_view_camera == true)
                    count.Rear_view_camera = count.Rear_view_camera + 1;

                if (car.Rear_Eid_Pluss == true)
                    count.Rear_Eid_Pluss = count.Rear_Eid_Pluss + 1;

                if (car.Rear_parking_Eid == true)
                    count.Rear_parking_Eid = count.Rear_parking_Eid + 1;

                if (car.Park_assist == true)
                    count.Park_assist = count.Park_assist + 1;

                if (car.Pre_sense == true)
                    count.Pre_sense = count.Pre_sense + 1;

                if (car.LED == true)
                    count.LED = count.LED + 1;

                if (car.Matrix_LED == true)
                    count.Matrix_LED = count.Matrix_LED + 1;

                if (car.Audi_smartphone_interface == true)
                    count.Audi_smartphone_interface = count.Audi_smartphone_interface + 1;

                if (car.Standard == true)
                    count.Standard = count.Standard + 1;

                if (car.Sport_seats == true)
                    count.Sport_seats = count.Sport_seats + 1;
            }

            Session["Counts"] = count;
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