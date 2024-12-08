using CandidateHubAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CandidateHubAPI.Data
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        public virtual DbSet<Candidate> Candidates { get; set; }
        public virtual DbSet<OnboardedCandidate> OnboardedCandidates { get; set; }
        public virtual DbSet<InterviewScore> InterviewScores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
            => optionsBuilder.UseSqlServer("Server=localhost;Initial Catalog=sigmasoftware;Integrated Security=true;MultipleActiveResultSets=true;TrustServerCertificate=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Candidate>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Candidat__3214EC074142091E");

                entity.HasIndex(e => e.Email, "UQ__Candidat__A9D1053448B6F1EE").IsUnique();

                entity.Property(e => e.CallTimeInterval).HasMaxLength(50);
                entity.Property(e => e.Email).HasMaxLength(255);
                entity.Property(e => e.FirstName).HasMaxLength(100);
                entity.Property(e => e.GitHubUrl).HasMaxLength(500);
                entity.Property(e => e.InterviewTime).HasColumnType("datetime");
                entity.Property(e => e.LastName).HasMaxLength(100);
                entity.Property(e => e.LinkedInUrl).HasMaxLength(500);
                entity.Property(e => e.PhoneNumber).HasMaxLength(15);
                entity.Property(e => e.SentEmail).HasColumnName("SentEmail ");
            });

            modelBuilder.Entity<OnboardedCandidate>(entity =>
            {
                entity.HasKey(e => e.TempEmployeeId).HasName("PK__Onboarde__6015F20E833049D4");

                entity.Property(e => e.Email).HasMaxLength(255);
                entity.Property(e => e.FullName).HasMaxLength(200);
                entity.Property(e => e.OnboardedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<InterviewScore>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Intervie__3214EC070BA1A6DB");

                entity.Property(e => e.Comments).HasMaxLength(500);
                entity.Property(e => e.ScoredOn)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.TotalScore).HasComputedColumnSql("(([TechnicalScore]+[CommunicationScore])+[ProblemSolvingScore])", true);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
