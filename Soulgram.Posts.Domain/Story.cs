using System.Collections.Generic;
using System.Linq;
using Nest;

namespace Soulgram.Posts.Domain;

[ElasticsearchType(RelationName = "story")]
public record Story : BasePost
{
    [Text(Name = "user_group")] public string UserGroup { get; init; }

    [Object(Enabled = false)] public IEnumerable<string> Medias { get; set; } = Enumerable.Empty<string>();

    [Keyword] public IEnumerable<string> Hashtags { get; init; } = Enumerable.Empty<string>();
}