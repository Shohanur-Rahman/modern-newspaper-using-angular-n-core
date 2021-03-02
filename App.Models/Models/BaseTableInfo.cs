using System;
using System.Collections.Generic;
using System.Text;

namespace App.Models.Models
{
    public class BaseTableInfo
    {
        public long? CreatedId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? EditedId { get; set; }
        public DateTime? EditedDate { get; set; }
    }
}
