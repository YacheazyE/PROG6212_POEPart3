using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMCS_v3.Models;
using EntityState = System.Data.Entity.EntityState;


namespace CMCS_v3.Controllers
{
    public class ClaimsController : Controller
    {
        private cmcsDBEntities1 db = new cmcsDBEntities1();

        // GET: Claims
        public ActionResult Index()
        {
            return View(db.Claims.ToList());
        }

        // GET: Claims/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Claim claim = db.Claims.Find(id);
            if (claim == null)
            {
                return HttpNotFound();
            }
            return View(claim);
        }

        // GET: Claims/Create
        public ActionResult Create()
        {
            //var claim = new Claim
            //{
            //    Status = "Pending" 
            //};

            return View();
        }


        // POST: Claims/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClaimID,SubmittedDate,HoursWorked,HourlyRate,Status")] Claim claim, HttpPostedFileBase ClaimDocumentPath)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Handle file upload if there is a file
                    if (ClaimDocumentPath != null && ClaimDocumentPath.ContentLength > 0)
                    {
                        // Generate a unique file name
                        var fileName = Path.GetFileName(ClaimDocumentPath.FileName);
                        var path = Path.Combine(Server.MapPath("~/UploadedFiles"), fileName);

                        // Save the file to the server
                        ClaimDocumentPath.SaveAs(path);

                        // Save the file path in the Claim model
                        claim.ClaimDocumentPath = Path.Combine("/UploadedFiles", fileName);
                    }

                    // Generate a new ClaimID
                    claim.ClaimID = Guid.NewGuid();

                    // Add the claim to the database and save changes
                    db.Claims.Add(claim);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error saving to the database: " + ex.Message);
                }
            }

            return View(claim);
        }

        // GET: Claims/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Claim claim = db.Claims.Find(id);
            if (claim == null)
            {
                return HttpNotFound();
            }
            return View(claim);
        }

        // POST: Claims/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClaimID,SubmittedDate,HoursWorked,HourlyRate,ClaimDocumentPath,Status")] Claim claim)
        {
            if (ModelState.IsValid)
            {
                db.Entry(claim).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(claim);
        }

        // GET: Claims/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Claim claim = db.Claims.Find(id);
            if (claim == null)
            {
                return HttpNotFound();
            }
            return View(claim);
        }

        // POST: Claims/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Claim claim = db.Claims.Find(id);
            db.Claims.Remove(claim);
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
