using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WordsDB
{
    public partial class TestDatabaseContext : DbContext
    {
        public static string DatabasePath;
        public TestDatabaseContext()
        {
        }

        public TestDatabaseContext(DbContextOptions<TestDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<UsingWord> UsingWords { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite($"DataSource={DatabasePath}");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsingWord>(entity =>
            {
                entity.HasKey(e => e.Tuid);

                entity.ToTable("USING_WORDS");

                entity.HasIndex(e => e.Tuid, "IX_USING_WORDS_TUID")
                    .IsUnique();

                entity.HasIndex(e => e.Word, "IX_USING_WORDS_WORD")
                    .IsUnique();

                entity.HasIndex(e => e.Word, "WORD_INDEX")
                    .IsUnique();

                entity.Property(e => e.Tuid).HasColumnName("TUID");

                entity.Property(e => e.RepetionRate).HasColumnName("REPETION_RATE");

                entity.Property(e => e.Word)
                    .IsRequired()
                    .HasColumnType("STRING (15)")
                    .HasColumnName("WORD");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
