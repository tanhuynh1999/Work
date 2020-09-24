using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteWork.Models;

namespace WebsiteWork.Controllers
{
    public class WorkController : Controller
    {
        DataWorkEntities db = new DataWorkEntities();
        String login = "/Account/ViewLogin";
        // GET: Work
        public ActionResult WorkUser()
        {
            User user = (User)Session["user"];
            if (user == null)
            {
                return Redirect(login);
            }
            else
            {
                List<Submit> submits = db.Submits.Where(n => n.user_id == user.user_id).ToList();
                return View(submits);
            }
        }
        //Cài đặt nghành nghề
        public ActionResult EditCareer(FormCollection f)
        {
            User user = (User)Session["user"];
            String sCareerId = f["career_id"];
            db.Users.Find(user.user_id).career_id = Int32.Parse(sCareerId);
            db.SaveChanges();
            User us = db.Users.SingleOrDefault(n => n.user_id == user.user_id);
            Session["user"] = us;
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}