using Assignment.Application.DTOs.Product;
using Assignment.Application.DTOs.Statistics;
using Assignment.Application.DTOs.Supplier;
using Assignment.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatisticsController(IStatisticsService service) : ControllerBase
{
    private readonly IStatisticsService _service = service;

    /// <summary>
    /// Get all products that needs restock.
    /// </summary>
    /// <returns>Products list</returns>
    [HttpGet]
    [Route("GetProductsToRestock")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductResponse>))]
    [Produces("application/json")]
    public async Task<IActionResult> GetProductsToRestock()
    {
        var products = await _service.GetProductsToRestock();
        return Ok(products);
    }

    /// <summary>
    /// Get all suppliers with most products.
    /// </summary>
    /// <returns>Suppliers list</returns>
    [HttpGet]
    [Route("GetSuppliersWithMostProducts")]
    [ProducesResponseType(
        StatusCodes.Status200OK,
        Type = typeof(IEnumerable<SupplierWithMostProductsResponse>)
    )]
    [Produces("application/json")]
    public async Task<IActionResult> GetSuppliersWithMostProducts()
    {
        var suppliers = await _service.GetSuppliersWithMostProducts();
        return Ok(suppliers);
    }

    /// <summary>
    /// Get all products with minimum orders.
    /// </summary>
    /// <returns>Products list</returns>
    [HttpGet]
    [Route("GetProductsWithMinimumOrders")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductResponse>))]
    [Produces("application/json")]
    public async Task<IActionResult> GetProductsWithMinimumOrders()
    {
        var products = await _service.GetProductsWithMinimumOrders();
        return Ok(products);
    }
}
