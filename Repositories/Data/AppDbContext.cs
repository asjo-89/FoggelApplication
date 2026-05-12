using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Repositories.Entities;

namespace Repositories.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MonthIdToMonthName> MonthIdToMonthNames { get; set; }

    public virtual DbSet<Observation> Observations { get; set; }

    public virtual DbSet<ObservationMonth> ObservationMonths { get; set; }

    public virtual DbSet<Species> Species { get; set; }

    public virtual DbSet<SpeciesFilesMetadatum> SpeciesFilesMetadata { get; set; }

    public virtual DbSet<Vw_total_list> ViewTotalObservations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=asasacerbacer\\sqlexpress;Database=Birds;User Id=BirdsWeb;Password=123456;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MonthIdToMonthName>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MonthIdT__3214EC0772F4F23C");

            entity.ToTable("MonthIdToMonthName", tb => tb.HasTrigger("trg_MonthIdToMonthName_BlockAllDML"));
        });

        modelBuilder.Entity<Observation>(entity =>
        {
            entity.HasKey(e => e.ObservationId).HasName("PK__Observat__420EA5E7B5D44F9F");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(sysdatetime())", "DF_Observation_CreatedDate");

            entity.HasOne(d => d.ObservationMonth).WithMany(p => p.Observations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Observation_Month");

            entity.HasOne(d => d.Species).WithMany(p => p.Observations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Observation_Species");
        });

        modelBuilder.Entity<ObservationMonth>(entity =>
        {
            entity.HasKey(e => e.ObservationMonthId).HasName("PK__Observat__3516F905F247AB49");
        });

        modelBuilder.Entity<Species>(entity =>
        {
            entity.HasKey(e => e.SpeciesId).HasName("PK__Species__A938045F9B499056");
        });

        modelBuilder.Entity<SpeciesFilesMetadatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SpeciesF__3214EC07C10660FF");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
        });

        OnModelCreatingPartial(modelBuilder);

        modelBuilder.Entity<Vw_total_list>(entity =>
        {
            entity.HasNoKey();
            entity.ToView("Vw_total_list");
        });
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
