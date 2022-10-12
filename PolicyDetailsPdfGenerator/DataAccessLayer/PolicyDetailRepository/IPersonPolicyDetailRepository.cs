using PolicyDetailsPdfGenerator.Models;

namespace PolicyDetailsPdfGenerator.DataAccessLayer.PolicyDetailRepository
{
    public interface IPersonPolicyDetailRepository
    {
        Task<HtmlTemplate> GetHtmlTemplate();
        Task<Person> GetPersonDetailAsync(string ProductCode, int PolicyNumber);
        Task PeoplePdfInfoSavinginDb(PeoplePdfInformation peoplePdfInformation);
    }
}
