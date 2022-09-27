using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using secureBridge_Services.Helpers;
using secureBridge_Services.Repositories.OrganizationRepository;
using secureBridge_Services.ViewModels;

namespace SecureBridgeApi.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IOrganizationRepository organization;

        public AccountController(IOrganizationRepository organization)
        {
            this.organization = organization;
        }
        [HttpPost]
        [Route("api/[controller]/Register")]
        public async Task<ResponseModel> Register([FromBody] OrganizationVm ovm)
        {
            var res = await organization.Register(ovm);
            return res;
        }
        [HttpPost]
        [Route("api/[controller]/Login")]
        public async Task<ResponseModel> Login([FromBody] LoginVm lvm)
        {
            var res = await organization.Login(lvm);
            return res;
        }
    }
}
