using Assignment.Application.DTOs.Product;
using Assignment.Domain.Entities;
using AutoMapper;

namespace Assignment.Application.Mappings;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<ModifyProductRequest, Product>();
        CreateMap<Product, ProductResponse>();
    }
}
