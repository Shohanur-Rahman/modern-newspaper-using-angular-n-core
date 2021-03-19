using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace App.Models.Models
{
    [Table("NewsPosts")]
    public class NewsPost:BaseTableInfo
    {
        [Key]
        public long Id { get; set; }
        public string Title { get; set; }
        public long CategoryId { get; set; }
        public string TitleSlug { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string FeaturedImage { get; set; }
        public string VideoURL { get; set; }
        public bool IsPublished { get; set; }
        public bool IsBreaking { get; set; }
    }
}
