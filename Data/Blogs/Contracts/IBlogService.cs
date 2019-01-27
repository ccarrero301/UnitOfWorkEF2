namespace Data.Blogs.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DomainBlogs = Domain.Blogs;
    
    public interface IBlogService
    {
        Task<IEnumerable<DomainBlogs.Blog>> GetAllBlogsAsync();

        Task<string> GetBlogTitleAsync(int blogId);

        Task<DomainBlogs.Blog> GetBlogNotIncludingPostsAndCommentsAsync(int blogId);

        Task<DomainBlogs.Blog> GetBlogIncludingPostsAndNotIncludingCommentsAsync(int blogId);

        Task<DomainBlogs.Blog> GetBlogIncludingPostsAndCommentsAsync(int blogId);

        Task<int> AddBlogAsync(DomainBlogs.Blog domainBlog);

        Task<int> DeleteBlogAsync(int blogId);
    }
}