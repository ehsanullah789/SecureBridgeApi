using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using secureBridge_Services.Helpers;
using secureBridge_Services.Repositories.OpportunityRepository;
using secureBridge_Services.ViewModels;

namespace SecureBridgeApi.Controllers
{
    [ApiController]
    public class OpportunityController : ControllerBase
    {
        private readonly IOpportunityRepository opportunityRepository;

        public OpportunityController(IOpportunityRepository opportunityRepository)
        {
            this.opportunityRepository = opportunityRepository;
        }

        [HttpPost]
        [Route("api/[controller]/SelectOpportunityType")]
        public async Task<ResponseModel> SelectOpportunityType(string email)
        {
            var res = await opportunityRepository.AddOpportunityType(email);
            return res;
        }
        [HttpPost]
        [Route("api/[controller]/AddOpportunity")]
        public async Task<ResponseModel> AddOpportunity([FromBody]OpportunityVm ovm, Guid OrganozationId)
        {
            var res = await opportunityRepository.AddOpportunity(ovm,OrganozationId);
            return res;
        }
        [HttpGet]
        [Route("api/[controller]/GetAllOpportunities")]
        public async Task<ResponseModel> GetAllOpportunity()
        {
            var res = await opportunityRepository.GetAllOpportunity();
            return res;
        }
        [HttpGet]
        [Route("api/[controller]/GetAllOpportunitiesByType")]
        public async Task<ResponseModel> GetAllOpportunityByType(OpportunityTypeVm ovm)
        {
            var res = await opportunityRepository.GetAllOpportunityByTypes(ovm);
            return res;
        }
        [HttpPost]
        [Route("api/[controller]/AppliedOpportunity")]
        public async Task<ResponseModel> UserAppliedOpportunity(AppliedOpportunityVm avm)
        {
            var res = await opportunityRepository.UserAppliedOpportunity(avm);
            return res;
        }
        [HttpGet]
        [Route("api/[controller]/GetOpportunityByOrganizationId")]
        public async Task<ResponseModel> GetOpportunityByOrganizationId(Guid OrganizationId)
        {
            var res = await opportunityRepository.GetOpportunityByOrganizationId(OrganizationId);
            return res;
        }
        [HttpGet]
        [Route("api/[controller]/GetOpportunityByUserId")]
        public async Task<ResponseModel> GetOpportunityByUserId(Guid userId)
        {
            var res = await opportunityRepository.GetOpportunityByUserId(userId);
            return res;
        }
        [HttpGet]
        [Route("api/[controller]/GetOpportunityByOpportunityId")]
        public async Task<ResponseModel> GetOpportunityByOpportunityId(Guid opportunityId)
        {
            var res = await opportunityRepository.GetOpportunityByOpportunityId(opportunityId);
            return res;
        }
        [HttpGet]
        [Route("api/[controller]/OpportunityBySearch")]
        public async Task<ResponseModel> SearchOpportunity(string searchString)
        {
            var res = await opportunityRepository.OpportunityBySearch(searchString);
            return res;
        }

    }
}
