namespace Soulgram.Posts.Application.Models.Requests
{
    public record PostUpdateRequest : PostPublicationRequest
    {
        public string PostId { get; init; }
    }
}