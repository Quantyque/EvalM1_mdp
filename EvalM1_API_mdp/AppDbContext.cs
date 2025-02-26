using EvalM1_API_mdp.Model;
using Microsoft.EntityFrameworkCore;

namespace EvalM1_API_mdp
{
    public class AppDbContext : DbContext
    {
        public DbSet<Application> Applications { get; set; }
        public DbSet<Password> Passwords { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Application entity configuration
            modelBuilder.Entity<Application>(entity =>
            {
                entity.HasKey(e => e.IdApplication);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(25);
                entity.Property(e => e.Description).HasMaxLength(255);

                // Relation un-à-un entre Application et Password
                entity.HasOne(a => a.Password)
                      .WithMany(p => p.Applications)  // Un mot de passe peut être associé à plusieurs applications
                      .HasForeignKey(a => a.IdPassword)  // Clé étrangère vers Password
                      .OnDelete(DeleteBehavior.Cascade);  // Si un mot de passe est supprimé, les applications associées seront supprimées

                entity.HasOne(a => a.Type)
                      .WithMany(t => t.Applications)
                      .HasForeignKey(a => a.TypeId);
            });

            // Application entity configuration
            modelBuilder.Entity<Password>(entity =>
            {
                entity.HasKey(e => e.IdPassword);
                entity.Property(e => e.PasswordValue).IsRequired().HasMaxLength(255).IsUnicode(true);
            });

            // Ajout des entrées par défaut dans la table Type (PRO et CLI)
            modelBuilder.Entity<Model.Type>().HasData(
                new Model.Type { TypeCode = "PRO" },
                new Model.Type { TypeCode = "CLI" }
            );
        }
    }
}
