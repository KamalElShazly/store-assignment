using Assignment.Application.DTOs.Lookup;

namespace Assignment.Application.Services;

public interface ILookupsService
{
    public Task<IEnumerable<SupplierLookupResponse>> GetAllSuppliers();
}
