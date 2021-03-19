using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace App.Models.Models
{
    [Table("NewsComments")]
    public class NewsComments : BaseTableInfo
    {
        [Key]
        public long Id { get; set; }
        public long? NewsId { get; set; }
        public long? ReplyId { get; set; }
        public long UserId { get; set; }
        public string Comments { get; set; }
        public bool IsApprove { get; set; }
    }
}
