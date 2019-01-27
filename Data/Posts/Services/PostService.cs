namespace Data.Posts.Services
{
    using System.Threading.Tasks;
    using UnitOfWork.Contracts.UnitOfWork;
    using DomainPosts = Domain.Posts;
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
    }
}