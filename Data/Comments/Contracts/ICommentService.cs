namespace Data.Comments.Contracts
{
    using System.Threading.Tasks;
    using DomainComments = Domain.Comments;

    public interface ICommentService
    {
        Task<int> AddCommentToPostAsync(DomainComments.Comment comment);
    }
}