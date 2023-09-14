namespace TestPlatform.Services.Models;

internal class PaginationMetadata
{
    public PaginationMetadata(
        int totalCount,
        int currentPage,
        int pageSize)
    {
        TotalCount = totalCount;
        CurrentPage = currentPage;
        PageCount = (int)Math.Ceiling(1.0 * totalCount / pageSize);
    }

    public int TotalCount { get; set; }
    public int CurrentPage { get; set; }
    public int PageCount { get; set; }
    public bool HasNextPage => CurrentPage < PageCount;
    public bool HasPreviousPage => CurrentPage > 1 && CurrentPage <= PageCount + 1;
}