using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pandorax.PagedList.EntityFramework
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
        /// <param name="query">The collection of objects to be divided into pages.</param>
        /// <param name="pageNumber">The one-based index of the subset of objects to be contained by this instance.</param>
        /// <param name="pageSize">The maximum size of any individual page.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A subset of this collection of objects that can be individually accessed by index.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The page number must be greater than zero.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The page size must be greater than zero.</exception>
        public static async Task<IPagedList<T>> ToPagedListAsync<T>(this IQueryable<T> query, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            if (pageNumber <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "The page number must be greater than zero");
            }

            if (pageSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize), "The page size must be greater than zero");
            }

            int skipValue = pageSize * (pageNumber - 1);

            List<T> currentPage = await query.Skip(skipValue).Take(pageSize).ToListAsync(cancellationToken);

            int totalCount = await query.CountAsync(cancellationToken);

            return new PagedList<T>(currentPage, pageNumber, pageSize, totalCount);
        }
    }
}
