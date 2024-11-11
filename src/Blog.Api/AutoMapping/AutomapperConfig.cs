using AutoMapper;
using Blog.Api.Models;
using Blog.Data.Entities;
using System.Xml.Linq;

namespace Blog.Api.AutoMapping
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Post, PostApiModel>();
            CreateMap<Comment, CommentApiModel >();
        }
    }

	//public PostViewModel(Post entity)
	//{
	//	Id = entity.Id;
	//	Title = entity.Title;
	//	SubTitle = entity.SubTitle;
	//	Description = entity.Description;
	//	CreateAt = entity.CreatedAt;
	//	Author = string.IsNullOrEmpty(entity.Author?.Name) ? "" : entity.Author?.Name;
	//	Comments = entity.Comments == null ? [] : entity.Comments.Select(x => new CommentPostModel(x)).ToList();
	//}


}
