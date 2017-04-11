using MVC_HW3.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MVC_HW3.Areas.Bulletin.Controllers
{
    public class BulletinController : Controller
    {
        TravelEntities db = new TravelEntities();
        // GET: Bulletin/Bulletin
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult tBulletinCreate()
        {
            TempData["Date"] = DateTime.Now;
            ViewBag.fBC_ID = new SelectList(db.tBulletinClass, "fBC_ID", "fBC_Class");
            return View();
        }
        [HttpPost]
        public ActionResult tBulletinCreate([Bind(Include = "fBu_ID,fBu_Date,fBC_ID,fBu_Title,fBu_Content")] tBulletin tBulletin)
        {
            if (ModelState.IsValid)
            {

                tBulletin.fBu_Date = DateTime.Now;
                db.tBulletin.Add(tBulletin);
                db.SaveChanges();
                return RedirectToAction("Bulletin", "Home", new { area = "" });
            }

            ViewBag.fBC_ID = new SelectList(db.tBulletinClass, "fBC_ID", "fBC_Class", tBulletin.fBC_ID);
            return View(tBulletin);

        }
        public ActionResult tBulletinEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tBulletin tBulletin = db.tBulletin.Find(id);
            if (tBulletin == null)
            {
                return HttpNotFound();
            }
            ViewBag.fBC_ID = new SelectList(db.tBulletinClass, "fBC_ID", "fBC_Class", tBulletin.fBC_ID);
            return View(tBulletin);
        }

        // POST: tBulletins/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult tBulletinEdit([Bind(Include = "fBu_ID,fBu_Date,fBC_ID,fBu_Title,fBu_Content")] tBulletin tBulletin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tBulletin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Bulletin", "Home", new { area = "" });
            }
            ViewBag.fBC_ID = new SelectList(db.tBulletinClass, "fBC_ID", "fBC_Class", tBulletin.fBC_ID);
            return View(tBulletin);
        }
        public ActionResult tBulletinDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tBulletin tBulletin = db.tBulletin.Find(id);
            if (tBulletin == null)
            {
                return HttpNotFound();
            }
            return View(tBulletin);
        }

        // POST: tBulletins/Delete/5
        [HttpPost, ActionName("tBulletinDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult tBulletinDeleteConfirmed(int id)
        {
            tBulletin tBulletin = db.tBulletin.Find(id);
            db.tBulletin.Remove(tBulletin);
            db.SaveChanges();
            return RedirectToAction("Bulletin", "Home", new { area = "" });
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