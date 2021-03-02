using System;
using System.Collections.Generic;
using System.Text;

namespace App.Models.VMModels
{
    public class VMBreakingNews
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string CategorySlug { get; set; }
        public string TitleSlug { get; set; }
    }
}
