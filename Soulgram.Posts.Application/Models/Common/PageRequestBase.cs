namespace Soulgram.Posts.Application.Models.Common
{
	public abstract record PageRequestBase
	{
		public int Skip { get; set; } = 0;
		public int Take { get; set; } = 20;
	}
}
