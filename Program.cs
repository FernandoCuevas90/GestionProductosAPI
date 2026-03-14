using GestionProductosAPI.Services;
using GestionProductosAPI.Data;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using FluentValidation.AspNetCore;
using GestionProductosAPI.Validators;
using GestionProductosAPI.DTOs;
using GestionProductosAPI.IRepository;
using GestionProductosAPI.Models;
using AutoMapper;
using GestionProductosAPI.Automappers;

var builder = WebApplication.CreateBuilder(args);


//Configurar DBContext con SQL Server

//Entity Framework
builder.Services.AddDbContext<GestionProductosContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Repository
builder.Services.AddScoped<IRepository<Producto>, ProductoRepository>();

//Inyectar el servicio ProductoService AQUI
builder.Services.AddKeyedScoped<ICommonService<ProductoDto, CreateProductoDto, UpdateProductoDto>, ProductoService>("productoService");


//Registrar FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<ProductoInsertValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ProductoUpdateValidator>();

builder.Services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ProductoInsertValidator>());

//Mappers
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MappingProfile>();
});

/*
 1. builder.Services, es el contenedor de dependencias de .NET
 2. AddSingleton, crea una sola instancia de ProductoService para toda la aplicación (ideal en este caso porque estamos simulando datos en memoria)
 3. "Cada vez que alguien pida un IProductoService, entrégale una instancia de ProductoService"
   IProductoService es la interfaz, (contrato) define qué puede hacer un servicio, pero no cómo lo hace
   ProductoService, es la implementación real del contrato, el contrato es la interfaz(IProductoService)
 4. IProductoService, ProductoService, le decimos a .NET que cuando alguien pida
   un IProductoService, debe entregar un ProductoService
   
1. builder.Services, es la caja de herramientas de .NET
2. AddSingleton, le dices "guarda esta herramienta y usa siempre la misma instancia"
3. IProductoService, es la "promesa" o contrato (qué puede hacer el servicio)
4. ProductoService, es la "herramienta" real (como lo hace)
 */

builder.Services.AddControllers(); //agrega soporte para controladores
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
