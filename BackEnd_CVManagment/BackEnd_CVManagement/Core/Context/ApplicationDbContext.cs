using BackEnd_CVManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_CVManagement.Core.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Degree> Degrees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Candidate>()
                .HasOne(candidate => candidate.Degree)
                .WithMany(degree => degree.Candidates)
                .HasForeignKey(candidate => candidate.DegreeId);
        }
    }
}
