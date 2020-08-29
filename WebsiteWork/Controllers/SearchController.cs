using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebsiteWork.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public PartialViewResult ViewSrarch()
        {
            return PartialView();
        }
    }
}