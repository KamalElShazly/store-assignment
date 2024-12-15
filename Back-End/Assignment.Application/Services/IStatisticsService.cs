using Assignment.Application.DTOs.Product;
using Assignment.Application.DTOs.Statistics;

namespace Assignment.Application.Services;

public interface IStatisticsService
{
    public Task<IEnumerable<ProductResponse>> GetProductsToRestock();
    public Task<IEnumerable<SupplierWithMostProductsResponse>> GetSuppliersWithMostProducts();
    public Task<IEnumerable<ProductResponse>> GetProductsWithMinimumOrders();
}
