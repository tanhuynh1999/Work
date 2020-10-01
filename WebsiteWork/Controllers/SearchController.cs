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
        // Tìm kiếm tất cả việc làm
        public PartialViewResult SearchWorkAll()
        {
            return PartialView();
        }
        public PartialViewResult SearchCareer(int ? id)
        {
            System.Threading.Thread.Sleep(2000);
            List<Work> works = db.Works.Where(n => n.work_activate == true && n.work_option == true && n.Employer.employer_activate == true && n.career_id == id).ToList();
            return PartialView("_WorkAll",works);
        }




        public PartialViewResult AllWork()
        {
            List<Work> works = db.Works.Where(n => n.work_activate == true && n.work_option == true).OrderByDescending(n => n.work_datecreated).ToList();
            return PartialView("_WorkAll",works);
        }
        public PartialViewResult PartTimeWork()
        {
            List<Work> works = db.Works.Where(n => n.work_activate == true && n.work_option == true && n.Form.form_id == 3).OrderByDescending(n => n.work_datecreated).ToList();
            return PartialView("_WorkAll", works);
        }
        public PartialViewResult AllTimeWork()
        {
            List<Work> works = db.Works.Where(n => n.work_activate == true && n.work_option == true && n.Form.form_id == 2).OrderByDescending(n => n.work_datecreated).ToList();
            return PartialView("_WorkAll", works);
        }
        public PartialViewResult InternsWork()
        {
            List<Work> works = db.Works.Where(n => n.work_activate == true && n.work_option == true && n.Form.form_id == 4).OrderByDescending(n => n.work_datecreated).ToList();
            return PartialView("_WorkAll", works);
        }
        public PartialViewResult MonopolyWork()
        {
            List<Work> works = db.Works.Where(n => n.work_activate == true && n.work_option == true).Where(n=>n.Employer.employer_version == 2 || n.Employer.employer_version == 3).OrderByDescending(n => n.work_datecreated).ToList();
            return PartialView("_WorkAll", works);
        }
        public PartialViewResult ViewWork()
        {
            List<Work> works = db.Works.Where(n => n.work_activate == true && n.work_option == true).OrderByDescending(n => n.work_view).ToList();
            return PartialView("_WorkAll", works);
        }
        public PartialViewResult LikeWork()
        {
            List<Work> works = db.Works.Where(n => n.work_activate == true && n.work_option == true).OrderByDescending(n => n.work_love).ToList();
            return PartialView("_WorkAll", works);
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
        // Search ajax
        public PartialViewResult SearchUnWorkEmployer(string key)
        {
            List<Work> work = db.Works.Where(n => n.work_name.Contains(key)).ToList();
            return PartialView("_Work",work);
        }
    }
}