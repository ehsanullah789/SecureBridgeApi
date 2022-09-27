using secureBridge_Services.Helpers;
using secureBridge_Services.ViewModels;

namespace secureBridge_Services.Repositories.OrganizationRepository
{
    public interface IOrganizationRepository
    {
        Task<ResponseModel> Login(LoginVm lvm);
        Task<ResponseModel> Register(OrganizationVm ovm);
    }
}