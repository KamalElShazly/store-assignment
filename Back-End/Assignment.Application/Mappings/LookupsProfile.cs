using Assignment.Application.DTOs.Lookup;
using Assignment.Application.DTOs.Product;
using Assignment.Domain.Entities;
using AutoMapper;

namespace Assignment.Application.Mappings;

public class LookupsProfile : Profile
{
    public LookupsProfile()
    {
        CreateMap<Supplier, SupplierLookupResponse>();
    }
}
