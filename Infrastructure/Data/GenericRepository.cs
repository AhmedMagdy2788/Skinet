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

    private IQueryable<E> ApplySpecification(ISpecification<E> spec){
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
}
