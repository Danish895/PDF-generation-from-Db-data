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

        public async Task PeoplePdfInfoSavinginDb(PeoplePdfInformation peoplePdfInformation)
        {
            PeoplePdfInformation existing_peoplePdfInformation = await _context.PeoplePdfInformations.Where(t => t.ReferenceNumber == peoplePdfInformation.ReferenceNumber).FirstOrDefaultAsync();

            if (existing_peoplePdfInformation != null)
            {
                existing_peoplePdfInformation.ReferenceNumber = peoplePdfInformation.ReferenceNumber;
                existing_peoplePdfInformation.ObjectName = peoplePdfInformation.ObjectName;
                existing_peoplePdfInformation.FileName = peoplePdfInformation.FileName;
                existing_peoplePdfInformation.FileExtension = peoplePdfInformation.FileExtension;
                existing_peoplePdfInformation.ObjectCode = peoplePdfInformation.ObjectCode;
                existing_peoplePdfInformation.ReferenceType = peoplePdfInformation.ReferenceType;
                existing_peoplePdfInformation.Content = peoplePdfInformation.Content;
                existing_peoplePdfInformation.LanguageCode = peoplePdfInformation.LanguageCode;
                existing_peoplePdfInformation.CreatedUser = peoplePdfInformation.CreatedUser;
                existing_peoplePdfInformation.CreatedDateTime = peoplePdfInformation.CreatedDateTime;
            }
            else if (existing_peoplePdfInformation == null)
            {

                PeoplePdfInformation new_peoplePdfInformation = new PeoplePdfInformation()
                {
                    ReferenceNumber = peoplePdfInformation.ReferenceNumber,
                    ObjectName = peoplePdfInformation.ObjectName,
                    FileName = peoplePdfInformation.FileName,
                    FileExtension = peoplePdfInformation.FileExtension,
                    ObjectCode = peoplePdfInformation.ObjectCode,
                    ReferenceType = peoplePdfInformation.ReferenceType,
                    Content = peoplePdfInformation.Content,
                    LanguageCode = peoplePdfInformation.LanguageCode,
                    CreatedUser = peoplePdfInformation.CreatedUser,
                    CreatedDateTime = peoplePdfInformation.CreatedDateTime
                };

                await _context.PeoplePdfInformations.AddAsync(peoplePdfInformation);
            }
            
            await _context.SaveChangesAsync();

        }
    }
}
