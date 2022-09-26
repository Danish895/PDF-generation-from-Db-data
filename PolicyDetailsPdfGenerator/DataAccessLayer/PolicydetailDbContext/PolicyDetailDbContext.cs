using Microsoft.EntityFrameworkCore;
using PolicyDetailsPdfGenerator.Models;

namespace PolicyDetailsPdfGenerator.DataAccessLayer.PolicydetailDbContext
{
    public class PolicyDetailDbContext : DbContext
    {
        public PolicyDetailDbContext(DbContextOptions<PolicyDetailDbContext> options)
        : base(options) { }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<PeoplePdfInformation>().HasNoKey();
        //    //modelBuilder.Entity<PeopleInformation>().HasNoKey();
        //}
        public DbSet<Person> Persons { get; set; }
        public DbSet<HtmlTemplate> HtmlTemplates { get; set; }
        public DbSet<PeoplePdfInformation> PeoplePdfInformations { get; set; }
        //public DbSet<PeopleInformation> PeopleInformation { get; set; }
    }
}
