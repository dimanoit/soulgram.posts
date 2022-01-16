using Soulgram.Posts.Application.Models.Common;
using System;
using System.Collections.Generic;

namespace Soulgram.Posts.Application.Models.Post.Base
{
    public record Post
    {
        public UserInfo UserInfo { get; init; }
        public IEnumerable<string> Medias { get; init; }
        public string Text { get; init; }
        public bool IsArticle { get; init; }
    }
}
