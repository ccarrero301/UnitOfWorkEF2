namespace Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataModel.Models;

    public interface IBlogService
    {
        Task<IEnumerable<Blog>> GetAllBlogsAsync();

        Task<string> GetBlogTitleAsync(int blogId);

        Task<Blog> GetBlogNotIncludingPostsAndCommentsAsync(int blogId);

        Task<Blog> GetBlogIncludingPostsAndNotIncludingCommentsAsync(int blogId);

        Task<Blog> GetBlogIncludingPostsAndCommentsAsync(int blogId);

        Task<int> AddBlogAsync(Blog blog);
    }
}
