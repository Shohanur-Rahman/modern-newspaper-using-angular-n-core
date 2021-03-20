using System;
using System.Collections.Generic;
using System.Text;

namespace App.Models.VMModels
{
    public class VMHomeCategoryNews
    {
        public string Category1 { get; set; }
        public IList<VMNewsFrontModel> listOfNewsOne { get; set; }
        public string Category2 { get; set; }
        public IList<VMNewsFrontModel> listOfNewsTwo { get; set; }
        public string Category3 { get; set; }
        public IList<VMNewsFrontModel> listOfNewsThree { get; set; }
    }
}
