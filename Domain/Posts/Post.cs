namespace Domain.Posts
{
    using System.Collections.Generic;
    using Comments;

    public class Post
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public IEnumerable<Comment> Comments { get; set; }

        public int BlogId { get; set; }
    }
}
