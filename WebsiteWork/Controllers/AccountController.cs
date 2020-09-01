using System;
using System.Linq;
using System.Web.Mvc;
using WebsiteWork.Models;

namespace WebsiteWork.Controllers
{
    public class AccountController : Controller
    {
        DataWorkEntities db = new DataWorkEntities();
        // LINK
        String home = "/";
        String viewcv = "/CVUser/ViewCV";
        // GET: Account
        public ActionResult ViewLogin()
        {
            User user = (User)Session["user"];
            if(user != null)
            {
                return Redirect(viewcv);
            }
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection f)
        {
            String sEmail = f["user_email"].ToString();
            String sPass = f["user_pass"].ToString();

            User user = db.Users.Where(n => n.user_activate == true && n.user_role == 1).SingleOrDefault(n => n.user_email == sEmail && n.user_pass == sPass);
            if (user != null)
            {
                Session["user"] = user;
                db.Users.Find(user.user_id).user_datelogin = DateTime.Now;
                db.Users.Find(user.user_id).user_token = Guid.NewGuid().ToString();
                db.SaveChanges();
                return Redirect(viewcv);
            }
            else
            {
                Session["NotLogin"] = "<div class='alert alert-danger'><b class='text-danger'><i class='fas fa-times-circle' style='color: red'>&nbsp;</i>Email hoặc mật khẩu đã sai, vui lòng kiểm tra lại.</b></div>";
                return Redirect(Request.UrlReferrer.ToString());
            }
        }
        [HttpPost]
        public ActionResult Regist([Bind(Include = "user_id,user_login,user_pass,user_nicename,user_email,user_datecreated,user_token,user_role,user_datelogin,user_activate,user_image")] User user, FormCollection f)
        {
            String user_e = f["user_email"].ToString();
            User user_exist = db.Users.SingleOrDefault(n => n.user_email == user.user_email);
            if (user_exist != null)
            {
                Session["user_exist"] = "<div class='alert alert-danger w-100'><b class='text-danger'><i class='fas fa-times-circle' style='color: red'>&nbsp;</i>Email đã tồn tại, vui lòng chọn email khác.</b></div>";
                Session["active"] = "active";
                return Redirect(Request.UrlReferrer.ToString());
            }
            else
            {
                db.Users.Add(user);
                user.user_datecreated = DateTime.Now;
                user.user_role = 1;
                user.user_token = Guid.NewGuid().ToString();
                user.user_activate = true;
                user.user_login = user.user_login;
                db.SaveChanges();
                Session["user"] = user;
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