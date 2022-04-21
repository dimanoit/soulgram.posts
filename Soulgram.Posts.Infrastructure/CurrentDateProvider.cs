using System;
using Soulgram.Posts.Application.Services;

namespace Soulgram.Posts.Infrastructure;

internal sealed class CurrentDateProvider : ICurrentDateProvider
{
    public DateTime Now { get; } = DateTime.UtcNow;
}