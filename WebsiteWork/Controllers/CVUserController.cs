using System;
using System.Collections.Generic;
using System.Linq;
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
            return View();
        }
        //Hồ sơ 1 :
        public ActionResult Cvone()
        {
            return View();
        }
    }
}