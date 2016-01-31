using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using djMoney3.Models;

namespace djMoney3.Controllers
{
    public class pageController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            Pages pages = new Pages();
            pages.Load();
            return View(pages);
        }

        public ActionResult Pages ()
        {
            Pages pages = new Pages();
            pages.Load();
            return View(pages);
        }

        public ActionResult AddArticle ()
        {

            return View();
        }
    }
}