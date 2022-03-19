using System;
using System.Collections.Generic;
using Nest;

namespace Soulgram.Posts.Domain;

[ElasticsearchType(RelationName = "post")]
public record Post
{
    [Keyword(Name = "user_id")] public string UserId { get; init; }
    [Text] public string Text { get; init; }
    public DateTime Date { get; init; }
    [Keyword(Name = "post_type")] public PostType Type { get; init; }
    public int Views { get; init; }
    [Object(Enabled = false)] public IEnumerable<string> Medias { get; set; }
    [Keyword] public IEnumerable<string> Hashtags { get; init; }
    [Nested] public IEnumerable<Like> Likes { get; init; }
    [Nested] public IEnumerable<Comment> Comments { get; init; }
}