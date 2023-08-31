
using Microsoft.EntityFrameworkCore;
using Mensajeria_Windows.EntityFramework.Entities;

namespace Mensajeria_Windows.EntityFramework.Data
{
    public class NotificationContext : DbContext
    {


        protected readonly IConfiguration _configuration;

        public NotificationContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public DbSet<Agencia> Agencias { get; set; }
        public DbSet<InfoTeams> infoTeams { get; set; }
        public DbSet<InfoWhatsApp> infoWhatsApp { get; set; }
        public DbSet<InfoEmail>    infoEmail { get; set; }
        public DbSet<InfoSMS> infoSMS { get; set; }
        public DbSet<Administradores> administradores { get; set; }
        public DbSet<Plantillas> Plantilla { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("PostgreSQL"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agencia>()
                .HasMany(t => t.InfoTeams)
                .WithOne(a => a.agencia)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Agencia>()
                .HasMany(t => t.InfoWhatsApps)
                .WithOne(a => a.agencia)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Agencia>()
                .HasMany(e => e.InfoEmail)
                .WithOne(a => a.agencia)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Agencia>()
                .HasMany(e => e.InfoSMS)
                .WithOne(a => a.agencia)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Administradores>();
            modelBuilder.Entity<Plantillas>();
        }

    }

}
