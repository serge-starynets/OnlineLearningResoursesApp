using System;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;

namespace OnlineLearningResoursesApp.Models
{
    public class LearningContext : IdentityDbContext<User>
    {
        public LearningContext()
        {
            // Creating database
            Database.EnsureCreated();
        }

        public DbSet<LearningPlan> LearningPlans { get; set; }
        public DbSet<Course> Courses { get; set; }

        // Using sql server
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Connection string taken from config.json
            var connString = Startup.Configuration["Data:LearningContextConnection"];
            optionsBuilder.UseSqlServer(connString);

            base.OnConfiguring(optionsBuilder);
        }
    }
}
