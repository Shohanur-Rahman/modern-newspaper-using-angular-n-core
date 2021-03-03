using System;
using System.Collections.Generic;
using System.Text;

namespace App.Models.VMModels
{
    public class VMNewsComments
    {
        public DateTime? CommentsDate { get; set; }
        public string Comments { get; set; }
        public UserLinkedProfile Commentor { get; set; }
        public IList<VMNewsComments> ListOfReply { get; set; }
    }
}
