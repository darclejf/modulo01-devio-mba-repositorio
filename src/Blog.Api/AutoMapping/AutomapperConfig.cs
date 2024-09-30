using AutoMapper;
using Blog.Api.Models;
using Blog.Data.Entities;

namespace Blog.Api.AutoMapping
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Post, PostApiModel>();
        }
    }
}
