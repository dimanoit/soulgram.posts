using Soulgram.Posts.Application.Models.Common;

namespace Soulgram.Posts.Application.Models.Post
{
	public record Media
	{
		public FileType FileType { get; set; }
		public string Url { get; set; }
	}
}
