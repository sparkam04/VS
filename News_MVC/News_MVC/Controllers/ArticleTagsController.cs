﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using News_MVC.Models;

using PagedList;
using PagedList.Mvc;

namespace News_MVC.Controllers
{
    public class ArticleTagsController : Controller
    {
        private News_MVCEntities db = new News_MVCEntities();

        // GET: ArticleTags
        [Authorize(Roles = "Admin, Editor")]
        public ActionResult Index()
        {
            var articleTags = db.ArticleTags.Include(a => a.Articles).Include(a => a.Tags);

            return View(articleTags.ToList());
        }

        // GET: Article list by Tags
        public ActionResult TagArticleList(int? id,int? page)
        {
            var articleTags = db.ArticleTags.Include(a => a.Articles).Include(a => a.Tags);

           
            ////ViewBag.Message = "Articles by Tag " + articleTags.Where(s=> s.TagID==id).Select(s => s.Tags.TagName).First();
            //var Tag = articleTags.First(s => s.TagID == id);
            //ViewBag.Tag = Tag.Tags.TagName;

            //return View(articleTags.Where(s => s.TagID == id).ToList());
     
                var Tag = db.Tags.Find(id).TagName ;
                ViewBag.Tag = Tag;
                ViewData["Tags"] = db.Tags.ToList();

                return View(articleTags.Where(s => s.TagID == id).OrderByDescending(s => s.Articles.CreationDate).ToList().ToPagedList(page ?? 1, 10));
        }

        // GET: ArticleTags/Details/5
        [Authorize(Roles = "Admin, Editor")]
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
        [Authorize(Roles = "Admin, Editor")]
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
        [Authorize(Roles = "Admin, Editor")]
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

        // GET: ArticleTags/Edit/5
        [Authorize(Roles = "Admin, Editor")]
        public ActionResult Edit_(int? id)
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

            var tl = db.Tags.Select(a => a.TagID).ToList().Select(x => new SelectListItem()
            {
                Selected = db.Tags.Select(a => a.TagID).ToList().Contains(2),
                Text = "111",
                Value = "222"
            });
            
            var tba =articleTags.Tags.ArticleTags.Select(s=> s.Tags.TagName).ToList();
            var ls2 = new SelectList(db.Tags.Select(a => a.TagID).ToList(), "TagID", "TagName", articleTags.TagID).Select(x => new SelectListItem()
            {
                Selected = db.Tags.Select(a => a.TagID).ToList().Contains(1), //userRoles.Contains(x.Name),
                Text = x.Text,
                Value = x.Value
            });



            var ls = new SelectList(db.Tags, "TagID", "TagName", articleTags.TagID).Select(x => new SelectListItem()
            {
                Selected = x.Selected, //userRoles.Contains(x.Name),
                Text = x.Text,
                Value = x.Value
            });

            ViewData["TagID_"] = ls;

            ViewBag.Tags = db.ArticleTags.ToList();
            return View(articleTags);
        }

        // POST: ArticleTags/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_([Bind(Include = "Id,ArticleID,TagID")] ArticleTags articleTags, params int[] selectedTag)
        {
            if (ModelState.IsValid)
                {
                    foreach (var item in selectedTag)
                    {
                        articleTags.TagID = item;

                        db.Entry(articleTags).State = EntityState.Modified;
                        db.SaveChanges();
                    }           //return RedirectToAction("Index");
                    return RedirectToAction("Index");
                }
            
            ViewBag.ArticleID = new SelectList(db.Articles, "ArticleID", "AuthorID", articleTags.ArticleID);
            ViewBag.TagID = new SelectList(db.Tags, "TagID", "TagName", articleTags.TagID);
            return View(articleTags);
        }

        // GET: ArticleTags/Delete/5
        [Authorize(Roles = "Admin, Editor")]
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
