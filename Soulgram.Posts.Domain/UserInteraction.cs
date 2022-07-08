using System;
using Nest;

namespace Soulgram.Posts.Domain;

public record UserInteraction
{
    public DateTime CreateDate { get; init; }

    [Keyword(Name = "user_id")] 
    public string UserId { get; init; }
}