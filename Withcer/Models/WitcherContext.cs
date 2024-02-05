using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Withcer.Models
{
    public partial class WitcherContext : DbContext
    {
        public WitcherContext()
        {
        }

        public WitcherContext(DbContextOptions<WitcherContext> options)
            : base(options)
        {
        }

       
        public virtual DbSet<PostRatings> Friendships { get; set; } = null!;
        public virtual DbSet<PostSports> Likes { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<Sports> Sports { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-BIIOI1I\\SQLEXPRESS;Database=Witcher;Integrated Security=True;");
            }
        }



        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Dislike>(entity =>
            {
                entity.Property(e => e.DislikeId).HasColumnName("DislikeID");

                entity.Property(e => e.PostId).HasColumnName("PostID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Dislikes)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK__Dislikes__PostID__46E78A0C");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Dislikes)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Dislikes__UserID__47DBAE45");
            });

            modelBuilder.Entity<Friendship>(entity =>
            {
                entity.HasKey(e => e.FriedshipId)
                    .HasName("PK__Friendsh__7CB1272335216B11");

                entity.ToTable("Friendship");

                entity.Property(e => e.FriedshipId).HasColumnName("FriedshipID");

                entity.Property(e => e.Status).HasMaxLength(25);

                entity.Property(e => e.User1Id).HasColumnName("User1ID");

                entity.Property(e => e.User2Id).HasColumnName("User2ID");

                entity.HasOne(d => d.User1)
                .WithMany(p => p.FriendshipUser1s)
                .HasForeignKey(d => d.User1Id)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Friendship_User1");

                entity.HasOne(d => d.User2)
                    .WithMany(p => p.FriendshipUser2s)
                    .HasForeignKey(d => d.User2Id)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Friendship_User2");
            });

            modelBuilder.Entity<Like>(entity =>
            {
                entity.Property(e => e.LikeId).HasColumnName("LikeID");

                entity.Property(e => e.PostId).HasColumnName("PostID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK__Likes__PostID__4316F928");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Likes__UserID__440B1D61");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.Property(e => e.PostId).HasColumnName("PostID");

                entity.Property(e => e.Content).HasMaxLength(255);

                entity.Property(e => e.Timestamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Post__UserID__398D8EEE");
            });

            modelBuilder.Entity<Sport>(entity =>
            {
                entity.ToTable("Sport");

                entity.Property(e => e.SportId).HasColumnName("SportID");

                entity.Property(e => e.PostId).HasColumnName("PostID");

                entity.Property(e => e.SportName).HasMaxLength(255);

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Sports)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK__Sport__PostID__3C69FB99");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.Gender)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);*/
    }
}
