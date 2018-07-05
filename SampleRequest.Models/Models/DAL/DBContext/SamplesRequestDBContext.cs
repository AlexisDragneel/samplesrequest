using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SamplesRequest.Models.Models.Entities;

namespace SamplesRequest.Models.Models.DAL.DBContext
{
    public class SamplesRequestDBContext : DbContext
    {
        private readonly IConfiguration _config;

        public SamplesRequestDBContext(IConfiguration config, DbContextOptions<SamplesRequestDBContext> options) : base(options)
        {
            _config = config ?? throw new System.ArgumentNullException(nameof(config));
        }

        public DbSet<Entities.SampleRequest> SampleRequests { get; set; }
        public DbSet<SampleRequestDetail> SampleRequestDetails { get; set; }
        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<SamplePurpose> SamplesPurposes { get; set; }
        public DbSet<SamplePriority> SamplesPriorities { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<WorkflowStep> WorkflowSteps { get; set; }
        public DbSet<WorkflowUser> WorkflowUsers { get; set; }
        public DbSet<RequestWorkflowStep> RequestWorkflowStep { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("DefaultConnection"), options =>
            {
                options.MigrationsHistoryTable("__SamplesRequestMigrationsHistory");
            });

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SamplePurpose>().HasMany(e => e.sample_requests).WithOne(e => e.purpose)
                .OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            modelBuilder.Entity<SamplePriority>().HasMany(e => e.sample_requests).WithOne(e => e.priority)
                .OnDelete(deleteBehavior: DeleteBehavior.Restrict);
        }
    }
}
