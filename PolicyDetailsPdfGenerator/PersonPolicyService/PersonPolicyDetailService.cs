using PolicyDetailsPdfGenerator.DataAccessLayer.PolicyDetailRepository;
using PolicyDetailsPdfGenerator.Models;
using PuppeteerSharp;
using PuppeteerSharp.Media;

namespace PolicyDetailsPdfGenerator.PersonPolicyService
{
    public class PersonPolicyDetailService : IPersonPolicyDetailService
    {
        protected IPersonPolicyDetailRepository _UserRepository;

        public PersonPolicyDetailService(IServiceProvider serviceProvider)
        {
            _UserRepository = serviceProvider.GetRequiredService<IPersonPolicyDetailRepository>();
        }

        public async virtual Task GeneratePdf(string personDetailsInHtmlFormat, Person personDetail)
        {
            var options = new LaunchOptions
            {
                Headless = true,
                ExecutablePath = "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe",
            };
            var browser = await Puppeteer.LaunchAsync(options);

            await using var page = await browser.NewPageAsync();
            await page.SetContentAsync(personDetailsInHtmlFormat);


            await page.PdfAsync($"{personDetail.PolicyNumber}-{personDetail.Name}.pdf", new PdfOptions
            {
                Format = PaperFormat.A4,
                DisplayHeaderFooter = true,
                MarginOptions = new MarginOptions
                {
                    Top = "37.5px",
                    Right = "20px",
                    Bottom = "40px",
                    Left = "30px"
                }
            });
            PdfOptions pdfOptions = new PdfOptions()
            {
                Format = PaperFormat.A4,
                DisplayHeaderFooter = true,
                PrintBackground = true,
                MarginOptions = new PuppeteerSharp.Media.MarginOptions
                {
                    Top = "37.5px",
                    Right = "20px",
                    Bottom = "40px",
                    Left = "30px"
                },
            };
            byte[] streamResult = await page.PdfDataAsync(pdfOptions)
                    .ConfigureAwait(false);
            Console.WriteLine(page);


            var peoplePdfInformation = new PeoplePdfInformation()
            {
                ObjectCode = $"{personDetail.PolicyNumber} - {personDetail.ProductCode}",
                ObjectName = DateTime.Now,
                ReferenceType = "Policy",
                ReferenceNumber = personDetail.PolicyNumber,
                Content = streamResult,
                FileName = $"{personDetail.PolicyNumber}" + DateTime.Now.ToString(),
                FileExtension = ".pdf",
                LanguageCode = "km-KH",
                CreatedUser = " Danish",
                CreatedDateTime = DateTime.Now
            };
            await _UserRepository.PeoplePdfInfoSavinginDb(peoplePdfInformation);
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
