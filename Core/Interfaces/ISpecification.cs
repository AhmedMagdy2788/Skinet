using System;
using System.Linq.Expressions;

namespace Core.Interfaces;

public interface ISpecification<E>
{
    Expression<Func<E, bool>>? Criteria { get; }
    Expression<Func<E, object>>? OrderBy { get; }
    Expression<Func<E, object>>? OrderByDescending { get; }
    bool IsDistinct{ get; }
}
public interface ISpecification<E, EResult>: ISpecification<E>{
    Expression<Func<E, EResult>>? Select { get; }
}