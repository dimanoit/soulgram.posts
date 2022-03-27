using System.Collections.Generic;
using Nest;

namespace Soulgram.Posts.Domain;

[ElasticsearchType(RelationName = "post")]
public record Post : BasePost
{
    [Text] public string Description { get; init; }

    [Object(Enabled = false)] public IEnumerable<string> Medias { get; set; }
    [Keyword] public IEnumerable<string> Hashtags { get; init; }
    [Nested] public IEnumerable<Comment> Comments { get; set; }
}