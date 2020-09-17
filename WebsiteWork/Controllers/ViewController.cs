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
    }
}