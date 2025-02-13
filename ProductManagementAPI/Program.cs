using Application.DTO;
using Application.Mappers;
using Application.Validators;
using Core.Contracts;
using Core.Implementation;
using FluentValidation;
using Infrastructure.Contracts;
using Infrastructure.Data;
using Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;
using ProductManagementAPI.Middleware;
using Repository.Contracts;
using Repository.Data.Entities;
using Repository.Implementation;
using System.Text.Json.Serialization;
using Serilog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Configurar Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information() // Define el nivel mínimo de log (puedes usar Debug, Warning, Error, etc.)
    .WriteTo.Console() // Envía logs a la consola
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day) // Envía logs a archivos con rotación diaria
    .CreateLogger();

// Reemplaza el logger predeterminado de ASP.NET Core con Serilog
builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// JWT AUTHENTICATION
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option => {
        option.RequireHttpsMetadata = false;
        option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "ProductManagement.Issuer",
            ValidAudience = "ProductManagement.Audience",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MiClaveSecretaSCS_JWT2025ProductManagement"))
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("OnlyAdmin", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
    options.AddPolicy("OnlyBoss", policy => policy.RequireClaim(ClaimTypes.Role, "Boss"));
    options.AddPolicy("OnlyUser", policy => policy.RequireClaim(ClaimTypes.Role, "User"));
    options.AddPolicy("OnlyClient", policy => policy.RequireClaim(ClaimTypes.Role, "Client"));
});

// ENTITY FRAMEWORK - DBContext
builder.Services.AddDbContext<StoreContext>((options) =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("storeConnection"));
});

// Automappers
builder.Services.AddAutoMapper(typeof(MappingProfile));

// MEDIATR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(Application.Commands.CreateProductCommand).Assembly)
);

// Dependency injection - Repository
builder.Services.AddScoped<IProductRepository, ProductRepositoryImpl>();
builder.Services.AddScoped<IUserRepository, UserRepositoryImpl>();

// Dependency injection - Core
builder.Services.AddScoped<IProductCore, ProductCoreImpl>();
builder.Services.AddScoped<IUserCore, UserCoreImpl>();

// Email service
builder.Services.AddScoped<IEmailService, EmailService>();

// CORS Configuration
builder.Services.AddCors(c =>
    c.AddPolicy("Cors", builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    })
);

builder.Services.AddControllers()
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// FluentValidator
builder.Services.AddScoped<IValidator<CreateProductDTO>, ProductCreateValidator>();
builder.Services.AddScoped<IValidator<UpdateProductDTO>, ProductUpdateValidator>();

// INFRASTRUCTURE PROPERTIES
builder.Services.Configure<InfrastructureProperties>(builder.Configuration.GetSection("InfrastructureProperties"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseMiddleware<ProductMiddleware>();

app.UseHttpsRedirection();

// JWT
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
