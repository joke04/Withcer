using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Withcer.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Withcer.Models
{
    public class Data : DbContext
    {
        public Data(DbContextOptions<Data> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<PostSports> PostSports { get; set; }
        public DbSet<PostRatings> PostRatings { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Sports> Sports { get; set; }
        public DbSet<Comments> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostRatings>()
                .HasOne(pr => pr.User1)
                .WithMany(u => u.PostRatingsAsUser1)
                .HasForeignKey(pr => pr.User1ID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PostRatings>()
                .HasOne(pr => pr.User2)
                .WithMany(u => u.PostRatingsAsUser2)
                .HasForeignKey(pr => pr.Userb2ID)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }



        // Пример метода для выборки пользователей по имени
        public User GetUserByName(string firstName, string lastName)
        {
            return Users.FirstOrDefault(u => u.FirstName == firstName && u.LastName == lastName);
        }

        // Пример метода для вставки нового пользователя
        public void AddUser(User user)
        {
            Users.Add(user);
            SaveChanges();
        }

        // Другие методы можно реализовать аналогично для других 

    }

}