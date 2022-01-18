namespace Soulgram.Posts.Application.Models.Requests
{
    public record PostsByIdRequest
    {
        public string Id { get; init; }
    }
}