using System;
using System.Collections.Generic;
using System.Text;

namespace App.Models.VMModels
{
    public class VMNewsChildModel
    {
        public long Id { get; set; }
        public string CategoryName { get; set; }
        public string TagName { get; set; }
        public bool IsPublished { get; set; }
        public int TotalNews { get; set; }
        public int TotalComments { get; set; }
        public string Parent { get; set; }
        public long? ParentId { get; set; }
        public long? CreatedId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? EditedId { get; set; }
        public DateTime? EditedDate { get; set; }
    }

    public class NewsCategoryTree
    {
        public long Id { get; set; }
        public string CategoryName { get; set; }
        public IList<NewsCategoryTree> ChildCategory { get; set; }
    }
}
