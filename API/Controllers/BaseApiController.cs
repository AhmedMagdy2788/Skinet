using System;
using API.RequestHelpers;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseApiController() : ControllerBase
{
    protected async Task<Pagination<E>> CreatePagedResult<E>(IGenericRepository<E> repo,
        ISpecification<E> spec, int pageIndex, int pageSize) where E : BaseEntity
    {
        var items = await repo.ListWithSpecAsync(spec);
        var count = await repo.CountAsync(spec);
        return new Pagination<E>(pageIndex, pageSize, count, items);
    }
}
