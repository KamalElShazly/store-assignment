using System.Data;
using Assignment.Application.DTOs.Lookup;
using Assignment.Domain.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Application.Services;

public class LookupsService(IRepository<Supplier> suppliersRepository, IMapper mapper)
    : ILookupsService
{
    private readonly IRepository<Supplier> _suppliersRepository = suppliersRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<SupplierLookupResponse>> GetAllSuppliers()
    {
        var suppliers = await _suppliersRepository.GetAll().ToListAsync();
        return suppliers.Select(x => _mapper.Map<SupplierLookupResponse>(x));
    }
}
