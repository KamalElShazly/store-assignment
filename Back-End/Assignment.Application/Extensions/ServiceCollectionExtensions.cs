using System.Reflection;
using Assignment.Application.Services;
using Assignment.Application.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Assignment.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddScoped<IProductsService, ProductsService>();
        services.AddScoped<ISuppliersService, SuppliersService>();
        services.AddScoped<ILookupsService, LookupsService>();
        services.AddScoped<IStatisticsService, StatisticsService>();

        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<ModifyProductRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<ModifySupplierRequestValidator>();
    }
}
