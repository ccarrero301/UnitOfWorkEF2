﻿namespace Shared.DTOs
{
    using System.Collections.Generic;

    public class Post
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public IEnumerable<Comment> Comments { get; set; }

        public int BlogId { get; set; }

        public Blog Blog { get; set; }
    }
}
