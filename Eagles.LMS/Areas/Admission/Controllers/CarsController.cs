using Eagles.LMS.BLL;
using Eagles.LMS.Models;
using Eagles.LMS.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Eagles.LMS.Areas.Admission.Controllers
{

    [RouteArea("Admission")]
    [AuthorizeFilterAttribute]
    public class CarsController : Controller
    {
        // GET: Admission/Cars
        public ActionResult Index()
        {


            return View(new UnitOfWork().carManager.GetAll().OrderByDescending(s => s.ID).ToList());
        }

        //public ActionResult Pending()
        //{
        //    return View(new UnitOfWork().carManager.GetAll().Where(s => s.Status == EntityStatus.Pending).ToList());
        //}



        public int GetUserId()
        {
            var userFromSesstion = HttpContext.Session["User_Key"];
            return Convert.ToInt32(userFromSesstion);
        }
        public ActionResult Edit(int id)
        {
            var ctx = new UnitOfWork();
            var _car = ctx.carManager.GetBy(id);
            if (_car == null)
                return HttpNotFound();
            _car.CarImages = ctx.CarImagesManager.GetAll().Where(s => s.CarId == id).ToList();
            return View(_car);
        }

        [HttpPost]
        public ActionResult Edit(Car car, HttpPostedFileBase uploadattachments, List<HttpPostedFileBase> uploadattachments_multi = null)
        {

            var _ctx = new UnitOfWork();



            RequestStatus requestStatus;
            if (uploadattachments != null && !
                uploadattachments.ContentType.CheckImageExtention())
            {

                requestStatus = new ManageRequestStatus().GetStatus(Status.GeneralError, "Attachment not supported ,Plz Upload Image Only");
            }
            else
            {
                if (uploadattachments_multi != null && uploadattachments_multi[0] != null && uploadattachments_multi.Any() &&
            uploadattachments_multi.Any(s => !s.ContentType.CheckImageExtention()))
                {
                    requestStatus = new ManageRequestStatus().GetStatus(Status.GeneralError, "Attachment not supported ,Plz Upload Image Only");

                }
                else
                {
                    string _rendom, fileName, path;
                    if (uploadattachments != null)
                    {

                        _rendom = new Random().Next(1, 99999999).ToString();

                        //fileName = _rendom + Path.GetFileName(uploadattachments.FileName);
                        string extention = System.IO.Path.GetExtension(uploadattachments.FileName);
                        fileName = _rendom + extention;

                        path = Path.Combine(Server.MapPath("~/attachments"), fileName);
                        uploadattachments.SaveAs(path);
                        car.MainImage = $"/attachments/{fileName}";

                    }

                    int userId = GetUserId();
                    car.UserEditId = userId;
                    car.EditeTime = DateTime.Now;

                    _ctx.carManager.UpdateWithSave(car);
                    var user = _ctx.UserManager.GetById(GetUserId());



                    //_ctx.logManager.Add(new log
                    //{
                    //    UserId = GetUserId(),
                    //    ActionTime = DateTime.Now,
                    //    EntityId = service.Id,
                    //    TableName = "Service",
                    //    Action = "Update:Service",
                    //    LoginDate = user.LoginDate,
                    //    LogoutDate = user.LogoutDate,
                    //    ActionTitle = service.TitleArabic

                    //});



                    if (uploadattachments_multi != null && uploadattachments_multi[0] != null)
                    {
                        foreach (var item in uploadattachments_multi)
                        {
                            _rendom = new Random().Next(1, 99999999).ToString();

                            //fileName = _rendom + Path.GetFileName(item.FileName);
                            string extention = System.IO.Path.GetExtension(item.FileName);
                            fileName = _rendom + extention;

                            path = Path.Combine(Server.MapPath("~/attachments"), fileName);
                            item.SaveAs(path);
                            _ctx.CarImagesManager.Add(new CarImages
                            {

                                CarId = car.ID,
                                Path = $"/attachments/{fileName}",

                            });

                        }
                    }


                    requestStatus = new ManageRequestStatus().GetStatus(Status.Edited);




                }
            }

            TempData["RequestStatus"] = requestStatus;
            car.CarImages = _ctx.CarImagesManager.GetAll().Where(s => s.CarId == car.ID).ToList();
            return View(car);

        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Car car, HttpPostedFileBase uploadattachments, List<HttpPostedFileBase> uploadattachments_multi = null)
        {
            //service.Status = EntityStatus.Pending;

            ActionResult result = View(car);

            if (ModelState.IsValid)
            {

                RequestStatus requestStatus;
                if (uploadattachments == null || uploadattachments.ContentLength == 0 || !
                    uploadattachments.ContentType.CheckImageExtention())
                {
                    requestStatus = new ManageRequestStatus().GetStatus(Status.GeneralError, "Attachment not supported ,Plz Upload Image Only");
                }
                else
                {


                    if (uploadattachments_multi != null && uploadattachments_multi[0] != null && uploadattachments_multi.Any() &&
                 uploadattachments_multi.Any(s => !s.ContentType.CheckImageExtention()))
                    {
                        requestStatus = new ManageRequestStatus().GetStatus(Status.GeneralError, "Attachment not supported ,Plz Upload Image Only");

                    }
                    else
                    {
                        string _rendom = new Random().Next(1, 99999999).ToString();

                        //var fileName = _rendom + Path.GetFileName(uploadattachments.FileName);
                        string extention = System.IO.Path.GetExtension(uploadattachments.FileName);
                        var fileName = _rendom + extention;

                        var path = Path.Combine(Server.MapPath("~/attachments"), fileName);
                        uploadattachments.SaveAs(path);
                        car.MainImage = $"/attachments/{fileName}";

                        var _ctx = new UnitOfWork();

                        int userId = GetUserId();
                        car.UserCreateId = userId;
                        car.CreateTime = DateTime.Now;
                        car.UserEditId = userId;
                        car.EditeTime = DateTime.Now;
                        car = _ctx.carManager.Add(car);
                        var user = _ctx.UserManager.GetById(GetUserId());






                        //_ctx.logManager.Add(new log
                        //{
                        //    UserId = userId,
                        //    ActionTime = DateTime.Now,
                        //    EntityId = service.Id,
                        //    TableName = "Service",
                        //    Action = "Create:Service",
                        //    LoginDate = user.LoginDate,
                        //    LogoutDate = user.LogoutDate,
                        //    ActionTitle = service.TitleArabic

                        //});


                        if (uploadattachments_multi != null && uploadattachments_multi[0] != null)
                        {
                            foreach (var item in uploadattachments_multi)
                            {
                                _rendom = new Random().Next(1, 99999999).ToString();

                                //fileName = _rendom + Path.GetFileName(item.FileName);
                                extention = System.IO.Path.GetExtension(item.FileName);
                                fileName = _rendom + extention;

                                path = Path.Combine(Server.MapPath("~/attachments"), fileName);
                                item.SaveAs(path);
                                _ctx.CarImagesManager.Add(new CarImages
                                {

                                    CarId = car.ID,
                                    Path = $"/attachments/{fileName}",

                                });

                            }
                        }

                        requestStatus = new ManageRequestStatus().GetStatus(Status.Created);
                        result = RedirectToAction(nameof(Create));

                    }

                }
                TempData["RequestStatus"] = requestStatus;



            }
            return result;

        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            UnitOfWork ctx = new UnitOfWork();
            var entity = ctx.carManager.GetBy(id);
            var user = ctx.UserManager.GetById(GetUserId());

            //ctx.logManager.Add(new log
            //{
            //    UserId = GetUserId(),
            //    ActionTime = DateTime.Now,
            //    EntityId = id,
            //    TableName = "Service",
            //    Action = "Delete:Service",
            //    LoginDate = user.LoginDate,
            //    LogoutDate = user.LogoutDate,
            //    ActionTitle = entity.TitleArabic
            //});


            ctx.carManager.Delete(entity);
            return Json(JsonRequestBehavior.AllowGet);
        }


    }
}