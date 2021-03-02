using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace App.Models.Models
{
    [Table("NewsCategories")]
    public class NewsCategory:BaseTableInfo
    {
        [Key]
        public long Id { get; set; }
        public string CategoryName { get; set; }
        public string Slug { get; set; }
        public long? ParentId { get; set; }
        public bool IsPublished { get; set; }
    }
}
