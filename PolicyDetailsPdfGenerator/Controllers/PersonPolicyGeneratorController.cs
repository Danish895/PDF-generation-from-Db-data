using Microsoft.AspNetCore.Mvc;
using PolicyDetailsPdfGenerator.Models;
using PolicyDetailsPdfGenerator.PersonPolicyService;

namespace PolicyDetailsPdfGenerator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonPolicyGeneratorController : ControllerBase
    {
        private IPersonPolicyDetailService _PersonPolicyDetailService;
        public PersonPolicyGeneratorController(IServiceProvider serviceProvider)
        {
            _PersonPolicyDetailService = serviceProvider.GetRequiredService<HtmlToPdfConverterService>();
        }

        [Route("GetPolicyDetail")]
        [HttpGet]
        public async Task GetPolicyDetailOfPersonAsync(string productCode, int policyNumber)
        {
            if (policyNumber == null || productCode == null)
            {
                throw new Exception("Please Enter valid Credentials");
            }
           
            string personDetailsInHtmlFormat = await _PersonPolicyDetailService.PersonDetailsInHtml(productCode, policyNumber);

            Person getPersonDetail = await _PersonPolicyDetailService.GetPersonDetail(productCode, policyNumber);

            await _PersonPolicyDetailService.GeneratePdf(personDetailsInHtmlFormat, getPersonDetail);
        }
    }
}

