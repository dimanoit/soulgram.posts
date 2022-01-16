using System;
using Nest;

namespace Soulgram.Posts.Domain
{
    public record Comment
    {
        [Object(Enabled = false)]
        public string Text { get; init; }

        public DateTime Date { get; init; }
    }
}