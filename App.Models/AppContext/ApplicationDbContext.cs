using App.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Models.AppContext
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }

        public DbSet<Users> User { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserProfile> UserProfile { get; set; }

        public DbSet<NewsCategory> NewsCategories { get; set; }
        public DbSet<NewsCategoryMapper> NewsCategoryMapper { get; set; }
        public DbSet<NewsPost> NewsPost { get; set; }
        public DbSet<NewsTagMapper> NewsTagMapper { get; set; }
        public DbSet<NewsTags> NewsTags { get; set; }
        public DbSet<NewsComments> NewsComment { get; set; }
    }
}
