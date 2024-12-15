using Assignment.Application.DTOs.Supplier;

namespace Assignment.Application.Services;

public interface ISuppliersService
{
    public Task<IEnumerable<SupplierResponse>> GetAll(int page, int pageSize);
    public Task<SupplierResponse?> GetById(int id);
    public Task Create(ModifySupplierRequest dto);
    public Task Update(int id, ModifySupplierRequest dto);
    public Task Delete(int id);
    public Task<int> GetCount();
}
