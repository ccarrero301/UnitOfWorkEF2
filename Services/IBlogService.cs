using System.Collections.Generic;
using System.Threading.Tasks;
using DataModel.Models;

namespace Services
{
    public interface IBlogService
    {
        Task<IEnumerable<Blog>> GetAllBlogs();
    }
}
