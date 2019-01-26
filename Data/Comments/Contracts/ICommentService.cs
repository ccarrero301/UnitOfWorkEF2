namespace Data.Comments.Contracts
{
    using System.Threading.Tasks;

    public interface ICommentService
    {
        Task<int> AddCommentToPostAsync(Comment comment);
    }
}