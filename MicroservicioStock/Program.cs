using Application.Interfaces.Handlers.Ingredient;
using Application.Interfaces.Handlers.IngredientDish;
using Application.Interfaces.Handlers.Stock;
using Application.Interfaces.Repositories;
using Application.UseCases.Ingredient.Handlers;
using Application.UseCases.IngredientDish.Handlers;
using Application.UseCases.Stock.Handlers;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Seed;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MicroservicioStock.Middlewares;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICreateStockHandler, CreateStockHandler>();
builder.Services.AddScoped<IUpdateStockHandler, UpdateStockHandler>();
builder.Services.AddScoped<IDeleteStockHandler, DeleteStockHandler>();
builder.Services.AddScoped<IGetAllStockHandler, GetAllStockHandler>();
builder.Services.AddScoped<IGetByIdStockHandler, GetByIdStockHandler>();
builder.Services.AddScoped<IConsumeStockForOrderHandler, ConsumeStockForOrderHandler>();
builder.Services.AddScoped<IReleaseStockForOrderHandler, ReleaseStockForOrderHandler>();

builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddScoped<IStockConsumptionRepository, StockConsumptionRepository>();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingrese: Bearer {token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? string.Empty)),
            RoleClaimType = ClaimTypes.Role
        };
    });

builder.Services.AddAuthorization();

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

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await dbContext.Database.MigrateAsync();
    await StockDbSeeder.SeedAsync(dbContext);
}

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
