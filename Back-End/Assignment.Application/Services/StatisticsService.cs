using System.Data;
using Assignment.Application.DTOs.Product;
using Assignment.Application.DTOs.Statistics;
using Assignment.Application.DTOs.Supplier;
using Assignment.Domain.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Application.Services;

public class StatisticsService(
    IRepository<Product> productsRepository,
    IRepository<Supplier> suppliersRepository,
    IMapper mapper
) : IStatisticsService
{
    private readonly IRepository<Product> _productsRepository = productsRepository;
    private readonly IRepository<Supplier> _suppliersRepository = suppliersRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<ProductResponse>> GetProductsToRestock()
    {
        var products = await _productsRepository
            .GetAll()
            .Where(x => x.UnitsInStock <= x.ReorderLevel)
            .OrderBy(x => x.UnitsInStock)
            .ToListAsync();

        return products.Select(x => _mapper.Map<ProductResponse>(x));
    }

    public async Task<IEnumerable<SupplierWithMostProductsResponse>> GetSuppliersWithMostProducts()
    {
        var result = await _suppliersRepository
            .GetAll()
            .Select(x => new SupplierWithMostProductsResponse
            {
                Id = x.Id,
                Name = x.Name,
                ProductsCount = x.Products.Count(),
            })
            .OrderByDescending(x => x.ProductsCount)
            .ToListAsync();

        return result;
    }

    public async Task<IEnumerable<ProductResponse>> GetProductsWithMinimumOrders()
    {
        var products = await _productsRepository
            .GetAll()
            .OrderBy(x => x.UnitsOnOrder)
            .ToListAsync();

        return products.Select(x => _mapper.Map<ProductResponse>(x));
    }
}
