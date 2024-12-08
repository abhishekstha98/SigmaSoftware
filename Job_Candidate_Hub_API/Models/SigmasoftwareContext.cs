using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CandidateHubAPI.Models;

public partial class SigmasoftwareContext : DbContext
{
    public SigmasoftwareContext()
    {
    }

    public SigmasoftwareContext(DbContextOptions<SigmasoftwareContext> options)
        : base(options)
    {
    }

    public virtual DbSet<OnboardedCandidate> OnboardedCandidates { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Initial Catalog=sigmasoftware;Integrated Security=true;MultipleActiveResultSets=true;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OnboardedCandidate>(entity =>
        {
            entity.HasKey(e => e.TempEmployeeId).HasName("PK__Onboarde__6015F20E833049D4");

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FullName).HasMaxLength(200);
            entity.Property(e => e.OnboardedDate).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
