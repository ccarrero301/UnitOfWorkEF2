namespace Data.Comments.Services
{
    using System.Threading.Tasks;
    using UnitOfWork.Contracts.UnitOfWork;
    using Contracts;

    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork<BloggingContext> _unitOfWork;

        public CommentService(IUnitOfWork<BloggingContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<int> AddCommentToPostAsync(Comment comment)
        {
            var commentRepository = _unitOfWork.GetRepository<Comment>();

            commentRepository.Insert(comment);

            return _unitOfWork.SaveChangesAsync();
        }
    }
}