using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces.Repositories;
using Infrastructure.Repositories;
using Application.Interfaces.Handlers.Ingredient;
using Application.UseCases.Ingredient.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Repositories
builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();

//Handlers
builder.Services.AddScoped<ICreateIngredientHandler, CreateIngredientHandler>();
builder.Services.AddScoped<IDeleteIngredientHandler, DeleteIngredientHandler>();
builder.Services.AddScoped<IUpdateIngredientHandler, UpdateIngredientHandler>();
builder.Services.AddScoped<IGetAllIngredientHandler, GetAllIngredientHandler>();
builder.Services.AddScoped<IGetByIdIngredientHandler, GetByIdIngredientHandler>();





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
