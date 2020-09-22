using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteWork.Models;

namespace WebsiteWork.Controllers
{
    public class ButtonController : Controller
    {
        DataWorkEntities db = new DataWorkEntities();
        // GET: Button
        [HttpPost]
        public ActionResult SubmitCV([Bind(Include = "submit_id,user_id,work_id,submit_datecreated,cv_id")] Submit submit)
        {
            db.Submits.Add(submit);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}