using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Soulgram.File.Manager.Models;

namespace Soulgram.Posts.Application.Mapper;

public static class FileMapper
{
    public static FileInfo ToFileInfo(this IFormFile file)
    {
        var fileInfo = new FileInfo
        {
            Content = file.OpenReadStream(),
            ContentType = file.ContentType,
            Name = file.FileName
        };

        return fileInfo;
    }

    public static IEnumerable<FileInfo> ToFileInfos(this IEnumerable<IFormFile> files)
    {
        return files?.Select(ToFileInfo);
    }
}