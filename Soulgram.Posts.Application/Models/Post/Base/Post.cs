﻿using System.Collections.Generic;

namespace Soulgram.Posts.Application.Models.Post.Base
{
    public record Post
    {
        public string UserId { get; init; }
        public string Text { get; init; }
        public IEnumerable<string> Medias { get; init; }
        public bool IsArticle { get; init; }
    }
}
