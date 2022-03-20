using AutoMapper;
using Microsoft.AspNetCore.Http;
using Soulgram.File.Manager.Models;

namespace Soulgram.Posts.Infrastructure.Mapper.Profiles;

internal class FileManagementProfile : Profile
{
    public FileManagementProfile()
    {
        CreateMap<IFormFile, FileInfo>()
            .ForMember(fi => fi.Name, opt => opt.MapFrom(iff => iff.FileName))
            .ForMember(fi => fi.ContentType, opt => opt.MapFrom(iff => iff.ContentType))
            .ForMember(fi => fi.Content, opt => opt.MapFrom(iff => iff.OpenReadStream()));
    }
}