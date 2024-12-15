using Assignment.Application.DTOs.Supplier;
using Assignment.Domain.Entities;
using AutoMapper;

namespace Assignment.Application.Mappings;

public class SupplierProfile : Profile
{
    public SupplierProfile()
    {
        CreateMap<ModifySupplierRequest, Supplier>();
        CreateMap<Supplier, SupplierResponse>();
    }
}
