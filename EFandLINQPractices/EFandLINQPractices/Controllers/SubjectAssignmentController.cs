using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EFandLINQPractices.Models;

namespace EFandLINQPractices.Controllers
{
    public class SubjectAssignmentController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: /SubjectAssignment/
        public ActionResult Index()
        {
            var subjectassignments = db.SubjectAssignments.Include(s => s.students).Include(s => s.subjects);
            return View(subjectassignments.ToList());
        }

        // GET: /SubjectAssignment/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubjectAssignment subjectassignment = db.SubjectAssignments.Find(id);
            if (subjectassignment == null)
            {
                return HttpNotFound();
            }
            return View(subjectassignment);
        }

        // GET: /SubjectAssignment/Create
        public ActionResult Create()
        {
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "StudentName");
            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName");
            return View();
        }

        // POST: /SubjectAssignment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="StudentID,SubjectId")] SubjectAssignment subjectassignment)
        {
            if (ModelState.IsValid)
            {
                db.SubjectAssignments.Add(subjectassignment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "StudentName", subjectassignment.StudentID);
            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName", subjectassignment.SubjectId);
            return View(subjectassignment);
        }

        // GET: /SubjectAssignment/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubjectAssignment subjectassignment = db.SubjectAssignments.Find(id);
            if (subjectassignment == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "StudentName", subjectassignment.StudentID);
            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName", subjectassignment.SubjectId);
            return View(subjectassignment);
        }

        // POST: /SubjectAssignment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="StudentID,SubjectId")] SubjectAssignment subjectassignment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subjectassignment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "StudentName", subjectassignment.StudentID);
            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName", subjectassignment.SubjectId);
            return View(subjectassignment);
        }

        // GET: /SubjectAssignment/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubjectAssignment subjectassignment = db.SubjectAssignments.Find(id);
            if (subjectassignment == null)
            {
                return HttpNotFound();
            }
            return View(subjectassignment);
        }

        // POST: /SubjectAssignment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SubjectAssignment subjectassignment = db.SubjectAssignments.Find(id);
            db.SubjectAssignments.Remove(subjectassignment);
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
