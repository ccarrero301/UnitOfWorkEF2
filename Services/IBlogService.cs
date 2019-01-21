namespace Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataModel.Models;

    public interface IBlogService
    {
        Task<IEnumerable<Blog>> GetAllBlogs();

        Task<string> GetBlogTitle(int blogId);

        Task<Blog> GetBlogNotIncludingPostsAndComments(int blogId);

        Task<Blog> GetBlogIncludingPostsAndNotIncludingComments(int blogId);

        Task<Blog> GetBlogIncludingPostsAndComments(int blogId);

        Task<int> AddPostToBlog(int blogId, Post post);
    }
}
