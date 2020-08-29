using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteWork.Models;

namespace WebsiteWork.Controllers
{
    public class AccountController : Controller
    {
        DataWorkEntities db = new DataWorkEntities();
        // LINK
        String home = "/";
        // GET: Account
        public ActionResult ViewLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection f)
        {
            String sEmail = f["user_email"].ToString();
            String sPass = f["user_pass"].ToString();

            User user = db.Users.Where(n => n.user_activate == true && n.user_role == 1).SingleOrDefault(n=>n.user_email == sEmail && n.user_pass == sPass);
            if(user != null)
            {
                Session["user"] = user;
                db.Users.Find(user.user_id).user_datelogin = DateTime.Now;
                db.SaveChanges();
                return Redirect(Request.UrlReferrer.ToString());
            }
            else
            {
                Session["NotLogin"] = "<div class='alert alert-danger'><b class='text-danger'><i class='fas fa-times-circle' style='color: red'>&nbsp;</i>Email hoặc mật khẩu đã sai, vui lòng kiểm tra lại.</b></div>";
                return Redirect(Request.UrlReferrer.ToString());
            }
        }
        // Log-out
        public ActionResult Logout()
        {
            Session["user"] = null;
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}