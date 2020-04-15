using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NoiThatAdmin.Models.DataModels;
using PagedList;
using NoiThatAdmin.Utilities;
using System.Net;
using System.Data.Entity;

namespace NoiThatAdmin.Controllers
{
    public class CollectionCategoriesController : BaseController
    {
        private TanThoiEntities db = new TanThoiEntities();
        Helpers h = new Helpers();

        // GET: Categories
        [Authorize]
        public ActionResult Index()
        {
            ViewData["ListCate"] = db.Categories.Where(c => c.TypeCate == WebConstants.CategoryCollection && c.Parent == 0).ToList();
            return View();
        }

        public ActionResult _PartialIndex(int? pageNumber, int? pageSize, string TenChungLoai, int? DanhMucCha,
                                          string SEOKeywords)
        {
            TenChungLoai = TenChungLoai ?? "";
            ViewBag.TenChungLoai = TenChungLoai;
            pageNumber = pageNumber ?? 1;
            pageSize = pageSize ?? 10;

            if (pageSize == -1)
            {
                pageSize = db.Categories.Where(c => c.TypeCate == WebConstants.CategoryCollection).ToList().Count;
            }
            ViewBag.PageSize = pageSize;

            var lstCates = db.Categories.Where(c => c.TypeCate == WebConstants.CategoryCollection).ToList();
            if (!string.IsNullOrEmpty(TenChungLoai))
            {
                lstCates = lstCates.Where(s => s.CategoryName.ToUpper().Contains(TenChungLoai.ToUpper())).ToList();
            }
            ViewBag.TenChungLoai = TenChungLoai;

            if (!string.IsNullOrEmpty(DanhMucCha.ToString()))
            {
                lstCates = lstCates.Where(s => s.Parent == DanhMucCha).ToList();
            }
            ViewBag.DanhMucCha = DanhMucCha;

            if (!string.IsNullOrEmpty(SEOKeywords))
            {
                lstCates = lstCates.Where(s => s.SEOKeywords.ToUpper().Contains(SEOKeywords.ToUpper())).ToList();
            }
            ViewBag.SEOKeywords = SEOKeywords;

            lstCates = lstCates.OrderBy(s => s.Sort).ToList();
            ViewBag.STT = pageNumber * pageSize - pageSize + 1;
            int count = lstCates.ToList().Count();
            ViewBag.TotalRow = count;
            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Views/CollectionCategories/_PartialIndex.cshtml", lstCates.ToList().ToPagedList(pageNumber ?? 1, pageSize ?? 2));
            }
            return View(lstCates.ToList().ToPagedList(pageNumber ?? 1, pageSize ?? 2));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Categories/Details/5

        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        /// <summary>
        /// tạo mới
        /// </summary>
        /// <returns></returns>
        // GET: Categories/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewData["ListCate"] = db.Categories.Where(c => c.TypeCate == WebConstants.CategoryCollection && c.Parent == 0).ToList();
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryID,CategoryName,Parent,DisplayMenu,IsActive,Sort,TypeCate,SEOTitle,SEOUrlRewrite,SEOKeywords,SEOMetadescription")] Category category)
        {
            if (ModelState.IsValid)
            {
                category.TypeCate = WebConstants.CategoryCollection;
                category.SEOUrlRewrite = Helpers.ConvertToUpperLower(category.CategoryName);
                db.Categories.Add(category);
                db.SaveChanges();
                Success(string.Format("Thêm mới <b>{0}</b> thành công, Bạn có thể chọn slide hình ảnh cho <b>{0}</b>.", category.CategoryName), true);
                //return RedirectToAction("Index");
                return RedirectToAction("Edit", "CollectionCategories", new { id = category.CategoryID });
            }

            return View(category);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Categories/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            ViewData["ListCate"] = db.Categories.Where(c => c.TypeCate == WebConstants.CategoryCollection && c.Parent == 0).ToList();
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryID,CategoryName,Parent,DisplayMenu,IsActive,Sort,SEOTitle,SEOUrlRewrite,SEOKeywords,SEOMetadescription")] Category category)
        {
            if (ModelState.IsValid)
            {
                category.TypeCate = WebConstants.CategoryProduct;
                category.SEOUrlRewrite = Helpers.ConvertToUpperLower(category.CategoryName);
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }
    }
}