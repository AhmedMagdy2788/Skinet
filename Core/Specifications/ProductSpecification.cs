using System;
using Core.Entities;

namespace Core.Specifications;

public class ProductSpecification : BaseSpecification<Product>
{
    public ProductSpecification(ProductSpecParams specParams)
    : base(e =>
        (string.IsNullOrEmpty(specParams.Search) || e.Name.ToLower().Contains(specParams.Search)) &&
        (specParams.Brands.Count == 0 || specParams.Brands.Contains(e.Brand)) &&
        (specParams.Types.Count == 0 || specParams.Types.Contains(e.Type))
    )
    {
        ApplyPaging((specParams.PageIndex -1) * specParams.PageSize, specParams.PageSize);
        switch (specParams.Sort)
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

public class BrandListSpecification : BaseSpecification<Product, string>
{
    public BrandListSpecification()
    {
        AddSelect(e => e.Brand);
        ApplyDistinct();
    }
}

public class TypeListSpecification : BaseSpecification<Product, string>
{
    public TypeListSpecification()
    {
        AddSelect(e => e.Type);
        ApplyDistinct();
    }
}