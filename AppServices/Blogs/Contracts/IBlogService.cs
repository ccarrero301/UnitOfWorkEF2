namespace AppServices.Blogs.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Shared.DTOs;

    public interface IBlogService
    {
        Task<IEnumerable<Blog>> GetAllBlogsAsync();

        Task<string> GetBlogTitleAsync(int blogId);

        Task<Blog> GetBlogNotIncludingPostsAndCommentsAsync(int blogId);

        Task<Blog> GetBlogIncludingPostsAndNotIncludingCommentsAsync(int blogId);

        Task<Blog> GetBlogIncludingPostsAndCommentsAsync(int blogId);

        Task<int> AddBlogAsync(Blog dtoBlog);

        Task<int> DeleteBlogAsync(int blogId);
    }
}
