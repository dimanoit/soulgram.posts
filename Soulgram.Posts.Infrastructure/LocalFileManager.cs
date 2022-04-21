using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs.Models;
using Soulgram.File.Manager.Interfaces;
using FileInfo = Soulgram.File.Manager.Models.FileInfo;

namespace Soulgram.Posts.Infrastructure;

public class LocalFileManager : IFileManager
{
    public async Task<string> UploadFileAsync(FileInfo file, string userId)
    {
        var filePath = "D:/SoulgramUserFiles/" + file.Name;
        await using (var fileStream = System.IO.File.Create(filePath))
        {
            file.Content.Seek(0, SeekOrigin.Begin);
            await file.Content.CopyToAsync(fileStream);
            await file.Content.DisposeAsync();
        }

        return filePath;
    }

    public async Task<IEnumerable<string>> UploadFilesAndGetIds(IEnumerable<FileInfo> files, string userId)
    {
        var uploadTasks = files
            .Select(f => UploadFileAsync(f, userId))
            .ToArray();

        await Task.WhenAll(uploadTasks);

        return uploadTasks.Select(t => t.Result);
    }

    public Task<string> UploadFileAsync(FileInfo file, string userId, BlobUploadOptions options)
    {
        throw new NotImplementedException();
    }
}