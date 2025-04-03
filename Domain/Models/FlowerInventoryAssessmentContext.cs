using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Domain.Models;

public partial class FlowerInventoryAssessmentContext : DbContext
{
    public FlowerInventoryAssessmentContext()
    {
    }

    public FlowerInventoryAssessmentContext(DbContextOptions<FlowerInventoryAssessmentContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Flower> Flowers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Description).HasMaxLength(750);
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.Timestamp).HasPrecision(0);
        });

        modelBuilder.Entity<Flower>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.Description).HasMaxLength(750);
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.Price).HasColumnType("decimal(9, 2)");
            entity.Property(e => e.Timestamp).HasPrecision(0);

            entity.HasOne(d => d.Category).WithMany(p => p.Flowers)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Flowers_Categories");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
