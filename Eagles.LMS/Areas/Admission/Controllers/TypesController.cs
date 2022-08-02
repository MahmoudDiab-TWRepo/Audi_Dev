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
    public class TypesController : Controller
    {
        // GET: Admission/Types
        public ActionResult Index()
        {
            return View(new UnitOfWork().typeManager.GetAll().ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Types types, HttpPostedFileBase uploadattachments, HttpPostedFileBase uploadattachments_multi)
        {

            ActionResult result = View(types);

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


                    if (uploadattachments_multi == null || uploadattachments_multi.ContentLength == 0 || !
                    uploadattachments_multi.ContentType.CheckImageExtention())
                    {
                        requestStatus = new ManageRequestStatus().GetStatus(Status.GeneralError, "Attachment not supported two ,Plz Upload Image Only");

                    }
                    else
                    {
                        string _rendom = new Random().Next(1, 999999999).ToString();
                        string _rendomtwo = new Random().Next(1, 99999999).ToString();
                        //var fileName = _rendom + Path.GetFileName(uploadattachments.FileName);
                        string extention = System.IO.Path.GetExtension(uploadattachments.FileName);
                        string extentiontwo = System.IO.Path.GetExtension(uploadattachments_multi.FileName);
                        var fileName = _rendom + extention;
                        var fileNametwo = _rendomtwo + extentiontwo;

                        var path = Path.Combine(Server.MapPath("~/attachments"), fileName);
                        var pathtwo = Path.Combine(Server.MapPath("~/attachments"), fileNametwo);
                        uploadattachments.SaveAs(path);
                        uploadattachments_multi.SaveAs(pathtwo);
                        types.MainImageOne = $"/attachments/{fileName}";
                        types.MainImageTwo = $"/attachments/{fileNametwo}";
                        var _ctx = new UnitOfWork();

                        int userId = GetUserId();
                        //category.UserCreateId = userId;
                        //category.CreateTime = DateTime.Now;
                        //category.UserEditId = userId;
                        //category.EditeTime = DateTime.Now;
                        types = _ctx.typeManager.Add(types);
                        var user = _ctx.UserManager.GetById(GetUserId());



                        requestStatus = new ManageRequestStatus().GetStatus(Status.Created);
                        result = RedirectToAction(nameof(Create));

                    }

                }
                TempData["RequestStatus"] = requestStatus;



            }
            return result;

        }



        public ActionResult Edit(int id)
        {

            var catego = new UnitOfWork().typeManager.GetBy(id);
            if (catego == null)
                return HttpNotFound();
            return View(catego);
            //return View();
        }
        [HttpPost]
        public ActionResult Edit(Types category, HttpPostedFileBase uploadattachments, HttpPostedFileBase uploadattachments_multi)
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
                if (uploadattachments_multi != null && !
                uploadattachments_multi.ContentType.CheckImageExtention())
                {

                    requestStatus = new ManageRequestStatus().GetStatus(Status.GeneralError, "Attachment not supported ,Plz Upload Image Only");
                }
                else
                {
                    //string _rendom, fileName, path;
                    if (uploadattachments != null)
                    {

                        //_rendom = new Random().Next(1, 99999999).ToString();

                        ////fileName = _rendom + Path.GetFileName(uploadattachments.FileName);
                        //string extention = System.IO.Path.GetExtension(uploadattachments.FileName);
                        //fileName = _rendom + extention;

                        //path = Path.Combine(Server.MapPath("~/attachments"), fileName);
                        //uploadattachments.SaveAs(path);
                        //category.MainImageOne = $"/attachments/{fileName}";




                        string _rendom = new Random().Next(1, 999999999).ToString();
                        string _rendomtwo = new Random().Next(1, 99999999).ToString();
                        //var fileName = _rendom + Path.GetFileName(uploadattachments.FileName);
                        string extention = System.IO.Path.GetExtension(uploadattachments.FileName);
                        string extentiontwo = System.IO.Path.GetExtension(uploadattachments_multi.FileName);
                        var fileName = _rendom + extention;
                        var fileNametwo = _rendomtwo + extentiontwo;

                        var path = Path.Combine(Server.MapPath("~/attachments"), fileName);
                        var pathtwo = Path.Combine(Server.MapPath("~/attachments"), fileNametwo);
                        uploadattachments.SaveAs(path);
                        uploadattachments_multi.SaveAs(pathtwo);
                        category.MainImageOne = $"/attachments/{fileName}";
                        category.MainImageTwo = $"/attachments/{fileNametwo}";
                        //var _ctx = new UnitOfWork();

                        //int userId = GetUserId();
                        //category = _ctx.categoryManager.Add(category);
                        //var user = _ctx.UserManager.GetById(GetUserId());



                        requestStatus = new ManageRequestStatus().GetStatus(Status.Created);
                        //result = RedirectToAction(nameof(Create));









                    }

                    int userId = GetUserId();

                    _ctx.typeManager.UpdateWithSave(category);
                    var user = _ctx.UserManager.GetById(GetUserId());






                    //if (uploadattachments_multi != null && uploadattachments_multi[0] != null)
                    //{
                    //    foreach (var item in uploadattachments_multi)
                    //    {
                    //        _rendom = new Random().Next(1, 99999999).ToString();

                    //        //fileName = _rendom + Path.GetFileName(item.FileName);
                    //        string extention = System.IO.Path.GetExtension(item.FileName);
                    //        fileName = _rendom + extention;

                    //        path = Path.Combine(Server.MapPath("~/attachments"), fileName);
                    //        item.SaveAs(path);
                    //        _ctx.ServiceImagesManager.Add(new ServiceImages
                    //        {

                    //            ServiceId = service.Id,
                    //            Path = $"/attachments/{fileName}",

                    //        });

                    //    }
                    //}


                    requestStatus = new ManageRequestStatus().GetStatus(Status.Edited);




                }
            }

            TempData["RequestStatus"] = requestStatus;
            //service.ServiceImages = _ctx.ServiceImagesManager.GetAll().Where(s => s.ServiceId == service.Id).ToList();
            return View(category);

        }



        public int GetUserId()
        {
            var userFromSesstion = HttpContext.Session["User_Key"];
            return Convert.ToInt32(userFromSesstion);
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            UnitOfWork ctx = new UnitOfWork();
            var entity = ctx.typeManager.GetBy(id);
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


            ctx.typeManager.Delete(entity);
            return Json(JsonRequestBehavior.AllowGet);
        }
    }
}