using Assignment.Application.DTOs;
using Assignment.Application.DTOs.Lookup;
using Assignment.Application.DTOs.Supplier;
using Assignment.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LookupsController(ILookupsService service) : ControllerBase
{
    private readonly ILookupsService _service = service;

    /// <summary>
    /// Get all suppliers.
    /// </summary>
    /// <returns>Suppliers list</returns>
    [HttpGet]
    [Route("Suppliers")]
    [ProducesResponseType(
        StatusCodes.Status200OK,
        Type = typeof(IEnumerable<SupplierLookupResponse>)
    )]
    [Produces("application/json")]
    public async Task<IActionResult> GetAllSuppliers()
    {
        var suppliers = await _service.GetAllSuppliers();
        return Ok(suppliers);
    }
}
