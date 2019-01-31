namespace AppServices.Comments.Services
{
    using System.Threading.Tasks;
    using DataCommentsContracts = Data.Comments.Contracts;
    using DomainComments = Domain.Comments;
    using Shared.DTOs;
    using Contracts;
    using AutoMapper;

    public class CommentService : ICommentService
    {
        private readonly IMapper _mapper;
        private readonly DataCommentsContracts.ICommentService _commentService;

        public CommentService(IMapper mapper, DataCommentsContracts.ICommentService commentService)
        {
            _mapper = mapper;
            _commentService = commentService;
        }

        public Task<int> AddCommentToPostAsync(Comment dtoComment)
        {
            var domainComment = _mapper.Map<DomainComments.Comment>(dtoComment);

            return _commentService.AddCommentToPostAsync(domainComment);
        }
    }
}