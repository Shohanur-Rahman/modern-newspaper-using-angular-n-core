using System;
using System.Collections.Generic;
using System.Text;

namespace App.Models.VMModels
{
    public class VMHomeNewsViews
    {
        public IList<VMBreakingNews> BreakingNews { get; set; }
        public IList<VMNewsFrontModel> RecentThree { get; set; }
        public IList<VMNewsFrontModel> TrendingNewsFirstThree { get; set; }
        public IList<VMNewsFrontModel> TrendingNewsSecondTwo { get; set; }
        public IList<VMNewsFrontModel> TrendingNewsThirdPart { get; set; }
        public IList<VMNewsFrontModel> TrendingNewsFourthPart { get; set; }
        public IList<VMNewsFrontModel> VideoNews { get; set; }
        public VMHomeCategoryNews ListOfCategoryNews { get; set; }
    }
}
