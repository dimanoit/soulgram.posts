using Nest;

namespace Soulgram.Posts.Domain;

public record Comment : BasePost
{
    [Object(Enabled = false)] public string Data { get; init; }
}