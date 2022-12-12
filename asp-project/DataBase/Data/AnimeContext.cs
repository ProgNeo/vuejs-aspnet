using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Anime.Models;

namespace Anime.Data
{
    public partial class AnimeContext : DbContext
    {
        public AnimeContext()
        {
        }

        public AnimeContext(DbContextOptions<AnimeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AnimeObject> AnimeObjects { get; set; } = null!;
        public virtual DbSet<Genre> Genres { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<AnimeObject>(entity =>
            {
                entity.ToTable("anime_objects");

                entity.HasIndex(e => e.Genre, "objects_genres_null_fk");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.Genre).HasColumnName("genre");

                entity.Property(e => e.Image)
                    .HasMaxLength(512)
                    .HasColumnName("image");

                entity.Property(e => e.Info)
                    .HasColumnType("text")
                    .HasColumnName("info");

                entity.Property(e => e.Title)
                    .HasMaxLength(256)
                    .HasColumnName("title");

                entity.HasOne(d => d.GenreNavigation)
                    .WithMany(p => p.AnimeObjects)
                    .HasForeignKey(d => d.Genre)
                    .HasConstraintName("objects_genres_null_fk");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.ToTable("genres");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(128)
                    .HasColumnName("name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
