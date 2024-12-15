using Assignment.Application.DTOs.Product;

namespace Assignment.Application.Services;

public interface IProductsService
{
    public Task<IEnumerable<ProductResponse>> GetAll(int page, int pageSize);
    public Task<ProductResponse?> GetById(int id);
    public Task Create(ModifyProductRequest dto);
    public Task Update(int id, ModifyProductRequest dto);
    public Task Delete(int id);
    public Task<int> GetCount();
}
