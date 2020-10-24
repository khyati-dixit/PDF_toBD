using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Pdf_storedInDB.EntityFramework;

namespace Pdf_storedInDB.Controllers
{
    public class Pdf_DataController : Controller
    {
        private khyatiDBEntities db = new khyatiDBEntities();

        // GET: Pdf_Data
        public ActionResult Index()
        {
            return View(db.Pdf_Data.ToList());
        }

        // GET: Pdf_Data/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pdf_Data pdf_Data = db.Pdf_Data.Find(id);
            if (pdf_Data == null)
            {
                return HttpNotFound();
            }
            return View(pdf_Data);
        }

        // GET: Pdf_Data/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pdf_Data/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pdf_Data pdf_Data, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                string path = Path.Combine(Server.MapPath("~/PDF"), Path.GetFileName(file.FileName));
                file.SaveAs(path);
                db.Pdf_Data.Add(new Pdf_Data
                {
                    Id = pdf_Data.Id,
                    Data = "~/PDF/" + file.FileName
                });
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pdf_Data);
        }

        // GET: Pdf_Data/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pdf_Data pdf_Data = db.Pdf_Data.Find(id);
            if (pdf_Data == null)
            {
                return HttpNotFound();
            }
            return View(pdf_Data);
        }

        // POST: Pdf_Data/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Data")] Pdf_Data pdf_Data)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pdf_Data).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pdf_Data);
        }

        // GET: Pdf_Data/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pdf_Data pdf_Data = db.Pdf_Data.Find(id);
            if (pdf_Data == null)
            {
                return HttpNotFound();
            }
            return View(pdf_Data);
        }

        // POST: Pdf_Data/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pdf_Data pdf_Data = db.Pdf_Data.Find(id);
            db.Pdf_Data.Remove(pdf_Data);
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
