﻿using Soulgram.Posts.Application.Models.Post.Base;

namespace Soulgram.Posts.Application.Models.Requests;

public record CommentEditRequest : BaseComment
{
    public string Id { get; init; }
}