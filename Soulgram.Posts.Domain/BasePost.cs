using System;
using System.Collections.Generic;
using System.Linq;
using Nest;

namespace Soulgram.Posts.Domain;

public record BasePost
{
    [Keyword(Name = "user_id")] 
    public string UserId { get; init; }
    
    [Keyword] 
    public DocumentType Type { get; init; }
    
    [Keyword] 
    public PostState State { get; init; }
    
    [Nested] 
    public IEnumerable<UserInteraction> Likes { get; init; } = Enumerable.Empty<UserInteraction>();
    
    [Nested] 
    public IEnumerable<UserInteraction> Views { get; init; } = Enumerable.Empty<UserInteraction>();
    
    [Date(Name = "creation_date")] 
    public DateTime CreationDate { get; init; }
}