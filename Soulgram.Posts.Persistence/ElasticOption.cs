namespace Soulgram.Posts.Persistence;

public record ElasticOption
{
    public string Url { get; init; }
    public string Index { get; init; }
}