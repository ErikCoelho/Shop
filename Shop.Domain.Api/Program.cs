using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shop.Domain.Api;
using Shop.Domain.Handlers;
using Shop.Domain.Infra.Caching;
using Shop.Domain.Infra.Contexts;
using Shop.Domain.Infra.Repositories;
using Shop.Domain.Repositories;
using Shop.Domain.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

ConfigureAuthentication(builder);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("Database"));
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(opt => opt.UseSqlServer(connectionString));

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "instance";
});

ConfigureServices(builder);


builder.Services.AddCors(options =>
{
    options.AddPolicy("DevPolicy",
        builder => builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("DevPolicy");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();


void ConfigureAuthentication(WebApplicationBuilder builder)
{
    var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);

    builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(x =>
    {
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
}

void ConfigureServices(WebApplicationBuilder builder)
{
    builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
    builder.Services.AddTransient<IProductRepository, ProductRepository>();
    builder.Services.AddTransient<IOrderRepository, OrderRepository>();
    builder.Services.AddTransient<IDeliveryFeeRepository, DeliveryFeeRepository>();
    builder.Services.AddTransient<ICachingService, CachingService>();


    builder.Services.AddTransient<OrderHandler, OrderHandler>();
    builder.Services.AddTransient<ProductHandler, ProductHandler>();
    builder.Services.AddTransient<CustomerHandler, CustomerHandler>();
    builder.Services.AddTransient<TokenService>();
}