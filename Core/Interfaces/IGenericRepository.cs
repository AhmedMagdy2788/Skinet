using System;
using Core.Entities;

namespace Core.Interfaces;

public interface IGenericRepository<E> where E : BaseEntity
{
    Task<E?> GetByIdAsync(int id);
    Task<IReadOnlyList<E>> ListAllAsync();
    Task<E?> GetEntityWithSpec(ISpecification<E> spec);
    Task<IReadOnlyList<E>> ListWithSpecAsync(ISpecification<E> spec);
    Task<EResult?> GetEntityWithSpec<EResult>(ISpecification<E, EResult> spec);
    Task<IReadOnlyList<EResult>> ListWithSpecAsync<EResult>(
        ISpecification<E, EResult> spec);
    void Add(E Entity);
    void Update(E Entity);
    void Remove(E Entity);
    bool IsExists(int id);
    Task<bool> SaveAllAsync();
    Task<int> CountAsync(ISpecification<E> spec);
}
