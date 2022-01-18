﻿using Soulgram.Posts.Application.Models.Post.Base;
using System.Collections.Generic;

namespace Soulgram.Posts.Application.Models.Post
{
	public record Comment : BaseComment
	{
		public IEnumerable<Like> Likes { get; set; }
	}
}
