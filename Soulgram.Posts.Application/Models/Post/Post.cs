using System.Collections.Generic;

namespace Soulgram.Posts.Application.Models.Post;

public record EnrichedPost : Base.Post
{
    public PostMetadata Metadata { get; set; }
    public IEnumerable<string> Medias { get; set; }
    public string Id { get; init; }
}