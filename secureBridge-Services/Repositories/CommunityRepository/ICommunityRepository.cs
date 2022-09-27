using secureBridge_Services.Helpers;
using secureBridge_Services.ViewModels;

namespace secureBridge_Services.Repositories.CommunityRepository
{
    public interface ICommunityRepository
    {
        Task<ResponseModel> PostComment(PostVm pvm);
        Task<ResponseModel> UserReply(ReplyVm rvm);
        Task<ResponseModel> GetAllPostByComment();
        Task<ResponseModel> GetPostReplyByPostId(Guid postId);
    }
}