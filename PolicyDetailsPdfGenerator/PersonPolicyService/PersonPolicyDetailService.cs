using PolicyDetailsPdfGenerator.DataAccessLayer.PolicyDetailRepository;
using PolicyDetailsPdfGenerator.Models;

namespace PolicyDetailsPdfGenerator.PersonPolicyService
{
    public class PersonPolicyDetailService : IPersonPolicyDetailService
    {
        private IPersonPolicyDetailRepository _UserRepository;

        public PersonPolicyDetailService(IServiceProvider serviceProvider)
        {
            _UserRepository = serviceProvider.GetRequiredService<IPersonPolicyDetailRepository>();
        }

        public virtual Task GeneratePdf(string personDetailsInHtmlFormat, Person personDetail)
        {
            throw new NotImplementedException();
        }

        public async Task<string> PersonDetailsInHtml(string productCode, int policyNumber)
        {
            Person personDetail = await _UserRepository.GetPersonDetailAsync(productCode, policyNumber);
            var Htmldetail = await _UserRepository.GetHtmlTemplate();

            string htmlTemplateFromDb = HtmlTemplateBodyMethod(Htmldetail);
            string PersondetailInHtmlFormat = ReplacingDataInHtml(personDetail, htmlTemplateFromDb);

            return PersondetailInHtmlFormat;
        }
        public async Task<Person> GetPersonDetail(string productCode, int policyNumber)
        {
            Person PersonDetail = await _UserRepository.GetPersonDetailAsync(productCode, policyNumber);
            return PersonDetail;    
        }

        private string HtmlTemplateBodyMethod(HtmlTemplate Htmldetail)
        {
            string htmlBody = Htmldetail.HtmlTemplateContent;
            return htmlBody;
        }

        private string ReplacingDataInHtml(Person PersonDetail, string HtmlTemplateFromDb)
        {
            HtmlTemplateFromDb = HtmlTemplateFromDb.Replace("{{Name}}", PersonDetail.Name);
            HtmlTemplateFromDb = HtmlTemplateFromDb.Replace("{{PolicyNumber}}", PersonDetail.PolicyNumber.ToString());
            HtmlTemplateFromDb = HtmlTemplateFromDb.Replace("{{PersonAge}}", PersonDetail.Age.ToString());
            HtmlTemplateFromDb = HtmlTemplateFromDb.Replace("{{PersonSalary}}", PersonDetail.Salary.ToString());
            HtmlTemplateFromDb = HtmlTemplateFromDb.Replace("{{PersonTenure}}", PersonDetail.Tenure.ToString());
            HtmlTemplateFromDb = HtmlTemplateFromDb.Replace("{{PersonAPolicyExpiryDate}}", PersonDetail.PolicyExpiryDate.ToString());
            HtmlTemplateFromDb = HtmlTemplateFromDb.Replace("{{PersonProductCode}}", PersonDetail.ProductCode);
            HtmlTemplateFromDb = HtmlTemplateFromDb.Replace("{{PersonOccupation}}", PersonDetail.Occupation);

            return HtmlTemplateFromDb;
        }   
    }
}
