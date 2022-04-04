﻿using System;

namespace Soulgram.Posts.Application.Models.Post.Base;

public record BaseComment
{
    public string UserId { get; set; }
    public string Text { get; set; }
    public DateTime Time { get; set; }
}