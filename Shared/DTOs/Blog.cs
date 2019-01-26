namespace Shared.DTOs
{
    using System.Collections.Generic;

    public class Blog
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public string Title { get; set; }

        public IEnumerable<Post> Posts { get; set; }
    }
}
