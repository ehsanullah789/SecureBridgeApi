using secureBridge_Services.Helpers;
using secureBridge_Services.ViewModels;

namespace secureBridge_Services.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task<ResponseModel> UserLogin(LoginVm lvm);
        Task<ResponseModel> UserSignUp(UserVm uvm);
    }
}