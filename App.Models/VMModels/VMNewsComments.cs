using System;
using System.Collections.Generic;
using System.Text;

namespace App.Models.VMModels
{
    public class VMNewsComments
    {
        public long Id { get; set; }
        public long? NewsId { get; set; }
        public long? ReplyId { get; set; }
        public long UserId { get; set; }
        public DateTime? CommentsDate { get; set; }
        public string Comments { get; set; }
        public UserLinkedProfile Commentor { get; set; }
        public IList<VMNewsComments> ListOfReply { get; set; }
        public string CommentsPath { get; set; }
        public bool IsApprove { get; set; }
        public string CommentorName { get; set; }
        public long? CreatedId { get; set; }
        public long? EditedId { get; set; }
        public DateTime? EditedDate { get; set; }

    }
}
