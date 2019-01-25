namespace Services.Comments
{
    using System.Threading.Tasks;
    using DataModel.Models;

    public interface ICommentService
    {
        Task<int> AddCommentToPostAsync(Comment comment);
    }
}