using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace App.Models.Models
{
    [Table("NewsTagMapper")]
    public class NewsTagMapper
    {
        [Key]
        public long Id { get; set; }
        public long NewsId { get; set; }
        public long TagId { get; set; }
    }
}
