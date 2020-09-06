using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebsiteWork.Controllers
{
    public class HomeEmployerController : Controller
    {
        // GET: HomeEmployer
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult IntroduceEmployer()
        {
            return View();
        }
        public PartialViewResult Menu()
        {
            return PartialView();
        }
    }
}