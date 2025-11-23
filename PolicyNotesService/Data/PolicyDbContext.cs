using Microsoft.EntityFrameworkCore;
using PolicyNotesService.Model;

namespace PolicyNotesService.Data
{
    public class PolicyDbContext : DbContext
    {
        public PolicyDbContext(DbContextOptions<PolicyDbContext> options) : base(options)
        {
        }

        public DbSet<Policy> Policies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Policy>().HasData(
                new Policy
                {
                    Id = 1,
                    PolicyNumber = "PN1",
                    Note = "Note"
                },
                new Policy
                {
                    Id = 2,
                    PolicyNumber = "PN2",
                    Note = "Note"
                }
            );
        }
    }
}