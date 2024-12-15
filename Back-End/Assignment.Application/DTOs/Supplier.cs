using System.ComponentModel.DataAnnotations;

namespace Assignment.Application.DTOs.Supplier;

public class ModifySupplierRequest
{
    // [Required]
    // [StringLength(100, MinimumLength = 3)]
    public required string Name { get; set; }
}

public class SupplierResponse
{
    public int Id { get; set; }
    public required string Name { get; set; }
}
