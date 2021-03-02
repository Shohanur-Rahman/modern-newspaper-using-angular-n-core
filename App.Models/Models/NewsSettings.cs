using System;
using System.Collections.Generic;
using System.Text;

namespace App.Models.Models
{
    public class NewsSettings
    {
        public int Id { get; set; }
        public long BreakingNewsCategoryId { get; set; }
        public long HeilightNewsCategoryId { get; set; }
        public long ThreeColFirstCategoryId { get; set; }
        public long ThreeColSecondCategoryId { get; set; }
        public long ThreeColThirdCategoryId { get; set; }
    }
}
