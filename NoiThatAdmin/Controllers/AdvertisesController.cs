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

namespace NoiThatAdmin.Controllers
{
    public class AdvertisesController : Controller
    {
        private TanThoiEntities db = new TanThoiEntities();

        // GET: Advertises
        public ActionResult Index()
        {
            return View(db.Advertises.ToList());
        }

        // GET: Advertises/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advertise advertise = db.Advertises.Find(id);
            if (advertise == null)
            {
                return HttpNotFound();
            }
            return View(advertise);
        }

        // GET: Advertises/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Advertises/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "AdvertiseID,AdvertiseCode,AdvertiseName,AdvertiseImage,AdvertiseURL,IsActive,Location,AdvContent,SEOTitle," +
            "SEOUrlRewrite,SEOKeywords,SEOMetadescription")] Advertise advertise,
            HttpPostedFileBase HinhAnh)
        {
            if (ModelState.IsValid)
            {
                var allowedExtensions = new[] {
            ".Jpg", ".png", ".jpg", "jpeg"
                };
                if (HinhAnh == null)
                {
                    advertise.AdvertiseImage = "NoImage.png";
                }
                else
                {
                    var fileName = Path.GetFileName(HinhAnh.FileName);

                    var ext = Path.GetExtension(HinhAnh.FileName);
                    if (allowedExtensions.Contains(ext)) //check what type of extension  
                    {
                        string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                        string myfile = name + "_" + DateTime.Now.Millisecond + "adv" + ext; //appending the name with id  
                                                                                     // store the file inside ~/project folder(Img)  

                        var path = Path.Combine(Server.MapPath(WebConstants.imgNewsURL), myfile);
                        //var dir = Directory.CreateDirectory(path);
                        //HinhAnh.SaveAs(Path.Combine(path, myfile));

                        advertise.AdvertiseImage = myfile;
                        HinhAnh.SaveAs(path);
                    }
                    else
                    {
                        ViewBag.message = "Please choose only Image file";
                    }
                }
                advertise.SEOUrlRewrite = Helpers.ConvertToUpperLower(advertise.AdvertiseName);
                db.Advertises.Add(advertise);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(advertise);
        }

        // GET: Advertises/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advertise advertise = db.Advertises.Find(id);
            if (advertise == null)
            {
                return HttpNotFound();
            }
            return View(advertise);
        }

        // POST: Advertises/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AdvertiseID,AdvertiseCode,AdvertiseName,AdvertiseImage,AdvertiseURL,IsActive")] Advertise advertise)
        {
            if (ModelState.IsValid)
            {
                db.Entry(advertise).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(advertise);
        }

        // GET: Advertises/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advertise advertise = db.Advertises.Find(id);
            if (advertise == null)
            {
                return HttpNotFound();
            }
            return View(advertise);
        }

        // POST: Advertises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Advertise advertise = db.Advertises.Find(id);
            db.Advertises.Remove(advertise);
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
