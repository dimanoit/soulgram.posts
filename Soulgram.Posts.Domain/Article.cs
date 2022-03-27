using System.Collections.Generic;
using Nest;

namespace Soulgram.Posts.Domain;

[ElasticsearchType(RelationName = "article")]
public record Article : BasePost
{
    [Text] public string Content { get; init; }
    public string Title { get; init; }

    [Keyword] public IEnumerable<string> Hashtags { get; init; }
}