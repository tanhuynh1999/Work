using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteWork.Models;

namespace WebsiteWork.Controllers
{
    public class HomeController : Controller
    {
        DataWorkEntities db = new DataWorkEntities();
        public ActionResult Index()
        {
            return View(db.Works.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public PartialViewResult Menu()
        {
            return PartialView();
        }
        public PartialViewResult Employer()
        {
            List<Employer> employers = db.Employers.Where(n => n.employer_activate == true).OrderByDescending(n => n.employer_datecreated).OrderByDescending(n => n.employer_version).Take(18).ToList();
            return PartialView(employers);
        }
        public PartialViewResult Footer()
        {
            return PartialView();
        }
    }
}