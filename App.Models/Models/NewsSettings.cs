using System;
using System.Collections.Generic;
using System.Text;

namespace App.Models.Models
{
    public class NewsPortalSetting
    {
        public int Id { get; set; }
        public long HomeNewsCategory1 { get; set; }
        public long HomeNewsCategory2 { get; set; }
        public long HomeNewsCategory3 { get; set; }
    }
}
