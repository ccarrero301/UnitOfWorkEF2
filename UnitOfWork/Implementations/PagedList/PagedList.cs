﻿namespace UnitOfWork.Implementations.PagedList
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using UnitOfWork.Contracts.PagedList;

    internal class PagedList<T> : IPagedList<T>
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public int TotalPages { get; set; }

        public int IndexFrom { get; set; }


        public IList<T> Items { get; set; }

        public bool HasPreviousPage => PageIndex - IndexFrom > 0;

        public bool HasNextPage => PageIndex - IndexFrom + 1 < TotalPages;

        internal PagedList(IEnumerable<T> source, int pageIndex, int pageSize, int indexFrom)
        {
            if (indexFrom > pageIndex)
                throw new ArgumentException(
                    $"indexFrom: {indexFrom} > pageIndex: {pageIndex}, must indexFrom <= pageIndex");

            var sourceAsList = source.ToList();

            PageIndex = pageIndex;
            PageSize = pageSize;
            IndexFrom = indexFrom;
            TotalCount = sourceAsList.Count;
            TotalPages = (int) Math.Ceiling(TotalCount / (double) PageSize);

            Items = sourceAsList.Skip((PageIndex - IndexFrom) * PageSize).Take(PageSize).ToList();
        }

        internal PagedList() => Items = Array.Empty<T>();
    }

    internal class PagedList<TSource, TResult> : IPagedList<TResult>
    {
        public int PageIndex { get; }

        public int PageSize { get; }

        public int TotalCount { get; }

        public int TotalPages { get; }

        public int IndexFrom { get; }

        public IList<TResult> Items { get; }

        public bool HasPreviousPage => PageIndex - IndexFrom > 0;

        public bool HasNextPage => PageIndex - IndexFrom + 1 < TotalPages;

        public PagedList(IEnumerable<TSource> source, Func<IEnumerable<TSource>, IEnumerable<TResult>> converter,
            int pageIndex, int pageSize, int indexFrom)
        {
            if (indexFrom > pageIndex)
                throw new ArgumentException(
                    $"indexFrom: {indexFrom} > pageIndex: {pageIndex}, must indexFrom <= pageIndex");

            var sourceAsList = source.ToList();

            PageIndex = pageIndex;
            PageSize = pageSize;
            IndexFrom = indexFrom;
            TotalCount = sourceAsList.Count;
            TotalPages = (int) Math.Ceiling(TotalCount / (double) PageSize);

            var items = sourceAsList.Skip((PageIndex - IndexFrom) * PageSize).Take(PageSize).ToArray();

            Items = new List<TResult>(converter(items));
        }

        public PagedList(IPagedList<TSource> source, Func<IEnumerable<TSource>, IEnumerable<TResult>> converter)
        {
            PageIndex = source.PageIndex;
            PageSize = source.PageSize;
            IndexFrom = source.IndexFrom;
            TotalCount = source.TotalCount;
            TotalPages = source.TotalPages;

            Items = new List<TResult>(converter(source.Items));
        }
    }

    internal static class PagedList
    {
        public static IPagedList<T> Empty<T>() => new PagedList<T>();

        public static IPagedList<TResult> From<TResult, TSource>(IPagedList<TSource> source,
            Func<IEnumerable<TSource>, IEnumerable<TResult>> converter) =>
            new PagedList<TSource, TResult>(source, converter);
    }
}