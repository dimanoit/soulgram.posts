using Nest;
using System;
using System.Collections.Generic;

namespace Soulgram.Posts.Domain
{
    public record Comment
    {
        [Object(Enabled = false)]
        public string Text { get; init; }

        [Keyword(Name = "user_id")]
        public string UserId { get; init; }

        public DateTime Date { get; init; }

        [Nested]
        public IEnumerable<Like> Likes { get; init; }
    }
}