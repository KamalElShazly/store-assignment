using Assignment.Application.DTOs;
using Assignment.Application.DTOs.Product;
using Assignment.Application.Services;
using Assignment.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductsService service) : ControllerBase
{
    private readonly IProductsService _service = service;

    /// <summary>
    /// Get all products.
    /// </summary>
    /// <param name="page">Page</param>
    /// <param name="pageSize">Page Size</param>
    /// <returns>Products list</returns>
    [HttpGet]
    [ProducesResponseType(
        StatusCodes.Status200OK,
        Type = typeof(PaginationResponse<ProductResponse>)
    )]
    [Produces("application/json")]
    public async Task<IActionResult> GetAll([FromQuery] int page, [FromQuery] int pageSize)
    {
        var products = await _service.GetAll(page, pageSize);
        var totalCount = await _service.GetCount();
        var response = new PaginationResponse<ProductResponse>(products, totalCount);
        return Ok(response);
    }

    /// <summary>
    /// Get a product by ID.
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <returns>Product object</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Produces("application/json")]
    public async Task<IActionResult> Get(int id)
    {
        var product = await _service.GetById(id);
        return Ok(product);
    }

    /// <summary>
    /// Create product.
    /// </summary>
    /// <param name="request">ModifyProductRequest</param>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [Produces("application/json")]
    public async Task<IActionResult> Create(ModifyProductRequest request)
    {
        await _service.Create(request);
        return Created();
    }

    /// <summary>
    /// Update product.
    /// </summary>
    /// <param name="id">Id</param>
    /// <param name="request">ModifyProductRequest</param>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Produces("application/json")]
    public async Task<IActionResult> Update(int id, ModifyProductRequest request)
    {
        await _service.Update(id, request);
        return NoContent();
    }

    /// <summary>
    /// Delete product.
    /// </summary>
    /// <param name="id">Id</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Produces("application/json")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.Delete(id);
        return NoContent();
    }
}
