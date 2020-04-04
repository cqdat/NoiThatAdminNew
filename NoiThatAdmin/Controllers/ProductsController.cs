﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
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

    public class ProductsController : BaseController
    {
        private TanThoiEntities db = new TanThoiEntities();

        // GET: Products
        #region List of product
        [Authorize]
        public ActionResult Index()
        {
            ViewData["ListCateParent"] = db.Categories.Where(c => c.Parent == 0).ToList();
            ViewData["ListCate"] = db.Categories.Where(c => c.Parent != 0).ToList();
            return View();
        }
        public ActionResult _PartialIndex(int? pageNumber, int? pageSize, string MaSP, string SanPham, int? DanhMucCha, int? DanhMucCon,
                                          string SEOKeywords)
        {
            SanPham = SanPham ?? "";
            ViewBag.SanPham = SanPham;
            pageNumber = pageNumber ?? 1;
            pageSize = pageSize ?? 10;

            if (pageSize == -1)
            {
                pageSize = db.Products.ToList().Count;
            }
            ViewBag.PageSize = pageSize;

            var lstprod = db.Products.ToList();

            if (!string.IsNullOrEmpty(MaSP))
            {
                lstprod = lstprod.Where(s => s.ProductCode.ToUpper().Contains(MaSP.ToUpper())).ToList();
            }
            ViewBag.MaSP = MaSP;

            if (!string.IsNullOrEmpty(SanPham))
            {
                lstprod = lstprod.Where(s => s.ProductName.ToUpper().Contains(SanPham.ToUpper())).ToList();
            }
            ViewBag.SanPham = SanPham;

            if (!string.IsNullOrEmpty(DanhMucCha.ToString()))
            {
                lstprod = lstprod.Where(s => s.CategoryIDParent == DanhMucCha).ToList();
            }
            ViewBag.DanhMucCha = DanhMucCha;

            if (!string.IsNullOrEmpty(DanhMucCon.ToString()))
            {
                lstprod = lstprod.Where(s => s.CategoryID == DanhMucCon).ToList();
            }
            ViewBag.DanhMucCon = DanhMucCon;

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
                return PartialView("~/Views/Products/_PartialIndex.cshtml", lstprod.ToList().ToPagedList(pageNumber ?? 1, pageSize ?? 2));
            }
            return View(lstprod.ToList().ToPagedList(pageNumber ?? 1, pageSize ?? 2));
        }
        #endregion

        // GET: Products/Details/5
        #region Detail of product
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        #endregion


        // GET: Products/Create
        #region Create new product
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.CategoryIDParent = new SelectList(db.Categories.Where(c => c.Parent == 0), "CategoryID", "CategoryName");
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            ViewBag.CreatedBy = new SelectList(db.Users, "UserID", "UserName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "ProductID,ProductCode,ProductName,Price,PriceSale,CategoryIDParent,CategoryID,Images,ImagesThumb,ShortDescription,Content,InStock,IsSale,IsNew,Rating,IsActive,CountView,Created,CreatedBy,SEOTitle,SEOUrlRewrite,SEOKeywords,SEOMetadescription")] Product product,
                                   HttpPostedFileBase HinhAnh)
        {
            if (ModelState.IsValid)
            {
                var allowedExtensions = new[] {
            ".Jpg", ".png", ".jpg", "jpeg"
                };
                if (HinhAnh == null)
                {
                    product.Images = "NoImage.png";
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

                        var path = Path.Combine(Server.MapPath(WebConstants.ImgProduct), myfile);
                        //var dir = Directory.CreateDirectory(path);
                        //HinhAnh.SaveAs(Path.Combine(path, myfile));

                        product.Images = myfile;
                        HinhAnh.SaveAs(path);
                    }
                    else
                    {
                        ViewBag.message = "Please choose only Image file";
                    }
                }


                product.Price = "0";
                product.PriceSale = "0";
                product.SEOUrlRewrite = Helpers.ConvertToUpperLower(product.ProductName);
                product.Created = DateTime.Now;
                //product.CreatedBy = db.Users.FirstOrDefault(q => q.UserName == User.Identity.Name).UserID;
                product.CreatedBy = 1;
                db.Products.Add(product);
                db.SaveChanges();
                Success(string.Format("Thêm mới sản phẩm<b>{0}</b> thành công.", product.ProductName), true);
                return RedirectToAction("Index");
            }

            ViewBag.CategoryIDParent = new SelectList(db.Categories.Where(c => c.Parent == 0), "CategoryID", "CategoryName", product.CategoryIDParent);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.CreatedBy = new SelectList(db.Users, "UserID", "UserName", product.CreatedBy);
            Success(string.Format("Thêm mới sản phẩm<b>{0}</b> thành công.", product.ProductName), true);
            //return RedirectToAction("Index");
            return View(product);
        }
        #endregion


        // GET: Products/Edit/5
        #region Edit product
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryIDParent = new SelectList(db.Categories.Where(c => c.Parent == 0), "CategoryID", "CategoryName", product.CategoryIDParent);
            ViewBag.CategoryID = new SelectList(db.Categories.Where(c => c.Parent == product.CategoryIDParent), "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.CreatedBy = new SelectList(db.Users, "UserID", "UserName", product.CreatedBy);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "ProductID,ProductCode,ProductName,Price,PriceSale,CategoryIDParent,CategoryID,Images,ImagesThumb,ShortDescription,Content,InStock,IsSale,IsNew,Rating,IsActive,CountView,Created,CreatedBy,SEOTitle,SEOUrlRewrite,SEOKeywords,SEOMetadescription")] Product product,
            HttpPostedFileBase HinhAnh)
        {
            if (ModelState.IsValid)
            {
                var allowedExtensions = new[] {
            ".Jpg", ".png", ".jpg", "jpeg"
                };

                if (HinhAnh == null)
                {
                    product.Images = product.Images;
                }
                else
                {

                    //Xóa hình ảnh đã tồn tại, trừ hình ảnh mặc định.
                    if (product.Images != "NoImage.png")
                    {
                        string fullPath = Request.MapPath(WebConstants.ImgProduct + "/" + product.Images);
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

                        var path = Path.Combine(Server.MapPath(WebConstants.ImgProduct), myfile);
                        //var dir = Directory.CreateDirectory(path);
                        //HinhAnh.SaveAs(Path.Combine(path, myfile));

                        product.Images = myfile;
                        HinhAnh.SaveAs(path);
                    }
                    else
                    {
                        ViewBag.message = "Please choose only Image file";
                    }
                }

                product.Price = product.Price;
                product.PriceSale = product.PriceSale;
                product.Created = product.Created;
                product.CreatedBy = product.CreatedBy;
                product.SEOUrlRewrite = Helpers.ConvertToUpperLower(product.ProductName);
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                Success(string.Format("Chỉnh sửa thông tin sản phẩm <b>{0}</b> thành công.", product.ProductName), true);

                return RedirectToAction("Details", "Products", new { id = product.ProductID });
            }
            ViewBag.CategoryIDParent = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryIDParent);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.CreatedBy = new SelectList(db.Users, "UserID", "UserName", product.CreatedBy);
            Success(string.Format("Chỉnh sửa thông tin sản phẩm <b>{0}</b> thành công.", product.ProductName), true);
            return RedirectToAction("Details", "Products", new { id = product.ProductID });
        }
        #endregion


        // GET: Products/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            //Xóa hình ảnh đã tồn tại, trừ hình ảnh mặc định.
            if (product.Images != "NoImage.png")
            {
                string fullPath = Request.MapPath(WebConstants.ImgProduct + "/" + product.Images);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }
            db.Products.Remove(product);
            db.SaveChanges();
            Success(string.Format("Xóa thành công!", ""), true);

            return RedirectToAction("Index");
        }

        #region JSON 
        public JsonResult LoadMenuCap2(int ParentID)
        {
            //var tmp = from s in db.MODELDEVICEs
            //          where s.IDCate == idCate
            //          select s.NameModel;
            //var sItems = new SelectList(tmp);
            //return Json(sItems, JsonRequestBehavior.AllowGet);

            var result = new SelectList(db.Categories.Where(m => m.Parent == ParentID), "CategoryID", "CategoryName");
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

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
