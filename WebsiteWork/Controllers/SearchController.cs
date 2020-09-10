using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteWork.Models;

namespace WebsiteWork.Controllers
{
    public class SearchController : Controller
    {
        DataWorkEntities db = new DataWorkEntities();
        //Link
        String LoginEmployer = "/Account/LoginEmployer";
        // GET: Search
        public PartialViewResult ViewSrarch()
        {
            return PartialView();
        }
        //Tìm kiếm quản lý việc làm nhà tuyển dụng
        public ActionResult SearchWorkEmployer()
        {
            Employer employer = (Employer)Session["employer"];
            if(employer == null)
            {
                return Redirect(LoginEmployer);
            }
            List<Work> work = db.Works.Where(n=>n.employer_id == employer.employer_id).ToList();
            return View(work);
        }
        [HttpPost]
        public ActionResult SearchWorkEmployer(String work_search)
        {
            List<Work> work;
            if (string.IsNullOrEmpty(work_search))
            {
                work = db.Works.ToList();
            }
            else
            {
                work = db.Works.Where(n => n.work_name.StartsWith(work_search)).ToList();
            }
            return View(work);
        }
        public JsonResult GetWork(string work_search)
        {
            List<string> work;
            work = db.Works.Where(n => n.work_name.StartsWith(work_search)).Select(n => n.work_name).ToList();
            return Json(work);
        }
    }
}