using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteWork.Models;

namespace WebsiteWork.Controllers
{
    public class ViewController : Controller
    {
        DataWorkEntities db = new DataWorkEntities();
        // GET: View
        //Xem chi tiết bài tuyển
        public ActionResult DetailsView(int ? id)
        {
            Work work = db.Works.SingleOrDefault(n => n.work_id == id);
            if(work == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(work);
        }
        //Xem chi tiết trang nhà tuyển dụng
        public ActionResult DetailsEmployer(int? id)
        {
            Employer employer = db.Employers.SingleOrDefault(n => n.employer_id == id);
            if (employer == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(employer);
        }
        //All Employer
        public ActionResult AllEmployer()
        {
            List<Employer> employer = db.Employers.ToList();
            return View(employer);
        }
        [HttpPost]
        public JsonResult AjaxMethod(string name)
        {
            List<Work> work = db.Works.ToList();
            return Json(work,JsonRequestBehavior.AllowGet);
        }
        public ActionResult TestCode()
        {
            return View();
        }
        public ActionResult WorkPro()
        {
            List<Work> works = db.Works.ToList();
            return View(works);
        }
        public ActionResult WorkAll()
        {
            List<Work> works = db.Works.Where(n => n.work_activate == true && n.work_option == true).OrderByDescending(n => n.work_datecreated).ToList();
            return View(works);
        }
    }
}