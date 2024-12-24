using BackAppPersonal.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackAppPersonal.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> option) : base(option)
        {
        }
        public DbSet<Academia> Academias { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Personal> Personais { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<AcademiaPersonal> AcademiaPersonais { get; set; }
        public DbSet<TipoUsuario> TipoUsuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TipoUsuario>().HasData(
                new TipoUsuario { Id = Guid.NewGuid(), TIpo = "Aluno" },
                new TipoUsuario { Id = Guid.NewGuid(), TIpo = "Academia" },
                new TipoUsuario { Id = Guid.NewGuid(), TIpo = "Personal" }
            );
        }
    }
}