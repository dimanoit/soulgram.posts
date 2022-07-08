using System;
using System.Collections.Generic;
using Soulgram.Posts.Domain;

namespace Soulgram.Posts.Application.Models.Post;

public record EnrichedPost : Base.Post
{
    public string Id { get; init; }
    public DateTime CreationDate { get; init; }
    public bool Liked { get; init; }
    public PostMetadata Metadata { get; set; }
    public IEnumerable<string> Medias { get; set; }
}