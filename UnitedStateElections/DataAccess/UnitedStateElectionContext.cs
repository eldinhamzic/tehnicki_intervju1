using Microsoft.EntityFrameworkCore;
using UnitedStateElections.Database;
using Exception = UnitedStateElections.Database.Exception;

namespace UnitedStateElections.DataAccess
{
    public class UnitedStateElectionContext : DbContext
    {
        public UnitedStateElectionContext(DbContextOptions<UnitedStateElectionContext> options) : base(options)
        {

        }

        public virtual DbSet<Candidate> Candidates { get; set; }
        public virtual DbSet<Constituency> Constituencies { get; set; }
        public virtual DbSet<ConstituencyOfCandudate> ConstituencyOfCandudates { get; set; }
        public virtual DbSet<Exception> Exceptions { get; set; }

        protected void OnConfig(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=UnitedStateElections;Trusted_Connection=True;");

            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Exception>(entity =>
            {
                entity.ToTable("Exception");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Message)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Constituency>(entity =>
            {
                entity.ToTable("Constituency");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                base.OnModelCreating(modelBuilder);
            });

            modelBuilder.Entity<ConstituencyOfCandudate>(entity =>
            {
                entity.ToTable("ConstituencyOfCandidate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ConstituencyId).HasColumnName("ConstituencyId");

                entity.Property(e => e.CandidateId).HasColumnName("CandidateId");

                entity.HasOne(d => d.Constituency)
                    .WithMany(p => p.ConstituencyOfCandudates)
                    .HasForeignKey(d => d.ConstituencyId)
                    .HasConstraintName("FK__Costituency__Cons__1122");

                entity.HasOne(d => d.Candidate)
                    .WithMany(p => p.ConstituencyOfCandudates)
                    .HasForeignKey(d => d.CandidateId)
                    .HasConstraintName("FK__Constituency__Can__1112");
            });

            modelBuilder.Entity<Candidate>(entity =>
            {
                entity.ToTable("Candidate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FullName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Fullname");
                base.OnModelCreating(modelBuilder);

                entity.Property(e => e.CandidateCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        private void OnModelCreatingPartial(ModelBuilder modelBuilder) { }





    }
}
