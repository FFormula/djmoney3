using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using djMoney3.Models;

namespace djMoney3.Controllers
{
    public class storyController : Controller
    {
        // GET: story
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult number()
        {
            string id;
            if (RouteData.Values["id"] == null)
                id = "1";
            else
                id = (string)RouteData.Values["id"];

            Article article = new Article();
            article.GetArticle(id);
            return View(article);
        }

        public ActionResult random()
        {
            Article article = new Article();
            article.GetRandom();
            //OpenURL("/story/number/" + NNN);
            return View("number", article);
        }


    }
}