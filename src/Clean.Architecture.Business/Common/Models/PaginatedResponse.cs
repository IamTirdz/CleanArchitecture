namespace Clean.Architecture.Business.Common.Models;

public class PaginatedResponse<T>
{
    public PaginatedResponse(List<T> source, int? pageNumber = 1, int? pageSize = 10)
    {
        var totalCount = source.Count;
        var items = source.Skip((pageNumber!.Value - 1) * pageSize!.Value).Take(pageSize!.Value).ToList();

        Data = items;
        PageNumber = pageNumber!.Value;
        PageSize = pageSize!.Value;
        TotalCount = totalCount;
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
    }

    public IEnumerable<T> Data { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
}
