namespace AppServices.Blogs.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Shared.DTOs;

    public interface IBlogService
    {
        Task<IEnumerable<Blog>> GetAllBlogsAsync();
    }
}
