using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using News_MVC.Models;

namespace News_MVC.Controllers
{
    public class ArticleTagsController : Controller
    {
        private News_MVCEntities db = new News_MVCEntities();

        // GET: ArticleTags
        [Authorize]
        public ActionResult Index()
        {
            var articleTags = db.ArticleTags.Include(a => a.Articles).Include(a => a.Tags);
            return View(articleTags.ToList());
        }

        // GET: Article list by Tags
        public ActionResult TagArticleList(int? id)
        {
            var articleTags = db.ArticleTags.Include(a => a.Articles).Include(a => a.Tags);

           
            ////ViewBag.Message = "Articles by Tag " + articleTags.Where(s=> s.TagID==id).Select(s => s.Tags.TagName).First();
            //var Tag = articleTags.First(s => s.TagID == id);
            //ViewBag.Tag = Tag.Tags.TagName;

            //return View(articleTags.Where(s => s.TagID == id).ToList());
     
                var Tag = db.Tags.Find(id).TagName ;
                ViewBag.Tag = Tag;
               

            return View(articleTags.Where(s => s.TagID == id).ToList());
        }

        // GET: ArticleTags/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticleTags articleTags = db.ArticleTags.Find(id);
            if (articleTags == null)
            {
                return HttpNotFound();
            }
            return View(articleTags);
        }

        // GET: ArticleTags/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.ArticleID = new SelectList(db.Articles, "ArticleID", "ArticleName");
            ViewBag.TagID = new SelectList(db.Tags, "TagID", "TagName");
            return View();
        }

        // POST: ArticleTags/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ArticleID,TagID")] ArticleTags articleTags)
        {
            if (ModelState.IsValid)
            {
                db.ArticleTags.Add(articleTags);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArticleID = new SelectList(db.Articles, "ArticleID", "AuthorID", articleTags.ArticleID);
            ViewBag.TagID = new SelectList(db.Tags, "TagID", "TagName", articleTags.TagID);
            return View(articleTags);
        }

        // GET: ArticleTags/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticleTags articleTags = db.ArticleTags.Find(id);
            if (articleTags == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArticleID = new SelectList(db.Articles, "ArticleID", "ArticleName", articleTags.ArticleID);
            ViewBag.TagID = new SelectList(db.Tags, "TagID", "TagName", articleTags.TagID);
            return View(articleTags);
        }

        // POST: ArticleTags/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ArticleID,TagID")] ArticleTags articleTags)
        {
            if (ModelState.IsValid)
            {
                db.Entry(articleTags).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArticleID = new SelectList(db.Articles, "ArticleID", "AuthorID", articleTags.ArticleID);
            ViewBag.TagID = new SelectList(db.Tags, "TagID", "TagName", articleTags.TagID);
            return View(articleTags);
        }

        // GET: ArticleTags/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticleTags articleTags = db.ArticleTags.Find(id);
            if (articleTags == null)
            {
                return HttpNotFound();
            }
            return View(articleTags);
        }

        // POST: ArticleTags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ArticleTags articleTags = db.ArticleTags.Find(id);
            db.ArticleTags.Remove(articleTags);
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
