using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Security.Models.Entities;

namespace Security.Models.DAL
{
    public class SecurityDbContext: DbContext
    {
        private readonly IConfiguration _config;

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public string ConnectionString { get; set; } = "";

        public SecurityDbContext(IConfiguration config, DbContextOptions<SecurityDbContext> options) : base(options)
        {
            _config = config ?? throw new System.ArgumentNullException(nameof(config));
        }

        public SecurityDbContext(string connectionString)
        {
            ConnectionString = connectionString;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(ConnectionString != "")
            {
                optionsBuilder.UseSqlServer(ConnectionString);
                optionsBuilder.EnableSensitiveDataLogging();
            }
            else
            {
                base.OnConfiguring(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>()
                .HasKey(e => new { e.uname, e.role_id });
        }
        
    }
}
