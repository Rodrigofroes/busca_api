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
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<AcademiaPersonal> AcademiaPersonais { get; set; }
    }
}