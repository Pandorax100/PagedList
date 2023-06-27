using System.Collections.Generic;

namespace Pandorax.PagedList
{
    /// <summary>
    /// Container for extension methods designed to simplify the creation of instances of <see cref="PagedList{T}"/>.
    /// </summary>
    public static class PagedListExtensions
    {
        /// <summary>
        /// Creates a <see cref="IPagedList{T}"/> of this collection of objects that can be individually accessed by index.
        /// </summary>
        /// <typeparam name="T">The type of object the collection should contain.</typeparam>
        /// <param name="source">The collection of objects to be divided into pages.</param>
        /// <param name="pageIndex">The one-based index of the page.</param>
        /// <param name="pageSize">The maximum size of any individual page.</param>
        /// <returns>A <see cref="IPagedList{T}"/> from this collection.</returns>
        /// <seealso cref="PagedList{T}"/>
        public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> source, int pageIndex, int pageSize)
        {
            return new PagedList<T>(source, pageIndex, pageSize);
        }
    }
}
