using Microsoft.EntityFrameworkCore;
using WeatherForUser_Core.Entities;

namespace WeatherForUser_Core
{
    public class WFU_Context : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        public DbSet<UserSettingsEntity> UserSettings { get; set; }

        public WFU_Context(DbContextOptions<WFU_Context> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");
            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.Property(e => e.Device)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.HashCode)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                    entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<UserSettingsEntity>(entity =>
            {
                entity.Property(e => e.LastUsedCity)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasKey(e => e.Id);
            });
        }
    }
}
