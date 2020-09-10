using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsiteWork.Models;

namespace WebsiteWork.Controllers
{
    public class EmployerController : Controller
    {
        //Link
        String home = "/";
        String LoginEmployer = "/Account/LoginEmployer";
        String HomeEmployer = "/HomeEmployer/Index";
        String managerwork = "/Employer/ManagerWork";
        DataWorkEntities db = new DataWorkEntities();
        // GET: Employer
        public ActionResult ViewPageEmployer()
        {
            return View();
        }
        public ActionResult CreatePage(int? id)
        {
            Employer em = (Employer)Session["employer"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employer employer = db.Employers.Find(id);
            if (employer == null)
            {
                return HttpNotFound();
            }
            if(em == null)
            {
                return Redirect(LoginEmployer);
            }
            return View(employer);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreatePage([Bind(Include = "employer_id,employer_email,employer_pass,employer_fullname,employer_sex,employer_company,employer_position,employer_address,employer_phone,employer_vacancies,employer_token,employer_datelogin,employer_datecreated,employer_activate,employer_status,employer_logo,employer_specialized,employer_version,employer_option,employer_personalpage,employer_name,employer_introduce,employer_favourite,employer_linkfc,employer_recruitment,employer_addressmain,employer_ifamemapmain,employer_addresstwo,employer_ifamemaptwo,employer_addressthree,employer_ifamemapthree,employer_province,employer_pricemin,employer_pricemax,employer_symbol,employer_recrequirement")] Employer employer)
        {
            Employer em = (Employer)Session["employer"];
            if (ModelState.IsValid)
            {
                db.Entry(employer).State = EntityState.Modified;
                employer.employer_activate = em.employer_activate;
                employer.employer_version = em.employer_version;
                employer.employer_personalpage = true;
                employer.employer_pass = em.employer_pass;
                employer.employer_email = em.employer_email;
                employer.employer_name = em.employer_name;
                db.SaveChanges();
                Session["employer"] = employer;
                return Redirect(HomeEmployer);
            }
            return View(employer);
        }
        //Đăng bài
        public ActionResult CreateWork()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateWork([Bind(Include = "work_id,work_name,work_image,work_deadline,work_description,work_request,work_benefit,work_address,work_moneyf,work_moneye,work_amount,work_activate,work_option,work_view,work_love,work_map,work_datecreated,work_dateexpired,work_datesend,work_imagetwo,work_imgathree,employer_id,work_pricemin,work_pricemax,work_symbol,work_sex,work_format,work_yearsofexp,work_province,work_delete")] Work work)
        {
            Employer employer = (Employer)Session["employer"];
            db.Works.Add(work);
            work.employer_id = employer.employer_id;
            work.work_activate = false;
            work.work_datecreated = DateTime.Now;
            work.work_view = 1;
            work.work_love = 1;
            work.work_delete = false;
            work.work_option = true;
            db.SaveChanges();
            return RedirectToAction("ManagerWork");
        }
        //Quản lý bài tuyển
        public ActionResult Manager()
        {
            return View();
        }
        public ActionResult ManagerWork()
        {
            Employer employer = (Employer)Session["employer"];
            if(employer == null)
            {
                return Redirect(LoginEmployer);
            }
            List<Work> work = db.Works.Where(n=>n.employer_id == employer.employer_id && n.work_delete == true).OrderByDescending(n => n.work_datecreated).ToList();
            return View(work);
        }
        //Ajax work
        public PartialViewResult AllWork()
        {
            Employer employer = (Employer)Session["employer"];
            List<Work> work = db.Works.Where(n => n.employer_id == employer.employer_id && n.work_delete == false).OrderByDescending(n => n.work_datecreated).ToList();
            return PartialView("_Work", work);
        }
        public PartialViewResult TopLoveWork()
        {
            Employer employer = (Employer)Session["employer"];
            List<Work> work = db.Works.Where(n => n.employer_id == employer.employer_id && n.work_delete == false).OrderByDescending(n=>n.work_love).ToList();
            return PartialView("_Work", work);
        }
        public PartialViewResult WorkAc()
        {
            Employer employer = (Employer)Session["employer"];
            List<Work> work = db.Works.Where(n => n.employer_id == employer.employer_id && n.work_activate == true && n.work_delete == false).OrderByDescending(n => n.work_datecreated).ToList();
            return PartialView("_Work", work);
        }
        public PartialViewResult WorkNotAc()
        {
            Employer employer = (Employer)Session["employer"];
            List<Work> work = db.Works.Where(n => n.employer_id == employer.employer_id && n.work_activate == false && n.work_delete == false).OrderByDescending(n => n.work_datecreated).ToList();
            return PartialView("_Work", work);
        }
        public PartialViewResult WorkTrash()
        {
            Employer employer = (Employer)Session["employer"];
            List<Work> work = db.Works.Where(n => n.employer_id == employer.employer_id && n.work_delete == true).OrderByDescending(n => n.work_datecreated).ToList();
            return PartialView("_Work", work);
        }
        public PartialViewResult WorkOld()
        {
            Employer employer = (Employer)Session["employer"];
            List<Work> work = db.Works.Where(n => n.employer_id == employer.employer_id && n.work_delete == false).OrderBy(n => n.work_datecreated).ToList();
            return PartialView("_Work", work);
        }
        public PartialViewResult WorkAZ()
        {
            Employer employer = (Employer)Session["employer"];
            List<Work> work = db.Works.Where(n => n.employer_id == employer.employer_id && n.work_delete == false).OrderBy(n => n.work_name).ToList();
            return PartialView("_Work", work);
        }
        public PartialViewResult WorkSpam()
        {
            Employer employer = (Employer)Session["employer"];
            List<Work> work = db.Works.Where(n => n.employer_id == employer.employer_id && n.work_delete == false && n.work_spam == true).OrderByDescending(n => n.work_datecreated).ToList();
            return PartialView("_Work", work);
        }
        public PartialViewResult WorkView()
        {
            Employer employer = (Employer)Session["employer"];
            List<Work> work = db.Works.Where(n => n.employer_id == employer.employer_id && n.work_delete == false).OrderByDescending(n => n.work_view).ToList();
            return PartialView("_Work", work);
        }
    }
}