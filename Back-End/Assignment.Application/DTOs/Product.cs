using System.ComponentModel.DataAnnotations;
using Assignment.Domain.Entities;

namespace Assignment.Application.DTOs.Product;

public class ModifyProductRequest
{
    // [Required]
    // [StringLength(100, MinimumLength = 3)]
    public required string Name { get; set; }

    // [Required]
    public QuantityPerUnit QuantityPerUnit { get; set; }

    // [Required]
    // [Range(0, int.MaxValue)]
    public int ReorderLevel { get; set; }

    // [Required]
    // [Range(0, double.MaxValue)]
    public decimal UnitPrice { get; set; }

    // [Required]
    // [Range(0, int.MaxValue)]
    public int UnitsInStock { get; set; }

    // [Required]
    // [Range(0, int.MaxValue)]
    public int UnitsOnOrder { get; set; }

    // [Required]
    public int SupplierId { get; set; }
}

public class ProductResponse
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public QuantityPerUnit QuantityPerUnit { get; set; }
    public int ReorderLevel { get; set; }
    public decimal UnitPrice { get; set; }
    public int UnitsInStock { get; set; }
    public int UnitsOnOrder { get; set; }
    public int SupplierId { get; set; }
}
