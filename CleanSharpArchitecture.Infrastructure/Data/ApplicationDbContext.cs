using CleanSharpArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanSharpArchitecture.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Conjunto de usuários no banco de dados.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Configurações do modelo de dados.
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurações adicionais para as entidades, se necessário
            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(256);

            modelBuilder.Entity<User>()
                .Property(u => u.Password)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<User>()
                .Property(u => u.ProfileImageUrl)
                .HasMaxLength(500);

            modelBuilder.Entity<User>()
                .Property(u => u.Biography)
                .HasMaxLength(1000);
        }
    }
}