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
    public class CategorysController : Controller
    {
        // GET: Admission/Categorys
        public ActionResult Index()
        {
            return View(new UnitOfWork().categoryManager.GetAll().ToList());
        }
        public ActionResult Edit(int id)
        {

            var catego = new UnitOfWork().categoryManager.GetBy(id);
            if (catego == null)
                return HttpNotFound();
            return View(catego);
        }
        [HttpPost]
        public ActionResult Edit(Category category, HttpPostedFileBase uploadattachments, HttpPostedFileBase uploadattachments_multi)
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

                        string _rendom = new Random().Next(1, 999999999).ToString();

                        string extention = System.IO.Path.GetExtension(uploadattachments.FileName);

                        var fileName = _rendom + extention;

                        var path = Path.Combine(Server.MapPath("~/attachments"), fileName);

                        uploadattachments.SaveAs(path);

                        category.MainImageOne = $"/attachments/{fileName}";
                     
                        requestStatus = new ManageRequestStatus().GetStatus(Status.Created);
                    }


                    if (uploadattachments_multi != null)
                    {

                        string _rendomtwo = new Random().Next(1, 99999999).ToString();
                        //var fileName = _rendom + Path.GetFileName(uploadattachments.FileName);

                        string extentiontwo = System.IO.Path.GetExtension(uploadattachments_multi.FileName);

                        var fileNametwo = _rendomtwo + extentiontwo;


                        var pathtwo = Path.Combine(Server.MapPath("~/attachments"), fileNametwo);

                        uploadattachments_multi.SaveAs(pathtwo);

                        category.MainImageTwo = $"/attachments/{fileNametwo}";
                        requestStatus = new ManageRequestStatus().GetStatus(Status.Created);
                        //result = RedirectToAction(nameof(Create));
                    }

                    int userId = GetUserId();

                    _ctx.categoryManager.UpdateWithSave(category);

                    //var user = _ctx.UserManager.GetById(GetUserId());

                    requestStatus = new ManageRequestStatus().GetStatus(Status.Edited);


                }
            }

            TempData["RequestStatus"] = requestStatus;
            return View(category);

        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category, HttpPostedFileBase uploadattachments, HttpPostedFileBase uploadattachments_multi )
        {

            ActionResult result = View(category);

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
                        category.MainImageOne = $"/attachments/{fileName}";
                        category.MainImageTwo = $"/attachments/{fileNametwo}";
                        var _ctx = new UnitOfWork();

                        int userId = GetUserId();
                        category = _ctx.categoryManager.Add(category);
                        var user = _ctx.UserManager.GetById(GetUserId());



                        requestStatus = new ManageRequestStatus().GetStatus(Status.Created);
                        result = RedirectToAction(nameof(Create));

                    }

                }
                TempData["RequestStatus"] = requestStatus;



            }
            return result;

        }

        public int GetUserId()
        {
            var userFromSesstion = HttpContext.Session["User_Key"];
            return Convert.ToInt32(userFromSesstion);
        }
    }
}
