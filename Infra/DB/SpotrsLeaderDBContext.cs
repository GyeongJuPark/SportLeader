using Microsoft.EntityFrameworkCore;
using SportLeader.Models;

namespace SportLeader.Infra.DB
{
    public class SpotrsLeaderDBContext : DbContext
    {
        public SpotrsLeaderDBContext(DbContextOptions options) : base(options) { }

        public DbSet<T_LeaderWorkInfo> T_LeaderWorkInfo { get; set; }
        public DbSet<T_Sport> T_Sport { get; set; }
        public DbSet<T_School> T_School { get; set; }
        public DbSet<T_History> T_History { get; set; }
        public DbSet<T_LeaderImage> T_LeaderImage { get; set; }
        public DbSet<T_Certificate> T_Certificate { get; set; }
        public DbSet<T_Leader> T_Leader { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<T_LeaderWorkInfo>(option =>
            {
                option.HasKey(a => a.LeaderNo);

                option.HasOne(a => a.T_Leader)
                      .WithOne()
                      .HasForeignKey<T_LeaderWorkInfo>(a => a.LeaderNo);

                option.HasOne(a => a.T_Sport)
                      .WithMany()
                      .HasForeignKey(a => a.SportsNo)
                      .OnDelete(DeleteBehavior.ClientCascade);

                option.HasOne(a => a.T_School)
                      .WithMany()
                      .HasForeignKey(a => a.SchoolNo)
                      .OnDelete(DeleteBehavior.ClientCascade);

                option.HasOne(a => a.T_LeaderImage)
                      .WithOne()
                      .HasForeignKey<T_LeaderImage>(a => a.LeaderNo);
            });


            modelBuilder.Entity<T_History>(option =>
            {
                option.HasKey(h => new { h.LeaderNo, h.HistorySequence });
                option.Property(r => r.HistorySequence).UseIdentityColumn();

                option.HasOne(h => h.T_LeaderWorkInfo)
                      .WithMany(s => s.T_History)
                      .HasForeignKey(h => h.LeaderNo);

                option.HasOne(h => h.T_Sport)
                       .WithMany()
                       .HasForeignKey(h => h.SportsNo);
            });

            modelBuilder.Entity<T_LeaderImage>(entity =>
            {
                entity.HasKey(d => d.LeaderNo);
            });

            modelBuilder.Entity<T_Certificate>(option =>
            {
                option.HasKey(c => new { c.LeaderNo, c.CertificateSequence });
                option.Property(r => r.CertificateSequence).UseIdentityColumn();
                option.HasOne(h => h.T_LeaderWorkInfo)
                      .WithMany(s => s.T_Certificate)
                      .HasForeignKey(h => h.LeaderNo);
            });



        }
    }
}
