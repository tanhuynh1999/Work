using System;
using System.Collections.Generic;
using System.Linq;
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
        DataWorkEntities db = new DataWorkEntities();
        // GET: Employer
        public ActionResult ViewPageEmployer()
        {
            return View();
        }
        public ActionResult CreatePage()
        {
            Employer employer = (Employer)Session["employer"];
            if(employer == null)
            {
                return Redirect(LoginEmployer);
            }
            return View();
        }
        [HttpPost]
        public ActionResult CreatePage([Bind(Include = "employer_id,employer_email,employer_pass,employer_fullname,employer_sex,employer_company,employer_position,employer_address,employer_phone,employer_vacancies,employer_token,employer_datelogin,employer_datecreated,employer_activate,employer_status,employer_logo,employer_specialized,employer_version,employer_option,employer_personalpage,employer_name,employer_introduce,employer_favourite,employer_linkfc,employer_recruitment,employer_addressmain,employer_ifamemapmain,employer_addresstwo,employer_ifamemaptwo,employer_addressthree,employer_ifamemapthree")] Employer employer)
        {
            if (ModelState.IsValid)
            {
                db.Employers.Add(employer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employer);
        }
    }
}