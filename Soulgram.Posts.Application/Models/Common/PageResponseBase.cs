using System.Collections.Generic;

namespace Soulgram.Posts.Application.Models.Common;

public abstract record PageResponseBase<T>
{
    public IEnumerable<T> Data { get; set; }
    public int TotalCount { get; set; }
}