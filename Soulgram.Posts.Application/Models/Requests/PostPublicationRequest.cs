using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Soulgram.Posts.Application.Models.Requests;

public record PostPublicationRequest : Post.Base.Post
{
    public IEnumerable<IFormFile> Files { get; set; }
}