using AutoMapper;
using Soulgram.Posts.Application.Models.Common;

namespace Soulgram.Posts.Infrastracture.Mapper.Profiles
{
    public class UserInfoDomainUserInfoProfile : Profile
    {
        public UserInfoDomainUserInfoProfile()
        {
            CreateMap<UserInfo, Domain.UserInfo>().ReverseMap();
        }
    }
}