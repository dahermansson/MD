using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MD.Models;
using System.IO;

namespace MD.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            string url = Request.Url.PathAndQuery;
            FileLoader fileLoader = new FileLoader();
            var page = fileLoader.GetPage(url);
            if (page.Type == SiteType.SingelPage)
                return View("Index", page);
            else if (page.Type == SiteType.MultiPage)
                return View("Blog", page);
            return View();
        }

        public ActionResult Menu()
        {
            return PartialView("_Menu", new MenuBuilder().GetMenu());
        }

    }
}