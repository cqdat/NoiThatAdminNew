using NoiThatAdmin.Models;
using NoiThatAdmin.Models.DataModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace NoiThatAdmin.Controllers
{
    public class CategoryImageController : Controller
    {
        FilesHelper2 h;
        TanThoiEntities db = new TanThoiEntities();

        string tempPath = "~/Upload/";
        string serverMapPath = "~/Photos/Category/";

        private string StorageRoot
        {
            get { return Path.Combine(HostingEnvironment.MapPath(serverMapPath)); }
        }

        private string UrlBase = "/Photos/Category/";
        string DeleteURL = "/CategoryImage/DeleteFile/?file=";
        string DeleteType = "GET";

        public CategoryImageController()
        {
            h = new FilesHelper2(DeleteURL, DeleteType, StorageRoot, UrlBase, tempPath, serverMapPath);
        }
        // GET: CategoryImage


        public ActionResult Index()
        {
            var model = db.Categories.Where(q => q.IsActive == true && q.Parent == 0);
            return View(model);
        }


        [Authorize]
        public ActionResult LstCategoryImages()
        {
            return View();
        }


        public JsonResult GetChildCategory(int? cateid)
        {
            var model = db.Categories.Where(q => q.Parent == cateid).Select(s => new {
                CateID = s.CategoryID,
                CateName = s.CategoryName
            }).ToList();

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Upload(int? childid, int? parentid)
        {
            //productid = 1;

            var resultList = new List<ViewDataUploadFilesResult>();

            var CurrentContext = HttpContext;

            h.UploadAndShowResults(CurrentContext, resultList, childid, parentid);


            JsonFiles files = new JsonFiles(resultList);

            bool isEmpty = !resultList.Any();
            if (isEmpty)
            {
                return Json("Error");
            }
            else
            {
                return Json(files);
            }
        }

        public JsonResult GetFileList()
        {
            var list = h.GetFileList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult DeleteFile(string file)
        {
            h.DeleteFile(file);
            return Json("OK", JsonRequestBehavior.AllowGet);
        }


    }
}