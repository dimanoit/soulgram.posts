using System.Collections.Generic;

namespace Soulgram.Posts.Application.Models.Post
{
	public record PostMetadata
	{
		public int Views { get; set; }
		public IEnumerable<string> LikersIds { get; set; }
		public IEnumerable<Comment> Comments { get; set; }
	}
}
