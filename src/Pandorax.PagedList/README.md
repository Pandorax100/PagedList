# Pandorax.PagedList

`Pandorax.PagedList` is a lightweight library for handling paginated data in C# applications. It provides easy-to-use extension methods for `IEnumerable<T>` to simplify working with paginated data.

## Installation

You can install the library via NuGet:

```bash
Install-Package Pandorax.PagedList
```

## Features

- Extension methods to create paginated lists from any `IEnumerable<T>`.
- Easy-to-use API with minimal configuration.
- Useful for building pagination functionality in web applications, APIs, or any project where data paging is required.

## Usage

### Example: Converting an `IEnumerable<T>` to a Paged List

The library provides an extension method, `ToPagedList<T>`, which creates a paged list from an `IEnumerable<T>`.

```csharp
using System;
using System.Collections.Generic;
using Pandorax.PagedList;

class Program
{
    static void Main()
    {
        IEnumerable<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        int pageIndex = 1; // Page number (1-based index)
        int pageSize = 3;  // Number of items per page

        var pagedList = numbers.ToPagedList(pageIndex, pageSize);

        Console.WriteLine($"Page {pageIndex} of {pagedList.TotalPages}");
        foreach (var number in pagedList)
        {
            Console.WriteLine(number);
        }
    }
}
```

### IPagedList<T> Interface

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

## Contributing

Contributions are welcome! Feel free to open issues or submit pull requests to improve the library.

## License

This project is licensed under the MIT License.

## Support

For questions or support, please create an issue in the GitHub repository.

