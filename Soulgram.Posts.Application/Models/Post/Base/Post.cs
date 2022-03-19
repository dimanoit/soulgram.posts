using System.Collections.Generic;

namespace Soulgram.Posts.Application.Models.Post.Base;

public record Post
{
    public string UserId { get; init; }
    public string Text { get; init; }
    public string[] Hashtags { get; set; }
    public IEnumerable<string> Medias { get; init; }
    public PostType Type { get; init; }
}