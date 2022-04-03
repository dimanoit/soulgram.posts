using System;

namespace Soulgram.Posts.Application.Services;

public interface ICurrentDateProvider
{
    DateTime Now { get; set; }
}