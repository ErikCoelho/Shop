using Microsoft.EntityFrameworkCore;
using Shop.Domain.Handlers;
using Shop.Domain.Infra.Contexts;
using Shop.Domain.Infra.Repositories;
using Shop.Domain.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("Database"));
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<DataContext>(opt => opt.UseSqlServer(connectionString));

builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<IDeliveryFeeRepository, DeliveryFeeRepository>();

builder.Services.AddTransient<OrderHandler, OrderHandler>();
builder.Services.AddTransient<ProductHandler, ProductHandler>();
builder.Services.AddTransient<CustomerHandler, CustomerHandler>();


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

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
