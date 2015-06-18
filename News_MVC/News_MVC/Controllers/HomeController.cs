using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using System.Data;
using System.Data.Entity;
using System.Net;
using News_MVC.Models;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;


using PagedList;
using PagedList.Mvc;

namespace News_MVC.Controllers
{
    public class HomeController : Controller
    {
        private News_MVCEntities db = new News_MVCEntities();

        public ActionResult Index(int? page)
        {
            var articles = db.Articles.Include(a => a.AspNetUsers);

            var l1 = articles.Where(s => s.ToHomePage == true && (s.ToArchive == false) && (s.Priority > 0)).OrderBy(s => s.Priority).ToList();
            var l2 = articles.Where(s => s.ToHomePage == true && (s.ToArchive == false) && (s.Priority == null)).OrderByDescending(s => s.CreationDate).ToList();

            //ViewBag.Tags = db.Tags.ToList();
            ViewData["Tags"] = db.Tags.ToList();

            //ViewBag.FeatArticle = db.Articles.Where(s => s.ToHomePage == true && (s.ToArchive == false) && (s.Priority <= 0)).OrderBy(s => s.Priority).ToList();

            //return View(l1.Union(l2).ToPagedList(page ?? 1, 10));
            return View(articles.Where(s => s.ToHomePage == true && (s.ToArchive == false)).OrderByDescending(s => s.CreationDate).ToList().ToPagedList(page ?? 1, 10));
         
            
            //return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Это сайт новостей команды CodeHunters.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            
            return View();
        }


    }
}