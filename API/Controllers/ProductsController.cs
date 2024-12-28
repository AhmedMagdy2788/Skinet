using System;
using API.RequestHelpers;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;


public class ProductsController(IGenericRepository<Product> genericRepository)
    : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<Pagination<Product>>> GetProducts(
        [FromQuery] ProductSpecParams specParams)
    {
        var spec = new ProductSpecification(specParams);
        var pagination = await CreatePagedResult<Product>(genericRepository, spec, specParams.PageIndex, specParams.PageSize);
        return Ok(pagination);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await genericRepository.GetByIdAsync(id);
        if (product == null) return NotFound();
        return Ok(product);
    }
    [HttpGet("Brands")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
    {
        var spec = new BrandListSpecification();
        return Ok(await genericRepository.ListWithSpecAsync(spec));
    }
    [HttpGet("Types")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
    {
        var spec = new TypeListSpecification();
        return Ok(await genericRepository.ListWithSpecAsync(spec));
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        genericRepository.Add(product);
        if (await genericRepository.SaveAllAsync())
            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        return BadRequest("Problem creating product");
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProduct(int id, Product product)
    {
        if (product.Id != id || !ProductExists(id)) return BadRequest("Cannot update this product");

        genericRepository.Update(product);
        if (await genericRepository.SaveAllAsync())
            return NoContent();
        return BadRequest("Problem updating he product");
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await genericRepository.GetByIdAsync(id);
        if (product == null) return NotFound();
        genericRepository.Remove(product);
        if (await genericRepository.SaveAllAsync())
            return NoContent();
        return BadRequest("Problem deleting he product");
    }

    private bool ProductExists(int id)
    {
        return genericRepository.IsExists(id);
    }
}
