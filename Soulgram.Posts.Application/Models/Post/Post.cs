using Soulgram.Posts.Application.Models.Post.Base;

namespace Soulgram.Posts.Application.Models.Post
{
	public record Post : BasePost
	{
		public PostMetadata Metadata { get; set; }
	}
}
