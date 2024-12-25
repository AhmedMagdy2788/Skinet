using System;
using Core.Entities;

namespace Core.Specifications;

public class ProductSpecification : BaseSpecification<Product>
{
    public ProductSpecification(string? brand, string? type, string? sort)
    : base(e =>
        (string.IsNullOrWhiteSpace(brand) || e.Brand == brand) &&
        (string.IsNullOrWhiteSpace(type) || e.Type == type)
    )
    {
        switch (sort)
        {
            case "priceAsc":
                AddOrderBy(e => e.Price);
                break;
            case "priceDesc":
                AddOrderByDescending(e => e.Price);
                break;
            default:
                AddOrderBy(e => e.Name);
                break;
        }
    }
}
