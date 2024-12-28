using System;
using System.Linq.Expressions;

namespace Core.Interfaces;

public interface ISpecification<E>
{
    Expression<Func<E, bool>>? Criteria { get; }
    Expression<Func<E, object>>? OrderBy { get; }
    Expression<Func<E, object>>? OrderByDescending { get; }
    bool IsDistinct{ get; }
    bool IsPagingEnabled{ get; }
    int Take { get; }
    int Skip { get; }
    // IQueryable<E> ApplyCriteria(IQueryable<E> query);
}
public interface ISpecification<E, EResult>: ISpecification<E>{
    Expression<Func<E, EResult>>? Select { get; }
}