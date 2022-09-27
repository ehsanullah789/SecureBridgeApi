using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using secureBridge_Services.Helpers;
using secureBridge_Services.Repositories.UserRepository;
using secureBridge_Services.ViewModels;

namespace SecureBridgeApi.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        [HttpPost]
        [Route("api/[controller]/UserSignUp")]
        public async Task<ResponseModel> SignUp(UserVm uvm)
        {
            var res = await userRepository.UserSignUp(uvm);
            return res;
        }

        [HttpPost]
        [Route("api/[controller]/UserLogin")]
        public async Task<ResponseModel> Login(LoginVm lvm)
        {
            var res = await userRepository.UserLogin(lvm);
            return res;
        }
    }
}
