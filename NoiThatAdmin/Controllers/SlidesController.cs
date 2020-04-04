using NoiThatAdmin.Models.DataModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoiThatAdmin.Controllers
{
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
            var model = db.Slides.Where(q => q.CategoryID > 0);
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
                        string x = file.FileName;

                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];

                            x += "_" + fname;
                        }
                        else
                        {
                            fname = DateTime.Now.ToString("HHmmssddMMyyyy") + "_" + file.FileName;

                            x += "_" + fname;
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

    }
}