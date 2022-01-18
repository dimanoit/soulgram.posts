using System;
using Nest;

namespace Soulgram.Posts.Domain
{
    public record Like
    {
        public DateTime Date { get; init; }

        [Keyword(Name = "user_id")]
        public string UserId { get; init; }
    }
}