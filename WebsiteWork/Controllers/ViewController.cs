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
        public ActionResult DetailsView()
        {
            return View();
        }
        [HttpPost]
        public JsonResult AjaxMethod(string name)
        {
            List<Work> work = db.Works.ToList();
            return Json(work,JsonRequestBehavior.AllowGet);
        }
    }
}