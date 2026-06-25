using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Repositories.Entities;

namespace Repositories.Context;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<MonthIdToMonthName> MonthIdToMonthNames { get; set; }

    public virtual DbSet<Observation> Observations { get; set; }

    public virtual DbSet<ObservationMonth> ObservationMonths { get; set; }

    public virtual DbSet<Species> Species { get; set; }

    public virtual DbSet<SpeciesFiles> SpeciesFiles { get; set; }

    public virtual DbSet<SpeciesFilesMetadatum> SpeciesFilesMetadata { get; set; }

    public virtual DbSet<VwTopListPerMonth> VwTopListPerMonths { get; set; }

    public virtual DbSet<VwTopListPerSpecy> VwTopListPerSpecies { get; set; }

    public virtual DbSet<VwTotalList> VwTotalLists { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Location__3214EC07E67955D5");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.LocationName).HasMaxLength(50);
        });

        modelBuilder.Entity<MonthIdToMonthName>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MonthIdT__3214EC0772F4F23C");

            entity.ToTable("MonthIdToMonthName", tb => tb.HasTrigger("trg_MonthIdToMonthName_BlockAllDML"));

            entity.HasIndex(e => e.MonthId, "UX_MonthIdToMonthName_MonthId").IsUnique();

            entity.Property(e => e.MonthName).HasMaxLength(20);
        });

        modelBuilder.Entity<Observation>(entity =>
        {
            entity.HasKey(e => e.ObservationId).HasName("PK__Observat__420EA5E7B5D44F9F");

            entity.ToTable("Observation");

            entity.HasIndex(e => new { e.ObservationMonthId, e.SpeciesId }, "UQ_Observation").IsUnique();

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(sysdatetime())", "DF_Observation_CreatedDate");

            entity.HasOne(d => d.Location).WithMany(p => p.Observations)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("FK_Observation_Location");

            entity.HasOne(d => d.ObservationMonth).WithMany(p => p.Observations)
                .HasForeignKey(d => d.ObservationMonthId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Observation_Month");

            entity.HasOne(d => d.Species).WithMany(p => p.Observations)
                .HasForeignKey(d => d.SpeciesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Observation_Species");
        });

        modelBuilder.Entity<ObservationMonth>(entity =>
        {
            entity.HasKey(e => e.ObservationMonthId).HasName("PK__Observat__3516F905F247AB49");

            entity.ToTable("ObservationMonth");

            entity.HasIndex(e => new { e.ObservationYear, e.ObservationMonth1 }, "UQ_ObservationMonth").IsUnique();

            entity.Property(e => e.ObservationMonth1).HasColumnName("ObservationMonth");
        });

        modelBuilder.Entity<Species>(entity =>
        {
            entity.HasKey(e => e.SpeciesId).HasName("PK__Species__A938045F9B499056");

            entity.HasIndex(e => e.SpeciesName, "UQ__Species__304D4C0D31EE26C3").IsUnique();

            entity.Property(e => e.SpeciesName).HasMaxLength(100);
        });

        modelBuilder.Entity<SpeciesFiles>(entity =>
        {
            entity.ToTable("SpeciesFiles");

            entity.HasKey(x => x.StreamId);

            entity.Property(x => x.StreamId)
                .HasColumnName("stream_id");

            entity.Property(x => x.FileData)
                .HasColumnName("file_stream");

            entity.Property(x => x.FileName)
                .HasColumnName("name");

            entity.Property(x => x.FileType)
                .HasColumnName("file_type");
        });

        modelBuilder.Entity<SpeciesFilesMetadatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SpeciesF__3214EC07C10660FF");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FileName).HasMaxLength(50);
            entity.Property(e => e.FileType).HasMaxLength(50);
        });

        modelBuilder.Entity<VwTopListPerMonth>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Vw_top_list_per_month");

            entity.Property(e => e.Antal).HasColumnName("antal");
            entity.Property(e => e.MonthName).HasMaxLength(20);
        });

        modelBuilder.Entity<VwTopListPerSpecy>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Vw_top_list_per_species");

            entity.Property(e => e.SpeciesName).HasMaxLength(100);
        });

        modelBuilder.Entity<VwTotalList>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Vw_total_list");

            entity.Property(e => e.MonthName).HasMaxLength(20);
            entity.Property(e => e.SpeciesName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
