namespace Assignment.Application.DTOs;

public class PaginationResponse<TEntity>(IEnumerable<TEntity> items, int totalCount)
    where TEntity : class
{
    public IEnumerable<TEntity> Items { get; set; } = items;
    public int TotalCount { get; set; } = totalCount;
}
