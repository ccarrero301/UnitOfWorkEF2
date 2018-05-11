namespace UnitOfWork.Contracts.PagedList
{
    using System.Collections.Generic;

    public interface IPagedList<TEntity>
    {
        int IndexFrom { get; }

        int PageIndex { get; }

        int PageSize { get; }

        int TotalCount { get; }

        int TotalPages { get; }

        IList<TEntity> Items { get; }

        bool HasPreviousPage { get; }

        bool HasNextPage { get; }
    }
}