using Microsoft.EntityFrameworkCore;
using PolicyDetailsPdfGenerator.Models;

namespace PolicyDetailsPdfGenerator.DataAccessLayer.PolicydetailDbContext
{
    public class PolicyDetailDbContext : DbContext
    {
        public PolicyDetailDbContext(DbContextOptions<PolicyDetailDbContext> options)
        : base(options) { }

        public DbSet<Person> Persons { get; set; }
        public DbSet<HtmlTemplate> HtmlTemplates { get; set; }
    }
}
