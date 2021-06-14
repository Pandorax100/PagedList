using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Pandorax.PagedList
{
    /// <inheritdoc cref="IPagedList{T}"/>
    public class PagedList<T> : IPagedList<T>
    {
        private readonly List<T> _subset = new List<T>();

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedList{T}"/> class that divides the supplied enumerable into pages the size of the supplied <paramref name="pageSize"/>. The instance then only containes the objects contained in the subset specified by index.
        /// </summary>
        /// <param name="superset">The collection of objects to be divided into subsets.</param>
        /// <param name="pageNumber">The one-based index of the current page.</param>
        /// <param name="pageSize">The maximum size of any individual page.</param>
        /// <exception cref="ArgumentOutOfRangeException">The specified index cannot be less than zero.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The specified page size cannot be less than one.</exception>
        public PagedList(IEnumerable<T> superset, int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageNumber), pageNumber, "The specified page number cannot be less than one.");
            }

            if (pageSize < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The specified page size cannot be less than one.");
            }

            // Set source to blank list if superset is null to prevent exceptions
            TotalItemCount = superset == null ? 0 : superset.Count();
            PageSize = pageSize;
            PageNumber = pageNumber;
            TotalPageCount = TotalItemCount > 0
                        ? (int)Math.Ceiling(TotalItemCount / (double)PageSize)
                        : 0;

            // Add items to internal list
            if (superset != null && TotalItemCount > 0)
            {
                _subset.AddRange(pageNumber == 1
                    ? superset.Skip(0).Take(pageSize).ToList()
                    : superset.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList());
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedList{TElement}"/> class.
        /// </summary>
        /// <param name="subset">The enumerable of current page objects.</param>
        /// <param name="pageNumber">The 1 based index of the current page.</param>
        /// <param name="pageSize">The size of any individual page.</param>
        /// <param name="totalItemCount">The total number of items in the superset.</param>
        public PagedList(
            IEnumerable<T> subset,
            int pageNumber,
            int pageSize,
            int totalItemCount)
        {
            if (pageNumber < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "The specified page number cannot be less than one.");
            }

            if (pageSize < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize), "The specified page size cannot be less than one.");
            }

            _subset.AddRange(subset);
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalItemCount = totalItemCount;
            TotalPageCount = ((totalItemCount - 1) / pageSize) + 1;
        }

        /// <inheritdoc />
        public int Count => _subset.Count;

        /// <inheritdoc />
        public int TotalPageCount { get; }

        /// <inheritdoc />
        public int TotalItemCount { get; }

        /// <inheritdoc />
        public int PageNumber { get; }

        /// <inheritdoc />
        public int PageSize { get; }

#if !NET5_0
        /// <inheritdoc />
        public bool HasPreviousPage => PageNumber > 1;

        /// <inheritdoc />
        public bool HasNextPage => PageNumber < TotalPageCount;

        /// <inheritdoc />
        public bool IsFirstPage => PageNumber == 1;

        /// <inheritdoc />
        public bool IsLastPage => PageNumber >= TotalPageCount;
#endif

        /// <summary>
        /// Gets the element at the specified index.
        /// </summary>
        /// <param name = "index">The zero-based index of the element to get.</param>
        public T this[int index] => _subset[index];

        /// <inheritdoc/>
        public IEnumerator<T> GetEnumerator() => _subset.GetEnumerator();

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
