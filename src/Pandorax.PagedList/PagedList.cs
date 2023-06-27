using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Pandorax.PagedList
{
    /// <inheritdoc cref="IPagedList{T}"/>
    public class PagedList<T> : IPagedList<T>
    {
        private readonly List<T> _page = new List<T>();

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedList{T}"/> class that divides the supplied enumerable into pages the size of the supplied <paramref name="pageSize"/>. The instance then only containes the objects contained in the page specified by index.
        /// </summary>
        /// <param name="source">The collection of objects to be divided into pages.</param>
        /// <param name="pageIndex">The one-based index of the current page.</param>
        /// <param name="pageSize">The maximum size of any individual page.</param>
        /// <exception cref="ArgumentOutOfRangeException">The specified index cannot be less than zero.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The specified page size cannot be less than one.</exception>
        public PagedList(IEnumerable<T> source, int pageIndex, int pageSize)
        {
            if (pageIndex < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageIndex), pageIndex, "The specified page index cannot be less than one.");
            }

            if (pageSize < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The specified page size cannot be less than one.");
            }

            // Set source to blank list if superset is null to prevent exceptions
            TotalItemCount = source == null ? 0 : source.Count();
            PageSize = pageSize;
            PageIndex = pageIndex;
            TotalPageCount = TotalItemCount > 0
                        ? (int)Math.Ceiling(TotalItemCount / (double)PageSize)
                        : 0;

            // Add items to internal list
            if (source != null && TotalItemCount > 0)
            {
                _page.AddRange(pageIndex == 1
                    ? source.Skip(0).Take(pageSize).ToList()
                    : source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList());
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedList{TElement}"/> class.
        /// </summary>
        /// <param name="subset">The enumerable of current page objects.</param>
        /// <param name="pageIndex">The 1 based index of the current page.</param>
        /// <param name="pageSize">The size of any individual page.</param>
        /// <param name="totalItemCount">The total number of items in the superset.</param>
        public PagedList(
            IEnumerable<T> subset,
            int pageIndex,
            int pageSize,
            int totalItemCount)
        {
            if (pageIndex < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageIndex), pageIndex, "The specified page index cannot be less than one.");
            }

            if (pageSize < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize), "The specified page size cannot be less than one.");
            }

            _page.AddRange(subset);
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalItemCount = totalItemCount;
            TotalPageCount = ((totalItemCount - 1) / pageSize) + 1;
        }

        /// <inheritdoc />
        public int Count => _page.Count;

        /// <inheritdoc />
        public int TotalPageCount { get; }

        /// <inheritdoc />
        public int TotalItemCount { get; }

        /// <inheritdoc />
        public int PageIndex { get; }

        /// <inheritdoc />
        public int PageSize { get; }

        /// <inheritdoc />
        public bool HasPreviousPage => PageIndex > 1;

        /// <inheritdoc />
        public bool HasNextPage => PageIndex < TotalPageCount;

        /// <inheritdoc />
        public bool IsFirstPage => PageIndex == 1;

        /// <inheritdoc />
        public bool IsLastPage => PageIndex >= TotalPageCount;

        /// <inheritdoc />
        public T this[int index] => _page[index];

        /// <inheritdoc/>
        public IEnumerator<T> GetEnumerator() => _page.GetEnumerator();

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
