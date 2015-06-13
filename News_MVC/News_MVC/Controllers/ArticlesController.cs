using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using News_MVC.Models;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

using PagedList;
using PagedList.Mvc;

namespace News_MVC.Controllers
{
    public class ArticlesController : Controller
    {
        private News_MVCEntities db = new News_MVCEntities();

        // GET: Articles
        [Authorize]
        public ActionResult Index(int? page)
        {
            var articles = db.Articles.Include(a => a.AspNetUsers);
            return View(articles.OrderByDescending(s => s.CreationDate).ToList().ToPagedList(page ?? 1, 3));
        }

        // GET: Articles
        public ActionResult ArticleList(int? page)
        {
            var articles = db.Articles.Include(a => a.AspNetUsers);
            
            var l1 = articles.Where(s => s.ToHomePage == true && (s.ToArchive == false) && (s.Priority != null)).OrderBy(s => s.Priority).ToList();
            var l2 = articles.Where(s => s.ToHomePage == true && (s.ToArchive == false)).OrderByDescending(s => s.CreationDate).ToList();

            //ViewBag.Tags = db.Tags.ToList();
            ViewData["Tags"] = db.Tags.ToList();
            return View(l1.Union(l2).ToPagedList(page ?? 1, 3));
            //return View(articles.Where(s => s.ToHomePage == true && (s.ToArchive == false)).OrderByDescending(s => s.CreationDate).ToList().ToPagedList(page ?? 1, 3));
        }


        // GET: Articles in Archive
        public ActionResult ArchiveList(int? page)
        {
            var articles = db.Articles.Include(a => a.AspNetUsers);

            ViewData["Tags"] = db.Tags.ToList();
            return View(articles.Where(s => s.ToArchive == true).OrderByDescending(s => s.CreationDate).ToList().ToPagedList(page ?? 1, 3));
        }


        // GET: Articles/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articles articles = db.Articles.Find(id);
            if (articles == null)
            {
                return HttpNotFound();
            }
            return View(articles);
        }

        // GET: Articles/Details/5   Content
        public ActionResult ArticleContent(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articles articles = db.Articles.Find(id);
            if (articles == null)
            {
                return HttpNotFound();
            }


            return View(articles);
        }

        // GET: Articles/Create
        [Authorize]
        public ActionResult Create()
        {
            //var userId = User.Identity.GetUserId();
            //var userName = User.Identity.Name;
            //ViewBag.AuthorID = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArticleID,AuthorID,ArticleName,ArticleContent,CreationDate,ToHomePage,ToArchive,Priority")] Articles articles)
        {
            if (ModelState.IsValid)
            {
                articles.CreationDate = DateTime.Now; ///
                articles.AuthorID = User.Identity.GetUserId();///

                db.Articles.Add(articles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorID = new SelectList(db.AspNetUsers, "Id", "Email", articles.AuthorID);
            return View(articles);
        }

        // GET: Articles/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articles articles = db.Articles.Find(id);
            if (articles == null)
            {
                return HttpNotFound();
            }

            ViewBag.AuthorID = db.AspNetUsers.Find(articles.AuthorID).Email;
            //ViewBag.AuthorID = new SelectList(db.AspNetUsers, "Id", "Email", articles.AuthorID);
            return View(articles);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArticleID,AuthorID,ArticleName,ArticleContent,CreationDate,ToHomePage,ToArchive,Priority")] Articles articles)
        {
  
            if (ModelState.IsValid)
            {
                articles.DateModitied = DateTime.Now;
                                
                db.Entry(articles).State = EntityState.Modified;
                db.Entry(articles).Property("CreationDate").IsModified = false;
                db.Entry(articles).Property("AuthorID").IsModified = false;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            ViewBag.AuthorID = new SelectList(db.AspNetUsers, "Id", "Email", articles.AuthorID);
            return View(articles);
        }

        // GET: Articles/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articles articles = db.Articles.Find(id);
            if (articles == null)
            {
                return HttpNotFound();
            }
            return View(articles);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Articles articles = db.Articles.Find(id);
            db.Articles.Remove(articles);
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
