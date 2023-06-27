using System.Collections;
using System.Collections.Generic;

namespace Pandorax.PagedList
{
    /// <summary>
    /// Represents a subset of a collection of objects.
    /// </summary>
    /// <seealso cref="IEnumerable"/>
    public interface IPagedList : IEnumerable
    {
        /// <summary>
        /// Gets the total number of pages.
        /// </summary>
        int TotalPageCount { get; }

        /// <summary>
        /// Gets the total number of items.
        /// </summary>
        int TotalItemCount { get; }

        /// <summary>
        /// Gets the 1 based index of the current page.
        /// </summary>
        int PageIndex { get; }

        /// <summary>
        /// Gets the maximum size of any individual page.
        /// </summary>
        int PageSize { get; }

        /// <summary>
        /// Gets a value indicating whether this is NOT the first subset within the superset.
        /// </summary>
        bool HasPreviousPage { get; }

        /// <summary>
        /// Gets a value indicating whether this is NOT the last subset within the superset.
        /// </summary>
        bool HasNextPage { get; }

        /// <summary>
        /// Gets a value indicating whether this is the first subset within the superset.
        /// </summary>
        bool IsFirstPage { get; }

        /// <summary>
        /// Gets a value indicating whether this is the last subset within the superset.
        /// </summary>
        bool IsLastPage { get; }

        /// <summary>
        /// Gets the number of elements contained on this page.
        /// </summary>
        int Count { get; }
    }

    /// <summary>
    /// Represents a subset of a collection of objects.
    /// </summary>
    /// <typeparam name="T">The type of object the collection should contain.</typeparam>
    /// <seealso cref="IEnumerable{T}"/>
    public interface IPagedList<out T> : IEnumerable<T>, IPagedList
    {
        /// <summary>
        /// Gets the element at the specified index from the current page.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get.</param>
        T this[int index] { get; }
    }
}
