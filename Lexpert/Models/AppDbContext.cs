using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lexpert.Models
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<AboutUs> AboutUs { get; set; }
        public DbSet<BlogPost> BlogPost { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<EmailSetting> EmailSetting { get; set; }
        public DbSet<MailBodyContent> MailBodyContent { get; set; }
        public DbSet<Privacy> Privacy { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<SiteSetting> SiteSetting { get; set; }
        public DbSet<Warranty> Warranty { get; set; }
        public DbSet<NavlinkSetup> NavlinkSetup { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


        }
    }
}