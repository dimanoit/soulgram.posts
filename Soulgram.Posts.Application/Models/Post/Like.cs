using System;

namespace Soulgram.Posts.Application.Models.Post;

public record Like
{
    public string UserId { get; init; }
    public DateTime Date { get; init; }
}