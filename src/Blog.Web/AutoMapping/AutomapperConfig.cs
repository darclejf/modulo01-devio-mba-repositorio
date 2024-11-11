using AutoMapper;
using Blog.Application.Models;
using Blog.Data.Entities;
using Blog.Web.Models;

namespace Blog.Web.AutoMapping
{
	public class AutomapperConfig : Profile
	{
		public AutomapperConfig()
		{
			CreateMap<Post, PostViewModel>()
				.ForMember(dest => dest.Author, m => m.MapFrom(a => a.Author.Name));

            CreateMap<Comment, CommentPostViewModel>()
                .ForMember(dest => dest.UserName, m => m.MapFrom(a => a.User.UserName));

            CreateMap<Post, ListItemPostViewModel>()
				.ForMember(dest => dest.Author, m => m.MapFrom(a => a.Author.Name));

			CreateMap<Post, EditPostModel>();

            CreateMap<Comment, CommentPostModel>();
        }
	}
}
