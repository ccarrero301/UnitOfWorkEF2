namespace AppServices.Comments.Contracts
{
    using System.Threading.Tasks;
    using Shared.DTOs;

    public interface ICommentService
    {
        Task<int> AddCommentToPostAsync(Comment dtoComment);
    }
}
