using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NoiThatAdmin.Models.DataModels;
using NoiThatAdmin.Models;
using NoiThatAdmin.Utilities;

namespace NoiThatAdmin.Controllers
{
    public class HomeController : Controller
    {
        private TanThoiEntities db = new TanThoiEntities();
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ListMenus()
        {
            MenuViewModel menus = new MenuViewModel();
            menus.lstMenuCha = db.Categories.Where(c => c.Parent == 0 && c.TypeCate == WebConstants.CategoryProduct).ToList();
            menus.lstCollectionParent = db.Categories.Where(c => c.Parent == 0 && c.TypeCate == WebConstants.CategoryCollection).ToList();
            menus.lstNews = db.Categories.Where(c => c.Parent == 0 && c.TypeCate == WebConstants.CategoryNews).ToList();
            return View(menus);
        }
    }
}