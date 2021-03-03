using App.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Models.VMModels
{
    public class VMNewsDetailsModel: VMNewsModel
    {
        public IList<NewsTags> ListOfTag { get; set; }
        public IList<NewsCategory> ListOfCategories { get; set; }
        public UserLinkedProfile ReporterProfile { get; set; }
        public IList<VMNewsFrontModel> RelatedNews { get; set; }
        public IList<VMNewsComments> ListOfComments { get; set; }
    }

    public class UserLinkedProfile
    {
        public long ProfileId { get; set; }
        public string ProfilePicture { get; set; }
        public string UserBio { get; set; }
        public string DisplayName { get; set; }
    }
}
