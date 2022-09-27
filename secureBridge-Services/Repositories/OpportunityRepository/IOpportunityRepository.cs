using secureBridge_Services.Helpers;
using secureBridge_Services.ViewModels;

namespace secureBridge_Services.Repositories.OpportunityRepository
{
    public interface IOpportunityRepository
    {
        Task<ResponseModel> AddOpportunityType(string email);
        Task<ResponseModel> AddOpportunity(OpportunityVm ovm, Guid OrganozationId);
        Task<ResponseModel> GetAllOpportunity();
        Task<ResponseModel> GetAllOpportunityByTypes(OpportunityTypeVm ovm);
        Task<ResponseModel> UserAppliedOpportunity(AppliedOpportunityVm avm);
        Task<ResponseModel> GetOpportunityByOrganizationId(Guid OrganizationId);
        Task<ResponseModel> GetOpportunityByUserId(Guid UserId);
        Task<ResponseModel> GetOpportunityByOpportunityId(Guid OpportunityId);
        Task<ResponseModel> OpportunityBySearch(string searchString);
    }
}