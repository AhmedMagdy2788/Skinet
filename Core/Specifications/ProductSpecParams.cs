using System;

namespace Core.Specifications;

public class ProductSpecParams
{
    private const int MaxPageSize = 50;
    public int PageIndex { get; set; } = 1;
    private int _pageSize = 6;
    public int PageSize
    {
        get { return _pageSize; }
        set { _pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
    }

    private List<string> _brands = [];
    public List<string> Brands
    {
        get => _brands;
        set
        {
            _brands = [.. value.SelectMany(e => e.Split(',',
                StringSplitOptions.RemoveEmptyEntries))];
        }
    }
    private List<string> _types = [];
    public List<string> Types
    {
        get => _types;
        set
        {
            _types = [.. value.SelectMany(e => e.Split(',',
                StringSplitOptions.RemoveEmptyEntries))];
        }
    }
    public string? Sort { get; set; }
    private string? _search;
    public string Search
    {
        get { return _search ?? ""; }
        set { _search = value.ToLower(); }
    }

}
