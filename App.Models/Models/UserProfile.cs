using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace App.Models.Models
{
    [Table("UserProfiles")]
    public class UserProfile : BaseTableInfo
    {
        [Key]
        public long Id { get; set; }
        public long UserId { get; set; }
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SecondaryEmail { get; set; }
        public string PrimaryMobile { get; set; }
        public string SecondaryMobile { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public string Road { get; set; }
        public string Country { get; set; }
        public string AddressDetails { get; set; }
        public string NID { get; set; }
        public string NIDPhoto { get; set; }
        public string ProfilePicture { get; set; }
        public string UserBio { get; set; }
        public string ProfileDescription { get; set; }
    }
}
