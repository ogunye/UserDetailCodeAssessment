using CodeAssessment.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeAssessment.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<UserDetail> UserDetails { get; set; }
    }
}
