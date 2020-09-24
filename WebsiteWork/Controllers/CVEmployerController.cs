using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteWork.Models;

namespace WebsiteWork.Controllers
{
    public class CVEmployerController : Controller
    {
        DataWorkEntities db = new DataWorkEntities();
        String home = "/";
        String LoginEmployer = "/Account/LoginEmployer";
        String HomeEmployer = "/HomeEmployer/Index";
        String managerwork = "/Employer/ManagerWork";
        // GET: CVEmployer
        public ActionResult ManageCV()
        {
            Employer employer = (Employer)Session["employer"];
            if(employer == null)
            {
                return Redirect(LoginEmployer);
            }
            else
            {
                List<Submit> submits = db.Submits.Where(n => n.Work.employer_id == employer.employer_id).OrderByDescending(n=>n.submit_datecreated).ToList();
                return View(submits);
            }
        }
        public ActionResult ViewCv(int ? id)
        {
            Employer employer = (Employer)Session["employer"];
            if(employer == null)
            {
                return Redirect(LoginEmployer);
            }
            else
            {
                Submit submit = db.Submits.SingleOrDefault(n => n.submit_id == id);
                return View(submit);
            }
        }
    }
}