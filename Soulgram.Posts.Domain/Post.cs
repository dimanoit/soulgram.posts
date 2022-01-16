using System;
using System.Collections.Generic;
using Nest;

namespace Soulgram.Posts.Domain
{
    [ElasticsearchType(RelationName = "post")]
    public record Post
    {
        [Text]
        public string Text { get; init; }
        public DateTime Date { get; init; }

        [Boolean(Name = "is_article", NullValue = false, Store = true)]
        public bool IsArticle { get; init; }

        public int Views { get; init; }

        [Nested]
        public UserInfo UserInfo { get; init; }

        [Object(Enabled = false)]
        public IEnumerable<string> Medias { get; init; }

        [Nested]
        public IEnumerable<Like> Likes { get; init; }

        [Nested]
        public IEnumerable<Comment> Comments { get; init; }

    }
}