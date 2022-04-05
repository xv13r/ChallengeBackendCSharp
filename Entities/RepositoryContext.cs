using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options)
            : base(options)
        { 

           this.Database.EnsureCreatedAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //many-to-many
            //modelBuilder.Entity<Acted>().HasKey(x => new { x.CharacterId, x.ContentId });

            //modelBuilder.Entity<Acted>()
            //     .HasOne(c => c.Character)
            //     .WithMany(c => c.Contents)
            //     .HasForeignKey(c => c.CharacterId);

            //modelBuilder.Entity<Acted>()
            //    .HasOne(c => c.Content)
            //    .WithMany(c => c.Characters)
            //    .HasForeignKey(c => c.ContentId);
        }

        public DbSet<Character> Characters { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
