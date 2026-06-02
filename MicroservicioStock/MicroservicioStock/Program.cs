using Application.Interfaces.Handlers.Stock;
using Application.Interfaces.Repositories;
using Application.UseCases.Stock.Handlers;
using Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICreateStockHandler, CreateStockHandler>();
builder.Services.AddScoped<IUpdateStockHandler, UpdateStockHandler>();
builder.Services.AddScoped<IDeleteStockHandler, DeleteStockHandler>();
builder.Services.AddScoped<IGetAllStockHandler, GetAllStockHandler>();
builder.Services.AddScoped<IGetByIdStockHandler, GetByIdStockHandler>();

builder.Services.AddScoped<IStockRepository, StockRepository>();

// Add services to the container.
builder.Services.AddControllers();
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
