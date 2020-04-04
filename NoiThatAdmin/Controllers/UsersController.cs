using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NoiThatAdmin.Models;
using NoiThatAdmin.Models.DataModels;
using NoiThatAdmin.Utilities;

namespace NoiThatAdmin.Controllers
{
    public class UsersController : Controller
    {
        private TanThoiEntities db = new TanThoiEntities();
        Helpers h = new Helpers();
        // GET: Users
        public ActionResult Index()
        {
            var model = new UserViewModel();
            model.users = db.Users.ToList();
            model.roles = db.Roles.ToList();
            model.permissions = db.Permissions.ToList();
            return View(model);
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.CreatedBy = new SelectList(db.Users, "UserID", "UserName");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,UserName,HashPass,Active,Created,CreatedBy")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CreatedBy = new SelectList(db.Users, "UserID", "UserName", user.CreatedBy);
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.CreatedBy = new SelectList(db.Users, "UserID", "UserName", user.CreatedBy);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,UserName,HashPass,Active,Created,CreatedBy")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CreatedBy = new SelectList(db.Users, "UserID", "UserName", user.CreatedBy);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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

        public ActionResult AddUser(string uname, string pword)
        {
            User u = new User();
            u.UserName = uname;
            u.HashPass = Helpers.GetMD5Hash(pword);
            u.Created = DateTime.Now;
            u.Active = true;
            u.CreatedBy = h.GetUserID(User.Identity.Name);
            db.Users.Add(u);
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }
        public ActionResult Role()
        {
            return View();
        }

        public ActionResult AddRole(string name)
        {
            Role r = new Role();
            r.RoleName = name;
            r.Created = DateTime.Now;
            db.Roles.Add(r);
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }

        public PartialViewResult GetUpdateRole(int? roleid)
        {
            var model = DataUpdateRole(roleid);
            return PartialView("_updaterole", model);
        }

        public PartialViewResult SetNewPermission(int? permissionid, int? roleid)
        {
            PermissionRole p = new PermissionRole();
            p.RoleID = roleid;
            p.PermissionID = permissionid;
            p.Created = DateTime.Now;
            db.PermissionRoles.Add(p);

            db.SaveChanges();

            return PartialView("_updaterole", DataUpdateRole(roleid));
        }

        public PartialViewResult SetDeleteRole(int? roleid, int? permissionid)
        {
            var perrole = db.PermissionRoles.FirstOrDefault(q => q.PermissionID == permissionid && q.RoleID == roleid);
            db.PermissionRoles.Remove(perrole);

            db.SaveChanges();

            return PartialView("_updaterole", DataUpdateRole(roleid));
        }


        public UpdateRoleViewModel DataUpdateRole(int? roleid)
        {
            var model = new UpdateRoleViewModel();

            var all = db.Permissions.Select(s => new DataPermission
            {
                PermissionID = s.PermissionID,
                PermissonName = s.PermissionName
            }).ToList();

            var allow = db.PermissionRoles.Where(q => q.RoleID == roleid).Select(s => new DataPermission
            {
                PermissionID = s.PermissionID,
                PermissonName = s.Permission.PermissionName
            }).ToList();       

            model.Allow = allow;

            all.RemoveAll(k => allow.Any(y => y.PermissionID == k.PermissionID));

            model.Available = all;

            return model;
        }

        public UserRoleViewModel DataRoleUser(int? userid)
        {
            var model = new UserRoleViewModel();

            var all = db.Roles.Select(s => new DataRole {
                RoleID = s.RoleID,
                RoleName = s.RoleName            
            }).ToList();

            var allow = db.RolesUsers.Where(q => q.UserID == userid).Select(s => new DataRole
            {
                RoleID = s.RoleID,
                RoleName = s.Role.RoleName
            }).ToList();

            all.RemoveAll(k => allow.Any(y => y.RoleID == k.RoleID));

            model.Allow = allow;
            model.Available = all;

            return model;
        }

        public PartialViewResult GetRoleUser(int? userid)
        {
            var model = DataRoleUser(userid);
            return PartialView("_roleuser", model);
        }

        public PartialViewResult SetNewRole(int? userid, int? roleid)
        {
            RolesUser p = new RolesUser();
            p.RoleID = roleid;
            p.UserID = userid;
            p.Created = DateTime.Now;
            db.RolesUsers.Add(p);

            db.SaveChanges();

            var model = DataRoleUser(userid);
            return PartialView("_roleuser", model);
        }

        public PartialViewResult SetDeleteRoleUser(int? roleid, int? userid)
        {
            var perrole = db.RolesUsers.FirstOrDefault(q => q.UserID == userid && q.RoleID == roleid);
            db.RolesUsers.Remove(perrole);

            db.SaveChanges();

            var model = DataRoleUser(userid);
            return PartialView("_roleuser", model);
        }

        public ActionResult ChangePass(int? userid, string password)
        {
            var u = db.Users.Find(userid);
            u.HashPass = Helpers.GetMD5Hash(password);
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }

        public JsonResult LockOrUnlockAccount(int? userid, int? xaction)
        {
            var u = db.Users.Find(userid);
            if(xaction == 1)
            {
                u.Active = false;
            }
            else
            {
                u.Active = true;
            }
            db.SaveChanges();

            return Json("DONE", JsonRequestBehavior.AllowGet);
        }

    }
}
