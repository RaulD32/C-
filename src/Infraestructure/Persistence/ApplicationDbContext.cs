
using ApplicationCore.Interfaces;
using Domain.Entities;
using Finbuckle.MultiTenant;
using Infraestructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence
{
    public class ApplicationDbContext : BaseDbContext
    {
        public ApplicationDbContext(ITenantInfo currentTenant, DbContextOptions options, ICurrentUserService currentUser)
            : base(currentTenant, options, currentUser)
        {

        }

        public DbSet<persona> persona {  get; set; }
        public DbSet<logs> logs { get; set; }
        public DbSet<jugador> jugador { get; set; }
        public DbSet<Estudiante> estudiantes { get; set; }

        public DbSet<Colaborador> Colaboradores { get; set; }
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<Admin> Administrativos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {
            modelBuilder.Entity<Profesor>()
           .HasOne(p => p.Colaborador)
           .WithOne(c => c.Profesor)
           .HasForeignKey<Profesor>(p => p.FkColaborador);

            modelBuilder.Entity<Admin>()
                .HasOne(a => a.Colaborador)
                .WithOne(c => c.Admin)
                .HasForeignKey<Admin>(a => a.FkColaborador);
            base.OnModelCreating(modelBuilder);
        }      
    }
}
