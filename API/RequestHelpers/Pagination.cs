using System;

namespace API.RequestHelpers;

public class Pagination<E>(int pageIndex, int pageSize,
    int count, IReadOnlyList<E> data)
{
    public int PageIndex { get; set; } = pageIndex;
    public int PageSize { get; set; } = pageSize;
    public int Count { get; set; } = count;
    public IReadOnlyList<E> Data { get; set; } = data;

}
