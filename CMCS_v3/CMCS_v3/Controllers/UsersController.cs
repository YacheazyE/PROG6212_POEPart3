using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMCS_v3.Models;
using EntityState = System.Data.Entity.EntityState;

namespace CMCS_v3.Controllers
{
    public class UsersController : Controller
    {
        private cmcsDBEntities db = new cmcsDBEntities();

        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(Guid? id)
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
            // Creating a list of roles
            var roles = new List<string>
    {
        "Lecturer",
        "Academic Admin",
        "Programme Coordinator"
    };

            // Passing the roles to the view via ViewBag
            ViewBag.Roles = new SelectList(roles);

            return View();
        }


        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,Name,Surname,Role,Email,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                user.UserID = Guid.NewGuid();
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Login");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(Guid? id)
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

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,Name,Surname,Role,Email,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(Guid? id)
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
        public ActionResult DeleteConfirmed(Guid id)
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

        public ActionResult Login()
        {
            return View();
        }
        
        public ActionResult Register()
        {
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password, bool remember)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                // Optionally, add a model error if the email or password is empty
                ModelState.AddModelError("", "Email or password cannot be empty.");
                return View();
            }

            // Find the user in the database
            var user = db.Users.SingleOrDefault(u => u.Email == email);
            if (user == null)
            {
                // If no user found
                ModelState.AddModelError("", "Invalid login attempt.");
                return View();
            }

            // Check if the password is correct
            if (user.Password == password) // Assuming you've stored passwords as plain text for simplicity
            {
                // Check if the user role is "Lecturer"
                if (user.Role == "Lecturer")
                {
                    // Redirect to the Claims Create action
                    return RedirectToAction("Create", "Claims");
                }

                // You can add additional role checks here if needed (e.g. Academic Admin, Programme Coordinator)

                // If the user is not a Lecturer, redirect to a default dashboard or homepage
                return RedirectToAction("Index", "Claims"); // Or any other page you prefer
            }
            else
            {
                // Invalid password
                ModelState.AddModelError("", "Invalid login attempt.");
                return View();
            }
        }
    }
}
