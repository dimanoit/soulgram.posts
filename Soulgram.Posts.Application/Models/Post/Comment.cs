using Soulgram.Posts.Application.Models.Post.Base;
using System.Collections.Generic;

namespace Soulgram.Posts.Application.Models.Post
{
	public record Comment : BaseComment
	{
		public Media ImageProfileUrl { get; set; }
		public IEnumerable<string> LikersIds { get; set; }
	}
}
