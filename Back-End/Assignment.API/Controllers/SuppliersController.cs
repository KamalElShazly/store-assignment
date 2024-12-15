using Assignment.Application.DTOs;
using Assignment.Application.DTOs.Supplier;
using Assignment.Application.Services;
using Assignment.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SuppliersController(ISuppliersService service) : ControllerBase
{
    private readonly ISuppliersService _service = service;

    /// <summary>
    /// Get all suppliers.
    /// </summary>
    /// <param name="page">Page</param>
    /// <param name="pageSize">Page Size</param>
    /// <returns>Suppliers list</returns>
    [HttpGet]
    [ProducesResponseType(
        StatusCodes.Status200OK,
        Type = typeof(PaginationResponse<SupplierResponse>)
    )]
    [Produces("application/json")]
    public async Task<IActionResult> GetAll([FromQuery] int page, [FromQuery] int pageSize)
    {
        var suppliers = await _service.GetAll(page, pageSize);
        var totalCount = await _service.GetCount();
        var response = new PaginationResponse<SupplierResponse>(suppliers, totalCount);

        return Ok(response);
    }

    /// <summary>
    /// Get a supplier by ID.
    /// </summary>
    /// <param name="id">Supplier ID</param>
    /// <returns>Supplier object</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Supplier))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Produces("application/json")]
    public async Task<IActionResult> Get(int id)
    {
        var supplier = await _service.GetById(id);
        return Ok(supplier);
    }

    /// <summary>
    /// Create supplier.
    /// </summary>
    /// <param name="request">ModifySupplierRequest</param>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [Produces("application/json")]
    public async Task<IActionResult> Create(ModifySupplierRequest request)
    {
        await _service.Create(request);
        return Created();
    }

    /// <summary>
    /// Update supplier.
    /// </summary>
    /// <param name="id">Id</param>
    /// <param name="request">ModifySupplierRequest</param>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Produces("application/json")]
    public async Task<IActionResult> Update(int id, ModifySupplierRequest request)
    {
        await _service.Update(id, request);
        return NoContent();
    }

    /// <summary>
    /// Delete supplier.
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
