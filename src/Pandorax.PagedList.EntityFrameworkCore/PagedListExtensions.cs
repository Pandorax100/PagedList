using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Pandorax.PagedList.EntityFrameworkCore
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
        /// <param name="pageIndex">The one-based index of the page.</param>
        /// <param name="pageSize">The maximum size of any individual page.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A <see cref="IPagedList{T}"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The page index must be greater than zero.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The page size must be greater than zero.</exception>
        public static async Task<IPagedList<T>> ToPagedListAsync<T>(
            this IQueryable<T> query,
            int pageIndex,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            if (pageIndex <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pageIndex), pageIndex, "The page index must be greater than zero");
            }

            if (pageSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "The page size must be greater than zero");
            }

            int skipValue = pageSize * (pageIndex - 1);

            List<T> currentPage = await query.Skip(skipValue).Take(pageSize).ToListAsync(cancellationToken);

            int totalCount = await query.CountAsync(cancellationToken);

            return new PagedList<T>(currentPage, pageIndex, pageSize, totalCount);
        }
    }
}
