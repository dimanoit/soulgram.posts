using System;
using System.Threading.Tasks;
using Nest;
using Soulgram.File.Manager.Interfaces;
using Soulgram.Posts.Application.Models.Post;
using Soulgram.Posts.Application.Models.Requests;
using Soulgram.Posts.Application.Services;
using Soulgram.Posts.Domain;

namespace Soulgram.Posts.Application.Mapper;

public static class PostMapper
{
    public static Article ToArticle(this ArticlePublicationRequest request)
    {
        var article = new Article
        {
            Content = request.Text,
            Title = request.Title,
            UserId = request.UserId,
            Hashtags = request.Hashtags,
            Type = DocumentType.Article,
            CreationDate = DateTime.UtcNow
        };

        return article;
    }

    public static EnrichedPost ToEnrichedPost(this IHit<Post> post)
    {
        return post.Source.ToEnrichedPost(post.Id);
    }

    public static EnrichedPost ToEnrichedPost(this GetResponse<Post> post)
    {
        return post.Source.ToEnrichedPost(post.Id);
    }

    public static EnrichedPost ToEnrichedPost(this Post post, string id = "")
    {
        var enrichedPost = new EnrichedPost
        {
            Id = id,
            UserId = post.UserId,
            Text = post.Description,
            Medias = post.Medias,
            Hashtags = post.Hashtags
        };

        return enrichedPost;
    }

    public static async Task<Post> ToPost(
        this PostPublicationRequest request,
        IFileManager fileManager,
        ICurrentDateProvider dateProvider)
    {
        var post = new Post
        {
            UserId = request.UserId,
            Description = request.Text,
            Hashtags = request.Hashtags,
            Type = DocumentType.Post,
            CreationDate = dateProvider.Now
        };

        await UploadFiles(request, fileManager, post);

        return post;
    }

    private static async Task UploadFiles(PostPublicationRequest request, IFileManager fileManager, Post post)
    {
        var files = request.Files.ToFileInfos();

        if (files != null)
        {
            var linksOfUploadedFiles =
                await fileManager.UploadFilesAndGetIds(files, request.UserId);
            post.Medias = linksOfUploadedFiles;
        }
    }
}