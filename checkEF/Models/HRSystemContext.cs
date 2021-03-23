using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace checkEF.Models
{
    public partial class HRSystemContext : DbContext
    {
        public HRSystemContext()
        {
        }

        public HRSystemContext(DbContextOptions<HRSystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MigrationHistory> MigrationHistory { get; set; }
        public virtual DbSet<TblGrade> TblGrade { get; set; }
        public virtual DbSet<TblStudentDetails> TblStudentDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=192.168.1.28;Database=HRSystem;MultipleActiveResultSets=True;user=sa;password=sa");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey })
                    .HasName("PK_dbo.__MigrationHistory");

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.Model).IsRequired();

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<TblGrade>(entity =>
            {
                entity.ToTable("tbl_Grade");
            });

            modelBuilder.Entity<TblStudentDetails>(entity =>
            {
                entity.ToTable("tbl_StudentDetails");

                entity.HasIndex(e => e.GradeId)
                    .HasName("IX_Grade_Id");

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.GradeId).HasColumnName("Grade_Id");

                entity.Property(e => e.Height).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Grade)
                    .WithMany(p => p.TblStudentDetails)
                    .HasForeignKey(d => d.GradeId)
                    .HasConstraintName("FK_dbo.tbl_StudentDetails_dbo.tbl_Grade_Grade_Id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
