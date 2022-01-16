using Soulgram.Posts.Application.Models.Common;
using Soulgram.Posts.Application.Models.Post;

namespace Soulgram.Posts.Application.Models.Responses
{
	public record PostsByIdResponse: PageResponseBase<EnrichedPost>;
}
