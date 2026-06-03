<<<<<<< HEAD
using Application.Interfaces.Handlers.Stock;
using Application.Interfaces.Repositories;
using Application.UseCases.Stock.Handlers;
using Infrastructure.Repositories;
=======
using Application.Interfaces.Handlers.Ingredient;
using Application.Interfaces.Handlers.IngredientDish;
using Application.Interfaces.Repositories;
using Application.UseCases.Ingredient.Handlers;
using Application.UseCases.IngredientDish.Handlers;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
>>>>>>> main

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

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Repositories
builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();
builder.Services.AddScoped<IIngredientDishRepository, IngredientDishRepository>();

//Handlers
builder.Services.AddScoped<ICreateIngredientHandler, CreateIngredientHandler>();
builder.Services.AddScoped<IDeleteIngredientHandler, DeleteIngredientHandler>();
builder.Services.AddScoped<IUpdateIngredientHandler, UpdateIngredientHandler>();
builder.Services.AddScoped<IGetAllIngredientHandler, GetAllIngredientHandler>();
builder.Services.AddScoped<IGetByIdIngredientHandler, GetByIdIngredientHandler>();


builder.Services.AddScoped<ICreateIngredientDishHandler, CreateIngredientDishHandler>();
builder.Services.AddScoped<IDeleteIngredientDishHandler, DeleteIngredientDishHandler>();
builder.Services.AddScoped<IGetAllIngredientDishHandler, GetAllIngredientDishHandler>();
builder.Services.AddScoped<IGetByIdIngredientDishHandler, GetByIdIngredientDishHandler>();





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
