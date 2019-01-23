namespace Services
{
    using System.Threading.Tasks;
    using UnitOfWork.Contracts.UnitOfWork;
    using DataModel.Models;

    public class PostService : IPostService
    {
        private readonly IUnitOfWork<BloggingContext> _unitOfWork;

        public PostService(IUnitOfWork<BloggingContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<int> AddPostToBlogAsync(Post post)
        {
            var postRepository = _unitOfWork.GetRepository<Post>();

            postRepository.Insert(post);

            return _unitOfWork.SaveChangesAsync();
        }
    }
}