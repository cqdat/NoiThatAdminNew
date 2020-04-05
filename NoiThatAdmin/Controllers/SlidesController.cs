using NoiThatAdmin.Models;
using NoiThatAdmin.Models.DataModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoiThatAdmin.Controllers
{
    [Authorize]
    public class SlidesController : Controller
    {
        TanThoiEntities db = new TanThoiEntities();
        // GET: Slides
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ForHomepage()
        {
            var model = db.Slides.Where(q => q.CategoryID == 0);
            return View(model);
        }

        public ActionResult ForCategory()
        {
            var model = new SlideCateViewModel();
            model.slides = db.Slides.Where(q => q.CategoryID > 0).ToList();
            model.categories = db.Categories.Where(q => q.IsActive == true && q.Parent > 0).ToList();
            return View(model);
        }

        public ActionResult ForDefault()
        {
            var model = db.Slides.Where(q => q.CategoryID == -1);
            return View(model);
        }

        public JsonResult UploadImage()
        {
            string result = "DONE";

            try
            {
                if (Request.Files.Count > 0)
                {
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fname;
                        string x = "";//file.FileName;

                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];

                            x = fname;
                        }
                        else
                        {
                            fname = DateTime.Now.ToString("HHmmssddMMyyyy") + "_" + file.FileName;

                            x = fname;
                        }

                        fname = Path.Combine(Server.MapPath("~/Photos/Slides/"), fname);
                        file.SaveAs(fname);

                        result = x;
                    }
                }
                else
                {
                    result = "FAIL";
                }
            }
            catch
            {
                result = "FAIL";
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CreateSlide(string urlimg, string slogan1, string slogan2, string title, string link, string target,int? sort)
        {
            string result = "FAIL";
            try
            {
                Slide s = new Slide();
                s.ImageURL = urlimg;
                s.Slogun1 = slogan1;
                s.Slogun2 = slogan2;
                s.SlideTitle = title;
                s.LinkBanner = link;
                s.LinkTarget = target;
                s.Sort = sort;
                s.CategoryID = 0;
                s.CreateBy = db.Users.FirstOrDefault(q => q.UserName == User.Identity.Name).UserID;
                s.Created = DateTime.Now;
                db.Slides.Add(s);
                
                db.SaveChanges();
                result = "DONE";
            }
            catch
            {

            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CreateSlide2(string urlimg, string slogan1, string slogan2, string title, string link, string target, int? sort, int? category)
        {
            string result = "FAIL";
            try
            {
                Slide s = new Slide();
                s.ImageURL = urlimg;
                s.Slogun1 = slogan1;
                s.Slogun2 = slogan2;
                s.SlideTitle = title;
                s.LinkBanner = link;
                s.LinkTarget = target;
                s.Sort = sort;
                s.CategoryID = category;
                s.CreateBy = db.Users.FirstOrDefault(q => q.UserName == User.Identity.Name).UserID;
                s.Created = DateTime.Now;
                db.Slides.Add(s);

                db.SaveChanges();
                result = "DONE";
            }
            catch
            {

            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CreateSlide3(string urlimg, string slogan1, string slogan2, string title, string link, string target, int? sort)
        {
            string result = "FAIL";
            try
            {
                Slide s = new Slide();
                s.ImageURL = urlimg;
                s.Slogun1 = slogan1;
                s.Slogun2 = slogan2;
                s.SlideTitle = title;
                s.LinkBanner = link;
                s.LinkTarget = target;
                s.Sort = sort;
                s.CategoryID = -1;
                s.CreateBy = db.Users.FirstOrDefault(q => q.UserName == User.Identity.Name).UserID;
                s.Created = DateTime.Now;
                db.Slides.Add(s);

                db.SaveChanges();
                result = "DONE";
            }
            catch
            {

            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}