using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace App.Models.Models
{
    [Table("NewsCategoryMapper")]
    public class NewsCategoryMapper
    {
        [Key]
        public long Id { get; set; }
        public long NewsId { get; set; }
        public long CategoryId { get; set; }
    }
}
