using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Models.VMModels
{
    public class VMNewsModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string FeaturedImage { get; set; }
        public bool IsPublished { get; set; }
        public bool IsBreaking { get; set; }
        public long CategoryId { get; set; }
        public long? CreatedId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? EditedId { get; set; }
        public DateTime? EditedDate { get; set; }
        public IFormFile Image { get; set; }
        public string[] Tags { get; set; }
        public string[] Categories { get; set; }
    }
}
