namespace Data.Posts
{
    using System.Collections.Generic;
    using Blogs;
    using Comments;

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