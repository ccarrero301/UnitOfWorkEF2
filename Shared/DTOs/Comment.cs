﻿namespace Shared.DTOs
{
    public class Comment
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int PostId { get; set; }
    }
}
