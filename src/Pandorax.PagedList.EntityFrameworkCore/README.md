# Pandorax.PagedList.EntityFrameworkCore

`Pandorax.PagedList.EntityFrameworkCore` is a lightweight extension library for handling paginated data asynchronously in C# applications using Entity Framework Core. It simplifies working with paginated data by providing an easy-to-use API for `IQueryable<T>`.

## Installation

You can install the library via NuGet:

```bash
Install-Package Pandorax.PagedList.EntityFrameworkCore
```

## Features

- Extension methods to asynchronously create paginated lists from any `IQueryable<T>`.
- Seamless integration with Entity Framework Core for database queries.
- Useful for building pagination functionality in web applications, APIs, or any project where data paging is required.

## Usage

### Example: Converting an `IQueryable<T>` to a Paged List

The library provides an extension method, `ToPagedListAsync<T>`, which creates a paged list asynchronously from an `IQueryable<T>`.

```csharp
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pandorax.PagedList.EntityFrameworkCore;

class Program
{
    static async Task Main()
    {
        using var context = new MyDbContext();

        var query = context.Products.OrderBy(p => p.Name);

        int pageIndex = 1; // Page number (1-based index)
        int pageSize = 10; // Number of items per page

        var pagedList = await query.ToPagedListAsync(pageIndex, pageSize);

        Console.WriteLine($"Page {pageIndex} of {pagedList.TotalPageCount}");
        foreach (var product in pagedList)
        {
            Console.WriteLine(product.Name);
        }
    }
}

public class MyDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
}
```

### IPagedList Interface

The `IPagedList<T>` interface includes properties and methods to work with paginated data:

- `int PageIndex`: The current page index (0-based).
- `int PageSize`: The number of items per page.
- `int Count`: The number of items contained on this page.
- `int TotalItemCount`: The total number of items.
- `int TotalPageCount`: The total number of pages.
- `bool HasPreviousPage`: Indicates if there is a previous page.
- `bool HasNextPage`: Indicates if there is a next page.
- `bool IsFirstPage`: Indicates if this page is the first page.
- `bool IsLastPage`: Indicates if this page is the last page.

## Requirements

- .NET 8.0 or higher.
- Entity Framework Core 8.0 or higher.

## Contributing

Contributions are welcome! Feel free to open issues or submit pull requests to improve the library.

## License

This project is licensed under the MIT License.

## Support

For questions or support, please create an issue in the GitHub repository.

