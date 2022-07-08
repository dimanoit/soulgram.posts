using System.Collections.Generic;
using System.Linq;
using Nest;

namespace Soulgram.Posts.Domain;

[ElasticsearchType(RelationName = "post")]
public record Post : BasePost
{
    [Text] 
    public string Description { get; init; }

    [Object(Enabled = false)] 
    public IEnumerable<string> Medias { get; set; } = Enumerable.Empty<string>();
    
    [Keyword] 
    public IEnumerable<string> Hashtags { get; init; } = Enumerable.Empty<string>();
    
    [Nested] 
    public IEnumerable<Comment> Comments { get; set; } = Enumerable.Empty<Comment>();
}