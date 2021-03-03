using System;
using System.Collections.Generic;
using System.Text;

namespace App.Models.VMModels
{
    public class VMNewsFrontModel:VMBreakingNews
    {
        public string FeaturedImage { get; set; }
        public VMNewsCategory CategoryInfo { get; set; }
        public DateTime? CreatedDate { get; set; }
    }

    public class VMNewsCategory
    {
        public string CategorySlug { get; set; }
        public string Category { get; set; }
    }
}
