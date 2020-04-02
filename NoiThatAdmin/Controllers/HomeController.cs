using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NoiThatAdmin.Models.DataModels;
using NoiThatAdmin.Models;

namespace NoiThatAdmin.Controllers
{
    public class HomeController : Controller
    {
        private TanThoiEntities db = new TanThoiEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ListMenus()
        {
            MenuViewModel menus = new MenuViewModel();
            menus.lstMenuCha = db.Categories.Where(c => c.Parent == 0).ToList();
            return View(menus);
        }
    }
}