using Microsoft.EntityFrameworkCore;
using SportLeader.Models;

namespace SportLeader.Data
{
    public class SpotrsLeaderDBContext : DbContext
    {
        public SpotrsLeaderDBContext(DbContextOptions options) : base(options) { }

        public DbSet<T_LeaderWorkInfo> T_LeaderWorkInfo { get; set; }
        public DbSet<T_Sport> T_Sport { get; set; }
        public DbSet<T_School> T_Schools { get; set; }
        public DbSet<T_History> T_Histories { get; set; }
        public DbSet<T_LeaderImage> T_LeaderImages { get; set; }
        public DbSet<T_Certificate> T_Certificates { get; set; }
        public DbSet<T_Leader> T_Leader { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<T_LeaderWorkInfo>(option =>
            {
                option.HasKey(a => a.LeaderSequence);
                option.Property(r => r.LeaderSequence).UseIdentityColumn();
            });

            modelBuilder.Entity<T_LeaderWorkInfo>()
                .HasOne(a => a.T_Leader)
                .WithOne()
                .HasForeignKey<T_LeaderWorkInfo>(a => a.LeaderNo);

            modelBuilder.Entity<T_LeaderWorkInfo>()
                .HasOne(a => a.T_Sport)
                .WithOne()
                .HasForeignKey<T_LeaderWorkInfo>(a => a.SportsNo);

            modelBuilder.Entity<T_LeaderWorkInfo>()
                .HasOne(a => a.T_School)
                .WithOne()
                .HasForeignKey<T_LeaderWorkInfo>(a => a.SchoolNo);

            modelBuilder.Entity<T_History>(option =>
            {
                option.HasKey(h => new { h.LeaderSequence, h.HistorySequence });
                option.Property(r => r.HistorySequence).UseIdentityColumn();
            });

            modelBuilder.Entity<T_History>()
                .HasOne(h => h.T_LeaderWorkInfo)
                .WithMany(s => s.T_History)
                .HasForeignKey(h => h.LeaderSequence);

            modelBuilder.Entity<T_History>()
                .HasOne(h => h.T_Sport)
                .WithMany()
                .HasForeignKey(h => h.SportsNo);

            modelBuilder.Entity<T_LeaderImage>()
                .HasKey(d => d.LeaderSequence);

            modelBuilder.Entity<T_LeaderImage>()
                .HasOne(d => d.T_LeaderWorkInfo)
                .WithOne()
                .HasForeignKey<T_LeaderImage>(d => d.LeaderSequence);

            modelBuilder.Entity<T_Certificate>(option =>
            {
                option.HasKey(c => new { c.LeaderSequence, c.CertificateSequence });
                option.Property(r => r.CertificateSequence).UseIdentityColumn();
            });
                

            modelBuilder.Entity<T_Certificate>()
                .HasOne(c => c.T_LeaderWorkInfo)
                .WithMany(s => s.T_Certificate)
                .HasForeignKey(c => c.LeaderSequence);
        }
    }
}
