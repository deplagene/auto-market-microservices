using Microsoft.EntityFrameworkCore;

namespace AutoMarket.Infrastructure.Pagination;

public static class QueryableExtension
{
    public static async Task<SerializablePagedList<T>> ToPagedListAsync<T>(
        this IQueryable<T> source,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        if(pageNumber < 1)
            throw new ArgumentException($"Parameter {nameof(pageNumber)} must be greater than or equal to 1.", nameof(pageNumber));

        if(pageSize < 1)
            throw new ArgumentException($"Parameter {nameof(pageSize)} must be greater than or equal to 1.", nameof(pageSize));

        var serializablePagedList = new SerializablePagedList<T>
        {
            Items = await source
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken),

            TotalItemCount =
                await source.CountAsync(cancellationToken),

            PageSize = pageSize,
            PageNumber = pageNumber,
            FirstItemOnPage = (pageNumber - 1) * pageSize + 1
        };
        serializablePagedList.LastItemOnPage =
            serializablePagedList.FirstItemOnPage + pageSize - 1 > serializablePagedList.TotalItemCount ?
                serializablePagedList.TotalItemCount :
                serializablePagedList.FirstItemOnPage + pageSize - 1;

        serializablePagedList.PageCount = serializablePagedList.TotalItemCount > 0 ?
            (int)Math.Ceiling(serializablePagedList.TotalItemCount / (double)serializablePagedList.PageSize) : 0;

        serializablePagedList.IsFirstPage = pageNumber == 1;
        serializablePagedList.IsLastPage = pageNumber >= serializablePagedList.PageCount;

        serializablePagedList.HasNextPage = pageNumber < serializablePagedList.PageCount;
        serializablePagedList.HasPreviousPage = pageNumber > 1;

        return serializablePagedList;
    }
}