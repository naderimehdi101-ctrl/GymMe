using System;

namespace GymMe;

public class PagedResponse<T>
{
    public List<T> Items { get; set; } = new List<T>();
    
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    
    public int TotalCount { get; set; }
    public int  TotalPages => (TotalCount + PageSize - 1) / PageSize;
}