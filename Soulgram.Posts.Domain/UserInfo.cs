using Nest;

namespace Soulgram.Posts.Domain
{
    public record UserInfo
    {
        [Keyword(Name = "user_id")]
        public string Id { get; init; }

        [Object(Enabled = false)]
        public string FullName { get; init; }

        [Object(Enabled = false)]
        public string Nickname { get; init; }

        [Object(Enabled = false)]
        public string AvatarUrl { get; init; }
    }
}