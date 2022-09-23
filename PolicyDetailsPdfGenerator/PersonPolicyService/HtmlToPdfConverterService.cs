using PolicyDetailsPdfGenerator.Models;
using PuppeteerSharp;
using PuppeteerSharp.Media;

namespace PolicyDetailsPdfGenerator.PersonPolicyService
{
    public class HtmlToPdfConverterService : PersonPolicyDetailService
    {
        public HtmlToPdfConverterService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
        public async override Task GeneratePdf(string personDetailsInHtmlFormat, Person personDetail)
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
        }
    }
}
