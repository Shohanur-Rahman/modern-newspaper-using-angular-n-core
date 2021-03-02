using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace App.Models.Models
{
    [Table("NewsTags")]
    public class NewsTags:BaseTableInfo
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
