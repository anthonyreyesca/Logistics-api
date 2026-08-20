using Microsoft.EntityFrameworkCore;
using Projectopdracht.Models;

namespace Projectopdracht.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Container> Containers { get; set; }
        public DbSet<Depot> Depots { get; set; }
        public DbSet<Transport> Transports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Container>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.ContainerNumber)
                      .IsRequired()
                      .HasMaxLength(11);

                entity.HasOne(c => c.Depot)
                      .WithMany() 
                      .HasForeignKey(c => c.DepotId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Transport>(entity =>
            {
                entity.HasKey(t => t.Id);

                entity.HasOne(t => t.Container)
                      .WithMany()
                      .HasForeignKey(t => t.ContainerId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}