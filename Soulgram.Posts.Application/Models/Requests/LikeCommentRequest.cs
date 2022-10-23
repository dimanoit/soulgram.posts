namespace Soulgram.Posts.Application.Models.Requests;

public record LikeCommentRequest
{
    public string UserId { get; init; }
    public string PostId { get; init; }
    public string CommentId { get; init; }
}