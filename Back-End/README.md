dotnet new sln -n Assignment
dotnet new webapi --use-controllers -o Assignment.API
dotnet new classlib -o Assignment.Application
dotnet new classlib -o Assignment.Domain
dotnet new classlib -o Assignment.Infrastructure

dotnet sln add Assignment.API
dotnet sln add Assignment.Application
dotnet sln add Assignment.Domain
dotnet sln add Assignment.Infrastructure

dotnet add Assignment.API reference Assignment.Application
dotnet add Assignment.API reference Assignment.Infrastructure
dotnet add Assignment.Infrastructure reference Assignment.Application
dotnet add Assignment.Application reference Assignment.Domain

dotnet add Assignment.API package Microsoft.EntityFrameworkCore.Tools
dotnet add Assignment.Infrastructure package Microsoft.EntityFrameworkCore.SqlServer
dotnet add Assignment.Infrastructure package Microsoft.EntityFrameworkCore.Design
dotnet add Assignment.Application package Microsoft.EntityFrameworkCore

dotnet add Assignment.Application package AutoMapper
dotnet add Assignment.Application package FluentValidation.AspNetCore

dotnet ef migrations add Initial --project Assignment.Infrastructure --startup-project Assignment.API
dotnet ef database update --project Assignment.Infrastructure --startup-project Assignment.API

dotnet run --project Assignment.API --launch-profile "https"
