using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using EMA.Logics.Models;
using PagedList;

namespace EMA.WebApp.Controllers
{
    public class ApplicationsController : Controller
    {
        private EMADBEntities db = new EMADBEntities();

        // POST: Get the list of all applications or search by Name or Acronym
        public ActionResult Index(string search, int? page, string sortOrder, string currentFilter)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.AcronymSortParm = sortOrder == "Acronym" ? "acronym_desc" : "Acronym";
            ViewBag.VersionSortParm = sortOrder == "Version" ? "version_desc" : "Version";
            ViewBag.StatusSortParm = sortOrder == "Status" ? "status_desc" : "Status";

            if (search == null) {
                search = currentFilter;
            } else
            {
                page = 1;
            }

            ViewBag.CurrentFilter = search;

            var application = from s in db.Application
                              select s;
            if (!String.IsNullOrEmpty(search))
            {
                application = application.Where(a => a.Name.ToLower().Contains(search.ToLower()) ||
                            a.Acronym.ToLower().Contains(search.ToLower()) ||
                            search == "");
            }
            switch (sortOrder)
            {
                case "name_desc":
                    application = application.OrderByDescending(s => s.Name);
                    break;
                case "Acronym":
                    application = application.OrderBy(s => s.Acronym);
                    break;
                case "acronym_desc":
                    application = application.OrderByDescending(s => s.Acronym);
                    break;
                case "Version":
                    application = application.OrderBy(s => s.Version);
                    break;
                case "version_desc":
                    application = application.OrderByDescending(s => s.Version);
                    break;
                case "Status":
                    application = application.OrderBy(s => s.Status);
                    break;
                case "status_desc":
                    application = application.OrderByDescending(s => s.Status);
                    break;
                default:  
                    application = application.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 30;
            int pageNumber = (page ?? 1);
            return View(application.ToPagedList(pageNumber, pageSize));
        }

        // GET: Applications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Application.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // GET: Applications/Create
        public ActionResult Create()
        {
            ViewBag.IdVendor = new SelectList(db.Vendor, "Id", "Acronym");
            return View();
        }

        // POST: Applications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Acronym,Name,Version,Owner,SecurityModelUrl,CartaUrl,RiskAssessmentUrl,SplunkUrl,Core,AppTier,UserCount,LicenseType,Status,Description,IdVendor")] Application application)
        {
            if (ModelState.IsValid)
            {
                db.Application.Add(application);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdVendor = new SelectList(db.Vendor, "Id", "Acronym", application.IdVendor);
            return View(application);
        }

        // GET: Applications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Application.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdVendor = new SelectList(db.Vendor, "Id", "Acronym", application.IdVendor);
            return View(application);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Acronym,Name,Version,Owner,SecurityModelUrl,CartaUrl,RiskAssessmentUrl,SplunkUrl,Core,AppTier,UserCount,LicenseType,Status,Description,IdVendor")] Application application)
        {
            if (ModelState.IsValid)
            {
                db.Entry(application).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdVendor = new SelectList(db.Vendor, "Id", "Acronym", application.IdVendor);
            return View(application);
        }

        // GET: Applications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Application.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // POST: Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Application application = db.Application.Find(id);
            db.Application.Remove(application);
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
