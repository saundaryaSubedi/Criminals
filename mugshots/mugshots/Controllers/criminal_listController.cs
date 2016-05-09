using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mugshots.dal;
using System.IO;

namespace mugshots.Controllers
{
    [Authorize]
    public class criminal_listController : Controller
    {
        private mugshotsEntities db = new mugshotsEntities();

        // GET: criminal_list
        public ActionResult Index()
        {
            return View(db.criminal_list.ToList());
        }

        // GET: criminal_list/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            criminal_list criminal_list = db.criminal_list.Find(id);
            if (criminal_list == null)
            {
                return HttpNotFound();
            }
            return View(criminal_list);
        }

        // GET: criminal_list/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: criminal_list/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(criminal_list criminal_list, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                    image.SaveAs(Server.MapPath("~/Image/" + filename));
                    criminal_list.image_path = filename;
                }
                db.criminal_list.Add(criminal_list);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(criminal_list);
        }
        // GET: criminal_list/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            criminal_list criminal_list = db.criminal_list.Find(id);
            if (criminal_list == null)
            {
                return HttpNotFound();
            }
            return View(criminal_list);
        }

        // POST: criminal_list/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,name,address,image_path,Arrest_Agency,Arrest_Date,Arrest_Time,DOB,Eyes,Hair,Build,Current_Age,Height,Weight,Charge_Description,Class,Court,DISP,Charge_Type")] criminal_list criminal_list)
        {
            if (ModelState.IsValid)
            {
                db.Entry(criminal_list).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(criminal_list);
        }

        // GET: criminal_list/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            criminal_list criminal_list = db.criminal_list.Find(id);
            if (criminal_list == null)
            {
                return HttpNotFound();
            }
            return View(criminal_list);
        }

        // POST: criminal_list/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            criminal_list criminal_list = db.criminal_list.Find(id);
            db.criminal_list.Remove(criminal_list);
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
