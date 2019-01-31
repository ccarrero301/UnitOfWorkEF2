namespace Data.Posts.Services
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using UnitOfWork.Contracts.UnitOfWork;
    using DomainPosts = Domain.Posts;
    using Shared.Patterns.Specification.Base;
    using Specifications;
    using Contracts;
    using AutoMapper;

    public class PostService : IPostService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<BloggingContext> _unitOfWork;

        public PostService(IMapper mapper, IUnitOfWork<BloggingContext> unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public Task<int> AddPostToBlogAsync(DomainPosts.Post domainPost)
        {
            var dataPost = _mapper.Map<Post>(domainPost);

            var postRepository = _unitOfWork.GetRepository<Post>();

            postRepository.Insert(dataPost);

            return _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<DomainPosts.Post>> GetPostsFromBlogContainingWordAsync(int blogId, string word)
        {
            var queryablePostRepository = _unitOfWork.GetQueryableRepository<Post>();

            var postByBlogIdSpecification = new PostByBlogIdSpecification(blogId);
            var postContainsWordInContentSpecification = new PostContainsWordInContentSpecification(word);
            var postSpecification = postByBlogIdSpecification & postContainsWordInContentSpecification;

            var dataPostsPagedList = await queryablePostRepository
                .GetPagedListAsync(postSpecification as ExpressionSpecification<Post>).ConfigureAwait(false);

            var domainPosts = _mapper.Map<IEnumerable<DomainPosts.Post>>(dataPostsPagedList.Items);

            return domainPosts;
        }
    }
}