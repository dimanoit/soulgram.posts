using System;
using AutoMapper;
using Soulgram.Posts.Application.Models.Post;
using Soulgram.Posts.Application.Models.Post.Base;

namespace Soulgram.Posts.Infrastructure.Mapper.Profiles;

internal class PostsProfile : Profile
{
    public PostsProfile()
    {
        CreateMap<Post, Domain.Post>()
            .ForMember(dp => dp.UserId, opt => opt.MapFrom(p => p.UserId))
            .ForMember(dp => dp.Text, opt => opt.MapFrom(p => p.Text))
            .ForMember(dp => dp.Hashtags, opt => opt.MapFrom(p => p.Hashtags))
            .ForMember(dp => dp.Date, opt => opt.MapFrom(c => DateTime.UtcNow))
            .ForMember(dp => dp.Type, opt => opt.MapFrom(c => c.Type))
            .ForMember(dp => dp.Comments, opt => opt.Ignore())
            .ForMember(dp => dp.Likes, opt => opt.Ignore())
            .ForMember(dp => dp.Views, opt => opt.Ignore());

        CreateMap<Domain.Post, EnrichedPost>()
            .ForMember(ep => ep.UserId, opt => opt.MapFrom(dp => dp.UserId))
            .ForMember(ep => ep.Text, opt => opt.MapFrom(dp => dp.Text))
            .ForMember(ep => ep.Medias, opt => opt.MapFrom(dp => dp.Medias))
            .ForMember(ep => ep.Type, opt => opt.MapFrom(dp => dp.Type))
            .ForMember(ep => ep.Metadata, opt => opt.Ignore());
    }
}