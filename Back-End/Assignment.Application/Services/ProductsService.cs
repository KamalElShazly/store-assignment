using System.Data;
using Assignment.Application.DTOs.Product;
using Assignment.Domain.Entities;
using Assignment.Domain.Exceptions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Application.Services;

public class ProductsService(
    IRepository<Product> repository,
    IUnitOfWork unitOfWork,
    IMapper mapper
) : IProductsService
{
    private readonly IRepository<Product> _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<ProductResponse>> GetAll(int page, int pageSize)
    {
        var products = await _repository
            .GetAll()
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return products.Select(x => _mapper.Map<ProductResponse>(x));
    }

    public async Task<ProductResponse?> GetById(int id)
    {
        var product = await _repository.GetByIdAsync(id);
        if (product == null)
            throw new NotFoundException();
        else
            return _mapper.Map<ProductResponse>(product);
    }

    public async Task Create(ModifyProductRequest dto)
    {
        var product = _mapper.Map<Product>(dto);
        await _repository.InsertAsync(product);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task Update(int id, ModifyProductRequest dto)
    {
        var product = await _repository.GetByIdAsync(id);
        if (product == null)
            throw new NotFoundException();
        else
        {
            product.Name = dto.Name;
            product.QuantityPerUnit = dto.QuantityPerUnit;
            product.ReorderLevel = dto.ReorderLevel;
            product.UnitPrice = dto.UnitPrice;
            product.UnitsInStock = dto.UnitsInStock;
            product.UnitsOnOrder = dto.UnitsOnOrder;
            product.SupplierId = dto.SupplierId;

            _repository.Update(product);
            await _unitOfWork.SaveChangesAsync();
        }
    }

    public async Task Delete(int id)
    {
        var product = await _repository.GetByIdAsync(id);
        if (product == null)
            throw new NotFoundException();
        else
        {
            _repository.Delete(product);
            await _unitOfWork.SaveChangesAsync();
        }
    }

    public async Task<int> GetCount()
    {
        return await _repository.GetCountAsync();
    }
}
