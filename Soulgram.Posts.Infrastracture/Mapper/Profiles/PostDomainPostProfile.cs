using AutoMapper;
using Soulgram.Posts.Application.Models.Post.Base;
using System;

namespace Soulgram.Posts.Infrastracture.Mapper.Profiles
{
    internal class PostDomainPostProfile : Profile
    {
        public PostDomainPostProfile()
        {
            CreateMap<Post, Domain.Post>()
                .ForMember(dp => dp.UserInfo, opt => opt.MapFrom(p => p.UserInfo))
                .ForMember(dp => dp.Text, opt => opt.MapFrom(p => p.Text))
                .ForMember(dp => dp.Medias, opt => opt.MapFrom(p => p.Medias))
                .ForMember(dp => dp.IsArticle, opt => opt.MapFrom(p => p.IsArticle))
                .ForMember(dp => dp.Date, opt => opt.MapFrom(c => DateTime.UtcNow))
                .ForMember(dp => dp.Comments, opt => opt.Ignore())
                .ForMember(dp => dp.Likes, opt => opt.Ignore())
                .ForMember(dp => dp.Views, opt => opt.Ignore());
        }
    }
}
