namespace Soulgram.Posts.Application.Models.Requests;

public record ArticlePublicationRequest(string Title) : Post.Base.Post;