﻿namespace Data.Blogs.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UnitOfWork.Contracts.UnitOfWork;
    using DomainBlogs = Domain.Blogs;
    using Specifications;
    using Contracts;
    using AutoMapper;

    public class BlogService : IBlogService
    {
        private readonly IUnitOfWork<BloggingContext> _unitOfWork;
        private readonly IMapper _mapper;

        public BlogService(IMapper mapper, IUnitOfWork<BloggingContext> unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<DomainBlogs.Blog>> GetAllBlogsAsync()
        {
            var queryableBlogRepository = _unitOfWork.GetQueryableRepository<Blog>();

            var dataBlogsPagedList = await queryableBlogRepository
                .GetPagedListAsync(blog => blog, new AllBlogsSpecification()).ConfigureAwait(false);

            var domainBlogs = _mapper.Map<IEnumerable<DomainBlogs.Blog>>(dataBlogsPagedList.Items);

            return domainBlogs;
        }

        public Task<string> GetBlogTitleAsync(int blogId)
        {
            var queryableBlogRepository = _unitOfWork.GetQueryableRepository<Blog>();

            return queryableBlogRepository.GetFirstOrDefaultAsync(blog => blog.Title,
                new BlogTitleSpecification(blogId));
        }

        public async Task<DomainBlogs.Blog> GetBlogNotIncludingPostsAndCommentsAsync(int blogId)
        {
            var queryableBlogRepository = _unitOfWork.GetQueryableRepository<Blog>();

            var dataBlog = await queryableBlogRepository.GetFirstOrDefaultAsync(new BlogSpecification(blogId))
                .ConfigureAwait(false);

            var domainBlog = _mapper.Map<DomainBlogs.Blog>(dataBlog);

            return domainBlog;
        }

        public async Task<DomainBlogs.Blog> GetBlogIncludingPostsAndNotIncludingCommentsAsync(int blogId)
        {
            var queryableBlogRepository = _unitOfWork.GetQueryableRepository<Blog>();

            var dataBlog = await queryableBlogRepository.GetFirstOrDefaultAsync(new BlogWithPostsSpecification(blogId))
                .ConfigureAwait(false);

            var domainBlog = _mapper.Map<DomainBlogs.Blog>(dataBlog);

            return domainBlog;
        }

        public async Task<DomainBlogs.Blog> GetBlogIncludingPostsAndCommentsAsync(int blogId)
        {
            var queryableBlogRepository = _unitOfWork.GetQueryableRepository<Blog>();

            var dataBlog =
                await queryableBlogRepository.GetFirstOrDefaultAsync(new BlogWithPostsAndCommentsSpecification(blogId))
                    .ConfigureAwait(false);

            var domainBlog = _mapper.Map<Domain.Blogs.Blog>(dataBlog);

            return domainBlog;
        }

        public Task<int> AddBlogAsync(DomainBlogs.Blog domainBlog)
        {
            var dataBlog = _mapper.Map<Blog>(domainBlog);

            var blogRepository = _unitOfWork.GetRepository<Blog>();

            blogRepository.Insert(dataBlog);

            return _unitOfWork.SaveChangesAsync();
        }

        public Task<int> DeleteBlogAsync(int blogId)
        {
            var blogRepository = _unitOfWork.GetRepository<Blog>();

            blogRepository.Delete(blogId);

            return _unitOfWork.SaveChangesAsync();
        }
    }
}