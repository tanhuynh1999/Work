using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsiteWork.Models;

namespace WebsiteWork.Controllers.Three
{
    public class SubmitsController : Controller
    {
        private DataWorkEntities db = new DataWorkEntities();

        // GET: Submits
        public ActionResult Index()
        {
            var submits = db.Submits.Include(s => s.Cv).Include(s => s.User).Include(s => s.Work);
            return View(submits.ToList());
        }

        // GET: Submits/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Submit submit = db.Submits.Find(id);
            if (submit == null)
            {
                return HttpNotFound();
            }
            return View(submit);
        }

        // GET: Submits/Create
        public ActionResult Create()
        {
            ViewBag.cv_id = new SelectList(db.Cvs, "cv_id", "cv_fullname");
            ViewBag.submit_id = new SelectList(db.Users, "user_id", "user_login");
            ViewBag.work_id = new SelectList(db.Works, "work_id", "work_name");
            return View();
        }

        // POST: Submits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "submit_id,user_id,work_id,submit_datecreated,cv_id")] Submit submit)
        {
            if (ModelState.IsValid)
            {
                db.Submits.Add(submit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cv_id = new SelectList(db.Cvs, "cv_id", "cv_fullname", submit.cv_id);
            ViewBag.submit_id = new SelectList(db.Users, "user_id", "user_login", submit.submit_id);
            ViewBag.work_id = new SelectList(db.Works, "work_id", "work_name", submit.work_id);
            return View(submit);
        }

        // GET: Submits/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Submit submit = db.Submits.Find(id);
            if (submit == null)
            {
                return HttpNotFound();
            }
            ViewBag.cv_id = new SelectList(db.Cvs, "cv_id", "cv_fullname", submit.cv_id);
            ViewBag.submit_id = new SelectList(db.Users, "user_id", "user_login", submit.submit_id);
            ViewBag.work_id = new SelectList(db.Works, "work_id", "work_name", submit.work_id);
            return View(submit);
        }

        // POST: Submits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "submit_id,user_id,work_id,submit_datecreated,cv_id")] Submit submit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(submit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cv_id = new SelectList(db.Cvs, "cv_id", "cv_fullname", submit.cv_id);
            ViewBag.submit_id = new SelectList(db.Users, "user_id", "user_login", submit.submit_id);
            ViewBag.work_id = new SelectList(db.Works, "work_id", "work_name", submit.work_id);
            return View(submit);
        }

        // GET: Submits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Submit submit = db.Submits.Find(id);
            if (submit == null)
            {
                return HttpNotFound();
            }
            return View(submit);
        }

        // POST: Submits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Submit submit = db.Submits.Find(id);
            db.Submits.Remove(submit);
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
