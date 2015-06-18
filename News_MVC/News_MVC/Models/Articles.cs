//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace News_MVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.ComponentModel.DataAnnotations;

    public partial class Articles
    {
        public Articles()
        {
            this.ArticleTags = new HashSet<ArticleTags>();
        }
    
        public int ArticleID { get; set; }
        public string AuthorID { get; set; }
        public string ArticleName { get; set; }
        [AllowHtml]
        [UIHint("tinymce_full_compressed")]
        public string ArticleContent { get; set; }
        public System.DateTime CreationDate { get; set; }
        public bool ToHomePage { get; set; }
        public bool ToArchive { get; set; }
        public Nullable<System.DateTime> DateModitied { get; set; }
        public Nullable<int> Priority { get; set; }
        public string Summary { get; set; }
        public string ImgURL { get; set; }
    
        public virtual AspNetUsers AspNetUsers { get; set; }
        public virtual ICollection<ArticleTags> ArticleTags { get; set; }
    }
}
