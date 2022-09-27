using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using secureBridge_Services.Helpers;
using secureBridge_Services.Repositories.CommunityRepository;
using secureBridge_Services.ViewModels;

namespace SecureBridgeApi.Controllers
{
    
    [ApiController]
    public class CommunityController : ControllerBase
    {
        private readonly ICommunityRepository communityRepository;

        public CommunityController(ICommunityRepository communityRepository)
        {
            this.communityRepository = communityRepository;
        }
        [HttpPost]
        [Route("api/[controller]/PostComment")]
        public async Task<ResponseModel> PostComment(PostVm pvm)
        {
            var res = await communityRepository.PostComment(pvm);
            return res;
        }

        [HttpPost]
        [Route("api/[controller]/PostReply")]
        public async Task<ResponseModel> CommunityPostReply(ReplyVm rvm)
        {
            var res = await communityRepository.UserReply(rvm);
            return res;
        }
        [HttpGet]
        [Route("api/[controller]/GetAllPostByComment")]
        public async Task<ResponseModel> GetAllPostByComment()
        {
            var res = await communityRepository.GetAllPostByComment();
            return res;
        }
        [HttpGet]
        [Route("api/[controller]/GetPostReplyByPostId")]
        public async Task<ResponseModel> GetPostReplyByPostId(Guid postId)
        {
            var res = await communityRepository.GetPostReplyByPostId(postId);
            return res;
        }
    }
}
