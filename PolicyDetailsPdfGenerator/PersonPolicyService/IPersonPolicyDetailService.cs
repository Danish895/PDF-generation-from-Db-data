using PolicyDetailsPdfGenerator.Models;

namespace PolicyDetailsPdfGenerator.PersonPolicyService
{
    public interface IPersonPolicyDetailService
    {
        Task<string> PersonDetailsInHtml(string productCode, int policyNumber);
        Task GeneratePdf(string personDetailsInHtmlFormat, Person personDetail);
        Task<Person> GetPersonDetail(string productCode, int policyNumber);
    
    }
}
