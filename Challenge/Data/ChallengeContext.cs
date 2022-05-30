using DataEF;
using Microsoft.EntityFrameworkCore;

namespace Challenge.Data
{
    public class ChallengeContext : DbContext
    {
        public ChallengeContext (DbContextOptions<ChallengeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Permisos> Permisos { get; set; }
        public virtual DbSet<TipoPermisos> TipoPermisos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permisos>().ToTable("Permisos");
            modelBuilder.Entity<Permisos>().HasKey(c => c.Id);
            modelBuilder.Entity<Permisos>().HasOne(a => a.TipoPermisos).WithMany(c => c.Permisos).HasForeignKey(a => a.TipoPermiso);
            base.OnModelCreating(modelBuilder);
        }

        public void CreateOrUpdateDatabase()
        {
            Database.Migrate();
        }
    }
}
