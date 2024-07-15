namespace AutoMarket.Infrastructure.Pagination;

public class SerializablePagedList<T> : PaginationMetaData
{
    public IReadOnlyCollection<T> Items { get; set; } = new List<T>();
}


public class PaginationMetaData
{
    public int FirstItemOnPage { get; set; }

    public int LastItemOnPage { get; set; }

    public bool HasNextPage { get; set; }

    public bool HasPreviousPage { get; set; }

    public bool IsFirstPage { get; set; }

    public bool IsLastPage { get; set; }

    public int PageCount { get; set; }

    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public int TotalItemCount { get; set; }
}