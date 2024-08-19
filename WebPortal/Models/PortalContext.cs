using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebPortal.Models;

public partial class PortalContext : DbContext
{
    public PortalContext()
    {
    }

    public PortalContext(DbContextOptions<PortalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Certificate> Certificates { get; set; }

    public virtual DbSet<Format> Formats { get; set; }

    public virtual DbSet<Procedure> Procedures { get; set; }

    public DbSet<Division> Divisions { get; set; }
    public DbSet<DocumentType> DocumentTypes { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Certificate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Certific__3214EC073913A3A6");

            entity.ToTable("Certificate");

            entity.Property(e => e.Division)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ImagePath)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("image_path");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);

        modelBuilder.Entity<Format>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Format__3214EC073913A3A6");

            entity.ToTable("Format");

            entity.Property(e => e.Division)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FormatPath)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("format_path");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);

        modelBuilder.Entity<Procedure>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Proc__3214EC073913A3A6");

            entity.ToTable("Procedure");

            entity.Property(e => e.Division)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ProcedurePath)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("procedure_path");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
