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
    public class CVUserController : Controller
    {
        DataWorkEntities db = new DataWorkEntities();
        String login = "/Account/ViewLogin";
        // GET: CVUser
        public ActionResult ViewCV()
        {
            User user = (User)Session["user"];
            if(user == null)
            {
                return Redirect(login);
            }
            return View(db.Cvs.ToList());
        }
        //Hồ sơ 1 :
        public ActionResult Cvone()
        {
            User user = (User)Session["user"];
            if(user == null)
            {
                return Redirect(login);
            }
            return View();
        }

        public ActionResult EditcVOne(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cv cv = db.Cvs.Find(id);
            if (cv == null)
            {
                return HttpNotFound();
            }
            ViewBag.user_id = new SelectList(db.Users, "user_id", "user_login", cv.user_id);
            return View(cv);
        }
        [HttpPost]
        public ActionResult CreateCv([Bind(Include = "cv_id,cv_fullname,cv_location,cv_bird,cv_sex,cv_phone,cv_email,cv_address,cv_linkfc,cv_target,cv_datebegineducation,cv_dateendeducation,cv_contenteducation,cv_datebeginexp,cv_dateendexp,cv_contentexp,cv_datebeginexptwo,cv_dateendexptwo,cv_contentexptwo,cv_datebeginactivate,cv_dateendactivate,cv_contentactivate,cv_yearcerti,cv_contentcertificate,cv_yearre,cv_contentreward,cv_english,cv_IT,user_id,cv_numbertheme,cv_schools,cv_industrystory,cv_species,cv_point,cv_roleexp,cv_roleexptwo")] Cv cv)
        {
            User user = (User)Session["user"];
            if (ModelState.IsValid)
            {
                db.Cvs.Add(cv);
                cv.user_id = user.user_id;
                db.SaveChanges();
                return Redirect(Request.UrlReferrer.ToString());
            }
            ViewBag.user_id = new SelectList(db.Users, "user_id", "user_login", cv.user_id);
            return View(cv);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditcV([Bind(Include = "cv_id,cv_fullname,cv_location,cv_bird,cv_sex,cv_phone,cv_email,cv_address,cv_linkfc,cv_target,cv_datebegineducation,cv_dateendeducation,cv_contenteducation,cv_datebeginexp,cv_dateendexp,cv_contentexp,cv_datebeginexptwo,cv_dateendexptwo,cv_contentexptwo,cv_datebeginactivate,cv_dateendactivate,cv_contentactivate,cv_contentcertificate,cv_contentreward,cv_english,cv_IT,user_id,cv_numbertheme,cv_yearre,cv_yearcerti,cv_schools,cv_industrystory,cv_species,cv_point,cv_roleexp,cv_roleexptwo")] Cv cv)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cv).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect(Request.UrlReferrer.ToString());
            }
            ViewBag.user_id = new SelectList(db.Users, "user_id", "user_login", cv.user_id);
            return View(cv);
        }
    }
}