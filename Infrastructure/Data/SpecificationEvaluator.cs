using System;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data;

public class SpecificationEvaluator<E> where E : BaseEntity
{
    public static IQueryable<E> GetQuery(IQueryable<E> query, ISpecification<E> spec)
    {
        if (spec.Criteria != null)
        {
            query = query.Where(spec.Criteria);
        }
        if (spec.OrderBy != null)
        {
            query = query.OrderBy(spec.OrderBy);
        }
        if (spec.OrderByDescending != null)
        {
            query = query.OrderByDescending(spec.OrderByDescending);
        }
        if(spec.IsDistinct){
            query = query.Distinct();
        }
        return query;
    }

    public static IQueryable<EResult> GetQuery<ESpec, EResult>(IQueryable<E> query,
        ISpecification<E, EResult> spec)
    {
        if (spec.Criteria != null)
        {
            query = query.Where(spec.Criteria);
        }
        if (spec.OrderBy != null)
        {
            query = query.OrderBy(spec.OrderBy);
        }
        if (spec.OrderByDescending != null)
        {
            query = query.OrderByDescending(spec.OrderByDescending);
        }
        var selectQuery = query as IQueryable<EResult>;
        if (spec.Select != null)
        {
            selectQuery = query.Select(spec.Select).Distinct();
        }
        if(spec.IsDistinct){
            selectQuery = selectQuery?.Distinct();
        }
        return selectQuery ?? query.Cast<EResult>();
    }
}
