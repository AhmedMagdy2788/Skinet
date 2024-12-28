using System;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class GenericRepository<E>(StoreContext context) : IGenericRepository<E> where E : BaseEntity
{
    public void Add(E Entity)
    {
        context.Set<E>().Add(Entity);
    }

    public async Task<E?> GetByIdAsync(int id)
    {
        return await context.Set<E>().FindAsync(id);
    }

    public async Task<IReadOnlyList<E>> ListAllAsync()
    {
        return await context.Set<E>().ToListAsync();
    }

    public void Update(E Entity)
    {
        context.Entry(Entity).State = EntityState.Modified;
    }
    public void Remove(E Entity)
    {
        context.Set<E>().Remove(Entity);
    }

    public async Task<bool> SaveAllAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public bool IsExists(int id)
    {
        return context.Set<E>().Any(e => e.Id == id);
    }

    public async Task<E?> GetEntityWithSpec(ISpecification<E> spec)
    {
        return await ApplySpecification(spec).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<E>> ListWithSpecAsync(ISpecification<E> spec)
    {
        return await ApplySpecification(spec).ToListAsync();
    }

    private IQueryable<E> ApplySpecification(ISpecification<E> spec)
    {
        return SpecificationEvaluator<E>.GetQuery(context.Set<E>().AsQueryable(), spec);
    }

    public async Task<EResult?> GetEntityWithSpec<EResult>(ISpecification<E, EResult> spec)
    {
        return await ApplySpecification(spec).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<EResult>> ListWithSpecAsync<EResult>(ISpecification<E, EResult> spec)
    {
        return await ApplySpecification(spec).ToListAsync();
    }
    private IQueryable<EResult> ApplySpecification<EResult>(
        ISpecification<E, EResult> spec)
    {
        return SpecificationEvaluator<E>.GetQuery<E, EResult>(context.Set<E>().AsQueryable(), spec);
    }

    public async Task<int> CountAsync(ISpecification<E> spec)
    {
        var evaluator = new SpecificationEvaluator<E>();
        return await evaluator.ApplyCriteria(context.Set<E>().AsQueryable(), spec);
        // var query = context.Set<E>().AsQueryable();
        // query = spec.ApplyCriteria(query);
        // return await query.CountAsync();
    }
}

public class SpecificationEvaluator<E> where E : BaseEntity
{
    public async Task<int> ApplyCriteria(IQueryable<E> query, ISpecification<E> spec)
    {
        if (spec.Criteria != null)
        {
            query = query.Where(spec.Criteria);
        }
        return await query.CountAsync();
    }
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
        if (spec.IsDistinct)
        {
            query = query.Distinct();
        }
        if (spec.IsPagingEnabled)
        {

            if (spec.Skip > 0)
            {
                query = query.Skip(spec.Skip);
            }
            if (spec.Take > 0)
            {
                query = query.Take(spec.Take);
            }
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
            selectQuery = query.Select(spec.Select);
        }
        if (spec.IsDistinct)
        {
            selectQuery = selectQuery?.Distinct();
        }
        if (spec.IsPagingEnabled)
        {

            if (spec.Skip > 0)
            {
                selectQuery = selectQuery?.Skip(spec.Skip);
            }
            if (spec.Take > 0)
            {
                selectQuery = selectQuery?.Take(spec.Take);
            }
        }
        return selectQuery ?? query.Cast<EResult>();
    }
}
