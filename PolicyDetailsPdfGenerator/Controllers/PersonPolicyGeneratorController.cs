using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PolicyDetailsPdfGenerator.DataAccessLayer.PolicydetailDbContext;
using PolicyDetailsPdfGenerator.Models;
using PolicyDetailsPdfGenerator.PersonPolicyService;

namespace PolicyDetailsPdfGenerator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonPolicyGeneratorController : ControllerBase
    {
        private IPersonPolicyDetailService _PersonPolicyDetailService;
        private readonly PolicyDetailDbContext _context;
        public PersonPolicyGeneratorController(IServiceProvider serviceProvider)
        {
            _PersonPolicyDetailService = serviceProvider.GetRequiredService<HtmlToPdfConverterService>();
            _context = serviceProvider.GetRequiredService<PolicyDetailDbContext>();
        }

        [Route("PostHtmlContent")]
        [HttpPost]
        public async Task<IActionResult> PostHtmlContent(string Code, string HtmlTemplateContent, string ContentType)
        {
            HtmlTemplate template = new HtmlTemplate()
            {
                Code = Code,
                HtmlTemplateContent = HtmlTemplateContent,
                ContentType = ContentType
            };
            //await _context.HtmlTemplates.AddAsync(template);
            
            await _context.PeoplePdfInformations.AddAsync(new PeoplePdfInformation());
            
            await _context.SaveChangesAsync();

            //_context.PeopleInformation.AsNoTracking();
            
            return Ok("Successfully put html template in db");
        }

        [Route("GetPolicyDetail")]
        [HttpGet]
        public async Task<IActionResult> GetPolicyDetailOfPersonAsync(string productCode, int policyNumber)
        {
            if (policyNumber == null || productCode == null)
            {
                throw new Exception("Please Enter valid Credentials");
            }
           
            string personDetailsInHtmlFormat = await _PersonPolicyDetailService.PersonDetailsInHtml(productCode, policyNumber);

            Person getPersonDetail = await _PersonPolicyDetailService.GetPersonDetail(productCode, policyNumber);

            try
            {

                await _PersonPolicyDetailService.GeneratePdf(personDetailsInHtmlFormat, getPersonDetail);
                return Ok("success");
            }
            catch(Exception ex)
            {
                return Ok(ex.Message);
            }
        }
    }
}

