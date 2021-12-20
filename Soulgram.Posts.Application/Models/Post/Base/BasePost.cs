using System.Collections.Generic;

namespace Soulgram.Posts.Application.Models.Post.Base
{
	public abstract record BasePost
	{
		public string UserId { get; set; }
		public IEnumerable<Media> Medias { get; set; }
		public string Text { get; set; }
	}
}
