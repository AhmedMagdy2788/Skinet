using System;
using System.Linq.Expressions;
using Core.Entities;
using Core.Interfaces;

namespace Core.Specifications;

public class BaseSpecification<E>(
    Expression<Func<E, bool>>? criteria)
    : ISpecification<E>
{
    protected BaseSpecification() : this(null) { }
    public Expression<Func<E, bool>>? Criteria => criteria;

    public Expression<Func<E, object>>? OrderBy { get; private set; }

    public Expression<Func<E, object>>? OrderByDescending { get; private set; }

    public bool IsDistinct{ get; private set; }

    protected void AddOrderBy(Expression<Func<E, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }
    protected void AddOrderByDescending(Expression<Func<E, object>> orderByDescExpression)
    {
        OrderByDescending = orderByDescExpression;
    }
    protected void ApplyDistinct(){
        IsDistinct = true;
    }
}
public class BaseSpecification<E, EResult>(
    Expression<Func<E, bool>>? criteria)
    : BaseSpecification<E>(criteria), ISpecification<E, EResult>
{
    protected BaseSpecification() : this(null) { }
    public Expression<Func<E, EResult>>? Select { get; private set; }
    protected void AddSelect(Expression<Func<E, EResult>> select)
    {
        Select = select;
    }
}