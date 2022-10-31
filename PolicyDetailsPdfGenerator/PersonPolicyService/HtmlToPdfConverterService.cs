﻿using PolicyDetailsPdfGenerator.DataAccessLayer.PolicyDetailRepository;
using PolicyDetailsPdfGenerator.Models;
using PuppeteerSharp;
using PuppeteerSharp.Media;

namespace PolicyDetailsPdfGenerator.PersonPolicyService
{
    public class HtmlToPdfConverterService : PersonPolicyDetailService
    {
        //private IPersonPolicyDetailRepository _UserRepository;
        public IEmailService _EmailService;

        public HtmlToPdfConverterService(IServiceProvider serviceProvider, IEmailService emailService) : base(serviceProvider)
        {
            _EmailService = emailService;
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
                CreatedUser = " Admin",
                CreatedDateTime = DateTime.Now
            };
            Console.WriteLine("Hello");
            await _UserRepository.PeoplePdfInfoSavinginDb(peoplePdfInformation);

            bool emailSent =  _EmailService.SendEmail(streamResult, peoplePdfInformation.FileExtension);
            if (emailSent)
            {
                Console.WriteLine("Email is sent");
            }
        }
    }
}
