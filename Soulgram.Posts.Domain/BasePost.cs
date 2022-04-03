using System;
using System.Collections.Generic;
using Nest;

namespace Soulgram.Posts.Domain;

public record BasePost
{
    [Keyword(Name = "user_id")] public string UserId { get; init; }
    [Keyword] public DocumentType Type { get; init; }
    [Nested] public IEnumerable<UserInteraction> Likes { get; init; }
    [Nested] public IEnumerable<UserInteraction> Views { get; init; }
    [Date(Name = "creation_date")] public DateTime CreationDate { get; init; }
}