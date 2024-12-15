namespace Assignment.Domain.Entities;

public class Product
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public QuantityPerUnit QuantityPerUnit { get; set; }
    public int ReorderLevel { get; set; }
    public decimal UnitPrice { get; set; }
    public int UnitsInStock { get; set; }
    public int UnitsOnOrder { get; set; }

    public int SupplierId { get; set; }
    public Supplier? Supplier { get; set; }
}

public enum QuantityPerUnit
{
    Kilo,
    Box,
    Can,
    Liter,
    Bottle,
}
