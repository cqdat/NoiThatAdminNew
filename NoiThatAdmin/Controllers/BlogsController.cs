using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NoiThatAdmin.Models.DataModels;
using NoiThatAdmin.Utilities;
using PagedList;

namespace NoiThatAdmin.Controllers
{
    public class BlogsController : BaseController
    {
        private TanThoiEntities db = new TanThoiEntities();

        // GET: Blogs
        public ActionResult Index()
        {
            ViewData["ListCate"] = db.Categories.Where(c => c.TypeCate == WebConstants.CategoryNews).ToList();
            return View();
        }

        public ActionResult _PartialIndex(int? pageNumber, int? pageSize, string TieuDe, int? DanhMuc,
                                          string SEOKeywords)
        {
            TieuDe = TieuDe ?? "";
            ViewBag.TieuDe = TieuDe;
            pageNumber = pageNumber ?? 1;
            pageSize = pageSize ?? 10;

            if (pageSize == -1)
            {
                pageSize = db.Blogs.Where(b=>b.TypeBlog==WebConstants.BlogNews).ToList().Count;
            }
            ViewBag.PageSize = pageSize;

            var lstprod = db.Blogs.Where(b => b.TypeBlog == WebConstants.BlogNews).ToList();

            

            if (!string.IsNullOrEmpty(TieuDe))
            {
                lstprod = lstprod.Where(s => s.BlogName.ToUpper().Contains(TieuDe.ToUpper())).ToList();
            }
            ViewBag.TieuDe = TieuDe;

            if (!string.IsNullOrEmpty(DanhMuc.ToString()))
            {
                lstprod = lstprod.Where(s => s.CategoryID == DanhMuc).ToList();
            }
            ViewBag.DanhMuc = DanhMuc;

           
            if (!string.IsNullOrEmpty(SEOKeywords))
            {
                lstprod = lstprod.Where(s => s.SEOKeywords.ToUpper().Contains(SEOKeywords.ToUpper())).ToList();
            }
            ViewBag.SEOKeywords = SEOKeywords;

            lstprod = lstprod.OrderBy(s => s.Created).ToList();
            ViewBag.STT = pageNumber * pageSize - pageSize + 1;
            int count = lstprod.ToList().Count();
            ViewBag.TotalRow = count;
            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Views/Blogs/_PartialIndex.cshtml", lstprod.ToList().ToPagedList(pageNumber ?? 1, pageSize ?? 2));
            }
            return View(lstprod.ToList().ToPagedList(pageNumber ?? 1, pageSize ?? 2));
        }

        // GET: Blogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // GET: Blogs/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories.Where(c=>c.TypeCate == WebConstants.CategoryNews), "CategoryID", "CategoryName");
            ViewBag.CreatedBy = new SelectList(db.Users, "UserID", "UserName");
            return View();
        }

        // POST: Blogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "BlogID,BlogName,ImageURL,Descript,Content,TypeBlog,CountView,IsActive,CategoryID,Created,CreatedBy,SEOTitle,SEOUrlRewrite,SEOKeywords,SEOMetadescription")] Blog blog,
            HttpPostedFileBase HinhAnh)
        {
            if (ModelState.IsValid)
            {
                var allowedExtensions = new[] {
            ".Jpg", ".png", ".jpg", "jpeg"
                };
                if (HinhAnh == null)
                {
                    blog.ImageURL = "NoImage.png";
                }
                else
                {
                    var fileName = Path.GetFileName(HinhAnh.FileName);

                    var ext = Path.GetExtension(HinhAnh.FileName);
                    if (allowedExtensions.Contains(ext)) //check what type of extension  
                    {
                        string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                        string myfile = name + "_" + DateTime.Now.Millisecond + ext; //appending the name with id  
                                                                                     // store the file inside ~/project folder(Img)  

                        var path = Path.Combine(Server.MapPath(WebConstants.imgNewsURL), myfile);
                        //var dir = Directory.CreateDirectory(path);
                        //HinhAnh.SaveAs(Path.Combine(path, myfile));

                        blog.ImageURL = myfile;
                        HinhAnh.SaveAs(path);
                    }
                    else
                    {
                        ViewBag.message = "Please choose only Image file";
                    }
                }

                blog.SEOUrlRewrite = Helpers.ConvertToUpperLower(blog.BlogName);
                blog.TypeBlog = WebConstants.BlogNews;
                blog.CreatedBy = db.Users.FirstOrDefault(q => q.UserName == User.Identity.Name).UserID;
                blog.Created = DateTime.Now;
                blog.LastModify = DateTime.Now;
                blog.CountView = 1;
                db.Blogs.Add(blog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", blog.CategoryID);
            ViewBag.CreatedBy = new SelectList(db.Users, "UserID", "UserName", blog.CreatedBy);
            return View(blog);
        }

        [Authorize]
        // GET: Blogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories.Where(c => c.TypeCate == WebConstants.CategoryNews), "CategoryID", "CategoryName", blog.CategoryID);
            ViewBag.CreatedBy = new SelectList(db.Users, "UserID", "UserName", blog.CreatedBy);
            return View(blog);
        }

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "BlogID,BlogName,ImageURL,Descript,Content,TypeBlog,CountView,IsActive,CategoryID,Created,CreatedBy,SEOTitle,SEOUrlRewrite,SEOKeywords,SEOMetadescription")] Blog blog,
            HttpPostedFileBase HinhAnh)
        {
            if (ModelState.IsValid)
            {
                var allowedExtensions = new[] {
            ".Jpg", ".png", ".jpg", "jpeg"
                };

                if (HinhAnh == null)
                {
                    blog.ImageURL = blog.ImageURL;
                }
                else
                {

                    //Xóa hình ảnh đã tồn tại, trừ hình ảnh mặc định.
                    if (blog.ImageURL != "NoImage.png")
                    {
                        string fullPath = Request.MapPath(WebConstants.imgNewsURL + "/" + blog.ImageURL);
                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
                        }
                    }

                    var fileName = Path.GetFileName(HinhAnh.FileName);
                    var ext = Path.GetExtension(HinhAnh.FileName);
                    if (allowedExtensions.Contains(ext)) //check what type of extension  
                    {
                        string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                        string myfile = name + "_" + DateTime.Now.Millisecond + ext; //appending the name with id  
                                                                                     // store the file inside ~/project folder(Img)  

                        var path = Path.Combine(Server.MapPath(WebConstants.imgNewsURL), myfile);
                        //var dir = Directory.CreateDirectory(path);
                        //HinhAnh.SaveAs(Path.Combine(path, myfile));

                        blog.ImageURL= myfile;
                        HinhAnh.SaveAs(path);
                    }
                    else
                    {
                        ViewBag.message = "Please choose only Image file";
                    }
                }
                
                blog.SEOUrlRewrite = Helpers.ConvertToUpperLower(blog.BlogName);
                blog.TypeBlog = blog.TypeBlog;
                blog.CreatedBy = db.Users.FirstOrDefault(q => q.UserName == User.Identity.Name).UserID;
                blog.Created = blog.Created;
                blog.LastModify = DateTime.Now;

                db.Entry(blog).State = EntityState.Modified;
                db.SaveChanges();
                Success(string.Format("Chỉnh sửa thông tin <b>{0}</b> thành công.", ""), true);
                return RedirectToAction("Index","Blogs");
            }
            ViewBag.CategoryID = new SelectList(db.Categories.Where(c => c.TypeCate == WebConstants.CategoryNews), "CategoryID", "CategoryName", blog.CategoryID);
            ViewBag.CreatedBy = new SelectList(db.Users, "UserID", "UserName", blog.CreatedBy);
            return View(blog);
        }

        // GET: Blogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Blog blog = db.Blogs.Find(id);
            db.Blogs.Remove(blog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
