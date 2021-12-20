using Soulgram.Posts.Application.Models.Common;

namespace Soulgram.Posts.Application.Models.Responses
{
	public record PostsByIdResponse: PageResponseBase<Post.Post>;
}
