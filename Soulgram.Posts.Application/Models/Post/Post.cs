namespace Soulgram.Posts.Application.Models.Post;

public record EnrichedPost : Base.Post
{
    public PostMetadata Metadata { get; set; }
    public string Id { get; init; }
}