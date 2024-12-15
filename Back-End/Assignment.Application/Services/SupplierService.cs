using System.Data;
using Assignment.Application.DTOs.Supplier;
using Assignment.Domain.Entities;
using Assignment.Domain.Exceptions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Application.Services;

public class SuppliersService(
    IRepository<Supplier> repository,
    IUnitOfWork unitOfWork,
    IMapper mapper
) : ISuppliersService
{
    private readonly IRepository<Supplier> _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<SupplierResponse>> GetAll(int page, int pageSize)
    {
        var suppliers = await _repository
            .GetAll()
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return suppliers.Select(x => _mapper.Map<SupplierResponse>(x));
    }

    public async Task<SupplierResponse?> GetById(int id)
    {
        var supplier = await _repository.GetByIdAsync(id);
        if (supplier == null)
            throw new NotFoundException();
        else
            return _mapper.Map<SupplierResponse>(supplier);
    }

    public async Task Create(ModifySupplierRequest dto)
    {
        var supplier = _mapper.Map<Supplier>(dto);
        await _repository.InsertAsync(supplier);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task Update(int id, ModifySupplierRequest dto)
    {
        var supplier = await _repository.GetByIdAsync(id);
        if (supplier == null)
            throw new NotFoundException();
        else
        {
            supplier.Name = dto.Name;

            _repository.Update(supplier);
            await _unitOfWork.SaveChangesAsync();
        }
    }

    public async Task Delete(int id)
    {
        var supplier = await _repository.GetByIdAsync(id);
        if (supplier == null)
            throw new NotFoundException();
        else
        {
            _repository.Delete(supplier);
            await _unitOfWork.SaveChangesAsync();
        }
    }

    public async Task<int> GetCount()
    {
        return await _repository.GetCountAsync();
    }
}
