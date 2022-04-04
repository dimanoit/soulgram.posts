using System.Collections.Generic;
using Soulgram.Posts.Application.Models.Post.Base;

namespace Soulgram.Posts.Application.Models.Post;

public record Comment : BaseComment
{
    public IEnumerable<Like> Likes { get; set; }
}