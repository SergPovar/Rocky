using Microsoft.EntityFrameworkCore;
using Rocky_Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Rocky_DataAccess
{
    public class RockyDbContext : IdentityDbContext
    {
        public RockyDbContext(DbContextOptions<RockyDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<ApplicationType> ApplicationType { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<InquiryHeader> InquiryHeaders { get; set; }
        public DbSet<InquiryDetail> InquiryDetail { get; set; }
    }
}
