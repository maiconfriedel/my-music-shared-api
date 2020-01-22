using Microsoft.EntityFrameworkCore;
using MyMusicSharedBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMusicSharedBackend.Database
{
    /// <summary>
    /// Db Context for Database Access
    /// </summary>
    public class MyMusicSharedDbContext : DbContext
    {
        /// <summary>
        /// Users DbSet
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Method called on configuring the Db Context
        /// </summary>
        /// <param name="optionsBuilder">Options</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("User ID=MyMusicShared;Password=1234;Host=localhost;Port=5432;Database=MyMusicShared;");
        }

        /// <summary>
        /// Method called on creating the models
        /// </summary>
        /// <param name="modelBuilder">Model builder</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.User>().HasIndex(p => p.Email).IsUnique(true);
        }
    }
}