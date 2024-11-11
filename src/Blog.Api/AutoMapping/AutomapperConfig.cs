using AutoMapper;
using Blog.Api.Models;
using Blog.Data.Entities;

namespace Blog.Api.AutoMapping
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Comment, CommentApiModel>()
                .ForMember(dest => dest.UserName, m => m.MapFrom(a => a.User.UserName));

            CreateMap<Post, PostApiModel>()
                .ForMember(dest => dest.Author, m => m.MapFrom(a => a.Author.Name));
        }
    }
}
