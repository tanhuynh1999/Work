using System;
using System.Collections.Generic;
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
        String homeemployer = "/HomeEmployer/Index";
        // GET: Account
        public ActionResult ViewLogin()
        {
            User user = (User)Session["user"];
            if(user != null)
            {
                return Redirect(viewcv);
            }
            return View(db.Cvs.ToList());
        }
        [HttpPost]
        public ActionResult Login(FormCollection f)
        {
            //User
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
        public ActionResult LoginEmployer(FormCollection f)
        {
            //User
            String sEmail = f["employer_email"].ToString();
            String sPass = f["employer_pass"].ToString();
            Employer employer = db.Employers.Where(n => n.employer_activate == true).SingleOrDefault(n => n.employer_email == sEmail && n.employer_pass == sPass);
            if (employer != null)
            {
                Session["employer"] = employer;
                return Redirect(homeemployer);
            }
            else
            {
                Session["NotLoginEmployer"] = "<div class='alert alert-danger'><b class='text-danger'><i class='fas fa-times-circle' style='color: red'>&nbsp;</i>Email hoặc mật khẩu đã sai, vui lòng kiểm tra lại.</b></div>";
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
        //Quản lý cá nhân
        public ActionResult ManageUser()
        {
            User user = (User)Session["user"];
            if (user == null)
            {
                return Redirect(home);
            }
            List<Cv> cv = db.Cvs.Where(n => n.user_id == user.user_id).ToList();
            Session["count_cv"] = cv.Count;
            Session["user"] = user;
            return View();
        }
        // Doi mat khau
        [HttpPost]
        public ActionResult EditInfo(FormCollection f)
        {
            String user_industryloving = f["user_industryloving"];
            String user_skill = f["user_skill"];
            String user_interests = f["user_interests"];
            User user = (User)Session["user"];
            User usershow = db.Users.SingleOrDefault(n => n.user_id == user.user_id);
            db.Users.Find(user.user_id).user_industryloving = user_industryloving;
            db.Users.Find(user.user_id).user_skill = user_skill;
            db.Users.Find(user.user_id).user_interests = user_interests;
            db.SaveChanges();
            Session["testpass"] = "<div class='alert alert-danger w-100'><b class='text-danger'><i class='fas fa-check-circle' style = 'color: green'>&nbsp;</i><span style = 'color: green'>Thông tin cập nhật thành công.</span></b></div>";
            Session["user"] = usershow;
            return Redirect(Request.UrlReferrer.ToString());
        }
        // Thay doi thong tin cá nhân
        [HttpPost]
        public ActionResult ChangePassword(FormCollection f)
        {
            String sPass = f["user_pass"];
            User user = (User)Session["user"];
            db.Users.Find(user.user_id).user_pass = sPass;
            db.Users.Find(user.user_id).user_token = Guid.NewGuid().ToString();
            db.SaveChanges();
            Session["testpass"] = "<div class='alert alert-danger w-100'><b class='text-danger'><i class='fas fa-check-circle' style = 'color: green'>&nbsp;</i><span style = 'color: green'>Đổi mật khẩu thành công.</span></b></div>";
            Session["user"] = user;
            return Redirect(Request.UrlReferrer.ToString());
        }
        // Log-out
        public ActionResult Logout()
        {
            Session["user"] = null;
            Session["employer"] = null;
            return Redirect(Request.UrlReferrer.ToString());
        }

        //Ajax

        //---------------------------------------------------------------------------------------------------------------
        //Nhà tuyển dụng
        public ActionResult LoginEmployer()
        {
            return View();
        }
        public PartialViewResult ResEmployer()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult CreateEmployer([Bind(Include = "employer_id,employer_email,employer_pass,employer_fullname,employer_sex,employer_company,employer_position,employer_address,employer_phone,employer_vacancies,employer_token,employer_datelogin,employer_datecreated,employer_activate,employer_status,employer_logo,employer_specialized,employer_version,employer_option,employer_personalpage,employer_name,employer_introduce,employer_favourite,employer_linkfc,employer_recruitment,employer_addressmain,employer_ifamemapmain,employer_addresstwo,employer_ifamemaptwo,employer_addressthree,employer_ifamemapthree")] Employer employer)
        {
            if (ModelState.IsValid)
            {
                db.Employers.Add(employer);
                employer.employer_version = 1;
                employer.employer_activate = true;
                db.SaveChanges();
                Session["employer"] = employer;
                return Redirect(homeemployer);
            }

            return View(employer);
        }
    }
}