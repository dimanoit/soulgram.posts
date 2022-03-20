using Microsoft.AspNetCore.Http;

namespace Soulgram.Posts.Application.Models.Requests;

public record PostPublicationRequest : Post.Base.Post
{
    public IFormFile[] Files { get; set; }
}