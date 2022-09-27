using Microsoft.EntityFrameworkCore;
using secureBridge_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace secureBridge_Services.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }
        //Db Models
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<OpportunityTypes> OpportunityTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Opportunity> Opportunities { get; set; }
        public DbSet<ApplyOpportunity> ApplyOpportunities { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Reply> Replies { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=.;Database=SecureBridgeDb;Trusted_Connection=true;");
        }
    }
}
