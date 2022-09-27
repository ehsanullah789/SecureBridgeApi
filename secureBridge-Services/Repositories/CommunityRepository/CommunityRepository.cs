using Braintree;
using Microsoft.EntityFrameworkCore;
using secureBridge_Models.Models;
using secureBridge_Services.Data;
using secureBridge_Services.Enum;
using secureBridge_Services.Helpers;
using secureBridge_Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace secureBridge_Services.Repositories.CommunityRepository
{
	public class CommunityRepository : ICommunityRepository
	{
		private readonly ApplicationDbContext db;

		public CommunityRepository(ApplicationDbContext db)
		{
			this.db = db;
		}

		public async Task<ResponseModel> PostComment(PostVm pvm)
		{
			ResponseModel res = new ResponseModel();
			Post post = new Post();
			try
			{
				post.PostComment = pvm.PostComment;
				post.UserId = pvm.UserId;
				await db.Posts.AddAsync(post);
				await db.SaveChangesAsync();
				res.Status = true;
				res.Message = ResponseEnum.Success.GetDescription().ToString();
				res.Data = post;
			}
			catch (Exception e)
			{
				res.Status = false;
				res.Message = e.Message;
			}

			return res;
		}
		public async Task<ResponseModel> UserReply(ReplyVm rvm)
		{
			ResponseModel res = new ResponseModel();
			Reply rep = new Reply();
			try
			{
				rep.ReplyComment = rvm.ReplyComment;
				rep.PostId = rvm.PostId;
				rep.UserId = rvm.UserId;
				await db.Replies.AddAsync(rep);
				await db.SaveChangesAsync();
				res.Status = true;
				res.Message = ResponseEnum.Success.GetDescription().ToString();
				res.Data = rep;
			}
			catch (Exception e)
			{
				res.Status = false;
				res.Message = e.Message;
			}
			return res;
		}
		public async Task<ResponseModel> GetAllPostByComment()
		{
			ResponseModel res = new ResponseModel();
			try
			{
				var allPost = await db.Posts.Include(x => x.Replies).ToListAsync();
				res.Status = true;
				res.Message = ResponseEnum.Success.GetDescription().ToString();
				res.Data = allPost;
			}
			catch (Exception e)
			{
				res.Status = false;
				res.Message = e.Message;
			}
			return res;
		}
        public async Task<ResponseModel> GetPostReplyByPostId(Guid postId)
        {
            ResponseModel res = new ResponseModel();
            try
            {
                var allComment = await db.Replies.Where(x=>x.PostId == postId).ToListAsync();
                res.Status = true;
                res.Message = ResponseEnum.Success.GetDescription().ToString();
                res.Data = allComment;
            }
            catch (Exception e)
            {
                res.Status = false;
                res.Message = e.Message;
            }
            return res;
        }
    }
}
