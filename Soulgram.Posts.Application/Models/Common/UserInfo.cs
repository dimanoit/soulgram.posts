namespace Soulgram.Posts.Application.Models.Common
{
    public record UserInfo
    {
        public string Id { get; init; }
        public string FullName { get; init; }
        public string Nickname { get; init; }
        public string AvatarUrl { get; init; }
    }
}
