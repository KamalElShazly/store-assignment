namespace Assignment.Application.DTOs.Statistics;

public class SupplierWithMostProductsResponse
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int ProductsCount { get; set; }
}
