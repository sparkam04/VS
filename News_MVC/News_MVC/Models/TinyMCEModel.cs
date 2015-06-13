using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace News_MVC.Models {

    public class TinyMCEModel {

        [AllowHtml]
        [UIHint("tinymce_full_compressed")]
        public string Content { get; set; }

    }
}