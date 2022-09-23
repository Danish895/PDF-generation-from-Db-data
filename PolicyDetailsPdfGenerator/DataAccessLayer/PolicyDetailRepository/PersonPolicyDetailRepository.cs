using Microsoft.EntityFrameworkCore;
using PolicyDetailsPdfGenerator.DataAccessLayer.PolicydetailDbContext;
using PolicyDetailsPdfGenerator.Models;

namespace PolicyDetailsPdfGenerator.DataAccessLayer.PolicyDetailRepository
{
    public class PersonPolicyDetailRepository : IPersonPolicyDetailRepository
    {
        private readonly PolicyDetailDbContext _context;

        public PersonPolicyDetailRepository(PolicyDetailDbContext context)
        {
            _context = context;
        }
        public async Task<Person> GetPersonDetailAsync(string productCode, int policyNumber)

        {
            Person Person = await _context.Persons.Where(t => t.PolicyNumber == policyNumber && t.ProductCode == productCode).FirstAsync();
            return Person;
        }

        public async Task<HtmlTemplate> GetHtmlTemplate()
        {
            HtmlTemplate Bodydata = await _context.HtmlTemplates.Where(t => t.Code == "GivenHtmlTemplate").FirstOrDefaultAsync();
            return Bodydata;
        }
    }
}
