using Soulgram.Posts.Application.Models.Common;

namespace Soulgram.Posts.Application.Models.Requests;

public record PostsByUserIdRequest : PageRequestBase
{
    public string[] UsersIds { get; set; }
    public string CurrentUserId { get; set; }
}