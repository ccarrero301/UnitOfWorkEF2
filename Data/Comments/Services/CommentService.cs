namespace Data.Comments.Services
{
    using System.Threading.Tasks;
    using UnitOfWork.Contracts.UnitOfWork;
    using DomainComments = Domain.Comments;
    using Contracts;
    using AutoMapper;

    public class CommentService : ICommentService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<BloggingContext> _unitOfWork;

        public CommentService(IMapper mapper, IUnitOfWork<BloggingContext> unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public Task<int> AddCommentToPostAsync(DomainComments.Comment domainComment)
        {
            var dataComment = _mapper.Map<Comment>(domainComment);

            var commentRepository = _unitOfWork.GetRepository<Comment>();

            commentRepository.Insert(dataComment);

            return _unitOfWork.SaveChangesAsync();
        }
    }
}