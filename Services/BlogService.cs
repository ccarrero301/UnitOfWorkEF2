using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataModel.Models;
using Microsoft.EntityFrameworkCore;
using UnitOfWork.Contracts.UnitOfWork;

namespace Services
{
    public class BlogService : IBlogService
    {
        private readonly IUnitOfWork<BloggingContext> _unitOfWork;

        public BlogService(IUnitOfWork<BloggingContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Blog>> GetAllBlogs()
        {
            var blogRepository = _unitOfWork.GetQueryableRepository<Blog>();

            var blogs = await blogRepository
                .GetPagedListAsync(
                    selector: blog => blog,
                    predicate: null,
                    orderBy: order => order.OrderBy(blog => blog.Id),
                    include: include => include.Include(blog => blog.Posts).ThenInclude(post => post.Comments),
                    pageIndex: 0,
                    pageSize: 20
                );

            return blogs.Items;
        }
    }
}
