using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WebApi.Models;
using Microsoft.EntityFrameworkCore.Internal;

namespace WebApi.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            DocumentTypeAdd(builder);

        }

        private static void DocumentTypeAdd(ModelBuilder builder)
        {
            builder.Entity<DocumentType>().HasData(
            new DocumentType() { Id = 1, Name = "Registro civil" },
            new DocumentType() { Id = 2, Name = "Tarjeta de identidad" },
            new DocumentType() { Id = 3, Name = "Cedula de ciudadania" },
            new DocumentType() { Id = 4, Name = "Cedula extranjera" }
        );
        }

        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Qualification> Qualifications { get; set; }
    }
}
