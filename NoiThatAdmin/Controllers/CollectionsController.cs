using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NoiThatAdmin.Models.DataModels;
using NoiThatAdmin.Utilities;
using PagedList;

namespace NoiThatAdmin.Controllers
{
    public class CollectionsController : Controller
    {
        private TanThoiEntities db = new TanThoiEntities();
        // GET: Collections
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
                pageSize = db.Products.Where(p => p.IsProduct == false).ToList().Count;
            }
            ViewBag.PageSize = pageSize;

            var lstprod = db.Products.Where(p => p.IsProduct == false).ToList();

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
                return PartialView("~/Views/Collections/_PartialIndex.cshtml", lstprod.ToList().ToPagedList(pageNumber ?? 1, pageSize ?? 2));
            }
            return View(lstprod.ToList().ToPagedList(pageNumber ?? 1, pageSize ?? 2));
        }
        #endregion
    }
}